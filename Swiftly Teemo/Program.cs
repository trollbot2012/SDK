#region

using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using SharpDX;
using System;
using System.Linq;
using Swiftly_Teemo.Draw;
using Swiftly_Teemo.Handler;
using Swiftly_Teemo.Main;

#endregion

namespace Swiftly_Teemo
{
    internal class Program : Core
    {
        private static void Main(string[] args)
        {
            Bootstrap.Init(args);
            Events.OnLoad += Load;
        }
        private static void Load(object sender, EventArgs e)
        {
            if (GameObjects.Player.ChampionName != "Teemo")
            {
                Game.PrintChat("Failed to load Swiftly Teemo!");
                return;
            }

            Game.PrintChat("<b><font color=\"#FFFFFF\">[</font></b><b><font color=\"#00e5e5\">Swiftly Teemo</font></b><b><font color=\"#FFFFFF\">]</font></b><b><font color=\"#FFFFFF\"> Version: 5</font></b>");
            Game.PrintChat("<b><font color=\"#FFFFFF\">[</font></b><b><font color=\"#00e5e5\">Update</font></b><b><font color=\"#FFFFFF\">]</font></b><b><font color=\"#FFFFFF\"> Compiling Error</font></b>");

             Spells.Load();
             MenuConfig.Load();

            Drawing.OnDraw += Drawings.OnDraw;
            Drawing.OnEndScene += Drawing_OnEndScene;

          //  Spellbook.OnCastSpell += Mode.OnCastSpell;

            Orbwalker.OnAction += AfterAa.OnAction;

            Game.OnUpdate += OnUpdate;
        }
        private static void OnUpdate(EventArgs args)
        {
            if (Player.IsDead || Player.IsRecalling()) return;

            Killsteal.KillSteal();
            Mode.Skin();
            Mode.Flee();
          
            switch (Orbwalker.ActiveMode)
            {
                case OrbwalkingMode.LaneClear:
                    Mode.Lane();
                    Mode.Jungle();
                    break;
                case OrbwalkingMode.Combo:
                    Mode.Combo();
                    break;
                case OrbwalkingMode.None:
                    break;
                case OrbwalkingMode.Hybrid:
                    break;
                case OrbwalkingMode.LastHit:
                    break;
            }
            
        }

        private static void Drawing_OnEndScene(EventArgs args)
        {
            foreach (var enemy in ObjectManager.Get<Obj_AI_Hero>().Where(ene => ene.IsValidTarget() && ene.IsValidTarget(1000) && !ene.IsZombie))
            {
                if (!MenuConfig.Dind) continue;

                var easyKill = Spells.Q.IsReady() && Dmg.IsLethal(enemy) ? new ColorBGRA(0, 255, 0, 120) : new ColorBGRA(255, 255, 0, 120);

                Drawings.DrawHpBar.unit = enemy;
                Drawings.DrawHpBar.drawDmg(Dmg.ComboDmg(enemy), easyKill);
            }
        }
    }
}
