using YellowTaxiAP.Archipelago;

namespace YellowTaxiAP.Managers
{
    public class APPlayerManager
    {
        public static bool AP_MoveRando => true;
        public static int AP_BoostLevel => AP_MoveRando ? boost_level : 2;
        public static int boost_level = 0;
        public static int AP_JumpLevel => AP_MoveRando ? jump_level : 2;
        public static int jump_level = 0;
        public static bool AP_FlipAttackEnabled => !AP_MoveRando || flip_enabled;
        public static bool flip_enabled = false;
        public static bool AP_GlideEnabled => !AP_MoveRando || glide_enabled;
        public static bool glide_enabled = false;

        public APPlayerManager()
        {
            // PlayerScript hooks
            On.PlayerScript.Awake += PlayerScript_Awake_AP;
            On.PlayerScript.Update += PlayerScript_Update_AP;
            On.PlayerScript.FlipOWill_Do += FlipOWill_AP;
            On.PlayerScript.IsFlipOWilling += IsFlipOWillingAP;
            On.PlayerScript.IsFlipOWillingExtraLong += FlipOWillExtraLong_AP;
            On.PlayerScript.FlipOWillAbort += FlipOWillAbort_AP;
            On.PlayerScript.BackFlip += FlipOWillBackFlip_AP;
        }

        private void PlayerScript_Awake_AP(On.PlayerScript.orig_Awake orig, PlayerScript self)
        {
            Data.flipOWillUnlockState[Data.gameDataIndex] = true;
            orig(self);
        }

        /// <summary>
        /// Removes the rat knockback interaction when the spin attack is disabled
        /// </summary>
        private bool IsFlipOWillingAP(On.PlayerScript.orig_IsFlipOWilling orig, PlayerScript self)
        {
            var callingMethod = (new System.Diagnostics.StackTrace()).GetFrame(2).GetMethod().Name;
            if (callingMethod.Equals("_RatPlayerScript::StateBehaviour_FollowPlayer"))
            {
                return AP_FlipAttackEnabled && orig(self);
            }
            return orig(self);
        }

        private void PlayerScript_Update_AP(On.PlayerScript.orig_Update orig, PlayerScript self)
        {
            if (AP_BoostLevel < 1 && self.flipOWill_FlipTimer > 0 && self.flipOWill_FlipTimer - (double)Tick.Time <= 0.0)
            {
                self.flipOWill_FlipTimer -= Tick.Time * 10; // Prevents regular boost from working
                self.flipOWill_CooldownTimer = self.OnGround ? 0.2f : self.flipOWill_CooldownTimerRESET;
                self.flipOWill_CooldownTimerLastResetValue = self.flipOWill_CooldownTimer;
            }
            if (AP_BoostLevel < 2)
            {
                self.flipOWillExtraBoostRuined = true; // Prevent superboost from working
            }
            if (!AP_GlideEnabled)
            {
                self.glidingKeepTimer = float.MaxValue; // Prevent glide from working
            }
            orig(self);
            if (!AP_FlipAttackEnabled)
            {
                DisableFlipOWillSpinAttack(self); // Disables spin effects
            }

            if (AP_BoostLevel < 1)
            {
                self.targettedFlipPowerup = null;
                self.flipTargetLineRenderer.enabled = false;
            }

            ArchipelagoClient.DeathLinkHandler?.KillPlayer();
        }

        private void FlipOWillBackFlip_AP(On.PlayerScript.orig_BackFlip orig, PlayerScript self)
        {
            if(AP_JumpLevel > 1)
            {
                orig(self);
            }
            //else if (AP_JumpLevel == 1)
            //{
            //    self.FlipOWillAbort(); // This oddly results in a lower jump than jumping normally. Doesn't really matter, but prob better to just disable
            //}
        }

        private float FlipOWillAbort_AP(On.PlayerScript.orig_FlipOWillAbort orig, PlayerScript self)
        {
            if (AP_JumpLevel > 0 || self.propellerUsesLeft > 0)
            {
                return orig(self);
            }

            // Allow you to abort a Flip O' Will without jumping
            self.freezeBooked = false;
            self.jumpMidairTimer = -1f;
            self.flipOWill_FlipTimer = -1f;
            self.flipOWill_FlipExtraTimer = -1f;
            self.flipOWill_AbortedRecently = 0.75f;
            self.justJumpedTimer = 0.25f;
            self.flipOWill_CooldownTimer = self.OnGround ? 0.2f : self.flipOWill_CooldownTimerRESET;
            self.flipOWill_CooldownTimerLastResetValue = self.flipOWill_CooldownTimer;

            return 0;
        }

        public void FlipOWill_AP(On.PlayerScript.orig_FlipOWill_Do orig, PlayerScript self)
        {
            orig(self);
            if (!AP_FlipAttackEnabled)
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
            if (FlipAreaOfEffect.instance != null)
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
            var callingMethod = (new System.Diagnostics.StackTrace()).GetFrame(2).GetMethod().Name;
            if (callingMethod.Contains("OnTrigger"))
            {
                return AP_FlipAttackEnabled && AP_BoostLevel > 0 && orig(self);
            }
            return AP_BoostLevel > 0 && orig(self);
        }
    }
}
