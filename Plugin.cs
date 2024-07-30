using BepInEx;
using BepInEx.Logging;
using YellowTaxiAP.Archipelago;
using YellowTaxiAP.Utils;
using UnityEngine;
using YellowTaxiAP.Managers;

namespace YellowTaxiAP;

[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
public class Plugin : BaseUnityPlugin
{
    public const string PluginGUID = "com.soopercool101.YellowTaxiAP";
    public const string PluginName = "YellowTaxiAP";
    public const string PluginVersion = "0.1.0";

    public const string ModDisplayInfo = $"{PluginName} v{PluginVersion}";
    private const string APDisplayInfo = $"Archipelago v{ArchipelagoClient.APVersion}";
    public static ManualLogSource BepinLogger;
    public static ArchipelagoClient ArchipelagoClient;
    public static Plugin Instance;

    public PlayerManager PlayerControlHook;
    public CollectableManager CollectableHook;
    public OrangeSwitchManager OrangeSwitchHook;
    public MenuManager MenuHook;

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
            PlayerControlHook = new PlayerManager();
            CollectableHook = new CollectableManager();
            OrangeSwitchHook = new OrangeSwitchManager();
            MenuHook = new MenuManager();
        };
#if DEBUG
        On.ModMaster.Update += (orig, self) =>
        {
            if (Input.GetKeyDown(KeyCode.Minus) && PlayerManager.boost_level > 0)
            {
                BepinLogger.LogMessage($"Flip-O-Will Boost Level lowered to {--PlayerManager.boost_level}");
            }
            if (Input.GetKeyDown(KeyCode.Equals) && PlayerManager.boost_level < 2)
            {
                BepinLogger.LogMessage($"Flip-O-Will Boost Level increased to {++PlayerManager.boost_level}");
            }

            if (Input.GetKeyDown(KeyCode.KeypadMinus) && PlayerManager.jump_level > 0)
            {
                BepinLogger.LogMessage($"Flip-O-Will Jump Level lowered to {--PlayerManager.jump_level}");
            }
            if (Input.GetKeyDown(KeyCode.KeypadPlus) && PlayerManager.jump_level < 2)
            {
                BepinLogger.LogMessage($"Flip-O-Will Jump Level increased to {++PlayerManager.jump_level}");
            }

            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                PlayerManager.flip_enabled = !PlayerManager.flip_enabled;
                BepinLogger.LogMessage($"Flip-O-Will Spin Attack {(PlayerManager.flip_enabled ? "enabled" : "disabled")}");
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                CollectableManager.GoldenSpringActive = !CollectableManager.GoldenSpringActive;
                BepinLogger.LogMessage($"Golden Spring {(CollectableManager.GoldenSpringActive ? "enabled" : "disabled")}");
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                CollectableManager.GoldenPropellerActive = !CollectableManager.GoldenPropellerActive;
                BepinLogger.LogMessage($"Golden Propeller {(CollectableManager.GoldenPropellerActive ? "enabled" : "disabled")}");
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                OrangeSwitchManager.OrangeSwitchActive = !OrangeSwitchManager.OrangeSwitchActive;
                BepinLogger.LogMessage($"Orange Switch {(OrangeSwitchManager.OrangeSwitchActive ? "enabled" : "disabled")}");
            }

            orig(self);
        };
#endif
    }

    private void OnGUI()
    {
        // show the mod is currently loaded in the corner
        GUI.Label(new Rect(16, 16, 300, 20), ModDisplayInfo);
        ArchipelagoConsole.OnGUI();

        string statusMessage;
        // show the Archipelago Version and whether we're connected or not
        if (ArchipelagoClient.Authenticated)
        {
            // if your game doesn't usually show the cursor this line may be necessary
            // Cursor.visible = false;

            statusMessage = " Status: Connected";
            GUI.Label(new Rect(16, 50, 300, 20), APDisplayInfo + statusMessage);
        }
        else
        {
            // if your game doesn't usually show the cursor this line may be necessary
            // Cursor.visible = true;

            statusMessage = " Status: Disconnected";
            GUI.Label(new Rect(16, 50, 300, 20), APDisplayInfo + statusMessage);
            GUI.Label(new Rect(16, 70, 150, 20), "Host: ");
            GUI.Label(new Rect(16, 90, 150, 20), "Player Name: ");
            GUI.Label(new Rect(16, 110, 150, 20), "Password: ");

            ArchipelagoClient.ServerData.Uri = GUI.TextField(new Rect(150, 70, 150, 20),
                ArchipelagoClient.ServerData.Uri);
            ArchipelagoClient.ServerData.SlotName = GUI.TextField(new Rect(150, 90, 150, 20),
                ArchipelagoClient.ServerData.SlotName);
            ArchipelagoClient.ServerData.Password = GUI.TextField(new Rect(150, 110, 150, 20),
                ArchipelagoClient.ServerData.Password);

            // requires that the player at least puts *something* in the slot name
            if (GUI.Button(new Rect(16, 130, 100, 20), "Connect") &&
                !ArchipelagoClient.ServerData.SlotName.IsNullOrWhiteSpace())
            {
                ArchipelagoClient.Connect();
            }
        }
        // this is a good place to create and add a bunch of debug buttons
    }
}