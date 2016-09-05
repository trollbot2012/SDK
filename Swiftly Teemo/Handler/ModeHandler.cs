using System.Linq;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using Swiftly_Teemo.Menu;

namespace Swiftly_Teemo.Handler
{
    class ModeHandler : Core
    {
        public static void OnDoCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!sender.IsMe)
            {
                return;
            }

            if (Orbwalker.ActiveMode == OrbwalkingMode.Combo || Orbwalker.ActiveMode == OrbwalkingMode.Hybrid)
            {
                var a = GameObjects.EnemyHeroes.Where(x => x.IsValidTarget(Spells.Q.Range));
                var targets = a as Obj_AI_Hero[] ?? a.ToArray();

                foreach (var target in targets)
                {
                    Spells.Q.CastOnUnit(target);   
                }
            }

            if (Orbwalker.ActiveMode != OrbwalkingMode.LaneClear || !(args.Target is Obj_AI_Minion))
            {
                return;
            }

            var mobs = GameObjects.JungleLarge.Where(x => x.IsValidTarget(Spells.Q.Range));

            foreach (var m in mobs)
            {
                Spells.Q.CastOnUnit(m);
            }

            if (!MenuConfig.LaneQ) return;

            var minions = GameObjects.EnemyMinions.Where(m => m.IsValidTarget(Spells.Q.Range));

            foreach (var m in minions)
            {
                if (m.Health > Player.GetAutoAttackDamage(m) || Player.IsWindingUp)
                {
                    return;
                }

                Spells.Q.CastOnUnit(m);
            }
        }
    }
}
