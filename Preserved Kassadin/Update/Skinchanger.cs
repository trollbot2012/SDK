namespace Preserved_Kassadin.Update
{
    using System;

    using LeagueSharp;

    using Preserved_Kassadin.Cores;

    internal class Skinchanger
    {
        public static void Update(EventArgs args)
        {
            ObjectManager.Player.SetSkin(ObjectManager.Player.CharData.BaseSkinName, MenuConfig.SkinList.Index);
        }
    }
}