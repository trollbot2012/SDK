﻿
#region

using LeagueSharp.SDK;

#endregion

namespace Reforged_Riven.Extras
{
    internal class Usables
    {
        public static void CastHydra()
        {
            if (Items.CanUseItem(3074))
            {
                Items.UseItem(3074);
            }
            if (Items.CanUseItem(3077))
            {
                Items.UseItem(3077);
            }
            Variables.Orbwalker.ResetSwingTimer();
        }
        public static void CastYoumoo()
        {
            if (Items.CanUseItem(3142))
            {
                if (GameObjects.Player.IsWindingUp ||
                    GameObjects.Player.IsCastingInterruptableSpell())
                {
                    Items.UseItem(3142);
                }
            }
        }
    }
}
