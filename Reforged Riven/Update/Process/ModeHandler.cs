#region

using System;
using System.Linq;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.Utils;
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
                    if (Spells.W.IsReady() && Spells.Q.IsReady() && Spells.E.IsReady())
                    {
                        Spells.E.Cast(target.ServerPosition);

                        if (Spells.R.IsReady() && Spells.R.Instance.Name == IsFirstR && MenuConfig.ForceR &&
                            !(Dmg.GetComboDamage(target) < target.Health))
                        {
                            Logic.ForceR();
                        }

                        DelayAction.Add(10, Logic.ForceItem);
                        DelayAction.Add(70, ()=> Spells.W.Cast());

                        if (Qstack != 1) return;
                        DelayAction.Add(160, () => Logic.ForceCastQ(target));
                    }

                    else if (Spells.W.IsReady() && Logic.InWRange(target))
                    {
                        Logic.ForceW();
                    }

                    else if (Spells.Q.IsReady() && Logic.InWRange(target))
                    {
                        Logic.ForceItem();
                        Logic.ForceCastQ(target);
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