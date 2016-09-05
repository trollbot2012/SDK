#region

using System;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Utils;
using Swiftly_Teemo.Menu;

#endregion

namespace Swiftly_Teemo.Draw
{
    internal class Drawings
    {
        public static void OnDraw(EventArgs args)
        {
            if (GameObjects.Player.IsDead)
            {
                return;
            }

            if (MenuConfig.EngageDraw)
            {
                Render.Circle.DrawCircle(GameObjects.Player.Position, Spells.Q.Range,
                    Spells.Q.IsReady()
                        ? System.Drawing.Color.DarkSlateGray
                        : System.Drawing.Color.LightGray);
            }
        }
    }
}
