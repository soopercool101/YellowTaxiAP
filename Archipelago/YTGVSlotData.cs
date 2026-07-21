using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
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
        public const int LowestSupportedMinorVersion = 5;
        /// <summary>
        /// Highest x.y.# version supported, where y is in the highest supported minor version
        /// Should be up to date with latest APWorld whenever a new version is released.
        /// </summary>
        public const int HighestSupportedMajorVersion = 0;
        /// <summary>
        /// Highest supported minor version in the highest supported major version.
        /// Should be up to date with latest APWorld whenever a new version is released.
        /// </summary>
        public const int HighestSupportedMinorVersion = 6;
        public bool FailedValidation { get; private set; }
        public long APWorldMajorVersion { get; set; }
        public long APWorldMinorVersion { get; set; }
        public long APWorldBuildVersion { get; set; }
        public string APWorldVersionString { get; set; }
        public static bool Loaded { get; private set; }

        public enum GoalType : long
        {
            Bombeach = 0,
            ToslaOffices = 1,
            Moon = 2,
        }
        public GoalType Goal { get; private set; }

        public int TotalGears { get; private set; }
        public int TotalBunnies { get; private set; }
        public int GoalPortalCost { get; private set; }
        public bool RemoveGoalPortalLocations { get; private set; }
        public bool RemovePostGoalPortals { get; private set; }

        public bool DeathLink { get; private set; }
        public int DeathLinkAmnesty { get; set; }
        public bool RingLink { get; private set; }
        public bool TrapLink { get; private set; }
        public bool TrapLinkUsesWhitelist { get; private set; }
        public List<string> TrapLinkWhiteList { get; private set; }
        public int PurchaseRebatePercent { get; private set; }
        public bool ShuffleGelaToni { get; private set; }
        public bool ShufflePizzaKing { get; private set; }
        public bool ShuffleOrangeSwitch { get; private set; }
        public bool ShuffleMoriosPassword { get; private set; }
        public bool ShuffleRocket { get; private set; }
        public bool ShuffleFullGame { get; private set; }
        public DemoPortalMode DemoPortalBehavior { get; private set; }
        public enum DemoPortalMode : long
        {
            Open = -1,
            Default = 2,
            NextFest = 3,
            Influencers = 4,
        }

        public bool ShufflePsychoTaxi { get; private set; }
        public bool ShuffleRat { get; private set; }
        public bool Bunnysanity { get; private set; }
        //public bool Checkpointsanity { get; private set; }
        //public bool Safesanity { get; private set; }
        //public bool Chestsanity { get; private set; }
        //public bool Coinbagsanity { get; private set; }
        //public bool Coinsanity { get; private set; }
        public bool Cheesesanity { get; private set; }

        public enum HatsanityType : long
        {
            Disabled = 0,
            Hatsanity = 1,
            Shopsanity = 2,
        }
        public HatsanityType Hatsanity { get; private set; }
        public bool ExtraDemoCollectables { get; private set; }
        public bool ShuffleFlipOWill { get; private set; }
        public bool ShuffleGlide { get; private set; }
        public bool ShuffleGoldenSpring { get; private set; }
        public bool ShuffleGoldenPropeller { get; private set; }

        public enum PizzaWheelsMode : long
        {
            Disabled = 0,
            Progression = 3,
            Useful = 2,
            Filler = 1,
        }

        public PizzaWheelsMode PizzaWheels { get; private set; }

        // Early location states, and explicitly excluded hubworld items
        public bool EarlyGelaToni { get; private set; }
        public bool EarlyPizzaKing { get; private set; }
        public bool EarlyRat { get; private set; }
        public bool EarlyBackflip { get; private set; }
        public bool EarlyPsychoTaxi { get; private set; }
        public bool EarlyOrangeSwitch { get; private set; }
        public bool EarlyGoldenSpring { get; private set; }
        public bool EarlyGoldenPropeller { get; private set; }
        public bool EarlyMoriosPassword { get; private set; }
        public bool EarlyRocket { get; private set; }
        public bool EarlySewerIsland { get; private set; }
        public bool EarlyPizzaWheels { get; private set; }
        public bool ExcludeSpikeBunny { get; private set; }
        public bool ExcludeTopBunny { get; private set; }

        public bool OpenGrannysIsland { get; private set; }
        public bool LockedMoriosLab { get; private set; }
        public bool StartInLab { get; private set; }
        public bool LockedMoriosWardrobe { get; private set; }

        public string FunnyFaces { get; private set; }

        public bool EasyAlienMosk { get; private set; }

        public enum LevelUnlockCondition : long
        {
            Special = -1,
            Open = 0,
            FullGame = 1,
            Item = 2,
            Exclude = 3,
        }
        public LevelUnlockCondition GymGearsUnlockCondition { get; private set; }
        public LevelUnlockCondition FecalMattersUnlockCondition { get; private set; }
        public LevelUnlockCondition FlushedAwayUnlockCondition { get; private set; }

        public YTGVSlotData()
        {
            // Defaults
        }

        public YTGVSlotData(Dictionary<string, object> slotData)
        {
            Plugin.Log("Getting Slot Data", true);
            if (slotData.TryGetValue("minor_version", out var minorVersion) && slotData.TryGetValue("major_version", out var majorVersion))
            {
                APWorldMajorVersion = (long)majorVersion;
                APWorldMinorVersion = (long)minorVersion;
                var buildVersionString = "X";
                if (slotData.TryGetValue("build_version", out var buildVersion))
                {
                    APWorldBuildVersion = (long)buildVersion;
                    buildVersionString = APWorldBuildVersion.ToString();
                }

                APWorldVersionString = $"{APWorldMajorVersion}.{APWorldMinorVersion}.{buildVersionString}";
                if (APWorldMajorVersion < LowestSupportedMajorVersion || (APWorldMajorVersion == LowestSupportedMajorVersion && APWorldMinorVersion < LowestSupportedMinorVersion))
                {
                    ArchipelagoClient.Authenticated = false;
                    Plugin.Log($"ERROR: Game was generated on version {APWorldVersionString} which is lower than lowest supported APWorld version {LowestSupportedMajorVersion}.{LowestSupportedMinorVersion}.0. Please update your APWorld or use an older version of the mod.", true);
                    FailedValidation = true;
                    return;
                }
                if (APWorldMajorVersion > HighestSupportedMajorVersion || (APWorldMajorVersion == HighestSupportedMajorVersion && APWorldMinorVersion > HighestSupportedMinorVersion))
                {
                    ArchipelagoClient.Authenticated = false;
                    Plugin.Log($"ERROR: Game was generated on version {APWorldVersionString} which is higher than highest supported APWorld version {HighestSupportedMajorVersion}.{HighestSupportedMinorVersion}.X. Please update your game mod.", true);
                    FailedValidation = true;
                    return;
                }
            }
            else
            {
                APWorldVersionString = "Unknown";
                Plugin.Log("No slot data for version found");
                ArchipelagoClient.Authenticated = false;
                Plugin.Log("ERROR: Game was generated on an unknown version, make sure your APWorld and game mod are up-to-date!", true);
                FailedValidation = true;
                return;
            }

            if (slotData.ContainsKey("goal"))
            {
                Goal = (GoalType) slotData["goal"];
            }
            else
            {
                Plugin.Log("No slot data for goal found");
            }

            if (slotData.ContainsKey("total_gears"))
            {
                TotalGears = (int) (long) slotData["total_gears"];
            }
            else
            {
                Plugin.Log("No slot data for total_gears found");
            }

            if (slotData.ContainsKey("total_bunnies"))
            {
                TotalBunnies = (int) (long) slotData["total_bunnies"];
            }
            else
            {
                Plugin.Log("No slot data for total_bunnies found");
            }

            if (slotData.ContainsKey("goal_portal_cost"))
            {
                GoalPortalCost = (int) (long) slotData["goal_portal_cost"];
            }
            else
            {
                Plugin.Log("No slot data for goal_portal_cost found");
            }

            if (slotData.ContainsKey("remove_post_goal_portals"))
            {
                RemovePostGoalPortals = (bool)slotData["remove_post_goal_portals"];
            }
            else
            {
                Plugin.Log("No slot data for remove_post_goal_portals found");
            }

            if (slotData.ContainsKey("remove_goal_portal_locations"))
            {
                RemoveGoalPortalLocations = (bool)slotData["remove_goal_portal_locations"];
            }
            else
            {
                Plugin.Log("No slot data for remove_goal_portal_locations found");
            }

            if (slotData.ContainsKey("death_link"))
            {
                DeathLink = (bool) slotData["death_link"];
            }
            else
            {
                Plugin.Log("No slot data for death_link found");
            }

            if (slotData.ContainsKey("death_link_amnesty"))
            {
                DeathLinkAmnesty = (int)(long) slotData["death_link_amnesty"];
            }
            else
            {
                DeathLinkAmnesty = 1;
                Plugin.Log("No slot data for death_link_amnesty found");
            }

            if (slotData.ContainsKey("ring_link"))
            {
                RingLink = (bool) slotData["ring_link"];
            }
            else
            {
                Plugin.Log("No slot data for ring_link found");
            }

            if (slotData.ContainsKey("trap_link"))
            {
                TrapLink = (bool)slotData["trap_link"];
            }
            else
            {
                Plugin.Log("No slot data for trap_link found");
            }

            if (slotData.ContainsKey("trap_link_whitelist"))
            {
                Plugin.Log("Trap Link Whitelist Loading");
                Plugin.Log(slotData["trap_link_whitelist"].GetType().ToString());
                TrapLinkWhiteList = [];
                TrapLinkUsesWhitelist = true;
                foreach (var trap in (JArray)slotData["trap_link_whitelist"])
                {
                    TrapLinkWhiteList.Add(trap.ToString());
                    // Ensure basic variants are accounted for
                    if (trap.ToString().Equals("Screen Flip Trap"))
                    {
                        TrapLinkWhiteList.Add("Flip Horizontal Trap");
                        TrapLinkWhiteList.Add("Flip Vertical Trap");
                    }
                    else if (trap.ToString().Equals("Slip Trap"))
                    {
                        TrapLinkWhiteList.Add("Banana Trap");
                    }
                }
            }
            else
            {
                TrapLinkUsesWhitelist = false;
                Plugin.Log("No slot data for trap_link_whitelist found");
            }

            if (slotData.ContainsKey("purchase_rebate_percent"))
            {
                PurchaseRebatePercent = (int)(long) slotData["purchase_rebate_percent"];
            }
            else
            {
                Plugin.Log("No slot data for purchase_rebate_percent found");
            }

            if (slotData.ContainsKey("shuffle_gela_toni"))
            {
                ShuffleGelaToni = (bool) slotData["shuffle_gela_toni"];
            }
            else
            {
                Plugin.Log("No slot data for shuffle_gela_toni found");
            }

            if (slotData.ContainsKey("shuffle_pizza_king"))
            {
                ShufflePizzaKing = (bool) slotData["shuffle_pizza_king"];
            }
            else
            {
                Plugin.Log("No slot data for shuffle_pizza_king found");
            }

            if (slotData.ContainsKey("shuffle_orange_switch"))
            {
                ShuffleOrangeSwitch = (bool) slotData["shuffle_orange_switch"];
            }
            else
            {
                Plugin.Log("No slot data for shuffle_orange_switch found");
            }

            if (slotData.ContainsKey("shuffle_morios_password"))
            {
                ShuffleMoriosPassword = (bool) slotData["shuffle_morios_password"];
            }
            else
            {
                Plugin.Log("No slot data for shuffle_morios_password found");
            }

            if (slotData.ContainsKey("shuffle_rocket"))
            {
                ShuffleRocket = (bool) slotData["shuffle_rocket"];
            }
            else
            {
                Plugin.Log("No slot data for shuffle_rocket found");
            }

            if (slotData.ContainsKey("shuffle_full_game"))
            {
                ShuffleFullGame = (bool) slotData["shuffle_full_game"];
            }
            else
            {
                Plugin.Log("No slot data for shuffle_full_game found");
            }

            if (!ShuffleFullGame)
            {
                APAreaStateManager.FullGameUnlocked = true;
            }

            if (slotData.ContainsKey("demo_portal_mode"))
            {
                DemoPortalBehavior = (DemoPortalMode) slotData["demo_portal_mode"];
            }
            else
            {
                Plugin.Log("No slot data for demo_portal_mode found");
            }

            if (slotData.ContainsKey("shuffle_psycho_taxi"))
            {
                ShufflePsychoTaxi = (bool) slotData["shuffle_psycho_taxi"];
            }
            else
            {
                Plugin.Log("No slot data for shuffle_psycho_taxi found");
            }

            if (slotData.ContainsKey("shuffle_rat"))
            {
                ShuffleRat = (bool) slotData["shuffle_rat"];
            }
            else
            {
                Plugin.Log("No slot data for shuffle_rat found");
            }

            if (slotData.ContainsKey("bunnysanity"))
            {
                Bunnysanity = (bool) slotData["bunnysanity"];
            }
            else
            {
                Plugin.Log("No slot data for bunnysanity found");
            }
            if (slotData.ContainsKey("cheesesanity"))
            {
                Cheesesanity = (bool) slotData["cheesesanity"];
            }
            else
            {
                Plugin.Log("No slot data for cheesesanity found");
            }

            if (slotData.ContainsKey("hatsanity"))
            {
                Hatsanity = (HatsanityType)(long) slotData["hatsanity"];
            }
            else
            {
                Plugin.Log("No slot data for hatsanity found");
                Hatsanity = HatsanityType.Disabled;
            }

            if (slotData.ContainsKey("shuffle_flip_o_will"))
            {
                ShuffleFlipOWill = (long) slotData["shuffle_flip_o_will"] != 0;
            }
            else
            {
                Plugin.Log("No slot data for shuffle_flip_o_will found");
            }

            if (slotData.ContainsKey("shuffle_glide"))
            {
                ShuffleGlide = (bool) slotData["shuffle_glide"];
            }
            else
            {
                Plugin.Log("No slot data for shuffle_glide found");
            }

            if (slotData.ContainsKey("shuffle_golden_spring"))
            {
                ShuffleGoldenSpring = (bool) slotData["shuffle_golden_spring"];
            }
            else
            {
                Plugin.Log("No slot data for shuffle_golden_spring found");
            }

            if (slotData.ContainsKey("pizza_wheels"))
            {
                PizzaWheels = (PizzaWheelsMode)(long) slotData["pizza_wheels"];
            }
            else
            {
                Plugin.Log("No slot data for pizza_wheels found");
            }

            if (slotData.ContainsKey("shuffle_golden_propeller"))
            {
                ShuffleGoldenPropeller = (bool) slotData["shuffle_golden_propeller"];
            }
            else
            {
                Plugin.Log("No slot data for shuffle_golden_propeller found");
            }

            if (slotData.ContainsKey("extra_demo_collectables"))
            {
                ExtraDemoCollectables = (bool) slotData["extra_demo_collectables"];
            }
            else
            {
                Plugin.Log("No slot data for extra_demo_collectables found");
            }

            if (slotData.ContainsKey("early_gela_toni"))
            {
                EarlyGelaToni = (bool) slotData["early_gela_toni"];
            }
            else
            {
                Plugin.Log("No slot data for early_gela_toni found");
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


            if (slotData.ContainsKey("open_grannys_island"))
            {
                OpenGrannysIsland = (bool) slotData["open_grannys_island"];
            }
            else
            {
                Plugin.Log("No slot data for open_grannys_island found");
            }

            if (slotData.ContainsKey("locked_morios_lab"))
            {
                LockedMoriosLab = (bool) slotData["locked_morios_lab"];
            }
            else
            {
                Plugin.Log("No slot data for locked_morios_lab found");
            }

            if (slotData.ContainsKey("gym_gears_unlock_condition"))
            {
                GymGearsUnlockCondition = (LevelUnlockCondition) (long) slotData["gym_gears_unlock_condition"];
            }
            else
            {
                GymGearsUnlockCondition = LevelUnlockCondition.Exclude;
                Plugin.Log("No slot data for gym_gears_unlock_condition found");
            }

            if (slotData.ContainsKey("fecal_matters_unlock_condition"))
            {
                FecalMattersUnlockCondition = (LevelUnlockCondition)(long) slotData["fecal_matters_unlock_condition"];
            }
            else
            {
                FecalMattersUnlockCondition = LevelUnlockCondition.Exclude;
                Plugin.Log("No slot data for fecal_matters_unlock_condition found");
            }

            if (slotData.ContainsKey("flushed_away_unlock_condition"))
            {
                FlushedAwayUnlockCondition = (LevelUnlockCondition)(long) slotData["flushed_away_unlock_condition"];
            }
            else
            {
                FlushedAwayUnlockCondition = LevelUnlockCondition.Exclude;
                Plugin.Log("No slot data for flushed_away_unlock_condition found");
            }

            if (slotData.ContainsKey("lab_start"))
            {
                StartInLab = (bool) slotData["lab_start"];
            }
            else
            {
                Plugin.Log("No slot data for lab_start found");
            }

            if (slotData.ContainsKey("locked_morios_wardrobe"))
            {
                LockedMoriosWardrobe = (bool) slotData["locked_morios_wardrobe"];
            }
            else
            {
                Plugin.Log("No slot data for locked_morios_wardrobe found");
            }

            if (slotData.ContainsKey("funny_faces"))
            {
                FunnyFaces = slotData["funny_faces"].ToString();
            }
            else
            {
                Plugin.Log("No slot data for funny_faces found");
            }

            if (slotData.ContainsKey("early_sewer_island"))
            {
                EarlySewerIsland = (bool) slotData["early_sewer_island"];
            }
            else
            {
                Plugin.Log("No slot data for early_sewer_island found");
            }

            if (slotData.ContainsKey("easy_alien_mosk"))
            {
                EasyAlienMosk = (bool)slotData["easy_alien_mosk"];
            }
            else
            {
                Plugin.Log("No slot data for easy_alien_mosk found");
            }

            if (slotData.ContainsKey("locked_time_trials"))
            {
                // We really only need to care if time trials are open or not
                if ((long) slotData["locked_time_trials"] == 0)
                {
                    APAreaStateManager.TimeTrial1Unlocked = APAreaStateManager.TimeTrial2Unlocked =
                        APAreaStateManager.TimeTrial3Unlocked = true;
                }
            }
            else
            {
                // Bugfix, unlock all time trials. Would rather have 20 OoL checks than 20 unobtainable ones
                Plugin.Log("No slot data for locked_time_trials found");
                APAreaStateManager.TimeTrial1Unlocked = APAreaStateManager.TimeTrial2Unlocked =
                    APAreaStateManager.TimeTrial3Unlocked = true;
            }

            if (slotData.ContainsKey("early_pizza_wheels"))
            {
                EarlyPizzaWheels = (bool) slotData["early_pizza_wheels"];
            }
            else
            {
                Plugin.Log("No slot data for early_pizza_wheels found");
            }

            Loaded = true;
        }
    }
}
