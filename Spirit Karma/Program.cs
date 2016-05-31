#region

using System;
using LeagueSharp;
using LeagueSharp.SDK;

#endregion

namespace Spirit_Karma
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Bootstrap.Init(args);
            Events.OnLoad += Load;
        }

        private static void Load(object sender, EventArgs e)
        {
            if (GameObjects.Player.ChampionName != "Karma")
            {
                Console.WriteLine("Could not load Karma!"); return;
            }
            Spirit_Karma.Load.Load.LoadAssembly();
        }
    }
}
