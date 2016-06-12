#region

using System.Linq;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Utils;

#endregion

namespace Swiftly_Teemo.Main
{
    internal class Mode : Core
    {
        public static void Combo()
        {
            if (Target == null || Target.IsZombie || Target.IsInvulnerable) return;
            if (MenuConfig.TowerCheck && Target.IsUnderEnemyTurret()) return;

            var ammo = ObjectManager.Player.Spellbook.GetSpell(SpellSlot.R).Ammo;
            var rPrediction = Spells.R.GetPrediction(Target).CastPosition;
            var newPos = Player.ServerPosition.Extend(rPrediction, Spells.R.Range);

            if (Spells.R.IsReady() && Target.IsValidTarget(Spells.R.Range))
            {
                if (ammo == 3)
                {
                    if (Target.Distance(Player) <= Spells.R.Range)
                    {
                        Spells.R.Cast(rPrediction);
                    }
                }
                else
                {
                    Spells.R.Cast(newPos);
                }
            }

            if (Spells.W.IsReady() && Player.ManaPercent >= 22.5)
            {
                Spells.W.Cast();
            }
        }
        public static void OnCastSpell(Spellbook sender, SpellbookCastSpellEventArgs args)
        {
            if (args.Slot != SpellSlot.Q) return;
            if (sender.Owner != Player) return;

            Orbwalker.SetAttackState(false);
            DelayAction.Add(350, () => Orbwalker.SetAttackState(true));
        }
        public static void Lane()
        {
            var minions = GameObjects.EnemyMinions.Where(m => m.IsMinion && m.IsEnemy && m.Team != GameObjectTeam.Neutral && m.IsValidTarget(800)).ToList();
            var ammo = ObjectManager.Player.Spellbook.GetSpell(SpellSlot.R).Ammo;
            var rPred = Spells.R.GetCircularFarmLocation(minions);

            foreach (var m in minions)
            {
                if (m.Health < Spells.Q.GetDamage(m) && Player.ManaPercent >= 20 && MenuConfig.LaneQ)
                {
                    Spells.Q.Cast(m);
                }

                if (!(m.Health < Spells.R.GetDamage(m)) || !(Player.ManaPercent >= 40) || ammo < 3) continue;

                if (rPred.MinionsHit >= 3)
                {
                    Spells.R.Cast(rPred.Position);
                }
            }
        }
        public static void Jungle()
        {
            var mob = GameObjects.Jungle.Where(m => m.IsValidTarget(Spells.Q.Range) && !GameObjects.JungleSmall.Contains(m)).ToList();
            var ammo = ObjectManager.Player.Spellbook.GetSpell(SpellSlot.R).Ammo;

            foreach (var m in mob)
            {
                if (!Spells.R.IsReady() || !(m.Distance(Player) <= Spells.R.Range) || !(m.Health > Spells.R.GetDamage(m))) continue;
                if (m.SkinName.Contains("Sru_Crab")) continue;

                if(ammo >= 3)
                {
                    Spells.R.Cast(m);
                }
            }
        }

        public static void Flee()
        {
            if (!MenuConfig.Flee.Active)
            {
                return;
            }

            ObjectManager.Player.IssueOrder(GameObjectOrder.MoveTo, Game.CursorPos);

            if (Spells.W.IsReady())
            {
                Spells.W.Cast();
            }

            if (!(Target.Distance(Player) <= Spells.R.Range) || !Target.IsValidTarget() || Target == null) return;

            if (Spells.R.IsReady())
            {
                Spells.R.Cast(Player.Position);
            }
        }

        public static void Skin()
        {
            if (!MenuConfig.UseSkin)
            {
                Player.SetSkin(Player.CharData.BaseSkinName, Player.BaseSkinId);
                return;
            }
            Player.SetSkin(Player.CharData.BaseSkinName, MenuConfig.SkinChanger.Index);
        }
    }
}
