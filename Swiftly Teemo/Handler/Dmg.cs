using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;
using SharpDX;
using System;
using System.Linq;
using Swiftly_Teemo.Main;

namespace Swiftly_Teemo.Handler
{
    class Dmg : Core
    {
        public static int IgniteDmg = 50 + 20 * GameObjects.Player.Level;
        public static float ComboDmg(Obj_AI_Base enemy)
        {
            if (enemy != null)
            {
                float damage = 0;
                if (MenuConfig.KillStealSummoner)
                {
                    if (Spells.Ignite.IsReady())
                    {
                        damage = damage + IgniteDmg;
                    }
                }
                if (Player.CanAttack) damage = damage + (float)Player.GetAutoAttackDamage(enemy);

                if (Spells.E.IsReady()) damage = damage + Spells.E.GetDamage(enemy);

                if (Spells.R.IsReady()) damage = damage + Spells.R.GetDamage(enemy);

                if (Spells.Q.IsReady()) damage = damage + Spells.Q.GetDamage(enemy);

                return damage;
            }
            return 0;
        }
        public static bool IsLethal(Obj_AI_Base unit)
        {
            return ComboDmg(unit) / 1.65 >= unit.Health;
        }
    }
}
