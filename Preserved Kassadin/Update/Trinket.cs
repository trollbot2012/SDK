namespace Preserved_Kassadin.Update
{
    using System;

    using LeagueSharp;
    using LeagueSharp.SDK;

    using Preserved_Kassadin.Cores;

    internal class Trinket
    {
        public static void Update(EventArgs args)
        {
            if (GameObjects.Player.Level < 9 || !GameObjects.Player.InShop() || !MenuConfig.BuyTrinket) return;
            if (Items.HasItem(3363) || Items.HasItem(3364)) return;

            switch (MenuConfig.TrinketList.Index)
            {
                case 0:
                    GameObjects.Player.BuyItem(ItemId.Oracles_Lens_Trinket);
                    break;
                case 1:
                    GameObjects.Player.BuyItem(ItemId.Farsight_Orb_Trinket);
                    break;
            }
        }
    }
}