using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;
using SharpDX;
using System;
using System.Linq;

namespace Swiftly_Teemo.Handler
{
    class AfterAA : Core
    {
        public static void OnAction(object sender, OrbwalkingActionArgs e)
        {
            if (Variables.Orbwalker.ActiveMode == OrbwalkingMode.Combo || Variables.Orbwalker.ActiveMode == OrbwalkingMode.Hybrid)
            {
                if (e.Type == OrbwalkingType.AfterAttack)
                {
                    if (Spells.Q.IsReady())
                    {
                        Spells.Q.Cast(Target);
                    }
                }
            }

            if (Variables.Orbwalker.ActiveMode != OrbwalkingMode.LaneClear) return;
            if (e.Type != OrbwalkingType.AfterAttack) return;

            var mob = GameObjects.Jungle.Where(m => m.IsValidTarget(Spells.Q.Range) && !GameObjects.JungleSmall.Contains(m)).ToList();

            foreach (var m in mob)
            {
                if (Spells.Q.IsReady())
                {
                    Spells.Q.Cast(m);
                }
            }
        }
    }
}
