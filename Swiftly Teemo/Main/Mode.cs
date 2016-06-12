﻿#region

using System.Linq;
using LeagueSharp;
using LeagueSharp.SDK;
using SharpDX;

#endregion

namespace Swiftly_Teemo.Main
{
    internal class Mode : Core
    {   
        public static void Combo()
        {
            
            if (!Target.IsValidTarget() || Target == null || Target.IsZombie || Target.IsInvulnerable) return;

            var rPrediction = Spells.R.GetPrediction(Target).CastPosition;
            var ammo = ObjectManager.Player.Spellbook.GetSpell(SpellSlot.R).Ammo;

            if (Spells.R.IsReady())
            {
                if (ammo <= 3 && !MenuConfig.RCombo)
                {
                    if (!Target.HasBuffOfType(BuffType.Poison) && !Target.HasBuffOfType(BuffType.Slow))
                    {
                        if (Target.Distance(Player) <= Spells.R.Range)
                        {
                            Spells.R.Cast(rPrediction);
                        }
                    }
                }
                else if (MenuConfig.RCombo)
                {
                    if (ammo <= 3)
                    {
                        if (Target.Distance(Player) <= Spells.R.Range*2)
                        {
                            Spells.R.Cast(rPrediction);
                        }
                    }
                }
            }
            if (Spells.W.IsReady() && Player.ManaPercent > 22.5)
            {
                Spells.W.Cast();
            }
        }
       
        public static void Lane()
        {
            var minions = GameObjects.EnemyMinions.Where(m => m.IsMinion && m.IsEnemy && m.Team != GameObjectTeam.Neutral && m.IsValidTarget(Player.AttackRange)).ToList();
            var ammo = ObjectManager.Player.Spellbook.GetSpell(SpellSlot.R).Ammo;

            foreach (var m in minions)
            {
                if (m.Health < Spells.Q.GetDamage(m) && Player.ManaPercent > 35 && MenuConfig.LaneQ)
                {
                    Spells.Q.Cast(m);
                }
                if (m.Health < Spells.R.GetDamage(m) && Player.ManaPercent > 40 && ammo >= 3)
                {
                    Spells.R.Cast(m);
                }
            }
        }
        public static void Jungle()
        {
            var mob = GameObjects.Jungle.Where(m => m.IsValidTarget(Spells.Q.Range) && !GameObjects.JungleSmall.Contains(m)).ToList();
            var ammo = ObjectManager.Player.Spellbook.GetSpell(SpellSlot.R).Ammo;

            foreach (var m in mob)
            {
                if(Spells.R.IsReady() && m.Distance(Player) <= Spells.R.Range && m.Health > Spells.R.GetDamage(m))
                {
                    if(!m.SkinName.Contains("Sru_Crab"))
                    {
                        if(ammo >= 3)
                        {
                            Spells.R.Cast(m);
                        }
                    }
                   
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

            if (Target.Distance(Player) <= Spells.R.Range && Target.IsValidTarget() && Target != null)
            {
                if (Spells.R.IsReady())
                {
                    Spells.R.Cast(Player.Position);
                }
            }
        }
        public static void OnSpell(Spellbook sender, SpellbookCastSpellEventArgs args)
        {
            if (args.Slot == SpellSlot.Q)
            {
                Orbwalker.ShouldWait();
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
