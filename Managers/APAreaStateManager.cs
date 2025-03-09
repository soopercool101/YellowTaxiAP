using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace YellowTaxiAP.Managers
{
    public class APAreaStateManager
    {
        public static bool RocketEnabled = false;
        public static bool MindPasswordReceived = false;

        public APAreaStateManager()
        {
            On.DisableAreaScript_BeatedFinalBoss.Start += DisableAreaScript_BeatedFinalBoss_Start;
            On.DisableAreaScript_GoldenSpring.Start += DisableAreaScript_GoldenSpring_Start;
            On.DisableAreaScript_MorioMindPassword.Start += DisableAreaScript_MorioMindPassword_Start;
            On.MorioDreamMachineScript.FixedUpdate += MorioDreamMachineScript_FixedUpdate;
            On.DisableAreaScript_GearsNumber.Start += DisableAreaScript_GearsNumber_Start;
        }

        /// <summary>
        /// Disables the hat wardrobe in the earlygame.
        /// Bizarrely, also disables the coins/gear on top of the rocket. Would think that would be handled by the rocket handler.
        ///
        /// Override and enable everything in all contexts
        /// </summary>
        private void DisableAreaScript_GearsNumber_Start(On.DisableAreaScript_GearsNumber.orig_Start orig, DisableAreaScript_GearsNumber self)
        {
            Plugin.DoubleLog(self.gameObject.name + $" is attempting to disable and enable areas (Expected Gears: {self.activeWhenGearsLowerThanThis})");
            orig(self);
            foreach (var toDisable in self.disableThisAreaWhenActive)
            {
                toDisable.SetActive(true);
                //var bonus = toDisable.GetComponent<BonusScript>();
                //Plugin.DoubleLog(bonus != null
                //    ? $"ToDisable: {toDisable.name} | ID: {APCollectableManager.GetID(bonus)}"
                //    : $"ToDisable: {toDisable.name}");
            }

            foreach (var toEnable in self.enableThisAreaWhenActive)
            {
                Plugin.DoubleLog($"ToEnable: {toEnable.name}");
                toEnable.SetActive(true);
            }
        }

        /// <summary>
        /// Re-implementation to allow Morio's Mind Password to be shuffled
        /// </summary>
        private void MorioDreamMachineScript_FixedUpdate(On.MorioDreamMachineScript.orig_FixedUpdate orig, MorioDreamMachineScript self)
        {
            if (!Tick.IsGameRunning)
                return;
            self.labWallTransform.gameObject.SetActive(!MindPasswordReceived);
            if (Data.morioMindDreamMachineUsedOnce[Data.gameDataIndex]) // todo: remove save file call
            {
                if (!self.lightBulb.enabled)
                    self.lightBulb.enabled = true;
            }
            else
                self.lightBulb.enabled = false;
            self.labWallText.enabled = Utility.AngleSin((float)(Tick.PassedTimePausable * 360.0 * 4.0)) < 0.89999997615814209;
        }

        private void DisableAreaScript_MorioMindPassword_Start(On.DisableAreaScript_MorioMindPassword.orig_Start orig, DisableAreaScript_MorioMindPassword self)
        {
            Plugin.DoubleLog(self.gameObject.name + " is attempting to disable and enable areas");
            UpdateMoriosPasswordState(self);
        }

        /// <summary>
        /// I'm not convinced this one is used anywhere?
        /// </summary>
        private void DisableAreaScript_GoldenSpring_Start(On.DisableAreaScript_GoldenSpring.orig_Start orig, DisableAreaScript_GoldenSpring self)
        {
            Plugin.DoubleLog(self.gameObject.name + " is attempting to disable and enable areas");
            orig(self);
            foreach (var toDisable in self.disableThisAreaWhenActive)
            {
                Plugin.DoubleLog($"ToDisable: {toDisable.name}");
                //toDisable.SetActive(true);
            }

            foreach (var toEnable in self.enableThisAreaWhenActive.Where(o =>
                         o.GetComponent<BonusScript>() != null))
            {
                Plugin.DoubleLog($"ToEnable: {toEnable.name}");
                //toEnable.SetActive(true);
            }
        }

        /// <summary>
        /// Disables and enables final boss state. Primarily used for the Rocket.
        /// </summary>
        private void DisableAreaScript_BeatedFinalBoss_Start(On.DisableAreaScript_BeatedFinalBoss.orig_Start orig, DisableAreaScript_BeatedFinalBoss self)
        {
            //Plugin.DoubleLog(self.gameObject.name + " is attempting to disable and enable areas");
            //foreach (var disable in self.disableThisAreaWhenActive)
            //{
            //    Plugin.DoubleLog($"Disable: {disable.name}");
            //}
            //foreach (var enable in self.enableThisAreaWhenActive)
            //{
            //    Plugin.DoubleLog($"Enable: {enable.name}");
            //}

            if (GameplayMaster.instance.levelId == Data.LevelId.Hub)
            {
                Plugin.DoubleLog(self.gameObject.name + " is attempting to disable and enable areas");
                UpdateRocketState(self);
            }
            else
            {
                Plugin.DoubleLog(self.gameObject.name + " is attempting to disable and enable areas");
                orig(self);
                foreach (var toDisable in self.disableThisAreaWhenActive.Where(o =>
                             o.GetComponent<BonusScript>() != null))
                {
                    Plugin.DoubleLog($"WARNING: THIS SHOULDN'T BE TRIGGERED! MISSABLE FOUND! {toDisable.name}");
                    toDisable.SetActive(true);
                }

                foreach (var toEnable in self.enableThisAreaWhenActive.Where(o =>
                             o.GetComponent<BonusScript>() != null))
                {
                    Plugin.DoubleLog($"WARNING: THIS SHOULDN'T BE TRIGGERED! MISSABLE FOUND! {toEnable.name}");
                    toEnable.SetActive(true);
                }
            }
        }

        public static void UpdateRocketState()
        {
            if (GameplayMaster.instance.levelId != Data.LevelId.Hub)
                return;

            foreach (var rocketObj in Object.FindObjectsByType<DisableAreaScript_BeatedFinalBoss>(FindObjectsSortMode.None))
            {
                UpdateRocketState(rocketObj);
            }
        }

        private static void UpdateRocketState(DisableAreaScript_BeatedFinalBoss rocketObj)
        {
            foreach (var toDisable in rocketObj.disableThisAreaWhenActive)
            {
                toDisable.SetActive(!RocketEnabled || toDisable.GetComponent<BonusScript>() != null);
            }

            foreach (var toEnable in rocketObj.enableThisAreaWhenActive)
            {
                toEnable.SetActive(RocketEnabled || toEnable.GetComponent<BonusScript>() != null);
            }
        }

        public static void UpdateMoriosPasswordState()
        {
            foreach (var morioMindObj in Object.FindObjectsByType<DisableAreaScript_MorioMindPassword>(FindObjectsSortMode.None))
            {
                UpdateMoriosPasswordState(morioMindObj);
            }
        }

        private static void UpdateMoriosPasswordState(DisableAreaScript_MorioMindPassword morioMindObj)
        {
            foreach (var toDisable in morioMindObj.disableThisAreaWhenActive)
            {
                toDisable?.SetActive(!MindPasswordReceived);
            }

            foreach (var toEnable in morioMindObj.enableThisAreaWhenActive)
            {
                toEnable?.SetActive(MindPasswordReceived);
            }
        }
    }
}
