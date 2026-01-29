using System.Linq;
using UnityEngine;
using YellowTaxiAP.Archipelago;

namespace YellowTaxiAP.Managers
{
    public class APHUDManager
    {
        public APHUDManager()
        {
            //On.HudMasterScript.Update += HudMasterScript_Update;
            On.HudMasterScript.Update += HudMasterScript_Update;
            On.HudMasterScript.UpdateGearsText += HudMasterScript_UpdateGearsText;
            On.MapMaster.Awake += MapMaster_Awake;
            On.MapMaster.GetAreaGearsTotal += MapMaster_GetAreaGearsTotal;
            On.MapMaster.GetAreaGearsCollected += MapMaster_GetAreaGearsCollected;
            On.MapMaster.GetAreaScriptableObject_ByAreaName += MapMaster_GetAreaScriptableObject_ByAreaName;
            //On.HudMasterScript.UpdateGearsText += HudMasterScript_UpdateGearsText;
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

        private void HudMasterScript_Update(On.HudMasterScript.orig_Update orig, HudMasterScript self)
        {
            orig(self);
            // Only update visual coins alongside the server, makes things cleaner visually
            if (self.coinsOld != APWalletManager.ServerCoins)
            {
                self.coinsText.SetText(APWalletManager.ServerCoins.ToString(), false);
                self.coinsOld = APWalletManager.ServerCoins;
            }
        }
    }
}
