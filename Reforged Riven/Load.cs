#region

using LeagueSharp;
using Reforged_Riven.Draw;
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
            Spells.Load();

            Obj_AI_Base.OnDoCast += LaneClear.OnDoCastLc;
            Obj_AI_Base.OnDoCast += ModeHandler.OnDoCast;
            Obj_AI_Base.OnProcessSpellCast += Logic.OnCast;
            Obj_AI_Base.OnPlayAnimation += Animation.OnPlay;

            Drawing.OnEndScene += DrawDmg.DmgDraw;
            Drawing.OnDraw += SpellRange.Draw;

            Game.OnUpdate += KillSteal.Update;
            Game.OnUpdate += PermaActive.Update;

            //  Interrupter2.OnInterruptableTarget += Interrupt2.OnInterruptableTarget;
            //  AntiGapcloser.OnEnemyGapcloser += Gapclose.gapcloser;

           AssemblyVersion.CheckVersion();

            Game.PrintChat("<b><font color=\"#FFFFFF\">[</font></b><b><font color=\"#00e5e5\">Reforged Riven</font></b><b><font color=\"#FFFFFF\">]</font></b><b><font color=\"#FFFFFF\"> - Loaded</font></b>");
        }
    }
}
