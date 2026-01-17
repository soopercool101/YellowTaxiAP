using System.Collections.Generic;
using Archipelago.MultiClient.Net.Models;
using YellowTaxiAP.Archipelago;

namespace YellowTaxiAP.Managers
{
    public class APDataManager
    {

        public APDataManager()
        {
            On.DemoDataImporter.DemoDataCheckAndImport += DemoDataImporter_DemoDataCheckAndImport;
            On.Data.LoadGame += Data_LoadGame;
            On.Data.SaveGame += Data_SaveGame;
            On.Data.BunniesCollectedGameGet += Data_BunniesCollectedGameGet;
            On.Data.BunniesGetLevelCollectedNumber_LevelId += Data_BunniesGetLevelCollectedNumber_LevelId;
            On.Data.HatGetCurrentPrefabName += Data_HatGetCurrentPrefabName;
            On.Data.HatGetCurrentKind += Data_HatGetCurrentKind;
            On.Data.HatSetUnlockedState += Data_HatSetUnlockedState;
            On.Data.HatGetUnlockedState += Data_HatGetUnlockedState;
            On.AchievementsMaster.UnlockAchievement_FullRelease += AchievementsMaster_UnlockAchievement_FullRelease;
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
            var b = HatSaveFlags & 0xFF;
            //Plugin.Log($"Hat Current Kind: {b}");
            return (Data.Hat) b;
        }

        public static ulong HatSaveFlags { get; set; } = 0x100;
        private void Data_HatSetUnlockedState(On.Data.orig_HatSetUnlockedState orig, int hatIndex, bool unlockedState)
        {
            if (unlockedState)
            {
                if (!Plugin.SlotData.Hatsanity)
                {
                    HatSaveFlags |= (ulong)1 << (hatIndex + 8);
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
            return ((HatSaveFlags >> (hatIndex + 8)) & 1) != 0;
        }

        public static int TotalBunniesReceived = 0;
        private int Data_BunniesCollectedGameGet(On.Data.orig_BunniesCollectedGameGet orig)
        {
            return TotalBunniesReceived;
        }

        private int Data_BunniesGetLevelCollectedNumber_LevelId(On.Data.orig_BunniesGetLevelCollectedNumber_LevelId orig, Data.LevelId levelId)
        {
            return Data.GetLevel(levelId).bunniesUnlocked;
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
#if DEBUG
            if (!ArchipelagoClient.Authenticated && false)
            {
                Plugin.BepinLogger.LogMessage("AP Not Connected, Default Load");
                orig();
                Data.cutscenePropellerFirstTimePickup[Data.gameDataIndex] = true;
                Data.goldenSpringUnlocked[Data.gameDataIndex] = true;
                Data.activeState_OrangeSwitch[Data.gameDataIndex] = true;
                Data.flipOWillUnlockState[Data.gameDataIndex] = true;
                Data.morioMindPasswordGot[Data.gameDataIndex] = false;
                Data.gononoBombeachDelivered[Data.gameDataIndex] = true;
                return;
            }
#endif
            Data.introCutscenePlayed[Data.gameDataIndex] = true;
            Data.cutscenePropellerFirstTimePickup[Data.gameDataIndex] = true;
            Data.goldenSpringUnlocked[Data.gameDataIndex] = true;
            Data.activeState_OrangeSwitch[Data.gameDataIndex] = true;
            Data.flipOWillUnlockState[Data.gameDataIndex] = true;
            Data.morioMindPasswordGot[Data.gameDataIndex] = false;
            Data.gononoBombeachDelivered[Data.gameDataIndex] = true;
        }

        private void Data_SaveGame(On.Data.orig_SaveGame orig, bool forceSave)
        {
            // Prevent saving while mod is running
            Plugin.BepinLogger.LogMessage("Saving Intercepted");
#if DEBUG
            if (!ArchipelagoClient.Authenticated)
            {
                orig(forceSave);
                return;
            }
#endif
        }
    }
}
