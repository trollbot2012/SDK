#region

using System;
using Spirit_Karma.Menus;

#endregion

namespace Spirit_Karma.Event
{
    internal class SkinChanger : Core.Core
    {
        public static void OnUpdate(EventArgs args)
        {
            Skins();
        }
        public static void Skins()
        {
            if(!MenuConfig.UseSkin) return;
            switch (MenuConfig.SkinChanger.Index)
            {
                case 0:
                    Player.SetSkin(Player.CharData.BaseSkinName, 0);
                    break;
                case 1:
                    Player.SetSkin(Player.CharData.BaseSkinName, 1);
                    break;
                case 2:
                    Player.SetSkin(Player.CharData.BaseSkinName, 2);
                    break;
                case 3:
                    Player.SetSkin(Player.CharData.BaseSkinName, 3);
                    break;
                case 4:
                    Player.SetSkin(Player.CharData.BaseSkinName, 4);
                    break;
                case 5:
                    Player.SetSkin(Player.CharData.BaseSkinName, 5);
                    break;
            }
        }
    }
}
