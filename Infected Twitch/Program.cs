#region

using System;
using LeagueSharp.SDK;

#endregion

namespace Infected_Twitch
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
          //  if (GameObjects.Player.Name != "twitch") return;

            LoadAssembly.Load();
        }
    }
}
