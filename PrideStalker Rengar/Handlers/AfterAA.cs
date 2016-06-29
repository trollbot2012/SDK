using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;
using SharpDX;
using PrideStalker_Rengar.Main;
using System.Linq;

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
                    if (Spells.Q.IsReady() && Player.HealthPercent >= 35 && Player.Mana == 5)
                    {
                        Spells.Q.Cast();
                    }
                    var mob = ObjectManager.Get<Obj_AI_Minion>().Where(m => !m.IsDead && !m.IsZombie && m.Team == GameObjectTeam.Neutral && m.IsValidTarget(Spells.W.Range)).ToList();
                    foreach(var m in mob)
                    {
                        if (Player.Mana < 5 && m.Health > Player.GetAutoAttackDamage(m))
                        {
                            Spells.Q.Cast();
                        }
                    }
                }
                if(MenuConfig.ComboMode.SelectedValue == "Ap Combo")
                {
                    if(Player.Mana < 5)
                    {
                        Spells.Q.Cast();
                    }
                }
            }
        }
    }
}
