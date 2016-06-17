﻿#region

using System.Windows.Forms;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;
using Menu = LeagueSharp.SDK.UI.Menu;

#endregion

namespace Infected_Twitch.Menus
{
    internal class MenuConfig
    {
        private const string MenuName = "Infected Twitch";
        private static Menu MainMenu { get; set; } = new Menu(MenuName, MenuName, true);

        public static void Load()
        {
            // Combo
            ComboMenu = MainMenu.Add(new Menu("ComboMenu", "Combo"));
            ComboW = ComboMenu.Add(new MenuBool("ComboW", "Use W", true));
            UseYoumuu = ComboMenu.Add(new MenuBool("UseYoumuu", "Use Youmuu", true));
            UseBotrk = ComboMenu.Add(new MenuBool("UseBotrk", "Use Blade Of The Ruined King", true));

            // Harass
            HarassMenu = MainMenu.Add(new Menu("HarassMenu", "Harass"));
            HarassW = HarassMenu.Add(new MenuBool("HarassW", "Use W"));
            HarassE = HarassMenu.Add(new MenuSliderButton("HarassE", "E At Max E Range", 4, 0, 6, true));

            // Lane
            LaneMenu = MainMenu.Add(new Menu("LaneMenu", "Lane"));
            LaneW = LaneMenu.Add(new MenuBool("LaneW", "Use W", true));

            // Jungle
            JungleMenu = MainMenu.Add(new Menu("JungleMenu", "Jungle"));
            JungleW= JungleMenu.Add(new MenuBool("JungleW", "Use W", true));
            JungleE = JungleMenu.Add(new MenuBool("JungleE", "Use E", true));

            // Misc
            MiscMenu = MainMenu.Add(new Menu("MiscMenu", "Misc"));
            
            EBeforeDeath = MiscMenu.Add(new MenuBool("EBeforeDeath", "Use E Before Death", true));
            StealEpic = MiscMenu.Add(new MenuBool("StealEpic", "Steal Herald, Baron & Dragons", true));
            StealRed = MiscMenu.Add(new MenuBool("StealRed", "Steal Redbuff", true));
            QRecall = MiscMenu.Add(new MenuKeyBind("QRecall", "Q Recall", Keys.B, KeyBindType.Press));

            // Drawings
            DrawMenu = MainMenu.Add(new Menu("DrawMenu", "Drawings"));
            DrawDmg = DrawMenu.Add(new MenuBool("DrawDmg", "Damage Indicator", true));
            DrawTimer = DrawMenu.Add(new MenuBool("DrawTimer", "Q Timer", true));
            DrawKillable = DrawMenu.Add(new MenuBool("DrawKillable", "Killable By Passive", true));

            // Killsteal
            KillstealMenu = MainMenu.Add(new Menu("KillstealMenu", "Killsteal"));
            KillstealE = KillstealMenu.Add(new MenuBool("KillstealE", "Killsecure E", true));
            KillstealIgnite = KillstealMenu.Add(new MenuBool("KillstealIgnite", "Killsecure Ignite", true));

            // Trinket
            TrinketMenu = MainMenu.Add(new Menu("TrinketMenu", "Trinket"));
            BuyTrinket = TrinketMenu.Add(new MenuBool("BuyTrinket", "Buy Trinket"));
            TrinketList = TrinketMenu.Add(new MenuList<string>("TrinketList", "Choose Trinket", new[] { "Oracle Alternation", "Farsight Alternation" }));

            // Skin
            SkinMenu = MainMenu.Add(new Menu("SkinMenu", "Skinchanger"));
            UseSkin = SkinMenu.Add(new MenuBool("UseSkin", "Use Skinchanger"));
            SkinList = SkinMenu.Add(new MenuList<string>("Skins", "Skins", new[] { "Default", "Kingping Twitch", "Whistler Village Twitch", "Medieval Twitch", "Gangster Twitch", "Vandal Twitch", "Pickpocket Twitch", "SSW Twitch" }));

            // Exploit / Mechanic
            ExploitMenu = MainMenu.Add(new Menu("ExploitMenu", "Exploit"));
            ExploitMenu.Add(new Menu("x", "Note: This is safe to use!"));
            UseExploit = ExploitMenu.Add(new MenuBool("UseExploit", "Exploit"));
            Eaaq = ExploitMenu.Add(new MenuBool("EAAQ", "E AA Q"));
            Eaaaaq = ExploitMenu.Add(new MenuBool("EAAAAQ", "E AA AA Q"));
            //  DisableAA = ExploitMenu.Add(new MenuBool("DisableAA", "Disable AA After Q (Good for Repositioning)"));

            MainMenu.Attach();
        }
        // Menu
        public static Menu ComboMenu;
        public static Menu HarassMenu;
        public static Menu LaneMenu;
        public static Menu JungleMenu;
        public static Menu MiscMenu;
        public static Menu DrawMenu;
        public static Menu KillstealMenu;
        public static Menu TrinketMenu;
        public static Menu SkinMenu;
        public static Menu ExploitMenu;
        

        // List
        public static MenuList<string> SkinList;
        public static MenuList<string> TrinketList;

        // Keybind
        public static MenuKeyBind QRecall;

        // Slider
        public static MenuSliderButton HarassE;

        // Bool
        public static MenuBool ComboW;
        public static MenuBool HarassW;
        public static MenuBool LaneW;
        public static MenuBool JungleW;
        public static MenuBool JungleE;
        public static MenuBool KillstealE;
        public static MenuBool KillstealIgnite;
        public static MenuBool UseBotrk;
        public static MenuBool UseYoumuu;
        public static MenuBool BuyTrinket;
        public static MenuBool UseSkin;
        public static MenuBool UseExploit;
        public static MenuBool Eaaq;
        public static MenuBool Eaaaaq;
        public static MenuBool StealEpic;
        public static MenuBool StealRed;
        public static MenuBool DrawDmg;
        public static MenuBool DisableAa;
        public static MenuBool EBeforeDeath;
        public static MenuBool DrawTimer;
        public static MenuBool DrawKillable;
    }
}