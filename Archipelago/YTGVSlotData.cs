using System.Collections.Generic;
using YellowTaxiAP.Managers;

namespace YellowTaxiAP.Archipelago
{
    public class YTGVSlotData
    {
        /// <summary>
        /// Backwards compatibility. Lowest x.y.0 version supported, where y is the lowest supported minor version
        /// </summary>
        public const int LowestSupportedMajorVersion = 0;
        /// <summary>
        /// Backwards compatibility. Lowest supported minor version in the lowest supported major version.
        /// </summary>
        public const int LowestSupportedMinorVersion = 1;
        /// <summary>
        /// Highest x.y.# version supported, where y is in the highest supported minor version
        /// Should be up to date with latest APWorld whenever a new version is released.
        /// </summary>
        public const int HighestSupportedMajorVersion = 0;
        /// <summary>
        /// Highest supported minor version in the highest supported major version.
        /// Should be up to date with latest APWorld whenever a new version is released.
        /// </summary>
        public const int HighestSupportedMinorVersion = 1;
        public bool FailedValidation { get; private set; }
        public long APWorldMajorVersion { get; set; }
        public long APWorldMinorVersion { get; set; }

        public enum GoalType : long
        {
            Bombeach = 0,
            ToslaOffices = 1,
            Moon = 2,
        }
        public GoalType Goal { get; private set; }
        public bool DeathLink { get; private set; }
        public bool ShuffleGelaToni { get; private set; }
        public bool ShufflePizzaKing { get; private set; }
        public bool ShuffleDoggo { get; private set; }
        public bool ShuffleOrangeSwitch { get; private set; }
        public bool ShuffleMoriosPassword { get; private set; }
        public bool ShuffleRocket { get; private set; }
        public bool ShuffleFullGame { get; private set; }
        public bool ShufflePsychoTaxi { get; private set; }
        public bool ShuffleRat { get; private set; }
        public bool Bunnysanity { get; private set; }
        public bool Checkpointsanity { get; private set; }
        public bool Safesanity { get; private set; }
        public bool Chestsanity { get; private set; }
        public bool Coinbagsanity { get; private set; }
        public bool Coinsanity { get; private set; }
        public bool Cheesesanity { get; private set; }
        public bool Hatsanity { get; private set; }
        public bool ExtraDemoCollectables { get; private set; }
        public bool ShuffleFlipOWill { get; private set; }
        public bool ShuffleGlide { get; private set; }
        public bool ShuffleGoldenSpring { get; private set; }
        public bool ShuffleGoldenPropeller { get; private set; }

        // Early location states, and explicitly excluded hubworld items
        public bool EarlyPizzaKing { get; private set; }
        public bool EarlyRat { get; private set; }
        public bool EarlyDoggo { get; private set; }
        public bool EarlyBackflip { get; private set; }
        public bool EarlyPsychoTaxi { get; private set; }
        public bool EarlyOrangeSwitch { get; private set; }
        public bool EarlyGoldenSpring { get; private set; }
        public bool EarlyGoldenPropeller { get; private set; }
        public bool EarlyMoriosPassword { get; private set; }
        public bool EarlyRocket { get; private set; }
        public bool ExcludeSpikeBunny { get; private set; }
        public bool ExcludeTopBunny { get; private set; }

        public YTGVSlotData()
        {
            // Defaults
        }

        public YTGVSlotData(Dictionary<string, object> slotData)
        {
            if (slotData.TryGetValue("minor_version", out var minorVersion) && slotData.TryGetValue("major_version", out var majorVersion))
            {
                APWorldMajorVersion = (long)majorVersion;
                APWorldMinorVersion = (long)minorVersion;
                if (APWorldMajorVersion < LowestSupportedMajorVersion || (APWorldMajorVersion == LowestSupportedMajorVersion && APWorldMinorVersion < LowestSupportedMinorVersion))
                {
                    ArchipelagoClient.Authenticated = false;
                    Plugin.Log($"ERROR: Game was generated on version {APWorldMajorVersion}.{APWorldMinorVersion}.x which is lower than lowest supported APWorld version {LowestSupportedMajorVersion}.{LowestSupportedMinorVersion}.0. Please update your APWorld or use an older version of the mod.", true);
                    FailedValidation = true;
                    return;
                }
                if (APWorldMajorVersion > HighestSupportedMajorVersion || (APWorldMajorVersion == HighestSupportedMajorVersion && APWorldMinorVersion > HighestSupportedMinorVersion))
                {
                    ArchipelagoClient.Authenticated = false;
                    Plugin.Log($"ERROR: Game was generated on version {APWorldMajorVersion}.{APWorldMinorVersion}.x which is higher than highest supported APWorld version {HighestSupportedMajorVersion}.{HighestSupportedMinorVersion}.x. Please update your game mod.", true);
                    FailedValidation = true;
                    return;
                }
            }
            else
            {
                Plugin.Log("No slot data for version found");
                Plugin.Log("ERROR: Game was generated on an unknown version, make sure your APWorld and game mod are up-to-date!", true);
                FailedValidation = true;
            }

            if (slotData.ContainsKey("goal"))
            {
                Goal = (GoalType)slotData["goal"];
            }
            else
            {
                Plugin.Log("No slot data for goal found");
            }

            if (slotData.ContainsKey("death_link"))
            {
                DeathLink = (long)slotData["death_link"] == 1;
            }
            else
            {
                Plugin.Log("No slot data for death_link found");
            }

            if (slotData.ContainsKey("shuffle_gela_toni"))
            {
                ShuffleGelaToni = (long)slotData["shuffle_gela_toni"] == 1;
            }
            else
            {
                Plugin.Log("No slot data for shuffle_gela_toni found");
            }

            if (slotData.ContainsKey("shuffle_pizza_king"))
            {
                ShufflePizzaKing = (long)slotData["shuffle_pizza_king"] == 1;
            }
            else
            {
                Plugin.Log("No slot data for shuffle_pizza_king found");
            }

            if (slotData.ContainsKey("shuffle_doggo"))
            {
                ShuffleDoggo = (long)slotData["shuffle_doggo"] == 1;
            }
            else
            {
                Plugin.Log("No slot data for shuffle_doggo found");
            }

            if (slotData.ContainsKey("shuffle_orange_switch"))
            {
                ShuffleOrangeSwitch = (long)slotData["shuffle_orange_switch"] == 1;
            }
            else
            {
                Plugin.Log("No slot data for shuffle_orange_switch found");
            }

            if (slotData.ContainsKey("shuffle_morios_password"))
            {
                ShuffleMoriosPassword = (long)slotData["shuffle_morios_password"] == 1;
            }
            else
            {
                Plugin.Log("No slot data for shuffle_morios_password found");
            }

            if (slotData.ContainsKey("shuffle_rocket"))
            {
                ShuffleRocket = (long)slotData["shuffle_rocket"] == 1;
            }
            else
            {
                Plugin.Log("No slot data for shuffle_rocket found");
            }

            if (slotData.ContainsKey("shuffle_full_game"))
            {
                ShuffleFullGame = (long)slotData["shuffle_full_game"] == 1;
            }
            else
            {
                Plugin.Log("No slot data for shuffle_full_game found");
            }

            if (!ShuffleFullGame)
            {
                APAreaStateManager.FullGameUnlocked = true;
            }

            if (slotData.ContainsKey("shuffle_psycho_taxi"))
            {
                ShufflePsychoTaxi = (long)slotData["shuffle_psycho_taxi"] == 1;
            }
            else
            {
                Plugin.Log("No slot data for shuffle_psycho_taxi found");
            }

            if (slotData.ContainsKey("shuffle_rat"))
            {
                ShuffleRat = (long)slotData["shuffle_rat"] == 1;
            }
            else
            {
                Plugin.Log("No slot data for shuffle_rat found");
            }

            if (slotData.ContainsKey("bunnysanity"))
            {
                Bunnysanity = (long)slotData["bunnysanity"] == 1;
            }
            else
            {
                Plugin.Log("No slot data for bunnysanity found");
            }

            if (slotData.ContainsKey("checkpointsanity"))
            {
                Checkpointsanity = (long)slotData["checkpointsanity"] == 1;
            }
            else
            {
                Plugin.Log("No slot data for checkpointsanity found");
            }

            if (slotData.ContainsKey("safesanity"))
            {
                Safesanity = (long)slotData["safesanity"] == 1;
            }
            else
            {
                Plugin.Log("No slot data for safesanity found");
            }

            if (slotData.ContainsKey("chestsanity"))
            {
                Chestsanity = (long)slotData["chestsanity"] == 1;
            }
            else
            {
                Plugin.Log("No slot data for chestsanity found");
            }

            if (slotData.ContainsKey("coinbagsanity"))
            {
                Coinbagsanity = (long)slotData["coinbagsanity"] == 1;
            }
            else
            {
                Plugin.Log("No slot data for coinbagsanity found");
            }

            if (slotData.ContainsKey("coinsanity"))
            {
                Coinsanity = (long)slotData["coinsanity"] == 1;
            }
            else
            {
                Plugin.Log("No slot data for coinsanity found");
            }

            if (slotData.ContainsKey("cheesesanity"))
            {
                Cheesesanity = (long)slotData["cheesesanity"] == 1;
            }
            else
            {
                Plugin.Log("No slot data for cheesesanity found");
            }

            if (slotData.ContainsKey("hatsanity"))
            {
                Cheesesanity = (long)slotData["hatsanity"] == 1;
            }
            else
            {
                Plugin.Log("No slot data for hatsanity found");
            }

            if (slotData.ContainsKey("shuffle_flip_o_will"))
            {
                ShuffleFlipOWill = (long)slotData["shuffle_flip_o_will"] != 0;
            }
            else
            {
                Plugin.Log("No slot data for shuffle_flip_o_will found");
            }

            if (slotData.ContainsKey("shuffle_glide"))
            {
                ShuffleGlide = (long)slotData["shuffle_glide"] == 1;
            }
            else
            {
                Plugin.Log("No slot data for shuffle_glide found");
            }

            if (slotData.ContainsKey("shuffle_golden_spring"))
            {
                ShuffleGoldenSpring = (long)slotData["shuffle_golden_spring"] == 1;
            }
            else
            {
                Plugin.Log("No slot data for shuffle_golden_spring found");
            }

            if (slotData.ContainsKey("shuffle_golden_propeller"))
            {
                ShuffleGoldenPropeller = (long)slotData["shuffle_golden_propeller"] == 1;
            }
            else
            {
                Plugin.Log("No slot data for shuffle_golden_propeller found");
            }

            if (slotData.ContainsKey("extra_demo_collectables"))
            {
                ExtraDemoCollectables = (long) slotData["extra_demo_collectables"] == 1;
            }
            else
            {
                Plugin.Log("No slot data for extra_demo_collectables found");
            }

            if (slotData.ContainsKey("early_pizza_king"))
            {
                EarlyPizzaKing = (bool) slotData["early_pizza_king"];
            }
            else
            {
                Plugin.Log("No slot data for early_pizza_king found");
            }

            if (slotData.ContainsKey("early_rat"))
            {
                EarlyRat = (bool) slotData["early_rat"];
            }
            else
            {
                Plugin.Log("No slot data for early_rat found");
            }

            if (slotData.ContainsKey("early_doggo"))
            {
                EarlyDoggo = (bool) slotData["early_doggo"];
            }
            else
            {
                Plugin.Log("No slot data for early_doggo found");
            }

            if (slotData.ContainsKey("early_backflip"))
            {
                EarlyBackflip = (bool) slotData["early_backflip"];
            }
            else
            {
                Plugin.Log("No slot data for early_backflip found");
            }

            if (slotData.ContainsKey("early_psycho_taxi"))
            {
                EarlyPsychoTaxi = (bool) slotData["early_psycho_taxi"];
            }
            else
            {
                Plugin.Log("No slot data for early_psycho_taxi found");
            }

            if (slotData.ContainsKey("early_orange_switch"))
            {
                EarlyOrangeSwitch = (bool) slotData["early_orange_switch"];
            }
            else
            {
                Plugin.Log("No slot data for early_orange_switch found");
            }

            if (slotData.ContainsKey("early_golden_spring"))
            {
                EarlyGoldenSpring = (bool) slotData["early_golden_spring"];
            }
            else
            {
                Plugin.Log("No slot data for early_golden_spring found");
            }

            if (slotData.ContainsKey("early_golden_propeller"))
            {
                EarlyGoldenPropeller = (bool) slotData["early_golden_propeller"];
            }
            else
            {
                Plugin.Log("No slot data for early_golden_propeller found");
            }

            if (slotData.ContainsKey("early_morios_password"))
            {
                EarlyMoriosPassword = (bool) slotData["early_morios_password"];
            }
            else
            {
                Plugin.Log("No slot data for early_morios_password found");
            }

            if (slotData.ContainsKey("early_rocket"))
            {
                EarlyRocket = (bool) slotData["early_rocket"];
            }
            else
            {
                Plugin.Log("No slot data for early_rocket found");
            }

            if (slotData.ContainsKey("exclude_spike_bunny"))
            {
                ExcludeSpikeBunny = (bool) slotData["exclude_spike_bunny"];
            }
            else
            {
                Plugin.Log("No slot data for exclude_spike_bunny found");
            }

            if (slotData.ContainsKey("exclude_top_bunny"))
            {
                ExcludeTopBunny = (bool) slotData["exclude_top_bunny"];
            }
            else
            {
                Plugin.Log("No slot data for exclude_top_bunny found");
            }

        }
    }
}
