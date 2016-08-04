#region

using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using SharpDX;
using System;
using System.Linq;
using Swiftly_Teemo.Draw;
using Swiftly_Teemo.Handler;
using Swiftly_Teemo.Menu;

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

            Obj_AI_Base.OnDoCast += ModeHandler.OnDoCast;
            
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
                    Mode.Jungle();
                    break;
                case OrbwalkingMode.Combo:
                   Mode.Combo();
                    break;
            }
            
        }

        private static readonly HpBarIndicator Indicator = new HpBarIndicator();

        private static void Drawing_OnEndScene(EventArgs args)
        {
           foreach (var enemy in ObjectManager.Get<Obj_AI_Hero>().Where(ene => ene.IsValidTarget(1500) && !ene.IsDead && ene.IsVisible))
            {
                Indicator.Unit = enemy;
                Indicator.DrawDmg(Dmg.ComboDmg(enemy), enemy.Health <= Dmg.ComboDmg(enemy) * 1.65 ? Color.LawnGreen : Color.Yellow);
            }
        }
    }
}
