using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YellowTaxiAP.Behaviours;
using static Data;

namespace YellowTaxiAP.Managers
{
    public class APPortalManager
    {
        public APPortalManager()
        {
            On.PlayerScript.Start += PlayerScript_Start;
            On.PortalScript.Awake += PortalScript_Awake;
            On.PortalScript.CoroutineGo += PortalScript_CoroutineGo;
            On.PortalScript.GoToLevel += PortalScript_GoToLevel;
            On.PortalScript.OnTriggerEnter += PortalScript_OnTriggerEnter;
            On.PortalScript.PortalIslandToLabCoroutine += PortalScript_PortalIslandToLabCoroutine;
            On.PortalScript.PortalOpenStart += PortalScript_PortalOpenStart;
            //On.PortalScript.DemoCheck_ShouldPortalBeEnabled += PortalScript_DemoCheck_ShouldPortalBeEnabled;
            //On.PortalScript.SetupDataForLevelComeback += PortalScript_SetupDataForLevelComeback;
            On.LoadingScreenScript.WelcomeSetup += LoadingScreenScript_WelcomeSetup;
        }

        private void PortalScript_PortalOpenStart(On.PortalScript.orig_PortalOpenStart orig, PortalScript self)
        {
            var trueId = self.gameObject.GetComponent<TruePortalId>();
            APSaveController.MiscSave.SetLevelPortalUnlocked(trueId.OriginalLevel);
            orig(self);
        }

        private bool PortalScript_DemoCheck_ShouldPortalBeEnabled(On.PortalScript.orig_DemoCheck_ShouldPortalBeEnabled orig, PortalScript self)
        {
            Plugin.Log($"{self.gameObject.name}: {self.USE_IN_DEMO_} | {self.USE_IN_DEMO_EXTRA} | {self.USE_IN_DEMO_EXTRA_INFLUENCERS}");
            return self.USE_IN_DEMO_;
        }

        private void PlayerScript_Start(On.PlayerScript.orig_Start orig, PlayerScript self)
        {
            orig(self);
            if (QueuedSubwarp == null) return;
            Plugin.Log($"Loading Queued Subwarp ({QueuedSubwarp.Name})");
            if (!string.IsNullOrEmpty(QueuedSubwarp.BackgroundChange) &&
                !QueuedSubwarp.BackgroundChange.Equals("default",
                    StringComparison.OrdinalIgnoreCase) && QueuedSubwarp.BackgroundChange !=
                BackgroundMaster.instance.name)
            {
                BackgroundMaster.Change(QueuedSubwarp.BackgroundChange);
            }

            if (!string.IsNullOrEmpty(QueuedSubwarp.SongChange) &&
                !QueuedSubwarp.SongChange.Equals("default",
                    StringComparison.OrdinalIgnoreCase) && QueuedSubwarp.SongChange !=
                GameplayMaster.instance.levelSoundtrack)
            {
                GameplayMaster.instance.levelSoundtrack = QueuedSubwarp.SongChange;
            }

            self.transform.position = QueuedSubwarp.MoveTaxiHere + new Vector3(0.0f, 0.1f, 0.0f);
            self.transform.SetYAngle(QueuedSubwarp.Rotation);
            LightDirectionalScript.instance.myLight.enabled = QueuedSubwarp.DesiredLightState;
            WaterScript.instance.WaterEnable = QueuedSubwarp.DesiredWaterState;
            self.myPausable.velBackup[0] = Vector3.zero;
            self.InstantCameraSet(0.0f);
            if (QueuedSubwarp.Zone >= 0)
                ZoneMaster.currentZoneId = QueuedSubwarp.Zone;
            self.TeleportComputeZoneMaster(self.transform);

            QueuedSubwarp = null;
        }

        private void LoadingScreenScript_WelcomeSetup(On.LoadingScreenScript.orig_WelcomeSetup orig, LevelId targetLevelId, string levelName, int gearsCollected, int maxGearsInsideLevel, bool enableCameraLevelIntro)
        {
            orig(targetLevelId, levelName, gearsCollected, maxGearsInsideLevel, enableCameraLevelIntro && QueuedSubwarp == null);
        }

        private void PortalScript_SetupDataForLevelComeback(On.PortalScript.orig_SetupDataForLevelComeback orig, PortalScript self, bool saveToDisk, bool forcePortalDesiredWaterState)
        {
            // Do nothing.
        }

        private System.Collections.IEnumerator PortalScript_PortalIslandToLabCoroutine(On.PortalScript.orig_PortalIslandToLabCoroutine orig, PortalScript self)
        {
            Plugin.Log("Portal Coroutine: Island to Lab");
            return orig(self);
        }

        public static WarpIdentifier QueuedSubwarp;

        private void PortalScript_OnTriggerEnter(On.PortalScript.orig_OnTriggerEnter orig, PortalScript self, Collider other)
        {
            if (self.disableTimer > 0.0 || self.disabledByExtraConditions || DialogueScript.instance ||
                GameplayMaster.instance.gameOver || TransictionScript.instance ||
                !self.DemoCheck_ShouldPortalBeEnabled() ||
                (self.kaizoLevelId != LevelId.noone && !self.kaizoEnabled) ||
                (self.PortalIsLevelPortal && self.gearOpenTr.gameObject.activeSelf) ||
                !(other.gameObject == PlayerScript.instance.gameObject) ||
                (self.targetLevel != Levels.Index.noone && !self.enableCanvas))
                return;
#if DEBUG
            var originalWarp = WarpIdentifier.IdentifyOriginalWarp(self);
            Plugin.Log(originalWarp);
            var skipTaxiRisucchio = self.skipTaxiRisucchio || (self.targetLevel == Levels.Index.noone && self.kaizoLevelId == LevelId.noone);
            if (WarpIdentifier.RedirectWarp(self))
            {
                Plugin.Log("Warp redirected");
                // Make sure that verification doesn't fail in orig. We've already verified the warp is valid!
                if(self.PortalIsLevelPortal)
                    self.gearOpenTr.gameObject.SetActive(false);
                if(self.targetLevel != Levels.Index.noone)
                    self.enableCanvas = true;
                if (self.kaizoLevelId != LevelId.noone)
                    self.kaizoEnabled = true;
                if (self.targetLevel == Levels.Index.level_hub) // Skip question about returning to Hub
                    self.targetLevel += 100;
                self.skipTaxiRisucchio = skipTaxiRisucchio; // Only do the canned shrink animation in portals
            }
#endif
            orig(self, other);
        }

        private void PortalScript_GoToLevel(On.PortalScript.orig_GoToLevel orig, Levels.Index levelSceneIndex, LevelId targetLevelId)
        {
            if ((int)levelSceneIndex > 100)
                levelSceneIndex -= 100;
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
            self.gameObject.AddComponent<TruePortalId>(); // Keep track of unaltered portal values
            if (self.PortalIsLevelPortal)
            {
                switch (self.targetLevelId)
                {
                    case LevelId.Hub:
                    case LevelId.L6_Gym:
                    case LevelId.L7_PoopWorld:
                    case LevelId.L8_Sewers:
                    case LevelId.L11_HubDemo:
                    case LevelId.L16_Rocket:
                    case LevelId.L17_TimeAttack01:
                    case LevelId.L18_TimeAttack02:
                    case LevelId.L19_TimeAttack03:
                    case LevelId.L20_PsychoTaxi:
                        GetLevel(self.targetLevelId).everOpened = true;
                        break;
                    default:
                        Plugin.Log($"Checking if {self.targetLevelId} portal ({self.gameObject.name}) should be open {APSaveController.MiscSave.HasLevelPortalUnlocked(self.targetLevelId)}");
                        GetLevel(self.targetLevelId).everOpened = APSaveController.MiscSave.HasLevelPortalUnlocked(self.targetLevelId);
                        break;
                }
            }
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
        public string LinkedExit;
        public string ExitGroup;
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

        public string Group => string.IsNullOrEmpty(ExitGroup) ? Name : ExitGroup;

        public WarpIdentifier(PortalScript warp) : this(GameplayMaster.instance.levelId, warp.targetLevel, warp.targetLevelId,
            warp.portalStartPosition, warp.moveTaxiHere,
            warp.rotateTaxiY, warp.desiredZoneId, warp.desiredLightState, warp.desiredWaterState, warp.songChange, warp.backgroundChange)
        {

        }

        public WarpIdentifier(string name, string linkedExit, string exitGroup, LevelId levelId, Levels.Index targetLevel, LevelId targetLevelId, Vector3 startPosition,
            Vector3 moveTaxiHere, float rotation, int zone, bool desiredLightState, bool desiredWaterState, string songChange, string backgroundChange, LevelId kaizoLevelId = LevelId.noone)
            : this(levelId, targetLevel, targetLevelId, startPosition, moveTaxiHere, rotation, zone, desiredLightState, desiredWaterState, songChange, backgroundChange)
        {
            Name = name;
            LinkedExit = linkedExit;
            ExitGroup = exitGroup;
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
            new WarpIdentifier("Granny's Island - Morio's Lab Front Door", "Morio's Lab - Front Door", "", LevelId.Hub, Levels.Index.noone, LevelId.noone, new Vector3(80f, 20f, 0f), new Vector3(-750f, 10f, 680f), 0, 2, true, false, "SoundtrackHubInside", "Background Soffitto Laboratorio"),
            new WarpIdentifier("Granny's Island - Morio's Lab Back Door", "Morio's Lab - Back Door", "", LevelId.Hub, Levels.Index.noone, LevelId.noone, new Vector3(98.35f, 20f, -0.33f), new Vector3(-645.1f, 10f, 680f), 180, 2, true, false, "SoundtrackHubInside", "Background Soffitto Laboratorio"),
            new WarpIdentifier("Granny's Island - Hillside Pipe", "Granny's Island - Beach Pipe", "", LevelId.Hub, Levels.Index.noone, LevelId.noone, new Vector3(10f, 40f, 75f), new Vector3(685f, 20f, -120f), 0, 0, true, true, "SoundtrackHubOutside", "Background Sea and Sky"),
            new WarpIdentifier("Granny's Island - Beach Pipe", "Granny's Island - Hillside Pipe", "", LevelId.Hub, Levels.Index.noone, LevelId.noone, new Vector3(665f, 20f, -120f), new Vector3(-5f, 40f, 75f), -180, 0, true, true, "SoundtrackHubOutside", "Background Sea and Sky"),
            new WarpIdentifier("Granny's Island - Ice Cream Truck Entrance", "Ice Cream Truck - Exit", "", LevelId.Hub, Levels.Index.noone, LevelId.noone, new Vector3(282f, 20f, 41f), new Vector3(-690f, 10f, -760f), 0, 4, true, false, "SoundtrackBonusLevel", "Background Bonus Level"),
            new WarpIdentifier("Granny's Island - Hat World Entrance", "Granny's Island Hat World - Exit", "", LevelId.Hub, Levels.Index.noone, LevelId.noone, new Vector3(352f, 20f, 1.136496E-06f), new Vector3(-640f, 70f, 340f), 180, 1, false, false, "SoundtrackHatShop", "Background Sea and Sky"),
            new WarpIdentifier("Granny's Island - Pizza Oven Entrance", "Pizza Oven - Exit", "", LevelId.Hub, Levels.Index.noone, LevelId.noone, new Vector3(285f, 20f, -50f), new Vector3(-640f, 10f, 140f), 90, 3, true, false, "SoundtrackBonusLevel", "Background Bonus Level"),
            new WarpIdentifier("Granny's Island - Law Firm Roof Entrance", "Law Firm - Exit", "", LevelId.Hub, Levels.Index.noone, LevelId.noone, new Vector3(365f, 36f, 20f), new Vector3(-505f, 40f, 65f), -90, 5, true, true, "SoundtrackHubOutside", "Background Sea and Sky"),
            new WarpIdentifier("Granny's Island - Crash Again Entrance", "Crash Again - Exit", "", LevelId.Hub, Levels.Index.noone, LevelId.noone, new Vector3(-250f, 55f, 0f), new Vector3(-475f, 10f, -110f), 90, 6, true, false, "SoundtrackBonusLevel", "Background Bonus Level"),
            new WarpIdentifier("Granny's Island - Gym Gears Entrance", "", "", LevelId.Hub, Levels.Index.level_Gym, LevelId.L6_Gym, new Vector3(319.4f, 15f, 101.27f), new Vector3(5f, 0f, 0.1f), 0, 0, false, true, "SoundtrackGym", "Background Skyline Night"),
            new WarpIdentifier("Granny's Island - Fecal Matters House", "", "", LevelId.Hub, Levels.Index.level_PoopWorld, LevelId.L7_PoopWorld, new Vector3(250f, 20f, -95f), new Vector3(0f, 0f, 0f), 0, 0, true, true, "SoundtrackPoopWorld", "Backround Sky Poop World"),
            new WarpIdentifier("Granny's Island - Flushed Away Entrance", "", "", LevelId.Hub, Levels.Index.level_Sewers, LevelId.L8_Sewers, new Vector3(175f, 10f, -709.92f), new Vector3(20f, 250f, 5f), 0, 0, false, true, "SoundtrackSewers", "Background Black"),
            new WarpIdentifier("Granny's Island - Rocket Entrance", "Mosk's Rocket - Exit to Granny's Island", "", LevelId.Hub, Levels.Index.level_Rocket, LevelId.L16_Rocket, new Vector3(190f, 45f, -58.73f), new Vector3(-1140f, 0f, -1120f), 0, 0, true, true, "SoundtrackRocket", "Background Bonus Level"),
            // Granny's Island Subarea Warps
            new WarpIdentifier("Ice Cream Truck - Exit", "Granny's Island - Ice Cream Truck Entrance", "", LevelId.Hub, Levels.Index.noone, LevelId.noone, new Vector3(-710f, 10f, -760f), new Vector3(282f, 20f, 50f), -90, 0, true, true, "SoundtrackHubOutside", "Background Sea and Sky"),
            new WarpIdentifier("Granny's Island Hat World - Exit", "Granny's Island - Hat World Entrance", "", LevelId.Hub, Levels.Index.noone, LevelId.noone, new Vector3(-635f, 70f, 340f), new Vector3(345f, 20f, 1.748456E-06f), 180, 0, true, true, "SoundtrackHubOutside", "Background Sea and Sky"),
            new WarpIdentifier("Pizza Oven - Exit", "Granny's Island - Pizza Oven Entrance", "", LevelId.Hub, Levels.Index.noone, LevelId.noone, new Vector3(-640f, 10f, 150f), new Vector3(290f, 20f, -50f), 0, 0, true, true, "SoundtrackHubOutside", "Background Sea and Sky"),
            new WarpIdentifier("Law Firm - Exit", "Granny's Island - Law Firm Roof Entrance", "", LevelId.Hub, Levels.Index.noone, LevelId.noone, new Vector3(-505f, 10f, 90f), new Vector3(365f, 20f, 30f), -180, 0, true, true, "SoundtrackHubOutside", "Background Sea and Sky"),
            new WarpIdentifier("Crash Again - Exit", "Granny's Island - Crash Again Entrance", "", LevelId.Hub, Levels.Index.noone, LevelId.noone, new Vector3(-475f, 10f, -105f), new Vector3(-240f, 55f, 0f), 0, 0, true, true, "SoundtrackHubOutside", "Background Sea and Sky"),
            // Morio's Lab Warps
            new WarpIdentifier("Morio's Lab - Front Door", "Granny's Island - Morio's Lab Front Door", "", LevelId.Hub, Levels.Index.noone, LevelId.noone, new Vector3(-770f, 10f, 680f), new Vector3(70f, 20f, 0f), 180, 0, true, true, "SoundtrackHubOutside", "Background Sea and Sky"),
            new WarpIdentifier("Morio's Lab - Back Door", "Granny's Island - Morio's Lab Back Door", "", LevelId.Hub, Levels.Index.noone, LevelId.noone, new Vector3(-630f, 10f, 680f), new Vector3(110f, 20f, 0f), 0, 0, true, true, "SoundtrackHubOutside", "Background Sea and Sky"),
            new WarpIdentifier("Morio's Lab - Wardrobe Entrance", "Morio's Wardrobe - Exit", "", LevelId.Hub, Levels.Index.noone, LevelId.noone, new Vector3(-720f, 10f, 646.54f), new Vector3(-660f, 10f, 1115f), 0, 2, false, false, "SoundtrackHubInside", "Background Soffitto Laboratorio"),
            new WarpIdentifier("Morio's Wardrobe - Exit", "Morio's Lab - Wardrobe Entrance", "", LevelId.Hub, Levels.Index.noone, LevelId.noone, new Vector3(-670f, 10f, 1115f), new Vector3(-715f, 10f, 655f), -90, 2, true, false, "SoundtrackHubInside", "Background Soffitto Laboratorio"),
            new WarpIdentifier("Morio's Lab - Second Floor Shortcut Pipe", "Morio's Lab - Fifth Floor Shortcut Pipe", "", LevelId.Hub, Levels.Index.noone, LevelId.noone, new Vector3(-750f, 90f, 630f), new Vector3(-817.1f, 160.5f, 520f), 180, 2, true, true, "SoundtrackHubInside", "Background Soffitto Laboratorio"),
            new WarpIdentifier("Morio's Lab - Fifth Floor Shortcut Pipe", "Morio's Lab - Second Floor Shortcut Pipe", "", LevelId.Hub, Levels.Index.noone, LevelId.noone, new Vector3(-800f, 165f, 520f), new Vector3(-750f, 80f, 630f), -45, 2, true, true, "SoundtrackHubInside", "Background Soffitto Laboratorio"),
            new WarpIdentifier("Morio's Lab - Morio's Home Portal", "", "", LevelId.Hub, Levels.Index.level_MoriosHome, LevelId.L3_MoriosHome, new Vector3(-640f, 30f, 730f), new Vector3(0f, 10f, 960f), 0, 0, true, true, "SoundtrackMoriosHome", "Background Morio's Island"),
            new WarpIdentifier("Morio's Lab - Bombeach Portal", "Bombeach - Morio's Lab Portal Near Entrance", "", LevelId.Hub, Levels.Index.level_bombeach, LevelId.L1_Bombeach, new Vector3(-640f, 50f, 630f), new Vector3(0f, 0f, 800f), 0, 0, true, true, "SoundtrackBombeach", "Background Bombeach"),
            new WarpIdentifier("Morio's Lab - Arcade Panik Portal", "", "", LevelId.Hub, Levels.Index.level_PanikArcade, LevelId.L4_ArcadePanik, new Vector3(-780f, 50f, 680f), new Vector3(-800f, 0f, 0f), 180, 3, true, false, "SoundtrackArcadePanik", "Background Sea and Sky - Sunset"),
            new WarpIdentifier("Morio's Lab - Pizza Time Portal", "", "", LevelId.Hub, Levels.Index.level_PizzaTime, LevelId.L2_PizzaTime, new Vector3(-720f, 50f, 810f), new Vector3(0f, 0f, 0f), 180, 0, true, true, "SoundtrackPizzaTime", "Background Pizza Time"),
            new WarpIdentifier("Morio's Lab - Psycho Taxi Arcade Machine", "", "", LevelId.Hub, Levels.Index.level_psycho_taxi, LevelId.L20_PsychoTaxi, new Vector3(-785f, 70f, 615f), new Vector3(-400f, 60f, 20f), 0, 0, true, true, "MEGA_RAN_-_TAXI_REFERENCE", "Background Sea and Sky"),

            // Morio's Island Warps
            new WarpIdentifier("Morio's Island - Morio's Garage Entrance", "Morio's Home - Morio's Garage Exit", "", LevelId.L3_MoriosHome, Levels.Index.noone, LevelId.noone, new Vector3(-10f, 0f, 465f), new Vector3(-20f, 10f, -310f), 90, 3, false, false, "SoundtrackMoriosHomeInternal", "Background Morio's Home Internal"),
            // Morio's Home Warps
            new WarpIdentifier("Morio's Home - Morio's Garage Exit", "Morio's Island - Morio's Garage Entrance", "", LevelId.L3_MoriosHome, Levels.Index.noone, LevelId.noone, new Vector3(-20f, 10f, -300f), new Vector3(-10f, 0f, 460f), 90, 0, true, true, "SoundtrackMoriosHome", "Background Morio's Island"),
            new WarpIdentifier("Morio's Home - Door to Weird Tunnels", "Weird Tunnels - Entrance Door", "", LevelId.L3_MoriosHome, Levels.Index.noone, LevelId.noone, new Vector3(85.10001f, 30.41628f, -409.6847f), new Vector3(-645f, 55f, -645f), 0, 4, false, false, "SoundtrackMoriosHomeInternal", "Background Morio's Home Internal"),
            new WarpIdentifier("Morio's Home - Morio's Lab Portal", "", "", LevelId.L3_MoriosHome, Levels.Index.level_HubDEMO, LevelId.Hub, new Vector3(83.79849f, 70.41628f, -847.1847f), new Vector3(-650f, 30f, 720f), 135, 2, true, false, "SoundtrackHubInside", "Background Soffitto Laboratorio"),
            // Weird Tunnel Warps
            new WarpIdentifier("Weird Tunnels - Entrance Door", "Morio's Home - Door to Weird Tunnels", "Weird Tunnels - Exit", LevelId.L3_MoriosHome, Levels.Index.noone, LevelId.noone, new Vector3(-670f, 10f, -645f), new Vector3(85f, 30f, -445f), 90, 3, false, false, "SoundtrackMoriosHomeInternal", "Background Morio's Home Internal"),
            new WarpIdentifier("Weird Tunnels - Exit Door", "Morio's Home - Door to Weird Tunnels", "Weird Tunnels - Exit", LevelId.L3_MoriosHome, Levels.Index.noone, LevelId.noone, new Vector3(-260f, 0f, -645f), new Vector3(85f, 30f, -445f), 90, 3, false, false, "SoundtrackMoriosHomeInternal", "Background Morio's Home Internal"),

            // Bombeach Warps
            new WarpIdentifier("Bombeach - Cave Entrance", "Cave - Exit", "", LevelId.L1_Bombeach, Levels.Index.noone, LevelId.noone, new Vector3(838.99f, -15f, 778.99f), new Vector3(-630f, 10f, -152.5f), 90, 0, true, false, "SoundtrackBonusLevel", "Background Bonus Level"),
            new WarpIdentifier("Bombeach - Morio's Lab Portal Near Entrance", "Morio's Lab - Portal to Bombeach", "Bombeach - Morio's Lab Portal", LevelId.L1_Bombeach, Levels.Index.level_HubDEMO, LevelId.Hub, new Vector3(275f, 5f, 780f), new Vector3(-650f, 50f, 640f), -135, 2, true, false, "SoundtrackHubInside", "Background Soffitto Laboratorio"),
            new WarpIdentifier("Bombeach - Hat World Entrance", "Bombeach Hat World - Exit", "", LevelId.L1_Bombeach, Levels.Index.noone, LevelId.noone, new Vector3(679.8076f, 15f, 758.8076f), new Vector3(-625f, 0f, 800f), 0, 1, false, false, "SoundtrackHatShop", "Background Bombeach"),
            new WarpIdentifier("Bombeach Hat World - Exit", "Bombeach - Hat World Entrance", "", LevelId.L1_Bombeach, Levels.Index.noone, LevelId.noone, new Vector3(-630f, 0f, 800f), new Vector3(674.8578f, 15f, 753.8578f), 135, 0, true, true, "SoundtrackBombeach", "Background Bombeach"),
            new WarpIdentifier("Bombeach - Morio's Lab Portal on Summit", "Morio's Lab - Portal to Bombeach", "Bombeach - Morio's Lab Portal", LevelId.L1_Bombeach, Levels.Index.level_HubDEMO, LevelId.Hub, new Vector3(640f, 95f, 830f), new Vector3(-650f, 50f, 640f), -135, 2, true, false, "SoundtrackHubInside", "Background Soffitto Laboratorio"),
            new WarpIdentifier("Cave - Exit", "Bombeach - Cave Entrance", "", LevelId.L1_Bombeach, Levels.Index.noone, LevelId.noone, new Vector3(-630f, -10f, -94.76f), new Vector3(850f, -15f, 790f), -45, 0, true, true, "SoundtrackBombeach", "Background Bombeach"),

            // Arcade Panik Warps
            new WarpIdentifier("Arcade Plaza - Hat World Entrance", "Arcade Plaza Hat World - Exit", "", LevelId.L4_ArcadePanik, Levels.Index.noone, LevelId.noone, new Vector3(-763f, 0f, 1.136496E-06f), new Vector3(-1110f, 80f, -1.486187E-05f), 180, 1, false, false, "SoundtrackHatShop", "Background Sea and Sky - Sunset"),
            new WarpIdentifier("Arcade Plaza Hat World - Exit", "Arcade Plaza - Hat World Entrance", "", LevelId.L4_ArcadePanik, Levels.Index.noone, LevelId.noone, new Vector3(-1105f, 80f, -1.529899E-05f), new Vector3(-770f, 0f, 1.748456E-06f), 180, 3, true, true, "SoundtrackArcadePanik", "Background Sea and Sky - Sunset"),
            new WarpIdentifier("Arcade Plaza - Arcade Panik Entrance", "Arcade Panik - Arcade Exit", "", LevelId.L4_ArcadePanik, Levels.Index.noone, LevelId.noone, new Vector3(-840f, 0f, 0f), new Vector3(-500f, 10f, 0f), 0, 0, true, true, "SoundtrackArcadePanik", "Background Panik Arcade Internal"),
            new WarpIdentifier("Arcade Panik - Arcade Exit", "Arcade Plaza - Arcade Panik Entrance", "", LevelId.L4_ArcadePanik, Levels.Index.noone, LevelId.noone, new Vector3(-518f, 10f, 0f), new Vector3(-815f, 0f, 0f), 0, 3, true, true, "SoundtrackArcadePanik", "Background Sea and Sky - Sunset"),
            new WarpIdentifier("Arcade Panik - Pinball Entrance", "Flipper - Hole in Tower Exit", "", LevelId.L4_ArcadePanik, Levels.Index.noone, LevelId.noone, new Vector3(-240f, 85f, 0f), new Vector3(-145f, 380f, 960f), 0, 4, true, true, "SoundtrackArcadePanik", "Background Panik Arcade Internal"),
            new WarpIdentifier("Arcade Panik - Morio's Lab Portal in Bowling Alley", "Morio's Lab - Arcade Panik Portal", "Arcade Panik - Portal to Morio's Lab", LevelId.L4_ArcadePanik, Levels.Index.level_HubDEMO, LevelId.Hub, new Vector3(-442.5f, 10f, 55f), new Vector3(-765f, 50f, 680f), 0, 2, true, false, "SoundtrackHubInside", "Background Soffitto Laboratorio"),
            new WarpIdentifier("Arcade Panik - Morio's Lab Portal on Center Platform", "Morio's Lab - Arcade Panik Portal", "Arcade Panik - Portal to Morio's Lab", LevelId.L4_ArcadePanik, Levels.Index.level_HubDEMO, LevelId.Hub, new Vector3(90f, 5f, -20f), new Vector3(-765f, 50f, 680f), 0, 2, true, false, "SoundtrackHubInside", "Background Soffitto Laboratorio"),
            new WarpIdentifier("Flipper - Hole in Tower Exit", "Arcade Panik - Pinball Entrance", "Flipper - Exit", LevelId.L4_ArcadePanik, Levels.Index.noone, LevelId.noone, new Vector3(-130f, -10f, 960f), new Vector3(-225f, 85f, 0f), 0, 0, true, true, "SoundtrackArcadePanik", "Background Panik Arcade Internal"),
            new WarpIdentifier("Flipper - Pipe Before Final Challenge Exit", "Arcade Panik - Pinball Entrance", "Flipper - Exit", LevelId.L4_ArcadePanik, Levels.Index.noone, LevelId.noone, new Vector3(-855f, 162.5f, 960f), new Vector3(-225f, 85f, 0f), 0, 0, true, true, "SoundtrackArcadePanik", "Background Panik Arcade Internal"),
            new WarpIdentifier("Flipper - Pipe After Final Challenge Exit", "Arcade Panik - Pinball Entrance", "Flipper - Exit", LevelId.L4_ArcadePanik, Levels.Index.noone, LevelId.noone, new Vector3(-1225f, 332.5f, 955f), new Vector3(-225f, 85f, 0f), 0, 0, true, true, "SoundtrackArcadePanik", "Background Panik Arcade Internal"),

            // Pizza Time Warps
            new WarpIdentifier("Pizza Time - Hat World Entrance", "Pizza Time Hat World - Exit", "", LevelId.L2_PizzaTime, Levels.Index.noone, LevelId.noone, new Vector3(-123.5f, 0f, -8.741669f), new Vector3(1067.5f, 0f, 0f), 0, 1, false, false, "SoundtrackHatShop", "Background Pizza Time"),
            new WarpIdentifier("Pizza Time Hat World - Exit", "Pizza Time - Hat World Entrance", "", LevelId.L2_PizzaTime, Levels.Index.noone, LevelId.noone, new Vector3(1062.5f, 0f, 0f), new Vector3(-120f, 0f, -2.679491f), 300, 0, true, true, "SoundtrackPizzaTime", "Background Pizza Time"),
            new WarpIdentifier("Pizza Time - Sewer Entrance", "Pizza Time Sewer - Exit", "", LevelId.L2_PizzaTime, Levels.Index.noone, LevelId.noone, new Vector3(-567.5f, 5f, 180f), new Vector3(155f, 10f, 475f), -90, 6, true, true, "SoundtrackPizzaTime", "Background Pizza Time"),
            new WarpIdentifier("Pizza Time Sewer - Exit", "Pizza Time - Sewer Entrance", "", LevelId.L2_PizzaTime, Levels.Index.noone, LevelId.noone, new Vector3(155f, 10f, 460f), new Vector3(-560f, 30f, 185f), -90, 0, true, true, "SoundtrackPizzaTime", "Background Pizza Time"),
            new WarpIdentifier("Pizza Time - 400° Oven", "400° - Exit", "", LevelId.L2_PizzaTime, Levels.Index.noone, LevelId.noone, new Vector3(-425.26f, 30f, 210f), new Vector3(980f, 10f, -750f), 180, 3, true, false, "SoundtrackBonusLevel", "Background Bonus Level"),
            new WarpIdentifier("400° - Exit", "Pizza Time - 400° Oven", "", LevelId.L2_PizzaTime, Levels.Index.noone, LevelId.noone, new Vector3(990f, 10f, -750f), new Vector3(-435f, 30f, 210f), 180, 0, true, true, "SoundtrackPizzaTime", "Background Pizza Time"),
            new WarpIdentifier("Pizza Time - 600° Oven", "600° - Exit", "", LevelId.L2_PizzaTime, Levels.Index.noone, LevelId.noone, new Vector3(-410f, 30f, -5f), new Vector3(460f, 10f, -810f), 180, 4, true, false, "SoundtrackBonusLevel", "Background Bonus Level"),
            new WarpIdentifier("600° - Exit", "Pizza Time - 600° Oven", "", LevelId.L2_PizzaTime, Levels.Index.noone, LevelId.noone, new Vector3(470f, 10f, -810f), new Vector3(-402.5f, 30f, 2.5f), -45, 0, true, true, "SoundtrackPizzaTime", "Background Pizza Time"),
            new WarpIdentifier("Pizza Time - 900° Oven", "900° - Exit", "", LevelId.L2_PizzaTime, Levels.Index.noone, LevelId.noone, new Vector3(-876.69f, 70f, 281.18f), new Vector3(-330f, 10f, -790f), -90, 5, true, false, "SoundtrackBonusLevel", "Background Bonus Level"),
            new WarpIdentifier("900° - Exit", "Pizza Time - 900° Oven", "", LevelId.L2_PizzaTime, Levels.Index.noone, LevelId.noone, new Vector3(-330f, 10f, -800f), new Vector3(-870f, 70f, 279.9f), 15, 0, true, true, "SoundtrackPizzaTime", "Background Pizza Time"),

            // Rocket Warps
            new WarpIdentifier("Mosk's Rocket - Exit to Granny's Island", "Granny's Island - Rocket Entrance", "", LevelId.L16_Rocket, Levels.Index.level_HubDEMO, LevelId.Hub, new Vector3(-1150f, 0f, -1120f), new Vector3(190f, 20f, -30f), 90, 0, true, true, "SoundtrackHubOutside", "Background Sea and Sky"),
            new WarpIdentifier("Mosk's Rocket - Welcoming Climbs Portal", "Welcoming Climbs - Mosk's Rocket Portal", "", LevelId.L16_Rocket, Levels.Index.noone, LevelId.noone, new Vector3(-1045f, 30f, -1135f), new Vector3(-490f, 10f, -1120f), 0, 4, true, true, "SoundtrackRocket", "Background Bonus Level", LevelId.L3_MoriosHome),
            new WarpIdentifier("Welcoming Climbs - Mosk's Rocket Portal", "Mosk's Rocket - Welcoming Climbs Portal", "", LevelId.L16_Rocket, Levels.Index.noone, LevelId.noone, new Vector3(-500f, 10f, -1120f), new Vector3(-1045f, 30f, -1120f), -90, 0, true, true, "SoundtrackRocket", "Background Bonus Level", LevelId.L16_Rocket),
        };

        public static Dictionary<string, string> WarpRedirects = new()
        {
            //{"Granny's Island - Morio's Lab Front Door", "Bombeach - Hat World Entrance"},
            //{"Cave - Exit", "Morio's Lab - Portal to Bombeach"},
            //{"Bombeach - Morio's Lab Portal", "Cave - Exit"},
            //{"Bombeach - Hat World Entrance", "Morio's Home - Portal to Morio's Lab"},
            //{"Morio's Lab - Portal to Morio's Island", "Bombeach - Morio's Lab Portal Near Entrance"},
            //{"Mosk's Rocket - Portal to Welcoming Climbs", "Morio's Home - Door to Weird Tunnels"},
            //{"Weird Tunnels - Entrance Door", "Crash Again - Exit"},
            //{"Granny's Island - Crash Again Entrance", "Granny's Island - Fecal Matters House"},
            //{"Granny's Island - Gym Gears Entrance", "Law Firm - Exit"},
            //{"Granny's Island - Law Firm Roof Entrance", "Granny's Island - Gym Gears Entrance"},
            //{"Granny's Island Hat World - Exit", "Granny's Island Hat World - Exit"},
            //{"Morio's Lab - Front Door", "Granny's Island - Hat World Entrance"},
            //{"Granny's Island - Hat World Entrance", "Morio's Home - Door to Weird Tunnels"},
            //{"Morio's Home - Door to Weird Tunnels", "Granny's Island - Gym Gears Entrance"},
            //{"Morio's Lab - Portal to Morio's Island", "Morio's Home - Morio's Garage Exit"},
            //{"Morio's Island - Morio's Garage Entrance", "Morio's Lab - Portal to Morio's Island"},
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
                   (OriginalTargetLevelId != LevelId.noone || ( // The following only matter for in-level warps normally
                   Mathf.Approximately(MoveTaxiHere.x, otherWarp.MoveTaxiHere.x) &&
                   Mathf.Approximately(MoveTaxiHere.y, otherWarp.MoveTaxiHere.y) &&
                   Mathf.Approximately(MoveTaxiHere.z, otherWarp.MoveTaxiHere.z) &&
                   Mathf.Approximately(Rotation, otherWarp.Rotation) &&
                   (otherWarp.Zone == -1 || Zone == -1 || Zone == otherWarp.Zone) &&
                   DesiredLightState == otherWarp.DesiredLightState &&
                   DesiredWaterState == otherWarp.DesiredWaterState));
        }

        public static string IdentifyOriginalWarp(PortalScript warp)
        {
            var warpIdentifier = new WarpIdentifier(warp);
            var knownWarp = KnownWarps.FirstOrDefault(o => o.Equals(warpIdentifier));
            if (knownWarp != null && knownWarp.Zone != -1 && !string.IsNullOrEmpty(knownWarp.SongChange) && !string.IsNullOrEmpty(knownWarp.BackgroundChange))
            {
                return $"Known Warp: {knownWarp.Name}" + (string.IsNullOrEmpty(knownWarp.ExitGroup)
                        ? string.Empty
                        : $" (Group: {knownWarp.ExitGroup})");
            }

            knownWarp = KnownWarps.FirstOrDefault(o => o.ProbablyEquals(warpIdentifier));
#if DEBUG
            var warpName = knownWarp?.Name ?? string.Empty;
            var linkedExit = knownWarp?.LinkedExit ?? string.Empty;
            var groupName = knownWarp?.ExitGroup ?? string.Empty;
            var moveTaxiHere = warp.targetLevelId != LevelId.Hub ? warp.moveTaxiHere : PortalScript.latestPortalHub_Pos;
            var rotateTaxiY = warp.targetLevelId != LevelId.Hub ? warp.rotateTaxiY : PortalScript.latestPortalHub_RotationY;
            var zone = warp.targetLevelId != LevelId.Hub ? (warp.desiredZoneId != -1 ? warp.desiredZoneId : (warp.targetLevelId == LevelId.noone ? ZoneMaster.currentZoneId : 0)) : PortalScript.latestHubZoneId;
            var water = warp.targetLevelId != LevelId.Hub ? warp.desiredWaterState : PortalScript.latestHubWaterState;
            var light = warp.targetLevelId != LevelId.Hub ? warp.desiredLightState : PortalScript.latestHubLightState;
            var song = warp.targetLevelId != LevelId.Hub ? warp.songChange : PortalScript.latestHubSoundtrack;
            var bg = warp.targetLevelId != LevelId.Hub ? warp.backgroundChange : PortalScript.latestHubBackground;

            if (warp.targetLevelId == LevelId.noone)
            {
                if (string.IsNullOrEmpty(song))
                {
                    song = GameplayMaster.instance.levelSoundtrack;
                }
                else if (song.Equals("default"))
                {
                    song = GameplayMaster.instance.defaultLevelSoundtrack;
                }

                if (string.IsNullOrEmpty(bg))
                {
                    bg = BackgroundMaster.instance.name;
                }
                else if (bg.Equals("default"))
                {
                    bg = GameplayMaster.instance.initialBackground;
                }
            }
            GUIUtility.systemCopyBuffer = $"new WarpIdentifier(\"{warpName}\", \"{linkedExit}\", \"{groupName}\", LevelId.{GameplayMaster.instance.levelId}, Levels.Index.{warp.targetLevel}, LevelId.{warp.targetLevelId}, new Vector3({warp.portalStartPosition.x}f, {warp.portalStartPosition.y}f, {warp.portalStartPosition.z}f), new Vector3({moveTaxiHere?.x ?? 0}f, {moveTaxiHere?.y ?? 0}f, {moveTaxiHere?.z ?? 0}f), {rotateTaxiY ?? 0}, {zone}, {light.ToString().ToLower()}, {water.ToString().ToLower()}, \"{song}\", \"{bg}\""+(warp.kaizoLevelId != LevelId.noone ? $", LevelId.{warp.kaizoLevelId}" : string.Empty)+"),";
#endif

            if (knownWarp != null)
            {
                return $"(Probably?) Known Warp: {knownWarp.Name}";
            }

            if (warp.targetLevel != Levels.Index.noone)
                return $"Unknown Portal to {warp.targetLevelId}";

            return $"Unknown TaxiWarp to {warp.moveTaxiHere} with rotation {warp.rotateTaxiY}";
        }

        public static WarpIdentifier GetRedirectedWarp(PortalScript warp)
        {
            var warpId = new WarpIdentifier(warp);
            return KnownWarps.FirstOrDefault(o => o.Equals(warpId));
        }

        public static bool GroupExits = false;
        public static bool RedirectWarp(PortalScript warp)
        {
            var knownWarp = GetRedirectedWarp(warp);
            if (knownWarp == null)
                return false;
            WarpRedirects.TryGetValue(GroupExits ? knownWarp.Group : knownWarp.Name, out var redirectedWarpName);

            if (!string.IsNullOrEmpty(redirectedWarpName))
            {
                var redirectedWarp = KnownWarps.FirstOrDefault(o => o.Name.Equals(redirectedWarpName));
                if (redirectedWarp == null)
                {
                    Plugin.Log($"ERROR: COULD NOT PROPERLY REDIRECT TO {redirectedWarpName}");
                    throw new KeyNotFoundException($"Could not find warp: \"{redirectedWarpName}\"");
                }

                warp.targetLevel = redirectedWarp.OriginalTargetLevel;
                warp.targetLevelId = redirectedWarp.OriginalTargetLevelId;

                if (redirectedWarp.OriginalTargetLevelId == LevelId.Hub &&
                    GameplayMaster.instance.levelId != LevelId.Hub)
                {
                    APPortalManager.QueuedSubwarp = redirectedWarp;
                }
                else if (redirectedWarp.OriginalTargetLevelId == LevelId.noone &&
                    GameplayMaster.instance.levelId != redirectedWarp.OriginalLevelId)
                {
                    APPortalManager.QueuedSubwarp = redirectedWarp;
                    warp.targetLevel = LevelConverter.GetLevelIndex(redirectedWarp.OriginalLevelId);
                    warp.targetLevelId = redirectedWarp.OriginalLevelId;
                }
                else if (redirectedWarp.OriginalTargetLevelId == GameplayMaster.instance.levelId)
                {
                    warp.targetLevel = Levels.Index.noone;
                    warp.targetLevelId = LevelId.noone;
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
