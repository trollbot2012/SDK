using LeagueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrideStalker_Rengar.Main;
using LeagueSharp.SDK.Utils;
using Nechrito_Rengar;

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
                Drawing.DrawText(heropos.X - 15, heropos.Y + 20, System.Drawing.Color.Cyan, "Stacking  (     )");
                Drawing.DrawText(heropos.X + 62, heropos.Y + 20, MenuConfig.Passive.Active ? System.Drawing.Color.White : System.Drawing.Color.Red, MenuConfig.Passive.Active ? "On" : "Off");
            }
            if (MenuConfig.DrawCombo)
            {
                if(MenuConfig.ComboMode.SelectedValue == "Ap Combo")
                {
                    Drawing.DrawText(heropos.X - 15, heropos.Y + 40, System.Drawing.Color.White, "Ap Combo");
                }
                if (MenuConfig.ComboMode.SelectedValue == "Triple Q")
                {
                    Drawing.DrawText(heropos.X - 15, heropos.Y + 40, System.Drawing.Color.White, "Triple Q");
                }
                if (MenuConfig.ComboMode.SelectedValue == "Gank")
                {
                    Drawing.DrawText(heropos.X - 15, heropos.Y + 40, System.Drawing.Color.White, "Gank");
                }
            }
            if (MenuConfig.EngageDraw)
            {
                Render.Circle.DrawCircle(Player.Position, Spells.E.Range,
                   Spells.E.IsReady() ? System.Drawing.Color.FromArgb(120, 0, 170, 255) : System.Drawing.Color.IndianRed);
            }
        }
    }
}
