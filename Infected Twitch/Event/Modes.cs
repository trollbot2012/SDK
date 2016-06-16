﻿#region

using System;
using System.Linq;
using Infected_Twitch.Core;
using Infected_Twitch.Menus;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;

#endregion

namespace Infected_Twitch.Event
{
    internal class Modes : Core.Core
    {
        public static void Update(EventArgs args)
        {
            AutoE();
            switch (Orbwalker.ActiveMode)
            {
                case OrbwalkingMode.None:
                    break;
                case OrbwalkingMode.Combo:
                    Combo();
                    break;
                case OrbwalkingMode.Hybrid:
                    Harass();
                    break;
                case OrbwalkingMode.LaneClear:
                    Lane();
                    Jungle();
                    break;
            }
        }

        private static void AutoE()
        {
            if(!Spells.E.IsReady()) return;

            if (MenuConfig.KillstealE)
            {
                if(Target == null || Target.IsDead || Target.IsInvulnerable || !Target.IsValidTarget(Spells.E.Range)) return;
                if (Target.Health < Dmg.EDamage(Target))
                {
                    Spells.E.Cast();
                }
            }
           
            if (MenuConfig.StealEpic)
            {
                foreach (var m in ObjectManager.Get<Obj_AI_Base>().Where(x => Dragons.Contains(x.CharData.BaseSkinName) && !x.IsDead))
                {
                    if (m.Health < Player.GetSpellDamage(m, SpellSlot.E))
                    {
                        Spells.E.Cast();
                    }
                }
            }

            if(!MenuConfig.StealRed) return;

            var mob = ObjectManager.Get<Obj_AI_Minion>().Where(m => !m.IsDead && !m.IsZombie && m.Team == GameObjectTeam.Neutral && !GameObjects.JungleSmall.Contains(m) && m.IsValidTarget(Spells.E.Range)).ToList();

            foreach (var m in mob)
            {
                if (m.CharData.BaseSkinName.Contains("SRU_Red"))
                {
                    if (m.Health < Player.GetSpellDamage(m, SpellSlot.E))
                    {
                        Spells.E.Cast();
                    }
                }
            }
        }

        private static void Combo()
        {
           if(!MenuConfig.ComboW) return;

            if(Target == null || Target.IsInvulnerable || !Target.IsValidTarget(Spells.W.Range)) return;

            if (MenuConfig.UseYoumuu && Target.IsValidTarget(Spells.W.Range))
            {
                Usables.CastYomu();
            }

            if (Target.HealthPercent <= 70)
            {
                Usables.Botrk();
            }
             

             if(Target.Health < Player.GetAutoAttackDamage(Target) * 2 && Target.Distance(Player) < Player.AttackRange) return;

               if (!Spells.W.IsReady()) return;

                if (!(Player.ManaPercent >= 7.5)) return;


            var wPred = Spells.W.GetPrediction(Target).CastPosition;

            Spells.W.Cast(wPred);
        }

        private static void Harass()
        {
            if(Target == null || Target.IsInvulnerable || !Target.IsValidTarget()) return;

            if (Dmg.Stacks(Target) >= MenuConfig.HarassE && Target.Distance(Player) >= Player.AttackRange + 50)
            {
                Spells.E.Cast();
            }

            if (!MenuConfig.HarassW) return;

            var wPred = Spells.W.GetPrediction(Target).CastPosition;

            Spells.W.Cast(wPred);
        }

        private static void Lane()
        {
            var minions = GameObjects.EnemyMinions.Where(m => m.IsMinion && m.IsEnemy && m.Team != GameObjectTeam.Neutral && m.IsValidTarget(Player.AttackRange)).ToList();
            if(!MenuConfig.LaneW) return;
            if(!Spells.W.IsReady()) return;

            var wPred = Spells.W.GetCircularFarmLocation(minions);

            if (wPred.MinionsHit >= 4)
            {
                Spells.W.Cast(wPred.Position);
            }
                
        }

        private static void Jungle()
        {
            if(Player.Level == 1) return;
            var mob = ObjectManager.Get<Obj_AI_Minion>().Where(m => !m.IsDead && !m.IsZombie && m.Team == GameObjectTeam.Neutral && !GameObjects.JungleSmall.Contains(m) && m.IsValidTarget(Spells.E.Range)).ToList();

            if (MenuConfig.JungleW && Player.ManaPercent >= 20)
            {
                if (mob.Count == 0) return;

                var wPrediction = Spells.W.GetCircularFarmLocation(mob);
                if(wPrediction.MinionsHit >= 2)
                {
                    Spells.W.Cast(wPrediction.Position);
                }
               
            }
           
            if(!MenuConfig.JungleE) return;

            foreach (var m in mob)
            {
                if (m.Health < Player.GetSpellDamage(m, SpellSlot.E))
                {
                    Spells.E.Cast();
                }
            }
        }
    }
}
