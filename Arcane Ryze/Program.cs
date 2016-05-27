﻿#region

using System;
using System.Linq;
using Arcane_Ryze.Draw;
using Arcane_Ryze.Handler;
using Arcane_Ryze.Main;
using Arcane_Ryze.Modes;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using SharpDX;

#endregion

namespace Arcane_Ryze
{
    internal class Program : Core
    {
        Random Random = new Random();
        internal static float Timer;
        internal static float LastQ;
        static void Main(string[] args)
        {
            Bootstrap.Init(args);
            Events.OnLoad += Load;
        }

        private static void Load(object sender, EventArgs e)
        {
            
                if (GameObjects.Player.ChampionName != "Ryze")
                {
                    return;
                }
            Game.PrintChat("<b><font color=\"#FFFFFF\">[</font></b><b><font color=\"#00e5e5\">Arcane Ryze</font></b><b><font color=\"#FFFFFF\">]</font></b><b><font color=\"#FFFFFF\"> Version: 1</font></b>");
            Game.PrintChat("<b><font color=\"#FFFFFF\">[</font></b><b><font color=\"#00e5e5\">Update</font></b><b><font color=\"#FFFFFF\">]</font></b><b><font color=\"#FFFFFF\"> Release</font></b>");

            Spells.Load();
            MenuConfig.Load();
            Orbwalker.OnAction += BeforeAA.OnAction;
           
            Game.OnUpdate += OnUpdate;
            Drawing.OnEndScene += Drawing_OnEndScene;
        }
       
        private static void OnUpdate(EventArgs args)
        {
            if (Player.IsDead || Player.IsRecalling())
            {
                return;
            }
            //  Console.WriteLine("Buffs: {0}", string.Join(" | ", Player.Buffs.Where(b => b.Caster.NetworkId == Player.NetworkId).Select(b => b.DisplayName)));

          
            // Useless Code.
            switch (Orbwalker.ActiveMode)
            {
                case OrbwalkingMode.Combo:
                    Combo.ComboLogic();
                    break;
                case OrbwalkingMode.Hybrid:
                    Harass.HarassLogic();
                    break;
                case OrbwalkingMode.LaneClear:
                    Jungle.JungleLogic();
                    Lane.LaneLogic();
                    break;
                case OrbwalkingMode.LastHit:
                    LastHit.LastHitLogic();
                    break;
            }
            KillSteal.Killsteal();

        }
       public static HpBarDraw HpBarDraw = new HpBarDraw();
        private static void Drawing_OnEndScene(EventArgs args)
        {
           foreach (var enemy in ObjectManager.Get<Obj_AI_Hero>().Where(ene => ene.IsValidTarget() && !ene.IsZombie))
            {
                if(MenuConfig.dind)
                {
                    var EasyFuckingKill = Spells.Q.IsReady() && Dmg.EasyFuckingKillKappa(enemy)
                        ? new ColorBGRA(0, 255, 0, 120)
                        : new ColorBGRA(255, 255, 0, 120);
                    HpBarDraw.unit = enemy;
                    HpBarDraw.drawDmg(Dmg.EDmg(enemy) + Dmg.QDmg(enemy) + Dmg.WDmg(enemy), EasyFuckingKill);
                }
            }
        }
    }
}
