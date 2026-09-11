using System;
using System.Collections.Generic;
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
            On.MinimapUiNodeScript.Update += MinimapUiNodeScript_Update;
        }

        public static Dictionary<string, string> Titles = new();

        private void MinimapUiNodeScript_Update(On.MinimapUiNodeScript.orig_Update orig, MinimapUiNodeScript self)
        {
            if (MinimapUiNodeScript.instanceYouAreHere != self)
            {
                self.youAreHereAnimTimer += Tick.Time;
                if (self.youAreHereAnimTimer > 2.0)
                    self.youAreHereAnimTimer = 0.0f;
            }

            orig(self);

            if (self.isAreaUnlocked && self.youAreHereAnimTimer < Tick.Time && Titles.ContainsKey(self.myMapAreaScriptableObject.areaName))
            {
                (self.titleText.text, Titles[self.myMapAreaScriptableObject.areaName]) = (Titles[self.myMapAreaScriptableObject.areaName], self.titleText.text);
                self.titleText.rectTransform.offsetMin = new Vector2((self.titleText.text.StartsWith("<sprite name=\"Portal\">") ? 0.1f : 0.5f), self.titleText.rectTransform.offsetMin.y);
            }
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
            Data.LevelId? kaizoLevel = null;
            if (!self.isDiscovered)
            {
                self.isMyLevelUnlocked = Data.GetLevelIfUnlocked(self.myMapAreaScriptableObject.levelId) != null;
                if (self.isMyLevelUnlocked)
                {
                    self.isAreaUnlocked = true;
                    switch (self.myMapAreaScriptableObject.levelId)
                    {
                        case Data.LevelId.L16_Rocket:
                            kaizoLevel = self.myMapAreaScriptableObject.areaName.Substring(21) switch
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
                                self.isAreaUnlocked = Data.BunniesGetLevelCollectedNumber(kaizoLevel.Value) >=
                                                      Data.BunniesGetLevelMaxNumber(kaizoLevel.Value);
                                // Keep kaizo level declared if the level is locked, use that to update the title text later
                                if (self.isAreaUnlocked)
                                {
                                    kaizoLevel = null;
                                }
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
                                case "MAP_AREA_NAME_GRANNY_ISLAND_BONUS_CRASH_TEST" when grannysInaccessible || !APAreaStateManager.FullGameUnlocked:
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

            self.isAreaUnlocked = self.isDiscovered;
            if (self.isDiscovered)
            {
                var portal = APPortalManager.GetRandomizedPortalId(self.myMapAreaScriptableObject.levelId, true);
                if (portal != self.myMapAreaScriptableObject.levelId && portal != Data.LevelId.L11_HubDemo)
                {
                    Titles[self.myMapAreaScriptableObject.areaName] = $"<sprite name=\"Portal\"> {Data.levelDataList[(int)portal].GetName()}";
                }
                if (LocationsByMapArea.LocationsByMapAreaDictionary.ContainsKey(self.myMapAreaScriptableObject
                        .areaName))
                {
                    var locations =
                        LocationsByMapArea.LocationsByMapAreaDictionary[self.myMapAreaScriptableObject.areaName];
                    var checked_locations = locations.Intersect(Plugin.ArchipelagoClient.AllClearedLocations).Count();
                    var all_locations = locations.Intersect(Plugin.ArchipelagoClient.AllLocations).Count();
                    self.gearsText.enableWordWrapping = false;
                    if (all_locations == 0)
                    {
                        self.gearsText.text = string.Empty;
                    }
                    else
                    {
                        var trophy = string.Empty;
                        if (checked_locations == all_locations)
                        {
                            trophy = " <sprite name=\"CompletitionTrophy\">";
                        }
                        self.gearsText.text =
                            $"<size=1.3>{APDialogueManager.SetTextColor($"{checked_locations}/{all_locations}",
                                checked_locations == all_locations
                                    ? APDialogueManager.DialogueColors.OrangeYellow
                                    : APDialogueManager.DialogueColors.Acqua)}{trophy}</size>";
                    }
                }
                else
                {
                    Plugin.BepinLogger.LogWarning($"No location info for {self.myMapAreaScriptableObject.areaName} ({MapMaster.GetAreaNameTranslated(self.myMapAreaScriptableObject)})!");
                }
                if (Plugin.SlotData.ShuffleFlipOWill == YTGVSlotData.MoveRandoType.PerLevel)
                {
                    var level = self.myMapAreaScriptableObject.levelId;

                    var text = $"Boosts: {APPlayerManager.PerLevelBoostItems[level]}";
                    self.gearsText.outlineWidth = 0.1f;
                    self.gearsText.outlineColor = new Color32(0, 0, 0, 0xFF);
                    if (level != Data.LevelId.L10_CrashTestIndustries || Plugin.SlotData.CanPacManJump)
                    {
                        text += $"\nJumps:  {APPlayerManager.PerLevelJumpItems[level]}";
                    }

                    var spacing = string.Empty;
                    if (!string.IsNullOrEmpty(self.gearsText.text))
                    {
                        spacing = "<size=0.5>\n\n</size>";
                    }
                    self.gearsText.text += $"{spacing}<size=1>{APDialogueManager.SetTextColor(text, APDialogueManager.DialogueColors.RedYellow)}</size>";
                }
            }
            else if (kaizoLevel.HasValue)
            {
                self.titleText.text = $"<sprite name=\"OrangeBunny\"> {Data.BunniesGetLevelCollectedNumber(kaizoLevel.Value)}/{Data.BunniesGetLevelMaxNumber(kaizoLevel.Value)}";
            }

            MinimapUiNodeScript.unlockedUndiscoveredList.Remove(self);
        }
    }
}
