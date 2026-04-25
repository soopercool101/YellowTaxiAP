using BepInEx;
using BepInEx.Logging;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Steamworks;
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
    public const string PluginVersion = "0.3.3";

    public const string ModDisplayInfo = $"{PluginName} v{PluginVersion}";
    public const string APDisplayInfo = $"Archipelago v{ArchipelagoClient.APVersion}";
    public static ManualLogSource BepinLogger;
    public static ArchipelagoClient ArchipelagoClient;
    public static YTGVSlotData SlotData = new ();
    public static Plugin Instance;
    public static bool DeathLinkInProgress = false;

    public static bool IsSteam { get; private set; }
    public static bool EnableSteamKeyboard { get; set; }

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
    public APPsychoTaxiManager PsychoTaxiHook;
    public APRatManager RatHook;
    public APMenuManager MenuHook;
    public APDataManager DataHook;
    public APHUDManager HudHook;
    public APWalletManager WalletHook;
    public APMinimapManager MinimapHook;
    public APTimeAttackManager TimeAttackHook;

    public bool AllowLaser = true;
#if DEBUG
    public static void Log(string message, bool? logInGameWindow = null)
    {
        var logInGame = logInGameWindow ?? DebugLocationHelper.Enabled;
#else
    public static void Log(string message, bool logInGame = false)
    {
#endif
        if (logInGame)
            ArchipelagoConsole.LogMessage(message);
        else
            BepinLogger.LogMessage(message);
    }

    private void Awake()
    {
        // Plugin startup logic
        BepinLogger = Logger;
        Instance = this;

        BepinLogger.LogMessage($"{ModDisplayInfo} loaded!");
        ArchipelagoConsole.LogMessage($"{ModDisplayInfo} loaded!");
        On.Master.Awake += (orig, self) =>
        {
            orig(self);
            Master.influencerHatsAndGraphicsEnabled = true;
        };
        On.Master.InfluecerGraphicsCheatReset += _ =>
        {
            // Don't reset this cheat
        };
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

            if (Master.instance.PlatformManager.GetType().Name.Equals("SteamManager"))
            {
                try
                {
                    BepinLogger.LogMessage($"Steam detected. Enabling Steam features. Big picture: {SteamUtils.IsSteamInBigPictureMode} | Overlay: {SteamUtils.IsOverlayEnabled}");
                    IsSteam = true;
                    EnableSteamKeyboard = SteamUtils.IsSteamInBigPictureMode;// && SteamUtils.IsOverlayEnabled;

                }
                catch(Exception ex)
                {
                    BepinLogger.LogError("Steam setup failed. Disabling steam features.");
                    BepinLogger.LogError(ex);
                    IsSteam = EnableSteamKeyboard = false;
                }
            }
            else
            {
                BepinLogger.LogMessage($"Current platform manager: {Master.instance.PlatformManager.GetType().Name}");
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
            HudHook = new APHUDManager();
            MinimapHook = new APMinimapManager();
            WalletHook = new APWalletManager();
            TimeAttackHook = new APTimeAttackManager();
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
        };
        On.ModMaster.OnPlayerDie += (orig, self) =>
        {
            orig(self);
            if (!DeathLinkInProgress)
            {
                ArchipelagoClient.DeathLinkHandler?.SendDeathLink();
                Log("Death Link Sent");
            }

            DeathLinkInProgress = false;
        };
#if DEBUG
        On.ModMaster.Update += (orig, self) =>
        {
            if ((Input.GetKeyDown(KeyCode.KeypadMinus) || Input.GetKeyDown(KeyCode.Minus)) && APPlayerManager.BoostItems > 0)
            {
                Log($"Flip-O-Will Boost Level lowered to {--APPlayerManager.BoostItems}", true);
            }
            if ((Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetKeyDown(KeyCode.Equals)) && APPlayerManager.BoostItems < 2)
            {
                Log($"Flip-O-Will Boost Level increased to {++APPlayerManager.BoostItems}", true);
            }

            if (Input.GetKeyDown(KeyCode.LeftBracket) && APPlayerManager.JumpItems > 0)
            {
                Log($"Flip-O-Will Jump Level lowered to {--APPlayerManager.JumpItems}", true);
            }
            if (Input.GetKeyDown(KeyCode.RightBracket) && APPlayerManager.JumpItems < 2)
            {
                Log($"Flip-O-Will Jump Level increased to {++APPlayerManager.JumpItems}", true);
            }

            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                APPlayerManager.SpinAttackItem = !APPlayerManager.SpinAttackItem;
                Log($"Flip-O-Will Spin Attack {(APPlayerManager.SpinAttackItem ? "enabled" : "disabled")}", true);
            }
            if (Input.GetKeyDown(KeyCode.Backslash))
            {
                APPlayerManager.GlideEnabledItem = !APPlayerManager.GlideEnabledItem;
                Log($"Glide {(APPlayerManager.GlideEnabledItem ? "enabled" : "disabled")}", true);
            }
            if (Input.GetKeyDown(KeyCode.Period) || Input.GetKeyDown(KeyCode.KeypadPeriod))
            {
                GameplayMaster.instance.useGameTimer = !GameplayMaster.instance.useGameTimer;
                Log($"Game Timer {(GameplayMaster.instance.useGameTimer ? "enabled" : "disabled")}", true);
            }
            if (Input.GetKeyDown(KeyCode.Comma))
            {
                AllowLaser = !AllowLaser;
                Log($"Dream Gigalaser {(AllowLaser ? "enabled" : "disabled")}", true);
                var rivers = FindObjectsByType<RiverScript>(FindObjectsInactive.Include, FindObjectsSortMode.None);
                foreach (var river in rivers)
                {
                    river.gameObject.SetActive(AllowLaser);
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
            {
                APCollectableManager.GoldenSpringActive = !APCollectableManager.GoldenSpringActive;
                Log($"Golden Spring {(APCollectableManager.GoldenSpringActive ? "enabled" : "disabled")}", true);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
            {
                APCollectableManager.GoldenPropellerActive = !APCollectableManager.GoldenPropellerActive;
                Log($"Golden Propeller {(APCollectableManager.GoldenPropellerActive ? "enabled" : "disabled")}", true);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
            {
                APSwitchManager.OrangeSwitchUnlocked = !APSwitchManager.OrangeSwitchUnlocked;
                Log($"Orange Switch {(APSwitchManager.OrangeSwitchUnlocked ? "enabled" : "disabled")}", true);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
            {
                APRatManager.ReceivedRatItem = !APRatManager.ReceivedRatItem;
                GameStateUpdater.RatStateNeedsUpdate = true;
                Log($"Rat {(APRatManager.ReceivedRatItem ? "enabled" : "disabled")}", true);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
            {
                APSwitchManager.PurpleSwitchUnlocked = !APSwitchManager.PurpleSwitchUnlocked;
                Log($"Purple Switch {(APSwitchManager.PurpleSwitchUnlocked ? "enabled" : "disabled")}", true);
            }
            if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
            {
                APSwitchManager.GreenSwitchUnlocked = !APSwitchManager.GreenSwitchUnlocked;
                Log($"Green Switch {(APSwitchManager.GreenSwitchUnlocked ? "enabled" : "disabled")}", true);
            }
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                DeathLinkInProgress = true;
                GameplayMaster.instance?.Die();
                Log($"Attempting to kill player", true);
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                ExplosionScript.SpawnNew(PlayerScript.instance.transform.position - new Vector3(0, 3, 0));
                //Log($"Spawning explosion at player");
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                PlayerScript.instance.propellerUsesLeft = 3;
                Log($"Granting Propeller", true);
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (PlayerScript.instance.invincible)
                {
                    PlayerScript.instance.InvincibleStop();
                    Log($"Toggling invincibility off", true);
                }
                else
                {
                    PlayerScript.instance.InvincibleSet(float.PositiveInfinity);
                    Log($"Toggling invincibility on", true);
                }
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                APAreaStateManager.RocketEnabled = !APAreaStateManager.RocketEnabled;
                Log($"Mosk Rocket {(APAreaStateManager.RocketEnabled ? "enabled" : "disabled")}", true);
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                APAreaStateManager.MindPasswordReceived = !APAreaStateManager.MindPasswordReceived;
                Log($"Morio's Password {(APAreaStateManager.MindPasswordReceived ? "enabled" : "disabled")}", true);
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                APAreaStateManager.DoggoReceived = !APAreaStateManager.DoggoReceived;
                Log($"Doggo {(APAreaStateManager.DoggoReceived ? "enabled" : "disabled")}", true);
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                APAreaStateManager.GelaToniReceived = !APAreaStateManager.GelaToniReceived;
                Log($"Gela-Toni {(APAreaStateManager.GelaToniReceived ? "enabled" : "disabled")}", true);
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                APAreaStateManager.PizzaKingReceived = !APAreaStateManager.PizzaKingReceived;
                Log($"Pizza King {(APAreaStateManager.PizzaKingReceived ? "enabled" : "disabled")}", true);
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                APAreaStateManager.FullGameUnlocked = !APAreaStateManager.FullGameUnlocked;
                Log($"Full Game {(APAreaStateManager.FullGameUnlocked ? "enabled" : "disabled")}", true);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                APAreaStateManager.LabDoorUnlocked = !APAreaStateManager.LabDoorUnlocked;
                Log($"DEBUG: Lab Key {(APAreaStateManager.LabDoorUnlocked ? "enabled" : "disabled")}", true);
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                var rotation = (int)Math.Round(PlayerScript.instance?.transform?.GetYAngle() ?? 0);
                if (rotation == 360)
                    rotation = 0;
                var zoneVals =
                    $"new Vector3({Math.Round(PlayerScript.instance?.transform?.position.x ?? 0, 1)}f, {Math.Round(PlayerScript.instance?.transform?.position.y ?? 0, 1)}f, {Math.Round(PlayerScript.instance?.transform?.position.z ?? 0, 1)}f), {rotation}, {ZoneMaster.currentZoneId}, {LightDirectionalScript.instance?.myLight?.enabled.ToString().ToLower() ?? "false"}, {WaterScript.instance?.WaterEnable.ToString().ToLower() ?? "false"}, \"{GameplayMaster.instance?.levelSoundtrack ?? "default"}\", \"{BackgroundMaster.instance?.name ?? "default"}\", \"{HudMasterScript.instance.currentMapAreaScriptableObject.areaName}\"),";
                Log($"Copying current zone values ({zoneVals}", true);
                GUIUtility.systemCopyBuffer = zoneVals;
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                var zoneVals =
                    $"\"{GameplayMaster.instance?.levelSoundtrack ?? "default"}\", \"{BackgroundMaster.instance?.name ?? "default"}\"),";
                Log($"Copying current music/bg values ({zoneVals}", true);
                GUIUtility.systemCopyBuffer = zoneVals;
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                Spawn.Instance("CutsceneHolder_DemoBombossBeated", new Vector3(0.0f, 512f, 0.0f));
            }

            if (Input.GetKeyDown(KeyCode.KeypadMultiply))
            {
                Log($"Copying Known BG/Soundtrack");
                var bgs = "";
                foreach (var bg in APAreaStateManager.KnownBackgrounds.Keys)
                {
                    bgs += $"\"{bg}\", ";
                }

                bgs = bgs.TrimEnd(',', ' ');

                var sts = "";
                foreach (var st in APAreaStateManager.KnownSoundtracks.Keys)
                {
                    sts += $"\"{st}\", ";
                }

                sts = sts.TrimEnd(',', ' ');

                GUIUtility.systemCopyBuffer = $"{bgs}\n{sts}";
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                Master.cheat_PizzaWheels = !Master.cheat_PizzaWheels;
                PlayerScript.instance.PizzaWheelsInit();
                Log($"DEBUG: Pizza Wheels {(Master.cheat_PizzaWheels ? "enabled" : "disabled")}", true);
            }

            if (Input.GetKeyDown(KeyCode.Home))
            {
                DebugLocationHelper.Enabled = !DebugLocationHelper.Enabled;
                Log($"DEBUG: Location Helper {(DebugLocationHelper.Enabled ? "enabled" : "disabled")}", true);
            }

            if (false)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    try
                    {
                        bgIndex = KnownBGs.ToList().IndexOf(BackgroundMaster.instance.name);
                    }
                    catch { }
                    bgIndex--;
                    if (bgIndex < 0)
                        bgIndex = KnownBGs.Length - 1;
                    Log($"Attempting to set background to [{bgIndex}]: {KnownBGs[bgIndex]}", true);
                    BackgroundMaster.Change(KnownBGs[bgIndex]);
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    try
                    {
                        bgIndex = KnownBGs.ToList().IndexOf(BackgroundMaster.instance.name);
                    }
                    catch { }
                    bgIndex++;
                    if (bgIndex >= KnownBGs.Length)
                        bgIndex = 0;
                    Log($"Attempting to set background to [{bgIndex}]: {KnownBGs[bgIndex]}", true);
                    BackgroundMaster.Change(KnownBGs[bgIndex]);
                }

                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    try
                    {
                        songIndex = KnownSongs.ToList().IndexOf(GameplayMaster.instance?.levelSoundtrack);
                    }
                    catch { }
                    songIndex--;
                    if (songIndex < 0)
                        songIndex = KnownSongs.Length - 1;
                    Log($"Attempting to set soundtrack to [{songIndex}]: {KnownSongs[songIndex]}", true);
                    GameplayMaster.instance.levelSoundtrack = KnownSongs[songIndex];
                }
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    try
                    {
                        songIndex = KnownSongs.ToList().IndexOf(GameplayMaster.instance?.levelSoundtrack);
                    }
                    catch { }
                    songIndex++;
                    if (songIndex >= KnownSongs.Length)
                        songIndex = 0;
                    Log($"Attempting to set soundtrack to [{songIndex}]: {KnownSongs[songIndex]}", true);
                    GameplayMaster.instance.levelSoundtrack = KnownSongs[songIndex];
                }
            }

            if (Input.GetKeyDown(KeyCode.BackQuote) || Input.GetKeyDown(KeyCode.Tilde))
            {
                ModMaster.instance.gameObject.GetComponent<ArchipelagoRenderer>().enabled =
                    !ModMaster.instance.gameObject.GetComponent<ArchipelagoRenderer>().enabled;
                Log($"Archipelago rendering {(ModMaster.instance.gameObject.GetComponent<ArchipelagoRenderer>().enabled ? "enabled" : "disabled")}", true);
            }
            orig(self);
        };
#endif
    }

#if DEBUG
    public int bgIndex = 0;
    public static readonly string[] KnownBGs =
    [
        "Background Soffitto Laboratorio",
        "Background Morio's Home Internal",
        "Background Sea and Sky",
        "Background Bonus Level",
        "Background Panik Arcade Internal",
        "Background Sea and Sky - Sunset",
        "Background Pizza Time",
        "Background Soffitto Castello",
        "Background Sky Night Moon",
        "Background Black",
        "Background Space",
        "Background Soffitto ToslaHQ",
        "Background Sky Night Moon Tosla Hq",
        "Background Morio's Island",
        "Background Bombeach",
        "Background Dark Sky Tosla Offices",
        "Background Skyline Autumn",
        "Background Sky Morio's Mind",
        "Background Skyline Night",
        "Backround Sky Poop World",
        "Background Simulation",
        "Background Panik Arcade Internal(Clone)",
    ];

    public int songIndex = 0;
    public static readonly string[] KnownSongs =
    [
        "SoundtrackHubOutside",
        "SoundtrackHubInside",
        "SoundtrackMoriosHome",
        "SoundtrackMoriosHomeInternal",
        "SoundtrackBonusLevel",
        "SoundtrackBombeach",
        "SoundtrackBossFight1",
        "MEGA_RAN_-_TAXI_REFERENCE",
        "Fasten_your_Seatbelt_MASTER Silence Cut",
        "CrGuitarfasten_your_seatbelts_wav",
        "SoundtrackArcadePanik",
        "SoundtrackHatShop",
        "SoundtrackPizzaTime",
        "SoundtrackTimeAttack",
        "SoundtrackToslaOffices",
        "SoundtrackBossFightImportant",
        "SoundtrackCityLevel",
        "SoundtrackCrashTestIndustries",
        "SoundtrackMoriosMind",
        "SoundtrackRuinedObservatory",
        "SoundtrackRocket",
        "SoundtrackToslaHQ",
        "SoundtrackMoonTheme",
        "SoundtrackBossFightFinal",
        "SoundtrackGym",
        "SoundtrackPoopWorld",
        "SoundtrackSewers"
    ];
#endif
}