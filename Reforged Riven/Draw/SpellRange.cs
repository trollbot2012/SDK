using System;
using System.Drawing;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Utils;
using Reforged_Riven.Main;

namespace Reforged_Riven.Draw
{
    internal class SpellRange : Core
    {
        public static void Draw(EventArgs args)
        {
            if (Player.IsDead) return;

            if (!MenuConfig.DrawCombo) return;

            if (Spells.Flash.IsReady() && MenuConfig.Flash && Spells.R.IsReady())
            {
                Render.Circle.DrawCircle(Player.Position, 750, Color.LightGreen);
            }

            else if (Spells.E.IsReady())
            {
                Render.Circle.DrawCircle(Player.Position, 375 + Player.AttackRange,
                    Spells.Q.IsReady()
                        ? Color.LightBlue
                        : Color.DarkSlateGray);
            }
            else
            {
                Render.Circle.DrawCircle(Player.Position, Player.AttackRange,
                    Spells.Q.IsReady()
                        ? Color.LightBlue
                        : Color.DarkSlateGray);
            }
        }
    }
}