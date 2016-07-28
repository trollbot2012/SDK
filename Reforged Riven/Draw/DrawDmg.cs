﻿using System;
using System.Linq;
using LeagueSharp;
using LeagueSharp.SDK;
using Reforged_Riven.Main;
using SharpDX;

namespace Reforged_Riven.Draw
{
    internal class DrawDmg
    {
        private static readonly HpBarIndicator Indicator = new HpBarIndicator();

        public static void DmgDraw(EventArgs args)
        {
            foreach (var enemy in ObjectManager.Get<Obj_AI_Hero>().Where(ene => ene.IsValidTarget(1000) && !ene.IsDead && ene.IsVisible))
            {
                if (!MenuConfig.Dind) return;

                Indicator.Unit = enemy;
                Indicator.DrawDmg(Dmg.GetComboDamage(enemy), Color.GreenYellow);
            }
        }
    }
}