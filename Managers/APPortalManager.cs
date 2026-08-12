using System;
using System.Linq;
using Febucci.UI;
using UnityEngine;
using YellowTaxiAP.Archipelago;
using YellowTaxiAP.Behaviours;
using YellowTaxiAP.Helpers;
using static Data;
using Random = UnityEngine.Random;

namespace YellowTaxiAP.Managers
{
    public class APPortalManager
    {
        public APPortalManager()
        {
            On.PlayerScript.Start += PlayerScript_Start;
            On.PortalScript.PortalOpenedSet += PortalScript_PortalOpenedSet;
            On.PortalScript.Awake += PortalScript_Awake;
            On.PortalScript.CoroutineGo += PortalScript_CoroutineGo;
            On.PortalScript.GoToLevel += PortalScript_GoToLevel;
            On.PortalScript.OnTriggerEnter += PortalScript_OnTriggerEnter;
            On.PortalScript.PortalIslandToLabCoroutine += PortalScript_PortalIslandToLabCoroutine;
            On.PortalScript.PortalOpenStart += PortalScript_PortalOpenStart;
            On.PortalScript.CostUpdateTry += PortalScript_CostUpdateTry;
            On.PortalScript.UpdatePortalToLevelName += PortalScript_UpdatePortalToLevelName;
            On.PortalScript.SetupDataForLevelComeback += PortalScript_SetupDataForLevelComeback;
            On.MorioDreamMachineScript.AnimationCoroutine += MorioDreamMachineScript_AnimationCoroutine;
            On.MorioDreamMachineScript.Start += MorioDreamMachineScript_Start;
            On.MorioDreamMachineScript.MachineReady += MorioDreamMachineScript_MachineReady;
            On.LoadingScreenScript.WelcomeSetup += LoadingScreenScript_WelcomeSetup;
            On.Colors.PortalTextureGet += Colors_PortalTextureGet;
            On.PsychoTaxiCabinetScript.Awake += PsychoTaxiCabinetScript_Awake;
        }

        private void PsychoTaxiCabinetScript_Awake(On.PsychoTaxiCabinetScript.orig_Awake orig, PsychoTaxiCabinetScript self)
        {
            orig(self);
            try
            {
                AssetMaster.AddTexture2D(self.turnedOnMat.mainTexture as Texture2D);
            }
            catch (ArgumentException)
            {
                // Ignore, if already added it's chill
            }
        }

        public Sprite Colors_PortalTextureGet(On.Colors.orig_PortalTextureGet orig, LevelId levelId)
        {
            if (levelId == LevelId.L20_PsychoTaxi)
            {
                var resource = AssetMaster.GetTexture2D("Cabinato Gigante Psycho Taxi Acceso");
                var sprite = Sprite.Create(resource, new Rect(199, 72, 175, 175), new Vector2(0.5f, 0.5f), 1);
                return sprite;
            }

            return orig(levelId);
        }

        private void PortalScript_SetupDataForLevelComeback(On.PortalScript.orig_SetupDataForLevelComeback orig, PortalScript self, bool saveToDisk, bool forcePortalDesiredWaterState)
        {
            orig(self, saveToDisk, forcePortalDesiredWaterState);
            if (IsLevelIdHub(GameplayMaster.instance.levelId))
            {
                APMenuManager.CurrentAreaFromIsland = !MapArea.IsPlayerInsideLab();
            }
        }

        public static readonly LevelId[] OriginalPortalLevelOrder =
        [
            // Lab Levels
            LevelId.L3_MoriosHome,
            LevelId.L1_Bombeach,
            LevelId.L4_ArcadePanik,
            LevelId.L2_PizzaTime,
            LevelId.L5_ToslaOffices,
            LevelId.L9_City,
            LevelId.L10_CrashTestIndustries,
            LevelId.L12_MoriosMind,
            LevelId.L13_StarmanCastle,
            LevelId.L14_ToslaHQ,
            LevelId.L15_Moon,
            // Granny's Levels
            LevelId.L6_Gym,
            LevelId.L7_PoopWorld,
            LevelId.L8_Sewers,
            // Bonus Levels
            LevelId.L16_Rocket,
            LevelId.L17_TimeAttack01,
            LevelId.L18_TimeAttack02,
            LevelId.L19_TimeAttack03,
            LevelId.L20_PsychoTaxi,
        ];

        public static LevelId[] RandomizedPortalLevelOrder = OriginalPortalLevelOrder;

        public static LevelId[] GetRandomizedPortalLevelOrder()
        {
            var levels = new LevelId[OriginalPortalLevelOrder.Length];
            Array.Copy(OriginalPortalLevelOrder, levels, OriginalPortalLevelOrder.Length);
            Array.Sort(levels, (_, _) => Random.RandomRangeInt(-1, 2));
            var s = "Levels randomized! Mapping:\n";
            for (var i = 0; i < levels.Length; i++)
            {
                s += OriginalPortalLevelOrder[i] + " -> " + levels[i] + "\n";
            }

            Plugin.Log(s.TrimEnd('\n'), false);
            
            return levels;
        }

        public static LevelId GetRandomizedLevelId(LevelId originalLevel, bool ignoreExcluded = false)
        {
            if (OriginalPortalLevelOrder.Contains(originalLevel))
            {
                var randomizedLevel =
                    RandomizedPortalLevelOrder[Array.IndexOf(OriginalPortalLevelOrder, originalLevel)];
                if (!ignoreExcluded || randomizedLevel != LevelId.L11_HubDemo)
                    return randomizedLevel;
            }
            return originalLevel;
        }

        private void PortalScript_UpdatePortalToLevelName(On.PortalScript.orig_UpdatePortalToLevelName orig, PortalScript self)
        {
            if (string.IsNullOrEmpty(self._name) || self._name.Any(c => char.IsDigit(c)))
                orig(self);
            if (!string.IsNullOrEmpty(self._name) && self.kaizoLevelId == LevelId.noone)
            {
                self.nameTextAnimator.GetComponent<TextAnimatorPlayer>().useTypeWriter = false;
                if (self.PortalIsLevelPortal && !self.PortalIsAlreadyOpened)
                {
                    self._name = self.nameText.text = "???";
                }
                else if (self._name.Contains("?") || self._name.Any(c => char.IsDigit(c)))
                {
                    self._name = self.nameText.text = levelDataList[(int)GetRandomizedLevelId(self.targetLevelId, true)].GetName();
                }
            }
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
                if (DebugLocationHelper.Enabled)
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
            APSaveController.PortalSave.SetLevelPortalUnlocked(self.targetLevelId);
            SetPortalToRandomized(self);
            orig(self);
            self.UpdatePortalToLevelName();
        }

        private void PortalScript_PortalOpenedSet(On.PortalScript.orig_PortalOpenedSet orig, PortalScript self)
        {
            SetPortalToRandomized(self);
            orig(self);
            self.UpdatePortalToLevelName();
        }

        private void SetPortalToRandomized(PortalScript self)
        {
            if (self.PortalIsLevelPortal)
            {
                var randomizedLevelId = GetRandomizedLevelId(self.targetLevelId, true);
                self._name = self.nameText.text = levelDataList[(int)randomizedLevelId].GetName();
                self.levelImage.sprite = Colors.PortalTextureGet(randomizedLevelId);
            }
        }

        private void PlayerScript_Start(On.PlayerScript.orig_Start orig, PlayerScript self)
        {
            orig(self);
            if (QueuedSubwarp == null || (GameplayMaster.selfRespawnRecordingDataList.Count > 0 && QueuedSubwarpLoaded))
                return;

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

            //QueuedSubwarp = null;
            QueuedSubwarpLoaded = true;
        }

        private void LoadingScreenScript_WelcomeSetup(On.LoadingScreenScript.orig_WelcomeSetup orig, LevelId targetLevelId, string levelName, int gearsCollected, int maxGearsInsideLevel, bool enableCameraLevelIntro)
        {
            if (QueuedSubwarpLoaded)
            {
                QueuedSubwarp = null;
            }
            orig(targetLevelId, levelName, gearsCollected, maxGearsInsideLevel, enableCameraLevelIntro && QueuedSubwarp == null);
        }

        private System.Collections.IEnumerator PortalScript_PortalIslandToLabCoroutine(On.PortalScript.orig_PortalIslandToLabCoroutine orig, PortalScript self)
        {
            Plugin.Log("Portal Coroutine: Island to Lab");
            return orig(self);
        }

        private static WarpIdentifier _queuedSubwarp;
        public static bool QueuedSubwarpLoaded { get; private set; }

        public static WarpIdentifier QueuedSubwarp
        {
            get => _queuedSubwarp;
            set
            {
                _queuedSubwarp = value;
                QueuedSubwarpLoaded = false;
            }
        }

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
            var randomizedLevelId = GetRandomizedLevelId(targetLevelId, true);
            Plugin.Log($"PortalWarp to {targetLevelId} with index {levelSceneIndex} ({(int)levelSceneIndex}). Per randomization, actually leading to {randomizedLevelId}");
            orig(LevelConverter.GetLevelIndex(randomizedLevelId), randomizedLevelId);
        }

        private System.Collections.IEnumerator PortalScript_CoroutineGo(On.PortalScript.orig_CoroutineGo orig, PortalScript self, int levelIndex)
        {
            Plugin.Log($"Portal Coroutine: Warp to {self.targetLevelId} with index {levelIndex}. Portal index {self.targetLevel}");
            return orig(self, levelIndex);
        }

        private void PortalScript_Awake(On.PortalScript.orig_Awake orig, PortalScript self)
        {
            // Bombeach can be in a variable spot, otherwise just use standard
            if ((GetRandomizedLevelId(self.targetLevelId) == LevelId.L1_Bombeach && Plugin.SlotData.Goal == YTGVSlotData.GoalType.Bombeach) ||
                (self.targetLevelId == LevelId.L5_ToslaOffices && Plugin.SlotData.Goal == YTGVSlotData.GoalType.ToslaOffices) ||
                (self.targetLevelId == LevelId.L9_City && Plugin.SlotData.Goal == YTGVSlotData.GoalType.MauriziosCity) ||
                (self.targetLevelId == LevelId.L14_ToslaHQ && Plugin.SlotData.Goal == YTGVSlotData.GoalType.Moon))
            {
                Data.levelDataList[(int)self.targetLevelId].levelCost = Plugin.SlotData.GoalPortalCost;
            }

            self.hubPortalForceEnabled = true;
#if DEBUG
            if (DebugLocationHelper.Enabled)
            {
                if (self.PortalIsLevelPortal)
                {
                    levelDataList[(int)self.targetLevelId].levelCost = -1;
                    GetLevel(self.targetLevelId).everOpened = true;
                    self.CostUpdateTry();
                    orig(self);
                    self.UpdatePortalToLevelName();
                    return;
                }

                if (self.kaizoLevelId != LevelId.noone)
                {
                    orig(self);
                    return;
                }
            }
#endif
            if (self.PortalIsLevelPortal)
            {
                // Delete portals that are excluded
                if (GetRandomizedLevelId(self.targetLevelId) == LevelId.L11_HubDemo)
                {
                    // Disable level cost. This fixes issues with main menu.
                    // -1 is later used (by me) as a magic number to prevent populating the minimap with these disabled portals
                    levelDataList[(int)self.targetLevelId].levelCost = -1;
                    GetLevel(self.targetLevelId).everOpened = false;
                    self.CostUpdateTry();
                    ObjectHelper.DestroyRecursive(self.transform);
                    return;
                }

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
                        Plugin.Log($"Checking if {self.targetLevelId} portal ({self.gameObject.name}) should be open {APSaveController.PortalSave.IsLevelPortalUnlocked(self.targetLevelId)}");
                        GetLevel(self.targetLevelId).everOpened = APSaveController.PortalSave.IsLevelPortalUnlocked(self.targetLevelId);
                        break;
                }
            }
            else if (self.kaizoLevelId != LevelId.noone && !RandomizedPortalLevelOrder.Contains(self.kaizoLevelId))
            {
                ObjectHelper.DestroyRecursive(self.transform);
            }

            orig(self);
            self.UpdatePortalToLevelName();
            self.CostUpdateTry();
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

        public static LevelId GetLevelId(Levels.Index index)
        {
            return index switch
            {
                Levels.Index.noone => LevelId.noone,
                Levels.Index.level_hub => LevelId.Hub,
                Levels.Index.level_bombeach => LevelId.L1_Bombeach,
                Levels.Index.level_PizzaTime => LevelId.L2_PizzaTime,
                Levels.Index.level_MoriosHome => LevelId.L3_MoriosHome,
                Levels.Index.level_PanikArcade => LevelId.L4_ArcadePanik,
                Levels.Index.level_ToslaOffices => LevelId.L5_ToslaOffices,
                Levels.Index.level_Gym => LevelId.L6_Gym,
                Levels.Index.level_PoopWorld => LevelId.L7_PoopWorld,
                Levels.Index.level_Sewers => LevelId.L8_Sewers,
                Levels.Index.level_City => LevelId.L9_City,
                Levels.Index.level_CrashTestIndustries => LevelId.L10_CrashTestIndustries,
                Levels.Index.level_MoriosMind => LevelId.L12_MoriosMind,
                Levels.Index.level_StarmanCastle => LevelId.L13_StarmanCastle,
                Levels.Index.level_ToslaHq => LevelId.L14_ToslaHQ,
                Levels.Index.level_Moon => LevelId.L15_Moon,
                Levels.Index.level_Rocket => LevelId.L16_Rocket,
                Levels.Index.level_time_attack_01 => LevelId.L17_TimeAttack01,
                Levels.Index.level_time_attack_02 => LevelId.L18_TimeAttack02,
                Levels.Index.level_time_attack_03 => LevelId.L19_TimeAttack03,
                Levels.Index.level_psycho_taxi => LevelId.L20_PsychoTaxi,
                _ => LevelId.noone
            };
        }
    }
}
