namespace Preserved_Kassadin.Update
{
    using System;
    using System.Linq;

    using LeagueSharp;
    using LeagueSharp.SDK;
    using LeagueSharp.SDK.Enumerations;

    using Preserved_Kassadin.Cores;

    internal class Mode : Core
    {
        public static void Update(EventArgs args)
        {
            if (MenuConfig.AutoHarass.Active)
            {
                Harass();
            }

            switch (Orbwalker.ActiveMode)
            {
                case OrbwalkingMode.None:
                    StackQ();
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

        public static void Combo()
        {
            if (!SafeTarget(Target)) return;

            if (Target.Distance(Player) <= Spells.Q.Range)
            {
                if (Spells.Q.IsReady())
                {
                    Spells.Q.Cast(Target);
                }
            }

            if (Target.Distance(Player) <= Spells.R.Range && Spells.R.IsReady())
            {
                if (Target.CountEnemyHeroesInRange(Spells.R.Range) >= MenuConfig.SafeR.Value) return;

                var rPred = Spells.R.GetPrediction(Target).CastPosition;
                Spells.R.Cast(rPred);
            }

            if (Target.Distance(Player) <= 250)
            {
                if (Spells.W.IsReady())
                {
                    Spells.W.Cast();
                }
            }

            if (Player.Distance(Target) <= Spells.E.Range && Spells.E.IsReady())
            {
                var ePred = Spells.E.GetPrediction(Target).CastPosition;

                Spells.E.Cast(ePred);
            }
        }

        public static void Harass()
        {
            if (!SafeTarget(Target) || !Spells.Q.IsReady()) return;
           
            if (Target.Distance(Player) <= Spells.Q.Range)
            {
                Spells.Q.Cast(Target);
            }
        }

        public static void StackQ()
        {
            var minions =
                GameObjects.EnemyMinions.Where(
                    m =>
                    m.IsMinion && m.IsEnemy && m.Team != GameObjectTeam.Neutral && m.IsValidTarget(Player.AttackRange))
                    .ToList();

            foreach (var m in minions)
            {
                if (!SafeTarget(m) || Player.IsWindingUp || !Spells.Q.IsReady())
                {
                    return;
                }

                if (MenuConfig.StackQ && Player.ManaPercent <= MenuConfig.StackMana.Value && m.Health <= Spells.Q.GetDamage(m))
                {
                    Spells.Q.Cast(m);
                }
            }
        }

        public static void Lane()
        {
            var minions =
                GameObjects.EnemyMinions.Where(
                    m =>
                    m.IsMinion && m.IsEnemy && m.Team != GameObjectTeam.Neutral && m.IsValidTarget(Player.AttackRange))
                    .ToList();

            foreach (var m in minions)
            {
                if (!SafeTarget(m) || Player.IsWindingUp) return;
              
                if (Player.ManaPercent <= MenuConfig.LaneMana.Value)
                {
                    if (MenuConfig.LaneW && m.Distance(Player) <= Player.AttackRange + 75 && Spells.W.IsReady())
                    {
                        Spells.W.Cast();
                    }

                    if (MenuConfig.LaneE && m.Distance(Player) <= Spells.E.Range && Spells.E.IsReady())
                    {
                        var ePred = Spells.E.GetCircularFarmLocation(minions);

                        if (ePred.MinionsHit > 3)
                        {
                            Spells.E.Cast(ePred.Position);
                        }
                    }

                    if (!MenuConfig.LaneR || !Spells.R.IsReady() || m.CountEnemyHeroesInRange(Spells.R.Range) > 0) return;

                    var rPred = Spells.R.GetCircularFarmLocation(minions);

                    if (rPred.MinionsHit < 3) return;

                    Spells.R.Cast(rPred.Position);
                }
            }
        }

        public static void Jungle()
        {
            var mob =
                ObjectManager.Get<Obj_AI_Minion>()
                    .Where(
                        m =>
                        !m.IsDead && !m.IsZombie && m.Team == GameObjectTeam.Neutral
                        && m.IsValidTarget(Player.AttackRange))
                    .ToList();

            foreach (var m in mob)
            {
                if (!SafeTarget(m)) return;
                if (Player.IsWindingUp) return;

                if (MenuConfig.JungleQ)
                {
                    if (Spells.Q.IsReady()) Spells.Q.Cast(m);
                }

                if (MenuConfig.JungleW)
                {
                    if (Spells.W.IsReady()) Spells.W.Cast();
                }

                if (MenuConfig.JungleE)
                {
                    if (Spells.E.IsReady())
                    {
                        var ePred = Spells.E.GetCircularFarmLocation(mob);

                        Spells.E.Cast(ePred.Position);
                    }
                }

                if (MenuConfig.JungleR)
                {
                    if (!Spells.R.IsReady()) return;
                    if (m.Health < Player.GetAutoAttackDamage(m)) return;

                    var rPred = Spells.R.GetCircularFarmLocation(mob);

                    Spells.R.Cast(rPred.Position);
                }
            }
        }
    }
}