using System;
using LeagueSharp.SDK.Utils;
using Reforged_Riven.Main;

namespace Reforged_Riven.Draw
{
    class SpellRange : Core
    {
        public static void Draw(EventArgs args)
        {
            if(Player.IsDead) return;

            if (MenuConfig.DrawCombo)
            {
                if (Spells.E.IsReady())
                {
                    Render.Circle.DrawCircle(Player.Position, 450 + Player.AttackRange,
                    Spells.Q.IsReady()
                    ? System.Drawing.Color.FromArgb(120, 0, 170, 255)
                    : System.Drawing.Color.IndianRed);
                }
                else
                {
                    Render.Circle.DrawCircle(Player.Position, 450,
                     Spells.Q.IsReady()
                     ? System.Drawing.Color.FromArgb(120, 0, 170, 255)
                     : System.Drawing.Color.IndianRed);
                }
               
            }


        }
    }
}
