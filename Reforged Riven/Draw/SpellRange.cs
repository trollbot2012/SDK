using System;
using System.Drawing;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Utils;
using Reforged_Riven.Main;
using Reforged_Riven.Update;

namespace Reforged_Riven.Draw
{
    internal class SpellRange : Core
    {
        public static void Draw(EventArgs args)
        {
            if (Player.IsDead) return;

            var textPos = Drawing.WorldToScreen(Player.Position);

            if (MenuConfig.DrawFlee)
            {
                var end = Player.Position.Extend(Game.CursorPos, 200);
                var isWallDash = FleeLogic.IsWallDash(end, 200);

                var wallPoint = FleeLogic.GetFirstWallPoint(Player.ServerPosition, end);
               
                if (isWallDash && wallPoint.Distance(Player.ServerPosition) < 260)
                {
                    Render.Circle.DrawCircle(wallPoint, 60, Color.LightGreen);
                    Render.Circle.DrawCircle(end, 60, Color.White);
                }
               else
                {
                    Render.Circle.DrawCircle(wallPoint, 60, Color.Red);
                }
            }

            if (!MenuConfig.DrawCombo) return;

            if (Spells.Flash.IsReady() && Spells.R.IsReady() && !MenuConfig.DontFlash)
            {
                Render.Circle.DrawCircle(Player.Position, 720, Color.LightGreen);
            }

            else if (Spells.E.IsReady())
            {
                Render.Circle.DrawCircle(Player.Position, 310 + Player.AttackRange,
                    Qstack != 1
                        ? Color.LightBlue
                        : Color.DarkSlateGray);
            }
            else
            {
                Render.Circle.DrawCircle(Player.Position, Player.GetRealAutoAttackRange(Player),
                    Spells.Q.IsReady()
                        ? Color.LightBlue
                        : Color.DarkSlateGray);
            }
        }
    }
}