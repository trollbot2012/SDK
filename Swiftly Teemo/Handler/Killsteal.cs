#region

using System.Linq;
using LeagueSharp;
using LeagueSharp.SDK;
using Swiftly_Teemo.Menu;

#endregion

namespace Swiftly_Teemo.Handler
{
    internal class Killsteal : Core
    {
        public static void KillSteal()
        {
            foreach (var target in GameObjects.EnemyHeroes.Where(x => x.IsValidTarget(Spells.Q.Range)))
            {
                if (target == null)
                {
                    return;
                }

                if (Spells.Q.IsReady() && target.Health < Spells.Q.GetDamage(target))
                {
                    Spells.Q.Cast(target);
                }
            }

            if (!MenuConfig.KillStealSummoner || !Spells.Ignite.IsReady() || Spells.Ignite == SpellSlot.Unknown)
            {
                return;
            }

            foreach (var target in GameObjects.EnemyHeroes.Where(t => t.IsValidTarget(600f)))
            {
                if (target == null)
                {
                    return;
                }

                if (target.Health < Dmg.IgniteDmg)
                {
                    GameObjects.Player.Spellbook.CastSpell(Spells.Ignite, target);
                }
            }
        }
    }
}

