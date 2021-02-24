using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows;
using System.Diagnostics;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;
using System.ComponentModel;
using System.Windows.Forms.Integration;

namespace Lego_Island_Rebuler__With_an_ok_ui_i_think_
{
    /// <summary>
    /// Displays a WinForms.WebBrowser control over a given placement target element in a WPF Window.
    /// Applies the opacity of the Window to the WebBrowser control.
    /// </summary>
    public class OverlayWF
    {
        Window _owner;
        FrameworkElement _placementTarget;
        public static Form _form; // the top-level window holding the WebBrowser control
        MusicInjector _wb = new MusicInjector();

        public MusicInjector injector { get { return _wb; } }

        public OverlayWF(FrameworkElement placementTarget)
        {
            _placementTarget = placementTarget;
            Window owner = Window.GetWindow(placementTarget);
            Debug.Assert(owner != null);
            _owner = owner;

            _form = new Form();
            _form.Opacity = owner.Opacity;
            _form.ShowInTaskbar = false;
            _form.FormBorderStyle = FormBorderStyle.None;
            _form.Controls.Add(_wb);

            //owner.SizeChanged += delegate { OnSizeLocationChanged(); };
            owner.LocationChanged += delegate { OnSizeLocationChanged(); };
            _placementTarget.SizeChanged += delegate { OnSizeLocationChanged(); };

            if (owner.IsVisible)
                InitialShow();
            else
                owner.SourceInitialized += delegate
                {
                    InitialShow();
                };

            DependencyPropertyDescriptor dpd = DependencyPropertyDescriptor.FromProperty(UIElement.OpacityProperty, typeof(Window));
            dpd.AddValueChanged(owner, delegate { _form.Opacity = _owner.Opacity; });

            _form.FormClosing += delegate { _owner.Close(); };
        }

        void InitialShow()
        {
            NativeWindow owner = new NativeWindow();
            owner.AssignHandle(((HwndSource)HwndSource.FromVisual(_owner)).Handle);
            _form.Show(owner);
            owner.ReleaseHandle();
        }

        DispatcherOperation _repositionCallback;

        public void OnSizeLocationChanged()
        {
            // To reduce flicker when transparency is applied without DWM composition, 
            // do resizing at lower priority.
            if (_repositionCallback == null)
                _repositionCallback = _owner.Dispatcher.BeginInvoke(new Action(Reposition), DispatcherPriority.Input);
        }

        public void Reposition()
        {
            _repositionCallback = null;

            Point offset = _placementTarget.TranslatePoint(new Point(), _owner);
            Point size = new Point(_placementTarget.ActualWidth, _placementTarget.ActualHeight);
            HwndSource hwndSource = (HwndSource)HwndSource.FromVisual(_owner);
            CompositionTarget ct = hwndSource.CompositionTarget;
            offset = ct.TransformToDevice.Transform(offset);
            size = ct.TransformToDevice.Transform(size);

            Win32.POINT screenLocation = new Win32.POINT(offset);
            Win32.ClientToScreen(hwndSource.Handle, ref screenLocation);
            Win32.POINT screenSize = new Win32.POINT(size);

            Win32.MoveWindow(_form.Handle, screenLocation.X, screenLocation.Y, screenSize.X, screenSize.Y, true);
            //_form.SetBounds(screenLocation.X, screenLocation.Y, screenSize.X, screenSize.Y);
            //_form.Update();
        }
    };
}
