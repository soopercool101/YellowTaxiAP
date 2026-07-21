using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YellowTaxiAP.Archipelago;
using YellowTaxiAP.Behaviours;
using YellowTaxiAP.Helpers;
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
            On.PortalScript.CostUpdateTry += PortalScript_CostUpdateTry;
            On.MorioDreamMachineScript.AnimationCoroutine += MorioDreamMachineScript_AnimationCoroutine;
            On.MorioDreamMachineScript.Start += MorioDreamMachineScript_Start;
            On.MorioDreamMachineScript.MachineReady += MorioDreamMachineScript_MachineReady;
            On.LoadingScreenScript.WelcomeSetup += LoadingScreenScript_WelcomeSetup;
        }

        private System.Collections.IEnumerator MorioDreamMachineScript_AnimationCoroutine(On.MorioDreamMachineScript.orig_AnimationCoroutine orig, MorioDreamMachineScript self)
        {
            yield return orig(self);
            if (Plugin.SlotData.EarlyMoriosPassword)
            {
                Plugin.Log("Dream Machine activated");
                Plugin.ArchipelagoClient.SendLocation((long) Identifiers.NotableLocations.HubMoriosPassword);
            }
        }

        /// <summary>
        /// Nullable check on portalGObj, doesn't always exist in rando
        /// </summary>
        private void MorioDreamMachineScript_MachineReady(On.MorioDreamMachineScript.orig_MachineReady orig, MorioDreamMachineScript self)
        {
            self.myMorioPerson.gameObject.SetActive(true);
            if (self.portalGObj)
            {
                self.portalGObj?.SetActive(true);
            }
            self.SetAnimation(3);
            self.myMorioPerson.dialoguePickup = APAreaStateManager.MindPasswordReceived ? self.dialogueActiveAndPasswordRetrieved : self.dialogueActive;
        }

        /// <summary>
        /// Nullable check on portalGObj, doesn't always exist in rando
        /// </summary>
        private void MorioDreamMachineScript_Start(On.MorioDreamMachineScript.orig_Start orig, MorioDreamMachineScript self)
        {
            if (self.portalGObj)
            {
                self.portalGObj?.SetActive(false);
            }
            self.lightBulb.enabled = Data.morioMindDreamMachineUsedOnce[Data.gameDataIndex];
            self.labWallText.text = "----";
            if (Data.morioMindDreamMachineUsedOnce[Data.gameDataIndex])
                self.MachineReady();
            if (!MorioDreamMachineScript.justUpdatedPassword)
                return;
            self.StartCoroutine(self.JustUpdatedPasswordCorotuine());
        }

        private void PortalScript_CostUpdateTry(On.PortalScript.orig_CostUpdateTry orig, PortalScript self)
        {
            // Update gear portals
            if (self.kaizoLevelId == LevelId.noone)
            {
                orig(self);
            }
            else // Update bunny portals
            {
                self.kaizoEnabled = BunniesGetLevelCollectedNumber(self.kaizoLevelId) >= BunniesGetLevelMaxNumber(self.kaizoLevelId) || self.kaizoLevelId == LevelId.L16_Rocket;
#if DEBUG
                if (DebugLocationHelper.Enabled && (levelDataList[(int) self.kaizoLevelId].levelCost == -1 || self.kaizoLevelId == LevelId.L1_Bombeach))
                {
                    self.kaizoEnabled = true;
                }
#endif
                self._name = self.kaizoLevelId == LevelId.L16_Rocket ? "" : $"{BunniesGetLevelCollectedNumber(self.kaizoLevelId).ToString()}/{BunniesGetLevelMaxNumber(self.kaizoLevelId).ToString()}<sprite name=\"GoldenBunnyOutlined\">";
                self.nameText.text = self._name;
                self.nameText.rectTransform.anchoredPosition = self.kaizoEnabled ? new Vector2(0.0f, 8.0f) : new Vector2(0.0f, 3.5f);
            }
        }

        private void PortalScript_PortalOpenStart(On.PortalScript.orig_PortalOpenStart orig, PortalScript self)
        {
            var trueId = self.gameObject.GetComponent<TruePortalId>();
            APSaveController.MiscSave.SetLevelPortalUnlocked(trueId.OriginalLevel);
            orig(self);
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
            LightDirectionalScript.instance?.myLight?.enabled = QueuedSubwarp.DesiredLightState;
            WaterScript.instance?.WaterEnable = QueuedSubwarp.DesiredWaterState;
            self.myPausable.velBackup[0] = Vector3.zero;
            self.InstantCameraSet(0.0f);
            if (QueuedSubwarp.Zone >= 0)
                ZoneMaster.currentZoneId = QueuedSubwarp.Zone;
            self.TeleportComputeZoneMaster(self.transform);
            GameplayMaster.SelfRespawnClear();
            GameplayMaster.instance.recordingIndex = 0;
            GameplayMaster.selfRespawnRecordingDataList.Add(new GameplayMaster.SelfRespawnRecordingData());
            GameplayMaster.selfRespawnRecordingDataList[0].playerPosition = QueuedSubwarp.MoveTaxiHere;
            GameplayMaster.selfRespawnRecordingDataList[0].playerYAngle = QueuedSubwarp.Rotation;
            GameplayMaster.selfRespawnRecordingDataList[0].currentBackground = QueuedSubwarp.BackgroundChange;
            GameplayMaster.selfRespawnRecordingDataList[0].currentZoneId = QueuedSubwarp.Zone;
            GameplayMaster.selfRespawnRecordingDataList[0].currentMusic = QueuedSubwarp.SongChange;
            GameplayMaster.selfRespawnRecordingDataList[0].currentTimer = Mathf.Max(CheckpointScript.latestCheckpointTimerSet, GameplayMaster.instance.gameTimer, GameplayMaster.instance.gameTimerReset);
            GameplayMaster.selfRespawnRecordingDataList[0].waterState = WaterScript.instance && QueuedSubwarp.DesiredWaterState;
            GameplayMaster.selfRespawnRecordingDataList[0].lightState = LightDirectionalScript.instance && QueuedSubwarp.DesiredLightState;

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
            if ((self.targetLevelId == LevelId.L1_Bombeach && Plugin.SlotData.Goal == YTGVSlotData.GoalType.Bombeach) ||
                (self.targetLevelId == LevelId.L5_ToslaOffices && Plugin.SlotData.Goal == YTGVSlotData.GoalType.ToslaOffices) ||
                (self.targetLevelId == LevelId.L14_ToslaHQ && Plugin.SlotData.Goal == YTGVSlotData.GoalType.Moon))
            {
                Data.levelDataList[(int)self.targetLevelId].levelCost = Plugin.SlotData.GoalPortalCost;
            }

            self.hubPortalForceEnabled = true;
            self.gameObject.AddComponent<TruePortalId>(); // Keep track of unaltered portal values
#if DEBUG
            if (DebugLocationHelper.Enabled && self.PortalIsLevelPortal)
            {
                levelDataList[(int)self.targetLevelId].levelCost = -1;
                GetLevel(self.targetLevelId).everOpened = true;
                self.CostUpdateTry();
                orig(self);
                self.UpdatePortalToLevelName();
                return;
            }
#endif
            if (self.PortalIsLevelPortal || self.kaizoLevelId != LevelId.noone)
            {
                var target = self.kaizoLevelId != LevelId.noone ? self.kaizoLevelId : self.targetLevelId;
                // Delete portals that are excluded
                switch (target)
                {
                    case LevelId.L2_PizzaTime when Plugin.SlotData.Goal < YTGVSlotData.GoalType.ToslaOffices && Plugin.SlotData.RemovePostGoalPortals:
                    case LevelId.L4_ArcadePanik when Plugin.SlotData.Goal < YTGVSlotData.GoalType.ToslaOffices && Plugin.SlotData.RemovePostGoalPortals:
                    case LevelId.L5_ToslaOffices when Plugin.SlotData.Goal < YTGVSlotData.GoalType.ToslaOffices && Plugin.SlotData.RemovePostGoalPortals:
                    case LevelId.L6_Gym when Plugin.SlotData.GymGearsUnlockCondition == YTGVSlotData.LevelUnlockCondition.Exclude:
                    case LevelId.L7_PoopWorld when Plugin.SlotData.FecalMattersUnlockCondition == YTGVSlotData.LevelUnlockCondition.Exclude:
                    case LevelId.L8_Sewers when Plugin.SlotData.FlushedAwayUnlockCondition == YTGVSlotData.LevelUnlockCondition.Exclude:
                    case LevelId.L9_City: //when Plugin.SlotData.Goal < YTGVSlotData.GoalType.Moon && Plugin.SlotData.RemovePostGoalPortals:
                    case LevelId.L10_CrashTestIndustries: //when Plugin.SlotData.Goal < YTGVSlotData.GoalType.Moon && Plugin.SlotData.RemovePostGoalPortals:
                    case LevelId.L12_MoriosMind: //when Plugin.SlotData.Goal < YTGVSlotData.GoalType.Moon && Plugin.SlotData.RemovePostGoalPortals:
                    case LevelId.L13_StarmanCastle: //when Plugin.SlotData.Goal < YTGVSlotData.GoalType.Moon && Plugin.SlotData.RemovePostGoalPortals:
                    case LevelId.L14_ToslaHQ: //when Plugin.SlotData.Goal < YTGVSlotData.GoalType.Moon && Plugin.SlotData.RemovePostGoalPortals:
                    case LevelId.L15_Moon: //when Plugin.SlotData.Goal < YTGVSlotData.GoalType.Moon && Plugin.SlotData.RemovePostGoalPortals:
                        // Disable level cost. This fixes issues with main menu.
                        // -1 is later used (by me) as a magic number to prevent populating the minimap with these disabled portals
                        if (self.PortalIsLevelPortal)
                        {
                            levelDataList[(int)self.targetLevelId].levelCost = -1;
                            GetLevel(self.targetLevelId).everOpened = false;
                            self.CostUpdateTry();
                        }
                        ObjectHelper.DestroyRecursive(self.transform);
                        return;
                }
            }

            if (self.PortalIsLevelPortal)
            {
                // Get Portal Opened state from save
                switch (self.targetLevelId)
                {
                    // These levels are always open
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
}
