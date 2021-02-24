using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.IO;
using Microsoft.Win32;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Windows.Media.Animation;

namespace Lego_Island_Rebuler__With_an_ok_ui_i_think_
{
    public partial class MainWindow : Window
    {
        private TimeSpan duration { get; set; } = TimeSpan.FromSeconds(1);
        private IEasingFunction ease { get; set; } = new QuarticEase { EasingMode = EasingMode.EaseInOut };
        OverlayWF thing;
        MusicInjector music_injector;
        PatchList thingy = new PatchList();
        OpenFileDialog openFileDialog = new OpenFileDialog();
        private string jukebox_output;
        MainWindow form;
        MusicInjector injector = new MusicInjector();

        public MainWindow()
        {
            InitializeComponent();
            prop.SelectedObject = thingy;
            form = this;
            Tabs.SelectionChanged += Tabs_SelectionChanged;
            Tabs.SelectedIndex = 1;
            foreach (var i in LogicalTreeHelper.GetChildren(Music))
            {
                if (i.ToString().Contains("Grid"))
                {
                    thing = new OverlayWF((FrameworkElement)i);
                    thing.injector.Prepare();
                    music_injector = thing.injector;
                    return;
                }
            }
        }

        public void Shift(DependencyObject element, Thickness from, Thickness to)
        {
            ThicknessAnimation shiftAnimation = new ThicknessAnimation()
            {
                From = from,
                To = to,
                Duration = duration,
                EasingFunction = ease
            };

            Storyboard.SetTarget(shiftAnimation, element);
            Storyboard.SetTargetProperty(shiftAnimation, new PropertyPath(MarginProperty));

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(shiftAnimation);
            //storyboard.FillBehavior = FillBehavior.Stop;
            storyboard.Begin();
        }

        private void Tabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (thing != null)
            {
                if (Tabs.SelectedIndex == 0)
                {
                    thing.injector.Hide();
                    OverlayWF._form.Hide();
                }
                else
                {
                    thing.injector.Show();
                    thing.OnSizeLocationChanged();
                    OverlayWF._form.Show();
                }
            }
        }

        private static string[] VersionHashes = new string[]
        {
            "58FCF0F6500614E9F743712D1DD4D340088123DE",
            "BBE289E89E5A39949D272174162711EA5CFF522C",
            "96A6BAE8345AA04C21F1B319A632CAECFEE22443",
            "8DFD3E5FDDE8C95C61013069795171163C9A4821",
            "47EE50FC1EC5F6C54F465EB296D2F1B7CA25D5D2"
        };


        public static void Log(string text)
        {
            using (StreamWriter streamWriter = new StreamWriter(Path.Combine(Path.GetTempPath(), "legoislandrebuilder.log"), true))
            {
                streamWriter.WriteLine("[" + DateTime.Now.ToString() + "] " + text);
                streamWriter.Close();
            }
        }

        private List<Process> processes = new List<Process>();

        private void Write(FileStream fs, byte[] bytes, long pos = -1L)
        {
            if (pos > -1L)
            {
                fs.Position = pos;
            }
            fs.Write(bytes, 0, bytes.Length);
        }

        // Token: 0x06000015 RID: 21 RVA: 0x00003880 File Offset: 0x00001A80
        private void WriteByte(FileStream fs, byte b, long pos = -1L)
        {
            if (pos > -1L)
            {
                fs.Position = pos;
            }
            fs.WriteByte(b);
        }

        // Token: 0x06000016 RID: 22 RVA: 0x00003898 File Offset: 0x00001A98
        private void WriteManyBytes(FileStream fs, byte b, int count, long pos = -1L)
        {
            if (pos > -1L)
            {
                fs.Position = pos;
            }
            for (int i = 0; i < count; i++)
            {
                fs.WriteByte(b);
            }
        }

        // Token: 0x06000017 RID: 23 RVA: 0x000038C8 File Offset: 0x00001AC8
        private void WriteInt32(FileStream fs, int integer, long pos = -1L)
        {
            byte[] bytes = BitConverter.GetBytes(integer);
            this.Write(fs, bytes, pos);
        }

        // Token: 0x06000018 RID: 24 RVA: 0x000038E8 File Offset: 0x00001AE8
        private void WriteFloat(FileStream fs, float f, long pos = -1L)
        {
            byte[] bytes = BitConverter.GetBytes(f);
            this.Write(fs, bytes, pos);
        }

        // Token: 0x06000019 RID: 25 RVA: 0x00003908 File Offset: 0x00001B08
        private void WriteString(FileStream fs, string s, long pos = -1L)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(s);
            this.Write(fs, bytes, pos);
        }

        // Token: 0x0600001A RID: 26 RVA: 0x0000392A File Offset: 0x00001B2A
        private bool ApproxEqual(float a, float b)
        {
            return (double)Math.Abs(a - b) < 0.0001;
        }

        // Token: 0x0600001B RID: 27 RVA: 0x00003940 File Offset: 0x00001B40
        private bool IncompatibleBuildMessage(string incompatibilities)
        {
            return MessageBox.Show("The following patches you've chosen are not compatible with this version of LEGO Island:\n\n" + incompatibilities + "\nContinue without them?", "Compatibility", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes;
        }

        // Token: 0x0600001C RID: 28 RVA: 0x00003962 File Offset: 0x00001B62
        private static string GetDisplayNameOfProperty(string property)
        {
            return ((DisplayNameAttribute)typeof(PatchList).GetProperty(property).GetCustomAttributes(typeof(DisplayNameAttribute), true)[0]).DisplayName;
        }

        // Token: 0x0600001D RID: 29 RVA: 0x00003990 File Offset: 0x00001B90
        public static RegistryKey GetGameRegistryKey()
        {
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Mindscape\\LEGO Island", false);
            if (registryKey == null)
            {
                registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\WOW6432Node\\Mindscape\\LEGO Island", false);
            }
            return registryKey;
        }

        // Token: 0x0600001E RID: 30 RVA: 0x000039C4 File Offset: 0x00001BC4
        public static string GetRegistryEntry(string key)
        {
            using (RegistryKey gameRegistryKey = GetGameRegistryKey())
            {
                if (gameRegistryKey != null)
                {
                    object value = gameRegistryKey.GetValue(key);
                    if (value != null)
                    {
                        return value.ToString();
                    }
                }
            }
            return null;
        }

        // Token: 0x0600001F RID: 31 RVA: 0x00003A10 File Offset: 0x00001C10
        private static Version DetermineVersion(string lego1dll_url)
        {
            Version result;
            using (FileStream fileStream = new FileStream(lego1dll_url, FileMode.Open, FileAccess.Read))
            {
                using (BufferedStream bufferedStream = new BufferedStream(fileStream))
                {
                    using (SHA1Managed sha1Managed = new SHA1Managed())
                    {
                        byte[] array = sha1Managed.ComputeHash(bufferedStream);
                        StringBuilder stringBuilder = new StringBuilder(2 * array.Length);
                        foreach (byte b in array)
                        {
                            stringBuilder.AppendFormat("{0:X2}", b);
                        }
                        string text = stringBuilder.ToString();
                        Version version = (Version)Array.IndexOf<string>(VersionHashes, text);
                        if (version == Version.kUnknown)
                        {
                            Log("Unknown version: " + text);
                            if (MessageBox.Show("The version of LEGO Island you have installed is unknown to Rebuilder. This may result in unpredictable behavior. Would you like to continue?\n\nYour version is: " + text, "Unknown Version", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
                            {
                                return Version.kEnglish11;
                            }
                        }
                        result = version;
                    }
                }
            }
            return result;
        }


        private bool Patch(string source_dir, string dir)
        {
            string text = "";
            string path = Path.Combine(dir, "ISLE.EXE");
            string text2 = Path.Combine(dir, "LEGO1.DLL");
            Version version = DetermineVersion(text2);
            Log("Found version: " + version.ToString());
            if (version == Version.kUnknown)
            {
                return false;
            }
            using (FileStream fileStream = File.Open(text2, FileMode.Open, FileAccess.ReadWrite))
            {
                using (FileStream fileStream2 = File.Open(path, FileMode.Open, FileAccess.ReadWrite))
                {
                    long pos;
                    long pos2;
                    long pos3;
                    long pos4;
                    long pos5;
                    long pos6;
                    long pos7;
                    long pos8;
                    long pos9;
                    long pos10;
                    long pos11;
                    switch (version)
                    {
                        case Version.kEnglish10:
                            pos = 994344L;
                            pos2 = 662887L;
                            pos3 = 662834L;
                            pos4 = 344664L;
                            pos5 = 739579L;
                            pos6 = 739569L;
                            pos7 = 710611L;
                            pos8 = 501387L;
                            pos9 = 862454L;
                            pos10 = 1044520L;
                            pos11 = 347167L;
                            goto IL_240;
                        case Version.kGerman11:
                            pos = 996392L;
                            pos2 = 664855L;
                            pos3 = 664802L;
                            pos4 = 345336L;
                            pos5 = 726091L;
                            pos6 = 726081L;
                            pos7 = 712579L;
                            pos8 = 503195L;
                            pos9 = 864422L;
                            pos10 = 1046648L;
                            pos11 = 347839L;
                            goto IL_240;
                        case Version.kDanish11:
                            pos = 996392L;
                            pos2 = 664775L;
                            pos3 = 664722L;
                            pos4 = 345336L;
                            pos5 = 726011L;
                            pos6 = 726001L;
                            pos7 = 712499L;
                            pos8 = 503131L;
                            pos9 = 864342L;
                            pos10 = 1046632L;
                            pos11 = 347839L;
                            goto IL_240;
                        case Version.kSpanish11:
                            pos = 995880L;
                            pos2 = 664583L;
                            pos3 = 664530L;
                            pos4 = 345336L;
                            pos5 = 725819L;
                            pos6 = 725809L;
                            pos7 = 712307L;
                            pos8 = 502971L;
                            pos9 = 864150L;
                            pos10 = 1046104L;
                            pos11 = 347839L;
                            goto IL_240;
                    }
                    pos = 995880L;
                    pos2 = 664279L;
                    pos3 = 664226L;
                    pos4 = 345336L;
                    pos5 = 725515L;
                    pos6 = 725505L;
                    pos7 = 712003L;
                    pos8 = 502699L;
                    pos9 = 863846L;
                    pos10 = 1046088L;
                    pos11 = 347839L;
                IL_240:
                    this.WriteInt32(fileStream, this.thingy.MouseDeadzone, pos);
                    fileStream.Position += 4L;
                    this.WriteFloat(fileStream, this.thingy.MovementMaxSpeed, -1L);
                    this.WriteFloat(fileStream, this.thingy.TurnMaxSpeed, -1L);
                    this.WriteFloat(fileStream, this.thingy.MovementMaxAcceleration, -1L);
                    this.WriteFloat(fileStream, this.thingy.TurnMaxAcceleration, -1L);
                    this.WriteFloat(fileStream, this.thingy.MovementMinAcceleration, -1L);
                    this.WriteFloat(fileStream, this.thingy.TurnMinAcceleration, -1L);
                    this.WriteFloat(fileStream, this.thingy.MovementDeceleration, -1L);
                    this.WriteFloat(fileStream, this.thingy.TurnDeceleration, -1L);
                    fileStream.Position += 4L;
                    this.WriteInt32(fileStream, Convert.ToInt32(this.thingy.TurnUseVelocity), -1L);
                    this.WriteByte(fileStream2, 1, 7007L);
                    if (this.thingy.UnhookTurnSpeed)
                    {
                        this.Write(fileStream, new byte[]
                        {
                            217,
                            70,
                            36,
                            216,
                            76,
                            36,
                            20,
                            216,
                            78,
                            52
                        }, pos4);
                        this.WriteManyBytes(fileStream, 144, 26, -1L);
                    }
                    if (this.thingy.StayActiveWhenDefocused)
                    {
                        this.Write(fileStream2, new byte[]
                        {
                            144,
                            144,
                            144
                        }, 4963L);
                        this.WriteByte(fileStream, 128, pos5);
                        this.WriteByte(fileStream, 128, 23446L);
                        this.WriteByte(fileStream, 128, pos6);
                        this.WriteByte(fileStream, 128, pos7);
                    }
                    if (this.thingy.MultipleInstances)
                    {
                        this.WriteByte(fileStream2, 235, 4277L);
                    }
                    if (this.music_injector.ReplaceCount() > 0)
                    {
                        Uri uri = new Uri(this.jukebox_output.Substring(0, this.jukebox_output.LastIndexOf(".")));
                        Uri uri2 = new Uri(Path.Combine(source_dir, "ISLE.EXE")).MakeRelativeUri(uri);
                        string s = "\\" + Uri.UnescapeDataString(uri2.ToString()).Replace("/", "\\");
                        switch (version)
                        {
                            case Version.kEnglish10:
                                this.WriteByte(fileStream, 246, 335605L);
                                this.WriteByte(fileStream, 52, -1L);
                                this.WriteByte(fileStream, 13, -1L);
                                this.WriteByte(fileStream, 16, -1L);
                                goto IL_5BF;
                            case Version.kGerman11:
                                this.WriteByte(fileStream, 166, 336277L);
                                this.WriteByte(fileStream, 60, -1L);
                                this.WriteByte(fileStream, 13, -1L);
                                this.WriteByte(fileStream, 16, -1L);
                                goto IL_5BF;
                            case Version.kDanish11:
                                this.WriteByte(fileStream, 86, 336277L);
                                this.WriteByte(fileStream, 60, -1L);
                                this.WriteByte(fileStream, 13, -1L);
                                this.WriteByte(fileStream, 16, -1L);
                                goto IL_5BF;
                            case Version.kSpanish11:
                                this.WriteByte(fileStream, 150, 336277L);
                                this.WriteByte(fileStream, 59, -1L);
                                this.WriteByte(fileStream, 13, -1L);
                                this.WriteByte(fileStream, 16, -1L);
                                goto IL_5BF;
                        }
                        this.WriteByte(fileStream, 102, 336277L);
                        this.WriteByte(fileStream, 58, -1L);
                        this.WriteByte(fileStream, 13, -1L);
                        this.WriteByte(fileStream, 16, -1L);
                    IL_5BF:
                        this.WriteString(fileStream, s, pos9);
                    }
                    this.WriteByte(fileStream, 235, pos2);
                    this.WriteByte(fileStream, 201, -1L);
                    this.WriteByte(fileStream, 104, pos3);
                    this.WriteFloat(fileStream, this.thingy.FOVMultiplier, -1L);
                    this.WriteByte(fileStream, 216, -1L);
                    this.WriteByte(fileStream, 12, -1L);
                    this.WriteByte(fileStream, 36, -1L);
                    this.WriteByte(fileStream, 94, -1L);
                    this.WriteByte(fileStream, 235, -1L);
                    this.WriteByte(fileStream, 46, -1L);
                    if (this.thingy.FPSLimit == FPSLimitType.Uncapped)
                    {
                        this.WriteInt32(fileStream2, 0, 1204L);
                    }
                    else if (this.thingy.FPSLimit == FPSLimitType.Limited)
                    {
                        int integer = (int)Math.Round((double)(1000f / this.thingy.CustomFPS));
                        this.WriteInt32(fileStream2, integer, 1204L);
                    }
                    if (this.thingy.FPSLimit != FPSLimitType.Default)
                    {
                        this.WriteManyBytes(fileStream, 144, 8, pos8);
                    }
                    switch (this.thingy.ModelQuality)
                    {
                        case ModelQualityType.Infinite:
                            this.WriteFloat(fileStream, float.MaxValue, pos10);
                            break;
                        case ModelQualityType.High:
                            this.WriteFloat(fileStream, 5f, pos10);
                            break;
                        case ModelQualityType.Medium:
                            this.WriteFloat(fileStream, 3.6f, pos10);
                            break;
                        case ModelQualityType.Low:
                            this.WriteFloat(fileStream, 0f, pos10);
                            break;
                    }
                    if (this.thingy.OverrideResolution)
                    {
                        this.WriteInt32(fileStream2, this.thingy.ResolutionWidth, 59464L);
                        this.WriteInt32(fileStream2, this.thingy.ResolutionHeight, 59468L);
                        this.WriteInt32(fileStream2, this.thingy.ResolutionWidth - 1, 1232L);
                        this.WriteInt32(fileStream2, this.thingy.ResolutionHeight - 1, 1239L);
                        if (this.thingy.UpscaleBitmaps)
                        {
                            this.Write(fileStream, new byte[]
                            {
                                233,
                                45,
                                1,
                                0,
                                0,
                                139,
                                86,
                                28,
                                106,
                                0,
                                141,
                                69,
                                228,
                                246,
                                66,
                                48,
                                8,
                                116,
                                7,
                                104,
                                0,
                                128,
                                0,
                                0,
                                235,
                                2,
                                106,
                                0,
                                139,
                                59,
                                80,
                                81,
                                141,
                                77,
                                212,
                                81,
                                83,
                                83,
                                80,
                                104
                            }, 729321L);
                            this.WriteFloat(fileStream, (float)this.thingy.ResolutionHeight / 480f, -1L);
                            int integer2 = (int)Math.Round(((double)this.thingy.ResolutionWidth - (double)this.thingy.ResolutionHeight / 3.0 * 4.0) / 2.0);
                            this.Write(fileStream, new byte[]
                            {
                                219,
                                69,
                                212,
                                216,
                                12,
                                36,
                                219,
                                93,
                                212,
                                219,
                                69,
                                216,
                                216,
                                12,
                                36,
                                219,
                                93,
                                216,
                                219,
                                69,
                                220,
                                216,
                                12,
                                36,
                                219,
                                93,
                                220,
                                219,
                                69,
                                224,
                                216,
                                12,
                                36,
                                219,
                                93,
                                224,
                                88,
                                139,
                                69,
                                212,
                                5
                            }, -1L);
                            this.WriteInt32(fileStream, integer2, -1L);
                            this.Write(fileStream, new byte[]
                            {
                                137,
                                69,
                                212,
                                139,
                                69,
                                220,
                                5
                            }, -1L);
                            this.WriteInt32(fileStream, integer2, -1L);
                            this.Write(fileStream, new byte[]
                            {
                                137,
                                69,
                                220
                            }, -1L);
                            this.WriteManyBytes(fileStream, 144, 143, -1L);
                            this.Write(fileStream, new byte[]
                            {
                                88,
                                91
                            }, -1L);
                            this.Write(fileStream, new byte[]
                            {
                                233,
                                246,
                                253,
                                byte.MaxValue,
                                byte.MaxValue
                            }, 729843L);
                            this.WriteManyBytes(fileStream, 144, 19, -1L);
                        }
                    }
                    if (this.thingy.DebugToggle)
                    {
                        this.WriteByte(fileStream, 235, pos11);
                    }
                    if (this.thingy.DisableAutoFinishBuilding)
                    {
                        if (version == Version.kEnglish10)
                        {
                            text = text + "- " + GetDisplayNameOfProperty("DisableAutoFinishBuilding") + "\n";
                        }
                        else
                        {
                            this.WriteManyBytes(fileStream, 144, 5, 142347L);
                            this.WriteManyBytes(fileStream, 144, 7, 142442L);
                        }
                    }
                }
            }
            using (RegistryKey gameRegistryKey = GetGameRegistryKey())
            {
                if (gameRegistryKey == null)
                {
                    if (MessageBox.Show("Failed to find LEGO Island's registry entries. Some patches may fail. Do you wish to continue?", "Failed to find registry keys", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.No)
                    {
                        return false;
                    }
                }
                else
                {
                    using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Software\\Mindscape\\LEGO Island"))
                    {
                        this.CopyRegistryKey(gameRegistryKey, registryKey);
                        registryKey.SetValue("Full Screen", this.thingy.FullScreen ? "YES" : "NO");
                        registryKey.SetValue("Draw Cursor", this.thingy.DrawCursor ? "YES" : "NO");
                        registryKey.SetValue("UseJoystick", this.thingy.UseJoystick ? "YES" : "NO");
                        registryKey.SetValue("Music", this.thingy.MusicToggle ? "YES" : "NO");
                        if (this.thingy.RedirectSaveData)
                        {
                            string text3 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\LEGO Island\\";
                            Directory.CreateDirectory(text3);
                            registryKey.SetValue("savepath", text3);
                        }
                    }
                }
            }
            return string.IsNullOrEmpty(text) || this.IncompatibleBuildMessage(text);
        }

        private void CopyRegistryKey(RegistryKey src, RegistryKey dst)
        {
            foreach (string name in src.GetValueNames())
            {
                dst.SetValue(name, src.GetValue(name), src.GetValueKind(name));
            }
            foreach (string text in src.GetSubKeyNames())
            {
                using (RegistryKey registryKey = src.OpenSubKey(text, false))
                {
                    RegistryKey dst2 = dst.CreateSubKey(text);
                    this.CopyRegistryKey(registryKey, dst2);
                }
            }
        }


        private bool IsValidDir(string dir)
        {
            return File.Exists(Path.Combine(dir, "ISLE.EXE")) && File.Exists(Path.Combine(dir, "LEGO1.DLL"));
        }

        public void LaunchGame()
        {
            if (this.processes.Count > 0)
            {
                foreach (Process process in this.processes)
                {
                    process.Kill();
                }
                this.processes.Clear();
                return;
            }
            string text = Path.Combine(Path.GetTempPath(), "LEGOIslandRebuilder");
            Log("Using working directory: " + text);
            if (Directory.Exists(text))
            {
                Log("Working directory already exists, no need to create");
            }
            else
            {
                try
                {
                    Directory.CreateDirectory(text);
                    Log("Working directory created successfully");
                }
                catch (Exception ex)
                {
                    Log("Failed to create working directory: " + ex.ToString());
                    MessageBox.Show("Failed to create temporary path: " + ex.ToString(), "Failed to patch files", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return;
                }
            }
            Log("Searching for game directory...");
            string text2 = "";
            if (string.IsNullOrEmpty(text2))
            {
                text2 = GetRegistryEntry("diskpath");
            }
            if (string.IsNullOrEmpty(text2))
            {
                    openFileDialog.Filter = "ISLE.EXE|ISLE.EXE";
                    openFileDialog.Title = "Where is LEGO Island installed?";
                    while (openFileDialog.ShowDialog() == true)
                    {
                        text2 = Path.GetDirectoryName(openFileDialog.FileName);
                        if (this.IsValidDir(text2))
                        {
                            goto IL_16D;
                        }
                        MessageBox.Show("This directory does not contain ISLE.EXE and LEGO1.DLL.", "Failed to find critical files", MessageBoxButton.OK, MessageBoxImage.Hand);
                    }
                    Log("Failed to find game directory, user cancelled prompt.");
                    return;
                }
            
        IL_16D:
            Log("Found game directory: " + text2);
            try
            {
                string[] files = Directory.GetFiles(text);
                for (int i = 0; i < files.Length; i++)
                {
                    Log("Deleting existing file: " + files[i]);
                    File.SetAttributes(files[i], FileAttributes.Normal);
                    File.Delete(files[i]);
                }
            }
            catch (Exception ex2)
            {
                Log("Failed to delete old files: " + ex2.ToString());
                MessageBox.Show("Failed to delete old files: " + ex2.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                return;
            }
            try
            {
                string[] files2 = Directory.GetFiles(text2);
                for (int j = 0; j < files2.Length; j++)
                {
                    Log("Copying game file: " + Path.GetFileName(files2[j]));
                    File.Copy(files2[j], Path.Combine(text, Path.GetFileName(files2[j])), true);
                }
            }
            catch (Exception ex3)
            {
                Log("Failed to copy files for patching: " + ex3.ToString());
                MessageBox.Show("Failed to copy files for patching: " + ex3.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                return;
            }
            if (this.music_injector.ReplaceCount() > 0)
            {
                this.jukebox_output = Path.Combine(text2, "LEGO/Scripts/REJUKEBOX.SI");
                Log("Attempting to create injected jukebox file: " + this.jukebox_output);
                try
                {
                    using (FileStream fileStream = new FileStream(this.jukebox_output, FileMode.Create, FileAccess.Write))
                    {
                        fileStream.Close();
                    }
                }
                catch
                {
                    this.jukebox_output = Path.Combine(Path.GetTempPath(), "REJUKEBOX.SI");
                    Log("Unable to use previous jukebox path, using alternative: " + this.jukebox_output);
                }
                this.music_injector.Insert(this.jukebox_output);
            }
            Log("Patching files");
            if (!this.Patch(text2, text))
            {
                return;
            }
            Log("Creating compatibility registry keys");
            using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\AppCompatFlags\\Layers", true))
            {
                if (registryKey != null)
                {
                    string text3 = "DWM8And16BitMitigation";
                    if (!this.thingy.FullScreen)
                    {
                        if (Environment.OSVersion.Version.Major < 8)
                        {
                            text3 += " 16BITCOLOR";
                        }
                        else
                        {
                            text3 += " 256COLOR";
                        }
                    }
                    if (!this.thingy.RedirectSaveData)
                    {
                        text3 += " RUNASADMIN";
                    }
                    registryKey.SetValue(text + "\\ISLE.EXE", text3);
                }
            }
            Log("Launching game...");
            ProcessStartInfo processStartInfo = new ProcessStartInfo(Path.Combine(text, "ISLE.EXE"));
            processStartInfo.WorkingDirectory = text2;
            try
            {
                Process process2 = Process.Start(processStartInfo);
                process2.EnableRaisingEvents = true;
                process2.Exited += this.ProcessExit;
                this.processes.Add(process2);
                this.form.run_button.Content = "Kill Game";
                if (this.thingy.MultipleInstances)
                {
                    Shift(this.form.run_button,this.form.run_button.Margin,new Thickness(10, 367, 210, 22));
                    this.form.run_additional_button.Visibility = Visibility.Visible;
                }
                Log("Game launched successfully");
            }
            catch (Exception ex4)
            {
                Log("Failed to launch LEGO Island: " + ex4.ToString());
                MessageBox.Show("Failed to launch LEGO Island: " + ex4.ToString(), "Failed to launch", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
        }

        private async void ProcessExit(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(delegate ()
            {
                this.form.run_button.Content = "Run Game";
                Shift(this.form.run_button,this.form.run_button.Margin,new Thickness(10, 367, 10, 22));
                this.form.run_additional_button.Visibility = Visibility.Hidden;
            }));
            for (int i = 0; i < this.processes.Count; i++)
            {
                if (this.processes[i] == sender)
                {
                    this.processes.RemoveAt(i);
                    return;
                }
            }
        }

        private void Exitbutton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(e.Uri.AbsoluteUri);
            e.Handled = true;
        }

        private void Run_button_Click(object sender, RoutedEventArgs e)
        {
           LaunchGame();
        }

        private void Draggingthingy_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Run_additional_button_Click(object sender, RoutedEventArgs e)
        {
            Process process = Process.Start(this.processes[0].StartInfo);
            process.EnableRaisingEvents = true;
            process.Exited += this.ProcessExit;
            this.processes.Add(process);
        }
    }
}
