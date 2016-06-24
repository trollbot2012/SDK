using LeagueSharp;
using Preserved_Kassadin.Cores;
using System;

namespace Preserved_Kassadin.Update
{
    class Skinchanger
    {
        public static void Update(EventArgs args)
        {
            ObjectManager.Player.SetSkin(ObjectManager.Player.CharData.BaseSkinName, MenuConfig.SkinList.Index);
        }
    }
}
