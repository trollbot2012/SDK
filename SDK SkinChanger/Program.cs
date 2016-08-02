using System;
using LeagueSharp;
using LeagueSharp.SDK;

namespace SDK_SkinChanger
{
    internal class Program
    {
        public static Obj_AI_Hero Player => ObjectManager.Player;

        private static void Main(string[] args)
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
            Skin();
        }

        public static void Skin()
        {
           Player.SetSkin(Player.CharData.BaseSkinName, MenuConfig.UseSkin ? MenuConfig.SkinChanger.Index : Player.BaseSkinId);
        }
    }
}