using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Data;

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
            if (self.disableTimer > 0.0 || self.disabledByExtraConditions || DialogueScript.instance ||
                GameplayMaster.instance.gameOver || TransictionScript.instance ||
                !self.DemoCheck_ShouldPortalBeEnabled() ||
                self.kaizoLevelId != Data.LevelId.noone && !self.kaizoEnabled ||
                self.PortalIsLevelPortal && self.gearOpenTr.gameObject.activeSelf ||
                !(other.gameObject == PlayerScript.instance.gameObject) ||
                self.targetLevel != Levels.Index.noone && !self.enableCanvas)
                return;
#if DEBUG
            Plugin.Log(WarpIdentifier.IdentifyOriginalWarp(self));
#endif
            orig(self, other);
        }

        private void PortalScript_GoToLevel(On.PortalScript.orig_GoToLevel orig, Levels.Index levelSceneIndex, Data.LevelId targetLevelId)
        {
            Plugin.Log($"PortalWarp to {targetLevelId} with index {levelSceneIndex} ({(int)levelSceneIndex})");
            orig(levelSceneIndex, targetLevelId);
        }

        private System.Collections.IEnumerator PortalScript_CoroutineGo(On.PortalScript.orig_CoroutineGo orig, PortalScript self, int levelIndex)
        {
            Plugin.Log($"Portal Coroutine: Warp to {self.targetLevelId} with index {levelIndex}. Portal index {self.targetLevel}");
            return orig(self, levelIndex);
        }

        private void PortalScript_Awake(On.PortalScript.orig_Awake orig, PortalScript self)
        {
            self.hubPortalForceEnabled = true;
            orig(self);
            self.UpdatePortalToLevelName();
        }
    }

    public class WarpIdentifier
    {
        public string Name;
        public LevelId OriginalLevel;
        public LevelId OriginalTargetLevel;
        public Vector3 WarpLocation;
        public Vector3 MoveTaxiHere;
        public float Rotation;
        public int Zone;
        public bool DesiredLightState;
        public bool DesiredWaterState;

        public WarpIdentifier(PortalScript warp) : this(GameplayMaster.instance.levelId, warp.targetLevelId, warp.gameObject.transform.position, warp.moveTaxiHere,
            warp.rotateTaxiY, warp.desiredZoneId, warp.desiredLightState, warp.desiredWaterState)
        {

        }

        public WarpIdentifier(string name, LevelId level, LevelId targetLevel, Vector3 warpLocation, Vector3 moveTaxiHere,
            float rotation, int zone, bool desiredLightState, bool desiredWaterState) : this(level, targetLevel, warpLocation,
            moveTaxiHere, rotation, zone, desiredLightState, desiredWaterState)
        {
            Name = name;
        }

        public WarpIdentifier(LevelId level, LevelId targetLevel, Vector3 warpLocation, Vector3 moveTaxiHere, float rotation, int zone, bool desiredLightState, bool desiredWaterState)
        {
            OriginalLevel = level;
            WarpLocation = warpLocation;
            OriginalTargetLevel = targetLevel;
            MoveTaxiHere = moveTaxiHere;
            Rotation = rotation;
            Zone = zone;
            DesiredLightState = desiredLightState;
            DesiredWaterState = desiredWaterState;
        }

        public static List<WarpIdentifier> KnownWarps = new()
        {
            // Morio's Island Warps
            new WarpIdentifier("Morio's Island - Morio's Garage Entrance", LevelId.L3_MoriosHome, LevelId.noone, new Vector3(-10f, 0f, 465f), new Vector3(-20f, 10f, -310f), 90, 3, false, false),
            // Morio's Home Warps
            new WarpIdentifier("Morio's Home - Morio's Garage Exit", LevelId.L3_MoriosHome, LevelId.noone, new Vector3(-20f, 10f, -300f), new Vector3(-10f, 0f, 460f), 90, 0, true, true),
            new WarpIdentifier("Morio's Home - Door to Weird Tunnels", LevelId.L3_MoriosHome, LevelId.noone, new Vector3(85.10001f, 30.41628f, -409.6847f), new Vector3(-645f, 55f, -645f), 0, 4, false, false),
            // Weird Tunnel Warps
            new WarpIdentifier("Weird Tunnels - Entrance Door", LevelId.L3_MoriosHome, LevelId.noone, new Vector3(-670f, 10f, -645f), new Vector3(85f, 30f, -445f), 90, 3, false, false),
            new WarpIdentifier("Weird Tunnels - Exit Door", LevelId.L3_MoriosHome, LevelId.noone, new Vector3(-260f, 0f, -645f), new Vector3(85f, 30f, -445f), 90, 3, false, false)
        };

        public override bool Equals(object obj)
        {
            if (obj is WarpIdentifier otherWarp)
            {
                return ProbablyEquals(otherWarp) &&
                       Mathf.Approximately(WarpLocation.x, otherWarp.WarpLocation.x) &&
                       Mathf.Approximately(WarpLocation.y, otherWarp.WarpLocation.y) &&
                       Mathf.Approximately(WarpLocation.z, otherWarp.WarpLocation.z);
            }

            return false;
        }

        private bool ProbablyEquals(WarpIdentifier otherWarp)
        {
            return OriginalLevel == otherWarp.OriginalLevel &&
                   OriginalTargetLevel == otherWarp.OriginalTargetLevel &&
                   Mathf.Approximately(MoveTaxiHere.x, otherWarp.MoveTaxiHere.x) &&
                   Mathf.Approximately(MoveTaxiHere.y, otherWarp.MoveTaxiHere.y) &&
                   Mathf.Approximately(MoveTaxiHere.z, otherWarp.MoveTaxiHere.z) &&
                   Mathf.Approximately(Rotation, otherWarp.Rotation) &&
                   Zone == otherWarp.Zone &&
                   DesiredLightState == otherWarp.DesiredLightState &&
                   DesiredWaterState == otherWarp.DesiredWaterState;
        }

        public static string IdentifyOriginalWarp(PortalScript warp)
        {
            var warpIdentifier = new WarpIdentifier(warp);
            var knownWarp = KnownWarps.FirstOrDefault(o => o.Equals(warpIdentifier));
            if (knownWarp != null)
            {
                return $"Known Warp: {knownWarp.Name}";
            }

            knownWarp = KnownWarps.FirstOrDefault(o => o.ProbablyEquals(warpIdentifier));
            var warpName = knownWarp?.Name ?? string.Empty;
#if DEBUG
            GUIUtility.systemCopyBuffer = $"new WarpIdentifier(\"{warpName}\", Data.LevelId.{GameplayMaster.instance.levelId}, Data.LevelId.{warp.targetLevelId}, new Vector3({warp.gameObject.transform.position.x}f, {warp.gameObject.transform.position.y}f, {warp.gameObject.transform.position.z}f), new Vector3({warp.moveTaxiHere.x}f, {warp.moveTaxiHere.y}f, {warp.moveTaxiHere.z}f), {warp.rotateTaxiY}, {warp.desiredZoneId}, {warp.desiredLightState.ToString().ToLower()}, {warp.desiredWaterState.ToString().ToLower()})";
#endif

            if (knownWarp != null)
            {
                return $"(Probably?) Known Warp: {knownWarp.Name}";
            }

            if (warp.targetLevel != Levels.Index.noone)
                return $"Portal to {warp.targetLevelId}";

            return $"Unknown TaxiWarp to {warp.moveTaxiHere} with rotation {warp.rotateTaxiY}";
        }
    }
}
