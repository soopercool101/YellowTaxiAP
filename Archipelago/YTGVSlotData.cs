using System.Collections.Generic;
using YellowTaxiAP.Managers;

namespace YellowTaxiAP.Archipelago
{
    public class YTGVSlotData
    {
        public const int LowestSupportedVersion = 0;
        public const int HighestSupportedVersion = 0;
        public bool FailedValidation { get; private set; }
        public long BreakingAPWorldVersion { get; set; }

        public enum GoalType : long
        {
            Bombeach = 0,
            ToslaHQ = 1,
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

        public YTGVSlotData()
        {
            // Defaults
        }

        public YTGVSlotData(Dictionary<string, object> slotData)
        {
            if (slotData.ContainsKey("breaking_version"))
            {
                BreakingAPWorldVersion = (long)slotData["breaking_version"];
                if (BreakingAPWorldVersion < LowestSupportedVersion)
                {
                    ArchipelagoClient.Authenticated = false;
                    ArchipelagoConsole.LogMessage($"ERROR: Game was generated with major version {BreakingAPWorldVersion} which is lower than lowest supported APWorld major version {LowestSupportedVersion}. Please update your APWorld or use an older version of the mod.");
                    Plugin.BepinLogger.LogError($"ERROR: Game was generated with major version {BreakingAPWorldVersion} which is lower than lowest supported APWorld major version {LowestSupportedVersion}. Please update your APWorld or use an older version of the mod.");
                    FailedValidation = true;
                    return;
                }
                if (BreakingAPWorldVersion > HighestSupportedVersion)
                {
                    ArchipelagoClient.Authenticated = false;
                    ArchipelagoConsole.LogMessage($"ERROR: Game was generated with major version {BreakingAPWorldVersion} which is higher than highest supported APWorld major version {HighestSupportedVersion}. Please update your game mod.");
                    Plugin.BepinLogger.LogError($"ERROR: Game was generated with major version {BreakingAPWorldVersion} which is higher than highest supported APWorld major version {HighestSupportedVersion}. Please update your game mod.");
                    FailedValidation = true;
                    return;
                }
            }
            else
            {
                Plugin.Log("No slot data for version found");
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
        }
    }
}
