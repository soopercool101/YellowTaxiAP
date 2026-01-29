using System.Linq;

namespace YellowTaxiAP.Managers
{
    public class APMinimapManager
    {
        public APMinimapManager()
        {
            On.Data.GetLevelIfUnlocked += Data_GetLevelIfUnlocked;
            On.MinimapUiNodeScript.OnEnable += MinimapUiNodeScript_OnEnable;
        }

        private Data.LevelData Data_GetLevelIfUnlocked(On.Data.orig_GetLevelIfUnlocked orig, Data.LevelId _id)
        {
            switch (_id)
            {
                case Data.LevelId.L7_PoopWorld when !APAreaStateManager.DoggoReceived:
                case Data.LevelId.L8_Sewers when !APSwitchManager.OrangeSwitchUnlocked:
                case Data.LevelId.L12_MoriosMind when !Data.morioMindDreamMachineUsedOnce[Data.gameDataIndex]:
                case Data.LevelId.L13_StarmanCastle when !APAreaStateManager.MindPasswordReceived:
                case Data.LevelId.L15_Moon when Data.gearsUnlockedNumber[Data.gameDataIndex] < 130:
                case Data.LevelId.L16_Rocket when !APAreaStateManager.RocketEnabled:
                    return null;
            }

            if (Data.IsLevelTimeAttack(_id) || Data.IsLevelPsychoTaxiMode(_id))
                return null;

            return (from level in Data.levelDataList
                where (Data.LevelId) level.levelId == _id
                select level.levelCost > Data.gearsUnlockedNumber[Data.gameDataIndex] ? null : level).FirstOrDefault();
        }

        /// <summary>
        /// Always enable minimap discovery of unlocked levels.
        ///
        /// TODO: Store which levels have been visited in server for entrance rando tracking
        /// </summary>
        private void MinimapUiNodeScript_OnEnable(On.MinimapUiNodeScript.orig_OnEnable orig, MinimapUiNodeScript self)
        {
            if (!self.isDiscovered)
            {
                self.isMyLevelUnlocked = Data.GetLevelIfUnlocked(self.myMapAreaScriptableObject.levelId) != null;
                if (self.isMyLevelUnlocked)
                {
                    self.isAreaUnlocked = true;
                    if (!APAreaStateManager.FullGameUnlocked &&
                        self.myMapAreaScriptableObject.levelId is Data.LevelId.L2_PizzaTime
                            or Data.LevelId.L4_ArcadePanik or Data.LevelId.L5_ToslaOffices or Data.LevelId.L8_Sewers
                            or Data.LevelId.L9_City or Data.LevelId.L10_CrashTestIndustries
                            or Data.LevelId.L12_MoriosMind or Data.LevelId.L13_StarmanCastle or Data.LevelId.L14_ToslaHQ
                            or Data.LevelId.L15_Moon)
                    {
                        self.isAreaUnlocked = false;
                    }
                    switch (self.myMapAreaScriptableObject.areaName)
                    {
                        case "MAP_AREA_NAME_GRANNY_ISLAND_BONUS_BOMBS" when !APAreaStateManager.GelaToniReceived:
                        case "MAP_AREA_NAME_GRANNY_ISLAND_BONUS_PIZZA" when !APAreaStateManager.PizzaKingReceived:
                        case "MAP_AREA_NAME_GRANNY_ISLAND_BONUS_CRASH_TEST" when (!APSwitchManager.OrangeSwitchUnlocked || !APAreaStateManager.FullGameUnlocked):
                            self.isAreaUnlocked = false;
                            break;
                    }
                }
                else
                    self.isAreaUnlocked = false;

                if (self.isAreaUnlocked)
                {
                    self.isDiscovered = Data.discoveredMapAreas[Data.gameDataIndex]
                        .Contains(self.myMapAreaScriptableObject.areaName);
                    if (!self.isDiscovered)
                    {
                        Data.discoveredMapAreas[Data.gameDataIndex] += self.myMapAreaScriptableObject.areaName;
                    }
                }
            }

            orig(self);

            self.isAreaUnlocked = self.isDiscovered;
            MinimapUiNodeScript.unlockedUndiscoveredList.Remove(self);
        }
    }
}
