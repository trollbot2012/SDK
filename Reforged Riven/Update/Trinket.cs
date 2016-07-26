#region

using System;
using LeagueSharp;
using LeagueSharp.SDK;
using Reforged_Riven.Main;

#endregion

namespace Reforged_Riven.Update
{
    internal class Trinket : Core
    {
        public static void Update(EventArgs args)
        {
            if (!MenuConfig.BuyTrinket || Player.Level < 9 || !Player.InShop() || Items.HasItem(3363) || Items.HasItem(3364)) return;

            switch (MenuConfig.TrinketList.Index)
            {
                case 0:
                    Player.BuyItem(ItemId.Oracles_Lens_Trinket);
                    break;
                case 1:
                    Player.BuyItem(ItemId.Farsight_Orb_Trinket);
                    break;
            }
        }
    }
}
