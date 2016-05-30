#region

using LeagueSharp;
using LeagueSharp.SDK;

#endregion

namespace Spirit_Karma.Core
{
    internal class Core
    {
        public static Orbwalker Orbwalker => Variables.Orbwalker;
        public static Obj_AI_Hero Player => ObjectManager.Player;
        public static Obj_AI_Hero Target => Variables.TargetSelector.GetTarget(1050, DamageType.Magical);
    }
}
