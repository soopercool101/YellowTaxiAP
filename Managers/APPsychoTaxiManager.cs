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
            On.PsychoTaxiCabinetScript.Awake += PsychoTaxiCabinetScript_Awake;
            On.PsychoTaxiCabinetScript.Update += PsychoTaxiCabinetScript_Update;

        }

        private void PsychoTaxiCabinetScript_Update(On.PsychoTaxiCabinetScript.orig_Update orig, PsychoTaxiCabinetScript self)
        {
            orig(self);
            if (TrueLevelDisplay)
            {
                if (!self.turnedOn)
                {
                    TrueLevelDisplay.gameObject.SetActive(false);
                }
                else if (self.cabMeshRend.sharedMaterial == self.turnedOnMat)
                {
                    self.cabMeshRend.sharedMaterial = self.turnedOffmat;
                    TrueLevelDisplay.gameObject.SetActive(true);
                    TrueLevelDisplay.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 0.8f);
                }
                else
                {
                    TrueLevelDisplay.gameObject.SetActive(true);
                    TrueLevelDisplay.GetComponent<SpriteRenderer>().color = new Color(0.72f, 0.72f, 0.72f);
                }
            }
        }

        public SpriteRenderer TrueLevelDisplay;
        private void PsychoTaxiCabinetScript_Awake(On.PsychoTaxiCabinetScript.orig_Awake orig, PsychoTaxiCabinetScript self)
        {
            var targetLevel = APPortalManager.GetRandomizedLevelId(Data.LevelId.L20_PsychoTaxi, true);
            if (targetLevel != Data.LevelId.L20_PsychoTaxi)
            {
                TrueLevelDisplay = new GameObject("Psycho Level Display").AddComponent<SpriteRenderer>();
                var originalSprite = Colors.PortalTextureGet(targetLevel);
                TrueLevelDisplay.sprite = Sprite.Create(originalSprite.texture, new Rect(28, 53, 197, 155),
                    new Vector2(0.5f, 0.5f), 32);
                TrueLevelDisplay.transform.position = new Vector3(-785.0f, 65.06f, 616.78f);
                TrueLevelDisplay.transform.Rotate(-16, 0, 0);
                TrueLevelDisplay.transform.SetParent(self.transform);
                TrueLevelDisplay.transform.localScale = new Vector3(1.05f, 1.05f, 1.05f);
            }

            orig(self);
        }

        private void PsychoTaxiCassetteScript_Start(On.PsychoTaxiCassetteScript.orig_Start orig, PsychoTaxiCassetteScript self)
        {
            if (Plugin.ArchipelagoClient.AllClearedLocations.Contains((long)GameplayMaster.instance.levelId * 1_00_00000 + (long) Identifiers.NotableLocations.HubPsychoTaxi) ||
                (!Plugin.SlotData.ShufflePsychoTaxi && APSaveController.MiscSave.HasPsychoTaxi))
            {
                Object.Destroy(self.gameObject);
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
            DebugLocationHelper.CheckLocation("Psycho Taxi Cartridge", $"{(int)GameplayMaster.instance.levelId}_{Identifiers.PSYCHO_ID:D2}_99999");
#endif
            Plugin.ArchipelagoClient.SendLocation((long)GameplayMaster.instance.levelId * 1_00_00000 + (long)Identifiers.NotableLocations.HubPsychoTaxi);
            GenericPickupAnimationScript.SpawnNew("PickupVisualizer_PsychoTaxiCartridge").waitForDialogue = true;
            //Spawn.Instance("Dialogue Psycho Taxi - Cartridge found 1", Vector3.zero);
            Sound.Play_Unpausable("SoundLevelCollectiblePickup");
            Object.Destroy(self.gameObject);
        }
    }
}
