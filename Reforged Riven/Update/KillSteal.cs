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
            var target = Variables.TargetSelector.GetTarget(Spells.R.Range, DamageType.Physical);

            if (target == null) return;

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
                    var rPred = Spells.R.GetPrediction(target).CastPosition;

                    Spells.R.Cast(rPred);
                }
            }

            if (!Spells.Ignite.IsReady() || !MenuConfig.Ignite) return;

            foreach (var x in GameObjects.EnemyHeroes.Where(t => t.IsValidTarget(600f) && t.Health < Dmg.IgniteDmg))
            {
                GameObjects.Player.Spellbook.CastSpell(Spells.Ignite, x);
            }
        }
    }
}
