using System;
using System.Collections.Generic;
using System.Text;
using static Steamworks.InventoryItem;

namespace YellowTaxiAP.Managers
{
    public class APWalletManager
    {
        public APWalletManager()
        {
            On.ModMaster.OnPlayerOnCoinCollect += ModMaster_OnPlayerOnCoinCollect;
            On.ModMaster.OnPlayerDie += ModMaster_OnPlayerDie;
        }

        private void ModMaster_OnPlayerDie(On.ModMaster.orig_OnPlayerDie orig, ModMaster self)
        {
            orig(self);
            if (Data.IsLevelPsychoTaxiMode(GameplayMaster.instance.levelId))
            {
                return;
            }

            var amount = Math.Min(30, Data.coinsCollected[Data.gameDataIndex]);
            Plugin.ArchipelagoClient.UpdateWallet(-amount);
        }

        private void ModMaster_OnPlayerOnCoinCollect(On.ModMaster.orig_OnPlayerOnCoinCollect orig, ModMaster self, int amount)
        {
            orig(self, amount);
            if (amount != 0)
            {
                Plugin.ArchipelagoClient.UpdateWallet(amount);
            }
        }

    }
}
