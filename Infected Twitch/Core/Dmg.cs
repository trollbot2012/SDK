﻿#region

using System;
using LeagueSharp;
using LeagueSharp.SDK;

#endregion

namespace Infected_Twitch.Core
{
    internal class Dmg
    {
        public static int IgniteDmg = 50 + 20 * GameObjects.Player.Level;
       
        // Mostly supporting Exploit
        public static float EDamage(Obj_AI_Base target)
        {
            if (target == null || !target.IsValidTarget()) return 0;
            if (target.IsInvulnerable || target.HasBuff("KindredRNoDeathBuff") || target.HasBuffOfType(BuffType.SpellShield)) return 0;

            float eDmg = 0;

            if (Spells.E.IsReady()) eDmg = eDmg + Spells.E.GetDamage(target) + (float)GameObjects.Player.CalculateDamage(target, DamageType.True, Passive(target) * Stacks(target) * GameObjects.Player.FlatMagicDamageMod + GameObjects.Player.FlatPhysicalDamageMod);

            if (GameObjects.Player.HasBuff("SummonerExhaust")) eDmg = eDmg *= (float)0.6;

            return eDmg;
        }

        public static double Passive(Obj_AI_Base target)
        {
            float dmg = 6;

            if (GameObjects.Player.Level > 16) dmg = 6;
            if (GameObjects.Player.Level > 12) dmg = 5;
            if (GameObjects.Player.Level > 8) dmg = 4;
            if (GameObjects.Player.Level > 4) dmg = 3;
            if (GameObjects.Player.Level > 0) dmg = 2;

            return dmg * Stacks(target) * PassiveTime(target) - target.HPRegenRate * PassiveTime(target);
        }

        public static float Stacks(Obj_AI_Base target)
        {
            return target.GetBuffCount("TwitchDeadlyVenom");
        }

        public static float PassiveTime(Obj_AI_Base target)
        {
            if (!target.HasBuff("twitchdeadlyvenom")) return 0;

            return Math.Max(0, target.GetBuff("twitchdeadlyvenom").EndTime) - Game.Time;   
        }

        public static bool Executable(Obj_AI_Hero target)
        {
            return !target.IsInvulnerable && target.Health < EDamage(target);
        }
    }
}