using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using YellowTaxiAP.Archipelago;
using YellowTaxiAP.Behaviours;
using YellowTaxiAP.Helpers;
using static Data;

namespace YellowTaxiAP.Managers
{
    public class APHatManager
    {
        public static bool ReceivedNoHatItem { get; set; }

        public APHatManager()
        {
            //On.HatBuyScript.Update += AP_HatUpdate;
            On.HatBuyScript.Awake += HatBuyScript_Awake;
            On.HatBuyScript.Update += HatBuyScript_Update;
            On.HatBuyScript.Buy += AP_BuyHat;
            On.HatBuyScript.OnTriggerEnter += HatBuyScript_OnTriggerEnter;
            On.HatWBurger.Start += HatWBurger_Start;
            //On.HatScript.RemoveHat += HatScript_RemoveHat;
            On.HatScript.Instantiate += HatScript_Instantiate;
            On.InfluencerPictureScript.Start += InfluencerPictureScript_Start;
            On.InfluencerPictureScript.Refresh += InfluencerPictureScript_Refresh;
        }

        /// <summary>
        /// Don't modify coin material here in order to ensure it's optional
        /// </summary>
        private void InfluencerPictureScript_Start(On.InfluencerPictureScript.orig_Start orig, InfluencerPictureScript self)
        {
            BonusScript componentInParent = self.GetComponentInParent<BonusScript>();
            if ((!Master.influencerHatsAndGraphicsEnabled ? 1 : (!((Object)componentInParent != (Object)null) || componentInParent.myIdentity != BonusScript.Identity.coin ? 0 : (componentInParent.CoinPickedUpGet() ? 1 : 0))) != 0)
            {
                self.enabled = false;
            }
            else
            {
                self.Refresh();
            }
        }

        /// <summary>
        /// Reimplementation, allowing either coin or tv, not requiring both
        /// </summary>
        private void InfluencerPictureScript_Refresh(On.InfluencerPictureScript.orig_Refresh orig, InfluencerPictureScript self)
        {
            if (string.IsNullOrEmpty(Plugin.SlotData.FunnyFaces))
            {
                return;
            }

            Master.influencerHatsAndGraphicsCheatString = Plugin.SlotData.FunnyFaces;
            var pathInfluencersGraphics = Master.pathInfluencersGraphics;
            if (!Directory.Exists(Master.pathInfluencersGraphics))
            {
                Directory.CreateDirectory(Master.pathInfluencersGraphics);
            }
            if (InfluencerPictureScript.cheatString != Master.influencerHatsAndGraphicsCheatString)
            {
                InfluencerPictureScript.cheatString = Master.influencerHatsAndGraphicsCheatString;
                var path = pathInfluencersGraphics + InfluencerPictureScript.cheatString + ".png";
                if (File.Exists(path))
                {
                    try
                    {
                        var data = File.ReadAllBytes(path);
                        if (InfluencerPictureScript.faceTextureUi != null)
                        {
                            Object.Destroy(InfluencerPictureScript.faceTextureUi);
                        }

                        InfluencerPictureScript.faceTextureUi = new Texture2D(2, 2);
                        InfluencerPictureScript.faceTextureUi.LoadImage(data);
                        if (InfluencerPictureScript.faceSpriteUi != null)
                        {
                            Object.Destroy(InfluencerPictureScript.faceSpriteUi);
                        }

                        InfluencerPictureScript.faceSpriteUi = Sprite.Create(InfluencerPictureScript.faceTextureUi,
                            new Rect(0f, 0f, InfluencerPictureScript.faceTextureUi.width,
                                InfluencerPictureScript.faceTextureUi.height), new Vector2(0.5f, 0.5f), 100f);
                    }
                    catch
                    {
                        InfluencerPictureScript.faceSpriteUi = null;
                    }
                }
                else
                {
                    InfluencerPictureScript.faceSpriteUi = null;
                }
                path = pathInfluencersGraphics + InfluencerPictureScript.cheatString + "Coin.png";
                if (File.Exists(path))
                {
                    try
                    {
                        var data = File.ReadAllBytes(path);
                        if (InfluencerPictureScript.faceTextureMesh)
                        {
                            Object.Destroy(InfluencerPictureScript.faceTextureMesh);
                        }

                        InfluencerPictureScript.faceTextureMesh = new Texture2D(2, 2);
                        InfluencerPictureScript.faceTextureMesh.LoadImage(data);
                    }
                    catch
                    {
                        InfluencerPictureScript.faceTextureMesh = null;
                    }
                }
                else
                {
                    InfluencerPictureScript.faceTextureMesh = null;
                }
            }
            if (self.imageToChange && InfluencerPictureScript.faceSpriteUi)
            {
                self.imageToChange.sprite = InfluencerPictureScript.faceSpriteUi;
                if (self.flicker)
                {
                    self.FlickerRoutine();
                }
            }
            if (self.meshToChange && InfluencerPictureScript.faceTextureMesh)
            {
                if (self.meshToChange)
                    self.meshToChange.sharedMaterial = self.modificableMaterial;
                self.meshToChange.sharedMaterial.SetTexture("_MainTex", InfluencerPictureScript.faceTextureMesh);
            }
        }

        private void HatWBurger_Start(On.HatWBurger.orig_Start orig, HatWBurger self)
        {
            // Do nothing
        }

        private void HatBuyScript_Update(On.HatBuyScript.orig_Update orig, HatBuyScript self)
        {
            if (Plugin.SlotData.Hatsanity == YTGVSlotData.HatsanityType.Disabled || self.versioneArmadio)
            {
                orig(self);
                return;
            }
            if (self.disabled)
            {
                if (self.rendererHolder.gameObject.activeSelf)
                    self.rendererHolder.gameObject.SetActive(false);
                return;
            }
            
            if (!self.rendererHolder.gameObject.activeSelf)
                self.rendererHolder.gameObject.SetActive(true);
            self.priceText.text = self.versioneArmadio || self.forceHidePriceText ? "" : (self.price <= 0 ? "FREE" : self.price + "<sprite name=\"Coin\">");
            self.priceText.transform.SetXAngle(CameraGame.instance.transform.GetXAngle());
            self.priceText.transform.SetYAngle(CameraGame.instance.transform.GetYAngle());
            self.priceText.enabled = HatBuyScript.currentBuyingHat != self;
            if (!Tick.IsGameRunning)
            {
                if (!self.myAnimator)
                    return;
                self.myAnimator.speed = 0.0f;
            }
            else
            {
                if (self.myAnimator)
                    self.myAnimator.speed = 1f;
                if (self.disabled)
                    return;
                self.rotationSpd -= Tick.Time;
                self.rotationSpd = Mathf.Max(1f, self.rotationSpd);
                self.hatModelTr.AddLocalYAngle(Tick.Time * 180f * self.rotationSpd);
                self.rendererHolder.SetLocalY((float)(Utility.AngleSin(Tick.PassedTimePausable * 360f) * 0.25 + self.rotationSpd * 0.25));
                self.ring.transform.position = self.transform.position + new Vector3(0.0f, 0.5f, 0.0f);
                if (self.buyDelay <= 0.0 || (PlayerScript.instance.transform.position - self.transform.position).sqrMagnitude <= 7.5)
                    return;
                self.buyDelay -= Tick.Time;
            }
        }

        private void HatBuyScript_OnTriggerEnter(On.HatBuyScript.orig_OnTriggerEnter orig, HatBuyScript self, Collider other)
        {
            if (Plugin.SlotData.Hatsanity == YTGVSlotData.HatsanityType.Disabled || self.versioneArmadio)
            {
                orig(self, other);
                return;
            }

            if (self.disabled || !Tick.IsGameRunning || self.buyDelay > 0.0 || !(other.gameObject == PlayerScript.instance.gameObject) || !(PlayerScript.instance != null))
                return;
#if DEBUG
            var str_id = $"{(int)GameplayMaster.instance.levelId}_{Identifiers.HAT_ID:D2}_{(int)self.myHatKind:D5}";
            DebugLocationHelper.CheckLocation("hat", str_id);
#endif
            var id = 1_00_00000 * (Plugin.SlotData.Hatsanity == YTGVSlotData.HatsanityType.Hatsanity ? 99 : (long)GameplayMaster.instance.levelId) + 7_00000 + (long)self.myHatKind;
            self.disabled = Plugin.ArchipelagoClient.AllClearedLocations.Contains(id) || !Plugin.ArchipelagoClient.AllLocations.Contains(id);
            if (self.disabled)
                return;
            HatBuyScript.currentBuyingHat = self;
            HudMasterScript.instance.StartCoroutine(ShopsanityPurchaseCoroutine(id, self.price));
        }

        private void HatBuyScript_Awake(On.HatBuyScript.orig_Awake orig, HatBuyScript self)
        {
            // Disable
            self.influencerHat = false; // This is used for disabling the hat
            if (self.myHatKind == Hat.Hat52_WishlistBurger) // Burger hat isn't flagged properly as a wardrobe hat
            {
                self.versioneArmadio = true;
            }
            orig(self);

            if (!self.versioneArmadio)
            {
                if (Plugin.SlotData.Hatsanity != YTGVSlotData.HatsanityType.Disabled)
                {
                    var id = 1_00_00000 * (Plugin.SlotData.Hatsanity == YTGVSlotData.HatsanityType.Hatsanity ? 99 : (long)GameplayMaster.instance.levelId) + 7_00000 + (long)self.myHatKind;
                    self.disabled = Plugin.ArchipelagoClient.AllClearedLocations.Contains(id) || !Plugin.ArchipelagoClient.AllLocations.Contains(id);
                }
                if (Plugin.SlotData.Hatsanity == YTGVSlotData.HatsanityType.Shopsanity && !self.disabled)
                {
                    self.hatModelTr.gameObject.SetActive(true);
                    self.hatModelTr.localPosition = new Vector3(0, 1.5f, 0);
                    self.spr?.gameObject.SetActive(false);
                    for (var i = self.hatModelTr.childCount - 1; i >= 0; i--)
                    {
                        ObjectHelper.DestroyImmediateRecursive(self.hatModelTr.GetChild(i));
                    }
                    var questionMark = Object.Instantiate(AssetMaster.GetPrefab("PersonNotificationText")).GetComponentInChildren<TextMeshProUGUI>();
                    questionMark.transform.parent.position = self.hatModelTr.transform.position + new Vector3(0, 1, 0);
                    questionMark.transform.parent.SetParent(self.hatModelTr.transform);
                    // Question mark sprite is offset. Compensate for this.
                    questionMark.transform.localPosition += new Vector3(2f, 0, 0);
                    questionMark.text = "<sprite name=\"QuestionMark\">";
                    self.hatModelTr.gameObject.GetComponent<Renderer>()?.enabled = false;
                }
#if DEBUG
                if (DebugLocationHelper.Enabled) 
                {
                    self.disabled = false;
                }
#endif
            }
        }

        private void HatScript_RemoveHat(On.HatScript.orig_RemoveHat orig, bool removeHatFromData)
        {
            orig(false);
        }

        private HatScript HatScript_Instantiate(On.HatScript.orig_Instantiate orig, Data.Hat hatKind)
        {
            if (Plugin.SlotData.Hatsanity == YTGVSlotData.HatsanityType.Disabled)
            {
                APSaveController.HatSave.SetHatUnlocked(hatKind);
            }
            return orig(hatKind);
        }

        private void AP_BuyHat(On.HatBuyScript.orig_Buy orig, HatBuyScript self)
        {
            if (!self.versioneArmadio)
            {
#if DEBUG
                var id = $"{(int)GameplayMaster.instance.levelId}_{Identifiers.HAT_ID:D2}_{(int)self.myHatKind:D5}";
                DebugLocationHelper.CheckLocation("hat", id);
#endif
            }

            APSaveController.MiscSave.CurrentHat = self.myHatKind;
            orig(self);
            //APSaveController.MiscSave.CurrentHat = (Data.Hat)Data.currentHat[Data.gameDataIndex];
        }

        public IEnumerator ShopsanityPurchaseCoroutine(long id, int cost)
        {
            HudMasterScript.instance.buyingHat = true;
            Tick.FreezeTimer = 0.5f;
            HudMasterScript.instance.hatBuyHolder.SetActive(true);
            var questionString =
                $"{APDialogueManager.SetTextColor($"Buy {APDialogueManager.GetItemText(id, true, false)}?", APDialogueManager.DialogueColors.Acqua)}";
            HudMasterScript.instance.hatBuyText.enableAutoSizing = true;
            HudMasterScript.instance.hatBuyText.fontSizeMin = 1;
            HudMasterScript.instance.hatBuyText.fontSizeMax = 3;
            HudMasterScript.instance.hatBuyText.rectTransform.sizeDelta = new Vector2(39, HudMasterScript.instance.hatBuyText.rectTransform.sizeDelta.y);
            HudMasterScript.instance.hatBuyText.enableWordWrapping = true;
            var costString = cost == 0 ? "Free" : $"-{cost.ToString()}<sprite name=\"Coin\">";
            HudMasterScript.instance.hatBuyText.text = $"{costString}\n<size=2><sprite name=\"AnswerYes\"> </size><size=1><sprite name=\"AnswerNo\"></size>\n{questionString}";
            Sound.Play_Unpausable("SoundMenuPopUp");
            yield return new WaitForSeconds(0.25f);
            var selectionIndex = 0;
            while (!Controls.MenuSelectPress(0))
            {
                if (Controls.MenuLeft(0) || Controls.MenuRight(0))
                {
                    selectionIndex = selectionIndex == 0 ? 1 : 0;
                    Sound.Play_Unpausable("SoundMenuChange");
                    if (selectionIndex == 0)
                        HudMasterScript.instance.hatBuyText.text = $"{costString}\n<size=2><sprite name=\"AnswerYes\"> </size><size=1><sprite name=\"AnswerNo\"></size>\n{questionString}";
                    else
                        HudMasterScript.instance.hatBuyText.text = $"{costString}\n<size=1><sprite name=\"AnswerYes\"></size><size=2> <sprite name=\"AnswerNo\"></size>\n{questionString}";
                }
                Tick.FreezeTimer = 0.1f;
                if (Controls.MenuBackPress(0))
                {
                    selectionIndex = 1;
                    break;
                }
                yield return null;
            }
            if (selectionIndex == 0)
            {
                if (HatBuyScript.currentBuyingHat && APWalletManager.ServerCoins >= cost)
                {
                    Sound.Play_Unpausable("SoundHatBuy");
                    MenuEventLeaderboard.HatsBoughtAdd(1);
                    MenuEventLeaderboard.CoinsSpentAdd(cost);
                    HatBuyScript.currentBuyingHat.disabled = true;
                    Plugin.ArchipelagoClient.SendLocation(id);
                }
                else
                {
                    Sound.Play_Unpausable("SoundMenuError");
                    if (HatBuyScript.currentBuyingHat)
                        HatBuyScript.currentBuyingHat.BuyNot();
                }
            }
            else
            {
                if (HatBuyScript.currentBuyingHat)
                    HatBuyScript.currentBuyingHat.BuyNot();
                Sound.Play_Unpausable("SoundMenuBack");
            }
            HudMasterScript.instance.hatBuyHolder.SetActive(false);
            Tick.Paused = false;
            HudMasterScript.instance.buyingHat = false;
        }
    }
}
