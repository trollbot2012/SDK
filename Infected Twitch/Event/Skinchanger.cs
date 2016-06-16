#region

using System;
using Infected_Twitch.Menus;

#endregion

namespace Infected_Twitch.Event
{
    internal class Skinchanger : Core.Core
    {
        public static void Update(EventArgs args)
        {
            if (!MenuConfig.UseSkin)
            {
                Player.SetSkin(Player.CharData.BaseSkinName, Player.BaseSkinId);
                return;
            }
            Player.SetSkin(Player.CharData.BaseSkinName, MenuConfig.SkinList.Index);
        }
    }
}
