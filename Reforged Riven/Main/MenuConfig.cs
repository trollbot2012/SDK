using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;

namespace Reforged_Riven.Main
{
    class MenuConfig
    {
        private const string MenuName = "Reforged Riven";
        public static Menu MainMenu { get; set; } = new Menu(MenuName, MenuName, true);
        public static void Load()
        {
            // Combo
            ComboMenu = MainMenu.Add(new Menu("ComboMenu", "Combo"));
            ForceR = ComboMenu.Add(new MenuKeyBind("ForceR", "Force R", System.Windows.Forms.Keys.G, KeyBindType.Toggle));
            RKillable = ComboMenu.Add(new MenuBool("RKillable", "R2 For Max Damage"));

            FlashMenu = MainMenu.Add(new Menu("FlashMenu", "Flash"));
            Flash = FlashMenu.Add(new MenuBool("Flash", "Flash If Selected && Killable"));
            FlashEnemies = FlashMenu.Add(new MenuSlider("FlashEnemies", "Flash If X Enemies", 4, 0, 5));

            // Lane
            LaneMenu = MainMenu.Add(new Menu("LaneMenu", "Lane"));
            LaneVisible = LaneMenu.Add(new MenuBool("LaneVisible", "Only If No Enemy Visible", true));
            LaneQ = LaneMenu.Add(new MenuBool("LaneQ", "Use Q"));
            LaneW = LaneMenu.Add(new MenuBool("LaneW", "Use W"));
            LaneE = LaneMenu.Add(new MenuBool("LaneE", "Use E"));

            // Jungle
            JungleMenu = MainMenu.Add(new Menu("JungleMenu", "Jungle"));
            JungleQ = JungleMenu.Add(new MenuBool("JungleQ", "Use Q"));
            JungleW = JungleMenu.Add(new MenuBool("JungleW", "Use W"));
            JungleE = JungleMenu.Add(new MenuBool("JungleE", "Use E"));

            // Misc
            MiscMenu = MainMenu.Add(new Menu("MiscMenu", "Misc"));
            KeepQ = MiscMenu.Add(new MenuBool("KeepQ", "Keep Q Alive"));
            Ignite = MiscMenu.Add(new MenuBool("ignite", "Killsteal Ignite"));
            QMove = MiscMenu.Add(new MenuKeyBind("QMove", "Q to cursor", System.Windows.Forms.Keys.K, KeyBindType.Press));

            // Draw
            DrawMenu = MainMenu.Add(new Menu("Draw", "Draw"));
            DrawFleeSpots = DrawMenu.Add(new MenuBool("DrawFleeSpots", "Draw Flee Spots"));
            dind = DrawMenu.Add(new MenuBool("dind", "Damage Indicator"));

            // Flee
            FleeMenu = MainMenu.Add(new Menu("Flee", "Flee"));
            WallFlee = FleeMenu.Add(new MenuBool("WallFlee", "WallFlee"));

            // Trinket
            TrinketMenu = MainMenu.Add(new Menu("TrinketMenu", "Trinket"));
            BuyTrinket = TrinketMenu.Add(new MenuBool("BuyTrinket", "Buy Trinket"));
            TrinketList = TrinketMenu.Add(new MenuList<string>("TrinketList", "Trinkets", new[] { "Oracle Alternation", "Farsight Alternation" }));

            // Skin
            SkinMenu = MainMenu.Add(new Menu("SkinMenu", "Skin"));
            UseSkin = SkinMenu.Add(new MenuBool("UseSkin", "Use SkinChanger"));
            SkinChanger = SkinMenu.Add(new MenuList<string>("Skins", "Skins", new[] { "Default", "Redeemed", "Crimson Elite", "Battle Bunny", "Championship", "Dragonblade", "Arcade" }));

            MainMenu.Attach();
        }
        // Menu
        public static Menu ComboMenu;
        public static Menu FlashMenu;
        public static Menu LaneMenu;
        public static Menu JungleMenu;
        public static Menu MiscMenu;
        public static Menu DrawMenu;
        public static Menu FleeMenu;
        public static Menu TrinketMenu;
        public static Menu SkinMenu;

        // List
        public static MenuList<string> SkinChanger;
        public static MenuList<string> TrinketList;

        // Slider
       public static MenuSlider FlashEnemies;


       // Keybind
      //public static MenuKeyBind Passive;

        public static MenuKeyBind QMove;
        public static MenuKeyBind ForceR;


        // Menu Bool
        public static MenuBool RKillable;
        public static MenuBool Flash;
        public static MenuBool Ignite;
        public static MenuBool JungleQ;
        public static MenuBool JungleW;
        public static MenuBool JungleE;
        public static MenuBool LaneVisible;
        public static MenuBool LaneQ;
        public static MenuBool LaneW;
        public static MenuBool LaneE;
        public static MenuBool BuyTrinket;
        public static MenuBool KeepQ;
        public static MenuBool DrawFleeSpots;
        public static MenuBool dind;
        public static MenuBool WallFlee;
        public static MenuBool UseSkin;
    }
}
