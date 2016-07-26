#region

using System;
using System.Linq;
using LeagueSharp;
using LeagueSharp.SDK;
using Reforged_Riven.Main;

#endregion

namespace Reforged_Riven.Update
{
    internal class KillSteal : Core
    {
        public static void Update(EventArgs args)
        {
            foreach (var target in ObjectManager.Get<Obj_AI_Hero>().OrderBy(hp => hp.Health))
            {
                if(target == null) return;

                if (target.HasBuff("kindrednodeathbuff") || target.HasBuff("Undying Rage") ||
                    target.HasBuff("JudicatorIntervention")) return;

                if (Spells.Q.IsReady())
                {
                    if (target.Health < Spells.Q.GetDamage(target) && Logic.InQRange(target))
                    {
                        Spells.Q.Cast(target);
                    }
                }
                if (Spells.W.IsReady())
                {

                    if (target.Health < Spells.W.GetDamage(target) && Logic.InWRange(target))
                    {
                        Spells.W.Cast();
                    }
                }
                if (Spells.R.IsReady() && Spells.R.Instance.Name == IsSecondR)
                {

                    if (target.Health < Dmg.Rdame(target, target.Health))
                    {
                        Spells.R.Cast(target.Position);
                    }
                }
                if (!Spells.Ignite.IsReady() || !MenuConfig.Ignite) continue;

                foreach (var x in GameObjects.EnemyHeroes.Where(t => t.IsValidTarget(600f)).Where(x => target.IsValidTarget(600f) && target.Health < Dmg.IgniteDmg))
                {
                    GameObjects.Player.Spellbook.CastSpell(Spells.Ignite, x);
                }
            }
        }
    }
}
