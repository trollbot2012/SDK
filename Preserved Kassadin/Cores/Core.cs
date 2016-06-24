using LeagueSharp;
using LeagueSharp.SDK;

namespace Preserved_Kassadin.Cores
{
    class Core
    {
        public static Orbwalker Orbwalker => Variables.Orbwalker;
        public static Obj_AI_Hero Player => ObjectManager.Player;
        public static Obj_AI_Hero Target => Variables.TargetSelector.GetTarget(1400, DamageType.Magical);

        public static bool SafeTarget(Obj_AI_Base target)
        {
            return target != null && !target.IsDead && !target.IsInvulnerable;
        }
    }
}
