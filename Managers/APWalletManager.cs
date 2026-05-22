using System;

namespace YellowTaxiAP.Managers
{
    public class APWalletManager
    {
        public static int ServerCoins { get; set; }

        public APWalletManager()
        {
            On.ModMaster.OnPlayerOnCoinCollect += ModMaster_OnPlayerOnCoinCollect;
            On.MenuEventLeaderboard.CoinsSpentAdd += MenuEventLeaderboard_CoinsSpentAdd;
            On.Data.CoinsLostCountAdd += Data_CoinsLostCountAdd;
        }

        private void Data_CoinsLostCountAdd(On.Data.orig_CoinsLostCountAdd orig, int value)
        {
            orig(value);
            if (value != 0)
            {
                Plugin.ArchipelagoClient.UpdateWallet(-Math.Min(value, ServerCoins));
            }
        }

        private void MenuEventLeaderboard_CoinsSpentAdd(On.MenuEventLeaderboard.orig_CoinsSpentAdd orig, int coins)
        {
            Plugin.Log($"Purchase made for {coins} coins. A {Plugin.SlotData.PurchaseRebatePercent}% rebate should be granted.");
            orig(coins);
            if (coins != 0)
            {
                Plugin.ArchipelagoClient.UpdateWallet(-coins);
                if (Plugin.SlotData.PurchaseRebatePercent > 0)
                {
                    var rebate = (coins * Plugin.SlotData.PurchaseRebatePercent) / 100;
                    Plugin.Log($"Purchase made for {coins} coins. Granting {Plugin.SlotData.PurchaseRebatePercent}% rebate of {rebate} coins.");
                    Data.coinsLostCount[Data.gameDataIndex] += rebate;
                }
            }
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
