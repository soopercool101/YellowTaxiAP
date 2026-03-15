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

            On.LoadingScreenScript.WelcomeInit += LoadingScreenScript_WelcomeInit;
        }

        private string[] MenuV2Script_PauseMenuVoicesStringsGet(On.MenuV2Script.orig_PauseMenuVoicesStringsGet orig, MenuV2Script self)
        {
            var strings = orig(self);
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

            return strings;
        }

        private void MenuV2Script_MenuVoicesInit(On.MenuV2Script.orig_MenuVoicesInit orig, MenuV2Script self)
        {
            orig(self);
            self.menuSubTitles[15] = LocalizationManager.GetTermTranslation(Plugin.SlotData.StartInLab ? "MENU_SUB_TITLE_HEAD_TO_LAB" : "MENU_SUB_TITLE_HEAD_TO_GRANNYS_ISLAND");
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
                startText.textAnimator.SetText("Update Required", false);
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
            else if (self.menuIndex == 15 && self.voiceIndex == 0)
            {
                // Override behavior to return to starting position in all cases
                Data.lastHubPortalVisited[Data.gameDataIndex] = -1;
                if (!Data.IsLevelIdHub(GameplayMaster.instance.levelId))
                {
                    //Data.HubPortalSetLast();
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
                }
                else
                {
                    Tick.Paused = false;
                    Object.Destroy(self.gameObject);
                    PlayerScript.instance.propellerUsesLeft = 0;
                    GameplayMaster.SelfRespawnClear();
                    if (!Plugin.SlotData.StartInLab)
                    {
                        var transitionScript = PortalTransitionScript.Spawn(new Vector3(0, 0, 0), 0);
                        transitionScript.songChange = "SoundtrackHubOutside";
                        transitionScript.backgroundChange = "Background Sea and Sky";
                        transitionScript.desiredWaterState = true;
                        transitionScript.desiredLightState = true;
                        transitionScript.desiredZoneId = 0;
                    }
                    else
                    {
                        var transitionScript = PortalTransitionScript.Spawn(new Vector3(-750f, 10f, 680f), 0);
                        transitionScript.songChange = "SoundtrackHubInside";
                        transitionScript.backgroundChange = "Background Soffitto Laboratorio";
                        transitionScript.desiredWaterState = false;
                        transitionScript.desiredLightState = true;
                        transitionScript.desiredZoneId = 2;
                    }
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
