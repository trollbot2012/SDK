﻿#region

using System;
using System.Linq;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.Utils;
using Reforged_Riven.Extras;
using Reforged_Riven.Main;

#endregion

namespace Reforged_Riven.Update
{
    internal class Mode : Core
    {
        public static void Combo()
        {
            var target = Variables.TargetSelector.GetTarget(Player.AttackRange + 320, DamageType.Physical);

            if(target == null || !target.IsValid || target.IsZombie) return;

            if (Spells.R.IsReady() && Spells.R.Instance.Name == IsFirstR && MenuConfig.ForceR.Active) Logic.ForceR();

            if (Spells.W.IsReady() && Logic.InWRange(target)) Spells.W.Cast();

            if (Spells.R.IsReady() && Spells.R.Instance.Name == IsFirstR && Spells.W.IsReady() &&
                Spells.E.IsReady() && (Dmg.IsKillableR(target) || MenuConfig.ForceR.Active))
            {
                if (Logic.InWRange(target)) return;

                Spells.E.Cast(target.Position);
                Logic.ForceR();
                DelayAction.Add(170, Logic.ForceW);
                DelayAction.Add(30, () => Logic.ForceCastQ(target));
            }

            else if (Spells.W.IsReady() && Spells.E.IsReady())
            {
                Spells.E.Cast(target.Position);

                if (!Logic.InWRange(target)) return;

                DelayAction.Add(100, Logic.ForceW);
                DelayAction.Add(30, () => Logic.ForceCastQ(target));
            }

            else if (Spells.E.IsReady() && !Logic.InWRange(target))
            {
                if (!Logic.InWRange(target))
                {
                    Spells.E.Cast(target);
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

            Logic._qtarget = (Obj_AI_Base)args.Target;

            if (!(args.Target is Obj_AI_Minion)) return;

            var minions = GameObjects.EnemyMinions.Where(m => m.IsMinion && m.IsEnemy && m.Team != GameObjectTeam.Neutral && m.IsValidTarget(Player.AttackRange + Player.BoundingRadius + 260)).ToList();

            foreach (var m in minions)
            {
                if (Spells.E.IsReady())
                {
                    Spells.E.Cast(m.ServerPosition);
                }

                if (Spells.Q.IsReady())
                {
                    Logic.CastHydra();
                    Spells.Q.Cast(m.ServerPosition);
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

            var mob = ObjectManager.Get<Obj_AI_Minion>().Where(m => !m.IsDead && !m.IsZombie && m.Team == GameObjectTeam.Neutral && m.IsValidTarget(Spells.W.Range)).ToList();

            foreach (var m in mob)
            {
                if(!m.IsValid) return;

                if (Spells.E.IsReady() && MenuConfig.JungleE)
                {
                    Spells.E.Cast(m);
                }

                if (Spells.Q.IsReady() && MenuConfig.JungleQ)
                {
                    Logic.ForceItem();
                    Spells.Q.Cast(m);
                }

                if (!Spells.W.IsReady() || !MenuConfig.JungleW) continue;

                Logic.ForceItem();
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

                    if (target.Distance(Player) <= Spells.W.Range)
                    {
                        Logic.ForceW();
                    }
                }
            }

            if (!Spells.Q.IsReady() || !Spells.E.IsReady() || Qstack != 3) return;

            var epos = Player.ServerPosition + (Player.ServerPosition - target.ServerPosition).Normalized() * 300;

            Spells.E.Cast(epos);
            DelayAction.Add(150, () => Spells.Q.Cast(epos));
        }

        public static void QMove()
        {
            if (!MenuConfig.QMove.Active)
            {
                return;
            }

            Player.IssueOrder(GameObjectOrder.MoveTo, Game.CursorPos);

            if (Spells.Q.IsReady())
            {
                DelayAction.Add(50, () => Spells.Q.Cast(Game.CursorPos));
            }
        }

        public static void Flee()
        {
            if (MenuConfig.WallFlee)
            {
                var end = Player.ServerPosition.Extend(Game.CursorPos, Spells.Q.Range);
                var IsWallDash = FleeLogic.IsWallDash(end, Spells.Q.Range);

                var Eend = Player.ServerPosition.Extend(Game.CursorPos, Spells.E.Range);
                var WallE = FleeLogic.GetFirstWallPoint(Player.ServerPosition, Eend);
                var WallPoint = FleeLogic.GetFirstWallPoint(Player.ServerPosition, end);
                Player.GetPath(WallPoint);

                if (Spells.Q.IsReady() && Qstack < 3)
                { Spells.Q.Cast(Game.CursorPos); }


                if (!IsWallDash || Qstack != 3 || !(WallPoint.Distance(Player.ServerPosition) <= 800)) return;

                ObjectManager.Player.IssueOrder(GameObjectOrder.MoveTo, WallPoint);

                if (!(WallPoint.Distance(Player.ServerPosition) <= 600)) return;

                ObjectManager.Player.IssueOrder(GameObjectOrder.MoveTo, WallPoint);

                if (!(WallPoint.Distance(Player.ServerPosition) <= 45)) return;

                if (Spells.E.IsReady())
                {
                    Spells.E.Cast(WallE);
                }

                if (Qstack != 3 || !(end.Distance(Player.Position) <= 260) || !IsWallDash || !WallPoint.IsValid()) return;

                Player.IssueOrder(GameObjectOrder.MoveTo, WallPoint);
                Spells.Q.Cast(WallPoint);
            }

            else
            {
                var enemy = ObjectManager.Get<Obj_AI_Hero>().Where(hero => hero.IsValidTarget(Player.HasBuff("RivenFengShuiEngine")
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
                if (Spells.E.IsReady() && !Player.IsDashing()) Spells.E.Cast(x);
            }
        }
    }
}