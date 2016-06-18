#region

using LeagueSharp;
using LeagueSharp.SDK;

#endregion

namespace Infected_Twitch.Core
{
    internal class Core
    {
        public static bool HasPassive => Player.HasBuff("TwitchHideInShadows");
        public static Orbwalker Orbwalker => Variables.Orbwalker;
        public static Obj_AI_Hero Player => ObjectManager.Player;
        public static Obj_AI_Hero Target => Variables.TargetSelector.GetTarget(1200, DamageType.Physical);

        public static bool SafeTarget(Obj_AI_Base target)
        {
            return target != null && target.IsValidTarget() && !target.IsDead && !target.IsInvulnerable && !target.HasBuff("KindredRNoDeathBuff") && !target.HasBuffOfType(BuffType.SpellShield);
        }
    }
}
