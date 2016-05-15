using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;
using System;
using System.Linq;

namespace SDK_SkinChanger
{
    class Program
    {
        public static Obj_AI_Hero Player => ObjectManager.Player;
        static void Main(string[] args)
        {
            Bootstrap.Init(args);
            Events.OnLoad += Load;
        }
        private static void Load(object sender, EventArgs e)
        {
            Game.PrintChat("<b><font color=\"#FFFFFF\">[</font></b><b><font color=\"#00e5e5\">SDK SkinChanger</font></b><b><font color=\"#FFFFFF\">]</font></b><b><font color=\"#FFFFFF\"> Loaded</font></b>");
            Game.OnUpdate += OnUpdate;

            MenuConfig.Load();
        }
        private static void OnUpdate(EventArgs args)
        {
            SKIN();
        }
        public static void SKIN()
        {
            if (MenuConfig.UseSkin)
            {
                switch (MenuConfig.SkinChanger.Index)
                {
                    case 0:
                        Player.SetSkin(Player.CharData.BaseSkinName, 0);
                        break;
                    case 1:
                        Player.SetSkin(Player.CharData.BaseSkinName, 1);
                        break;
                    case 2:
                        Player.SetSkin(Player.CharData.BaseSkinName, 2);
                        break;
                    case 3:
                        Player.SetSkin(Player.CharData.BaseSkinName, 3);
                        break;
                    case 4:
                        Player.SetSkin(Player.CharData.BaseSkinName, 4);
                        break;
                    case 5:
                        Player.SetSkin(Player.CharData.BaseSkinName, 5);
                        break;
                    case 6:
                        Player.SetSkin(Player.CharData.BaseSkinName, 6);
                        break;
                    case 7:
                        Player.SetSkin(Player.CharData.BaseSkinName, 7);
                        break;
                    case 8:
                        Player.SetSkin(Player.CharData.BaseSkinName, 8);
                        break;
                    case 9:
                        Player.SetSkin(Player.CharData.BaseSkinName, 9);
                        break;
                }
            }
            else
            {
                Player.SetSkin(Player.CharData.BaseSkinName, Player.BaseSkinId);
            }
        }
    }
}
