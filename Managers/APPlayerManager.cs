using System;
using YellowTaxiAP.Archipelago;

namespace YellowTaxiAP.Managers
{
    public class APPlayerManager
    {
        public static int BoostLevel => Plugin.SlotData.ShuffleFlipOWill ? BoostItems : 2;
        public static int BoostItems = 0;
        public static bool CanPacManBoost => BoostLevel >= 1;
        public static bool PacManBoostItem = false;
        public static int JumpLevel => Plugin.SlotData.ShuffleFlipOWill ? JumpItems : 2;
        public static int JumpItems = 0;
        public static bool CanPacManJump => PacManJumpItem;
        public static bool PacManJumpItem = false;
        public static bool SpinAttackEnabled => !Plugin.SlotData.ShuffleFlipOWill || SpinAttackItem;
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

            On.PlayerDamager.CollideWithPlayer += PlayerDamager_CollideWithPlayer;

            On.GameplayMaster.Die += GameplayMaster_Die;
            // Don't reset pizza wheels!
            On.Master.CheatsOthers_Reset += _ => { };
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
                !self.gameObject.name.Equals("ModelObjectSega", StringComparison.OrdinalIgnoreCase))
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

            ArchipelagoClient.DeathLinkHandler?.KillPlayer();
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
