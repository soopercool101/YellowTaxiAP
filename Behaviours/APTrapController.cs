using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace YellowTaxiAP.Behaviours
{

    public class APTrapController : MonoBehaviour
    {
        public static Queue<Trap> TrapsToActivate = new();
        public static int Count = 0;
        public static float DefaultTrapDuration = 10f;
        public static int? OriginalFramerate;

        public static bool Armed;

        public static void ActivateTrap(string trapName, bool fromTrapLink = false)
        {
            if (!Armed)
                return;

            var originalTrapName = trapName;
            if (trapName.EndsWith(" Trap", StringComparison.OrdinalIgnoreCase))
            {
                trapName = trapName.Substring(0, trapName.Length - 5);
            }

            Trap newTrap = null;
            switch (trapName)
            {
                case "Explosion":
                case "Bomb":
                case "TNT":
                case "TNT Barrel":
                    newTrap = new BombTrap();
                    break;
                case "Bald":
                case "No Hat":
                    newTrap = new NoHatTrap();
                    break;
                case "Burger Hat":
                    newTrap = new BurgerHatTrap();
                    break;
                case "Teleport":
                    newTrap = new TeleportTrap();
                    break;
                case "Wishlist":
                    newTrap = new WishlistTrap();
                    break;
                case "Bullet Time":
                    newTrap = new BulletTimeTrap();
                    break;
                case "Slow":
                case "Slow Time":
                case "Chaos Control":
                    newTrap = new SlowTimeTrap();
                    break;
                case "Pixelate":
                case "Pixellation": // sic, how it's typed on the sheet
                case "Pixelation":
                case "144p":
                    newTrap = new PixelateTrap();
                    break;
                case "Well Done":
                case "Psychotic":
                    newTrap = new PsychoticTrap();
                    break;
            }

            // I have no earthly idea why a null check didn't work here, but this does so whatever
            if (!string.IsNullOrEmpty(newTrap?.Name))
            {
                if (fromTrapLink)
                {
                    Plugin.Log($"Received linked \"{originalTrapName}\"", true);
                    newTrap.FromTrapLink = true;
                }
                TrapsToActivate.Enqueue(newTrap);
            }
#if DEBUG
            else if (fromTrapLink)
            {
                Plugin.Log($"Unknown Trap Linked: \"{trapName}\"", true);
            }
#endif
        }

        public void Update()
        {
            if (!PlayerScript.instance|| MenuV2Script.instance)
                return;

            Armed = true;

            if (TrapsToActivate.Count <= 0)
                return;

            var trap = TrapsToActivate.Dequeue();
            Plugin.Log($"{trap.Name} dequeued");
            var newObject = new GameObject($"ArchipelagoTrap{Count++}");
            newObject.AddComponent(trap.GetType());

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
            if (!PlayerScript.instance || MenuV2Script.instance)
                return; // Don't tick timer while in menus
            DurationSeconds -= Time.deltaTime;
            if (DurationSeconds <= 0.0f)
            {
                Plugin.Log($"Deactivating {Name}");
                TrapDeactivate();
                Destroy(gameObject);
            }
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
                PortalTransitionScript.Spawn(GameplayMaster.selfRespawnRecordingDataList.Last().playerPosition, Random.RandomRangeInt(0, 360));
            }
        }
    }

    public class WishlistTrap : Trap
    {
        public override string Name => "Wishlist Trap";

        public override void TrapActivate()
        {
            Spawn.Instance("CutsceneHolder_DemoBombossBeated", new Vector3(0.0f, 512f, 0.0f));
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

            if (Instance == null || Instance is not BulletTimeTrap)
            {
                if (Instance != null)
                {
                    Instance.DurationSeconds = APTrapController.DefaultTrapDuration / 2;
                }
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
                    Debug.Log("newRenderTexture is null!");
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
}
