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
            Player.SetSkin(Player.CharData.BaseSkinName, MenuConfig.UseSkin ? MenuConfig.SkinList.Index : Player.BaseSkinId);
        }
    }
}
