using System;
using System.Collections.Generic;
using System.Text;

namespace YellowTaxiAP.Managers
{
    public class APPortalManager
    {
        public APPortalManager()
        {
            On.PortalScript.Awake += PortalScript_Awake;
        }

        private void PortalScript_Awake(On.PortalScript.orig_Awake orig, PortalScript self)
        {
            self.hubPortalForceEnabled = true;
            orig(self);
        }
    }
}
