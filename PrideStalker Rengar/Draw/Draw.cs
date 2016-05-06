using LeagueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrideStalker_Rengar.Main;
using LeagueSharp.SDK.Utils;

namespace PrideStalker_Rengar.Draw
{
    class DRAW : Core
    {
        public static HpBarDraw DrawHpBar = new HpBarDraw();
        public static void OnDraw(EventArgs args)
        {
            if (Player.IsDead)
            {
                return;
            }
            var heropos = Drawing.WorldToScreen(ObjectManager.Player.Position);

            if(MenuConfig.Passive.Active)
            {
                Drawing.DrawText(heropos.X - 15, heropos.Y + 20, System.Drawing.Color.DodgerBlue, "Passive  (     )");
                Drawing.DrawText(heropos.X + 53, heropos.Y + 20, MenuConfig.Passive.Active ? System.Drawing.Color.LimeGreen : System.Drawing.Color.Red, MenuConfig.Passive.Active ? "On" : "Off");
            }
            if (MenuConfig.EngageDraw)
            {
                Render.Circle.DrawCircle(Player.Position, Spells.E.Range,
                   Spells.E.IsReady() ? System.Drawing.Color.FromArgb(120, 0, 170, 255) : System.Drawing.Color.IndianRed);
            }
        }
    }
}
