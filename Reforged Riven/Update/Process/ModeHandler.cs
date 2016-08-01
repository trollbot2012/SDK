#region

using System;
using System.Linq;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using Reforged_Riven.Main;

#endregion

namespace Reforged_Riven.Update.Process
{
    internal class ModeHandler : Core
    {
        public static void OnDoCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!sender.IsMe) return;

            if (Orbwalker.ActiveMode == OrbwalkingMode.LaneClear)
            {
                if (args.Target is Obj_AI_Minion)
                {
                    Mode.Jungle();

                    Mode.Lane();
                }

                var turret = args.Target as Obj_AI_Turret;

                if (turret != null && MenuConfig.LaneQ)
                {
                    if (turret.IsValid && Spells.Q.IsReady())
                    {
                        Logic.ForceCastQ(turret);
                    }
                }
            }

            var a = GameObjects.EnemyHeroes.Where(x => x.IsValidTarget(Player.AttackRange + 360));

            var targets = a as Obj_AI_Hero[] ?? a.ToArray();

            if (Orbwalker.ActiveMode == OrbwalkingMode.Combo)
            {
                foreach (var target in targets)
                {
                    if (Spells.Q.IsReady() && Logic.InQRange(target))
                    {
                        Logic.ForceItem();
                        Logic.ForceCastQ(target);
                    }

                    if (MenuConfig.RKillable)
                    {
                        if (!Spells.R.IsReady() || Spells.R.Instance.Name != IsSecondR || Spells.Q.IsReady()) continue;

                        var pred = Spells.R.GetPrediction(target);
                        if (pred.Hitchance > HitChance.High)
                        {
                            Spells.R.Cast(pred.CastPosition);
                        }
                    }
                    else
                    {
                        if (!Spells.R.IsReady() || !Spells.Q.IsReady() || Spells.R.Instance.Name != IsSecondR) continue;

                        var pred = Spells.R.GetPrediction(target);
                        if (pred.Hitchance > HitChance.High)
                        {
                            Spells.R.Cast(pred.CastPosition);
                        }
                    }
                }
            }

            if (Orbwalker.ActiveMode != OrbwalkingMode.Hybrid || Qstack < 2 || !Spells.Q.IsReady()) return;

            foreach (var target in targets)
            {
                Logic.ForceItem();
                Logic.ForceCastQ(target);
            }
        }
    }
}