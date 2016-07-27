﻿using System;
using LeagueSharp;
using LeagueSharp.SDK;
using Reforged_Riven.Main;

namespace Reforged_Riven.Update
{
    internal class Animation : Logic
    {
        public static float lastQ;

        public static void OnPlay(Obj_AI_Base sender, GameObjectPlayAnimationEventArgs args)
        {
            if (!sender.IsMe) return;

            switch (args.Animation)
            {
                case "Spell1a":
                    lastQ = Environment.TickCount;
                    Qstack = 2;
                    break;
                case "Spell1b":
                    lastQ = Environment.TickCount;
                    Qstack = 3;
                    break;
                case "Spell1c":
                    lastQ = Environment.TickCount;
                    Qstack = 1;
                    break;
                case "Spell4b":
                    var target = Variables.TargetSelector.GetSelectedTarget();
                    if (Spells.Q.IsReady() && target.IsValidTarget()) ForceCastQ(target);
                    break;
            }
        }
    }
}
