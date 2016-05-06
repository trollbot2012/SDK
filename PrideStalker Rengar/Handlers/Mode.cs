using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;
using SharpDX;
using System;
using System.Linq;
using PrideStalker_Rengar.Main;

namespace PrideStalker_Rengar.Handlers
{
    class Mode : Core
    {
        #region Combo
        public static void Combo()
        {
            var hasPassive = Player.HasBuff("RengarRBuff") || Player.HasBuff("RengarPassiveBuff");

            var Target = Variables.TargetSelector.GetTarget(Spells.E.Range, DamageType.Physical);

            if(Target != null && Target.IsValidTarget() && !Target.IsZombie)
            {
                if(Player.Mana == 5)
                {
                    if(MenuConfig.UseItem && Spells.Q.IsReady() && Spells.W.IsReady())
                    {
                        ITEM.CastYomu();
                    }
                    if(Spells.E.IsReady() && Target.Distance(Player) <= Player.AttackRange)
                    {
                        Spells.E.Cast(Target);
                    }   
                }
                if(Player.Mana < 5)
                {
                    if (MenuConfig.UseItem && Spells.Q.IsReady() && Spells.W.IsReady())
                    {
                        ITEM.CastYomu();
                    }
                    if (Spells.E.IsReady() && !hasPassive)
                    {
                        Spells.E.Cast(Target);
                    }
                    if(Target.Distance(Player) <= Spells.W.Range)
                    {
                        if (Spells.Q.IsReady())
                        {
                            Spells.Q.Cast(Target);
                        }
                        if (Spells.W.IsReady())
                        {
                            if (MenuConfig.UseItem)
                            {
                                ITEM.CastHydra();
                            }
                            Spells.W.Cast(Target);
                        }
                    }
                }
            }
        }
        #endregion
        #region ApCombo
        public static void ApCombo()
        {
            var hasPassive = Player.HasBuff("RengarRBuff") || Player.HasBuff("RengarPassiveBuff");

            var Target = Variables.TargetSelector.GetTarget(Spells.E.Range, DamageType.Magical);

            if (Target != null && Target.IsValidTarget() && !Target.IsZombie)
            {
                if (Player.Mana == 5)
                {
                    if (MenuConfig.UseItem && Spells.Q.IsReady() && Spells.W.IsReady() && hasPassive)
                    {
                        ITEM.CastYomu();
                    }
                    if(Target.Distance(Player) <= Spells.W.Range)
                    {
                        if (MenuConfig.UseItem && Spells.W.IsReady())
                        {
                            ITEM.CastHydra();
                        }
                        if (Spells.W.IsReady())
                        {
                            Spells.W.Cast(Target);
                        }
                    }
                }
                if (Player.Mana < 5)
                {
                     if (MenuConfig.UseItem && Spells.Q.IsReady() && Spells.W.IsReady() && hasPassive)
                    {
                        ITEM.CastYomu();
                    }
                    if(Target.Distance(Player) <= Spells.W.Range)
                    {
                        if (MenuConfig.UseItem && Spells.W.IsReady())
                        {
                            ITEM.CastHydra();
                        }
                        if (Spells.W.IsReady())
                        {
                            Spells.W.Cast(Target);
                        }
                        else if (Spells.Q.IsReady())
                        {
                            Spells.Q.Cast(Target);
                        }
                        else if (Spells.E.IsReady())
                        {
                            if (MenuConfig.IgnoreE)
                            {
                                Spells.E.Cast(Target.Position);
                            }
                            else
                            {
                                Spells.E.Cast(Target);
                            }
                        }
                    }
                }
            }
        }
        #endregion
        #region TripleQ
        public static void TripleQ()
        {
            var hasPassive = Player.HasBuff("RengarRBuff") || Player.HasBuff("RengarPassiveBuff");

            var Target = Variables.TargetSelector.GetTarget(Spells.E.Range, DamageType.Physical);

            if (Target != null && Target.IsValidTarget() && !Target.IsZombie)
            {
                if (Player.Mana == 5)
                {
                    if (MenuConfig.UseItem && Spells.Q.IsReady() && Spells.W.IsReady())
                    {
                        ITEM.CastYomu();
                    }
                    if (Spells.Q.IsReady() && Target.Distance(Player) <= Player.AttackRange)
                    {
                        Spells.Q.Cast(Target);
                    }
                }
                if (Player.Mana < 5)
                {
                    if (MenuConfig.UseItem && Spells.Q.IsReady() && Spells.W.IsReady())
                    {
                        ITEM.CastYomu();
                    }
                    if (Spells.Q.IsReady())
                    {
                        Spells.Q.Cast(Target);
                    }
                    if (Spells.E.IsReady() && !Spells.Q.IsReady() && !hasPassive)
                    {
                        if (MenuConfig.IgnoreE)
                        {
                            Spells.E.Cast(Target.Position);
                        }
                        else
                        {
                            Spells.E.Cast(Target);
                        }
                    }
                   if (Spells.W.IsReady() && !Spells.Q.IsReady() && Player.Distance(Target) <= Spells.W.Range)
                    {
                        if (MenuConfig.UseItem)
                        {
                            ITEM.CastHydra();
                        }
                        Spells.W.Cast(Target);
                    }
                }
            }
        }
        #endregion

        #region Lane
        public static void Lane()
        {
            var minions = GameObjects.EnemyMinions.Where(m => m.IsMinion && m.IsEnemy && m.Team != GameObjectTeam.Neutral && m.IsValidTarget(Player.AttackRange)).ToList();
            var hasPassive = Player.HasBuff("RengarRBuff") || Player.HasBuff("RengarPassiveBuff");

            if (minions == null || Player.Mana == 5 && MenuConfig.Passive.Active)
            {
                return;
            }

            foreach(var m in minions)
            {
                if (Player.Mana == 5)
                {
                    if (Spells.Q.IsReady() && m.Distance(Player) <= Player.AttackRange)
                    {
                        if (MenuConfig.UseItem)
                        {
                            ITEM.CastHydra();
                        }
                        Spells.Q.Cast(m);
                    }
                }
                if (Player.Mana < 5)
                {
                    if (Spells.Q.IsReady())
                    {
                        Spells.Q.Cast(m);
                    }
                    if (Spells.E.IsReady() && !hasPassive)
                    {
                        Spells.E.Cast(m);
                    }
                    if (Spells.W.IsReady() && m.Distance(Player) <= Spells.W.Range)
                    {
                        if (MenuConfig.UseItem)
                        {
                            ITEM.CastHydra();
                        }
                        Spells.W.Cast(m);
                    }
                }
            }
              
        }
        #endregion
        #region Jungle
        public static void Jungle()
        {
            var mob = ObjectManager.Get<Obj_AI_Minion>().Where(m => !m.IsDead && !m.IsZombie && m.Team == GameObjectTeam.Neutral && m.IsValidTarget(Spells.W.Range)).ToList();

            var hasPassive = Player.HasBuff("RengarRBuff") || Player.HasBuff("RengarPassiveBuff");

            if (mob == null || Player.Mana == 5 && MenuConfig.Passive.Active)
            {
                return;
            }

            foreach (var m in mob)
            {
                if (Player.Mana == 5)
                {
                    if (Spells.Q.IsReady() && Player.HealthPercent >= 80)
                    {
                        Spells.Q.Cast(m.ServerPosition);
                    }
                    if (Spells.W.IsReady() && m.Distance(Player) <= Spells.W.Range && Player.HealthPercent < 80)
                    {
                        if (MenuConfig.UseItem)
                        {
                            ITEM.CastHydra();
                        }
                        Spells.W.Cast(m.ServerPosition);
                    }
                }
                if (Player.Mana < 5)
                {
                    if (Spells.Q.IsReady())
                    {
                        Spells.Q.Cast(m.ServerPosition);
                    }
                   else if (Spells.W.IsReady() && m.Distance(Player) <= Spells.W.Range)
                    {
                        if (MenuConfig.UseItem)
                        {
                            ITEM.CastHydra();
                        }
                        Spells.W.Cast(m.ServerPosition);
                    }
                   else if (Spells.E.IsReady() && !hasPassive)
                    {
                        Spells.E.Cast(m.ServerPosition);
                    }
                }
            }
        }
        #endregion
        #region LastHit
        public static void LastHit()
        {
            var minions = GameObjects.EnemyMinions.Where(m => m.IsMinion && m.IsEnemy && m.Team != GameObjectTeam.Neutral && m.IsValidTarget(Player.AttackRange)).ToList();
            

            if (minions == null || Player.Mana == 5 && MenuConfig.Passive.Active)
            {
                return;
            }
            if(MenuConfig.StackLastHit)
            {
                foreach (var m in minions)
                {
                    if (m.Health < Spells.Q.GetDamage(m) + (float)Player.GetAutoAttackDamage(m))
                    {
                        Spells.Q.Cast();
                    }
                }
            }
        }
        #endregion

        #region Skin
        public static void SKIN()
        {
           if(MenuConfig.UseSkin)
            {
                var Skin = MenuConfig.SkinChanger.Index;
                switch(Skin)
                {
                    case 0:
                        Player.SetSkin(Player.CharData.BaseSkinName, 0);
                        break;
                    case 1:
                        Player.SetSkin(Player.CharData.BaseSkinName, 1);
                        break;
                    case 2:
                        Player.SetSkin(Player.CharData.BaseSkinName, 2);
                        break;
                    case 3:
                        Player.SetSkin(Player.CharData.BaseSkinName, 3);
                        break;
                }
            }
           else
            {
                Player.SetSkin(Player.CharData.BaseSkinName, Player.BaseSkinId);
            }
        }
        #endregion
    }
}
