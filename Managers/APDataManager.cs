using YellowTaxiAP.Archipelago;
using YellowTaxiAP.Behaviours;

namespace YellowTaxiAP.Managers
{
    public class APDataManager
    {

        public APDataManager()
        {
            On.DemoDataImporter.DemoDataCheckAndImport += DemoDataImporter_DemoDataCheckAndImport;
            On.Data.GearStateGet += Data_GearStateGet;
            On.Data.GearStateGetAbsolute += Data_GearStateGetAbsolute;
            On.Data.LoadGame += Data_LoadGame;
            On.Data.SaveGame += Data_SaveGame;
            On.Data.BunniesTotalGameGet += Data_BunniesTotalGameGet;
            On.Data.BunniesCollectedGameGet += Data_BunniesCollectedGameGet;
            On.Data.BunniesGetLevelCollectedNumber += Data_BunniesGetLevelCollectedNumber;
            On.Data.BunniesGetLevelCollectedNumber_LevelId += Data_BunniesGetLevelCollectedNumber_LevelId;
            On.Data.BunniesGetLevelMaxNumber_LevelId += Data_BunniesGetLevelMaxNumber_LevelId;
            On.Data.HatGetCurrentPrefabName += Data_HatGetCurrentPrefabName;
            On.Data.HatGetCurrentKind += Data_HatGetCurrentKind;
            On.Data.HatSetUnlockedState += Data_HatSetUnlockedState;
            On.Data.HatGetUnlockedState += Data_HatGetUnlockedState;
            On.Data.CutsceneCarsTransformDisplayedGet += Data_CutsceneCarsTransformDisplayedGet;
            On.AchievementsMaster.UnlockAchievement_FullRelease += AchievementsMaster_UnlockAchievement_FullRelease;
        }

        private int Data_BunniesTotalGameGet(On.Data.orig_BunniesTotalGameGet orig)
        {
            return Plugin.SlotData.ExtraDemoCollectables ? 47 : 45;
        }

        /// <summary>
        /// This is called for bunnies collected in current level for UI purposes, based on locations not items.
        /// </summary>
        private int Data_BunniesGetLevelCollectedNumber(On.Data.orig_BunniesGetLevelCollectedNumber orig)
        {
            if (!Plugin.SlotData.Bunnysanity)
                return Data.BunniesGetLevelCollectedNumber(GameplayMaster.instance.levelId);

            var count = 0;
            for (var i = 0; i < Data.BunniesGetLevelMaxNumber(GameplayMaster.instance.levelId); i++)
            {
                if (Plugin.ArchipelagoClient.AllClearedLocations.Contains(
                        (int) GameplayMaster.instance.levelId * 1_00_00000 + 2_00000 + i))
                {
                    count++;
                }
            }

            return count;

        }

        /// <summary>
        /// Skip certain explanatory cutscenes
        /// </summary>
        private bool Data_CutsceneCarsTransformDisplayedGet(On.Data.orig_CutsceneCarsTransformDisplayedGet orig, int indexOrBitPosition)
        {
            Plugin.Log($"Skipping CutsceneCars: {indexOrBitPosition}");
            return true;
        }

        private bool Data_GearStateGetAbsolute(On.Data.orig_GearStateGetAbsolute orig, int levelIndex, int gearAbsoluteIndex)
        {
            var id = levelIndex * 1_00_00000 + 1_00000 + gearAbsoluteIndex;
            return Plugin.ArchipelagoClient.AllClearedLocations.Contains(id) ||
                   !Plugin.ArchipelagoClient.AllLocations.Contains(id);
        }

        private bool Data_GearStateGet(On.Data.orig_GearStateGet orig, int levelIndex, int gearIntIndex, int gearBitPosition)
        {
            var id = levelIndex * 1_00_00000 + 1_00000 + gearIntIndex * 32 + gearBitPosition;
            return Plugin.ArchipelagoClient.AllClearedLocations.Contains(id) ||
                   !Plugin.ArchipelagoClient.AllLocations.Contains(id);
        }

        private int Data_BunniesGetLevelMaxNumber_LevelId(On.Data.orig_BunniesGetLevelMaxNumber_LevelId orig, Data.LevelId _levelId)
        {
            // Hub has special handling
            if (_levelId == Data.LevelId.Hub)
            {
                var count = 3;
                if (Plugin.SlotData.ExcludeSpikeBunny)
                    count -= 1;

                if (Plugin.SlotData.ExcludeTopBunny)
                    count -= 1;

                if (Plugin.SlotData.ExtraDemoCollectables)
                    count += 2;

                return count;
            }
            // 3 for any main world levels, 0 for others
            return _levelId switch
            {
                Data.LevelId.L1_Bombeach or Data.LevelId.L2_PizzaTime or Data.LevelId.L3_MoriosHome
                    or Data.LevelId.L4_ArcadePanik or Data.LevelId.L5_ToslaOffices or Data.LevelId.L6_Gym
                    or Data.LevelId.L7_PoopWorld or Data.LevelId.L8_Sewers or Data.LevelId.L9_City
                    or Data.LevelId.L10_CrashTestIndustries or Data.LevelId.L12_MoriosMind
                    or Data.LevelId.L13_StarmanCastle or Data.LevelId.L14_ToslaHQ or Data.LevelId.L15_Moon => 3,
                _ => 0
            };
        }

        private void DemoDataImporter_DemoDataCheckAndImport(On.DemoDataImporter.orig_DemoDataCheckAndImport orig, bool reloadOriginalGameDataIndex)
        {
            // Ignore demo data
        }

        private string Data_HatGetCurrentPrefabName(On.Data.orig_HatGetCurrentPrefabName orig)
        {
            return Data.hatPrefabNames[(int) Data.HatGetCurrentKind()];
        }

        private Data.Hat Data_HatGetCurrentKind(On.Data.orig_HatGetCurrentKind orig)
        {
            return APSaveController.MiscSave.CurrentHat;
        }

        private void Data_HatSetUnlockedState(On.Data.orig_HatSetUnlockedState orig, int hatIndex, bool unlockedState)
        {
            if (unlockedState)
            {
                if (!Plugin.SlotData.Hatsanity)
                {
                    APSaveController.HatSave.SetHatUnlocked((Data.Hat)hatIndex);
                }
                else
                {
                    Plugin.ArchipelagoClient.SendLocation(07_00000 + hatIndex);
                }
            }
            else
            {
                Plugin.Log($"Hats shouldn't be taken away! {hatIndex}");
            }
        }

        private bool Data_HatGetUnlockedState(On.Data.orig_HatGetUnlockedState orig, int hatIndex)
        {
            return APSaveController.HatSave.GetHatUnlocked((Data.Hat)hatIndex);
        }

        public static int TotalBunniesReceived = 0;
        private int Data_BunniesCollectedGameGet(On.Data.orig_BunniesCollectedGameGet orig)
        {
            return Plugin.SlotData.Bunnysanity ? TotalBunniesReceived : APSaveController.BunnySave.GetBunnyTotal();
        }

        private int Data_BunniesGetLevelCollectedNumber_LevelId(On.Data.orig_BunniesGetLevelCollectedNumber_LevelId orig, Data.LevelId levelId)
        {
            return Plugin.SlotData.Bunnysanity ? Data.GetLevel(levelId).bunniesUnlocked : APSaveController.BunnySave.GetBunnyCount(levelId);
        }

        private bool AchievementsMaster_UnlockAchievement_FullRelease(On.AchievementsMaster.orig_UnlockAchievement_FullRelease orig, AchievementsMaster.AchievementRelease achievementToUnlock)
        {
            return false;
        }

        private void Data_LoadGame(On.Data.orig_LoadGame orig)
        {
            Plugin.BepinLogger.LogMessage("Loading Intercepted");
            if (Data.levelDataList.Count == 0)
                Data.CreateLevelData();
            Data.InitHatData();
            Data.introCutscenePlayed[Data.gameDataIndex] = true;
            Data.cutscenePropellerFirstTimePickup[Data.gameDataIndex] = true;
            Data.goldenSpringUnlocked[Data.gameDataIndex] = true;
            Data.activeState_OrangeSwitch[Data.gameDataIndex] = true;
            Data.flipOWillUnlockState[Data.gameDataIndex] = true;
            Data.morioMindPasswordGot[Data.gameDataIndex] = false;
            Data.gononoBombeachDelivered[Data.gameDataIndex] = true;
            Data.morioCutsceneToslaHQUnlocked[Data.gameDataIndex] = true;
        }

        private void Data_SaveGame(On.Data.orig_SaveGame orig, bool forceSave)
        {
            // Prevent saving while mod is running
            Plugin.BepinLogger.LogMessage("Saving Intercepted");
#if DEBUG
            if (!ArchipelagoClient.Authenticated)
            {
                orig(forceSave);
            }
#endif
        }
    }
}
