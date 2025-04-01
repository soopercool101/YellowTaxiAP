using BepInEx;
using BepInEx.Logging;
using System;
using UnityEngine;
using UnityEngine.Events;
using YellowTaxiAP.Archipelago;
using YellowTaxiAP.Managers;
using YellowTaxiAP.Utils;

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
    public static Plugin Instance;
    public static bool DeathLinkInProgress = false;

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
        ArchipelagoClient = new ArchipelagoClient();
        ArchipelagoConsole.Awake();

        BepinLogger.LogMessage($"{ModDisplayInfo} loaded!");
        ArchipelagoConsole.LogMessage($"{ModDisplayInfo} loaded!");
        On.ModMaster.Start += (orig, self) =>
        {
#if DEBUG
            self.ModMasterDebugLogsEnableSet(true);
#else
            self.ModMasterDebugLogsEnableSet(false);
#endif
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
            DataHook = new APDataManager();
            PsychoTaxiHook = new APPsychoTaxiManager();
            DestructableHook = new APDestructableManager();
            HudHook = new APHUDManager();
            self.gameObject.AddComponent<ArchipelagoRenderer>();
        };
        On.GigaMorioScript.Update += (orig, self) =>
        {
            if (!AllowLaser)
            {
                for (int index = self.myLasers.Count - 1; index >= 0; --index)
                {
                    Pool.Destroy(self.myLasers[index].gameObject);
                    self.myLasers.RemoveAt(index);
                }
                self.laserCD = self.laserCooldown;
                self.myAnimator.ResetTrigger("ChargeLaser");
                self.myAnimator.SetTrigger("LaserFireEnd");
            }
            orig(self);
        };
        On.BombCarScript.Update += (orig, self) =>
        {
            if (AllowLaser)
            {
                orig(self);
            }
        };

        On.ModMaster.OnPlayerDie += (orig, self) =>
        {
            orig(self);
            if (!DeathLinkInProgress)
            {
                ArchipelagoClient.DeathLinkHandler?.KillPlayer();
                Log("Death Link Sent");
            }

            DeathLinkInProgress = false;
        };

#if DEBUG
        On.ModMaster.Update += (orig, self) =>
        {
            if ((Input.GetKeyDown(KeyCode.KeypadMinus) || Input.GetKeyDown(KeyCode.Minus)) && APPlayerManager.boost_level > 0)
            {
                Log($"Flip-O-Will Boost Level lowered to {--APPlayerManager.boost_level}");
            }
            if ((Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetKeyDown(KeyCode.Equals)) && APPlayerManager.boost_level < 2)
            {
                Log($"Flip-O-Will Boost Level increased to {++APPlayerManager.boost_level}");
            }

            if (Input.GetKeyDown(KeyCode.LeftBracket) && APPlayerManager.jump_level > 0)
            {
                Log($"Flip-O-Will Jump Level lowered to {--APPlayerManager.jump_level}");
            }
            if (Input.GetKeyDown(KeyCode.RightBracket) && APPlayerManager.jump_level < 2)
            {
                Log($"Flip-O-Will Jump Level increased to {++APPlayerManager.jump_level}");
            }

            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                APPlayerManager.flip_enabled = !APPlayerManager.flip_enabled;
                Log($"Flip-O-Will Spin Attack {(APPlayerManager.flip_enabled ? "enabled" : "disabled")}");
            }
            if (Input.GetKeyDown(KeyCode.Backslash))
            {
                APPlayerManager.glide_enabled = !APPlayerManager.glide_enabled;
                Log($"Glide {(APPlayerManager.glide_enabled ? "enabled" : "disabled")}");
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
                APRatManager.AP_ReceivedRat = !APRatManager.AP_ReceivedRat;
                if (APRatManager.AP_ReceivedRat)
                {
                    RatPersonScript.pickedUp = false;
                    RatPersonScript.RatPickUp();
                }
                else
                {
                    UnityEngine.Object.Destroy(RatPlayerScript.instance?.gameObject);
                }
                Log($"Rat {(APRatManager.AP_ReceivedRat ? "enabled" : "disabled")}");
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
            if (Input.GetKeyDown(KeyCode.K))
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
                APAreaStateManager.UpdateRocketState();
                Log($"Mosk Rocket {(APAreaStateManager.RocketEnabled ? "enabled" : "disabled")}");
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                APAreaStateManager.MindPasswordReceived = !APAreaStateManager.MindPasswordReceived;
                APAreaStateManager.UpdateMoriosPasswordState();
                Log($"Morio's Password {(APAreaStateManager.MindPasswordReceived ? "enabled" : "disabled")}");
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