using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;
using SharpDX;
using System;
using System.Linq;
using PrideStalker_Rengar.Main;

namespace PrideStalker_Rengar.Handlers
{
    class AfterAA : Core
    {
        public static void OnAction(object sender, OrbwalkingActionArgs e)
        {
            if (Variables.Orbwalker.ActiveMode == OrbwalkingMode.LaneClear && e.Type == OrbwalkingType.AfterAttack)
            {
                if(Player.Mana == 5 && MenuConfig.Passive.Active)
                {
                    return;
                }
                if (MenuConfig.ComboMode.SelectedValue != "Ap Combo")
                {
                    if (Spells.Q.IsReady() && Player.HealthPercent >= 80 && Player.Mana == 5)
                    {
                        Spells.Q.Cast();
                    }
                    if (Player.Mana < 5)
                    {
                        Spells.Q.Cast();
                    }
                }
            }
        }
    }
}
