using System.Linq;
using UnityEngine;
using YellowTaxiAP.Archipelago;
using YellowTaxiAP.Behaviours;

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
            return _id != Data.LevelId.Hub &&
                   !APSaveController.PortalSave.IsLevelPortalUnlocked(APPortalManager.GetRandomizedPortalId(_id))
                ? null
                : (from level in Data.levelDataList where (Data.LevelId)level.levelId == _id select level)
                .FirstOrDefault();
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
                    switch (self.myMapAreaScriptableObject.levelId)
                    {
                        case Data.LevelId.L16_Rocket:
                            var kaizoLevel = self.myMapAreaScriptableObject.areaName.Substring(21) switch
                            {
                                "LAB" => Data.LevelId.Hub,
                                "MORIO_HOME" => Data.LevelId.L3_MoriosHome,
                                "BOMBEACH" => Data.LevelId.L1_Bombeach,
                                "PIZZA_TIME" => Data.LevelId.L2_PizzaTime,
                                "PANIK_ARCADE" => Data.LevelId.L4_ArcadePanik,
                                "TOSLA_OFFICES" => Data.LevelId.L5_ToslaOffices,
                                "GYM" => Data.LevelId.L6_Gym,
                                "POOP_WORLD" => Data.LevelId.L7_PoopWorld,
                                "SEWERS" => Data.LevelId.L8_Sewers,
                                "MAURIZIO_CITY" => Data.LevelId.L9_City,
                                "CRASH_TEST" => Data.LevelId.L10_CrashTestIndustries,
                                "MORIO_MIND" => Data.LevelId.L12_MoriosMind,
                                "RUINED_OBSERVATORY" => Data.LevelId.L13_StarmanCastle,
                                "TOSLA_HQ" => Data.LevelId.L14_ToslaHQ,
                                "MOON" => Data.LevelId.L15_Moon,
                                _ => Data.LevelId.L16_Rocket
                            };

                            if (kaizoLevel != Data.LevelId.L16_Rocket)
                            {
                                self.isAreaUnlocked = Data.BunniesGetLevelCollectedNumber(kaizoLevel) >=
                                                      Data.BunniesGetLevelMaxNumber(kaizoLevel);
                            }

                            break;
                        case Data.LevelId.Hub:
                            var grannysInaccessible = !APAreaStateManager.LabDoorUnlocked && Plugin.SlotData.StartInLab;
                            switch (self.myMapAreaScriptableObject.areaName)
                            {
                                case "LEVEL_NAME_GRANNY_ISLAND" when grannysInaccessible:
                                case "MAP_AREA_NAME_GRANNY_ISLAND_BONUS_LAWYER_ROOM" when grannysInaccessible:
                                case "MAP_AREA_NAME_GRANNY_ISLAND_LAB" when !APAreaStateManager.LabDoorUnlocked && !Plugin.SlotData.StartInLab:
                                case "MAP_AREA_NAME_GRANNY_ISLAND_BONUS_BOMBS" when grannysInaccessible || !APAreaStateManager.GelaToniReceived:
                                case "MAP_AREA_NAME_GRANNY_ISLAND_BONUS_PIZZA" when grannysInaccessible || !APAreaStateManager.PizzaKingReceived:
                                case "MAP_AREA_NAME_GRANNY_ISLAND_BONUS_CRASH_TEST" when grannysInaccessible || !APSwitchManager.OrangeSwitchUnlocked || !APAreaStateManager.FullGameUnlocked:
                                    self.isAreaUnlocked = false;
                                    break;
                            }

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

            if (self.isDiscovered && self.isAreaUnlocked && Plugin.SlotData.ShuffleFlipOWill == YTGVSlotData.MoveRandoType.PerLevel)
            {
                var level = self.myMapAreaScriptableObject.levelId;
                
                var text = $"Boosts: {APPlayerManager.PerLevelBoostItems[level]}";
                self.gearsText.outlineWidth = 0.1f;
                self.gearsText.outlineColor = new Color32(0, 0, 0, 0xFF);
                if (level != Data.LevelId.L10_CrashTestIndustries || Plugin.SlotData.CanPacManJump)
                {
                    text += $"\nJumps:  {APPlayerManager.PerLevelJumpItems[level]}";
                }
                self.gearsText.text += $"<size=0.5>\n\n</size><size=1>{APDialogueManager.SetTextColor(text, APDialogueManager.DialogueColors.RedYellow)}</size>";
            }

            self.isAreaUnlocked = self.isDiscovered;
            MinimapUiNodeScript.unlockedUndiscoveredList.Remove(self);
        }
    }
}
