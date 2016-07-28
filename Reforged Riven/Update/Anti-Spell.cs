using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using Reforged_Riven.Main;

namespace Reforged_Riven.Update
{
    internal class AntiSpell : Core
    {
        public static void OnCasting(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!sender.IsEnemy || sender.Type != Player.Type) return;

            var epos = Player.ServerPosition + (Player.ServerPosition - sender.ServerPosition).Normalized()*300;

            if (!(Player.Distance(sender.ServerPosition) <= args.SData.CastRange)) return;

            if (args.SData.TargettingType == SpellDataTargetType.SelfAoe)
            {
                if (Orbwalker.ActiveMode == OrbwalkingMode.LastHit || Orbwalker.ActiveMode == OrbwalkingMode.LaneClear)
                {
                    if (Spells.E.IsReady()) Spells.E.Cast(epos);
                }
            }

            if (args.SData.Name.Contains("IreliaEquilibriumStrike"))
            {
                if (Spells.E.IsReady()) Spells.E.Cast(epos);
                if (Spells.W.IsReady() && Logic.InWRange(sender)) Spells.W.Cast();
            }
            if (args.SData.Name.Contains("TalonCutthroat"))
            {
                if (Spells.W.IsReady()) Spells.W.Cast();
            }
            if (args.SData.Name.Contains("RenektonPreExecute"))
            {
                if (Spells.W.IsReady()) Spells.W.Cast();
            }
            if (args.SData.Name.Contains("GarenRPreCast"))
            {
                if (Spells.E.IsReady()) Spells.E.Cast(epos);
            }

            if (args.SData.Name.Contains("GarenQAttack"))
            {
                if (Spells.E.IsReady()) Spells.E.Cast(Game.CursorPos);
            }

            if (args.SData.Name.Contains("XenZhaoThrust3"))
            {
                if (Spells.W.IsReady()) Spells.W.Cast();
            }
            if (args.SData.Name.Contains("RengarQ"))
            {
                if (Spells.E.IsReady()) Spells.E.Cast();
            }
            if (args.SData.Name.Contains("RengarPassiveBuffDash"))
            {
                if (Spells.E.IsReady()) Spells.E.Cast(Game.CursorPos);
            }
            if (args.SData.Name.Contains("RengarPassiveBuffDashAADummy"))
            {
                if (Spells.E.IsReady()) Spells.E.Cast();
            }
            if (args.SData.Name.Contains("TwitchEParticle"))
            {
                if (Spells.E.IsReady()) Spells.E.Cast();
            }
            if (args.SData.Name.Contains("FizzPiercingStrike"))
            {
                if (Spells.E.IsReady()) Spells.E.Cast(Game.CursorPos);
            }
            if (args.SData.Name.Contains("HungeringStrike"))
            {
                if (Spells.E.IsReady()) Spells.E.Cast();
            }
            if (args.SData.Name.Contains("YasuoDash"))
            {
                if (Spells.E.IsReady()) Spells.E.Cast();
            }
            if (args.SData.Name.Contains("KatarinaRTrigger"))
            {
                if (Spells.W.IsReady() && Logic.InWRange(sender)) Spells.W.Cast();

                else if (Spells.E.IsReady()) Spells.E.Cast(Game.CursorPos);
            }
            if (args.SData.Name.Contains("KatarinaE"))
            {
                if (Spells.W.IsReady()) Spells.W.Cast();
            }
            if (args.SData.Name.Contains("MonkeyKingQAttack"))
            {
                if (Spells.E.IsReady()) Spells.E.Cast();
            }
            if (!args.SData.Name.Contains("MonkeyKingSpinToWin")) return;

            if (args.Target.NetworkId != Player.NetworkId) return;

            if (Spells.E.IsReady()) Spells.E.Cast(Game.CursorPos);

            else if (Spells.W.IsReady()) Spells.W.Cast();
        }
    }
}