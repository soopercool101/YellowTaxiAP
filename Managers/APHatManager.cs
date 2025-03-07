using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace YellowTaxiAP.Managers
{
    public class APHatManager
    {
        public APHatManager()
        {
            On.HatBuyScript.Buy += AP_BuyHat;
        }

        private void AP_BuyHat(On.HatBuyScript.orig_Buy orig, HatBuyScript self)
        {
            var id = $"0_{Identifiers.HAT_ID:D2}_{(int)self.myHatKind:D5}";
            DebugLocationHelper.CheckLocation("hat", id);
            orig(self);
        }
    }
}
