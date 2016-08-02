#region

using System;
using LeagueSharp;
using LeagueSharp.SDK;

#endregion

namespace Reforged_Riven.Main
{
    internal class Dmg : Core
    {
        public static int IgniteDmg = 50 + 20 * GameObjects.Player.Level;

        public static float TargetDamage(Obj_AI_Hero target)
        {
            if (target == null) return 0;

            float dmg = 0;
          
            dmg += (float)target.GetAutoAttackDamage(Player);

            var spells = target.Spellbook.Spells;

            foreach (var spell in spells) // <-- Credits to Soresu for this
            {
                var cd = spell.CooldownExpires - Game.Time;

                if (spell.Level <= 0 || !(cd < 0.5) || !(target.GetSpellDamage(Player, spell.Slot) > 0)) continue; 

                if (spell.Slot == SpellSlot.Q)
                {
                    dmg += (float) target.GetSpellDamage(Player, spell.Slot);
                }
                if (spell.Slot == SpellSlot.W)
                {
                    dmg += (float)target.GetSpellDamage(Player, spell.Slot);
                }
                if (spell.Slot == SpellSlot.E)
                {
                    dmg += (float)target.GetSpellDamage(Player, spell.Slot);
                }
                if (spell.Slot == SpellSlot.R)
                {
                    dmg += (float)target.GetSpellDamage(Player, spell.Slot);
                }
            }

            return dmg;
        }

        public static float GetComboDamage(Obj_AI_Base enemy)
        {
            if (enemy == null) return 0;

            float damage = 0;

            if (Spells.W.IsReady())
            {
                damage += Spells.W.GetDamage(enemy);
            }

            if (Spells.Q.IsReady())
            {
                var qcount = 4 - Qstack;
                damage += Spells.Q.GetDamage(enemy) * qcount + (float)Player.GetAutoAttackDamage(enemy) * (qcount + 1);
            }
          
            if (Spells.R.IsReady())
            {
               damage += Spells.R.GetDamage(enemy);
            }

         // damage += (float) Player.GetAutoAttackDamage(enemy);

            return damage;
        }

        public static float RDmg(Obj_AI_Hero target)
        {
            float dmg = 0;

            if (target == null || !Spells.R.IsReady()) return 0;

            dmg += Spells.R.GetDamage(target);

            return dmg;
        }
    }
}
