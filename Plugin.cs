using BepInEx;
using BepInEx.Logging;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using YellowTaxiAP.Archipelago;
using YellowTaxiAP.Behaviours;
using YellowTaxiAP.Managers;

namespace YellowTaxiAP;

[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
public class Plugin : BaseUnityPlugin
{
    public const string PluginGUID = "com.soopercool101.YellowTaxiAP";
    public const string PluginName = "YellowTaxiAP";
    public const string PluginVersion = "0.0.1";

    public const string ModDisplayInfo = $"{PluginName} v{PluginVersion}";
    public const string APDisplayInfo = $"Archipelago v{ArchipelagoClient.APVersion}";
    public static ManualLogSource BepinLogger;
    public static ArchipelagoClient ArchipelagoClient;
    public static YTGVSlotData SlotData = new ();
    public static Plugin Instance;
    public static bool DeathLinkInProgress = false;

    public static string PluginDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    public static string LoginDetailsFile = Path.Combine(PluginDirectory, "login.txt");

    public APPlayerManager PlayerControlHook;
    public APPortalManager PortalHook;
    public APAreaStateManager AreaStateHook;
    public APCheckpointManager CheckpointHook;
    public APHatManager HatHook;
    public APDialogueManager DialogueHook;
    public APCollectableManager CollectableHook;
    public APSwitchManager SwitchHook;
    public APDestructableManager DestructableHook;
    public APPsychoTaxiManager PsychoTaxiHook;
    public APRatManager RatHook;
    public APMenuManager MenuHook;
    public APDataManager DataHook;
    public APHUDManager HudHook;
    public APWalletManager WalletHook;
    public APMinimapManager MinimapHook;

    public bool AllowLaser = true;
    public static void Log(string message)
    {
#if DEBUG
        ArchipelagoConsole.LogMessage(message);
#else
        BepinLogger.LogMessage(message);
#endif
    }

    private void Awake()
    {
        // Plugin startup logic
        BepinLogger = Logger;
        Instance = this;

        BepinLogger.LogMessage($"{ModDisplayInfo} loaded!");
        ArchipelagoConsole.LogMessage($"{ModDisplayInfo} loaded!");
        On.Master.Start += (orig, self) =>
        {
            DataHook = new APDataManager();
            orig(self);
            // Add extra bunnies and gears from demo-only locations
            var grannysIslandMap = MapMaster.GetAreaScriptableObject_ByAreaName("LEVEL_NAME_GRANNY_ISLAND");
            grannysIslandMap.gearsId.Add(10004);    // Pizza Oven
            grannysIslandMap.gearsId.Add(10010);    // Crash Again
            grannysIslandMap.gearsId.Add(10020);    // Sewer
            var moriosIslandMap = MapMaster.GetAreaScriptableObject_ByAreaName("MAP_AREA_NAME_GRANNY_ISLAND_LAB");
            moriosIslandMap.gearsId.Add(10019);     // Orange Blocks
            moriosIslandMap.gearsId.Add(10024);     // Nut
            moriosIslandMap.bunniesId.Add(3);       // Above Morio's Home Portal
            moriosIslandMap.bunniesId.Add(4);       // Above Demo Wall
        };
        On.ModMaster.Start += (orig, self) =>
        {
            if (Master.instance.isDemo)
            {
                // Disable the mod if this is the demo
                Log("Demo detected, not enabling mod");
                return;
            }
#if DEBUG
            self.ModMasterDebugLogsEnableSet(true);
#else
            self.ModMasterDebugLogsEnableSet(false);
#endif
            ArchipelagoClient = new ArchipelagoClient();
            ArchipelagoConsole.Awake();
            self.ModEnableSet(true);
            orig(self);
            PlayerControlHook = new APPlayerManager();
            CollectableHook = new APCollectableManager();
            SwitchHook = new APSwitchManager();
            PortalHook = new APPortalManager();
            AreaStateHook = new APAreaStateManager();
            HatHook = new APHatManager();
            CheckpointHook = new APCheckpointManager();
            DialogueHook = new APDialogueManager();
            RatHook = new APRatManager();
            MenuHook = new APMenuManager();
            PsychoTaxiHook = new APPsychoTaxiManager();
            DestructableHook = new APDestructableManager();
            HudHook = new APHUDManager();
            MinimapHook = new APMinimapManager();
            WalletHook = new APWalletManager();
            self.gameObject.AddComponent<ArchipelagoRenderer>();
            self.gameObject.AddComponent<GameStateUpdater>();
            self.gameObject.AddComponent<APSaveController>();
            self.gameObject.AddComponent<OrangeSwitchController>();
            On.GigaMorioScript.Update += (origUpdate, selfGigaMorio) =>
            {
                if (!AllowLaser)
                {
                    for (var index = selfGigaMorio.myLasers.Count - 1; index >= 0; --index)
                    {
                        Pool.Destroy(selfGigaMorio.myLasers[index].gameObject);
                        selfGigaMorio.myLasers.RemoveAt(index);
                    }
                    selfGigaMorio.laserCD = selfGigaMorio.laserCooldown;
                    selfGigaMorio.myAnimator.ResetTrigger("ChargeLaser");
                    selfGigaMorio.myAnimator.SetTrigger("LaserFireEnd");
                }
                origUpdate(selfGigaMorio);
            };

            On.BombCarScript.Update += (origUpdate, selfBombCar) =>
            {
                if (AllowLaser)
                {
                    origUpdate(selfBombCar);
                }
            };

            On.ModMaster.OnPlayerDie += (origOnPlayerDie, selfModMaster) =>
            {
                origOnPlayerDie(selfModMaster);
                if (!DeathLinkInProgress)
                {
                    ArchipelagoClient.DeathLinkHandler?.SendDeathLink();
                    Log("Death Link Sent");
                }

                DeathLinkInProgress = false;
            };
        };

#if DEBUG
        On.ModMaster.Update += (orig, self) =>
        {
            if ((Input.GetKeyDown(KeyCode.KeypadMinus) || Input.GetKeyDown(KeyCode.Minus)) && APPlayerManager.BoostItems > 0)
            {
                Log($"Flip-O-Will Boost Level lowered to {--APPlayerManager.BoostItems}");
            }
            if ((Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetKeyDown(KeyCode.Equals)) && APPlayerManager.BoostItems < 2)
            {
                Log($"Flip-O-Will Boost Level increased to {++APPlayerManager.BoostItems}");
            }

            if (Input.GetKeyDown(KeyCode.LeftBracket) && APPlayerManager.JumpItems > 0)
            {
                Log($"Flip-O-Will Jump Level lowered to {--APPlayerManager.JumpItems}");
            }
            if (Input.GetKeyDown(KeyCode.RightBracket) && APPlayerManager.JumpItems < 2)
            {
                Log($"Flip-O-Will Jump Level increased to {++APPlayerManager.JumpItems}");
            }

            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                APPlayerManager.SpinAttackItem = !APPlayerManager.SpinAttackItem;
                Log($"Flip-O-Will Spin Attack {(APPlayerManager.SpinAttackItem ? "enabled" : "disabled")}");
            }
            if (Input.GetKeyDown(KeyCode.Backslash))
            {
                APPlayerManager.GlideEnabledItem = !APPlayerManager.GlideEnabledItem;
                Log($"Glide {(APPlayerManager.GlideEnabledItem ? "enabled" : "disabled")}");
            }
            if (Input.GetKeyDown(KeyCode.Period) || Input.GetKeyDown(KeyCode.KeypadPeriod))
            {
                GameplayMaster.instance.useGameTimer = !GameplayMaster.instance.useGameTimer;
                Log($"Game Timer {(GameplayMaster.instance.useGameTimer ? "enabled" : "disabled")}");
            }
            if (Input.GetKeyDown(KeyCode.Comma))
            {
                AllowLaser = !AllowLaser;
                Log($"Dream Gigalaser {(AllowLaser ? "enabled" : "disabled")}");
            }

            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
            {
                APCollectableManager.GoldenSpringActive = !APCollectableManager.GoldenSpringActive;
                Log($"Golden Spring {(APCollectableManager.GoldenSpringActive ? "enabled" : "disabled")}");
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
            {
                APCollectableManager.GoldenPropellerActive = !APCollectableManager.GoldenPropellerActive;
                Log($"Golden Propeller {(APCollectableManager.GoldenPropellerActive ? "enabled" : "disabled")}");
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
            {
                APSwitchManager.OrangeSwitchUnlocked = !APSwitchManager.OrangeSwitchUnlocked;
                Log($"Orange Switch {(APSwitchManager.OrangeSwitchUnlocked ? "enabled" : "disabled")}");
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
            {
                APRatManager.ReceivedRatItem = !APRatManager.ReceivedRatItem;
                GameStateUpdater.RatStateNeedsUpdate = true;
                Log($"Rat {(APRatManager.ReceivedRatItem ? "enabled" : "disabled")}");
            }
            if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
            {
                APSwitchManager.PurpleSwitchUnlocked = !APSwitchManager.PurpleSwitchUnlocked;
                Log($"Purple Switch {(APSwitchManager.PurpleSwitchUnlocked ? "enabled" : "disabled")}");
            }
            if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
            {
                APSwitchManager.GreenSwitchUnlocked = !APSwitchManager.GreenSwitchUnlocked;
                Log($"Green Switch {(APSwitchManager.GreenSwitchUnlocked ? "enabled" : "disabled")}");
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                DeathLinkInProgress = true;
                GameplayMaster.instance?.Die();
                Log($"Attempting to kill player");
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                ExplosionScript.SpawnNew(PlayerScript.instance.transform.position - new Vector3(0, 3, 0));
                Log($"Spawning explosion at player");
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                PlayerScript.instance.propellerUsesLeft = 3;
                Log($"Granting Propeller");
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (PlayerScript.instance.invincible)
                {
                    PlayerScript.instance.InvincibleStop();
                    Log($"Toggling invincibility off");
                }
                else
                {
                    PlayerScript.instance.InvincibleSet(float.PositiveInfinity);
                    Log($"Toggling invincibility on");
                }
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                APAreaStateManager.RocketEnabled = !APAreaStateManager.RocketEnabled;
                Log($"Mosk Rocket {(APAreaStateManager.RocketEnabled ? "enabled" : "disabled")}");
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                APAreaStateManager.MindPasswordReceived = !APAreaStateManager.MindPasswordReceived;
                Log($"Morio's Password {(APAreaStateManager.MindPasswordReceived ? "enabled" : "disabled")}");
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                APAreaStateManager.DoggoReceived = !APAreaStateManager.DoggoReceived;
                Log($"Doggo {(APAreaStateManager.DoggoReceived ? "enabled" : "disabled")}");
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                APAreaStateManager.GelaToniReceived = !APAreaStateManager.GelaToniReceived;
                Log($"Gela-Toni {(APAreaStateManager.GelaToniReceived ? "enabled" : "disabled")}");
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                APAreaStateManager.PizzaKingReceived = !APAreaStateManager.PizzaKingReceived;
                Log($"Pizza King {(APAreaStateManager.PizzaKingReceived ? "enabled" : "disabled")}");
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                APAreaStateManager.FullGameUnlocked = !APAreaStateManager.FullGameUnlocked;
                Log($"Full Game {(APAreaStateManager.FullGameUnlocked ? "enabled" : "disabled")}");
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                var rotation = (int)Math.Round(PlayerScript.instance?.transform?.GetYAngle() ?? 0);
                if (rotation == 360)
                    rotation = 0;
                var zoneVals =
                    $"new Vector3({Math.Round(PlayerScript.instance?.transform?.position.x ?? 0, 1)}f, {Math.Round(PlayerScript.instance?.transform?.position.y ?? 0, 1)}f, {Math.Round(PlayerScript.instance?.transform?.position.z ?? 0, 1)}f), {rotation}, {ZoneMaster.currentZoneId}, {LightDirectionalScript.instance?.myLight?.enabled.ToString().ToLower() ?? "false"}, {WaterScript.instance?.WaterEnable.ToString().ToLower() ?? "false"}, \"{GameplayMaster.instance?.levelSoundtrack ?? "default"}\", \"{BackgroundMaster.instance?.name ?? "default"}\"),";
                Log($"Copying current zone values ({zoneVals}");
                GUIUtility.systemCopyBuffer = zoneVals;
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                var zoneVals =
                    $"\"{GameplayMaster.instance?.levelSoundtrack ?? "default"}\", \"{BackgroundMaster.instance?.name ?? "default"}\"),";
                Log($"Copying current music/bg values ({zoneVals}");
                GUIUtility.systemCopyBuffer = zoneVals;
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                Spawn.Instance("CutsceneHolder_DemoBombossBeated", new Vector3(0.0f, 512f, 0.0f));
            }

            if (Input.GetKeyDown(KeyCode.BackQuote) || Input.GetKeyDown(KeyCode.Tilde))
            {
                ModMaster.instance.gameObject.GetComponent<ArchipelagoRenderer>().enabled =
                    !ModMaster.instance.gameObject.GetComponent<ArchipelagoRenderer>().enabled;
                Log($"Archipelago rendering {(ModMaster.instance.gameObject.GetComponent<ArchipelagoRenderer>().enabled ? "enabled" : "disabled")}");
            }
            orig(self);
        };
#endif
    }
}