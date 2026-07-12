using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using I2.Loc;
using UnityEngine;
using UnityEngine.UIElements;
using YellowTaxiAP.Managers;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace YellowTaxiAP.Behaviours
{

    public class APTrapController : MonoBehaviour
    {
        public static Queue<Trap> TrapsToActivate = new();
        public static ulong Count = 0;
        public static float DefaultTrapDuration = 10f;
        public static int? OriginalFramerate;

        public static bool Armed;

        public static void ActivateTrap(string trapName, string trapLinkSource = null)
        {
            if (!Armed)
                return;

            var originalTrapName = trapName;
            if (trapName.EndsWith(" Trap", StringComparison.OrdinalIgnoreCase))
            {
                trapName = trapName.Substring(0, trapName.Length - 5);
            }

            Trap newTrap = null;
            switch (trapName.ToLowerInvariant())
            {
                case "explosion":
                case "bomb":
                case "tnt":
                case "tnt barrel":
                    newTrap = new BombTrap();
                    break;
                case "bald":
                case "no hat":
                    newTrap = new NoHatTrap();
                    break;
                case "burger hat":
                    newTrap = new BurgerHatTrap();
                    break;
                case "teleport":
                    newTrap = new TeleportTrap();
                    break;
                case "undo":
                    newTrap = new UndoTrap();
                    break;
                case "fake transition":
                    newTrap = new FakeTransitionTrap();
                    break;
                case "cutscene":
                    newTrap = new CutsceneTrap();
                    break;
                case "bullet time":
                    newTrap = new BulletTimeTrap();
                    break;
                case "slow":
                case "slow time":
                    newTrap = new SlowTimeTrap();
                    break;
                case "fast":
                case "fast time":
                    newTrap = new FastTimeTrap();
                    break;
                case "pixelate":
                case "pixellation": // sic, how it's typed on the sheet
                case "pixelation":
                case "144p":
                    newTrap = new PixelateTrap();
                    break;
                case "well done":
                case "psychotic":
                    newTrap = new PsychoticTrap();
                    break;
                case "bonus skybox":
                case "weather sunny":
                    newTrap = new BonusSkyboxTrap();
                    break;
                case "bonus room":
                case "animal bonus":
                    newTrap = new BonusRoomTrap();
                    break;
                case "furry convention":
                case "rat infestation":
                    newTrap = new RatInfestationTrap();
                    break;
                case "flip":
                case "screen flip":
                    newTrap = Random.Range(0, 2) == 0 ? new FlipHorizontalTrap() : new FlipVerticalTrap();
                    break;
                case "flip horizontal":
                case "screen flip horizontal":
                case "mirror":
                    newTrap = new FlipHorizontalTrap();
                    break;
                case "flip vertical":
                case "screen flip vertical":
                    newTrap = new FlipVerticalTrap();
                    break;
                case "fear":
                    newTrap = new FearTrap();
                    break;
                case "stun":
                case "bubble":
                case "paralysis":
                case "paralyze":
                case "ice":
                case "freeze":
                case "frozen":
                case "chaos control":
                    newTrap = new StunTrap();
                    break;
                case "slip":
                case "banana peel":
                case "banana":
                    newTrap = new SlipTrap();
                    break;
                case "underwater":
                    newTrap = new UnderwaterTrap();
                    break;
                case "invisible":
                case "invisibility":
                case "invisiball":
                    newTrap = new InvisibleTrap();
                    break;
                case "whirlpool":
                    newTrap = new WhirlpoolTrap();
                    break;
                case "push":
                case "boost":
                    newTrap = new BoostTrap();
                    break;
                case "home":
                    newTrap = new HomeTrap();
                    break;
                case "literature":
                    newTrap = new LiteratureTrap();
                    break;
                case "spam":
                    newTrap = new SpamTrap();
                    break;
                case "phone":
                    newTrap = new PhoneTrap();
                    break;
            }

            // I have no earthly idea why a null check didn't work here, but this does so whatever
            if (!string.IsNullOrEmpty(newTrap?.Name))
            {
                if (!string.IsNullOrEmpty(trapLinkSource))
                {
                    var printTrapName =
                        $"\"{originalTrapName}\"{(originalTrapName.Equals(newTrap.Name) ? string.Empty : $" as \"{newTrap.Name}\"")}";
                    Plugin.Log($"Received linked {printTrapName} from {trapLinkSource}", true);
                    newTrap.FromTrapLink = true;
                }
                TrapsToActivate.Enqueue(newTrap);
            }
#if DEBUG
            else if (!string.IsNullOrEmpty(trapLinkSource))
            {
                Plugin.Log($"Unknown Trap Linked: \"{originalTrapName}\" from {trapLinkSource}", true);
            }
#endif
        }

        public static bool ShouldNotUpdateTraps => !PlayerScript.instance || MenuV2Script.instance ||
                                                LoadingScreenScript.instance || MenuV2PhotoModeController.instance;

        public void Update()
        {
            if (ShouldNotUpdateTraps)
                return;

            Armed = true;

            if (TrapsToActivate.Count <= 0)
                return;

            var trap = TrapsToActivate.Dequeue();
            Plugin.Log($"{trap.Name} dequeued");
            var newObject = new GameObject($"ArchipelagoTrap{Count++}");
            if (Count == ulong.MaxValue)
            {
                Count = 0;
            }
            newObject.AddComponent(trap.GetType());
            newObject.transform.SetParent(ModMaster.instance.transform);

            if (!trap.FromTrapLink)
            {
                Plugin.ArchipelagoClient.SendTrapLink(trap.Name);
            }
        }
    }

    public abstract class Trap : MonoBehaviour
    {
        public virtual string Name { get; }
        public bool FromTrapLink;
        public float DurationSeconds;

        public void Awake()
        {
            Plugin.Log($"Activating {Name}");
            TrapActivate();
        }

        public virtual void TrapActivate()
        {
            // Override by actual trap
        }

        public void Update()
        {
            if (APTrapController.ShouldNotUpdateTraps)
                return; // Don't do anything while in menus
            TrapUpdate();
            if (CameraLevelIntroController.instance || DialogueScript.instance)
                return; // Don't tick timer during camera overview or dialogue
            DurationSeconds -= Time.deltaTime;
            if (DurationSeconds <= 0.0f)
            {
                Plugin.Log($"Deactivating {Name}");
                TrapDeactivate();
                Destroy(gameObject);
            }
        }

        public virtual void TrapUpdate()
        {
            // Override by actual trap
        }

        public virtual void TrapDeactivate()
        {
            // Override by actual trap
        }
    }

    public class BurgerHatTrap : Trap
    {
        public override string Name => "Burger Hat Trap";

        public override void TrapActivate()
        {
            var component = Instantiate(AssetMaster.GetPrefab("Hat Wishlists Burger")).GetComponent<HatScript>();
            component.transform.parent = PlayerScript.instance.modelHolderTransform.GetChild(0).GetChild(0);
            component.transform.localPosition = component.myPositionOnHead;
            component.transform.localEulerAngles = Vector3.zero;
            PlayerScript.instance.myHat = component;
        }
    }

    public class NoHatTrap : Trap
    {
        public override string Name => "No Hat Trap";

        public override void TrapActivate()
        {
            HatScript.RemoveHat(false);
        }
    }

    public class BombTrap : Trap
    {
        public override string Name => "Bomb Trap";

        public override void TrapActivate()
        {
            ExplosionScript.SpawnNew(PlayerScript.instance.transform.position + new Vector3(0, 3, 0));
        }
    }

    public class TeleportTrap : Trap
    {
        public override string Name => "Teleport Trap";

        public override void TrapActivate()
        {
            if (GameplayMaster.selfRespawnRecordingDataList.Count > 0)
            {
                var record = GameplayMaster.selfRespawnRecordingDataList.Last();
                var tp = PortalTransitionScript.Spawn(record.playerPosition, Random.RandomRangeInt(0, 360));
                tp.desiredWaterState = record.waterState;
                tp.desiredLightState = record.lightState;
                tp.desiredZoneId = record.currentZoneId;
                tp.backgroundChange = record.currentBackground;
                tp.songChange = record.currentMusic;
            }
        }
    }

    public class UndoTrap : Trap
    {
        public override string Name => "Undo Trap";

        public override void TrapActivate()
        {
            if (GameplayMaster.selfRespawnRecordingDataList.Count > 0)
            {
                var record = GameplayMaster.selfRespawnRecordingDataList.Last();
                var tp = PortalTransitionScript.Spawn(record.playerPosition, record.playerYAngle);
                tp.desiredWaterState = record.waterState;
                tp.desiredLightState = record.lightState;
                tp.desiredZoneId = record.currentZoneId;
                tp.backgroundChange = record.currentBackground;
                tp.songChange = record.currentMusic;
            }
        }
    }

    public class FakeTransitionTrap : Trap
    {
        public override string Name => "Fake Transition Trap";

        public override void TrapActivate()
        {
            if (GameplayMaster.selfRespawnRecordingDataList.Count > 0)
            {
                PortalTransitionScript.Spawn(new Vector3(0,0,0), 0, true);
            }
        }
    }

    public class CutsceneTrap : Trap
    {
        public override string Name => "Cutscene Trap";

        public override void TrapActivate()
        {
            switch (Random.RandomRangeInt(0, 4))
            {
                case 0:
                    Plugin.Log("case 0");
                    Spawn.Instance("CutsceneHolder_DemoBombossBeated", new Vector3(0.0f, 512f, 0.0f));
                    break;
                case 1:
                    Plugin.Log("case 1");
                    Spawn.Instance("CutsceneHolder_ToslaHQUnlocked", new Vector3(0.0f, 500f, 0.0f));
                    break;
                case 2:
                    Plugin.Log("case 2");
                    Spawn.Instance("CutsceneHolder_StartGame", new Vector3(PlayerScript.instance.transform.position.x, 1000f, PlayerScript.instance.transform.position.z));
                    break;
                case 3:
                    Plugin.Log("case 3");
                    Spawn.Instance("CutsceneHolder_Game100PercentComplete", new Vector3(PlayerScript.instance.transform.position.x, 1000f, PlayerScript.instance.transform.position.z));
                    break;
            }
        }
    }

    public class FrameRateTrap : Trap
    {
        public override string Name => "Frame Rate Trap";
        public static FrameRateTrap Instance { get; set; }

        public override void TrapActivate()
        {
            if (Instance != null)
            {
                DurationSeconds = Instance.DurationSeconds = APTrapController.DefaultTrapDuration;
                Destroy(gameObject);
            }
            else
            {
                DurationSeconds = APTrapController.DefaultTrapDuration;
                Application.targetFrameRate = 15;
                Instance = this;
            }
        }

        public override void TrapDeactivate()
        {
            Application.targetFrameRate = APTrapController.OriginalFramerate ?? 60;
            Instance = null;
        }
    }

    public class BulletTimeTrap : FrameRateTrap
    {
        public override string Name => "Bullet Time Trap";

        public override void TrapActivate()
        {

            if (Instance is not BulletTimeTrap)
            {
                Instance?.DurationSeconds = APTrapController.DefaultTrapDuration / 2;
                DurationSeconds = APTrapController.DefaultTrapDuration / 2;
                Time.timeScale = 0.5f;
                Application.targetFrameRate = 20;
                Instance = this;
            }
            else
            {
                Instance.DurationSeconds += APTrapController.DefaultTrapDuration / 2;
                Destroy(gameObject);
            }

            if (SlowTimeTrap.Instance != null)
            {
                Plugin.Log("Increasing time of slow time trap to prevent overlap");
                SlowTimeTrap.Instance.DurationSeconds = Instance.DurationSeconds = Math.Max(SlowTimeTrap.Instance.DurationSeconds, Instance.DurationSeconds);
            }
        }

        public override void TrapDeactivate()
        {
            Time.timeScale = 1f;
            Application.targetFrameRate = APTrapController.OriginalFramerate ?? 60;
            Instance = null;
        }
    }

    public class SlowTimeTrap : Trap
    {
        public override string Name => "Slow Time Trap";
        public static SlowTimeTrap Instance { get; set; }

        public override void TrapActivate()
        {
            // Counteract a fast time trap
            if (FastTimeTrap.Instance != null)
            {
                FastTimeTrap.Instance.DurationSeconds = 0;
                Destroy(gameObject);
                return;
            }

            if (Instance == null)
            {
                DurationSeconds = APTrapController.DefaultTrapDuration / 2;
                Time.timeScale = 0.5f;
                Instance = this;
            }
            else
            {
                Instance.DurationSeconds += APTrapController.DefaultTrapDuration / 2;
                Destroy(gameObject);
            }

            if (FrameRateTrap.Instance is BulletTimeTrap)
            {
                Plugin.Log("Increasing time of bullet time trap to prevent overlap");
                FrameRateTrap.Instance.DurationSeconds = Instance.DurationSeconds = Math.Max(FrameRateTrap.Instance.DurationSeconds, Instance.DurationSeconds);
            }
        }

        public override void TrapDeactivate()
        {
            Time.timeScale = 1f;
            Instance = null;
        }
    }
    public class FastTimeTrap : Trap
    {
        public override string Name => "Fast Time Trap";
        public static FastTimeTrap Instance { get; set; }

        public override void TrapActivate()
        {
            // Counteract a slow time trap
            if (SlowTimeTrap.Instance != null)
            {
                SlowTimeTrap.Instance.DurationSeconds = 0;
                Destroy(gameObject);
                return;
            }

            if (Instance == null)
            {
                DurationSeconds = APTrapController.DefaultTrapDuration * 2;
                Time.timeScale = 2f;
                Instance = this;
            }
            else
            {
                Instance.DurationSeconds += APTrapController.DefaultTrapDuration * 2;
                Destroy(gameObject);
            }
        }

        public override void TrapDeactivate()
        {
            Time.timeScale = 1f;
            Instance = null;
        }
    }

    public class PixelateTrap : Trap
    {
        public override string Name => "Pixelate Trap";
        public static PixelateTrap Instance { get; set; }

        public override void TrapActivate()
        {
            if (Instance == null)
            {
                DurationSeconds = APTrapController.DefaultTrapDuration;

                RenderTexture renderTexture = new RenderTexture(256, 144, 24);
                renderTexture.antiAliasing = Master.instance.PlatformManager == null || !Master.instance.PlatformManager.UseAntialiasing ? 1 : 4;
                renderTexture.filterMode = FilterMode.Point;
                if (renderTexture == null)
                {
                    Debug.Log("newRenderTexture == null!");
                }
                else
                {
                    CameraGame.instance.myCamera.targetTexture.Release();
                    CameraGame.instance.myCamera.targetTexture = renderTexture;
                    CameraPostProcess.instance.myCamera.targetTexture = renderTexture;
                    Master.instance.gameRenderingRawImage.texture = renderTexture;
                }

                Instance = this;
            }
            else
            {
                Instance.DurationSeconds += APTrapController.DefaultTrapDuration;
                Destroy(gameObject);
            }
        }

        public override void TrapDeactivate()
        {
            CameraGame.UpdateRenderTextureToSettingsResolution();
            Instance = null;
        }
    }

    public class PsychoticTrap : Trap
    {
        public override string Name => "Psychotic Trap";

        public override void TrapActivate()
        {
            GiudgementScript.Spawn(GiudgementScript.Kind.psychotic);
        }
    }

    public class BonusSkyboxTrap : Trap
    {
        public override string Name => "Bonus Skybox Trap";

        public override void TrapActivate()
        {
            BackgroundMaster.Change("Background Bonus Level");
        }
    }

    public class BonusRoomTrap : Trap
    {
        public override string Name => "Bonus Room Trap";

        public override void TrapActivate()
        {
            BackgroundMaster.Change("Background Bonus Level");
            GameplayMaster.instance.levelSoundtrack = "SoundtrackBonusLevel";
        }
    }

    public class RatInfestationTrap : Trap
    {
        public override string Name => "Rat Infestation Trap";

        public override void TrapActivate()
        {
            for (var i = 0; i < 5; i++)
            {
                var component = Spawn.Instance("RatPlayer", RatPlayerScript.RespawnNearPlayerPositionGet()).GetComponent<RatPlayerScript>();
                component.myBounceScript.SetBounce(0.1f, true);
            }
        }
    }

    public class FlipHorizontalTrap : Trap
    {
        public override string Name => "Flip Horizontal Trap";
        public static FlipHorizontalTrap Instance { get; set; }
        public Data.LevelId CurrentLevel { get; set; }
        public bool NeedsRefresh { get; set; }

        public override void TrapActivate()
        {
            if (Instance == null)
            {
                CurrentLevel = GameplayMaster.instance.levelId;
                DurationSeconds = APTrapController.DefaultTrapDuration;
                FlipHorizontal();
                Instance = this;
            }
            else
            {
                Instance.DurationSeconds += APTrapController.DefaultTrapDuration;
                Destroy(gameObject);
            }
        }

        public override void TrapUpdate()
        {
            if (CurrentLevel != GameplayMaster.instance.levelId || GameplayMaster.instance.gameOver)
            {
                NeedsRefresh = true;
                CurrentLevel = GameplayMaster.instance.levelId;
            }

            if (!GameplayMaster.instance.gameOver && NeedsRefresh)
            {
                FlipHorizontal();
                NeedsRefresh = false;
            }
        }

        public override void TrapDeactivate()
        {
            if (!NeedsRefresh)
            {
                FlipHorizontal();
            }
            Instance = null;
        }

        public static void FlipHorizontal()
        {
            CameraGame.instance.myCamera.projectionMatrix *= Matrix4x4.Scale(new Vector3(-1, 1, 1));
            GL.invertCulling = !GL.invertCulling;
        }
    }

    public class FlipVerticalTrap : Trap
    {
        public override string Name => "Flip Vertical Trap";
        public static FlipVerticalTrap Instance { get; set; }
        public Data.LevelId CurrentLevel { get; set; }
        public bool NeedsRefresh { get; set; }

        public override void TrapActivate()
        {
            if (Instance == null)
            {
                CurrentLevel = GameplayMaster.instance.levelId;
                DurationSeconds = APTrapController.DefaultTrapDuration;
                FlipVertical();
                Instance = this;
            }
            else
            {
                Instance.DurationSeconds += APTrapController.DefaultTrapDuration;
                Destroy(gameObject);
            }
        }

        public override void TrapUpdate()
        {
            if (CurrentLevel != GameplayMaster.instance.levelId || GameplayMaster.instance.gameOver)
            {
                NeedsRefresh = true;
                CurrentLevel = GameplayMaster.instance.levelId;
            }

            if (!GameplayMaster.instance.gameOver && NeedsRefresh)
            {
                FlipVertical();
                NeedsRefresh = false;
            }
        }

        public override void TrapDeactivate()
        {
            if (!NeedsRefresh)
            {
                FlipVertical();
            }
            Instance = null;
        }

        public static void FlipVertical()
        {
            CameraGame.instance.myCamera.projectionMatrix *= Matrix4x4.Scale(new Vector3(1, -1, 1));
            GL.invertCulling = !GL.invertCulling;
        }
    }

    public class FearTrap : Trap
    {
        public override string Name => "Fear Trap";
        public static FearTrap Instance { get; set; }

        public override void TrapActivate()
        {
            if (Instance == null)
            {
                Instance = this;
                DurationSeconds = APTrapController.DefaultTrapDuration;
            }
            else
            {
                Instance.DurationSeconds += APTrapController.DefaultTrapDuration;
                Destroy(gameObject);
            }
        }

        public override void TrapUpdate()
        {
            if (!GameplayMaster.instance.gameOver && (!GameplayMaster.instance.useGameTimer || GameplayMaster.instance.gameTimer > 10))
            {
                if (Random.value < Tick.TimeFixed * (double)(1 + APTrapController.DefaultTrapDuration -
                                                             Math.Min(DurationSeconds,
                                                                 APTrapController.DefaultTrapDuration)))
                {
                    EffectSpawn.WaterDrop(PlayerScript.instance.transform.position + new Vector3(0.0f, 2f, 0.0f),
                        PlayerScript.instance.myRb.velocity * 0.75f + Utility.AngleToAxis3D(
                            (float)(PlayerScript.instance.transform.GetYAngle() +
                                    (double)Utility.Choose(90, -90, 90, -90)),
                            (float)(30.0 + Random.value * 30.0)) * 24f, Pool.instance.transform);
                }
                var pitch = 1f;
                if (DurationSeconds <= APTrapController.DefaultTrapDuration / 3 * 2)
                    pitch = 1.25f;
                if (DurationSeconds <= APTrapController.DefaultTrapDuration / 3)
                    pitch = 1.5f;
                if (!Sound.IsPlaying("SoundPlayerPanic"))
                    Sound.Play("SoundPlayerPanic", 0.25f, pitch);
            }
        }

        public override void TrapDeactivate()
        {
            Instance = null;
        }
    }

    public class StunTrap : Trap
    {
        public override string Name => "Stun Trap";
        public static StunTrap Instance { get; set; }

        public override void TrapActivate()
        {
            if (Instance == null)
            {
                Sound.Play("SoundPlayerFlipNegated");
                DurationSeconds = APTrapController.DefaultTrapDuration / 3;
                PlayerScript.instance.myRb.isKinematic = true;
                PlayerScript.instance.canAmbulate = false;
                Data.flipOWillUnlockState[Data.gameDataIndex] = false;
                Instance = this;
            }
            else
            {
                Instance.DurationSeconds += APTrapController.DefaultTrapDuration / 3;
                Destroy(gameObject);
            }
        }

        public override void TrapDeactivate()
        {
            PlayerScript.instance.myRb.isKinematic = false;
            PlayerScript.instance.canAmbulate = true;
            Data.flipOWillUnlockState[Data.gameDataIndex] = true;
            Instance = null;
        }
    }

    public class SlipTrap : Trap
    {
        public override string Name => "Slip Trap";

        public override void TrapActivate()
        {
            PlayerScript.instance.SlipperySet();
            Sound.Play("SoundPlayerDamageDefault");
        }
    }

    public class UnderwaterTrap : Trap
    {
        public override string Name => "Underwater Trap";

        public override void TrapActivate()
        {
            if (WaterScript.instance)
            {
                WaterScript.instance.WaterEnable = true;
                for (int index = 0; index < WaterScript.instance.waterGameobjects.Length; ++index)
                {
                    if (WaterScript.instance.waterGameobjects[index] != null)
                        WaterScript.instance.waterGameobjects[index].transform.position += new Vector3(0, 50, 0);
                }
            }
            else
            {
                PlayerScript.instance.underwaterDesiredState = true;
            }
        }
    }

    public class InvisibleTrap : Trap
    {
        public override string Name => "Invisible Trap";
        public static InvisibleTrap Instance = null;
        public static bool Active => Instance != null;

        public override void TrapActivate()
        {
            if (Instance == null)
            {
                Instance = this;
                DurationSeconds = APTrapController.DefaultTrapDuration;
            }
            else
            {
                Instance.DurationSeconds += APTrapController.DefaultTrapDuration;
                Destroy(gameObject);
            }
        }

        public override void TrapUpdate()
        {
            // Set the player visibility every frame, to prevent scene changes from disabling the trap
            PlayerScript.instance.taxiMeshRend.enabled = false;
            PlayerScript.instance.springRenderer.enabled = false;
            PlayerScript.instance.taxiWheelRendLeftTop.enabled = false;
            PlayerScript.instance.taxiWheelRendLeftBot.enabled = false;
            PlayerScript.instance.taxiWheelRendRightTop.enabled = false;
            PlayerScript.instance.taxiWheelRendRightBot.enabled = false;
            foreach (var pizzaWheel in PlayerScript.instance.pizzaWheels)
                pizzaWheel.SetActive(false);
        }

        public override void TrapDeactivate()
        {
            Instance = null;
            PlayerScript.instance.taxiMeshRend.enabled = true;
            PlayerScript.instance.springRenderer.enabled = true;
            PlayerScript.instance.taxiWheelRendLeftTop.enabled = true;
            PlayerScript.instance.taxiWheelRendLeftBot.enabled = true;
            PlayerScript.instance.taxiWheelRendRightTop.enabled = true;
            PlayerScript.instance.taxiWheelRendRightBot.enabled = true;
            PlayerScript.instance.PizzaWheelsInit();
        }
    }
    public class WhirlpoolTrap : Trap
    {
        public override string Name => "Whirlpool Trap";

        public override void TrapActivate()
        {
            DurationSeconds = APTrapController.DefaultTrapDuration;
        }

        public override void TrapUpdate()
        {
            if (!PlayerScript.instance.slipped)
                PlayerScript.instance.SlipperySet();
            PlayerScript.instance.WaterEffectsAndSound();
        }
    }
    public class BoostTrap : Trap
    {
        public override string Name => "Boost Trap";

        public override void TrapActivate()
        {
            PlayerScript.instance.maxSpeedOverride = 256f;
            if (!Sound.IsPlaying("SoundPlayerTurbo"))
                Sound.Play("SoundPlayerTurbo");
        }
    }

    public class HomeTrap : Trap
    {
        public override string Name => "Home Trap";

        public override void TrapActivate()
        {
            Data.lastHubPortalVisited[Data.gameDataIndex] = -1;

            TransictionScript.SpawnOut(TransictionScript.Kind.horizontalFadeFromRight, null, (int)Levels.GetHubIndex());
            Data.LevelId hubLevelId = Data.GetHubLevelId();
            LoadingScreenScript.WelcomeSetup(hubLevelId, Plugin.SlotData.StartInLab ? LocalizationManager.GetTermTranslation("LEVEL_NAME_GRANNY_ISLAND_LAB") : LocalizationManager.GetTermTranslation("Hub")
                , 0, 0, false);
            CheckpointScript.CheckpointDataReset();
            GameplayMaster.SelfRespawnClear();
            if (Plugin.SlotData.StartInLab)
            {
                APPortalManager.QueuedSubwarp = WarpIdentifier.LabStart;
            }
        }
    }

    public abstract class DialogueTrap : Trap
    {
        public virtual APDialogueManager.DialogueTrapType DialogueTrapType { get; }
        public static ulong CurrentId;
        public static ulong LatestId;
        public static ulong Id;

        public bool Activated;

        public override void TrapActivate()
        {
            Activated = false;
            DurationSeconds = 5;
            Id = LatestId++;
            if (LatestId == 5)
            {
                LatestId = 0;
            }
        }

        public override void TrapUpdate()
        {
            if (!Activated)
            {
                DurationSeconds = 5;
                if (Id < CurrentId)
                {
                    return;
                }
                if (!DialogueScript.instance && !HatBuyScript.currentBuyingHat && APDialogueManager.ActiveDialogueTrapType == APDialogueManager.DialogueTrapType.None)
                {
                    APDialogueManager.ActiveDialogueTrapType = DialogueTrapType;
                    Spawn.Instance("Dialogue Rat Pickup Answer Yes", Vector3.zero);
                    DurationSeconds = 0;
                    Activated = true;
                    CurrentId++;
                    if (CurrentId == 5)
                    {
                        CurrentId = 0;
                    }
                }
            }
        }
    }

    public class LiteratureTrap : DialogueTrap
    {
        public override string Name => "Literature Trap";

        public override APDialogueManager.DialogueTrapType DialogueTrapType =>
            APDialogueManager.DialogueTrapType.Literature;
    }

    public class SpamTrap : DialogueTrap
    {
        public override string Name => "Spam Trap";

        public override APDialogueManager.DialogueTrapType DialogueTrapType =>
            APDialogueManager.DialogueTrapType.Spam;
    }

    public class PhoneTrap : DialogueTrap
    {
        public override string Name => "Phone Trap";

        public override APDialogueManager.DialogueTrapType DialogueTrapType =>
            APDialogueManager.DialogueTrapType.Phone;
    }
}
