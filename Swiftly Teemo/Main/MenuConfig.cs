using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;

namespace Swiftly_Teemo.Main
{
    class MenuConfig
    {
        public Menu TargetSelectorMenu;
        private const string MenuName = "Swiftly Teemo";
        public static Menu MainMenu { get; set; } = new Menu(MenuName, MenuName, true);
        public static void Load()
        {
            ComboMenu = MainMenu.Add(new Menu("ComboMenu", "Combo"));
            KillStealSummoner = ComboMenu.Add(new MenuBool("KillStealSummoner", "KillSteal Summoner", true));

            LaneMenu = MainMenu.Add(new Menu("LaneMenu", "Lane"));
            LaneQ = LaneMenu.Add(new MenuBool("LaneQ", "Last Hit Q AA", true));

            DrawMenu = MainMenu.Add(new Menu("Draw", "Draw"));
            dind = DrawMenu.Add(new MenuBool("dind", "Damage Indicator", true));
            EngageDraw = DrawMenu.Add(new MenuBool("EngageDraw", "Draw Engage", true));

            SkinMenu = MainMenu.Add(new Menu("SkinChanger", "SkinChanger"));
            UseSkin = SkinMenu.Add(new MenuBool("UseSkin", "Use SkinChanger"));
            SkinChanger = SkinMenu.Add(new MenuList<string>("Skins", "Skins", new[] { "Default", "Happy Elf Teemo", "Recon Teemo", "Badger Teemo", "Astronaut Teemo", "Cottontail Teemo", "Super Teemo", "Panda Teemo", "Omega Squad Teemo" }));
            Flee = MainMenu.Add(new MenuKeyBind("Flee", "Flee", System.Windows.Forms.Keys.A, KeyBindType.Press));
            MainMenu.Attach();
        }
        public static Menu ComboMenu;
        public static Menu DrawMenu;
        public static Menu LaneMenu;
        public static Menu SkinMenu;

        public static MenuKeyBind Flee;

        public static MenuList<string> SkinChanger;

        public static MenuBool LaneQ;
        public static MenuBool EngageDraw;
        public static MenuBool dind;
        public static MenuBool UseSkin;
        public static MenuBool KillStealSummoner;
    }
}
