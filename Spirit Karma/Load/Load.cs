using LeagueSharp;
using LeagueSharp.SDK;
using Spirit_Karma.Core;
using Spirit_Karma.Draw;
using Spirit_Karma.Event;

namespace Spirit_Karma.Load
{
    internal class Load
    {
        public static void LoadAssembly()
        {

            Drawing.OnDraw += DrawMantra.OnDraw;
            Drawing.OnEndScene += DrawDmg.OnDrawEnemy;

            Events.OnInterruptableTarget += Interrupt.OnInterruptableTarget;

            Game.OnUpdate += SkinChanger.OnUpdate;
            Game.OnUpdate += Mode.OnUpdate;

            Spells.Load();
            Menus.MenuConfig.Load();
         //   AssemblyVersion.CheckVersion();

            Game.PrintChat("<b><font color=\"#FFFFFF\">[</font></b><b><font color=\"#00e5e5\">Spirit Karma</font></b><b><font color=\"#FFFFFF\">]</font></b><b><font color=\"#FFFFFF\"> Loaded Sucessfully</font></b>");
        }
    }
}
