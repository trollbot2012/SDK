namespace Preserved_Kassadin.Cores
{
    using LeagueSharp;
    using LeagueSharp.SDK;

    internal class Dmg : Core
    {
        public static int IgniteDmg = 50 + 20 * Player.Level;

        public static float Damage(Obj_AI_Base target)
        {
            if (!SafeTarget(target)) return 0;

            float Dmg = 0;

            if (Player.Spellbook.CanUseSpell(Spells.Ignite) == SpellState.Ready) Dmg += IgniteDmg;

            if (!Player.IsWindingUp) Dmg += (float)Player.GetAutoAttackDamage(target);

            if (Spells.Q.IsReady()) Dmg += Spells.Q.GetDamage(target);

            if (Spells.W.IsReady()) Dmg += Spells.W.GetDamage(target);

            if (Spells.E.IsReady()) Dmg += Spells.E.GetDamage(target);

            if (Spells.R.IsReady()) Dmg += Spells.R.GetDamage(target);

            return Dmg;
        }
    }
}