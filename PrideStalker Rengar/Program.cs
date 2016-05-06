using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;
using SharpDX;
using System;
using System.Linq;
using PrideStalker_Rengar.Main;
using PrideStalker_Rengar.Handlers;
using PrideStalker_Rengar.Draw;

namespace PrideStalker_Rengar
{
    class Program : Core
    {
        static void Main(string[] args)
        {
            Events.OnLoad += Load;
        }

        private static void Load(object sender, EventArgs e)
        {
            if (GameObjects.Player.ChampionName != "Rengar")
            {
                return;
            }

            Game.PrintChat("<b><font color=\"#FFFFFF\">[</font></b><b><font color=\"#00e5e5\">Nechrito Rengar</font></b><b><font color=\"#FFFFFF\">]</font></b><b><font color=\"#FFFFFF\"> Version: 2 (Date: 5/6/2016)</font></b>");
            Game.PrintChat("<b><font color=\"#FFFFFF\">[</font></b><b><font color=\"#00e5e5\">Update</font></b><b><font color=\"#FFFFFF\">]</font></b><b><font color=\"#FFFFFF\"> Ap Combo Logic</font></b>");

            Spells.Load();
            MenuConfig.Load();
            
            Drawing.OnDraw += DRAW.OnDraw;
            Drawing.OnEndScene += Drawing_OnEndScene;
            Game.OnUpdate += OnUpdate;
        }

        private static void OnUpdate(EventArgs args)
        {
            if(Player.IsDead || Player.IsRecalling())
            {
                return;
            }
            KillSteal.Killsteal();
            Mode.SKIN();
            if (Variables.Orbwalker.ActiveMode == OrbwalkingMode.Combo)
            {
                switch(MenuConfig.ComboMode.SelectedValue)
                {
                    case "Gank":
                        Mode.Combo();
                        break;
                    case "Triple Q":
                        Mode.TripleQ();
                        break;
                    case "Ap Combo":
                        Mode.ApCombo();
                        break;
                }
            }
            switch(Variables.Orbwalker.GetActiveMode())
            {
                case OrbwalkingMode.LaneClear:
                    Mode.Lane();
                    Mode.Jungle();
                    break;
                case OrbwalkingMode.LastHit:
                    Mode.LastHit();
                    break;
            }
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
                    DRAW.DrawHpBar.Unit = enemy;
                    DRAW.DrawHpBar.DrawDmg(Dmg.ComboDmg(enemy), EasyKill);
                }
            }
        }

    }
}
