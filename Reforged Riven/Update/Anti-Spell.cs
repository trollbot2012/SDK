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

            if (args.SData.TargettingType == SpellDataTargetType.SelfAoe && Spells.E.IsReady())
            {
                if (Orbwalker.ActiveMode == OrbwalkingMode.LastHit || Orbwalker.ActiveMode == OrbwalkingMode.LaneClear)
                {
                   Spells.E.Cast(epos);
                }
            }

            if (Logic.eAntiSpell.Contains(args.SData.Name) && Spells.E.IsReady())
            {
                Spells.E.Cast(epos);
            }

            if (!Logic.wAntiSpell.Contains(args.SData.Name) && Spells.W.IsReady()) return;

               Spells.W.Cast();
        }
    }
}