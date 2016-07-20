using PrideStalker_Rengar.Main;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;

namespace PrideStalker_Rengar.Handlers
{
    class BeforeAA : Core
    {
        public static void OnAction(object sender, OrbwalkingActionArgs e)
        {
            if (Variables.Orbwalker.ActiveMode != OrbwalkingMode.Combo || e.Type != OrbwalkingType.BeforeAttack) return;

            if (MenuConfig.ComboMode.SelectedValue != "Triple Q") return;

            if (!MenuConfig.TripleQAAReset) return;

            Spells.Q.Cast();
        }
    }
}
