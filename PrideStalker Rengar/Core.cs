using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;
using SharpDX;
using System;
using System.Linq;

namespace PrideStalker_Rengar
{
    class Core
    {
       public static Obj_AI_Hero Player => ObjectManager.Player;
       
        public class Spells
        {
            public static SpellSlot GetSmiteSlot()
            {
                foreach (var spell in ObjectManager.Player.Spellbook.Spells.Where(s => s.Name.ToLower().Contains("smite")))
                {
                    return spell.Slot;
                }
                return SpellSlot.Unknown;
            }
            public static SpellSlot Ignite;
            public static Spell Q { get; set; }
            public static Spell W { get; set; }
            public static Spell E { get; set; }
            public static Spell R { get; set; }
            public static void Load()
            {
                Q = new Spell(SpellSlot.Q);
                W = new Spell(SpellSlot.W, 300);
                E = new Spell(SpellSlot.E, 1000f);
                E.SetSkillshot(0.25f, 70, 1500f, true, SkillshotType.SkillshotLine);
                R = new Spell(SpellSlot.R);

                Ignite = Player.GetSpellSlot("SummonerDot");
            }
        }
    }
}
