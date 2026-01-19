using YellowTaxiAP.Archipelago;

namespace YellowTaxiAP.Managers
{
    public class APPlayerManager
    {
        public static int BoostLevel => Plugin.SlotData.ShuffleFlipOWill ? BoostItems : 2;
        public static int BoostItems = 0;
        public static int JumpLevel => Plugin.SlotData.ShuffleFlipOWill ? JumpItems : 2;
        public static int JumpItems = 0;
        public static bool SpinAttackEnabled => !Plugin.SlotData.ShuffleFlipOWill || SpinAttackItem;
        public static bool SpinAttackItem = false;
        public static bool GlideEnabled => !Plugin.SlotData.ShuffleGlide || GlideEnabledItem;
        public static bool GlideEnabledItem = false;

        public APPlayerManager()
        {
            // PlayerScript hooks
            On.PlayerScript.Update += PlayerScript_Update_AP;
            On.PlayerScript.FlipOWill_Do += FlipOWill_AP;
            On.PlayerScript.IsFlipOWilling += IsFlipOWillingAP;
            On.PlayerScript.IsFlipOWillingExtraLong += FlipOWillExtraLong_AP;
            On.PlayerScript.FlipOWillAbort += FlipOWillAbort_AP;
            On.PlayerScript.BackFlip += FlipOWillBackFlip_AP;
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
            if (!GlideEnabled)
            {
                self.glidingKeepTimer = float.MaxValue; // Prevent glide from working
            }
            orig(self);
            if (!SpinAttackEnabled)
            {
                DisableFlipOWillSpinAttack(self); // Disables spin effects
            }

            if (BoostLevel < 1) // Disable the homing beacons
            {
                self.targettedFlipPowerup = null;
                self.flipTargetLineRenderer.enabled = false;
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
