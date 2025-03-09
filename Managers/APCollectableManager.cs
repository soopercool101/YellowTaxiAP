using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace YellowTaxiAP.Managers
{
    public class APCollectableManager
    {
        public static bool GoldenPropellerActive = false;
        public static bool GoldenSpringActive = false;

        public APCollectableManager()
        {
            // BonusScript hooks
            On.BonusScript.Start += BonusScript_Start;
            On.BonusScript.Update += Update_AP;
            On.BonusScript.CoinPickedUpGet += BonusScript_CoinPickedUpGet;
            //On.BonusScript.CoinPickedUpSet += BonusScript_CoinPickedUpSet;
            //On.BonusScript.OnDestroy += BonusScript_OnDestroy;

            On.MapArea.MarkDiscovered += MapArea_MarkDiscovered;
            //On.Level.StartLoadedScene += Level_StartLoadedScene;

            On.PlayerScript.OnTriggerStay += PlayerScript_OnTriggerStay;

            On.GearAnimationScript.Update += GearAnimationScript_Update;
        }

        /// <summary>
        /// Complete reimplementation to prevent unnecessary destruction of needed objects
        /// </summary>
        /// <param name="orig"></param>
        /// <param name="self"></param>
        private void BonusScript_Start(On.BonusScript.orig_Start orig, BonusScript self)
        {
            if (self.doublePickupPreventionMeshRenderer != null)
                self.CoinPickedUpMaterialRefresh();
            if (GameplayMaster.instance != null && GameplayMaster.instance.levelId == Data.LevelId.L12_MoriosMind && !self.backupInstance)
            {
                switch (self.myIdentity)
                {
                    case BonusScript.Identity.coin:
                    {
                        var sharedMaterial = self.myMeshRend.sharedMaterial;
                        self.myMeshFilter = self.GetComponentInChildren<MeshFilter>(true);
                        self.myMeshFilter.mesh = AssetMaster.GetGeneric<Mesh>("MeltedCoin");
                        self.myMeshRend.sharedMaterial = sharedMaterial;
                        self.myMeshFilter.transform.localScale *= 100f;
                        break;
                    }
                    case BonusScript.Identity.bigCoin10:
                    {
                        var sharedMaterial = self.myMeshRend.sharedMaterial;
                        self.myMeshFilter = self.GetComponentInChildren<MeshFilter>(true);
                        self.myMeshFilter.mesh = AssetMaster.GetGeneric<Mesh>("Sacchetto di monete Sogno");
                        self.myMeshRend.sharedMaterial = sharedMaterial;
                        self.myMeshFilter.transform.localScale *= 100f;
                        break;
                    }
                    case BonusScript.Identity.bigCoin25:
                    {
                        var sharedMaterial = self.myMeshRend.sharedMaterial;
                        self.myMeshFilter = self.GetComponentInChildren<MeshFilter>(true);
                        self.myMeshFilter.mesh = AssetMaster.GetGeneric<Mesh>("Forziere Sogno");
                        self.myMeshRend.sharedMaterial = sharedMaterial;
                        self.myMeshFilter.transform.localScale *= 100f;
                        break;
                    }
                    case BonusScript.Identity.bigCoin100:
                    {
                        var sharedMaterial = self.myMeshRend.sharedMaterial;
                        self.myMeshFilter = self.GetComponentInChildren<MeshFilter>(true);
                        self.myMeshFilter.mesh = AssetMaster.GetGeneric<Mesh>("Cassaforte Sogno");
                        self.myMeshRend.sharedMaterial = sharedMaterial;
                        self.myMeshFilter.transform.localScale *= 100f;
                        break;
                    }
                    case BonusScript.Identity.gear:
                    {
                        var sharedMaterial = self.myMeshRend.sharedMaterial;
                        self.myMeshFilter = self.GetComponentInChildren<MeshFilter>(true);
                        self.myMeshFilter.mesh = AssetMaster.GetGeneric<Mesh>("MeltedGear");
                        self.myMeshRend.sharedMaterial = sharedMaterial;
                        self.myMeshFilter.transform.localScale *= 100f;
                        var componentInChildren = self.gearOutlineTr.GetComponentInChildren<MeshFilter>();
                        componentInChildren.mesh = self.gearOutlineMindMesh;
                        double num = componentInChildren.transform.SetLocalY(0.2f);
                        break;
                    }
                    case BonusScript.Identity.timer:
                    {
                        var sharedMaterial = self.myMeshRend.sharedMaterial;
                        self.myMeshFilter = self.GetComponentInChildren<MeshFilter>(true);
                        self.myMeshFilter.mesh = AssetMaster.GetGeneric<Mesh>("MeltedTimer7");
                        self.myMeshRend.sharedMaterial = sharedMaterial;
                        self.myMeshFilter.transform.localScale *= 100f;
                        break;
                    }
                    case BonusScript.Identity.bigTimer:
                    {
                        var sharedMaterial = self.myMeshRend.sharedMaterial;
                        self.myMeshFilter = self.GetComponentInChildren<MeshFilter>(true);
                        self.myMeshFilter.mesh = AssetMaster.GetGeneric<Mesh>("Sveglia Sciolta");
                        self.myMeshRend.sharedMaterial = sharedMaterial;
                        self.myMeshFilter.transform.localScale *= 100f;
                        break;
                    }
                    case BonusScript.Identity.bigBigTimer50:
                    case BonusScript.Identity.bigBigTimer100:
                    {
                        var sharedMaterial = self.myMeshRend.sharedMaterial;
                        self.myMeshFilter = self.GetComponentInChildren<MeshFilter>(true);
                        self.myMeshFilter.mesh = AssetMaster.GetGeneric<Mesh>("Taximetro Sciolto");
                        self.myMeshRend.sharedMaterial = sharedMaterial;
                        self.myMeshFilter.transform.localScale *= 100f;
                        break;
                    }
                }
            }
            if (self.myIdentity == BonusScript.Identity.gear)
                self.GearAlreadyPickedUpRefresh();
            else if (self.killCondition_GononoBombeach && !Data.gononoBombeachDelivered[Data.gameDataIndex])
                self.KillMe();
            if (self.myIdentity == BonusScript.Identity.bunny)
            {
                if (self.bunnyIndex < 0)
                    Debug.LogError("Bunny bonus doesn't have an index defined!");
                self.bunnyDefaultMaterial = self.myMeshRend.sharedMaterial;
                self.BunnyAlreadyPickedUpRefresh();
            }
            // Never destroy these objects
            //if (self.myIdentity == BonusScript.Identity.invincibilitySpring && GameplayMaster.instance.levelId != Data.LevelId.L5_ToslaOffices && GameplayMaster.instance.levelId != Data.LevelId.L14_ToslaHQ && !Data.goldenSpringUnlocked[Data.gameDataIndex])
            //    Object.Destroy(self.gameObject);
            //else if (self.myIdentity == BonusScript.Identity.goldenPropeller && GameplayMaster.instance.levelId != Data.LevelId.L13_StarmanCastle && GameplayMaster.instance.levelId != Data.LevelId.L14_ToslaHQ && !Data.cutscenePropellerFirstTimePickup[Data.gameDataIndex])
            //    Object.Destroy(self.gameObject);
            //else if (self.myIdentity == BonusScript.Identity.morioMindPassword && Data.morioMindPasswordGot[Data.gameDataIndex])
            //    Object.Destroy(self.gameObject);
            else
            {
                if (self.rotateMe != null)
                {
                    if (self.rortationSin)
                        self.onUpdate += self.RotateSin;
                    else
                        self.onUpdate += self.RotateFully;
                }
                if (self.alwaysFaceCamera)
                    self.onUpdate += self.FaceCamera;
                if (self.myIdentity == BonusScript.Identity.gear)
                    self.onUpdate += self.GearsCode;
                if (self.myIdentity != BonusScript.Identity.coin)
                    return;
                self.onUpdate += self.CoinNearPlayer;
            }
        }

        /// <summary>
        /// When a third gear is collected, typically you are teleported to Morio who will tell you the portal is unlocked.
        /// This script bugs out if you are anywhere besides the lab (which the multiworld context allows)
        /// Additionally, this would happen every time a gear is collected when you have 3, which could be any number of gears.
        ///
        /// This disables the flag to set this dialogue entirely.
        /// </summary>
        private void GearAnimationScript_Update(On.GearAnimationScript.orig_Update orig, GearAnimationScript self)
        {
            self.initialGearCameraZoomOnPortal = false;
            orig(self);
        }

        private bool BonusScript_CoinPickedUpGet(On.BonusScript.orig_CoinPickedUpGet orig, BonusScript self)
        {
            // TODO: Check against checked locations instead of this
            return self.coinIndex >= 0 && DebugLocationHelper.KnownIDs.Any(o => o.Item2.ContainsKey(GetID(self)));
        }

        private string previousSubarea = string.Empty;

        private void MapArea_MarkDiscovered(On.MapArea.orig_MarkDiscovered orig, MapArea self)
        {
            if (!previousSubarea.Equals(self.areaNameKey))
            {
                previousSubarea = self.areaNameKey;
                var trimmedName = previousSubarea;
                if (previousSubarea.Contains("_NAME_"))
                {
                    trimmedName = previousSubarea.Substring(previousSubarea.IndexOf("_NAME_", StringComparison.Ordinal) + "_NAME_".Length);
                }
                var bonuses = Object.FindObjectsByType<BonusScript>(FindObjectsInactive.Include, FindObjectsSortMode.None).Count(o =>
                    o.myIdentity is BonusScript.Identity.coin or BonusScript.Identity.gear or BonusScript.Identity.bunny
                        or BonusScript.Identity.bigCoin10 or BonusScript.Identity.bigCoin25
                        or BonusScript.Identity.bigCoin100);
                var cheeses = Object.FindObjectsByType<CheeseScript>(FindObjectsInactive.Include, FindObjectsSortMode.None).Length;
                var checkpoints = Object.FindObjectsByType<CheckpointScript>(FindObjectsInactive.Include, FindObjectsSortMode.None);
                var checkIds = new List<string>();
                foreach (var checkpoint in checkpoints)
                {
                    var id = APCheckpointManager.GetCheckpointID(checkpoint);
                    if (checkIds.Contains(id))
                    {
                        Plugin.DoubleLog("ERROR: CHECKPOINT ID NOT UNIQUE. NEW HASHING MECHANISM NEEDED.");
                    }
                    checkIds.Add(id);
                }
                //var activeBonuses = Object.FindObjectsOfType<BonusScript>(false).Count(o =>
                //    o.myIdentity is BonusScript.Identity.coin or BonusScript.Identity.gear or BonusScript.Identity.bunny
                //        or BonusScript.Identity.bigCoin10 or BonusScript.Identity.bigCoin25
                //        or BonusScript.Identity.bigCoin100);
                var documentedChecks = 0;
                if (DebugLocationHelper.PerLevelIDs.ContainsKey(GameplayMaster.instance.levelId.ToString()))
                {
                    documentedChecks = DebugLocationHelper.PerLevelIDs[GameplayMaster.instance.levelId.ToString()].Sum(known => known.Count);
                }
                else
                {
                    GUIUtility.systemCopyBuffer = GameplayMaster.instance.levelId.ToString();
                }
                Plugin.DoubleLog($"{GameplayMaster.instance.levelId}: {trimmedName}. There are {bonuses} AP collectables, {checkpoints.Length} checkpoints, and {cheeses} remaining cheeses in all subareas here for a total of {bonuses + cheeses + checkpoints.Length} likely checks. Currently {documentedChecks} have been sorted into regions.");
            }
            orig(self);
        }

        private void PlayerScript_OnTriggerStay(On.PlayerScript.orig_OnTriggerStay orig, PlayerScript self, Collider other)
        {
            if (!Tick.IsGameRunning)
                return;
            if (other.gameObject.layer == 17)
            {
                var bonusScr = other.GetComponent<BonusScript>();
                if (bonusScr != null)
                {
                    if (bonusScr.myIdentity == BonusScript.Identity.morioMindPassword)
                    {
                        //MorioDreamMachineScript.justUpdatedPassword = !Data.morioMindPasswordGot[Data.gameDataIndex];
                        //Data.morioMindPasswordGot[Data.gameDataIndex] = true;
                        //Data.SaveGame();
                        if (!bonusScr.skipGenericPickupAnimation)
                            GenericPickupAnimationScript.SpawnNew("PickupVisualizer_MorioMindKey");
                        Sound.Play("SoundLevelCollectiblePickup");
                        Controls.SetVibration(self.playerIndex, 0.5f);
                        if (ModMaster.instance.ModEnableGet())
                            ModMaster.instance.OnPlayerOnMorioMindKeyPickup();
                        // TODO: Send Check
                        DebugLocationHelper.CheckLocation("MindPassword", "12_00_00000");
                        bonusScr.KillMe();
                        return;
                    }
                    if (!(bonusScr.pickupDelay > 0.0) && !GameplayMaster.instance.gameOver &&
                        !bonusScr.IsRestoring() &&
                        (DialogueScript.instance == null ||
                         bonusScr.myIdentity != BonusScript.Identity.gear))
                    {
                        var id = GetID(bonusScr);
                        if (!string.IsNullOrEmpty(id))
                        {
                            DebugLocationHelper.CheckLocation(bonusScr.myIdentity.ToString(), id);
                        }
                    }
                }
            }
            orig(self, other);
        }

        public static string GetID(BonusScript item)
        {
            string s = (int)GameplayMaster.instance.levelId + "_";
            switch (item.myIdentity)
            {
                case BonusScript.Identity.coin:
                    if (item.coinIndex < 0)
                        return null;
                    s += Identifiers.COIN_ID.ToString("D2") + "_" + item.coinIndex.ToString("D5");
                    break;
                case BonusScript.Identity.bigCoin10:
                    if (item.coinIndex < 0)
                        return null;
                    s += Identifiers.COINBAG_ID.ToString("D2") + "_" + item.coinIndex.ToString("D5");
                    break;
                case BonusScript.Identity.bigCoin25:
                    if (item.coinIndex < 0)
                        return null;
                    s += Identifiers.CHEST_ID.ToString("D2") + "_" + item.coinIndex.ToString("D5");
                    break;
                case BonusScript.Identity.bigCoin100:
                    if (item.coinIndex < 0)
                        return null;
                    s += Identifiers.SAFE_ID.ToString("D2") + "_" + item.coinIndex.ToString("D5");
                    break;
                case BonusScript.Identity.gear:
                    s += Identifiers.GEAR_ID.ToString("D2") + "_" + item.gearArrayIndex.ToString("D5");
                    break;
                case BonusScript.Identity.bunny:
                    s += Identifiers.BUNNY_ID.ToString("D2") + "_" + item.bunnyIndex.ToString("D5");
                    break;
                default:
                    return null;
            }
            return s;
        }

        private void Update_AP(On.BonusScript.orig_Update orig, BonusScript self)
        {
            switch (self.myIdentity)
            {
                case BonusScript.Identity.goldenPropeller:
                    self.gameObject.GetComponent<SphereCollider>().enabled = GoldenPropellerActive;
                    foreach (var renderer in self.gameObject.GetComponentsInChildren<Renderer>())
                    {
                        renderer.enabled = GoldenPropellerActive;
                    }
                    if (!GoldenPropellerActive)
                    {
                        return;
                    }
                    break;
                case BonusScript.Identity.invincibilitySpring:
                    self.gameObject.GetComponent<SphereCollider>().enabled = GoldenSpringActive;
                    foreach (var renderer in self.gameObject.GetComponentsInChildren<Renderer>())
                    {
                        renderer.enabled = GoldenSpringActive;
                    }
                    if (!GoldenSpringActive)
                    {
                        return;
                    }
                    break;
            }
            orig(self);
        }
    }
}
