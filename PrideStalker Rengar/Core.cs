using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;

namespace PrideStalker_Rengar
{
    internal class Core
    {
        public static Obj_AI_Hero Player => ObjectManager.Player;
        public static Orbwalker Orbwalker => Variables.Orbwalker;
    }

    public class Spells
    {
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

            Ignite = ObjectManager.Player.GetSpellSlot("SummonerDot");
        }
    }
}
