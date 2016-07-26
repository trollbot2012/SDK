#region

using System;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using Reforged_Riven.Main;

#endregion

namespace Reforged_Riven.Update
{
    internal class alwaysUpdate : Core
    {
       
        public static void Update(EventArgs args)
        {
            if (Player.IsDead || Player.IsRecalling())
            {
                return;
            }

            Logic.ForceSkill();

            if (Environment.TickCount - Animation.lastQ >= 3650 && Qstack != 1 &&
                !Player.InFountain() && MenuConfig.KeepQ && Player.HasBuff("RivenTriCleave"))
            {
                Spells.Q.Cast(Game.CursorPos);
            }

            switch (Variables.Orbwalker.ActiveMode)
            {
                case OrbwalkingMode.Combo:
                    Mode.Combo();
                    break;
                case OrbwalkingMode.LaneClear:
                    break;
                case OrbwalkingMode.None:
                    Mode.QMove();
                    break;
                case OrbwalkingMode.Hybrid:
                    break;
                case OrbwalkingMode.LastHit:
                    break;
            }
        }
    }
}
