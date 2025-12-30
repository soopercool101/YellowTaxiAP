using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;
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
            On.GameplayMaster.Awake += GameplayMaster_Awake;
        }

        public static WarpIdentifier QueuedSubwarp;

        private void GameplayMaster_Awake(On.GameplayMaster.orig_Awake orig, GameplayMaster self)
        {
            orig(self);
            if (QueuedSubwarp != null)
            {
                Plugin.Log($"Loading Queued Subwarp ({QueuedSubwarp.Name})");
                GameplayMaster.SelfRespawnClear();
                PortalTransitionScript transitionScript = PortalTransitionScript.Spawn(QueuedSubwarp.MoveTaxiHere, QueuedSubwarp.Rotation);
                transitionScript.songChange = QueuedSubwarp.SongChange;
                transitionScript.backgroundChange = QueuedSubwarp.BackgroundChange;
                transitionScript.desiredWaterState = QueuedSubwarp.DesiredWaterState;
                transitionScript.desiredLightState = QueuedSubwarp.DesiredLightState;
                transitionScript.desiredZoneId = QueuedSubwarp.Zone;
                QueuedSubwarp = null;
            }
        }

        private void PortalScript_OnTriggerEnter(On.PortalScript.orig_OnTriggerEnter orig, PortalScript self, Collider other)
        {
            if (self.disableTimer > 0.0 || self.disabledByExtraConditions || DialogueScript.instance ||
                GameplayMaster.instance.gameOver || TransictionScript.instance ||
                !self.DemoCheck_ShouldPortalBeEnabled() ||
                self.kaizoLevelId != LevelId.noone && !self.kaizoEnabled ||
                self.PortalIsLevelPortal && self.gearOpenTr.gameObject.activeSelf ||
                !(other.gameObject == PlayerScript.instance.gameObject) ||
                self.targetLevel != Levels.Index.noone && !self.enableCanvas)
                return;
#if DEBUG
            var originalWarp = WarpIdentifier.IdentifyOriginalWarp(self);
            Plugin.Log(originalWarp);
            if (originalWarp.StartsWith("Known Warp: "))
            {
                WarpIdentifier.RedirectWarp(self);
            }
#endif
            orig(self, other);
        }

        private void PortalScript_GoToLevel(On.PortalScript.orig_GoToLevel orig, Levels.Index levelSceneIndex, LevelId targetLevelId)
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

    public static class LevelConverter
    {
        public static Levels.Index GetLevelIndex(LevelId id)
        {
            return id switch
            {
                LevelId.noone => Levels.Index.noone,
                LevelId.Hub => Levels.Index.level_hub,
                LevelId.L1_Bombeach => Levels.Index.level_bombeach,
                LevelId.L2_PizzaTime => Levels.Index.level_PizzaTime,
                LevelId.L3_MoriosHome => Levels.Index.level_MoriosHome,
                LevelId.L4_ArcadePanik => Levels.Index.level_PanikArcade,
                LevelId.L5_ToslaOffices => Levels.Index.level_ToslaOffices,
                LevelId.L6_Gym => Levels.Index.level_Gym,
                LevelId.L7_PoopWorld => Levels.Index.level_PoopWorld,
                LevelId.L8_Sewers => Levels.Index.level_Sewers,
                LevelId.L9_City => Levels.Index.level_City,
                LevelId.L10_CrashTestIndustries => Levels.Index.level_CrashTestIndustries,
                LevelId.L11_HubDemo => Levels.Index.level_HubDEMO,
                LevelId.L12_MoriosMind => Levels.Index.level_MoriosMind,
                LevelId.L13_StarmanCastle => Levels.Index.level_StarmanCastle,
                LevelId.L14_ToslaHQ => Levels.Index.level_ToslaHq,
                LevelId.L15_Moon => Levels.Index.level_Moon,
                LevelId.L16_Rocket => Levels.Index.level_Rocket,
                LevelId.L17_TimeAttack01 => Levels.Index.level_time_attack_01,
                LevelId.L18_TimeAttack02 => Levels.Index.level_time_attack_02,
                LevelId.L19_TimeAttack03 => Levels.Index.level_time_attack_03,
                LevelId.L20_PsychoTaxi => Levels.Index.level_psycho_taxi,
                _ => Levels.Index.noone
            };
        }
    }

    public class WarpIdentifier
    {
        public string Name;
        public LevelId OriginalLevelId;
        public Levels.Index OriginalTargetLevel;
        public LevelId OriginalTargetLevelId;
        public Vector3 StartPosition;
        public Vector3 MoveTaxiHere;
        public float Rotation;
        public int Zone;
        public bool DesiredLightState;
        public bool DesiredWaterState;
        public string SongChange;
        public string BackgroundChange;

        public WarpIdentifier(PortalScript warp) : this(GameplayMaster.instance.levelId, warp.targetLevel, warp.targetLevelId,
            warp.portalStartPosition, warp.moveTaxiHere,
            warp.rotateTaxiY, warp.desiredZoneId, warp.desiredLightState, warp.desiredWaterState, warp.songChange, warp.backgroundChange)
        {

        }

        public WarpIdentifier(string name, LevelId levelId, Levels.Index targetLevel, LevelId targetLevelId, Vector3 startPosition,
            Vector3 moveTaxiHere,
            float rotation, int zone, bool desiredLightState, bool desiredWaterState, string songChange, string backgroundChange) : this(levelId, targetLevel, targetLevelId,
            startPosition,
            moveTaxiHere, rotation, zone, desiredLightState, desiredWaterState, songChange, backgroundChange)
        {
            Name = name;
        }

        public WarpIdentifier(LevelId levelId, Levels.Index targetLevel, LevelId targetLevelId, Vector3 startPosition, Vector3 moveTaxiHere,
            float rotation, int zone, bool desiredLightState, bool desiredWaterState, string songChange, string backgroundChange)
        {
            OriginalLevelId = levelId;
            StartPosition = startPosition;
            OriginalTargetLevel = targetLevel;
            OriginalTargetLevelId = targetLevelId;
            MoveTaxiHere = moveTaxiHere;
            Rotation = rotation;
            Zone = zone;
            DesiredLightState = desiredLightState;
            DesiredWaterState = desiredWaterState;
            SongChange = songChange;
            BackgroundChange = backgroundChange;
        }

        public static List<WarpIdentifier> KnownWarps = new()
        {
            // Granny's Island Warps
            new WarpIdentifier("Granny's Island - Morio's Lab Front Door", LevelId.Hub, Levels.Index.noone, LevelId.noone, new Vector3(80f, 20f, 0f), new Vector3(-750f, 10f, 680f), 0, 2, true, false, "SoundtrackHubInside", "Background Soffitto Laboratorio"),
            new WarpIdentifier("Granny's Island - Morio's Lab Back Door", LevelId.Hub, Levels.Index.noone, LevelId.noone, new Vector3(98.35f, 20f, -0.33f), new Vector3(-645.1f, 10f, 680f), 180, 2, true, false, "SoundtrackHubInside", "Background Soffitto Laboratorio"),
            new WarpIdentifier("Granny's Island - Hillside Pipe", LevelId.Hub, Levels.Index.noone, LevelId.noone, new Vector3(10f, 40f, 75f), new Vector3(685f, 20f, -120f), 0, -1, true, true, "SoundtrackHubOutside", "Background Sea and Sky"),
            new WarpIdentifier("Granny's Island - Beach Pipe", LevelId.Hub, Levels.Index.noone, LevelId.noone, new Vector3(665f, 20f, -120f), new Vector3(-5f, 40f, 75f), -180, -1, true, true, "SoundtrackHubOutside", "Background Sea and Sky"),
            new WarpIdentifier("Granny's Island - Ice Cream Truck Entrance", LevelId.Hub, Levels.Index.noone, LevelId.noone, new Vector3(282f, 20f, 41f), new Vector3(-690f, 10f, -760f), 0, 4, true, false, "SoundtrackBonusLevel", "Background Bonus Level"),
            new WarpIdentifier("Granny's Island - Hat World Entrance", LevelId.Hub, Levels.Index.noone, LevelId.noone, new Vector3(352f, 20f, 1.136496E-06f), new Vector3(-640f, 70f, 340f), 180, 1, false, false, "SoundtrackHatShop", "Background Sea and Sky"),
            new WarpIdentifier("Granny's Island - Pizza Oven Entrance", LevelId.Hub, Levels.Index.noone, LevelId.noone, new Vector3(285f, 20f, -50f), new Vector3(-640f, 10f, 140f), 90, 3, true, false, "SoundtrackBonusLevel", "Background Bonus Level"),
            new WarpIdentifier("Granny's Island - Law Firm Roof Entrance", LevelId.Hub, Levels.Index.noone, LevelId.noone, new Vector3(365f, 36f, 20f), new Vector3(-505f, 40f, 65f), -90, 5, true, true, "SoundtrackHubOutside", "Background Sea and Sky"),
            new WarpIdentifier("Granny's Island - Gym Gears Entrance", LevelId.Hub, Levels.Index.level_Gym, LevelId.L6_Gym, new Vector3(319.4f, 15f, 101.27f), new Vector3(319.4f, 15f, 95.27f), 90, -1, true, true, "", ""),
            // Granny's Island Subarea Warps
            new WarpIdentifier("Ice Cream Truck - Exit", LevelId.Hub, Levels.Index.noone, LevelId.noone, new Vector3(-710f, 10f, -760f), new Vector3(282f, 20f, 50f), -90, 0, true, true, "SoundtrackHubOutside", "Background Sea and Sky"),
            new WarpIdentifier("Granny's Island Hat World - Exit", LevelId.Hub, Levels.Index.noone, LevelId.noone, new Vector3(-635f, 70f, 340f), new Vector3(345f, 20f, 1.748456E-06f), 180, 0, true, true, "SoundtrackHubOutside", "Background Sea and Sky"),
            new WarpIdentifier("Pizza Oven - Exit", LevelId.Hub, Levels.Index.noone, LevelId.noone, new Vector3(-640f, 10f, 150f), new Vector3(290f, 20f, -50f), 0, 0, true, true, "SoundtrackHubOutside", "Background Sea and Sky"),
            new WarpIdentifier("Law Firm - Exit", LevelId.Hub, Levels.Index.noone, LevelId.noone, new Vector3(-505f, 10f, 90f), new Vector3(365f, 20f, 30f), -180, 0, true, true, "SoundtrackHubOutside", "Background Sea and Sky"),
            // Morio's Lab Warps
            new WarpIdentifier("Morio's Lab - Front Door", LevelId.Hub, Levels.Index.noone, LevelId.noone, new Vector3(-770f, 10f, 680f), new Vector3(70f, 20f, 0f), 180, 0, true, true, "SoundtrackHubOutside", "Background Sea and Sky"),
            new WarpIdentifier("Morio's Lab - Back Door", LevelId.Hub, Levels.Index.noone, LevelId.noone, new Vector3(-630f, 10f, 680f), new Vector3(110f, 20f, 0f), 0, 0, true, true, "SoundtrackHubOutside", "Background Sea and Sky"),
            new WarpIdentifier("Morio's Lab - Wardrobe Entrance", LevelId.Hub, Levels.Index.noone, LevelId.noone, new Vector3(-720f, 10f, 646.54f), new Vector3(-660f, 10f, 1115f), 0, 2, false, false, "SoundtrackHubInside", "Background Soffitto Laboratorio"),
            new WarpIdentifier("Morio's Wardrobe - Exit", LevelId.Hub, Levels.Index.noone, LevelId.noone, new Vector3(-670f, 10f, 1115f), new Vector3(-715f, 10f, 655f), -90, 2, true, false, "SoundtrackHubInside", "Background Soffitto Laboratorio"),
            new WarpIdentifier("Morio's Lab - Second Floor Shortcut Pipe", LevelId.Hub, Levels.Index.noone, LevelId.noone, new Vector3(-750f, 90f, 630f), new Vector3(-817.1f, 160.5f, 520f), 180, -1, true, true, "SoundtrackHubInside", "Background Soffitto Laboratorio"),
            new WarpIdentifier("Morio's Lab - Fifth Floor Shortcut Pipe", LevelId.Hub, Levels.Index.noone, LevelId.noone, new Vector3(-800f, 165f, 520f), new Vector3(-750f, 80f, 630f), -45, -1, true, true, "SoundtrackHubInside", "Background Soffitto Laboratorio"),
            new WarpIdentifier("Morio's Lab - Portal to Morio's Island", LevelId.Hub, Levels.Index.level_MoriosHome, LevelId.L3_MoriosHome, new Vector3(-640f, 30f, 730f), new Vector3(-650f, 30f, 720f), 135, -1, true, false, "SoundtrackHubInside", "Background Soffitto Laboratorio"),

            // Morio's Island Warps
            new WarpIdentifier("Morio's Island - Morio's Garage Entrance", LevelId.L3_MoriosHome, Levels.Index.noone, LevelId.noone, new Vector3(-10f, 0f, 465f), new Vector3(-20f, 10f, -310f), 90, 3, false, false, "SoundtrackMoriosHomeInternal", "Background Morio's Home Internal"),
            // Morio's Home Warps
            new WarpIdentifier("Morio's Home - Morio's Garage Exit", LevelId.L3_MoriosHome, Levels.Index.noone, LevelId.noone, new Vector3(-20f, 10f, -300f), new Vector3(-10f, 0f, 460f), 90, 0, true, true, "default", "default"),
            new WarpIdentifier("Morio's Home - Door to Weird Tunnels", LevelId.L3_MoriosHome, Levels.Index.noone, LevelId.noone, new Vector3(85.10001f, 30.41628f, -409.6847f), new Vector3(-645f, 55f, -645f), 0, 4, false, false, "SoundtrackMoriosHomeInternal", "Background Morio's Home Internal"),
            // Weird Tunnel Warps
            new WarpIdentifier("Weird Tunnels - Entrance Door", LevelId.L3_MoriosHome, Levels.Index.noone, LevelId.noone, new Vector3(-670f, 10f, -645f), new Vector3(85f, 30f, -445f), 90, 3, false, false, "SoundtrackMoriosHomeInternal", "Background Morio's Home Internal"),
            new WarpIdentifier("Weird Tunnels - Exit Door", LevelId.L3_MoriosHome, Levels.Index.noone, LevelId.noone, new Vector3(-260f, 0f, -645f), new Vector3(85f, 30f, -445f), 90, 3, false, false, "SoundtrackMoriosHomeInternal", "Background Morio's Home Internal")
        };

        public static Dictionary<string, string> WarpRedirects = new()
        {
            {"Granny's Island - Morio's Lab Front Door", "Weird Tunnels - Exit Door"},
            //{"Granny's Island - Gym Gears Entrance", "Law Firm - Exit"},
            //{"Granny's Island - Law Firm Roof Entrance", "Granny's Island - Gym Gears Entrance"},
            //{"Granny's Island Hat World - Exit", "Granny's Island Hat World - Exit"},
            //{"Morio's Lab - Front Door", "Granny's Island - Hat World Entrance"},
            //{"Granny's Island - Hat World Entrance", "Morio's Home - Door to Weird Tunnels"},
            //{"Morio's Home - Door to Weird Tunnels", "Granny's Island - Gym Gears Entrance"},
            //{"Morio's Lab - Portal to Morio's Island", "Morio's Home - Morio's Garage Exit"},
            //{"Morio's Island - Morio's Garage Entrance", "Granny's Island - Law Firm Roof Entrance"},
        };

        public override bool Equals(object obj)
        {
            if (obj is WarpIdentifier otherWarp)
            {
                return ProbablyEquals(otherWarp) &&
                       // Background and song changes need to be adjusted to work properly (rather than assuming they're already set)
                       // As such, they are not equal
                       //SongChange.Equals(otherWarp.SongChange) &&
                       //BackgroundChange.Equals(otherWarp.BackgroundChange) &&
                       Mathf.Approximately(StartPosition.x, otherWarp.StartPosition.x) &&
                       Mathf.Approximately(StartPosition.y, otherWarp.StartPosition.y) &&
                       Mathf.Approximately(StartPosition.z, otherWarp.StartPosition.z);
            }

            return false;
        }

        private bool ProbablyEquals(WarpIdentifier otherWarp)
        {
            return OriginalLevelId == otherWarp.OriginalLevelId &&
                   OriginalTargetLevel == otherWarp.OriginalTargetLevel &&
                   OriginalTargetLevelId == otherWarp.OriginalTargetLevelId &&
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
            GUIUtility.systemCopyBuffer = $"new WarpIdentifier(\"{warpName}\", LevelId.{GameplayMaster.instance.levelId}, Levels.Index.{warp.targetLevel}, LevelId.{warp.targetLevelId}, new Vector3({warp.portalStartPosition.x}f, {warp.portalStartPosition.y}f, {warp.portalStartPosition.z}f), new Vector3({warp.moveTaxiHere.x}f, {warp.moveTaxiHere.y}f, {warp.moveTaxiHere.z}f), {warp.rotateTaxiY}, {warp.desiredZoneId}, {warp.desiredLightState.ToString().ToLower()}, {warp.desiredWaterState.ToString().ToLower()}, \"{warp.songChange}\", \"{warp.backgroundChange}\"),";
#endif

            if (knownWarp != null)
            {
                return $"(Probably?) Known Warp: {knownWarp.Name}";
            }

            if (warp.targetLevel != Levels.Index.noone)
                return $"Portal to {warp.targetLevelId}";

            return $"Unknown TaxiWarp to {warp.moveTaxiHere} with rotation {warp.rotateTaxiY}";
        }

        public static bool RedirectWarp(PortalScript warp)
        {
            var warpIdentifier = new WarpIdentifier(warp);
            var knownWarp = KnownWarps.FirstOrDefault(o => o.Equals(warpIdentifier));
            if (knownWarp != null && WarpRedirects.TryGetValue(knownWarp.Name, out var redirectedWarpName))
            {
                var redirectedWarp = KnownWarps.FirstOrDefault(o => o.Name.Equals(redirectedWarpName));
                if (redirectedWarp == null)
                {
                    Plugin.Log($"ERROR: COULD NOT PROPERLY REDIRECT TO {redirectedWarpName}");
                    throw new KeyNotFoundException($"Could not find warp: \"{redirectedWarpName}\"");
                }

                warp.targetLevel = redirectedWarp.OriginalTargetLevel;
                warp.targetLevelId = redirectedWarp.OriginalTargetLevelId;
                warp.islandToLabPortal = true;
                warp.skipTaxiRisucchio = false;

                if (redirectedWarp.OriginalTargetLevelId == LevelId.noone &&
                    GameplayMaster.instance.levelId != redirectedWarp.OriginalLevelId)
                {
                    APPortalManager.QueuedSubwarp = redirectedWarp;
                    warp.targetLevel = LevelConverter.GetLevelIndex(redirectedWarp.OriginalLevelId);
                    warp.targetLevelId = redirectedWarp.OriginalLevelId;
                }

                warp.moveTaxiHere = redirectedWarp.MoveTaxiHere;
                warp.rotateTaxiY = redirectedWarp.Rotation;
                warp.desiredZoneId = redirectedWarp.Zone;
                warp.desiredLightState = redirectedWarp.DesiredLightState;
                warp.desiredWaterState = redirectedWarp.DesiredWaterState;
                warp.backgroundChange = redirectedWarp.BackgroundChange;
                warp.songChange = redirectedWarp.SongChange;

                return true;
            }

            return false;
        }
    }
}
