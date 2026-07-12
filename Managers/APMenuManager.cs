using I2.Loc;
using UnityEngine;
using YellowTaxiAP.Archipelago;

namespace YellowTaxiAP.Managers
{
    public class APMenuManager
    {
        public APMenuManager()
        {
            // MenuV2Script hooks
            On.MenuV2Script.GotoLabConditionGet += GotoLabConditionGet_AP;
            On.MenuV2Script.MenuSelection += MenuV2Script_MenuSelection;
            On.MenuV2Script.Update += MenuV2Script_Update;
            On.MenuV2Element.Awake += MenuV2Element_Awake;
            On.MenuV2WhiteBackground.FixedUpdate += MenuV2WhiteBackground_FixedUpdate;
            On.MenuV2Script.MenuVoicesInit += MenuV2Script_MenuVoicesInit;

            On.MenuV2Script.PauseMenuVoicesStringsGet += MenuV2Script_PauseMenuVoicesStringsGet;

            On.MenuV2PhotoModeController.Update += MenuV2PhotoModeController_Update;

            On.LoadingScreenScript.WelcomeInit += LoadingScreenScript_WelcomeInit;
        }

        /// <summary>
        /// Reimplementation ignoring physics raycast and max distance from player
        /// </summary>
        private void MenuV2PhotoModeController_Update(On.MenuV2PhotoModeController.orig_Update orig, MenuV2PhotoModeController self)
        {
            if (Controls.MenuBackPress(0))
            {
                MenuV2Script.instance.gameObject.SetActive(true);
                Sound.Play_Unpausable("SoundMenuBack");
                Object.Destroy(self.gameObject);
            }
            else
            {
                var zero = Vector2.zero;
                if (Controls.MenuLeftHold(0))
                    --zero.x;
                if (Controls.MenuRightHold(0))
                    ++zero.x;
                if (Controls.MenuUpHold(0))
                    ++zero.y;
                if (Controls.MenuDownHold(0))
                    --zero.y;
                var num1 = 0.0f;
                if (Controls.MenuCameraUp(0))
                    ++num1;
                if (Controls.MenuCameraDown(0))
                    --num1;
                var vector2 = Controls.GameCameraAxis(0);
                if (zero != Vector2.zero || vector2 != Vector2.zero || num1 != 0.0)
                {
                    self.cameraDesiredPosition += (CameraGame.instance.transform.right * zero.x + CameraGame.instance.transform.forward * zero.y + CameraGame.instance.transform.up * num1) * self.cameraMovementSpeed * Tick.Time;
                    self.cameraDesiredEulers += new Vector3(-vector2.y, vector2.x, 0.0f) * self.cameraRotationSpeed * Tick.Time;
                    self.cameraDesiredEulers.x = Mathf.Clamp(self.cameraDesiredEulers.x, -89f, 89f);
                    if (self.cameraDesiredEulers.y < -180.0)
                        self.cameraDesiredEulers.y += 360f;
                    if (self.cameraDesiredEulers.y > 180.0)
                        self.cameraDesiredEulers.y -= 360f;
                }
                CameraGame.instance.SetTarget(null, self.cameraDesiredPosition, self.cameraDesiredEulers.y, self.cameraDesiredEulers.x, 0.0f, 0.0f, 0.0f, 75f);
                CameraGame.instance.SetChagesSpeedMagnitude(0.75f, 0.75f, 0.75f, 1f, 0.1f);
                if (zero != Vector2.zero && !Sound.IsPlaying("SoundMenuPanTick"))
                    Sound.Play_Unpausable("SoundMenuPanTick");
                self.textAlpha -= Tick.Time;
                self.textAlpha = Mathf.Max(self.textAlpha, 0.0f);
                if (zero != Vector2.zero)
                    self.textAlpha = 2f;
                self.textTitle.alpha = self.textAlpha;
            }
        }

        private string[] MenuV2Script_PauseMenuVoicesStringsGet(On.MenuV2Script.orig_PauseMenuVoicesStringsGet orig, MenuV2Script self)
        {
            var strings = orig(self);
            if (GameplayMaster.instance && Data.IsLevelIdHub(GameplayMaster.instance.levelId))
            {
                var grannysString = LocalizationManager.GetTermTranslation("PAUSE_MENU_BACK_TO_GRANNYS_ISLAND");
                var labString = LocalizationManager.GetTermTranslation("PAUSE_MENU_BACK_TO_LAB");
                for (var i = 0; i < strings.Length; i++)
                {
                    if (strings[i].Equals(Plugin.SlotData.StartInLab ? grannysString : labString))
                    {
                        strings[i] = Plugin.SlotData.StartInLab ? labString : grannysString;
                        break;
                    }
                }
            }

            return strings;
        }

        private void MenuV2Script_MenuVoicesInit(On.MenuV2Script.orig_MenuVoicesInit orig, MenuV2Script self)
        {
            orig(self);
            if (GameplayMaster.instance && Data.IsLevelIdHub(GameplayMaster.instance.levelId))
            {
                self.menuSubTitles[15] = LocalizationManager.GetTermTranslation(Plugin.SlotData.StartInLab ? "MENU_SUB_TITLE_HEAD_TO_LAB" : "MENU_SUB_TITLE_HEAD_TO_GRANNYS_ISLAND");
            }
        }

        /// <summary>
        /// Loading screen shows number of gears in level. This functionality is massively broken in rando, so disable.
        /// </summary>
        private void LoadingScreenScript_WelcomeInit(On.LoadingScreenScript.orig_WelcomeInit orig, LoadingScreenScript self)
        {
            orig(self);
            self.welcomeGearImage.enabled = false;
            self.welcomeGearsTextAnimator.SetText(string.Empty, true);
        }

        private MenuV2Element startText;
        private void MenuV2Element_Awake(On.MenuV2Element.orig_Awake orig, MenuV2Element self)
        {
            orig(self);
            if (self.name == "StartText")
            {
                startText = self;
            }
        }

        private void MenuV2Script_Update(On.MenuV2Script.orig_Update orig, MenuV2Script self)
        {
            orig(self);
            if (!self.isPauseMenu)
            {
                UpdateMainMenuText();
            }
        }

        private void UpdateMainMenuText()
        {
            if (MenuV2Script.instance.isPauseMenu || !startText) return;
            if (Plugin.SlotData.FailedValidation)
                startText.textAnimator.SetText($"Unsupported APWorld v{Plugin.SlotData.APWorldVersionString}", false);
            else if (Plugin.ArchipelagoClient.AttemptingConnection)
                startText.textAnimator.SetText("Connecting...", false);
            else if (!ArchipelagoClient.Authenticated)
                startText.textAnimator.SetText(
                    ArchipelagoRenderer.AttemptedConnectionOnce ? "Check connection details" : "Connect to Archipelago",
                    false);
            else
                startText.textAnimator.SetText("Start", false);
        }

        /// <summary>
        /// Reimplementation. Show white screen and low pass filter the music when AP Client isn't authenticated.
        /// Update "Start" text as needed.
        /// </summary>
        private void MenuV2WhiteBackground_FixedUpdate(On.MenuV2WhiteBackground.orig_FixedUpdate orig, MenuV2WhiteBackground self)
        {
            self.passedTime += Tick.TimeFixed;
            if (Sound.IsPlaying("SoundDeleteFileSiren"))
                Sound.Stop("SoundDeleteFileSiren");
            if (!MenuV2Script.IsSelectionDelayedStill())
                self.targetAlpha = ArchipelagoClient.Authenticated ? 0.0f : 1f;
            self.myImage.color = new Color(1f, 1f, 1f, self.myImage.color.a + (float)((self.targetAlpha - (double)self.myImage.color.a) * 0.10000000149011612));
            if (Level.currentScene == 2)
            {
                var a = self.targetAlpha >= 1.0 ? 0.05f : 1f;
                if (!MenuV2Script.instance.isPauseMenu)
                {
                    if (Music.lowPassFilterLevel > (double) a)
                    {
                        Music.SetLowpassFilter(Mathf.Max(a, Music.lowPassFilterLevel - Tick.TimeFixed * 2f));
                    }

                    if (Music.lowPassFilterLevel < (double) a)
                    {
                        Music.SetLowpassFilter(Mathf.Min(a, Music.lowPassFilterLevel + Tick.TimeFixed * 2f));
                    }
                }
            }
            Music.SetVolumeFade(1f, 2f);
        }

        private void MenuV2Script_MenuSelection(On.MenuV2Script.orig_MenuSelection orig, MenuV2Script self)
        {
            if (!self.isPauseMenu)
            {
                if (!ArchipelagoClient.Authenticated || Plugin.SlotData.FailedValidation)
                {
                    Sound.Play_Unpausable("SoundMenuError");
                    if (!ArchipelagoRenderer.AutomaticGamepadInput && !Plugin.SlotData.FailedValidation)
                    {
                        if (!Plugin.EnableSteamKeyboard && ArchipelagoRenderer.AttemptedConnectionOnce)
                        {
                            return;
                        }
                        ArchipelagoRenderer.GamepadInputStep = Plugin.EnableSteamKeyboard ? -1 : 3;
                        ArchipelagoRenderer.AutomaticGamepadInput = true;
                    }
                    return;
                }

                if (self.menuIndex == 0) // Start game immediately
                {
                    // Override behavior to return to starting position in all cases
                    Data.lastHubPortalVisited[Data.gameDataIndex] = -1;
                    if (Plugin.SlotData.StartInLab)
                    {
                        APPortalManager.QueuedSubwarp = WarpIdentifier.LabStart;
                    }
                    self.GotoStoryScene();
                }
                else
                {
                    return;
                }
            }
            else if (self.menuIndex == 15 && self.voiceIndex == 0 && Data.IsLevelIdHub(GameplayMaster.instance.levelId))
            {
                self.done = true;

                // Override behavior to return to starting position in all cases
                Data.lastHubPortalVisited[Data.gameDataIndex] = -1;

                TransictionScript.SpawnOut(TransictionScript.Kind.horizontalFadeFromRight, null, (int)Levels.GetHubIndex());
                Data.LevelId hubLevelId = Data.GetHubLevelId();
                LoadingScreenScript.WelcomeSetup(hubLevelId, Plugin.SlotData.StartInLab ? LocalizationManager.GetTermTranslation("LEVEL_NAME_GRANNY_ISLAND_LAB") : LocalizationManager.GetTermTranslation("Hub")
                    , 0, 0, false);
                CheckpointScript.CheckpointDataReset();
                GameplayMaster.SelfRespawnClear();
                if (Plugin.SlotData.StartInLab)
                {
                    APPortalManager.QueuedSubwarp = WarpIdentifier.LabStart;
                }

                return;
            }

            orig(self);
        }

        /// <summary>
        /// Enable Go To Lab option when in the Lab
        /// </summary>
        private bool GotoLabConditionGet_AP(On.MenuV2Script.orig_GotoLabConditionGet orig, MenuV2Script self)
        {
            return MapArea.IsPlayerInsideLab() || orig(self);
        }
    }
}
