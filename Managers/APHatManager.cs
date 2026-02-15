using YellowTaxiAP.Behaviours;

namespace YellowTaxiAP.Managers
{
    public class APHatManager
    {
        public APHatManager()
        {
            //On.HatBuyScript.Update += AP_HatUpdate;
            On.HatBuyScript.Buy += AP_BuyHat;
            On.HatScript.RemoveHat += HatScript_RemoveHat;
            On.HatScript.Instantiate += HatScript_Instantiate;
        }

        private void HatScript_RemoveHat(On.HatScript.orig_RemoveHat orig, bool removeHatFromData)
        {
            orig(false);
        }

        private HatScript HatScript_Instantiate(On.HatScript.orig_Instantiate orig, Data.Hat hatKind)
        {
            if (!Plugin.SlotData.Hatsanity)
            {
                APSaveController.HatSave.SetHatUnlocked(hatKind);
            }
            return orig(hatKind);
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
#if DEBUG
                var id = $"0_{Identifiers.HAT_ID:D2}_{(int)self.myHatKind:D5}";
                DebugLocationHelper.CheckLocation("hat", id);
#endif
            }
            orig(self);
            APSaveController.MiscSave.CurrentHat = (Data.Hat)Data.currentHat[Data.gameDataIndex];
        }
    }
}
