#region

using System;
using System.Linq;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Enumerations;
using Swiftly_Teemo.Menu;

#endregion

namespace Swiftly_Teemo.Handler
{
    internal class Mode : Core
    {
        public static void Combo()
        {
            var targets = GameObjects.EnemyHeroes.Where(x => x.IsValidTarget(Spells.R.Range)).ToList();

            foreach (var target in targets)
            {
                if (target == null)
                {
                    return;
                }

                if (MenuConfig.TowerCheck && target.IsUnderEnemyTurret())
                {
                    return;
                }
            
                if (Spells.R.IsReady())
                {
                   
                    var rPrediction = Spells.R.GetPrediction(target);

                    if (rPrediction.Hitchance >= HitChance.High)
                    {
                        Spells.R.Cast(rPrediction.CastPosition);
                    }
                }

                if (Spells.W.IsReady() && Player.ManaPercent >= 15)
                {
                    Spells.W.Cast();
                }
            }
        }
       
        public static void Jungle()
        {
            var mob = GameObjects.JungleLarge.Where(m => m.IsValidTarget(Spells.R.Range));
            var ammo = ObjectManager.Player.Spellbook.GetSpell(SpellSlot.R).Ammo;

         
            foreach (var m in from m in mob where Spells.R.IsReady() &&
                              m.Health > Spells.R.GetDamage(m)*2 where
                              !m.SkinName.Contains("Sru_Crab") where
                              ammo >= 3 select
                              m)
            {
                Spells.R.Cast(m);
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

            if (!Spells.R.IsReady()) return;

            foreach (var target in GameObjects.EnemyHeroes.Where(x => x.IsValidTarget(Spells.R.Range)))
            {
                if (target == null)
                {
                    return;
                }

                if(target.Distance(Player) > Spells.R.Range) return;

                Spells.R.Cast(Player.Position);
            }
        }

        public static void Skin()
        {
            Player.SetSkin(Player.CharData.BaseSkinName, MenuConfig.UseSkin ? MenuConfig.SkinChanger.Index : Player.BaseSkinId);
        }
    }
}
