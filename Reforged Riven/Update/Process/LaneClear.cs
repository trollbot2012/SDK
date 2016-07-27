#region

using System;
using System.Linq;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using Reforged_Riven.Extras;
using Reforged_Riven.Main;

#endregion

namespace Reforged_Riven.Update.Process
{
    internal class LaneClear : Core
    {
        public static void OnDoCastLc(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args) // If we don't do this Q AA wont work and it will spam Q 
        {
            if (!MenuConfig.LaneQ ||!sender.IsMe) return;

            Logic.Qtarget = (Obj_AI_Base)args.Target;

            if (!(args.Target is Obj_AI_Minion)) return;

            if (Orbwalker.ActiveMode != OrbwalkingMode.LaneClear || !Spells.Q.IsReady()) return;

            var minions = GameObjects.EnemyMinions.Where(m => m.IsMinion &&
            m.IsEnemy && m.Team != GameObjectTeam.Neutral && m.IsValidTarget(Player.AttackRange + Player.BoundingRadius));

          
            foreach (var m in minions)
            {
                if(m.Health > Player.GetAutoAttackDamage(m)) return;
                //Spells.Q.CastOnBestTarget(Spells.Q.Range, true, 1);
                Logic.ForceCastQ(m);
                Usables.CastHydra();
            }  
        }
    }
}
