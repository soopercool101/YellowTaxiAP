using UnityEngine;

namespace YellowTaxiAP.Managers
{
    public class PlayerManager
    {
        public bool AP_MoveRando => true;
        public int AP_BoostLevel => AP_MoveRando ? boost_level : 2;
        private int boost_level = 0;
        public int AP_JumpLevel => AP_MoveRando ? jump_level : 2;
        private int jump_level = 0;
        public bool AP_FlipAttackEnabled => AP_MoveRando ? flip_enabled : true;
        private bool flip_enabled = false;

        public PlayerManager()
        {
            // PlayerScript hooks
            On.PlayerScript.Update += PlayerScript_Update_AP;
            On.PlayerScript.FlipOWill_Do += FlipOWill_AP;
            On.PlayerScript.IsFlipOWillingExtraLong += FlipOWillExtraLong_AP;
            On.PlayerScript.FlipOWillAbort += FlipOWillAbort_AP;
            On.PlayerScript.BackFlip += FlipOWillBackFlip_AP;
        }

        private void PlayerScript_Update_AP(On.PlayerScript.orig_Update orig, PlayerScript self)
        {
            if (AP_BoostLevel < 1 && self.flipOWill_FlipTimer > 0 && self.flipOWill_FlipTimer - (double)Tick.Time < 0.0)
            {
                self.flipOWill_FlipTimer -= Tick.Time * 10; // Prevents regular boost from working
            }
            if (AP_BoostLevel < 2)
            {
                self.flipOWillExtraBoostRuined = true; // Prevent superboost from working
            }
            orig(self);
            if (!AP_FlipAttackEnabled)
            {
                DisableFlipOWillSpinAttack(self);
            }

            if (AP_BoostLevel < 1)
            {
                self.targettedFlipPowerup = null;
                self.flipTargetLineRenderer.enabled = false;
            }
#if DEBUG
            if (Input.GetKeyDown(KeyCode.Minus) && boost_level > 0)
            {
                Plugin.BepinLogger.LogMessage($"Flip-O-Will Boost Level lowered to {--boost_level}");
            }
            if (Input.GetKeyDown(KeyCode.Equals) && boost_level < 2)
            {
                Plugin.BepinLogger.LogMessage($"Flip-O-Will Boost Level increased to {++boost_level}");
            }

            if (Input.GetKeyDown(KeyCode.KeypadMinus) && jump_level > 0)
            {
                Plugin.BepinLogger.LogMessage($"Flip-O-Will Jump Level lowered to {--jump_level}");
            }
            if (Input.GetKeyDown(KeyCode.KeypadPlus) && jump_level < 2)
            {
                Plugin.BepinLogger.LogMessage($"Flip-O-Will Jump Level increased to {++jump_level}");
            }

            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                flip_enabled = !flip_enabled;
                Plugin.BepinLogger.LogMessage($"Flip-O-Will Spin Attack {(flip_enabled ? "enabled" : "disabled")}");
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                CollectableManager.GoldenSpringActive = !CollectableManager.GoldenSpringActive;
                Plugin.BepinLogger.LogMessage($"Golden Spring {(CollectableManager.GoldenSpringActive ? "enabled" : "disabled")}");
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                CollectableManager.GoldenPropellerActive = !CollectableManager.GoldenPropellerActive;
                Plugin.BepinLogger.LogMessage($"Golden Propeller {(CollectableManager.GoldenPropellerActive ? "enabled" : "disabled")}");
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                OrangeSwitchManager.OrangeSwitchActive = !OrangeSwitchManager.OrangeSwitchActive;
                Plugin.BepinLogger.LogMessage($"Orange Switch {(OrangeSwitchManager.OrangeSwitchActive ? "enabled" : "disabled")}");
            }
#endif
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
#if DEBUG
                Plugin.BepinLogger.LogMessage($"\"{callingMethod}\" Flip-O-Will interaction intercepted");
#endif
                return AP_FlipAttackEnabled && AP_BoostLevel > 0 && orig(self);
            }
            return AP_BoostLevel > 0 && orig(self);
        }
    }
}
