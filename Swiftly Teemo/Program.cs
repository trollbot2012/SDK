#region

using System;
using System.Linq;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using SharpDX;
using Swiftly_Teemo.Draw;
using Swiftly_Teemo.Handler;
using Swiftly_Teemo.Main;

#endregion

namespace Swiftly_Teemo
{
    internal class Program : Core
    {
        static void Main(string[] args)
        {
            Bootstrap.Init(args);
            Events.OnLoad += Load;
        }
        private static void Load(object sender, EventArgs e)
        {
            if (GameObjects.Player.ChampionName != "Teemo")
            {
                return;
            }
            Game.PrintChat("<b><font color=\"#FFFFFF\">[</font></b><b><font color=\"#00e5e5\">Swiftly Teemo</font></b><b><font color=\"#FFFFFF\">]</font></b><b><font color=\"#FFFFFF\"> Version: 1</font></b>");
            Game.PrintChat("<b><font color=\"#FFFFFF\">[</font></b><b><font color=\"#00e5e5\">Update</font></b><b><font color=\"#FFFFFF\">]</font></b><b><font color=\"#FFFFFF\"> Release</font></b>");

             Spells.Load();
             MenuConfig.Load();

            Drawing.OnDraw += Drawings.OnDraw;
            Drawing.OnEndScene += Drawing_OnEndScene;
            Orbwalker.OnAction += AfterAA.OnAction;
            Game.OnUpdate += OnUpdate;
        }
        private static void OnUpdate(EventArgs args)
        {
            if (Player.IsDead || Player.IsRecalling())
            {
                return;
            }
            Killsteal.KillSteal();
            Mode.SKIN();
            switch (Orbwalker.GetActiveMode())
            {
                case OrbwalkingMode.LaneClear:
                    Mode.Lane();
                    Mode.Jungle();
                    break;
                case OrbwalkingMode.Combo:
                    Mode.Combo();
                    break;
            }
            Mode.Flee();
        }
        private static void Drawing_OnEndScene(EventArgs args)
        {
            foreach (var enemy in ObjectManager.Get<Obj_AI_Hero>().Where(ene => ene.IsValidTarget() && !ene.IsZombie))
            {
                if (MenuConfig.dind)
                {
                    var EasyKill = Spells.Q.IsReady() && Dmg.IsLethal(enemy)
                       ? new ColorBGRA(0, 255, 0, 120)
                       : new ColorBGRA(255, 255, 0, 120);
                    Drawings.DrawHpBar.unit = enemy;
                    Drawings.DrawHpBar.drawDmg(Dmg.ComboDmg(enemy), EasyKill);
                }
            }
        }
    }
}
