﻿namespace YellowTaxiAP.Managers
{
    public class MenuManager
    {

        public MenuManager()
        {
            // MenuV2Script hooks
            On.MenuV2Script.GotoLabConditionGet += GotoLabConditionGet_AP;
        }

        private bool GotoLabConditionGet_AP(On.MenuV2Script.orig_GotoLabConditionGet orig, MenuV2Script self)
        {
            return MapArea.IsPlayerInsideLab() || orig(self);
        }
    }
}
