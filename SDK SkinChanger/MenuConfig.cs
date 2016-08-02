using LeagueSharp.SDK.UI;

namespace SDK_SkinChanger
{
    class MenuConfig
    {
        private const string MenuName = "SDK SkinChanger";
        public static Menu MainMenu { get; set; } = new Menu(MenuName, MenuName, true);
        public static void Load()
        {
           // SkinMenu = MainMenu.Add(new Menu("SkinChanger", "SkinChanger"));
            UseSkin = MainMenu.Add(new MenuBool("UseSkin", "Use SkinChanger"));
            SkinChanger = MainMenu.Add(new MenuList<string>("Skins", "Skins", new[] { "Default", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" }));
            MainMenu.Attach();
        }
      //  public static Menu SkinMenu;
        public static MenuBool UseSkin;
        public static MenuList<string> SkinChanger;
    }
}
