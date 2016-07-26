#region

using LeagueSharp;
using Reforged_Riven.Main;
using Reforged_Riven.Update;
using Reforged_Riven.Update.Process;

#endregion

namespace Reforged_Riven
{
    internal class Load
    {
        public static void LoadAssembly()
        {
            MenuConfig.Load();
            Core.Spells.Load();
            
             Obj_AI_Base.OnDoCast += LaneClear.OnDoCastLc;
             Obj_AI_Base.OnDoCast += ModeHandler.OnDoCast;
             Obj_AI_Base.OnProcessSpellCast += Logic.OnCast;
             Obj_AI_Base.OnPlayAnimation += Animation.OnPlay;

            //  Drawing.OnEndScene += DrawDmg.DmgDraw;
            //  Drawing.OnDraw += DrawRange.RangeDraw;
            //  Drawing.OnDraw += DrawWallSpot.WallDraw;

              Game.OnUpdate += Trinket.Update;
              Game.OnUpdate += KillSteal.Update;
              Game.OnUpdate += alwaysUpdate.Update;
              Game.OnUpdate += SkinChanger.Update;

          //  Interrupter2.OnInterruptableTarget += Interrupt2.OnInterruptableTarget;
          //  AntiGapcloser.OnEnemyGapcloser += Gapclose.gapcloser;

          //   AssemblyVersion.CheckVersion();
            
            Game.PrintChat("<b><font color=\"#FFFFFF\">[</font></b><b><font color=\"#00e5e5\">Reforged Riven</font></b><b><font color=\"#FFFFFF\">]</font></b><b><font color=\"#FFFFFF\"> Version: 1</font></b>");
            Game.PrintChat("<b><font color=\"#FFFFFF\">[</font></b><b><font color=\"#00e5e5\">Update</font></b><b><font color=\"#FFFFFF\">]</font></b><b><font color=\"#FFFFFF\"> Release</font></b>");
        }
    }
}
