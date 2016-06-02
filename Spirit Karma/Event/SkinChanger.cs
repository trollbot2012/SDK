#region

using System;
using Spirit_Karma.Menus;

#endregion

namespace Spirit_Karma.Event
{
    internal class SkinChanger : Core.Core
    {
        public static void Update(EventArgs args)
        {
            Skins();
        }
        public static void Skins()
        {
            if (!MenuConfig.UseSkin)
            {
                Player.SetSkin(Player.CharData.BaseSkinName, Player.BaseSkinId);
                return;
            }
            Player.SetSkin(Player.CharData.BaseSkinName, MenuConfig.SkinChanger.Index);
        }
    }
}
