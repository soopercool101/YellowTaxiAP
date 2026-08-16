using System.Collections.Generic;
using YellowTaxiAP.Archipelago;
using YellowTaxiAP.Behaviours;

namespace YellowTaxiAP.Managers
{
    public class APPlayerManager
    {
        public static int BoostLevel => Plugin.CheatsEnabled ? GlobalBoostItems : Plugin.SlotData.ShuffleFlipOWill switch
        {
            YTGVSlotData.MoveRandoType.Disabled => 2,
            YTGVSlotData.MoveRandoType.PerLevel => PerLevelBoostItems[GameplayMaster.instance?.levelId ?? Data.LevelId.Hub],
            _ => GlobalBoostItems
        };
        public static int GlobalBoostItems = 0;

        public static Dictionary<Data.LevelId, int> PerLevelBoostItems = new()
        {
            { Data.LevelId.Hub, 0 },
            { Data.LevelId.L1_Bombeach, 0 },
            { Data.LevelId.L2_PizzaTime, 0 },
            { Data.LevelId.L3_MoriosHome, 0 },
            { Data.LevelId.L4_ArcadePanik, 0 },
            { Data.LevelId.L5_ToslaOffices, 0 },
            { Data.LevelId.L6_Gym, 0 },
            { Data.LevelId.L7_PoopWorld, 0 },
            { Data.LevelId.L8_Sewers, 0 },
            { Data.LevelId.L9_City, 0 },
            { Data.LevelId.L10_CrashTestIndustries, 0 },
            { Data.LevelId.L12_MoriosMind, 0 },
            { Data.LevelId.L13_StarmanCastle, 0 },
            { Data.LevelId.L14_ToslaHQ, 0 },
            { Data.LevelId.L15_Moon, 0 },
            { Data.LevelId.L16_Rocket, 0 },
            { Data.LevelId.L17_TimeAttack01, 0 },
            { Data.LevelId.L18_TimeAttack02, 0 },
            { Data.LevelId.L19_TimeAttack03, 0 },
            { Data.LevelId.L20_PsychoTaxi, 0 },
        };
        public static bool CanPacManBoost => BoostLevel >= 1;
        public static bool PacManBoostItem = false;
        public static int JumpLevel => Plugin.CheatsEnabled ? GlobalJumpItems : Plugin.SlotData.ShuffleFlipOWill switch
        {
            YTGVSlotData.MoveRandoType.Disabled => 2,
            YTGVSlotData.MoveRandoType.PerLevel => PerLevelJumpItems[GameplayMaster.instance?.levelId ?? Data.LevelId.Hub],
            _ => GlobalJumpItems
        };
        public static int GlobalJumpItems = 0;
        public static Dictionary<Data.LevelId, int> PerLevelJumpItems = new()
        {
            { Data.LevelId.Hub, 0 },
            { Data.LevelId.L1_Bombeach, 0 },
            { Data.LevelId.L2_PizzaTime, 0 },
            { Data.LevelId.L3_MoriosHome, 0 },
            { Data.LevelId.L4_ArcadePanik, 0 },
            { Data.LevelId.L5_ToslaOffices, 0 },
            { Data.LevelId.L6_Gym, 0 },
            { Data.LevelId.L7_PoopWorld, 0 },
            { Data.LevelId.L8_Sewers, 0 },
            { Data.LevelId.L9_City, 0 },
            { Data.LevelId.L10_CrashTestIndustries, 0 },
            { Data.LevelId.L12_MoriosMind, 0 },
            { Data.LevelId.L13_StarmanCastle, 0 },
            { Data.LevelId.L14_ToslaHQ, 0 },
            { Data.LevelId.L15_Moon, 0 },
            { Data.LevelId.L16_Rocket, 0 },
            { Data.LevelId.L17_TimeAttack01, 0 },
            { Data.LevelId.L18_TimeAttack02, 0 },
            { Data.LevelId.L19_TimeAttack03, 0 },
            { Data.LevelId.L20_PsychoTaxi, 0 },
        };
        public static bool CanPacManJump => PacManJumpItem;
        public static bool PacManJumpItem = false;
        public static bool SpinAttackEnabled => !Plugin.SlotData.ShuffleSpinAttack || SpinAttackItem;
        public static bool SpinAttackItem = false;
        public static bool GlideEnabled => !Plugin.SlotData.ShuffleGlide || GlideEnabledItem;
        public static bool GlideEnabledItem = false;
        public static bool PizzaWheelsItem = false;
        public static bool PizzaWheelsInitialized = false;

        public static bool PizzaWheelProtection
        {
            get
            {
                if (!Master.cheat_PizzaWheels)
                    return false;

                switch (Plugin.SlotData.PizzaWheels)
                {
                    case YTGVSlotData.PizzaWheelsMode.Useful:
                        return APCollectableManager.GoldenSpringReceived;
                    case YTGVSlotData.PizzaWheelsMode.Progression:
                        return true;
                    case YTGVSlotData.PizzaWheelsMode.Disabled:
                    case YTGVSlotData.PizzaWheelsMode.Filler:
                    default:
                        return false;
                }
            }
        }
        public APPlayerManager()
        {
            // PlayerScript hooks
            On.PlayerScript.Update += PlayerScript_Update_AP;
            On.PlayerScript.FlipOWill_Do += FlipOWill_AP;
            On.PlayerScript.IsFlipOWilling += IsFlipOWillingAP;
            On.PlayerScript.IsFlipOWillingExtraLong += FlipOWillExtraLong_AP;
            On.PlayerScript.FlipOWillAbort += FlipOWillAbort_AP;
            On.PlayerScript.BackFlip += FlipOWillBackFlip_AP;
            On.PlayerScript.PizzaWheelsInit += PlayerScript_PizzaWheelsInit;
            On.PlayerScript.TaxiTextureGlassGet += PlayerScript_TaxiTextureGlassGet;
            On.PlayerScript.TaxiTextureInvincibleGet += PlayerScript_TaxiTextureInvincibleGet;

            On.PlayerDamager.CollideWithPlayer += PlayerDamager_CollideWithPlayer;

            On.GameplayMaster.Die += GameplayMaster_Die;
            // Don't reset pizza wheels!
            On.Master.CheatsOthers_Reset += _ => { };
        }

        private UnityEngine.Texture[] PlayerScript_TaxiTextureInvincibleGet(On.PlayerScript.orig_TaxiTextureInvincibleGet orig, PlayerScript self)
        {
            if (Plugin.SlotData.TaxiSkin >= 10 && !IsCurrentHatSkin())
            {
                return (Plugin.SlotData.TaxiSkin / 10) switch
                {
                    1 => self.taxiInvincibleAnimationBonesTextures,
                    2 => self.taxiInvincibleAnimationGoldenTextures,
                    3 => self.taxiPrototypeSkinInvincibleAnimationTextures,
                    _ => self.taxiInvincibleAnimationTextures
                };
            }
            return orig(self);
        }

        private UnityEngine.Texture[] PlayerScript_TaxiTextureGlassGet(On.PlayerScript.orig_TaxiTextureGlassGet orig, PlayerScript self)
        {
            if (Plugin.SlotData.TaxiSkin > 0 && Plugin.SlotData.TaxiSkin % 10 == 0 && !IsCurrentHatSkin())
            {
                return Plugin.SlotData.TaxiSkin switch
                {
                    10 => self.taxiGlassAnimationBonesTextures,
                    20 => self.taxiGlassAnimationGoldenTextures,
                    30 => self.taxiPrototypeSkinGlassAnimationTextures,
                    _ => self.taxiGlassAnimationTextures
                };
            }
            return orig(self);
        }

        private void PlayerScript_PizzaWheelsInit(On.PlayerScript.orig_PizzaWheelsInit orig, PlayerScript self)
        {
            orig(self);
            PizzaWheelsInitialized = PizzaWheelsItem;
        }

        private void GameplayMaster_Die(On.GameplayMaster.orig_Die orig, GameplayMaster self)
        {
            // Run checks first
            if (self.gameOver || TransictionScript.instance != null ||
                (self.timeAttackLevel && !self.timeAttackRunning) || CutsceneHolderScript.instance != null)
            {
                return;
            }

            if (Plugin.SlotData.DeathLink && !Plugin.DeathLinkInProgress)
            {
                if (++DeathLinkHandler.DeathLinkCount >= Plugin.SlotData.DeathLinkAmnesty)
                {
                    APHUDManager.DeathLinkMessage = "<size=6>TIME OUT</size>\n<size=2>Sending DeathLink!</size>";
                }
                else
                {
                    APHUDManager.DeathLinkMessage = $"<size=6>TIME OUT</size>\n<size=2>Amnesty ({DeathLinkHandler.DeathLinkCount}/{Plugin.SlotData.DeathLinkAmnesty})</size>";
                }
            }

            orig(self);

            if (Plugin.SlotData.DeathLink)
            {
                if (!Plugin.DeathLinkInProgress)
                {
                    ArchipelagoClient.DeathLinkHandler?.SendDeathLink();
                }

                Plugin.DeathLinkInProgress = false;
            }
        }

        private void PlayerDamager_CollideWithPlayer(On.PlayerDamager.orig_CollideWithPlayer orig, PlayerDamager self, PlayerScript scr)
        {
            if (self.instantKill && self.canDamagePlayer && PizzaWheelProtection &&
                self.gameObject.name.Equals("Hurting Collision"))
            {
                return;
            }

            orig(self, scr);
        }

        /// <summary>
        /// Removes the rat knockback interaction when the spin attack is disabled
        /// </summary>
        private bool IsFlipOWillingAP(On.PlayerScript.orig_IsFlipOWilling orig, PlayerScript self)
        {
            var callingMethod = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            if (callingMethod.Equals("_RatPlayerScript::StateBehaviour_FollowPlayer"))
            {
                return SpinAttackEnabled && orig(self);
            }
            return orig(self);
        }

        private void PlayerScript_Update_AP(On.PlayerScript.orig_Update orig, PlayerScript self)
        {
            if (HudMasterScript.introPausePlayer)
            {
                self.myRb.isKinematic = true;
            }
            else
            {
                self.myRb.isKinematic = StunTrap.Instance;
            }

            if (self.UsingPacmanInputs())
            {
                if (!CanPacManBoost && self.flipOWill_FlipTimer > 0 && self.flipOWill_FlipTimer - (double)Tick.Time <= 0.0)
                {
                    self.flipOWill_FlipTimer -= Tick.Time * 10; // Prevents regular boost from working
                    self.flipOWill_CooldownTimer = self.OnGround ? 0.2f : self.flipOWill_CooldownTimerRESET;
                    self.flipOWill_CooldownTimerLastResetValue = self.flipOWill_CooldownTimer;
                }
            }
            else
            {
                if (BoostLevel < 1 && self.flipOWill_FlipTimer > 0 && self.flipOWill_FlipTimer - (double)Tick.Time <= 0.0)
                {
                    self.flipOWill_FlipTimer -= Tick.Time * 10; // Prevents regular boost from working
                    self.flipOWill_CooldownTimer = self.OnGround ? 0.2f : self.flipOWill_CooldownTimerRESET;
                    self.flipOWill_CooldownTimerLastResetValue = self.flipOWill_CooldownTimer;
                }
                if (BoostLevel < 2)
                {
                    self.flipOWillExtraBoostRuined = true; // Prevent superboost from working
                }
            }
            if (!GlideEnabled)
            {
                self.glidingKeepTimer = float.MaxValue; // Prevent glide from working
            }

            orig(self);
            if (CanPacManJump && self.UsingPacmanInputs())
            {
                var _shouldFreezeCar = DialogueScript.instance || PersonParent.chargingClient ||
                                       (bool)(UnityEngine.Object)PersonParent.droppedPerson ||
                                       (MorioDreamMachineScript.instance &&
                                        MorioDreamMachineScript.instance.animationRunning) ||
                                       self.qBlockStopGoFreezingCar || self.ShouldHyperFreeze;
                if (self.CanPerformInputs(_shouldFreezeCar))
                {
                    if ((Controls.GameActio0Press(0) || PlayerScript.modFlipOWillJustPressed) &&
                        Data.flipOWillUnlockState[Data.gameDataIndex])
                    {
                        if (self.justJumpedTimer <= 0.0)
                        {
                            if ((Controls.GameActio0Press(0) || PlayerScript.modFlipOWillJustPressed) &&
                                self.flipOWill_FlipTimer > -0.20000000298023224 &&
                                self.flipOWill_FlipTimer < 0.5 && !self.flipOWilLDoubleInputPress)
                            {
                                self.FlipOWillAbort();
                                if (Sound.IsPlaying("SoundPlayerFlipNegated"))
                                {
                                    Sound.Stop("SoundPlayerFlipNegated");
                                }
                            }
                        }
                    }
                }
            }

            if (!SpinAttackEnabled)
            {
                DisableFlipOWillSpinAttack(self); // Disables spin effects
            }

            if (BoostLevel < 1) // Disable the homing beacons
            {
                self.targettedFlipPowerup = null;
                self.flipTargetLineRenderer.enabled = false;
            }

            if (PizzaWheelsItem != PizzaWheelsInitialized)
            {
                self.PizzaWheelsInit();
            }

            if (Plugin.SlotData.TaxiSkin % 10 != 0 && !self.invincible && !IsCurrentHatSkin())
            {
                self.animationTaxiGlassPlay = false;
                self.taxiMeshRend.sharedMaterial.mainTexture =
                    self.TaxiTextureInvincibleGet()[Plugin.SlotData.TaxiSkin % 10];
            }

            ArchipelagoClient.DeathLinkHandler?.KillPlayer();
        }

        public static bool IsCurrentHatSkin()
        {
            return Data.currentHat[Data.gameDataIndex] switch
            {
                33 or 34 or 49 or 50 => true,
                _ => false
            };
        }

        private void FlipOWillBackFlip_AP(On.PlayerScript.orig_BackFlip orig, PlayerScript self)
        {
            if(JumpLevel > 1)
            {
                orig(self);
            }
            //else if (AP_JumpLevel == 1)
            //{
            //    self.FlipOWillAbort(); // This oddly results in a lower jump than jumping normally. Doesn't really matter, but prob better to just disable
            //}
            else
            {
                FlipOWillJumplessAbort(self);
            }
        }

        private float FlipOWillAbort_AP(On.PlayerScript.orig_FlipOWillAbort orig, PlayerScript self)
        {
            if (JumpLevel > 0 || self.propellerUsesLeft > 0)
            {
                return orig(self);
            }

            FlipOWillJumplessAbort(self);

            return 0;
        }

        public void FlipOWillJumplessAbort(PlayerScript self)
        {
            // QOL: Allow you to abort a Flip O' Will without jumping
            self.freezeBooked = false;
            self.jumpMidairTimer = -1f;
            self.flipOWill_FlipTimer = -1f;
            self.flipOWill_FlipExtraTimer = -1f;
            self.flipOWill_AbortedRecently = 0.75f;
            self.justJumpedTimer = 0.25f;
            self.flipOWill_CooldownTimer = self.OnGround ? 0.2f : self.flipOWill_CooldownTimerRESET;
            self.flipOWill_CooldownTimerLastResetValue = self.flipOWill_CooldownTimer;
        }

        public void FlipOWill_AP(On.PlayerScript.orig_FlipOWill_Do orig, PlayerScript self)
        {
            orig(self);
            if (!SpinAttackEnabled)
            {
                DisableFlipOWillSpinAttack(self);
            }
        }

        /// <summary>
        /// Disables the visual spin trail and offensive "SpinArea" object
        /// </summary>
        public void DisableFlipOWillSpinAttack(PlayerScript self)
        {
            self.flipOWillTrailTransform.gameObject.SetActive(false);
            if (FlipAreaOfEffect.instance)
            {
                FlipAreaOfEffect.instance.GetComponentInChildren<FrameAnimator>().FrameIndex = 0;
                Pool.Destroy(FlipAreaOfEffect.instance.gameObject);
                FlipAreaOfEffect.instance = null;
            }
        }

        /// <summary>
        /// Blocks offensive effects of Flip-O-Will boosting unless Boost-1 and Spin Attack have been received.
        /// Blocks non-offensive effects of Flip-O-Will boosting unless Boost-1 has been received.
        /// </summary>
        private bool FlipOWillExtraLong_AP(On.PlayerScript.orig_IsFlipOWillingExtraLong orig, PlayerScript self)
        {
            var callingMethod = new System.Diagnostics.StackTrace().GetFrame(2).GetMethod().Name;
            if (callingMethod.Contains("OnTrigger"))
            {
                return SpinAttackEnabled && BoostLevel > 0 && orig(self);
            }
            return BoostLevel > 0 && orig(self);
        }
    }
}
