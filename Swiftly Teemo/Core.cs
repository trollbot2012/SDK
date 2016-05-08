using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;
using SharpDX;
using System;
using System.Linq;

namespace Swiftly_Teemo
{
    class Core
    {
        public static Orbwalker Orbwalker {get { return Variables.Orbwalker; }}
        public static Obj_AI_Hero Target => Variables.TargetSelector.GetTarget(Spells.Q.Range, DamageType.Physical);
        public static Obj_AI_Hero Player => ObjectManager.Player;
        public class Spells
        {
            public static SpellSlot Ignite;
            public static Spell Q { get; set; }
            public static Spell W { get; set; }
            public static Spell E { get; set; }
            public static Spell R { get; set; }
            public static void Load()
            {
                Q = new Spell(SpellSlot.Q, 680);
                W = new Spell(SpellSlot.W);
                E = new Spell(SpellSlot.E);
                R = new Spell(SpellSlot.R, 300);

                Q.SetTargetted(0.5f, 1500f);
                R.SetSkillshot(0.5f, 120f, 1000f, false, SkillshotType.SkillshotCircle);

                Ignite = Player.GetSpellSlot("SummonerDot");
            }
        }
    }
}
