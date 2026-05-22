using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YellowTaxiAP.Archipelago;
using Object = UnityEngine.Object;

namespace YellowTaxiAP.Managers
{
    public class APTVManager
    {
        public APTVManager()
        {
            On.AchievementsTvScript.Awake += AchievementsTvScript_Awake;
            On.AchievementsTvScript.Start += AchievementsTvScript_Start;
            On.AchievementsTvScript.MenuUpdate += AchievementsTvScript_MenuUpdate;
            On.AchievementsTvScript.MenuVisualsUpdate += AchievementsTvScript_MenuVisualsUpdate;
            On.AchievementsTvScript.MenuVisualsCloseCoroutine += AchievementsTvScript_MenuVisualsCloseCoroutine1;
            //On.AchievementsTvScript.MenuVisualsCloseCoroutine += AchievementsTvScript_MenuVisualsCloseCoroutine;
            On.AchievementsTvScript.MenuVisualsInitCoroutine += AchievementsTvScript_MenuVisualsInitCoroutine;
            On.AchievementsTvScript.Update += AchievementsTvScript_Update;
            On.AchievementsTvScript.MenuInit += AchievementsTvScript_MenuInit;

            // Bunny TV should only show when Mosk's Rocket is present
            On.BunnyTv.Start += (_, self) =>
            {
                if (Plugin.SlotData.TotalBunnies == 0)
                {
                    self.gameObject.SetActive(false);
                }
            };
        }

        private void AchievementsTvScript_MenuInit(On.AchievementsTvScript.orig_MenuInit orig, AchievementsTvScript self)
        {
            // Don't do anything here, the variables usually set here are either unused or set in UpdateAPTVInfo()
        }

        private void AchievementsTvScript_Update(On.AchievementsTvScript.orig_Update orig, AchievementsTvScript self)
        {
            orig(self);
            if (self.turnedOn && !self.playerInside)
            {
                self.TurnOff(false);
                self.turnOnDelay = 0.0f;
            }
        }

        private IEnumerator AchievementsTvScript_MenuVisualsCloseCoroutine1(On.AchievementsTvScript.orig_MenuVisualsCloseCoroutine orig, AchievementsTvScript self)
        {
            yield return null;
        }

        private void AchievementsTvScript_MenuVisualsUpdate(On.AchievementsTvScript.orig_MenuVisualsUpdate orig, AchievementsTvScript self)
        {
            orig(self);
            self.visualsHolder.transform.localScale = Vector3.one;
        }

        private void AchievementsTvScript_Start(On.AchievementsTvScript.orig_Start orig, AchievementsTvScript self)
        {
            orig(self);
            self.menuTitle.text = "ARCHIPELAGO";
            self.canBeTurnedOn = true;
            self.turnOnDelay = 0.5f; // deathlink on tv softlocks without some delay, this seems safe from tests
            UpdateAPTVInfo();
        }

        public static TextMeshProUGUI GoalText;
        public static TextMeshProUGUI ItemsReceivedText;
        public static TextMeshProUGUI LocationsCheckedText;
        public static TextMeshProUGUI MovesUnlockText;
        public static Image MovesUnlockImage;
        public static List<string> ImportantItems = [];
        public static TextMeshProUGUI[] ImportantItemsObjects;
        public const int ImportantItemsListMax = 7;

        public static object APTVUpdateLock = new object();
        public static void UpdateAPTVInfo()
        {
            lock (APTVUpdateLock)
            {
                if (!AchievementsTvScript.instance)
                {
                    return;
                }

                if (!GoalText)
                {
                    LocationsCheckedText = Object.Instantiate(AchievementsTvScript.instance.menuTitle,
                        AchievementsTvScript.instance.menuTitle.transform.parent);
                    Plugin.Log($"size: {LocationsCheckedText.fontSize} | max: {LocationsCheckedText.fontSizeMax} | min: {LocationsCheckedText.fontSizeMin} | align: {LocationsCheckedText.alignment} | width: {LocationsCheckedText.rectTransform.sizeDelta.x}");
                    LocationsCheckedText.fontSize = 0.4f;
                    LocationsCheckedText.fontSizeMin = 0.4f;
                    LocationsCheckedText.fontSizeMax = 0.4f;
                    LocationsCheckedText.transform.localPosition -= new Vector3(0, 1.1f, 0);
                    LocationsCheckedText.alignment = TextAlignmentOptions.Left;
                    LocationsCheckedText.rectTransform.sizeDelta =
                        new Vector2(12, LocationsCheckedText.rectTransform.sizeDelta.y);
                    ItemsReceivedText = Object.Instantiate(LocationsCheckedText, LocationsCheckedText.transform.parent);
                    ItemsReceivedText.alignment = TextAlignmentOptions.Right;
                    AchievementsTvScript.instance.achievementsCapsuleToClone.SetActive(true);
                    var gameObject = Object.Instantiate(AchievementsTvScript.instance.achievementsCapsuleToClone, AchievementsTvScript.instance.menuTitle.transform.parent);
                    gameObject.SetActive(true);
                    gameObject.transform.localPosition = new Vector3(0.0f, 2.25f, 0.0f);
                    gameObject.transform.localScale = Vector3.one;
                    gameObject.transform.localEulerAngles = Vector3.zero;
                    //gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(0.1f, 0.1f, 0.1f, 0.5f);
                    GoalText = gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
                    switch (Plugin.SlotData.Goal)
                    {
                        case YTGVSlotData.GoalType.Bombeach:
                            gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = AchievementsMaster.GetAchievementSprite(AchievementsMaster.AchievementRelease.BombossBeated);
                            gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Defeat Bomboss in Bombeach";
                            break;
                        case YTGVSlotData.GoalType.ToslaOffices:
                            gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = AchievementsMaster.GetAchievementSprite(AchievementsMaster.AchievementRelease.ToslaOfficesBeated);
                            gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Defeat Alien Mosk in Tosla Offices";
                            break;
                        case YTGVSlotData.GoalType.Moon:
                            gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = AchievementsMaster.GetAchievementSprite(AchievementsMaster.AchievementRelease.MoonGrandmaDiscovered);
                            gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Defeat Granny on the Moon";
                            break;
                    }
                    gameObject = Object.Instantiate(AchievementsTvScript.instance.achievementsCapsuleToClone, AchievementsTvScript.instance.menuTitle.transform.parent);
                    gameObject.SetActive(true);
                    gameObject.transform.localPosition = new Vector3(0.0f, -0.25f, 0.0f);
                    gameObject.transform.localScale = Vector3.one;
                    gameObject.transform.localEulerAngles = Vector3.zero;
                    gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "MOVES";
                    MovesUnlockImage = gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>();
                    MovesUnlockText = gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
                    var newItemsText = Object.Instantiate(AchievementsTvScript.instance.menuTitle,
                        AchievementsTvScript.instance.menuTitle.transform.parent);
                    newItemsText.text = "RECENT IMPORTANT ITEMS";
                    newItemsText.fontSize = 0.5f;
                    newItemsText.fontSizeMin = 0.5f;
                    newItemsText.fontSizeMax = 0.5f;
                    newItemsText.transform.localPosition -= new Vector3(0, 6.9f, 0);

                    ImportantItemsObjects = new TextMeshProUGUI[ImportantItemsListMax];
                    for (var i = 0; i < ImportantItemsListMax; i++)
                    {
                        ImportantItemsObjects[i] =
                            Object.Instantiate(LocationsCheckedText, LocationsCheckedText.transform.parent);
                        ImportantItemsObjects[i].text = string.Empty;
                        ImportantItemsObjects[i].transform.localPosition = new Vector3(0, -2.6f + -0.52f * i, 0);
                    }

                    AchievementsTvScript.instance.achievementsCapsuleToClone.SetActive(false);
                }

                GoalText.text = $"GOAL ({Data.gearsUnlockedNumber[Data.gameDataIndex]}/{Plugin.SlotData.GoalPortalCost})";
                MovesUnlockText.text = $"Boost: {APPlayerManager.BoostLevel}\tJump: {APPlayerManager.JumpLevel}\nSpin: {(APPlayerManager.SpinAttackEnabled ? "Y" : "N")} \tGlide: {(APPlayerManager.GlideEnabled ? "Y" : "N")}";
                MovesUnlockImage.sprite =
                    (APPlayerManager.BoostLevel == 2 && APPlayerManager.JumpLevel == 2 &&
                     APPlayerManager.SpinAttackEnabled && APPlayerManager.GlideEnabled)
                        ? AchievementsMaster.GetAchievementSprite(AchievementsMaster.AchievementRelease.MoriOTronKill)
                        : (APPlayerManager.BoostLevel == 0 && APPlayerManager.JumpLevel == 0 &&
                           !APPlayerManager.SpinAttackEnabled && !APPlayerManager.GlideEnabled)
                            ? AssetMaster.GetSprite("AchievementLocked")
                            : AchievementsMaster.GetAchievementSprite(AchievementsMaster.AchievementRelease
                                .MorioSecretRoom);
                LocationsCheckedText.text = $"Locations Checked: {Plugin.ArchipelagoClient.AllClearedLocations.Count}/{Plugin.ArchipelagoClient.AllLocations.Count}";
                ItemsReceivedText.text = $"Items Received: {ArchipelagoClient.ServerData.Index}";

                if (AchievementsTvScript.instance.menuIndexCount != ImportantItems.Count)
                {
                    AchievementsTvScript.instance.menuIndexCount = ImportantItems.Count;
                    for(var i = 0; i < ImportantItemsListMax && i < ImportantItems.Count; i++)
                    {
                        ImportantItemsObjects[i].text = $"<color=#FFFFFF>{ImportantItems[i]}</color>";
                    }
                }
            }
        }

        private IEnumerator AchievementsTvScript_MenuVisualsInitCoroutine(On.AchievementsTvScript.orig_MenuVisualsInitCoroutine orig, AchievementsTvScript self)
        {
            UpdateAPTVInfo();
            yield return null;
        }

        private System.Collections.IEnumerator AchievementsTvScript_MenuVisualsCloseCoroutine(On.AchievementsTvScript.orig_MenuVisualsCloseCoroutine orig, AchievementsTvScript self)
        {
            yield return null;
        }

        private void AchievementsTvScript_MenuUpdate(On.AchievementsTvScript.orig_MenuUpdate orig, AchievementsTvScript self)
        {
            if (Controls.MenuPausePress(0) || Controls.MenuBackPress(0))
                self.TurnOff(true);
        }

        private void AchievementsTvScript_Awake(On.AchievementsTvScript.orig_Awake orig, AchievementsTvScript self)
        {
            if (!Plugin.SlotData.StartInLab && GameplayMaster.instance.levelId == Data.LevelId.Hub)
            {
                Plugin.Log($"Moving {self.gameObject.name}");
                self.transform.parent = Object
                    .FindObjectsByType<ZoneMaster>(FindObjectsInactive.Include, FindObjectsSortMode.None)
                    .First(o => o.name.Equals("CENTER ZM   X: 0   Z: 0")).transform;
                foreach (Transform child in self.transform)
                {
                    // Trophy models interfere with other objects in new placement. Disable.
                    if (child.name.StartsWith("ModelObjectTrophy") || child.name.Equals("GameObject"))
                    {
                        child.position = self.cameraPos;
                        child.gameObject.SetActive(false);
                    }
                }
                self.transform.localPosition = new Vector3(74.5f, 20, -23.5f);
                self.transform.Rotate(new Vector3(0, 182, 0));

                // I automatically calculated the new position by using child objects. Baked for performance.
                self.cameraPos = new Vector3(62.4f, 29.6f, -10.5f);
                self.cameraAngle = new Vector3(self.cameraAngle.x, 227, self.cameraAngle.y);
                Plugin.Log($"{self.gameObject.name} finished moving");
            }

            orig(self);
            //self.MenuVisualsInit();
        }
    }
}
