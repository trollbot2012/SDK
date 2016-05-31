#region

using System;
using LeagueSharp;
using LeagueSharp.SDK;
using Spirit_Karma.Menus;

#endregion

namespace Spirit_Karma.Event
{
    internal class Trinkets : Core.Core
    {
        public static void OnUpdate(EventArgs args)
        {
            if(!MenuConfig.Trinket || Player.Level < 9 || !Player.InShop() || Items.HasItem(3364) || Items.HasItem(3364)) return;

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
