using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;
using SharpDX;
using System;
using System.Linq;
using PrideStalker_Rengar.Main;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;

namespace PrideStalker_Rengar.Handlers
{
    class BeforeAA : Core
    {
        public static void OnAction(object sender, OrbwalkingActionArgs e)
        {
            if (Variables.Orbwalker.ActiveMode == OrbwalkingMode.Combo && e.Type == OrbwalkingType.BeforeAttack)
            {
                if(MenuConfig.ComboMode.SelectedValue == "Triple Q")
                {
                    if(MenuConfig.TripleQAAReset)
                    {
                        if(Player.Mana == 5)
                        {
                            Spells.Q.Cast();
                        }
                        if(Player.Mana < 5)
                        {
                            Spells.Q.Cast();
                        }
                    }
                }
            }
        }
    }
}
