﻿#region

using System;
using System.Linq;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.Utils;
using Reforged_Riven.Extras;
using Reforged_Riven.Menu;

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

                    var minions = GameObjects.EnemyMinions.Where(m => m.IsValidTarget(Player.AttackRange + 380));
                    foreach (var m in minions)
                    {
                        if (MenuConfig.LaneVisible && m.CountEnemyHeroesInRange(1500) > 0) continue;
                        if (m.IsUnderEnemyTurret()) continue;

                        if (!Spells.Q.IsReady() || !MenuConfig.LaneQ) continue;

                        Logic.ForceItem();
                        Logic.ForceCastQ(m);
                    }
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
                    if (!Spells.Q.IsReady() || !Logic.InWRange(target)) continue;

                    Logic.ForceItem();
                    Logic.ForceCastQ(target);
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