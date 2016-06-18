#region

using System;
using System.Linq;
using Infected_Twitch.Core;
using Infected_Twitch.Menus;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using static Infected_Twitch.Core.Spells;

#endregion

namespace Infected_Twitch.Event
{
    internal class Modes : Core.Core
    {
        public static void Update(EventArgs args)
        {
           
            switch (Orbwalker.ActiveMode)
            {
                case OrbwalkingMode.None:
                    AutoE();
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
                case OrbwalkingMode.LastHit:
                    break;
            }
        }

        public static readonly string[] Monsters =
        {
            "SRU_Red", "SRU_Gromp", "SRU_Krug", "SRU_Razorbeak", "SRU_Murkwolf"
        };

        public static readonly string[] Dragons =
        {
            "SRU_Dragon_Air", "SRU_Dragon_Fire", "SRU_Dragon_Water", "SRU_Dragon_Earth", "SRU_Dragon_Elder", "SRU_Baron",
            "SRU_RiftHerald"
        };

        private static void AutoE()
        {
            if (!E.IsReady()) return;

            if (MenuConfig.StealEpic)
            {
                foreach (var m in ObjectManager.Get<Obj_AI_Base>().Where(x => Dragons.Contains(x.CharData.BaseSkinName) && !x.IsDead))
                {
                    if (m.Health < E.GetDamage(m))
                    {
                        E.Cast();
                    }
                }
            }

            if (!MenuConfig.StealRed) return;

            var mob = ObjectManager.Get<Obj_AI_Minion>().Where(m => !m.IsDead && !m.IsZombie && m.Team == GameObjectTeam.Neutral && m.IsValidTarget(E.Range)).ToList();

            foreach (var m in mob)
            {
                if (!m.CharData.BaseSkinName.Contains("SRU_Red")) continue;

                if (m.Health < E.GetDamage(m))
                {
                    E.Cast();
                }
            }

            if (!SafeTarget(Target)) return;
            if (!MenuConfig.KillstealE || MenuConfig.UseExploit) return;
            if (!(Dmg.EDamage(Target) >= Target.Health)) return;

            E.Cast();

            if (MenuConfig.Debug)
            {
                Game.PrintChat("Killteal E Active");
            }
        }

        private static void Combo()
        {
            if(Orbwalker.ActiveMode != OrbwalkingMode.Combo) return;

            if (!SafeTarget(Target)) return;

            if (MenuConfig.ComboE)
            {
                if(!E.IsReady()) return;
                if (Target.Health <= Dmg.EDamage(Target))
                {
                    E.Cast();

                    if (MenuConfig.Debug)
                    {
                        Game.PrintChat("Combo => Casting E");
                    }

                }
            }
            

            if (MenuConfig.UseYoumuu && Target.IsValidTarget(Player.AttackRange))
            {
                Usables.CastYomu();
            }

            if (Target.HealthPercent <= 70 && !MenuConfig.UseExploit)
            {
                Usables.Botrk();
            }

           
            if (!MenuConfig.ComboW) return;
             if (!W.IsReady()) return;
              if (!Target.IsValidTarget(W.Range))
               if (Target.Health <= Player.GetAutoAttackDamage(Target) * 2 && Target.Distance(Player) < Player.AttackRange) return;
                 if (!(Player.ManaPercent >= 7.5)) return;

            var wPred = W.GetPrediction(Target).CastPosition;

            W.Cast(wPred);
        }

        private static void Harass()
        {
            if (Target == null || Target.IsInvulnerable || !Target.IsValidTarget()) return;

            if (Dmg.Stacks(Target) >= MenuConfig.HarassE && Target.Distance(Player) >= Player.AttackRange + 50)
            {
                E.Cast();
            }

            if (!MenuConfig.HarassW) return;

            var wPred = W.GetPrediction(Target).CastPosition;

            W.Cast(wPred);
        }

        private static void Lane()
        {
            var minions = GameObjects.EnemyMinions.Where(m => m.IsMinion && m.IsEnemy && m.Team != GameObjectTeam.Neutral && m.IsValidTarget(Player.AttackRange)).ToList();
            if (!MenuConfig.LaneW) return;
            if (!W.IsReady()) return;

            var wPred = W.GetCircularFarmLocation(minions);

            if (wPred.MinionsHit >= 4)
            {
                W.Cast(wPred.Position);
            }
        }

        private static void Jungle()
        {
            if (Player.Level == 1) return;
            var mob = ObjectManager.Get<Obj_AI_Minion>().Where(m => !m.IsDead && !m.IsZombie && m.Team == GameObjectTeam.Neutral && !GameObjects.JungleSmall.Contains(m) && m.IsValidTarget(E.Range)).ToList();

            if (MenuConfig.JungleW && Player.ManaPercent >= 20)
            {
                if (mob.Count == 0) return;

                var wPrediction = W.GetCircularFarmLocation(mob);
                if (wPrediction.MinionsHit >= 3)
                {
                    W.Cast(wPrediction.Position);
                }
            }

            if (!MenuConfig.JungleE) return;
            if(!E.IsReady()) return;
                
            foreach (var m in ObjectManager.Get<Obj_AI_Base>().Where(x => Monsters.Contains(x.CharData.BaseSkinName) && !x.IsDead))
            {
                if (m.Health < Dmg.EDamage(m))
                {
                    E.Cast();
                }
            }
        }
    }
}
