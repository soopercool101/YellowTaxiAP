using System;
using System.IO;
using System.Linq;
using System.Threading;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.BounceFeatures.DeathLink;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Helpers;
using Archipelago.MultiClient.Net.Models;
using Archipelago.MultiClient.Net.Packets;
using JetBrains.Annotations;
using Mono.Collections.Generic;
using Newtonsoft.Json.Linq;
using YellowTaxiAP.Behaviours;
using YellowTaxiAP.Managers;

namespace YellowTaxiAP.Archipelago;

public class ArchipelagoClient
{
    public const string APVersion = "0.6.6";
    private const string Game = "Yellow Taxi Goes Vroom";

    public static bool Authenticated;
    private bool attemptingConnection;

    public static ArchipelagoData ServerData = new();
    public static DeathLinkHandler DeathLinkHandler;
    private ArchipelagoSession session;

    /// <summary>
    /// call to connect to an Archipelago session. Connection info should already be set up on ServerData
    /// </summary>
    /// <returns></returns>
    public void Connect()
    {
        if (Authenticated || attemptingConnection) return;

        try
        {
            session = ArchipelagoSessionFactory.CreateSession(ServerData.Uri);
            SetupSession();
        }
        catch (Exception e)
        {
            Plugin.BepinLogger.LogError(e);
        }

        TryConnect();
    }

    /// <summary>
    /// add handlers for Archipelago events
    /// </summary>
    private void SetupSession()
    {
        session.MessageLog.OnMessageReceived += message => ArchipelagoConsole.LogMessage(message.ToString());
        session.Items.ItemReceived += OnItemReceived;
        session.Socket.ErrorReceived += OnSessionErrorReceived;
        session.Socket.SocketClosed += OnSessionSocketClosed;
    }

    /// <summary>
    /// attempt to connect to the server with our connection info
    /// </summary>
    private void TryConnect()
    {
        try
        {
            // it's safe to thread this function call but unity notoriously hates threading so do not use excessively
            ThreadPool.QueueUserWorkItem(
                _ => HandleConnectResult(
                    session.TryConnectAndLogin(
                        Game,
                        ServerData.SlotName,
                        ItemsHandlingFlags.AllItems, // TODO make sure to change this line
                        new Version(APVersion),
                        password: ServerData.Password,
                        requestSlotData: true // ServerData.NeedSlotData
                    )));
        }
        catch (Exception e)
        {
            Plugin.BepinLogger.LogError(e);
            HandleConnectResult(new LoginFailure(e.ToString()));
            attemptingConnection = false;
        }
    }

    /// <summary>
    /// handle the connection result and do things
    /// </summary>
    /// <param name="result"></param>
    private void HandleConnectResult(LoginResult result)
    {
        string outText;
        if (result.Successful)
        {
            var success = (LoginSuccessful)result;

            ServerData.SetupSession(success.SlotData, session.RoomState.Seed);
            Authenticated = true;

            var enableDeathlink = ServerData.SlotData.ContainsKey("death_link") && (long)ServerData.SlotData["death_link"] == 1;

            Plugin.Log($"SlotData logging ({ServerData.SlotData.Count} values)");
            foreach (var key in ServerData.SlotData.Keys)
            {
                Plugin.Log($"SlotData: {key} | {ServerData.SlotData[key]}");
            }

            Plugin.SlotData = new YTGVSlotData(ServerData.SlotData);

            if (ServerData.SlotData.ContainsKey("death_link"))
            {
                Plugin.Log($"death_link is {ServerData.SlotData["death_link"].GetType()} {enableDeathlink}");
            }
            else
            {
                Plugin.Log("No Death Link variable found!");
            }
            DeathLinkHandler = new(session.CreateDeathLinkService(), ServerData.SlotName, enableDeathlink);
            //session.Locations.CompleteLocationChecksAsync(ServerData.CheckedLocations.ToArray());

            if (!Plugin.SlotData.Hatsanity)
            {
                session.DataStorage[Scope.Slot, "UnlockedHats"].GetAsync().ContinueWith(x =>
                {
                    try
                    {
                        APSaveController.HatSave.SaveData = x.Result.ToObject<ulong>();
                        APSaveController.HatSave.NeedsLoad = true;
                    }
                    catch
                    {
                        Plugin.Log("Hat Load Failed");
                        try
                        {
                            APSaveController.HatSave = new YTGVHatSave(1)
                            {
                                NeedsLoad = true
                            };
                            session.DataStorage[Scope.Slot, "UnlockedHats"].Initialize(1);
                            Plugin.Log("Hat State initialized");
                        }
                        catch
                        {
                            Plugin.Log("Hat State failed initialization");
                            throw;
                        }
                    }
                });
                session.DataStorage[Scope.Slot, "UnlockedHats"].OnValueChanged += HatData_OnValueChanged;
            }
            if (!Plugin.SlotData.Bunnysanity)
            {
                session.DataStorage[Scope.Slot, "Bunnies"].GetAsync().ContinueWith(x =>
                {
                    try
                    {
                        APSaveController.BunnySave.SaveData = x.Result.ToObject<ulong>();
                        APSaveController.BunnySave.NeedsLoad = true;
                    }
                    catch
                    {
                        Plugin.Log("Bunny Load Failed");
                        try
                        {
                            APSaveController.BunnySave = new YTGVBunnySave(0)
                            {
                                NeedsLoad = true
                            };
                            session.DataStorage[Scope.Slot, "Bunnies"].Initialize(0);
                            Plugin.Log("Bunny State initialized");
                        }
                        catch
                        {
                            Plugin.Log("Bunnies failed initialization");
                            throw;
                        }
                    }
                });
                session.DataStorage[Scope.Slot, "Bunnies"].OnValueChanged += Bunnies_OnValueChanged;
            }
            session.DataStorage[Scope.Slot, "Save"].GetAsync().ContinueWith(x =>
            {
                try
                {
                    APSaveController.MiscSave.SaveData = x.Result.ToObject<uint>();
                    APSaveController.MiscSave.NeedsLoad = true;
                }
                catch
                {
                    Plugin.Log("Save Load Failed");
                    try
                    {
                        APSaveController.MiscSave = new YTGVMiscSave(0)
                        {
                            NeedsLoad = true
                        };
                        session.DataStorage[Scope.Slot, "Save"].Initialize(0);
                        Plugin.Log("Save State initialized");
                    }
                    catch
                    {
                        Plugin.Log("Save failed initialization");
                        throw;
                    }
                }
            });
            session.DataStorage[Scope.Slot, "Save"].OnValueChanged += Save_OnValueChanged;

            session.DataStorage[Scope.Slot, "Wallet"].GetAsync().ContinueWith(x =>
            {
                try
                {
                    Data.coinsCollected[Data.gameDataIndex] = APWalletManager.ServerCoins = x.Result.ToObject<int>();
                    Plugin.Log($"Wallet Load Finished: {APWalletManager.ServerCoins}");
                }
                catch
                {
                    Plugin.Log("Wallet Load Failed");
                    try
                    {
                        Data.coinsCollected[Data.gameDataIndex] = APWalletManager.ServerCoins = 0;
                        session.DataStorage[Scope.Slot, "Wallet"].Initialize(0);
                        Plugin.Log("Wallet State initialized");
                    }
                    catch
                    {
                        Plugin.Log("Wallet failed initialization");
                        throw;
                    }
                }
            });
            session.DataStorage[Scope.Slot, "Wallet"].OnValueChanged += Wallet_OnValueChanged;

            outText = $"Successfully connected to {ServerData.Uri} as {ServerData.SlotName}!";

            ArchipelagoConsole.LogMessage(outText);
            try
            {
                if (Directory.Exists(Plugin.PluginDirectory))
                {
                    if (File.Exists(Plugin.LoginDetailsFile))
                        File.Delete(Plugin.LoginDetailsFile);
                    File.WriteAllLines(Plugin.LoginDetailsFile, [ServerData.Uri, ServerData.SlotName, ServerData.Password]);
                }
            }
            catch
            {
                Plugin.BepinLogger.LogWarning("Failed to save connection info");
            }
        }
        else
        {
            var failure = (LoginFailure)result;
            outText = $"Failed to connect to {ServerData.Uri} as {ServerData.SlotName}.";
            outText = failure.Errors.Aggregate(outText, (current, error) => current + $"\n    {error}");

            Plugin.BepinLogger.LogError(outText);

            Authenticated = false;
            Disconnect();
        }

        ArchipelagoConsole.LogMessage(outText);
        attemptingConnection = false;
    }

    /// <summary>
    /// something we wrong or we need to properly disconnect from the server. cleanup and re null our session
    /// </summary>
    private void Disconnect()
    {
        Plugin.BepinLogger.LogDebug("disconnecting from server...");
        session?.Socket.DisconnectAsync();
        session = null;
        Authenticated = false;
    }

    public void SendMessage(string message)
    {
        session.Socket.SendPacketAsync(new SayPacket { Text = message });
    }

    /// <summary>
    /// we received an item so reward it here
    /// </summary>
    /// <param name="helper">item helper which we can grab our item from</param>
    private void OnItemReceived(ReceivedItemsHelper helper)
    {
        var receivedItem = helper.DequeueItem();

        if (helper.Index < ServerData.Index) return;

        ServerData.Index++;

        switch ((Identifiers.ItemID) receivedItem.ItemId)
        {
            case Identifiers.ItemID.Gear:
                Data.gearsUnlockedNumber[Data.gameDataIndex] += 1;
                GameStateUpdater.GearStateNeedsUpdate = true;
                break;
            case Identifiers.ItemID.Coin1:
                ReceiveCoins(1);
                break;
            case Identifiers.ItemID.Coins10:
                ReceiveCoins(10);
                break;
            case Identifiers.ItemID.Coins25:
                ReceiveCoins(25);
                break;
            case Identifiers.ItemID.Coins100:
                ReceiveCoins(100);
                break;
            case Identifiers.ItemID.FlipOWill:
                APPlayerManager.BoostItems = 2;
                APPlayerManager.JumpItems = 2;
                APPlayerManager.SpinAttackItem = true;
                break;
            case Identifiers.ItemID.ProgressiveBoost:
                APPlayerManager.BoostItems++;
                break;
            case Identifiers.ItemID.ProgressiveJump:
                APPlayerManager.JumpItems++;
                break;
            case Identifiers.ItemID.SpinAttack:
                APPlayerManager.SpinAttackItem = true;
                break;
            case Identifiers.ItemID.Glide:
                APPlayerManager.GlideEnabledItem = true;
                break;
            case Identifiers.ItemID.GoldenSpringUnlock:
                APCollectableManager.GoldenSpringActive = true;
                break;
            case Identifiers.ItemID.GoldenPropellerUnlock:
                APCollectableManager.GoldenPropellerActive = true;
                break;
            case Identifiers.ItemID.Bunny:
                APDataManager.TotalBunniesReceived++;
                break;
            case Identifiers.ItemID.BunnyMoriosLab:
                Data.GetLevel(Data.LevelId.Hub).bunniesUnlocked++;
                APDataManager.TotalBunniesReceived++;
                break;
            case Identifiers.ItemID.BunnyBombeach:
                Data.GetLevel(Data.LevelId.L1_Bombeach).bunniesUnlocked++;
                APDataManager.TotalBunniesReceived++;
                break;
            case Identifiers.ItemID.BunnyPizzaTime:
                Data.GetLevel(Data.LevelId.L2_PizzaTime).bunniesUnlocked++;
                APDataManager.TotalBunniesReceived++;
                break;
            case Identifiers.ItemID.BunnyMoriosHome:
                Data.GetLevel(Data.LevelId.L3_MoriosHome).bunniesUnlocked++;
                APDataManager.TotalBunniesReceived++;
                break;
            case Identifiers.ItemID.BunnyArcadePanik:
                Data.GetLevel(Data.LevelId.L4_ArcadePanik).bunniesUnlocked++;
                APDataManager.TotalBunniesReceived++;
                break;
            case Identifiers.ItemID.BunnyToslasOffices:
                Data.GetLevel(Data.LevelId.L5_ToslaOffices).bunniesUnlocked++;
                APDataManager.TotalBunniesReceived++;
                break;
            case Identifiers.ItemID.BunnyGymGears:
                Data.GetLevel(Data.LevelId.L6_Gym).bunniesUnlocked++;
                APDataManager.TotalBunniesReceived++;
                break;
            case Identifiers.ItemID.BunnyFecalMatters:
                Data.GetLevel(Data.LevelId.L7_PoopWorld).bunniesUnlocked++;
                APDataManager.TotalBunniesReceived++;
                break;
            case Identifiers.ItemID.BunnyFlushedAway:
                Data.GetLevel(Data.LevelId.L8_Sewers).bunniesUnlocked++;
                APDataManager.TotalBunniesReceived++;
                break;
            case Identifiers.ItemID.BunnyMauriziosCity:
                Data.GetLevel(Data.LevelId.L9_City).bunniesUnlocked++;
                APDataManager.TotalBunniesReceived++;
                break;
            case Identifiers.ItemID.BunnyCrashTestIndustries:
                Data.GetLevel(Data.LevelId.L10_CrashTestIndustries).bunniesUnlocked++;
                APDataManager.TotalBunniesReceived++;
                break;
            case Identifiers.ItemID.BunnyDemo:
                // Placeholder
                //Data.GetLevel(Data.LevelId.L11_HubDemo).bunniesUnlocked++;
                //APDataManager.TotalBunniesReceived++;
                break;
            case Identifiers.ItemID.BunnyMoriosMind:
                Data.GetLevel(Data.LevelId.L12_MoriosMind).bunniesUnlocked++;
                APDataManager.TotalBunniesReceived++;
                break;
            case Identifiers.ItemID.BunnyRuinedObservatory:
                Data.GetLevel(Data.LevelId.L13_StarmanCastle).bunniesUnlocked++;
                APDataManager.TotalBunniesReceived++;
                break;
            case Identifiers.ItemID.BunnyToslaHQ:
                Data.GetLevel(Data.LevelId.L14_ToslaHQ).bunniesUnlocked++;
                APDataManager.TotalBunniesReceived++;
                break;
            case Identifiers.ItemID.BunnyMoon:
                Data.GetLevel(Data.LevelId.L15_Moon).bunniesUnlocked++;
                APDataManager.TotalBunniesReceived++;
                break;
            case Identifiers.ItemID.GelaToni:
                APAreaStateManager.GelaToniReceived = true;
                break;
            case Identifiers.ItemID.PizzaKing:
                APAreaStateManager.PizzaKingReceived = true;
                break;
            case Identifiers.ItemID.Doggo:
                APAreaStateManager.DoggoReceived = true;
                break;
            case Identifiers.ItemID.OrangeSwitch:
                APSwitchManager.OrangeSwitchUnlocked = true;
                break;
            case Identifiers.ItemID.MoriosPassword:
                APAreaStateManager.MindPasswordReceived = true;
                break;
            case Identifiers.ItemID.MosksRocket:
                APAreaStateManager.RocketEnabled = true;
                break;
            case Identifiers.ItemID.PsychoTaxiCartridge:
                Data.psychoTaxiMode1_Unlocked[Data.gameDataIndex] =
                    Data.psychoTaxiMode1_UnlockedCutsceneShown[Data.gameDataIndex] =
                        Data.psychoTaxiMode1_ExplanationDialogueShown[Data.gameDataIndex] = true;
                break;
            case Identifiers.ItemID.Michele:
                APRatManager.ReceivedRatItem = true;
                GameStateUpdater.RatStateNeedsUpdate = true;
                break;
            default:
                Plugin.Log($"Error: Unknown item ID: {receivedItem.ItemId}");
                throw new ArgumentOutOfRangeException();
        }
    }

    private void ReceiveCoins(int coinCount)
    {
        if (GameplayMaster.instance?.levelId == null || GameplayMaster.instance.levelId == Data.LevelId.noone)
            return;
        Data.coinsCollected[Data.gameDataIndex] += coinCount;
        UpdateWallet(coinCount);
    }

    /// <summary>
    /// something went wrong with our socket connection
    /// </summary>
    /// <param name="e">thrown exception from our socket</param>
    /// <param name="message">message received from the server</param>
    private void OnSessionErrorReceived(Exception e, string message)
    {
        Plugin.BepinLogger.LogError(e);
        ArchipelagoConsole.LogMessage(message);
    }

    /// <summary>
    /// something went wrong closing our connection. disconnect and clean up
    /// </summary>
    /// <param name="reason"></param>
    private void OnSessionSocketClosed(string reason)
    {
        Plugin.BepinLogger.LogError($"Connection to Archipelago lost: {reason}");
        Disconnect();
    }

    public void SendLocation(long id)
    {
        Plugin.BepinLogger.LogMessage($"Sending location #{id}");
        session.Locations.CompleteLocationChecks(id);
    }

    public void SendLocations(long[] ids)
    {
        session.Locations.CompleteLocationChecks(ids);
    }

    public System.Collections.ObjectModel.ReadOnlyCollection<long> AllClearedLocations => session.Locations.AllLocationsChecked;
    public System.Collections.ObjectModel.ReadOnlyCollection<long> AllLocations => session.Locations.AllLocations;

    private object hatDataLock = new();

    private void HatData_OnValueChanged(JToken originalValue, JToken newValue, System.Collections.Generic.Dictionary<string, JToken> additionalArguments)
    {
        lock (hatDataLock)
        {
            var newVal = newValue.ToObject<ulong>();
            if (APSaveController.HatSave.SaveData == newVal)
                return;

            APSaveController.HatSave.SaveData = newVal;
            Plugin.Log($"Updated hat data: {APSaveController.HatSave.SaveData:x16}");
            APSaveController.HatSave.NeedsLoad = true;
        }
    }

    public void SaveDSHatData()
    {
        lock (hatDataLock)
        {
            if (Plugin.SlotData.Hatsanity)
                return;

            try
            {
                session.DataStorage[Scope.Slot, "UnlockedHats"] = (JToken)APSaveController.HatSave.SaveData;
                Plugin.Log($"Saved hat data: {APSaveController.HatSave.SaveData:x16}");
            }
            catch
            {
                try
                {
                    session.DataStorage[Scope.Slot, "UnlockedHats"].Initialize(APSaveController.HatSave.SaveData);
                    Plugin.Log($"Initialized hat data: {APSaveController.HatSave.SaveData:x16}");
                }
                catch
                {
                    Plugin.Log("Could not save hat data");
                    throw;
                }
            }
        }
    }

    private object bunnyLock = new();
    private void Bunnies_OnValueChanged(JToken originalValue, JToken newValue, System.Collections.Generic.Dictionary<string, JToken> additionalArguments)
    {
        lock (bunnyLock)
        {
            var newVal = newValue.ToObject<ulong>();
            if (APSaveController.BunnySave.SaveData == newVal)
                return;

            APSaveController.BunnySave.SaveData = newVal;
            Plugin.Log($"Updated bunny data: {APSaveController.BunnySave.SaveData:x16}");
            APSaveController.BunnySave.NeedsLoad = true;
        }
    }

    public void SaveDSBunnyData()
    {
        lock (bunnyLock)
        {
            if (Plugin.SlotData.Bunnysanity)
                return;

            try
            {
                session.DataStorage[Scope.Slot, "Bunnies"] = (JToken)APSaveController.BunnySave.SaveData;
                Plugin.Log($"Saved bunny data: {APSaveController.BunnySave.SaveData:x16}");
            }
            catch
            {
                try
                {
                    session.DataStorage[Scope.Slot, "Bunnies"].Initialize(APSaveController.BunnySave.SaveData);
                    Plugin.Log($"Initialized bunny data: {APSaveController.BunnySave.SaveData:x16}");
                }
                catch
                {
                    Plugin.Log("Could not save bunny data");
                    throw;
                }
            }
        }
    }

    private object saveLock = new();
    private void Save_OnValueChanged(JToken originalValue, JToken newValue, System.Collections.Generic.Dictionary<string, JToken> additionalArguments)
    {
        lock (saveLock)
        {
            if (APSaveController.MiscSave.SaveData == newValue.ToObject<uint>())
                return;

            APSaveController.MiscSave.SaveData = newValue.ToObject<uint>();
            Plugin.Log($"Updated save data: {APSaveController.MiscSave.SaveData:x8}");
            APSaveController.MiscSave.NeedsLoad = true;
        }
    }

    public void SaveDSSaveData()
    {
        lock (saveLock)
        {
            try
            {
                session.DataStorage[Scope.Slot, "Save"] = (JToken)APSaveController.MiscSave.SaveData;
                Plugin.Log($"Saved data: {APSaveController.MiscSave.SaveData:x8}");
            }
            catch
            {
                try
                {
                    session.DataStorage[Scope.Slot, "Save"].Initialize(APSaveController.MiscSave.SaveData);
                    Plugin.Log($"Initialized save data: {APSaveController.MiscSave.SaveData:x8}");
                }
                catch
                {
                    Plugin.Log("Could not save data");
                    throw;
                }
            }
        }
    }

    private object walletLock = new ();
    private void Wallet_OnValueChanged(JToken originalValue, JToken newValue, System.Collections.Generic.Dictionary<string, JToken> additionalArguments)
    {
        lock (walletLock)
        {
            Data.coinsCollected[Data.gameDataIndex] = APWalletManager.ServerCoins = newValue.ToObject<int>();
        }
    }

    public void UpdateWallet(int amountChanged)
    {
        try
        {
            session.DataStorage[Scope.Slot, "Wallet"] += amountChanged;
            Plugin.BepinLogger.LogMessage($"Changed wallet: {amountChanged}");
        }
        catch
        {
            try
            {
                session.DataStorage[Scope.Slot, "Wallet"].Initialize(Data.coinsCollected[Data.gameDataIndex]);
                APWalletManager.ServerCoins = Data.coinsCollected[Data.gameDataIndex];
                //Plugin.Log($"Initialized wallet: {Data.coinsCollected[Data.gameDataIndex]}");
            }
            catch
            {
                Plugin.Log("Could not save wallet");
                throw;
            }
        }
    }
}