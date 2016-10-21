namespace Preserved_Kassadin
{
    using System;

    using LeagueSharp.SDK;

    internal class Program
    {
        private static void Main(string[] args)
        {
            Bootstrap.Init(args);
            Events.OnLoad += Load;
        }

        private static void Load(object sender, EventArgs e)
        {
            if (GameObjects.Player.ChampionName != "Kassadin") return;
            LoadAssembly.Load();
        }
    }
}