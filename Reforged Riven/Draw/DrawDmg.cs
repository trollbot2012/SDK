using System;
using System.Linq;
using LeagueSharp;
using LeagueSharp.SDK;
using Reforged_Riven.Main;
using SharpDX;

namespace Reforged_Riven.Draw
{
    internal class DrawDmg : Core
    {
        private static readonly HpBarIndicator Indicator = new HpBarIndicator();

        public static void DmgDraw(EventArgs args)
        {
            if (!MenuConfig.Dind) return;

            foreach (var enemy in ObjectManager.Get<Obj_AI_Hero>().Where(ene => ene.IsValidTarget(1500) && !ene.IsDead && ene.IsVisible))
            {
                Indicator.Unit = enemy;
                Indicator.DrawDmg(Dmg.GetComboDamage(enemy), Color.LawnGreen);

                var target = Variables.TargetSelector.GetSelectedTarget();

                if (target == null) continue;
                Indicator.Unit = Player;
                Indicator.DrawDmg(Dmg.TargetDamage(Player), Color.IndianRed);
            }
        }
    }
}
