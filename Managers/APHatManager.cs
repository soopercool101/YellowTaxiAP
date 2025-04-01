using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;

namespace YellowTaxiAP.Managers
{
    public class APHatManager
    {
        public APHatManager()
        {
            //On.HatBuyScript.Update += AP_HatUpdate;
            On.HatBuyScript.Buy += AP_BuyHat;
        }

        /// <summary>
        /// TODO: Don't disable in-level hat purchases until the item is purchased
        /// </summary>
        private void AP_HatUpdate(On.HatBuyScript.orig_Update orig, HatBuyScript self)
        {
            orig(self);
        }

        private void AP_BuyHat(On.HatBuyScript.orig_Buy orig, HatBuyScript self)
        {
            if (!self.versioneArmadio)
            {
                var id = $"0_{Identifiers.HAT_ID:D2}_{(int)self.myHatKind:D5}";
                DebugLocationHelper.CheckLocation("hat", id);
            }
            orig(self);
        }
    }
}
