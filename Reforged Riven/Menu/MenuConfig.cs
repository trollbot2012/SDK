using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;


namespace Reforged_Riven.Main
{
    internal class MenuConfig
    {
        private const string MenuName = "Reforged Riven";
        public static Menu MainMenu { get; set; } = new Menu(MenuName, MenuName, true);
        public static void Load()
        {
            // Combo
            ComboMenu = MainMenu.Add(new Menu("ComboMenu", "Combo"));
            ForceR = ComboMenu.Add(new MenuBool("ForceR", "Force R", true));
            RKillable = ComboMenu.Add(new MenuBool("RKillable", "R2 For Max Damage"));

            BurstMenu = MainMenu.Add(new Menu("Burst", "Burst"));
            BurstKeyBind = BurstMenu.Add(new MenuKeyBind("BurstKeyBind", "Use Burst", System.Windows.Forms.Keys.T, KeyBindType.Toggle));
            Flash = BurstMenu.Add(new MenuBool("Flash", "Check Killable", true));
            //BurstKey = BurstMenu.Add(new MenuKeyBind("BurstKey", "Burst Keybind", System.Windows.Forms.Keys.T, KeyBindType.Press));
            

            // Lane
            LaneMenu = MainMenu.Add(new Menu("LaneMenu", "Lane"));
            LaneVisible = LaneMenu.Add(new MenuBool("LaneVisible", "Only If No Enemy Visible", true));
            LaneQ = LaneMenu.Add(new MenuBool("LaneQ", "Use Q", true));
            LaneW = LaneMenu.Add(new MenuBool("LaneW", "Use W"));
            LaneE = LaneMenu.Add(new MenuBool("LaneE", "Use E"));

            // Jungle
            JungleMenu = MainMenu.Add(new Menu("JungleMenu", "Jungle"));
            JungleQ = JungleMenu.Add(new MenuBool("JungleQ", "Use Q", true));
            JungleW = JungleMenu.Add(new MenuBool("JungleW", "Use W", true));
            JungleE = JungleMenu.Add(new MenuBool("JungleE", "Use E", true));

            // Misc
            MiscMenu = MainMenu.Add(new Menu("MiscMenu", "Misc"));
            KeepQ = MiscMenu.Add(new MenuBool("KeepQ", "Keep Q Alive"));
            Ignite = MiscMenu.Add(new MenuBool("ignite", "Killsteal Ignite"));
            QMove = MiscMenu.Add(new MenuKeyBind("QMove", "Q to cursor", System.Windows.Forms.Keys.K, KeyBindType.Press));

            // Draw
            DrawMenu = MainMenu.Add(new Menu("Draw", "Draw"));
            QMinionDraw = DrawMenu.Add(new MenuBool("QMinionDraw", "Minion Killable Q"));
            Dind = DrawMenu.Add(new MenuBool("Dind", "Damage Indicator"));
            DrawFlee = DrawMenu.Add(new MenuBool("DrawFlee", "Draw Flee Spots"));
           // HealthDmg = DrawMenu.Add(new MenuBool("HealthDmg", "Write Dmg On Target"));
            DrawCombo = DrawMenu.Add(new MenuBool("DrawCombo", "Draw Combo Range", true));

            // Flee
            FleeMenu = MainMenu.Add(new Menu("Flee", "Flee"));
            WallFlee = FleeMenu.Add(new MenuBool("WallFlee", "WallFlee"));
            FleeKey = FleeMenu.Add(new MenuKeyBind("FleeKey", "Flee Key", System.Windows.Forms.Keys.A, KeyBindType.Press));

           
            MainMenu.Attach();
        }
        // Menu
        public static Menu ComboMenu;
        public static Menu BurstMenu;
        public static Menu LaneMenu;
        public static Menu JungleMenu;
        public static Menu MiscMenu;
        public static Menu DrawMenu;
        public static Menu FleeMenu;

       
        // Keybind
        public static MenuKeyBind FleeKey;
        public static MenuKeyBind QMove;
        public static MenuKeyBind BurstKeyBind;

        // Menu Bool
        // public static MenuBool EngageQ;
        public static MenuBool Flash;
        public static MenuBool ForceR;
        public static MenuBool RKillable;
        public static MenuBool Ignite;
        public static MenuBool JungleQ;
        public static MenuBool JungleW;
        public static MenuBool JungleE;
        public static MenuBool LaneVisible;
        public static MenuBool LaneQ;
        public static MenuBool LaneW;
        public static MenuBool LaneE;
        public static MenuBool KeepQ;
        public static MenuBool Dind;
        public static MenuBool DrawFlee;
        public static MenuBool QMinionDraw;
        public static MenuBool DrawCombo;
        public static MenuBool WallFlee;
    }
}
