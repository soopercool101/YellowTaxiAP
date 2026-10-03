using System.Linq;
using Febucci.UI;
using I2.Loc;
using UnityEngine;
using YellowTaxiAP.Behaviours;

namespace YellowTaxiAP.Managers
{
    public class APHUDManager
    {
        public APHUDManager()
        {
            On.HudMasterScript.Awake += HudMasterScript_Awake;
            On.HudMasterScript.Update += HudMasterScript_Update;
            On.HudMasterScript.UpdateGearsText += HudMasterScript_UpdateGearsText;
            On.HudMasterScript.UpdateBunniesTotalText += HudMasterScript_UpdateBunniesTotalText;
            On.HudExtraScript.Update += HudExtraScript_Update;
            On.MapMaster.Awake += MapMaster_Awake;
            On.MapMaster.GetAreaGearsTotal += MapMaster_GetAreaGearsTotal;
            On.MapMaster.GetAreaGearsCollected += MapMaster_GetAreaGearsCollected;
            On.MapMaster.GetAreaScriptableObject_ByAreaName += MapMaster_GetAreaScriptableObject_ByAreaName;
        }

        public static GameObject PizzaHudInstance;
        public static void ShowTempPizzaHud()
        {
            if (!GameplayMaster.instance)
                return;

            if (PizzaHudInstance)
            {
                PizzaHudInstance.GetComponent<TimedDestroy>().DestructionTimer = 10;
            }
            else
            {
                PizzaHudInstance = Spawn.Instance("HudExtra_RadioactivePizza", Vector3.zero);
                PizzaHudInstance.AddComponent<TimedDestroy>();
            }
        }

        private void HudExtraScript_Update(On.HudExtraScript.orig_Update orig, HudExtraScript self)
        {
            orig(self);
            if (Plugin.SlotData.Pizzasanity && self.kind == HudExtraScript.HudKind.pizzaHud)
            {
                var text = $"{APCollectableManager.PizzasReceived}/{Plugin.SlotData.PizzasanityCount}";
                if (!self.myTextAnimator.text.Equals(text))
                    self.myTextAnimator.SetText(text, false);
            }
        }

        /// <summary>
        /// Only show x/y bunnies on pickup if bunnysanity is off, otherwise want to save this hud element for receiving bunnies
        /// </summary>
        private void HudMasterScript_UpdateBunniesTotalText(On.HudMasterScript.orig_UpdateBunniesTotalText orig, HudMasterScript self)
        {
            if (!Plugin.SlotData.Bunnysanity)
                orig(self);
        }

        public static void ShowBunnyUIText(Data.LevelId level)
        {
            var hud = HudMasterScript.instance;
            if (!hud)
                return;
            var levelName = level == Data.LevelId.Hub ? LocalizationManager.GetTermTranslation("LEVEL_NAME_GRANNY_ISLAND_LAB") : Data.levelDataList[(int)level].GetName();
            hud.bunniesTotalText.text = $"<size=0.8> </size><size=2>{levelName}: {Data.BunniesGetLevelCollectedNumber(level)}/{Data.BunniesGetLevelMaxNumber(level)}</size>";
            hud.bunniesShowTimer = 3f;
            hud.bunniesTotalHolder.gameObject.SetActive(true);
            hud.destructionHolder.gameObject.SetActive(false);
            hud.destructionShowTimer = 0.0f;
        }

        private void HudMasterScript_Awake(On.HudMasterScript.orig_Awake orig, HudMasterScript self)
        {
            orig(self);
            if (GameplayMaster.instance.levelId != Data.LevelId.Hub)
                return;
            var bunnies = self.levelBunnies.ToList();
            var newBun = Object.Instantiate(bunnies[2], bunnies[2].transform.parent);
            newBun.transform.position -= new Vector3(0.5f, 0.5f, 0);
            bunnies.Add(newBun);
            newBun = Object.Instantiate(bunnies[0], bunnies[0].transform.parent);
            newBun.transform.position += new Vector3(0.65f, -0.42f, 0);
            bunnies.Add(newBun);
            self.levelBunnies = bunnies.ToArray();
        }

        private static bool updatedGears;
        private void MapMaster_Awake(On.MapMaster.orig_Awake orig, MapMaster self)
        {
            orig(self);
            if (updatedGears)
                return;
            foreach (var area in MapMaster.instance.mapAreasList)
            {
                if (area.areaName.Equals("LEVEL_NAME_GRANNY_ISLAND"))
                {
                    if (!area.gearsId.Contains(10004))
                    {
                        area.gearsId.AddRange([10004, 10010, 10020]);
                    }
                }
                else if (area.areaName.Equals("MAP_AREA_NAME_GRANNY_ISLAND_LAB"))
                {
                    if (!area.gearsId.Contains(10019))
                    {
                        area.gearsId.AddRange([10019, 10024]);
                    }
                }
            }

            updatedGears = true;
        }

        private void HudMasterScript_UpdateGearsText(On.HudMasterScript.orig_UpdateGearsText orig, HudMasterScript self)
        {
            if (!self.gearsText || !self.gearsText.gameObject.activeInHierarchy)
                return;
            self.gearsOld = Data.gearsUnlockedNumber[Data.gameDataIndex];
            if (self.gearShowCollectAnimation)
            {
                self.UpdateAreaGears();
                self.gearsOld_ForAreaGears = Data.gearsUnlockedNumber[Data.gameDataIndex];
            }
            if (self.gearIconImage.enabled)
                self.gearIconImage.enabled = false;
            if (self.gearsText.tmproText.rectTransform.anchoredPosition.x > 3.0)
                self.gearsText.tmproText.rectTransform.anchoredPosition = new Vector2(2.5f, self.gearsText.tmproText.rectTransform.anchoredPosition.y);
            var text = string.Empty;
            self.gearsText.tmproText.characterSpacing = Mathf.Clamp((float)(-(self.areaGearsTotal - 10) / 5.0 * 20.0), -20f, 0.0f);
            if (GameplayMaster.instance.timeAttackLevel)
            {
                for (var index = 0; index < Master.instance.levelsGearsMaxNumber[(int)GameplayMaster.instance.levelId]; ++index)
                    text = index >= GameplayMaster.instance.levelCollectedGearsNumber ? text + "<sprite name=\"GearCounterOff\">" : text + "<sprite name=\"GearCounterOn\">";
            }
            else
            {
                for (var index = 0; index < self.areaGearsTotal; ++index)
                    text = !(index < self.areaGearsCollected) ? text + "<sprite name=\"GearCounterOff\">" : text + "<sprite name=\"GearCounterOn\">";
                var flag3 = self.gearsText.tmproText.text.Contains("CompletitionOk");
                if (self.areaGearsCollected >= self.areaGearsTotal)
                {
                    text += "<size=1> </size><sprite name=\"CompletitionOk\">";
                    var flag4 = self.gearsText.tmproText.text.Contains("<sprite name=\"GearCounterOn\">") || self.gearsText.tmproText.text.Contains("<sprite name=\"GearCounterOff\">");
                    if (((flag3 || HudMasterScript.introRunning ? 0 : (self.introAFewMomentsAgoTimer <= 0.0 ? 1 : 0)) & (flag4 ? 1 : 0)) != 0)
                    {
                        var transform = Spawn.FromPool("Pt Star Rnbw - UI All gears in the section", self.gearStarsSpawnPoint.transform.position, Pool.instance.transform).transform;
                        transform.SetParent(self.gearStarsSpawnPoint.transform);
                        transform.localScale = Vector3.one;
                        transform.localEulerAngles = Vector3.zero;
                        transform.localPosition = Vector3.zero;
                    }
                }
            }

            if (!text.Equals(self.gearsText.text))
                self.gearsText.SetText(text, false);
            if (!self.gearShowCollectAnimation)
                return;
            self.gearShowCollectAnimation = false;
            if (self.gearsTextHighlightCoroutine != null)
                return;
            self.gearsTextHighlightCoroutine = self.StartCoroutine(self.GearsTextHighlightUnlockedCoroutine(text));
        }

        private MapAreaScriptableObject MapMaster_GetAreaScriptableObject_ByAreaName(On.MapMaster.orig_GetAreaScriptableObject_ByAreaName orig, string areaNameKey)
        {
            return MapMaster.instance.mapAreasList.FirstOrDefault(mapAreas => mapAreas.areaName == areaNameKey);
        }

        private int MapMaster_GetAreaGearsTotal(On.MapMaster.orig_GetAreaGearsTotal orig, MapAreaScriptableObject mapAreaScriptableObject)
        {
            return mapAreaScriptableObject.gearsId
                .Select(gear => (int) mapAreaScriptableObject.levelId * 10000000 + 100000 + gear)
                .Count(id => Plugin.ArchipelagoClient.AllLocations.Contains(id));
        }

        private int MapMaster_GetAreaGearsCollected(On.MapMaster.orig_GetAreaGearsCollected orig, MapAreaScriptableObject mapAreaScriptableObject)
        {
            return mapAreaScriptableObject.gearsId
                .Select(gear => (int) mapAreaScriptableObject.levelId * 10000000 + 100000 + gear)
                .Count(id => Plugin.ArchipelagoClient.AllClearedLocations.Contains(id));
        }

        public static string DeathLinkMessage = null;

        private void HudMasterScript_Update(On.HudMasterScript.orig_Update orig, HudMasterScript self)
        {
            orig(self);
            var canShowBunnies = self.CollectibleShouldBeVisible &&
                                 GameplayMaster.instance.levelId != Data.LevelId.L16_Rocket &&
                                 !(Data.IsLevelIdHub(GameplayMaster.instance.levelId) &&
                                   !MapArea.IsPlayerInsideLab()) && !HudEndGameScript.instance;
            // Force enable Bunny HUD without the need for save manipulation
            // Always gets disabled earlier in the vanilla hud script so don't need to do anything if bunnies shouldn't be shown currently
            if (canShowBunnies)
            {
                for (var index = 0; index < self.levelBunnies.Length; ++index)
                {
                    if (index >= Data.BunniesGetLevelMaxNumber())
                    {
                        self.levelBunnies[index].gameObject.SetActive(false);
                        continue;
                    }
                    if (!self.levelBunnies[index].gameObject.activeSelf)
                        self.levelBunnies[index].gameObject.SetActive(true);
                    var colorNum = Data.BunniesGetLevelCollectedNumber() > index ? 1 : 0;
                    self.levelBunnies[index].color = new Color(colorNum, colorNum, colorNum);
                    self.levelBunnies[index].rectTransform.sizeDelta = new Vector2(3f, (float)(3.0 + Utility.AngleSin((float)(index * 120.0 + Tick.PassedTimePausable * 180.0)) * 0.25));
                }
            }
            // Only update visual coins alongside the server, makes things cleaner visually
            if (self.coinsOld != APWalletManager.ServerCoins)
            {
                self.coinsText.SetText(APWalletManager.ServerCoins.ToString(), false);
                self.coinsOld = APWalletManager.ServerCoins;
            }
            // Add Deathlink message when applicable
            if (self.timeOutText.gameObject.activeSelf)
            {
                if (GameplayMaster.instance.gameOver && !HudMasterScript.psychoTaxiJudgementScreenRunning)
                {
                    if (!string.IsNullOrEmpty(DeathLinkMessage))
                    {
                        self.timeOutText.SetText(DeathLinkMessage);
                        self.timeOutText.GetComponent<TextAnimatorPlayer>().useTypeWriter = false;
                        DeathLinkMessage = null;
                    }
                }
            }
            else
            {
                //self.timeOutText.GetComponent<TextAnimator>().res
            }
        }
    }
}
