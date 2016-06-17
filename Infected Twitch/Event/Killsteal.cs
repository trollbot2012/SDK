#region

using System;
using Infected_Twitch.Core;
using Infected_Twitch.Menus;
using LeagueSharp.SDK;

#endregion

namespace Infected_Twitch.Event
{
    internal class Killsteal : Core.Core
    {
        public static void Update(EventArgs args)
        {
            var target = Variables.TargetSelector.GetTarget(600f);

            if (MenuConfig.KillstealIgnite)
            {
                if (!Spells.Ignite.IsReady()) return;

                if (target.IsValidTarget(600f) && Dmg.IgniteDmg >= target.Health)
                {
                    GameObjects.Player.Spellbook.CastSpell(Spells.Ignite, target);
                }
            }

            if (Target == null || Target.IsDead || !Target.IsValidTarget()) return;

            if (MenuConfig.KillstealE)
            {
                if (Dmg.EDamage(Target) >= Target.Health)
                {
                    Spells.E.Cast();
                }
            }

            if (Target.HealthPercent <= 10 && !Spells.Q.IsReady())
            {
                Usables.Botrk();
            }
        }
    }
}
