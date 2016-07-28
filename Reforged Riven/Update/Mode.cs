#region

using System.Linq;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.Utils;
using Reforged_Riven.Main;

#endregion

namespace Reforged_Riven.Update
{
    internal class Mode : Core
    {
        public static void Burst()
        {
            if (!MenuConfig.Flash) return;

            if (Spells.Flash == SpellSlot.Unknown || !Spells.Flash.IsReady()) return;

            if (!Spells.R.IsReady() || !Spells.W.IsReady() || !Spells.Q.IsReady()) return;

            var target = Variables.TargetSelector.GetSelectedTarget();

            if (target == null || !target.IsValidTarget(780) || target.IsDashing()) return;

            if(target.CountEnemyHeroesInRange(1500) >= MenuConfig.FlashEnemies) return;

            if(target.Health > Dmg.GetComboDamage(target) || target.Distance(Player) >= 600 ) return;

            Spells.E.Cast(target.Position);
            Spells.R.Cast();
            Player.Spellbook.CastSpell(Spells.Flash, target);
            Spells.W.Cast();
            DelayAction.Add(100, Logic.CastHydra);
        }

        public static void Combo()
        {
            var target = Variables.TargetSelector.GetTarget(Player.AttackRange + 375, DamageType.Physical);

            if (target == null || !target.IsValid || target.IsInvulnerable) return;

            if (Spells.R.IsReady() && Spells.R.Instance.Name == IsFirstR && MenuConfig.ForceR) Logic.ForceR();

            if (Spells.W.IsReady() && Logic.InWRange(target)) Spells.W.Cast();

            if (Spells.R.IsReady() && Spells.R.Instance.Name == IsFirstR && Spells.W.IsReady() &&
                Spells.E.IsReady() && (Dmg.IsKillableR(target) || MenuConfig.ForceR))
            {
                Logic.CastYomu();
                if (Logic.InWRange(target)) return;

                Spells.E.Cast(target.Position);
                Logic.ForceR();
                DelayAction.Add(70, Logic.ForceW);
                DelayAction.Add(45, () => Logic.ForceCastQ(target));
            }

            else if (Spells.W.IsReady() && Spells.E.IsReady())
            {
                Spells.E.Cast(target.Position);

                if (!Logic.InWRange(target)) return;

                DelayAction.Add(160, Logic.ForceW);
                DelayAction.Add(90, () => Logic.ForceCastQ(target));
            }

            else if (Spells.E.IsReady() && !Logic.InWRange(target))
            {
                if (!Logic.InWRange(target))
                {
                    Spells.E.Cast(target.Position);
                }
            }
        }


        // Lane
        public static void Lane(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!sender.IsMe)
            {
                return;
            }

            Logic.Qtarget = (Obj_AI_Base) args.Target;

            if (!(args.Target is Obj_AI_Minion)) return;

            var minions = GameObjects.EnemyMinions.Where(m =>
                        m.IsMinion && m.IsEnemy && m.Team != GameObjectTeam.Neutral &&
                        m.IsValidTarget(Player.AttackRange + Player.BoundingRadius + 375)).ToList();

            foreach (var m in minions)
            {
                if(m.IsUnderEnemyTurret()) return;

                if (Spells.E.IsReady())
                {
                    Spells.E.Cast(m.ServerPosition);
                }

                if (Spells.Q.IsReady())
                {
                    Spells.Q.Cast(m.ServerPosition);
                    Logic.CastHydra();
                }

                if (!Spells.W.IsReady() || !(m.Distance(Player) <= Spells.W.Range)) continue;

                if (!(m.Health < Spells.W.GetDamage(m))) continue;

                Logic.CastHydra();
                Spells.W.Cast(m);
            }
        }

        public static void Jungle()
        {
            if (Orbwalker.ActiveMode != OrbwalkingMode.LaneClear) return;

            var mob = ObjectManager.Get<Obj_AI_Minion>().Where(m =>
                            !m.IsDead && m.Team == GameObjectTeam.Neutral &&
                            m.IsValidTarget(Player.AttackRange + 375))
                            .ToList();

            foreach (var m in mob)
            {
                if (!m.IsValid) return;

                if (Spells.E.IsReady() && MenuConfig.JungleE)
                {
                    Spells.E.Cast(m.Position);
                }

                if (Spells.Q.IsReady() && MenuConfig.JungleQ)
                {
                    Spells.Q.Cast(m);
                }

                if (!Spells.W.IsReady() || !MenuConfig.JungleW) return;

                Logic.CastHydra();
                Spells.W.Cast(m);  
            }
        }

        public static void Harass()
        {
            var target = Variables.TargetSelector.GetTarget(400, DamageType.Physical);
            if (Spells.Q.IsReady() && Spells.W.IsReady() && Spells.E.IsReady() && Qstack == 1)
            {
                if (target.IsValidTarget() && !target.IsZombie)
                {
                    Logic.ForceCastQ(target);

                    if (Logic.InWRange(target))
                    {
                        Logic.ForceW();
                    }
                }
            }

            if (!Spells.Q.IsReady() || !Spells.E.IsReady() || Qstack < 3) return;

            var epos = Player.ServerPosition + (Player.ServerPosition - target.ServerPosition).Normalized()*300;

            Spells.E.Cast(epos);
            DelayAction.Add(190, () => Spells.Q.Cast(epos));
        }

        public static void QMove()
        {
            if (!MenuConfig.QMove.Active)
            {
                return;
            }

            if (!Spells.Q.IsReady()) return;

           
            Player.IssueOrder(GameObjectOrder.MoveTo, Game.CursorPos);
            DelayAction.Add(40, () => Spells.Q.Cast(Game.CursorPos));
        }

        public static void Flee()
        {
            if(!MenuConfig.FleeKey.Active) return;

            Player.IssueOrder(GameObjectOrder.MoveTo, Game.CursorPos);

            

            if (MenuConfig.WallFlee)
            {
                var end = Player.ServerPosition.Extend(Game.CursorPos, Spells.Q.Range);
                var isWallDash = FleeLogic.IsWallDash(end, Spells.Q.Range);

                var eend = Player.ServerPosition.Extend(Game.CursorPos, Spells.E.Range);
                var wallE = FleeLogic.GetFirstWallPoint(Player.ServerPosition, eend);
                var wallPoint = FleeLogic.GetFirstWallPoint(Player.ServerPosition, end);

                Player.GetPath(wallPoint);

                if (Spells.Q.IsReady() && Qstack < 3)
                {
                    Spells.Q.Cast(Game.CursorPos);
                }


                if (!isWallDash || Qstack != 3 || !(wallPoint.Distance(Player.ServerPosition) <= 800)) return;

                ObjectManager.Player.IssueOrder(GameObjectOrder.MoveTo, wallPoint);

                if (!(wallPoint.Distance(Player.ServerPosition) <= 600)) return;

                ObjectManager.Player.IssueOrder(GameObjectOrder.MoveTo, wallPoint);

                if (!(wallPoint.Distance(Player.ServerPosition) <= 45)) return;

                if (Spells.E.IsReady())
                {
                    Spells.E.Cast(wallE);
                }

                if (Qstack != 3 || !(end.Distance(Player.Position) <= 260) || !wallPoint.IsValid()) return;


                Player.IssueOrder(GameObjectOrder.MoveTo, wallPoint);
                Spells.Q.Cast(wallPoint);
            }

            else
            {
                var enemy =
                    ObjectManager.Get<Obj_AI_Hero>()
                        .Where(hero => hero.IsValidTarget(Player.HasBuff("RivenFengShuiEngine")
                            ? 70 + 195 + Player.BoundingRadius
                            : 70 + 120 + Player.BoundingRadius) && Spells.W.IsReady());

                var x = Player.Position.Extend(Game.CursorPos, 300);

                var objAiHeroes = enemy as Obj_AI_Hero[] ?? enemy.ToArray();

                if (Spells.W.IsReady() && objAiHeroes.Any())
                {
                    foreach (var target in objAiHeroes.Where(Logic.InWRange))
                    {
                        Spells.W.Cast();
                    }
                }

                if (Spells.Q.IsReady() && !Player.IsDashing()) Spells.Q.Cast(Game.CursorPos);
                Logic.CastYomu();
                if (Spells.E.IsReady() && !Player.IsDashing()) Spells.E.Cast(x);
            }
        }
    }
}