#region

using System.Linq;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using Swiftly_Teemo.Main;

#endregion

namespace Swiftly_Teemo.Handler
{
    internal class AfterAa : Core
    {
        public static void OnAction(object sender, OrbwalkingActionArgs e)
        {
          
            if (Variables.Orbwalker.ActiveMode == OrbwalkingMode.Combo || Variables.Orbwalker.ActiveMode == OrbwalkingMode.Hybrid)
            {  
                if (Target == null || Target.IsDead || Target.IsInvulnerable || !Target.IsValidTarget(Spells.Q.Range)) return;
                if (e.Type == OrbwalkingType.AfterAttack)
                {
                    if (MenuConfig.TowerCheck && Target.IsUnderEnemyTurret()) return;
                    if (Spells.Q.IsReady())
                    {
                        Spells.Q.Cast(Target);
                    }
                }
            }

            if (Variables.Orbwalker.ActiveMode != OrbwalkingMode.LaneClear) return;
            if (e.Type != OrbwalkingType.AfterAttack) return;

            var mob = GameObjects.Jungle.Where(m => m != null && m.IsValidTarget(Player.AttackRange) && !GameObjects.JungleSmall.Contains(m));

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
