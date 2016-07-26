#region

using LeagueSharp;
using LeagueSharp.SDK;

#endregion

namespace Reforged_Riven.Main
{
    internal class Dmg : Core
    {
        public static int IgniteDmg = 50 + 20 * GameObjects.Player.Level;

        public static double Basicdmg(Obj_AI_Base target)
        {
            if (target == null) return 0;

            double dmg = 0;
            double passivenhan;
            if (Player.Level >= 18)
                passivenhan = 0.5;
            else if (Player.Level >= 15)
                passivenhan = 0.45;
            else if (Player.Level >= 12)
                passivenhan = 0.4;
            else if (Player.Level >= 9)
                passivenhan = 0.35;
            else if (Player.Level >= 6)
                passivenhan = 0.3;
            else if (Player.Level >= 3)
                passivenhan = 0.25;
            else
                passivenhan = 0.2;

            if (Spells.W.IsReady()) dmg = dmg + Spells.W.GetDamage(target);

            if (Spells.Q.IsReady())
            {
                var qnhan = 4 - Qstack;
                dmg = dmg + Spells.Q.GetDamage(target) * qnhan + Player.GetAutoAttackDamage(target) * qnhan * (1 + passivenhan);
            }

            dmg = dmg + Player.GetAutoAttackDamage(target) * (1 + passivenhan);

            return dmg;
        }


        public static float GetComboDamage(Obj_AI_Base enemy)
        {
            if (enemy == null) return 0;

            float damage = 0;
            float passivenhan;
            if (Player.Level >= 18)
                passivenhan = 0.5f;
            else if (Player.Level >= 15)
                passivenhan = 0.45f;
            else if (Player.Level >= 12)
                passivenhan = 0.4f;
            else if (Player.Level >= 9)
                passivenhan = 0.35f;
            else if (Player.Level >= 6)
                passivenhan = 0.3f;
            else if (Player.Level >= 3)
                passivenhan = 0.25f;
            else
                passivenhan = 0.2f;

            if (Spells.W.IsReady()) damage = damage + Spells.W.GetDamage(enemy);
            if (Spells.Q.IsReady())
            {
                var qnhan = 4 - Qstack;
                damage = damage + Spells.Q.GetDamage(enemy) * qnhan +
                         (float)Player.GetAutoAttackDamage(enemy) * qnhan * (1 + passivenhan);
            }
            damage = damage + (float)Player.GetAutoAttackDamage(enemy) * (1 + passivenhan);
            if (Spells.R.IsReady())
            {
                return damage * 1.2f + Spells.R.GetDamage(enemy);
            }
            return damage;
        }

        public static bool IsKillableR(Obj_AI_Hero target)
        {
            return !target.IsInvulnerable && Totaldame(target) >= target.Health &&
                   Basicdmg(target) <= target.Health;
        }

        public static double Totaldame(Obj_AI_Base target)
        {
            if (target == null) return 0;
            double dmg = 0;
            double passivenhan;
            if (Player.Level >= 18)
                passivenhan = 0.5;
            else if (Player.Level >= 15)
                passivenhan = 0.45;
            else if (Player.Level >= 12)
                passivenhan = 0.4;
            else if (Player.Level >= 9)
                passivenhan = 0.35;
            else if (Player.Level >= 6)
                passivenhan = 0.3;
            else if (Player.Level >= 3)
                passivenhan = 0.25;
            else
                passivenhan = 0.2;

            if (Spells.W.IsReady()) dmg = dmg + Spells.W.GetDamage(target);

            if (Spells.Q.IsReady())
            {
                var qnhan = 4 - Qstack;
                dmg = dmg + Spells.Q.GetDamage(target) * qnhan + Player.GetAutoAttackDamage(target) * qnhan * (1 + passivenhan);
            }

            dmg = dmg + Player.GetAutoAttackDamage(target) * (1 + passivenhan);

            if (!Spells.R.IsReady()) return dmg;

            var rdmg = Rdame(target, target.Health - dmg * 1.2);

            return dmg * 1.2 + rdmg;
        }
        public static bool IsLethal(Obj_AI_Base unit)
        {
            return GetComboDamage(unit) / 1.65 >= unit.Health;
        }
        public static double Rdame(Obj_AI_Base target, double health)
        {
            if (target == null) return 0;

            var missinghealth = (target.MaxHealth - health) / target.MaxHealth > 0.75 ? 0.75 : (target.MaxHealth - health) / target.MaxHealth;
            var pluspercent = missinghealth * 2;
            var rawdmg = new double[] { 80, 120, 160 }[Spells.R.Level - 1] + 0.6 * Player.FlatPhysicalDamageMod;
            return Player.CalculateDamage(target, DamageType.Physical, rawdmg * (1 + pluspercent));
        }
    }
}
