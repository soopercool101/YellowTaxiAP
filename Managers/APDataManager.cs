using YellowTaxiAP.Archipelago;

namespace YellowTaxiAP.Managers
{
    public class APDataManager
    {
        public APDataManager()
        {
            On.Data.LoadGame += Data_LoadGame;
            On.Data.SaveGame += Data_SaveGame;
            On.AchievementsMaster.UnlockAchievement_FullRelease += AchievementsMaster_UnlockAchievement_FullRelease; 
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
#if DEBUG
            if (!ArchipelagoClient.Authenticated)
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
            Data.cutscenePropellerFirstTimePickup[Data.gameDataIndex] = true;
            Data.goldenSpringUnlocked[Data.gameDataIndex] = true;
            Data.activeState_OrangeSwitch[Data.gameDataIndex] = true;
            Data.flipOWillUnlockState[Data.gameDataIndex] = true;
            Data.morioMindPasswordGot[Data.gameDataIndex] = false;
            Data.gononoBombeachDelivered[Data.gameDataIndex] = true;
            //Data.gearsUnlockedNumber[Data.gameDataIndex] = Plugin.ArchipelagoClient.GetItemCount(1);
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
