using LeagueSharp;
using LeagueSharp.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PrideStalker_Rengar.Handlers
{
    class ITEM
    {
        public static readonly int[] BlueSmite = { 3706, 1400, 1401, 1402, 1403 };
        public static readonly int[] RedSmite = { 3715, 1415, 1414, 1413, 1412 };

        public static void CastHydra()
        {
            if(Items.CanUseItem(3074))
            {
                    Items.UseItem(3074);
            }
            if (Items.CanUseItem(3077))
            {
                    Items.UseItem(3077);
            }
        }
        public static void CastYomu()
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
