using System;
using System.Collections.Generic;
using System.Text;
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

                foreach (var portal in PortalScript.list)
                {
                    if(!portal.PortalIsLevelPortal || portal.PortalIsAlreadyOpened)
                        continue;
                    var trueId = portal.gameObject.GetComponent<TruePortalId>();
                    Plugin.Log($"Checking if portal to {trueId.OriginalLevel} ({portal.gameObject.name}) should be opened");
                    var open = Data.GetLevel(trueId.OriginalLevel).everOpened = MiscSave.HasLevelPortalUnlocked(trueId.OriginalLevel);
                    if (open)
                    {
                        Plugin.Log($"Opening portal {trueId.OriginalLevel}");
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
                    // Todo: Add 
                    BunnySave.NeedsLoad = false;
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
