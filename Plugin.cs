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
    public APOrangeSwitchManager OrangeSwitchHook;
    public APDestructableManager DestructableHook;
    public APPsychoTaxiManager PsychoTaxiHook;
    public APRatManager RatHook;
    public APMenuManager MenuHook;
    public APDataManager DataHook;

    public bool AllowLaser = true;
    public static void DoubleLog(string message)
    {
        //BepinLogger.LogMessage(message);
        ArchipelagoConsole.LogMessage(message);
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
            self.ModEnableSet(true);
            orig(self);
            PlayerControlHook = new APPlayerManager();
            CollectableHook = new APCollectableManager();
            OrangeSwitchHook = new APOrangeSwitchManager();
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

        On.ModMaster.OnPlayerDie += (orig, self) =>
        {
            orig(self);
            if (!DeathLinkInProgress)
            {
                ArchipelagoClient.DeathLinkHandler?.KillPlayer();
                DoubleLog("Death Link Sent");
            }

            DeathLinkInProgress = false;
        };

        On.ModMaster.Update += (orig, self) =>
        {
#if DEBUG
            if ((Input.GetKeyDown(KeyCode.KeypadMinus) || Input.GetKeyDown(KeyCode.Minus)) && APPlayerManager.boost_level > 0)
            {
                DoubleLog($"Flip-O-Will Boost Level lowered to {--APPlayerManager.boost_level}");
            }
            if ((Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetKeyDown(KeyCode.Equals)) && APPlayerManager.boost_level < 2)
            {
                DoubleLog($"Flip-O-Will Boost Level increased to {++APPlayerManager.boost_level}");
            }

            if (Input.GetKeyDown(KeyCode.LeftBracket) && APPlayerManager.jump_level > 0)
            {
                DoubleLog($"Flip-O-Will Jump Level lowered to {--APPlayerManager.jump_level}");
            }
            if (Input.GetKeyDown(KeyCode.RightBracket) && APPlayerManager.jump_level < 2)
            {
                DoubleLog($"Flip-O-Will Jump Level increased to {++APPlayerManager.jump_level}");
            }

            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                APPlayerManager.flip_enabled = !APPlayerManager.flip_enabled;
                DoubleLog($"Flip-O-Will Spin Attack {(APPlayerManager.flip_enabled ? "enabled" : "disabled")}");
            }
            if (Input.GetKeyDown(KeyCode.Backslash))
            {
                APPlayerManager.glide_enabled = !APPlayerManager.glide_enabled;
                DoubleLog($"Glide {(APPlayerManager.glide_enabled ? "enabled" : "disabled")}");
            }
            if (Input.GetKeyDown(KeyCode.Period) || Input.GetKeyDown(KeyCode.KeypadPeriod))
            {
                GameplayMaster.instance.useGameTimer = !GameplayMaster.instance.useGameTimer;
                DoubleLog($"Game Timer {(GameplayMaster.instance.useGameTimer ? "enabled" : "disabled")}");
            }
            if (Input.GetKeyDown(KeyCode.Comma))
            {
                AllowLaser = !AllowLaser;
                DoubleLog($"Dream Gigalaser {(AllowLaser ? "enabled" : "disabled")}");
            }

            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
            {
                APCollectableManager.GoldenSpringActive = !APCollectableManager.GoldenSpringActive;
                DoubleLog($"Golden Spring {(APCollectableManager.GoldenSpringActive ? "enabled" : "disabled")}");
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
            {
                APCollectableManager.GoldenPropellerActive = !APCollectableManager.GoldenPropellerActive;
                DoubleLog($"Golden Propeller {(APCollectableManager.GoldenPropellerActive ? "enabled" : "disabled")}");
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
            {
                APOrangeSwitchManager.OrangeSwitchActive = !APOrangeSwitchManager.OrangeSwitchActive;
                DoubleLog($"Orange Switch {(APOrangeSwitchManager.OrangeSwitchActive ? "enabled" : "disabled")}");
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
                DoubleLog($"Rat {(APRatManager.AP_ReceivedRat ? "enabled" : "disabled")}");
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                DeathLinkInProgress = true;
                GameplayMaster.instance?.Die();
                DoubleLog($"Attempting to kill player");
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                ExplosionScript.SpawnNew(PlayerScript.instance.transform.position - new Vector3(0, 3, 0));
                DoubleLog($"Spawning explosion at player");
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                PlayerScript.instance.propellerUsesLeft = 3;
                DoubleLog($"Granting Propeller");
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (PlayerScript.instance.invincible)
                {
                    PlayerScript.instance.InvincibleStop();
                    DoubleLog($"Toggling invincibility off");
                }
                else
                {
                    PlayerScript.instance.InvincibleSet(float.PositiveInfinity);
                    DoubleLog($"Toggling invincibility on");
                }
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                APAreaStateManager.RocketEnabled = !APAreaStateManager.RocketEnabled;
                APAreaStateManager.UpdateRocketState();
                DoubleLog($"Mosk Rocket {(APAreaStateManager.RocketEnabled ? "enabled" : "disabled")}");
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                APAreaStateManager.MindPasswordReceived = !APAreaStateManager.MindPasswordReceived;
                APAreaStateManager.UpdateMoriosPasswordState();
                DoubleLog($"Morio's Password {(APAreaStateManager.MindPasswordReceived ? "enabled" : "disabled")}");
            }

            if (Input.GetKeyDown(KeyCode.BackQuote) || Input.GetKeyDown(KeyCode.Tilde))
            {
                ModMaster.instance.gameObject.GetComponent<ArchipelagoRenderer>().enabled =
                    !ModMaster.instance.gameObject.GetComponent<ArchipelagoRenderer>().enabled;
                DoubleLog($"Archipelago rendering {(ModMaster.instance.gameObject.GetComponent<ArchipelagoRenderer>().enabled ? "enabled" : "disabled")}");
            }
#endif
            orig(self);
        };
    }
}