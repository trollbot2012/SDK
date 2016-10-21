namespace Preserved_Kassadin.Update.Draw
{
    using System;
    using System.Drawing;

    using LeagueSharp.SDK.Utils;

    using Preserved_Kassadin.Cores;

    internal class DrawSpells : Core
    {
        public static void OnDraw(EventArgs args)
        {
            if (MenuConfig.DisableDraw) return;

            if (MenuConfig.DrawQ)
                Render.Circle.DrawCircle(
                    Player.Position,
                    Spells.Q.Range,
                    Spells.Q.IsReady()
                    ? Color.DarkSlateGray
                    : Color.IndianRed);

            if (MenuConfig.DrawE)
                Render.Circle.DrawCircle(
                    Player.Position,
                    Spells.E.Range,
                    Spells.E.IsReady()
                    ? Color.DarkSlateGray
                    : Color.IndianRed);

            if (MenuConfig.DrawR)
                Render.Circle.DrawCircle(
                    Player.Position,
                    Spells.R.Range,
                    Spells.R.IsReady()
                    ? Color.DarkSlateGray
                    : Color.IndianRed);
        }
    }
}