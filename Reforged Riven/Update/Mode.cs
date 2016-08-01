#region

using System;
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
            if(!MenuConfig.BurstKeyBind.Active) return;

            if (!Spells.R.IsReady() || !Spells.W.IsReady() || !Spells.Q.IsReady() ||
                Spells.Flash == SpellSlot.Unknown || !Spells.Flash.IsReady()) return;

            var target = Variables.TargetSelector.GetSelectedTarget();

            if (target == null || !target.IsValidTarget(425 + Spells.W.Range - 35)) return;

            if (MenuConfig.Flash && target.Health > Dmg.GetComboDamage(target))
            {
                return;
            }

            Spells.E.Cast(target.Position);
            Logic.ForceR();
            Player.Spellbook.CastSpell(Spells.Flash, target);
            Logic.ForceW();
            DelayAction.Add(140, Logic.ForceItem);
            Spells.R.Cast(target);

        }

        public static void Combo()
        {
            var target = Variables.TargetSelector.GetTarget(Player.AttackRange + 310, DamageType.Physical);

            if (target == null || !target.IsValid || target.IsInvulnerable) return;

            if (Spells.R.IsReady() && Spells.R.Instance.Name == IsFirstR && MenuConfig.ForceR) Logic.ForceR();

            if (Spells.E.IsReady() && !Logic.InWRange(target))
            {
                Spells.E.Cast(target.ServerPosition);
            }

            if (Spells.R.IsReady() && Spells.R.Instance.Name == IsFirstR && Spells.W.IsReady() &&
                Spells.Q.IsReady() && MenuConfig.ForceR && !(Dmg.GetComboDamage(target) < target.Health))
            {
                Logic.CastYomu();

                Logic.ForceR();
                DelayAction.Add(70, Logic.ForceW);

                if (Qstack != 1) return;

                DelayAction.Add(160, () => Logic.ForceCastQ(target));
            }

            else if (MenuConfig.DoubleCast && Spells.W.IsReady() && Spells.Q.IsReady())
            {
                if (!Logic.InWRange(target)) return;

                Logic.ForceW();
                DelayAction.Add(160, () => Logic.ForceCastQ(target));
            }

           else if (Spells.W.IsReady() && Logic.InWRange(target))
            {
                Logic.ForceW();
            }
        }

        public static void Lane()
        {
            var minions = GameObjects.EnemyMinions.Where(m => m.IsValidTarget(Player.AttackRange + 350));

            foreach (var m in minions)
            {
                if (m.IsUnderEnemyTurret()) continue;

                if (Spells.E.IsReady() && MenuConfig.LaneE)
                {
                    Spells.E.Cast(m);
                }

                if (Spells.Q.IsReady() && MenuConfig.LaneQ)
                {
                    Logic.ForceItem();
                    Logic.ForceCastQ(m);
                }

                if (!Spells.W.IsReady() || !MenuConfig.LaneW) continue;

                if (!Logic.InWRange(m)) continue;

                if (m.Health < Spells.W.GetDamage(m) && !Player.IsWindingUp)
                {
                    Spells.W.Cast(m);
                }
            }
        }

        public static void Jungle()
        {
            var mobs = GameObjects.Jungle.Where(x => x.IsValidTarget(Player.GetRealAutoAttackRange(Player)));

            foreach (var m in mobs)
            {
                if (!m.IsValid) return;

                if (Spells.E.IsReady() && MenuConfig.JungleE)
                {
                    Spells.E.Cast(m.Position);
                }

                if (Spells.Q.IsReady() && MenuConfig.JungleQ)
                {
                    Logic.ForceItem();
                    Spells.Q.Cast(m);
                }

                if (!Spells.W.IsReady() || !MenuConfig.JungleW) return;

                Logic.ForceItem();
                Spells.W.Cast(m);
            }
        }

        public static void Harass()
        {
            var target = Variables.TargetSelector.GetTarget(400, DamageType.Physical);
            if (Spells.Q.IsReady() && Spells.W.IsReady() && Spells.E.IsReady() && Qstack == 1)
            {
                if (target.IsValidTarget())
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

            DelayAction.Add(Game.Ping + 2, () => Spells.Q.Cast(Player.Position - 15));
            
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

                if (Qstack != 3 || !(end.Distance(Player.ServerPosition) <= 260) || !wallPoint.IsValid()) return;


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