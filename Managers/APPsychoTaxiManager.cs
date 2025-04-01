using UnityEngine;
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

        private void PsychoTaxiCassetteScript_OnTriggerEnter(On.PsychoTaxiCassetteScript.orig_OnTriggerEnter orig, PsychoTaxiCassetteScript self, Collider other)
        {
            if (other.gameObject != PlayerScript.instance.gameObject)
                return;
            DebugLocationHelper.CheckLocation("Psycho Taxi Cartridge", $"{(int)Data.LevelId.L4_ArcadePanik}_{Identifiers.PSYCHO_ID:D2}_99999");
            GenericPickupAnimationScript.SpawnNew("PickupVisualizer_PsychoTaxiCartridge").waitForDialogue = true;
            //Spawn.Instance("Dialogue Psycho Taxi - Cartridge found 1", Vector3.zero);
            Sound.Play_Unpausable("SoundLevelCollectiblePickup");
            Object.Destroy(self.gameObject);
        }

        private void PsychoTaxiCassetteScript_Start(On.PsychoTaxiCassetteScript.orig_Start orig, PsychoTaxiCassetteScript self)
        {
            // TODO: Disable if the location has been checked. For now just keep the location enabled no matter what
        }
    }
}
