﻿#region

using Infected_Twitch.Core;
using Infected_Twitch.Event;
using Infected_Twitch.Menus;
using LeagueSharp;

#endregion

namespace Infected_Twitch
{
    internal class LoadAssembly
    {
        public static void Load()
        {

            Spells.Load();
            MenuConfig.Load();

            Recall.Load();
            Game.OnUpdate += Skinchanger.Update;
            Game.OnUpdate += Exploit.Update;
            Game.OnUpdate += Modes.Update;
            Game.OnUpdate += EOnDeath.Update;
            Game.OnUpdate += Trinkets.Update;

            Drawing.OnEndScene += DrawDmg.Draw;

           // AssemblyVersion.CheckVersion();

            Game.PrintChat("<b><font color=\"#FFFFFF\">[</font></b><b><font color=\"#00e5e5\">Nechrito Riven</font></b><b><font color=\"#FFFFFF\">]</font></b><b><font color=\"#FFFFFF\"> Version: Release</font></b>");
            Game.PrintChat("<b><font color=\"#FFFFFF\">[</font></b><b><font color=\"#00e5e5\">Update</font></b><b><font color=\"#FFFFFF\">]</font></b><b><font color=\"#FFFFFF\"> Release</font></b>");
        }
    }
}
