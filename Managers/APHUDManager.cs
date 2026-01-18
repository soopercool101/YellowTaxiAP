using I2.Loc;
using UnityEngine;

namespace YellowTaxiAP.Managers
{
    public class APHUDManager
    {
        public APHUDManager()
        {
            //On.HudMasterScript.Update += HudMasterScript_Update;
            On.HudMasterScript.Update += HudMasterScript_Update;
            //On.HudMasterScript.UpdateGearsText += HudMasterScript_UpdateGearsText;
        }

        private void HudMasterScript_Update(On.HudMasterScript.orig_Update orig, HudMasterScript self)
        {
            var shouldUpdateCoins = Data.coinsCollected[Data.gameDataIndex] != APWalletManager.ServerCoins &&
                (self.coinsOld != Data.coinsCollected[Data.gameDataIndex] || self.shouldUpdateCoinsText);
            orig(self);
            if (shouldUpdateCoins)
            {
                self.coinsText.SetText(APWalletManager.ServerCoins.ToString(), false);
            }
        }
    }
}
