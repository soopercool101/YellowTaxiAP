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
            orig(coins);
            if (coins != 0)
            {
                Plugin.ArchipelagoClient.UpdateWallet(-coins);
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
