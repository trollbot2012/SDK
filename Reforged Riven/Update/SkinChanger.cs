#region

using System;
using Reforged_Riven.Main;

#endregion

namespace Reforged_Riven.Update
{
    internal class SkinChanger : Core
    {
        public static void Update(EventArgs args)
        {
            if (!MenuConfig.UseSkin)
            {
                Player.SetSkin(Player.CharData.BaseSkinName, Player.BaseSkinId);
            }
            Player.SetSkin(Player.CharData.BaseSkinName, MenuConfig.SkinChanger.Index);
        }
    }
}
