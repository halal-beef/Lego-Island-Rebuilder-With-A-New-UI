using System.ComponentModel;

namespace Lego_Island_Rebuler__With_an_ok_ui_i_think_
{
    public enum Version
    {
        // Token: 0x04000016 RID: 22
        kUnknown = -1,
        // Token: 0x04000017 RID: 23
        kEnglish10,
        // Token: 0x04000018 RID: 24
        kEnglish11,
        // Token: 0x04000019 RID: 25
        kGerman11,
        // Token: 0x0400001A RID: 26
        kDanish11,
        // Token: 0x0400001B RID: 27
        kSpanish11
    }

    // Token: 0x02000006 RID: 6
    public enum FPSLimitType
    {
        // Token: 0x0400001D RID: 29
        Default,
        // Token: 0x0400001E RID: 30
        Uncapped,
        // Token: 0x0400001F RID: 31
        Limited
    }

    // Token: 0x02000007 RID: 7
    public enum ModelQualityType
    {
        // Token: 0x04000021 RID: 33
        Infinite,
        // Token: 0x04000022 RID: 34
        High,
        // Token: 0x04000023 RID: 35
        Medium,
        // Token: 0x04000024 RID: 36
        Low
    }

    public class PatchList
    {
        // Token: 0x17000001 RID: 1
        // (get) Token: 0x06000031 RID: 49 RVA: 0x00004FEE File Offset: 0x000031EE
        // (set) Token: 0x06000032 RID: 50 RVA: 0x00004FF6 File Offset: 0x000031F6
        [Category("Controls")]
        [DisplayName("Turning: Max Speed")]
        [Description("Set the maximum turning speed. (Default = 20.0)")]
        [DefaultValue(20f)]
        public float TurnMaxSpeed
        {
            get
            {
                return this.turn_max_speed;
            }
            set
            {
                this.turn_max_speed = value;
            }
        }

        // Token: 0x17000002 RID: 2
        // (get) Token: 0x06000033 RID: 51 RVA: 0x00004FFF File Offset: 0x000031FF
        // (set) Token: 0x06000034 RID: 52 RVA: 0x00005007 File Offset: 0x00003207
        [Category("Controls")]
        [DisplayName("Turning: Max Acceleration")]
        [Description("Set the speed at which turning accelerates (requires 'Turning: Enable Velocity') (Default = 30.0)")]
        [DefaultValue(30f)]
        public float TurnMaxAcceleration
        {
            get
            {
                return this.turn_max_acceleration;
            }
            set
            {
                this.turn_max_acceleration = value;
            }
        }

        // Token: 0x17000003 RID: 3
        // (get) Token: 0x06000035 RID: 53 RVA: 0x00005010 File Offset: 0x00003210
        // (set) Token: 0x06000036 RID: 54 RVA: 0x00005018 File Offset: 0x00003218
        [Category("Controls")]
        [DisplayName("Turning: Min Acceleration")]
        [Description("Set the speed at which turning accelerates (requires 'Turning: Enable Velocity') (Default = 15.0)")]
        [DefaultValue(15f)]
        public float TurnMinAcceleration
        {
            get
            {
                return this.turn_min_acceleration;
            }
            set
            {
                this.turn_min_acceleration = value;
            }
        }

        // Token: 0x17000004 RID: 4
        // (get) Token: 0x06000037 RID: 55 RVA: 0x00005021 File Offset: 0x00003221
        // (set) Token: 0x06000038 RID: 56 RVA: 0x00005029 File Offset: 0x00003229
        [Category("Controls")]
        [DisplayName("Turning: Deceleration")]
        [Description("Set the speed at which turning decelerates (requires 'Turning: Enable Velocity') (Default = 50.0)")]
        [DefaultValue(50f)]
        public float TurnDeceleration
        {
            get
            {
                return this.turn_deceleration;
            }
            set
            {
                this.turn_deceleration = value;
            }
        }

        // Token: 0x17000005 RID: 5
        // (get) Token: 0x06000039 RID: 57 RVA: 0x00005032 File Offset: 0x00003232
        // (set) Token: 0x0600003A RID: 58 RVA: 0x0000503A File Offset: 0x0000323A
        [Category("Controls")]
        [DisplayName("Turning: Enable Velocity")]
        [Description("By default, LEGO Island ignores the turning acceleration/deceleration values. Set this to TRUE to utilize them (Default = FALSE)")]
        [DefaultValue(false)]
        public bool TurnUseVelocity
        {
            get
            {
                return this.turn_use_velocity;
            }
            set
            {
                this.turn_use_velocity = value;
            }
        }

        // Token: 0x17000006 RID: 6
        // (get) Token: 0x0600003B RID: 59 RVA: 0x00005043 File Offset: 0x00003243
        // (set) Token: 0x0600003C RID: 60 RVA: 0x0000504B File Offset: 0x0000324B
        [Category("Controls")]
        [DisplayName("Movement: Max Speed")]
        [Description("Set the movement maximum speed. (Default = 40.0)")]
        [DefaultValue(40f)]
        public float MovementMaxSpeed
        {
            get
            {
                return this.movement_max_speed;
            }
            set
            {
                this.movement_max_speed = value;
            }
        }

        // Token: 0x17000007 RID: 7
        // (get) Token: 0x0600003D RID: 61 RVA: 0x00005054 File Offset: 0x00003254
        // (set) Token: 0x0600003E RID: 62 RVA: 0x0000505C File Offset: 0x0000325C
        [Category("Controls")]
        [DisplayName("Movement: Max Acceleration")]
        [Description("Set the movement acceleration speed (i.e. how long it takes to go from not moving to top speed) (Default = 15.0)")]
        [DefaultValue(15f)]
        public float MovementMaxAcceleration
        {
            get
            {
                return this.movement_max_acceleration;
            }
            set
            {
                this.movement_max_acceleration = value;
            }
        }

        // Token: 0x17000008 RID: 8
        // (get) Token: 0x0600003F RID: 63 RVA: 0x00005065 File Offset: 0x00003265
        // (set) Token: 0x06000040 RID: 64 RVA: 0x0000506D File Offset: 0x0000326D
        [Category("Controls")]
        [DisplayName("Movement: Min Acceleration")]
        [Description("Set the movement acceleration speed (i.e. how long it takes to go from not moving to top speed) (Default = 4.0)")]
        [DefaultValue(4f)]
        public float MovementMinAcceleration
        {
            get
            {
                return this.movement_min_acceleration;
            }
            set
            {
                this.movement_min_acceleration = value;
            }
        }

        // Token: 0x17000009 RID: 9
        // (get) Token: 0x06000041 RID: 65 RVA: 0x00005076 File Offset: 0x00003276
        // (set) Token: 0x06000042 RID: 66 RVA: 0x0000507E File Offset: 0x0000327E
        [Category("Controls")]
        [DisplayName("Movement: Deceleration")]
        [Description("Set the movement deceleration speed (i.e. how long it takes to slow to a stop after releasing the controls). Increase this value to stop faster, decrease it to stop slower. Usually this is set to a very high value making deceleration almost instant. (Default = 50.0)")]
        [DefaultValue(50f)]
        public float MovementDeceleration
        {
            get
            {
                return this.movement_deceleration;
            }
            set
            {
                this.movement_deceleration = value;
            }
        }

        // Token: 0x1700000A RID: 10
        // (get) Token: 0x06000043 RID: 67 RVA: 0x00005087 File Offset: 0x00003287
        // (set) Token: 0x06000044 RID: 68 RVA: 0x0000508F File Offset: 0x0000328F
        [Category("Controls")]
        [DisplayName("Mouse Deadzone")]
        [Description("Sets the radius from the center of the screen where the mouse will do nothing (40 = default).")]
        [DefaultValue(40)]
        public int MouseDeadzone
        {
            get
            {
                return this.mouse_deadzone;
            }
            set
            {
                this.mouse_deadzone = value;
            }
        }

        // Token: 0x1700000B RID: 11
        // (get) Token: 0x06000045 RID: 69 RVA: 0x00005098 File Offset: 0x00003298
        // (set) Token: 0x06000046 RID: 70 RVA: 0x000050A0 File Offset: 0x000032A0
        [Category("Controls")]
        [DisplayName("Turning: Unhook From Frame Rate")]
        [Description("LEGO Island contains a bug where the turning speed is influenced by the frame rate. Enable this to make the turn speed independent of the frame rate.")]
        [DefaultValue(false)]
        public bool UnhookTurnSpeed
        {
            get
            {
                return this.unhook_turn_speed;
            }
            set
            {
                this.unhook_turn_speed = value;
            }
        }

        // Token: 0x1700000C RID: 12
        // (get) Token: 0x06000047 RID: 71 RVA: 0x000050A9 File Offset: 0x000032A9
        // (set) Token: 0x06000048 RID: 72 RVA: 0x000050B1 File Offset: 0x000032B1
        [Category("Controls")]
        [DisplayName("Use Joystick")]
        [Description("Enables Joystick functionality.")]
        [DefaultValue(false)]
        public bool UseJoystick
        {
            get
            {
                return this.use_joystick;
            }
            set
            {
                this.use_joystick = value;
            }
        }

        // Token: 0x1700000D RID: 13
        // (get) Token: 0x06000049 RID: 73 RVA: 0x000050BA File Offset: 0x000032BA
        // (set) Token: 0x0600004A RID: 74 RVA: 0x000050C2 File Offset: 0x000032C2
        [Category("Graphics")]
        [DisplayName("Run in Full Screen")]
        [Description("Allows you to change modes without administrator privileges and registry editing.")]
        [DefaultValue(true)]
        public bool FullScreen
        {
            get
            {
                return this.full_screen;
            }
            set
            {
                this.full_screen = value;
            }
        }

        // Token: 0x1700000E RID: 14
        // (get) Token: 0x0600004B RID: 75 RVA: 0x000050CB File Offset: 0x000032CB
        // (set) Token: 0x0600004C RID: 76 RVA: 0x000050D3 File Offset: 0x000032D3
        [Category("Graphics")]
        [DisplayName("Draw Cursor")]
        [Description("Renders an in-game cursor, rather than a standard Windows pointer.")]
        [DefaultValue(false)]
        public bool DrawCursor
        {
            get
            {
                return this.draw_cursor;
            }
            set
            {
                this.draw_cursor = value;
            }
        }

        // Token: 0x1700000F RID: 15
        // (get) Token: 0x0600004D RID: 77 RVA: 0x000050DC File Offset: 0x000032DC
        // (set) Token: 0x0600004E RID: 78 RVA: 0x000050E4 File Offset: 0x000032E4
        [Category("System")]
        [DisplayName("Allow Multiple Instances")]
        [Description("By default, LEGO Island will allow only one instance of itself to run. This patch allows infinite instances of LEGO Island to run.")]
        [DefaultValue(false)]
        public bool MultipleInstances
        {
            get
            {
                return this.multiple_instances;
            }
            set
            {
                this.multiple_instances = value;
            }
        }

        // Token: 0x17000010 RID: 16
        // (get) Token: 0x0600004F RID: 79 RVA: 0x000050ED File Offset: 0x000032ED
        // (set) Token: 0x06000050 RID: 80 RVA: 0x000050F5 File Offset: 0x000032F5
        [Category("System")]
        [DisplayName("Stay Active When Defocused")]
        [Description("By default, LEGO Island pauses when it's not the active window. This patch prevents that behavior.")]
        [DefaultValue(false)]
        public bool StayActiveWhenDefocused
        {
            get
            {
                return this.stay_active_when_defocused;
            }
            set
            {
                this.stay_active_when_defocused = value;
            }
        }

        // Token: 0x17000011 RID: 17
        // (get) Token: 0x06000051 RID: 81 RVA: 0x000050FE File Offset: 0x000032FE
        // (set) Token: 0x06000052 RID: 82 RVA: 0x00005106 File Offset: 0x00003306
        [Category("System")]
        [DisplayName("Redirect Save Files to %APPDATA%")]
        [Description("By default LEGO Island saves its game data in its Program Files folder. In newer versions of Windows, this folder is considered privileged access, necessitating running LEGO Island as administrator to save here. This patch sets LEGO Island's save location to %APPDATA% instead, which is an accessible and standard location that most modern games and apps save to.")]
        [DefaultValue(true)]
        public bool RedirectSaveData
        {
            get
            {
                return this.redirect_save_data;
            }
            set
            {
                this.redirect_save_data = value;
            }
        }

        // Token: 0x17000012 RID: 18
        // (get) Token: 0x06000053 RID: 83 RVA: 0x0000510F File Offset: 0x0000330F
        // (set) Token: 0x06000054 RID: 84 RVA: 0x00005117 File Offset: 0x00003317
        [Category("Graphics")]
        [DisplayName("FPS Cap")]
        [Description("Modify LEGO Island's frame rate cap")]
        [DefaultValue(FPSLimitType.Default)]
        public FPSLimitType FPSLimit
        {
            get
            {
                return this.fps_limit_type;
            }
            set
            {
                this.fps_limit_type = value;
            }
        }

        // Token: 0x17000013 RID: 19
        // (get) Token: 0x06000055 RID: 85 RVA: 0x00005120 File Offset: 0x00003320
        // (set) Token: 0x06000056 RID: 86 RVA: 0x00005128 File Offset: 0x00003328
        [Category("Graphics")]
        [DisplayName("Model Quality")]
        [Description("Change LEGO Island's default model quality")]
        [DefaultValue(ModelQualityType.Medium)]
        public ModelQualityType ModelQuality
        {
            get
            {
                return this.model_quality;
            }
            set
            {
                this.model_quality = value;
            }
        }

        // Token: 0x17000014 RID: 20
        // (get) Token: 0x06000057 RID: 87 RVA: 0x00005131 File Offset: 0x00003331
        // (set) Token: 0x06000058 RID: 88 RVA: 0x00005139 File Offset: 0x00003339
        [Category("Graphics")]
        [DisplayName("FPS Cap - Custom Limit")]
        [Description("Is 'FPS Cap' is set to 'Limited', this will be the frame rate used.")]
        [DefaultValue(24f)]
        public float CustomFPS
        {
            get
            {
                return this.custom_fps_limit;
            }
            set
            {
                this.custom_fps_limit = value;
            }
        }

        // Token: 0x17000015 RID: 21
        // (get) Token: 0x06000059 RID: 89 RVA: 0x00005142 File Offset: 0x00003342
        // (set) Token: 0x0600005A RID: 90 RVA: 0x0000514A File Offset: 0x0000334A
        [Category("Experimental (Use at your own risk)")]
        [DisplayName("Override Resolution")]
        [Description("Override LEGO Island's hardcoded 640x480 resolution with a custom resolution. NOTE: This patch is currently incomplete and buggy.")]
        [DefaultValue(false)]
        public bool OverrideResolution
        {
            get
            {
                return this.override_resolution;
            }
            set
            {
                this.override_resolution = value;
            }
        }

        // Token: 0x17000016 RID: 22
        // (get) Token: 0x0600005B RID: 91 RVA: 0x00005153 File Offset: 0x00003353
        // (set) Token: 0x0600005C RID: 92 RVA: 0x0000515B File Offset: 0x0000335B
        [Category("Experimental (Use at your own risk)")]
        [DisplayName("Override Resolution - Width:")]
        [Description("If 'Override Resolution' is enabled, this is the screen resolution width to use instead.")]
        [DefaultValue(640)]
        public int ResolutionWidth
        {
            get
            {
                return this.resolution_width;
            }
            set
            {
                this.resolution_width = value;
            }
        }

        // Token: 0x17000017 RID: 23
        // (get) Token: 0x0600005D RID: 93 RVA: 0x00005164 File Offset: 0x00003364
        // (set) Token: 0x0600005E RID: 94 RVA: 0x0000516C File Offset: 0x0000336C
        [Category("Experimental (Use at your own risk)")]
        [DisplayName("Override Resolution - Height:")]
        [Description("If 'Override Resolution' is enabled, this is the screen resolution height to use instead.")]
        [DefaultValue(480)]
        public int ResolutionHeight
        {
            get
            {
                return this.resolution_height;
            }
            set
            {
                this.resolution_height = value;
            }
        }

        // Token: 0x17000018 RID: 24
        // (get) Token: 0x0600005F RID: 95 RVA: 0x00005175 File Offset: 0x00003375
        // (set) Token: 0x06000060 RID: 96 RVA: 0x0000517D File Offset: 0x0000337D
        [Category("Experimental (Use at your own risk)")]
        [DisplayName("Override Resolution - Bitmap Upscale")]
        [Description("WARNING: This doesn't upscale the bitmaps' hitboxes yet and can make 2D areas like the Information Center difficult to navigate.")]
        [DefaultValue(false)]
        public bool UpscaleBitmaps
        {
            get
            {
                return this.upscale_bitmaps;
            }
            set
            {
                this.upscale_bitmaps = value;
            }
        }

        // Token: 0x17000019 RID: 25
        // (get) Token: 0x06000061 RID: 97 RVA: 0x00005186 File Offset: 0x00003386
        // (set) Token: 0x06000062 RID: 98 RVA: 0x0000518E File Offset: 0x0000338E
        [Category("Gameplay")]
        [DisplayName("Disable Auto-Finish Building Section")]
        [Description("In LEGO Island v1.1, placing the last block when building will automatically end the building section. While convenient, this prevents players from making any further changes after placing the last brick. It also notably defies what Bill Ding says - you don't hit the triangle when you're finished building.\n\nThis patch restores the functionality in v1.0 where placing the last block will not automatically finish the build section.")]
        [DefaultValue(false)]
        public bool DisableAutoFinishBuilding
        {
            get
            {
                return this.disable_autofinish_building;
            }
            set
            {
                this.disable_autofinish_building = value;
            }
        }

        // Token: 0x1700001A RID: 26
        // (get) Token: 0x06000063 RID: 99 RVA: 0x00005197 File Offset: 0x00003397
        // (set) Token: 0x06000064 RID: 100 RVA: 0x0000519F File Offset: 0x0000339F
        [Category("Gameplay")]
        [DisplayName("Debug Mode")]
        [Description("Enables the in-game debug mode automatically without the need to type OGEL.")]
        [DefaultValue(false)]
        public bool DebugToggle
        {
            get
            {
                return this.debug_toggle;
            }
            set
            {
                this.debug_toggle = value;
            }
        }

        // Token: 0x1700001B RID: 27
        // (get) Token: 0x06000065 RID: 101 RVA: 0x000051A8 File Offset: 0x000033A8
        // (set) Token: 0x06000066 RID: 102 RVA: 0x000051B0 File Offset: 0x000033B0
        [Category("Gameplay")]
        [DisplayName("Play Music")]
        [Description("Turns in-game music on or off.")]
        [DefaultValue(true)]
        public bool MusicToggle
        {
            get
            {
                return this.music_toggle;
            }
            set
            {
                this.music_toggle = value;
            }
        }

        // Token: 0x1700001C RID: 28
        // (get) Token: 0x06000067 RID: 103 RVA: 0x000051B9 File Offset: 0x000033B9
        // (set) Token: 0x06000068 RID: 104 RVA: 0x000051C1 File Offset: 0x000033C1
        [Category("Graphics")]
        [DisplayName("Field of View Adjustment")]
        [Description("Globally adjusts the field of view by a multiplier\n\n0.1 = Default (smaller than 0.1 is more zoomed in, larger than 0.1 is more zoomed out")]
        [DefaultValue(0.1f)]
        public float FOVMultiplier
        {
            get
            {
                return this.fov_multiplier;
            }
            set
            {
                this.fov_multiplier = value;
            }
        }

        // Token: 0x04000025 RID: 37
        private float turn_max_speed = 20f;

        // Token: 0x04000026 RID: 38
        private float turn_max_acceleration = 30f;

        // Token: 0x04000027 RID: 39
        private float turn_min_acceleration = 15f;

        // Token: 0x04000028 RID: 40
        private float turn_deceleration = 50f;

        // Token: 0x04000029 RID: 41
        private bool turn_use_velocity;

        // Token: 0x0400002A RID: 42
        private float movement_max_speed = 40f;

        // Token: 0x0400002B RID: 43
        private float movement_max_acceleration = 15f;

        // Token: 0x0400002C RID: 44
        private float movement_min_acceleration = 4f;

        // Token: 0x0400002D RID: 45
        private float movement_deceleration = 50f;

        // Token: 0x0400002E RID: 46
        private int mouse_deadzone = 40;

        // Token: 0x0400002F RID: 47
        private bool unhook_turn_speed;

        // Token: 0x04000030 RID: 48
        private bool use_joystick;

        // Token: 0x04000031 RID: 49
        private bool full_screen = true;

        // Token: 0x04000032 RID: 50
        private bool draw_cursor;

        // Token: 0x04000033 RID: 51
        private bool multiple_instances;

        // Token: 0x04000034 RID: 52
        private bool stay_active_when_defocused;

        // Token: 0x04000035 RID: 53
        private bool redirect_save_data = true;

        // Token: 0x04000036 RID: 54
        private FPSLimitType fps_limit_type;

        // Token: 0x04000037 RID: 55
        private ModelQualityType model_quality = ModelQualityType.Medium;

        // Token: 0x04000038 RID: 56
        private float custom_fps_limit = 24f;

        // Token: 0x04000039 RID: 57
        private bool override_resolution;

        // Token: 0x0400003A RID: 58
        private int resolution_width = 640;

        // Token: 0x0400003B RID: 59
        private int resolution_height = 480;

        // Token: 0x0400003C RID: 60
        private bool upscale_bitmaps;

        // Token: 0x0400003D RID: 61
        private bool disable_autofinish_building;

        // Token: 0x0400003E RID: 62
        private bool debug_toggle;

        // Token: 0x0400003F RID: 63
        private bool music_toggle = true;

        // Token: 0x04000040 RID: 64
        private float fov_multiplier = 0.1f;
    }
}
