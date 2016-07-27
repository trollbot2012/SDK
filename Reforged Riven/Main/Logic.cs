#region

using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Utils;

#endregion

namespace Reforged_Riven.Main
{
    internal class Logic : Core // I NEED to come up with better ways rather than this class
    {
        public static bool _forceQ;
        public static bool _forceW;
        public static bool _forceR;
        public static bool _forceR2;
        public static bool _forceItem;
        public static AttackableUnit _qtarget;

        public static int WRange => Player.HasBuff("RivenFengShuiEngine")
            ? 330
            : 265;

        public static bool InWRange(AttackableUnit t) => t.IsValidTarget(WRange);

        public static bool InQRange(GameObject target)
        {
            return target != null && (Player.HasBuff("RivenFengShuiEngine")
                ? 330 >= Player.Distance(target.Position)
                : 265 >= Player.Distance(target.Position));
        }

        //public static void ForceItem()
        //{
        //    if (Items.CanUseItem(Item) && Items.HasItem(Item) && Item != 0) _forceItem = true;
        //    DelayAction.Add(500, () => _forceItem = false);
        //}

        public static void ForceSkill()
        {
            if (_forceQ && Spells.Q.IsReady() && _qtarget != null)
            {
                Spells.Q.Cast(_qtarget.Position);
            }

            if (_forceW)
            {
                Spells.W.Cast();
            }

            if (_forceR && Spells.R.Instance.Name == IsFirstR)
            {
                Spells.R.Cast();
            }

            //if (_forceItem && Items.CanUseItem(Item) && Items.HasItem(Item) && Item != 0)
            //{
            //    Items.UseItem(Item);
            //}

            if (!_forceR2 || Spells.R.Instance.Name != IsSecondR) return;

            var target = Variables.TargetSelector.GetSelectedTarget();
            if (target == null) return;

            Spells.R.Cast(target.Position);
        }

        public static void ForceR()
        {
            _forceR = Spells.R.IsReady() && Spells.R.Instance.Name == IsFirstR;
            DelayAction.Add(500, () => _forceR = false);
        }

        public static void ForceR2()
        {
            _forceR2 = Spells.R.IsReady() && Spells.R.Instance.Name == IsSecondR;
            DelayAction.Add(500, () => _forceR2 = false);
        }

        public static void ForceW()
        {
            _forceW = Spells.W.IsReady();
            DelayAction.Add(500, () => _forceW = false);
        }

        public static void ForceCastQ(AttackableUnit target)
        {
            _forceQ = true;
            _qtarget = target;
        }

        public static void CastHydra()
        {
            if (Items.CanUseItem(3074))
            {
                Items.UseItem(3074);
            }

            if (Items.CanUseItem(3077))
            {
                Items.UseItem(3077);
            }
        }
        public static void CastYomu()
        {
            if (!Items.CanUseItem(3142)) return;

             Items.UseItem(3142);
        }

        public static void OnCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!sender.IsMe) return;

            
            if (args.SData.Name.Contains("ItemTiamatCleave")) _forceItem = false;
            if (args.SData.Name.Contains("RivenTriCleave")) _forceQ = false;
            if (args.SData.Name.Contains("RivenMartyr")) _forceW = false;
            if (args.SData.Name == IsFirstR) _forceR = false;
            if (args.SData.Name == IsSecondR) _forceR2 = false;
        }
        //public static int Item
        //   =>
        //       Items.CanUseItem(3077) && Items.HasItem(3077)
        //           ? 3077
        //           : Items.CanUseItem(3074) && Items.HasItem(3074) ? 3074 : 0;

    }
}
