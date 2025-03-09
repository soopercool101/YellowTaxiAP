using System;
using System.Collections.Generic;
using System.Text;

namespace YellowTaxiAP.Managers
{
    public class APPortalManager
    {
        public APPortalManager()
        {
            On.PortalScript.Awake += PortalScript_Awake;
            On.PortalScript.CoroutineGo += PortalScript_CoroutineGo;
            On.PortalScript.GoToLevel += PortalScript_GoToLevel;
            On.PortalScript.OnTriggerEnter += PortalScript_OnTriggerEnter;
        }

        private void PortalScript_OnTriggerEnter(On.PortalScript.orig_OnTriggerEnter orig, PortalScript self, UnityEngine.Collider other)
        {
            if (self.disableTimer > 0.0 || self.disabledByExtraConditions || DialogueScript.instance != null ||
                GameplayMaster.instance.gameOver || TransictionScript.instance != null ||
                !self.DemoCheck_ShouldPortalBeEnabled() ||
                self.kaizoLevelId != Data.LevelId.noone && !self.kaizoEnabled ||
                self.PortalIsLevelPortal && self.gearOpenTr.gameObject.activeSelf ||
                !(other.gameObject == PlayerScript.instance.gameObject) ||
                self.targetLevel != Levels.Index.noone && !self.enableCanvas)
                return;
            if (self.targetLevel == Levels.Index.noone)
            {
                Plugin.DoubleLog($"TaxiWarp to {self.moveTaxiHere} with rotation {self.rotateTaxiY}");
            }
            orig(self, other);
        }

        private void PortalScript_GoToLevel(On.PortalScript.orig_GoToLevel orig, Levels.Index levelSceneIndex, Data.LevelId targetLevelId)
        {
            Plugin.DoubleLog($"PortalWarp to {targetLevelId} with index {levelSceneIndex} ({(int)levelSceneIndex})");
            orig(levelSceneIndex, targetLevelId);
        }

        private System.Collections.IEnumerator PortalScript_CoroutineGo(On.PortalScript.orig_CoroutineGo orig, PortalScript self, int levelIndex)
        {
            Plugin.DoubleLog($"Portal Coroutine: Warp to {self.targetLevelId} with index {levelIndex}. Portal index {self.targetLevel}");
            return orig(self, levelIndex);
        }

        private void PortalScript_Awake(On.PortalScript.orig_Awake orig, PortalScript self)
        {
            self.hubPortalForceEnabled = true;
            orig(self);
            self.UpdatePortalToLevelName();
        }
    }
}
