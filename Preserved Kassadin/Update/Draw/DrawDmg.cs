namespace Preserved_Kassadin.Update.Draw
{
    using System;
    using System.Linq;

    using LeagueSharp;
    using LeagueSharp.SDK;

    using Preserved_Kassadin.Cores;

    using SharpDX;

    internal class DrawDmg
    {
        private static readonly HpBarIndicator Indicator = new HpBarIndicator();

        public static void Draw(EventArgs args)
        {
            foreach (var enemy in ObjectManager.Get<Obj_AI_Hero>().Where(ene => ene.IsValidTarget(1500)))
            {
                if (!MenuConfig.DrawDmg) return;

                Indicator.Unit = enemy;
                Indicator.DrawDmg(Dmg.Damage(enemy), Color.LawnGreen);
            }
        }
    }
}