#region

using System;
using System.Linq;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.Utils;
using Reforged_Riven.Extras;
using Reforged_Riven.Main;

#endregion

namespace Reforged_Riven.Update.Process
{
    internal class ModeHandler : Core
    {
        public static void OnDoCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            var spellName = args.SData.Name;

            if (!sender.IsMe) return;

            Logic._qtarget = (Obj_AI_Base) args.Target;

            if (args.Target is Obj_AI_Minion)
            {
                if (Orbwalker.ActiveMode == OrbwalkingMode.LaneClear)
                {
                    Mode.Jungle();
                    
                    var minions =
                        GameObjects.EnemyMinions.Where(m => m.IsMinion && m.IsEnemy && m.Team != GameObjectTeam.Neutral && m.IsValidTarget(Player.AttackRange + Player.BoundingRadius));

                    foreach (var m in minions)
                    {
                        if (Spells.E.IsReady() && MenuConfig.LaneE)
                        {
                            Spells.E.Cast(m);
                        }

                        if (Spells.Q.IsReady() && MenuConfig.LaneQ)
                        {
                            Logic.ForceCastQ(m);
                            Usables.CastHydra();
                        }

                        if (Spells.W.IsReady() && MenuConfig.LaneW)
                        {
                            if (m.Health < Spells.W.GetDamage(m) && Player.IsWindingUp)
                            {
                                Spells.W.Cast(m);
                            }
                        }
                    }
                }
            }

            var turret = args.Target as Obj_AI_Turret;

            if (turret != null)
            {
                if (turret.IsValid && args.Target != null && Spells.Q.IsReady() && MenuConfig.LaneQ && Orbwalker.ActiveMode == OrbwalkingMode.LaneClear)
                {
                    Logic.ForceCastQ(turret);
                }
            }

            var target = args.Target as Obj_AI_Hero;

            if (target == null) return;

            if (Orbwalker.ActiveMode == OrbwalkingMode.Combo)
            {
                if (Spells.E.IsReady())
                {
                    Spells.E.Cast(target);
                    Usables.CastHydra();
                }

                if (Spells.W.IsReady() && Logic.InWRange(target))
                {
                    Usables.CastHydra();
                    Spells.W.Cast();
                }

                if (Spells.Q.IsReady())
                {
                    Logic.ForceCastQ(target);
                    Usables.CastHydra();
                }

                if (MenuConfig.RKillable)
                {
                    if (Spells.R.IsReady() && Spells.R.Instance.Name == IsSecondR && !Spells.Q.IsReady())
                    {
                        Spells.R.Cast(target.Position);
                    }
                }
                else
                {
                    if (Spells.R.IsReady() && Qstack > 2 && Spells.R.Instance.Name == IsSecondR)
                    {
                        Spells.R.Cast(target.Position);
                    }
                }
            }

            if (Orbwalker.ActiveMode != OrbwalkingMode.Hybrid || Qstack != 2 || !Spells.Q.IsReady()) return;

            Usables.CastHydra();
            Logic.ForceItem();
            Logic.ForceCastQ(target);
        }
    }
}
