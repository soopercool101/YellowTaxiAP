using I2.Loc;
using UnityEngine;

namespace YellowTaxiAP.Managers
{
    public class APHUDManager
    {
        public APHUDManager()
        {
            On.HudMasterScript.Update += HudMasterScript_Update;
            On.HudMasterScript.UpdateGearsText += HudMasterScript_UpdateGearsText;
        }

        /// <summary>
        /// Reimplementation to remove ? gears handling and 3 gears handling at the start of the game.
        /// TODO: Base on location checks rather than local save.
        /// </summary>
        private void HudMasterScript_UpdateGearsText(On.HudMasterScript.orig_UpdateGearsText orig, HudMasterScript self)
        {
            if (self.gearsText == null || !self.gearsText.gameObject.activeInHierarchy)
                return;
            var flag1 = self.gearsOld != Data.gearsUnlockedNumber[Data.gameDataIndex] || GameplayMaster.instance.timeAttackLevel;
            self.gearsOld = Data.gearsUnlockedNumber[Data.gameDataIndex];
            var num2 = Master.instance.EVENT_MODE ? Master.instance.EVENT_GEARS_NUM : self.areaGearsTotal;
            if (self.gearIconImage.enabled)
                self.gearIconImage.enabled = false;
            if (self.gearsText.tmproText.rectTransform.anchoredPosition.x > 3.0)
                self.gearsText.tmproText.rectTransform.anchoredPosition = new Vector2(2.5f, self.gearsText.tmproText.rectTransform.anchoredPosition.y);
            var text = "";
            self.gearsText.tmproText.characterSpacing = Mathf.Clamp((float)(-(num2 - 10) / 5.0 * 20.0), -20f, 0.0f);
            if (GameplayMaster.instance.timeAttackLevel)
            {
                for (var index = 0; index < Master.instance.levelsGearsMaxNumber[(int)GameplayMaster.instance.levelId]; ++index)
                    text = index >= GameplayMaster.instance.levelCollectedGearsNumber ? text + "<sprite name=\"GearCounterOff\">" : text + "<sprite name=\"GearCounterOn\">";
            }
            if (Master.instance.EVENT_MODE)
            {
                for (var index = 0; index < self.gearsVersionMaxNumber; ++index)
                    text = index >= Data.gearsUnlockedNumber[Data.gameDataIndex] ? text + "<sprite name=\"GearCounterOff\">" : text + "<sprite name=\"GearCounterOn\">";
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
            if (!(self.gearsTextHighlightCoroutine == null & flag1))
                return;
            self.gearsTextHighlightCoroutine = self.StartCoroutine(self.GearsTextHighlightUnlockedCoroutine(text));
        }

        public bool CanShowBunniesOverride(HudMasterScript self)
        {
            var flag1 = GameplayMaster.instance.levelId == Data.LevelId.L16_Rocket || GameplayMaster.instance.levelId == Data.LevelId.L20_PsychoTaxi;
            var flag2 = Data.IsLevelIdHub(GameplayMaster.instance.levelId) && !MapArea.IsPlayerInsideLab();
            return self.CollectibleShouldBeVisible && !flag1 && !flag2 && !HudEndGameScript.instance;
        }

        /// <summary>
        /// Full Reimplementation. Currently just adds consistency to the Gear/Bunny counter indicators.
        /// TODO: Remove local save dependency
        /// </summary>
        private void HudMasterScript_Update(On.HudMasterScript.orig_Update orig, HudMasterScript self)
        {
            if (Master.instance.USE_UI_TOTAL_GEARS && GearAnimationScript.instance && DialogueScript.instance == null && !Master.instance.EVENT_MODE && !GameplayMaster.instance.timeAttackLevel)
            {
                if (!self.gearsTotalHolder.gameObject.activeSelf)
                {
                    self.gearsTotalHolder.gameObject.SetActive(true);
                    var nextLevelCost = Data.ComputeNextLevelCost();
                    var maximumOfVersion = Master.instance.Gears_GetMaximumOfVersion();
                    if (Data.gearsUnlockedNumber[Data.gameDataIndex] < maximumOfVersion)
                        self.gearsTotalText.text = Data.gearsUnlockedNumber[Data.gameDataIndex] + "<size=0.5> </size>/<size=0.5> </size>" + nextLevelCost + "<size=1></size><sprite name=\"ArrowRight\">" + (maximumOfVersion == nextLevelCost ? "<sprite name=\"CompletitionTrophy\">" : "<sprite name=\"Portal\">");
                    else
                        self.gearsTotalText.text = Data.gearsUnlockedNumber[Data.gameDataIndex] + "<sprite name=\"CompletitionTrophy\">";
                }
                if (self.destructionHolder.gameObject.activeSelf)
                    self.destructionHolder.gameObject.SetActive(false);
            }
            else if (self.gearsTotalHolder.gameObject.activeSelf)
                self.gearsTotalHolder.gameObject.SetActive(false);
            if (MapArea.instancePlayerInside && (!self.currentMapAreaScriptableObject || self.currentMapAreaScriptableObject.areaName != MapArea.instancePlayerInside.areaNameKey || self.gearsOld_ForAreaGears != Data.gearsUnlockedNumber[Data.gameDataIndex]) && !GearAnimationScript.instance)
            {
                self.currentMapAreaScriptableObject = MapMaster.GetAreaScriptableObject_ByAreaName(MapArea.instancePlayerInside.areaNameKey);
                self.UpdateAreaGears();
                self.gearsOld_ForAreaGears = Data.gearsUnlockedNumber[Data.gameDataIndex];
                self.UpdateGearsText();
            }
            if (self.bunniesShowTimer > 0.0)
            {
                self.bunniesShowTimer -= Tick.Time;
                if (self.bunniesShowTimer <= 0.0)
                    self.bunniesTotalHolder.gameObject.SetActive(false);
            }
            if (CanShowBunniesOverride(self))
            {
                for (var index = 0; index < self.levelBunnies.Length; ++index)
                {
                    if (!self.levelBunnies[index].gameObject.activeSelf)
                        self.levelBunnies[index].gameObject.SetActive(true);
                    self.bunnyColorAppoggio.r = 0.0f;
                    self.bunnyColorAppoggio.g = 0.0f;
                    self.bunnyColorAppoggio.b = 0.0f;
                    if (Data.BunniesGetLevelCollectedNumber() > index)
                    {
                        self.bunnyColorAppoggio.r = 1f;
                        self.bunnyColorAppoggio.g = 1f;
                        self.bunnyColorAppoggio.b = 1f;
                    }
                    self.levelBunnies[index].color = self.bunnyColorAppoggio;
                    self.levelBunnies[index].rectTransform.sizeDelta = new Vector2(3f, (float)(3.0 + Utility.AngleSin((float)(index * 120.0 + Tick.PassedTimePausable * 180.0)) * 0.25));
                }
            }
            else
            {
                foreach (var bunny in self.levelBunnies)
                {
                    if (bunny.gameObject.activeSelf)
                        bunny.gameObject.SetActive(false);
                }
            }
            if (!Settings.useSpeedrunTimer)
            {
                if (self.speedrunTimer.enabled)
                    self.speedrunTimer.enabled = false;
            }
            else
            {
                if (!self.speedrunTimer.enabled)
                    self.speedrunTimer.enabled = true;
                HudMasterScript.SpeedrunTimerUpdate(self.speedrunTimer);
            }
            if (!Tick.IsGameRunning)
                return;
            self.introAFewMomentsAgoTimer -= Tick.Time;
            self.timePassed += Tick.Time;
            self.fakeFixedUpdateTimer -= Tick.Time;
            var flag1 = false;
            if (self.fakeFixedUpdateTimer <= 0.0)
            {
                self.fakeFixedUpdateTimer += Tick.TimeFixed;
                flag1 = true;
            }
            self.TravelTimerFontSet();
            self.TimerFontSet();
            if (self.extraTimeOffset > 0.0 & flag1)
            {
                self.extraTimeOffset += (float)((0.0 - self.extraTimeOffset) * 0.15000000596046448);
                if (self.extraTimeOffset <= 0.004999999888241291)
                {
                    self.extraTimeOffset = 0.0f;
                    self.extraTimeText.text = "";
                }
                self.extraTimeText.rectTransform.anchoredPosition = new Vector3(13.25f, self.extraTimeYPos - self.extraTimeOffset, 0.0f);
                self.extraTimeText.rectTransform.localScale = Vector3.one * (float)(1.0 + self.extraTimeOffset * 3.0);
                _ = self.extraTimeText.rectTransform.SetZAngle(Utility.AngleSin((float)(self.timePassed * 720.0 * 2.0) * self.extraTimeOffset) * 30f * self.extraTimeOffset);
            }
            if (self.negativeTimeOffset > 0.0 & flag1)
            {
                self.negativeTimeOffset += (float)((0.0 - self.negativeTimeOffset) * 0.15000000596046448);
                if (self.negativeTimeOffset <= 0.004999999888241291)
                {
                    self.negativeTimeOffset = 0.0f;
                    self.negativeTimeText.text = "";
                }
                self.negativeTimeText.rectTransform.anchoredPosition = new Vector3(13.25f, self.negativeTimeYPos - self.negativeTimeOffset, 0.0f);
                self.negativeTimeText.rectTransform.localScale = Vector3.one * (float)(1.0 + self.negativeTimeOffset * 3.0);
                _ = self.negativeTimeText.rectTransform.SetZAngle(Utility.AngleSin((float)(self.timePassed * 720.0 * 2.0) * self.negativeTimeOffset) * 30f * self.negativeTimeOffset);
            }
            if (self.DirectionArrowShouldBeVisible)
            {
                var position1 = PlayerScript.instance.currentTarget.transform.position;
                var position2 = PlayerScript.instance.transform.position;
                var y = PlayerScript.instance.directionToTarget_Relative3D;
                var z = Utility.AxisToEuler3D(position1 - position2).z;
                var flag2 = !PlayerScript.instance.currentTarget.PlayerIsInMyArea() || HatStoreInternalScript.isPlayerInsideStore;
                if (flag2)
                {
                    flag2 = true;
                    y = (float)(Tick.PassedTimePausable * 360.0 % 360.0);
                    if (y > 360.0)
                        y -= 360f;
                    z = 0.0f;
                    if (!self.lostMark.activeSelf)
                        self.lostMark.SetActive(true);
                }
                else if (self.lostMark.activeSelf)
                    self.lostMark.SetActive(false);
                if (PlayerScript.instance.insideTarget == null)
                {
                    if (self.arrivedMark3D.activeSelf)
                        self.arrivedMark3D.SetActive(false);
                    if (!self.leftArrow3D.gameObject.activeSelf)
                        self.leftArrow3D.gameObject.SetActive(true);
                    self.leftArrow3D.localEulerAngles = new Vector3(0.0f, y, z);
                    if (flag2)
                    {
                        if (self.leftArrow3DMeshRend.sharedMaterial != self.leftArrow3dMaterialGray)
                            self.leftArrow3DMeshRend.sharedMaterial = self.leftArrow3dMaterialGray;
                    }
                    else if (self.leftArrow3DMeshRend.sharedMaterial != self.leftArrow3dMaterialRainbow)
                        self.leftArrow3DMeshRend.sharedMaterial = self.leftArrow3dMaterialRainbow;
                }
                else
                {
                    if (!self.arrivedMark3D.activeSelf)
                        self.arrivedMark3D.SetActive(true);
                    if (self.lostMark.activeSelf)
                        self.lostMark.SetActive(false);
                    if (self.leftArrow3D.gameObject.activeSelf)
                        self.leftArrow3D.gameObject.SetActive(false);
                }
            }
            else
            {
                if (self.leftArrow3D.gameObject.activeSelf)
                    self.leftArrow3D.gameObject.SetActive(false);
                if (self.arrivedMark3D.activeSelf)
                    self.arrivedMark3D.SetActive(false);
                if (self.lostMark.activeSelf)
                    self.lostMark.SetActive(false);
            }
            if (!Master.instance.InterpolateMovementEnabled)
                self.UpdateUiFollowingPlayer();
            if (self.CollectibleShouldBeVisible)
            {
                if (!self.coinHudHolder.gameObject.activeSelf)
                    self.coinHudHolder.gameObject.SetActive(true);
                if (!self.gearHudHolder.gameObject.activeSelf)
                    self.gearHudHolder.gameObject.SetActive(true);
                self.destructionShowTimer -= Tick.Time;
                if (self.destructionOld != GameplayMaster.instance.objectsDestroyedInThisLevel)
                    self.destructionShowTimer = 2f;
                if (self.destructionShowTimer > 0.0 && GearAnimationScript.instance == null && !self.bunniesTotalHolder.gameObject.activeSelf)
                {
                    if (!self.destructionHolder.gameObject.activeSelf)
                        self.destructionHolder.gameObject.SetActive(true);
                }
                else if (self.destructionHolder.gameObject.activeSelf)
                    self.destructionHolder.gameObject.SetActive(false);
                if (self.coinsOld != Data.coinsCollected[Data.gameDataIndex] || self.shouldUpdateCoinsText)
                {
                    self.shouldUpdateCoinsText = false;
                    self.coinsOld = Data.coinsCollected[Data.gameDataIndex];
                    self.coinOffset = 0.5f;
                    var text = Data.coinsCollected[Data.gameDataIndex].ToString();
                    if (GameplayMaster.instance.useGameTimer)
                        self.coinFillImage.fillAmount = GameplayMaster.instance.coinsCollectedTimerBonusCounter / 25f;
                    if (GameplayMaster.instance.useGameTimer)
                    {
                        self.coins25TextTimer = 0.25f;
                        var num = GameplayMaster.instance.coinsCollectedTimerBonusCounter % 25;
                        self.coins25Text.text = num == 0 ? "25" : num.ToString();
                        self.coins25Text.enabled = true;
                    }
                    self.coinsText.SetText(text, false);
                }
                if (self.gearsOld != Data.gearsUnlockedNumber[Data.gameDataIndex])
                {
                    self.gearOffset = 1f;
                    self.UpdateGearsText();
                }
                if (self.destructionOld != GameplayMaster.instance.objectsDestroyedInThisLevel)
                {
                    self.destructionOld = GameplayMaster.instance.objectsDestroyedInThisLevel;
                    self.destructionOffset = 1f;
                    var text = (self.destructionOld % 50) + "/50";
                    if (self.destructionOld > 0 && self.destructionOld % 50 == 0)
                    {
                        text = "BONUS";
                        Sound.Play("SoundHudDestructionBonus");
                        if (self.destructionShowTimer > 1.0)
                            self.destructionShowTimer = 1f;
                    }
                    self.destructionText.SetText(text, false);
                }
                if (Data.IsLevelPsychoTaxiMode(GameplayMaster.instance == null ? Data.LevelId.noone : GameplayMaster.instance.levelId))
                {
                    if (!self.clientsText.gameObject.activeSelf)
                        self.clientsText.gameObject.SetActive(true);
                    self.gearOffset = 100f;
                    self.coinOffset = 0.25f;
                    if (self.clientsLeftOld != PersonParent.listClients.Count && PlayerScript.instance.currentTarget == null)
                    {
                        self.clientsLeftOld = PersonParent.listClients.Count;
                        self.clientsText.SetText("<sprite name=\"Guy\">X" + self.clientsLeftOld, false);
                    }
                }
                else if (self.clientsText.gameObject.activeSelf)
                    self.clientsText.gameObject.SetActive(false);
                self.coinOffset = Mathf.Max(0.0f, self.coinOffset - Tick.Time * 2f);
                self.gearOffset = Mathf.Max(0.0f, self.gearOffset - Tick.Time * 2f);
                self.destructionOffset = Mathf.Max(0.0f, self.destructionOffset - Tick.Time * 2f);
                self.coins25TextTimer -= Tick.Time;
                self.coinHudHolder.anchoredPosition = new Vector2(0.0f, self.coinOffset);
                self.gearHudHolder.anchoredPosition = new Vector2(0.0f, self.gearOffset);
                self.destructionHolder.anchoredPosition = new Vector2(0.0f, self.destructionOffset);
                self.coins25Text.rectTransform.anchoredPosition = new Vector2(0.2f, 0.0f + self.coins25TextTimer);
                if (self.coins25TextTimer <= -0.25 && self.coins25Text.enabled)
                    self.coins25Text.enabled = false;
            }
            else
            {
                if (self.coinHudHolder.gameObject.activeSelf)
                    self.coinHudHolder.gameObject.SetActive(false);
                if (self.gearHudHolder.gameObject.activeSelf)
                    self.gearHudHolder.gameObject.SetActive(false);
                if (self.destructionHolder.gameObject.activeSelf)
                    self.destructionHolder.gameObject.SetActive(false);
                if (self.clientsText.gameObject.activeSelf)
                    self.clientsText.gameObject.SetActive(false);
            }
            if (GameplayMaster.instance != null && Data.IsLevelPsychoTaxiMode(GameplayMaster.instance.levelId))
                self.UpdateGearsText();
            if (self.BossLifeShouldBeVisible)
            {
                if (!self.bossHolder.activeSelf)
                    self.bossHolder.SetActive(true);
                var text = "UNDEFINED";
                if (EnemyCarScript.bossInstance != null)
                    text = string.IsNullOrEmpty(EnemyCarScript.bossInstance.bossName) ? "UNDEFINED" : EnemyCarScript.bossInstance.bossName;
                else if (EnemyGenericScript.bossGenericScriptInstance != null)
                    text = string.IsNullOrEmpty(EnemyGenericScript.bossGenericScriptInstance.bossName) ? "UNDEFINED" : EnemyGenericScript.bossGenericScriptInstance.bossName;
                if (self.bossNameText.text != text)
                    self.bossNameText.SetText(text, false);
                var num = 0;
                if (EnemyCarScript.bossInstance != null)
                    num = EnemyCarScript.bossInstance.life;
                else if (EnemyGenericScript.bossGenericScriptInstance != null)
                    num = EnemyGenericScript.bossGenericScriptInstance.life;
                self.bossLifeText.text = num.ToString();
                self.bossLifeBounceScr.bouncesPerSecond = num < 3 ? (num < 2 ? 8f : 4f) : 2f;
            }
            else if (self.bossHolder.activeSelf)
                self.bossHolder.SetActive(false);
            if (self.ClientOnBoardBorderShouldBeVisible)
            {
                if (!self.clientOnBoardHolder.activeSelf)
                    self.clientOnBoardHolder.SetActive(true);
            }
            else if (self.clientOnBoardHolder.activeSelf)
                self.clientOnBoardHolder.SetActive(false);
            self.CheckpointRoutine();
            if (self.timerOld != GameplayMaster.instance.gameTimer)
            {
                self.timerOld = GameplayMaster.instance.gameTimer;
                self.timerYPosOffset = 0.75f;
                self.timerTextSpringIconBounceScript.SetBounce(0.5f);
                self.timerText.font = self.timerOld < 10 ? self.fontOrangeYellowCurrent : self.fontRedYellowCurrent;
            }
            self.timerYPosOffset -= Tick.Time * 4f;
            self.timerYPosOffset = Mathf.Max(0.0f, self.timerYPosOffset);
            var str = GameplayMaster.instance.gameTimer.ToString();
            if (GameplayMaster.instance.timeAttackLevel)
                str = (GameplayMaster.instance.TimeAttack_RecordingDataGetMicroseconds() / 100f).ToString("0.00");
            if (GameplayMaster.instance.gameTimer < 0 && self.timerText.gameObject.activeSelf)
            {
                self.timerText.gameObject.SetActive(false);
                self.SpeedrunTimerUpdatePosition();
            }
            if (GameplayMaster.instance.gameOver)
            {
                if (!HudMasterScript.psychoTaxiJudgementScreenRunning)
                {
                    if (!self.timeOutText.gameObject.activeSelf)
                    {
                        self.timeOutText.text = LocalizationManager.GetTermTranslation("UI_TIME_OUT");
                        self.timeOutText.gameObject.SetActive(true);
                        self.SpeedrunTimerUpdatePosition();
                    }
                }
                else if (self.timeOutText.gameObject.activeSelf)
                    self.timeOutText.gameObject.SetActive(false);
            }
            if (GameplayMaster.instance.gameOver || !GameplayMaster.instance.useGameTimer && !GameplayMaster.instance.timeAttackLevel)
            {
                str = "";
                if (self.timerTextSpringIcon.enabled)
                    self.timerTextSpringIcon.enabled = false;
            }
            self.timerShake = 0.0f;
            if (GameplayMaster.instance.gameTimer < 9)
                self.timerShake += 0.1f;
            if (GameplayMaster.instance.gameTimer < 6)
                self.timerShake += 0.1f;
            if (GameplayMaster.instance.gameTimer < 4)
                self.timerShake += 0.15f;
            if (GameplayMaster.instance.gameTimer < 1)
                self.timerShake += 0.15f;
            self.timerText.text = str;
            var num1 = 0.0f;
            if (self.timerText.text.Length <= 1.0 && !HudMasterScript.introRunning)
                num1 = -1f;
            self.timerText.rectTransform.anchoredPosition = self.initialTimerOffset + new Vector2(num1 + UnityEngine.Random.Range((float)(-(double)self.timerShake / 2.0), self.timerShake / 2f), self.timerYPosOffset + UnityEngine.Random.Range((float)(-(double)self.timerShake / 2.0), self.timerShake / 2f));
            self.timerText.rectTransform.localScale = Vector3.one * ((float)(1.0 + (GameplayMaster.instance.gameTimer < 10 ? 0.34999999403953552 : 0.0) + self.timerYPosOffset * 0.5) + self.timerIntroScale);
            var flag3 = BinocoloScript.IsPlayerPeeking();
            if (flag3 != self.binocleOverlayImage.enabled)
                self.binocleOverlayImage.enabled = flag3;
            self.MusicRadioRoutine();
        }
    }
}
