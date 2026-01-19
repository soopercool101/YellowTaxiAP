using UnityEngine;
using YellowTaxiAP.Behaviours;
using Object = UnityEngine.Object;

namespace YellowTaxiAP.Managers
{
    public class APPsychoTaxiManager
    {
        public APPsychoTaxiManager()
        {
            On.PsychoTaxiCassetteScript.Start += PsychoTaxiCassetteScript_Start;
            On.PsychoTaxiCassetteScript.OnTriggerEnter += PsychoTaxiCassetteScript_OnTriggerEnter;
        }

        private void PsychoTaxiCassetteScript_Start(On.PsychoTaxiCassetteScript.orig_Start orig, PsychoTaxiCassetteScript self)
        {
            if (Plugin.ArchipelagoClient.AllClearedLocations.Contains(4_20_99999) ||
                (!Plugin.SlotData.ShufflePsychoTaxi && APSaveController.MiscSave.HasPsychoTaxi))
            {
                Object.Destroy(self);
            }
        }

        private void PsychoTaxiCassetteScript_OnTriggerEnter(On.PsychoTaxiCassetteScript.orig_OnTriggerEnter orig, PsychoTaxiCassetteScript self, Collider other)
        {
            if (!Plugin.SlotData.ShufflePsychoTaxi)
            {
                if (other.gameObject != PlayerScript.instance.gameObject)
                    return;
                APSaveController.MiscSave.HasPsychoTaxi = true;
                orig(self, other);
                return;
            }

            if (other.gameObject != PlayerScript.instance.gameObject)
                return;
#if DEBUG
            DebugLocationHelper.CheckLocation("Psycho Taxi Cartridge", $"{(int)Data.LevelId.L4_ArcadePanik}_{Identifiers.PSYCHO_ID:D2}_99999");
#endif
            Plugin.ArchipelagoClient.SendLocation(4_20_99999);
            GenericPickupAnimationScript.SpawnNew("PickupVisualizer_PsychoTaxiCartridge").waitForDialogue = true;
            //Spawn.Instance("Dialogue Psycho Taxi - Cartridge found 1", Vector3.zero);
            Sound.Play_Unpausable("SoundLevelCollectiblePickup");
            Object.Destroy(self.gameObject);
        }
    }
}
