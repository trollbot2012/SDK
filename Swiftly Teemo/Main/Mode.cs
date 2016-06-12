#region

using System.Linq;
using LeagueSharp;
using LeagueSharp.SDK;

#endregion

namespace Swiftly_Teemo.Main
{
    internal class Mode : Core
    {   
        public static void Combo()
        {
            if(Target.IsValidTarget() && Target != null && !Target.IsZombie)
            {
                if (Spells.R.IsReady() && Target.Distance(Player) <= Spells.R.Range - 50)
                {
                    Spells.R.Cast(Target);
                }
                if (Target.IsInvulnerable && Target != null)
                {
                    Spells.R.Cast(Target.Position);
                }
                if (Spells.W.IsReady() && Player.ManaPercent > 22.5)
                {
                    Spells.W.Cast();
                }
            }
        }
       
        public static void Lane()
        {
            var minions = GameObjects.EnemyMinions.Where(m => m.IsMinion && m.IsEnemy && m.Team != GameObjectTeam.Neutral && m.IsValidTarget(Player.AttackRange)).ToList();
            var ammo = ObjectManager.Player.Spellbook.GetSpell(SpellSlot.R).Ammo;
            if (minions == null)
            {
                return;
            }
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
            var mob = ObjectManager.Get<Obj_AI_Minion>().Where(m => !m.IsDead && !m.IsZombie && m.Team == GameObjectTeam.Neutral && m.IsValidTarget(Spells.Q.Range)).ToList();
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
            if(!MenuConfig.Flee.Active)
            {
                return;
            }

                ObjectManager.Player.IssueOrder(GameObjectOrder.MoveTo, Game.CursorPos);

                if(Spells.W.IsReady())
                {
                    Spells.W.Cast();
                }
                if(Target.Distance(Player) <= Spells.R.Range && Target.IsValidTarget() && Target != null)
                {
                if(Spells.R.IsReady())
                  {
                    Spells.R.Cast(Player.Position);
                  }
                }
            }
        
        public static void SKIN()
        {
            if(MenuConfig.UseSkin)
            {
                switch(MenuConfig.SkinChanger.Index)
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
                    case 4:
                        Player.SetSkin(Player.CharData.BaseSkinName, 4);
                        break;
                    case 5:
                        Player.SetSkin(Player.CharData.BaseSkinName, 5);
                        break;
                    case 6:
                        Player.SetSkin(Player.CharData.BaseSkinName, 6);
                        break;
                    case 7:
                        Player.SetSkin(Player.CharData.BaseSkinName, 7);
                        break;
                    case 8:
                        Player.SetSkin(Player.CharData.BaseSkinName, 8);
                        break;
                }
            }
            else
            {
                Player.SetSkin(Player.CharData.BaseSkinName, Player.BaseSkinId);
            }
        }
    }
}
