#region

using System;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.Utils;
using Reforged_Riven.Main;

#endregion

namespace Reforged_Riven.Update
{
    internal class PermaActive : Core
    {
       
        public static void Update(EventArgs args)
        {
            if (Player.IsDead || Player.IsRecalling())
            {
                return;
            }

            if (Environment.TickCount - Animation.LastQ >= 3650 && Qstack != 1 &&
                !Player.InFountain() && MenuConfig.KeepQ && Player.HasBuff("RivenTriCleave"))
            {
                Spells.Q.Cast(Game.CursorPos);
            }

            //if (MenuConfig.BurstKey.Active)
            //{
            //    Mode.Burst();
            //}

            Logic.ForceSkill();

            switch (Variables.Orbwalker.ActiveMode)
            {
                case OrbwalkingMode.Combo:
                {
                    Mode.Combo();
                    Mode.Burst();
                }
                    break;
                case OrbwalkingMode.None:
                {
                    Mode.Flee();
                    Mode.QMove();
                }
                    break;

                case OrbwalkingMode.Hybrid:
                    Mode.Harass();
                    break;
            }
        }
    }
}
