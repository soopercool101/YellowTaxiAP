using System;
using UnityEngine;
using YellowTaxiAP.Archipelago;
using YellowTaxiAP.Managers;

namespace YellowTaxiAP.Behaviours
{
    public class APSaveController : MonoBehaviour
    {
        public static APSaveController Instance { get; private set; }

        public static YTGVMiscSave MiscSave = new(0);
        private static Data.Hat previousHat;
        public static YTGVHatSave HatSave = new(1);
        public static YTGVBunnySave BunnySave = new(0);

        public void Awake()
        {
            if (Instance)
                throw new NotSupportedException("Can only have one Game State updater!");

            Instance = this;
        }

        public void FixedUpdate()
        {
            if (MiscSave.NeedsLoad)
            {
                Plugin.Log("Loading save data");
                MiscSave.NeedsLoad = false;
                
                // Update current hat
                var hat = Data.HatGetCurrentKind();
                Data.currentHat[Data.gameDataIndex] = (int)hat;
                Plugin.Log($"Checking hat state {previousHat}->{MiscSave.CurrentHat}");
                if (PlayerScript.instance && previousHat != MiscSave.CurrentHat)
                {
                    HatScript.RemoveHat(false);
                    if (hat != Data.Hat.Noone)
                    {
                        HatScript.Instantiate(hat);
                    }
                    PlayerScript.PlayerHatsRenderingUpdate();
                }

                // Update world states
                if (!Plugin.SlotData.ShuffleGelaToni)
                {
                    APAreaStateManager.GelaToniReceived = MiscSave.HasGelaToni;
                }
                if (!Plugin.SlotData.ShufflePizzaKing)
                {
                    APAreaStateManager.PizzaKingReceived = MiscSave.HasPizzaKing;
                }
                if (!Plugin.SlotData.ShuffleDoggo)
                {
                    APAreaStateManager.DoggoReceived = MiscSave.HasDoggo;
                }
                if (!Plugin.SlotData.ShuffleMoriosPassword)
                {
                    APAreaStateManager.MindPasswordReceived = MiscSave.HasMoriosMindPassword;
                }
                if (!Plugin.SlotData.ShuffleRocket)
                {
                    APAreaStateManager.RocketEnabled = MiscSave.HasRocket;
                }

                // Update collectable states
                if (!Plugin.SlotData.ShuffleGoldenSpring)
                {
                    APCollectableManager.GoldenSpringActive = MiscSave.HasGoldenSpring;
                }
                if (!Plugin.SlotData.ShuffleGoldenPropeller)
                {
                    APCollectableManager.GoldenPropellerActive = MiscSave.HasGoldenPropeller;
                }
                if (!Plugin.SlotData.ShuffleOrangeSwitch)
                {
                    APSwitchManager.OrangeSwitchUnlocked = MiscSave.HasOrangeSwitch;
                }

                if (!Plugin.SlotData.ShufflePsychoTaxi)
                {
                    Data.psychoTaxiMode1_Unlocked[Data.gameDataIndex] = Data.psychoTaxiMode1_UnlockedCutsceneShown[Data.gameDataIndex] =
                        Data.psychoTaxiMode1_ExplanationDialogueShown[Data.gameDataIndex] = MiscSave.HasPsychoTaxi;
                }

                // Update key cutscene states
                Data.morioMindDreamMachineUsedOnce[Data.gameDataIndex] =
                    MiscSave.HasLevelPortalUnlocked(Data.LevelId.L12_MoriosMind);

                foreach (var portal in PortalScript.list)
                {
                    if(!portal.PortalIsLevelPortal || portal.PortalIsAlreadyOpened)
                        continue;
                    var trueId = portal.gameObject.GetComponent<TruePortalId>();
                    var open = Data.GetLevel(trueId.OriginalLevel).everOpened = MiscSave.HasLevelPortalUnlocked(trueId.OriginalLevel);
                    if (open)
                    {
                        portal.PortalOpenedSet();
                    }
                }

                previousHat = MiscSave.CurrentHat;
            }

            if (MiscSave.NeedsSave)
            {
                Plugin.BepinLogger.LogWarning("Saving AP Save Data");
                MiscSave.NeedsSave = false;
                previousHat = MiscSave.CurrentHat;
                Plugin.ArchipelagoClient.SaveDSSaveData();
            }

            if (!Plugin.SlotData.Hatsanity)
            {
                if (HatSave.NeedsLoad)
                {
                    Plugin.BepinLogger.LogWarning("Loading Hat Data");
                    HatSave.NeedsLoad = false;
                    // Todo: update hat objects if needed?
                }

                if (HatSave.NeedsSave)
                {
                    Plugin.BepinLogger.LogWarning("Saving Hat Data");
                    HatSave.NeedsSave = false;
                    Plugin.ArchipelagoClient.SaveDSHatData();
                }
            }

            if (!Plugin.SlotData.Bunnysanity)
            {
                if (BunnySave.NeedsLoad)
                {
                    Plugin.BepinLogger.LogWarning("Loading Bunny Data");
                    var bunnyCount = Data.GetLevel(Data.LevelId.Hub).bunniesUnlocked =
                        BunnySave.GetBunnyCount(Data.LevelId.Hub) + BunnySave.GetBunnyCount(Data.LevelId.L11_HubDemo);
                    for (var i = 1; i < 19; i++)
                    {
                        if (i == (int) Data.LevelId.L11_HubDemo) // Ignore, these were added to the hub
                            continue;
                        var bunnies = Data.GetLevel((Data.LevelId)i).bunniesUnlocked = BunnySave.GetBunnyCount((Data.LevelId)i);
                        bunnyCount += bunnies;
                    }

                    Data.totalBunniesCount = bunnyCount;
                    BunnySave.NeedsLoad = false;

                    if (GameplayMaster.instance && GameplayMaster.instance.levelId == Data.LevelId.L16_Rocket)
                    {
                        foreach (var portal in PortalScript.list)
                        {
                            portal.CostUpdateTry();
                        }
                    }
                }

                if (BunnySave.NeedsSave)
                {
                    Plugin.BepinLogger.LogWarning("Saving Bunny Data");
                    BunnySave.NeedsSave = false;
                    Plugin.ArchipelagoClient.SaveDSBunnyData();
                }
            }
        }
    }
}
