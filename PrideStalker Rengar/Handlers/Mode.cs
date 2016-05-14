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

        public static bool hasPassive => Player.Buffs.Any(x => x.Name.ToLower().Contains("rengarpassivebuff"));
        #region Combo
        public static void Combo()
        {   
            var Target = Variables.TargetSelector.GetTarget(1000, DamageType.Physical);
            

            if (Target != null && Target.IsValidTarget() && !Target.IsZombie)
            {
                if(Player.Mana == 5)
                {
                    if(MenuConfig.UseItem && Spells.Q.IsReady() && Spells.W.IsReady() && hasPassive)
                    {
                        ITEM.CastYomu();
                    }
                    if(Spells.E.IsReady() && Target.Distance(Player) <= Spells.E.Range)
                    {
                       Spells.E.Cast(Target);
                    }
                    if (Spells.Q.IsReady() && Target.Distance(Player) <= Player.AttackRange && !Spells.E.IsReady())
                    {
                        Spells.Q.Cast(Target);
                    }
                }
                if(Player.Mana < 5)
                {
                    if (MenuConfig.UseItem && Spells.Q.IsReady() && Spells.W.IsReady() && hasPassive)
                    {
                        ITEM.CastYomu();
                    }
                    if (Spells.E.IsReady() && !hasPassive && Target.Distance(Player) <= Spells.E.Range)
                    {
                        Spells.E.Cast(Target);
                    }
                    if(Target.Distance(Player) <= Spells.W.Range)
                    {
                        if (Spells.Q.IsReady())
                        {
                            Spells.Q.Cast(Target);
                        }
                        if (MenuConfig.UseItem)
                        {
                            ITEM.CastHydra();
                        }
                        if (Spells.W.IsReady())
                        {
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
                     if(MenuConfig.UseItem && !hasPassive)
                    {
                        ITEM.CastProtobelt();
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
                            Spells.W.Cast(Target);
                        }
                        else if (Spells.Q.IsReady())
                        {
                            Spells.Q.Cast(Target);
                        }
                        else if (Spells.E.IsReady())
                        {
                            if (MenuConfig.IgnoreE && hasPassive)
                            {
                                Spells.E.Cast(Game.CursorPos);
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
            var Target = Variables.TargetSelector.GetTarget(Spells.E.Range, DamageType.Physical);

            if (Target != null && Target.IsValidTarget() && !Target.IsZombie)
            {
                if (Player.Mana == 5)
                {
                    if (MenuConfig.UseItem && Spells.Q.IsReady() && hasPassive)
                    {
                        ITEM.CastYomu();
                    }
                    
                    if (Spells.Q.IsReady() && Target.Distance(Player) <= Spells.W.Range)
                    {
                        if(!MenuConfig.TripleQAAReset)
                        {
                            Spells.Q.Cast();
                        }
                    }
                }
                if (Player.Mana < 5)
                {
                    if (MenuConfig.UseItem && Spells.Q.IsReady() && Spells.W.IsReady())
                    {
                        ITEM.CastYomu();
                    }
                    if (Spells.Q.IsReady() && Target.Distance(Player) <= Spells.W.Range)
                    {
                        if (!MenuConfig.TripleQAAReset)
                        {
                            Spells.Q.Cast();
                        }
                    }
                    if (Spells.E.IsReady() && !Spells.Q.IsReady())
                    {
                        if (MenuConfig.IgnoreE && hasPassive)
                        {
                            Spells.E.Cast(Game.CursorPos);
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
        #region OneShot
        public static void OneShot()
        {
            var Target = Variables.TargetSelector.GetTarget(Spells.E.Range, DamageType.Physical);
            var minions = GameObjects.EnemyMinions.Where(m => m.IsMinion && m.IsEnemy && m.Team != GameObjectTeam.Neutral && m.IsValidTarget(Spells.W.Range)).ToList();

            if (Target != null && Target.IsValidTarget() && !Target.IsZombie)
            {
                if (Player.Mana == 5)
                {
                    if (MenuConfig.UseItem && Spells.Q.IsReady() && hasPassive)
                    {
                        ITEM.CastYomu();
                    }
                    if (Spells.Q.IsReady() && Target.Distance(Player) <= Spells.E.Range)
                    {
                        Spells.Q.Cast();   
                    }
                }
                if (Player.Mana < 5)
                {
                    if (MenuConfig.UseItem && Spells.Q.IsReady() && Spells.W.IsReady())
                    {
                        ITEM.CastYomu();
                    }
                    if (Spells.E.IsReady())
                    {
                        if (MenuConfig.IgnoreE && hasPassive)
                        {
                            Spells.E.Cast(Game.CursorPos);
                        }
                        else
                        {
                            Spells.E.Cast(Target);
                        }
                    }
                    if (Spells.Q.IsReady() && Target.Distance(Player) <= Spells.W.Range)
                    {
                        Spells.Q.Cast();
                    }
                    if (Spells.W.IsReady() && Player.Distance(Target) <= Spells.W.Range)
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
            var minions = GameObjects.EnemyMinions.Where(m => m.IsMinion && m.IsEnemy && m.Team != GameObjectTeam.Neutral && m.IsValidTarget(Spells.W.Range)).ToList();

            if (minions == null || Player.Mana == 5 && MenuConfig.Passive.Active)
            {
                return;
            }

            foreach(var m in minions)
            {
                if (Player.Mana == 5)
                {
                    if (MenuConfig.ComboMode.SelectedValue == "Ap Combo")
                    {
                        if (Spells.W.IsReady() && m.Distance(Player) <= Spells.W.Range)
                        {
                            Spells.W.Cast(m.ServerPosition);
                        }
                    }
                    else
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

            if (mob == null || Player.Mana == 5 && MenuConfig.Passive.Active)
            {
                return;
            }

            foreach (var m in mob)
            {
                if (Player.Mana == 5)
                {
                    if(MenuConfig.ComboMode.SelectedValue == "Ap Combo")
                    {
                        if(Spells.W.IsReady() && m.Distance(Player) <= Spells.W.Range)
                        {
                            Spells.W.Cast(m.ServerPosition);
                        }
                    }
                    else
                    {
                        if (Spells.W.IsReady() && m.Distance(Player) <= Spells.W.Range && Player.HealthPercent < 80)
                        {
                            if (MenuConfig.UseItem)
                            {
                                ITEM.CastHydra();
                            }
                            Spells.W.Cast(m.ServerPosition);
                        }
                    }
                }
                if (Player.Mana < 5)
                {
                   
                    if (Spells.W.IsReady() && m.Distance(Player) <= Spells.W.Range)
                    {
                        if (MenuConfig.UseItem)
                        {
                            ITEM.CastHydra();
                        }
                        Spells.W.Cast(m.ServerPosition);
                    }
                   if (Spells.E.IsReady())
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

        #region ComboMode
        public static void ChangeComboMode()
        {
            if(MenuConfig.ChangeComboMode.Active)
            {
                switch(MenuConfig.ComboMode.SelectedValue)
                {
                    case "Gank":
                        DelayAction.Add(200, () => MenuConfig.ComboMode.SelectedValue = "Triple Q");
                        break;
                    case "Triple Q":
                        DelayAction.Add(200, () => MenuConfig.ComboMode.SelectedValue = "Ap Combo");
                        break;
                    case "Ap Combo":
                        DelayAction.Add(200, () => MenuConfig.ComboMode.SelectedValue = "OneShot");
                        break;
                    case "OneShot":
                        DelayAction.Add(200, () => MenuConfig.ComboMode.SelectedValue = "Gank");
                        break;
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
