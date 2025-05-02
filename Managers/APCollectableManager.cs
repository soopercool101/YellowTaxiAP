﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
            On.BonusScript.BunnyAlreadyPickedUpRefresh += BonusScript_BunnyAlreadyPickedUpRefresh;
            On.BonusScript.GearAlreadyPickedUpRefresh += BonusScript_GearAlreadyPickedUpRefresh;
            On.BonusScript.Awake += BonusScript_Awake;

            //On.BonusScript.CoinPickedUpSet += BonusScript_CoinPickedUpSet;
            //On.BonusScript.OnDestroy += BonusScript_OnDestroy;
#if DEBUG
            On.MapArea.MarkDiscovered += MapArea_MarkDiscovered;
#endif
            //On.Level.StartLoadedScene += Level_StartLoadedScene;

            On.PlayerScript.OnTriggerStay += PlayerScript_OnTriggerStay;

            On.GearAnimationScript.Update += GearAnimationScript_Update;

            On.BunnyTv.Start += (_, _) =>
            {
                // Do nothing instead of deleting bunny tvs prior to final boss defeated
            };
        }

        private void BonusScript_GearAlreadyPickedUpRefresh(On.BonusScript.orig_GearAlreadyPickedUpRefresh orig, BonusScript self)
        {
            if (DebugLocationHelper.KnownIDs.Any(o => o.Item2.ContainsKey(GetID(self))))
            {
                self.GetComponentInChildren<MeshRenderer>().sharedMaterial = self.gearUsedMaterial;
                self.gearHasPickedupUpTexture = true;
                self.gearOutlineTr.gameObject.SetActive(false);
            }
            else
            {
                self.gearHasPickedupUpTexture = false;
                self.GearDaltonicTextureRefresh();
            }
        }

        private void BonusScript_BunnyAlreadyPickedUpRefresh(On.BonusScript.orig_BunnyAlreadyPickedUpRefresh orig, BonusScript self)
        {
            self.myMeshRend.sharedMaterial = DebugLocationHelper.KnownIDs.Any(o => o.Item2.ContainsKey(GetID(self))) ? self.bunnyPickedUpMaterial : self.bunnyDefaultMaterial;
        }

        /// <summary>
        /// A small number of coins are accidentally set as children of other coins, causing them to remove themselves when the parent is collected.
        /// Remove the parent in this instance.
        /// </summary>
        private void BonusScript_Awake(On.BonusScript.orig_Awake orig, BonusScript self)
        {
            if (self.transform.parent != null && self.transform.parent.gameObject.GetComponent<BonusScript>() != null)
            {
                Plugin.Log($"Warning! {self.myIdentity} ({GetID(self)}) has a parent object ({self.transform.parent.gameObject.name})! Removing parent");
                self.transform.parent = null;
            }
            orig(self);
        }

        /// <summary>
        /// Complete reimplementation to prevent unnecessary destruction of needed objects
        /// </summary>
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

        private string _previousSubarea = string.Empty;

        private void MapArea_MarkDiscovered(On.MapArea.orig_MarkDiscovered orig, MapArea self)
        {
            if (!_previousSubarea.Equals(self.areaNameKey))
            {
                _previousSubarea = self.areaNameKey;
                var trimmedName = _previousSubarea;
                if (_previousSubarea.Contains("_NAME_"))
                {
                    trimmedName = _previousSubarea.Substring(_previousSubarea.IndexOf("_NAME_", StringComparison.Ordinal) + "_NAME_".Length);
                }
                var bonuses = Object.FindObjectsByType<BonusScript>(FindObjectsInactive.Include, FindObjectsSortMode.None).Where(o =>
                    (o.myIdentity is BonusScript.Identity.gear or BonusScript.Identity.bunny)
                        || ((o.myIdentity is BonusScript.Identity.coin or BonusScript.Identity.bigCoin10 or BonusScript.Identity.bigCoin25
                        or BonusScript.Identity.bigCoin100) && o.coinIndex >= 0)).ToList();
                var unknownBonuses = bonuses.Where(o => !DebugLocationHelper.CheckLocation(string.Empty, GetID(o))).ToList();
                if (unknownBonuses.Count < 5)
                {
                    foreach (var bonus in unknownBonuses)
                    {
                        Plugin.Log($"Unknown {bonus.myIdentity} found at {bonus.transform.position}");
                    }
                }
                var cheeses = Object.FindObjectsByType<CheeseScript>(FindObjectsInactive.Include, FindObjectsSortMode.None).Length;
                var checkpoints = CheckpointScript.list;
                var checkIds = new List<string>();
                foreach (var checkpoint in checkpoints)
                {
                    var id = APCheckpointManager.GetCheckpointID(checkpoint);
                    if (checkIds.Contains(id))
                    {
                        Plugin.Log("ERROR: CHECKPOINT ID NOT UNIQUE. NEW HASHING MECHANISM NEEDED.");
                    }
                    //var check = DebugLocationHelper.KnownIDs.FirstOrDefault(area => area.Item2.ContainsKey(id));
                    //Plugin.Log(check == null
                    //    ? $"Found unknown checkpoint at {self.transform.position}"
                    //    : $"Found known checkpoint \"{check.Item2[id]}\"");
                    checkIds.Add(id);
                }

                //var activeBonuses = Object.FindObjectsOfType<BonusScript>(false).Count(o =>
                //    o.myIdentity is BonusScript.Identity.coin or BonusScript.Identity.gear or BonusScript.Identity.bunny
                //        or BonusScript.Identity.bigCoin10 or BonusScript.Identity.bigCoin25
                //        or BonusScript.Identity.bigCoin100);
                var documentedChecks = 0;
                if (DebugLocationHelper.PerLevelIDs.ContainsKey(GameplayMaster.instance.levelId.ToString()))
                {
                    documentedChecks = DebugLocationHelper.PerLevelIDs[GameplayMaster.instance.levelId.ToString()].Sum(known => known.Count(o => !string.IsNullOrEmpty(o.Key)));
                    if (documentedChecks >= bonuses.Count + cheeses + checkpoints.Count)
                    {
                        var json = "{";
                        //var i = 0;
                        foreach (var subregion in DebugLocationHelper.PerLevelIDs[
                                     GameplayMaster.instance.levelId.ToString()])
                        {
                            var regionName = DebugLocationHelper.KnownIDs.First(o => o.Item2.Equals(subregion)).Item1;
                            if (regionName.Contains("Special Rules"))
                            {
                                continue;
                            }

                            var sublevelname = regionName;
                            if (sublevelname.Contains("-"))
                            {
                                sublevelname = sublevelname.Substring(0, sublevelname.IndexOf("-", StringComparison.Ordinal)).TrimEnd();
                            }
                            var modifiedRegionName = DebugLocationHelper.GetRegionJsonName(regionName);
                            var regionCoins = new List<KeyValuePair<string, string>>();
                            var regionCoinbags = new List<KeyValuePair<string, string>>();
                            var regionChests = new List<KeyValuePair<string, string>>();
                            var regionSafes = new List<KeyValuePair<string, string>>();
                            var regionCheeses = new List<KeyValuePair<string, string>>();
                            var regionGears = new List<KeyValuePair<string, string>>();
                            var regionBunnies = new List<KeyValuePair<string, string>>();
                            var regionCheckpoints = new List<KeyValuePair<string, string>>();
                            var regionConnections = new List<DebugLocationHelper.RegionConnection>();
                            var regionWarps = new List<DebugLocationHelper.RegionConnection>();
                            var regionSubwarps = new List<DebugLocationHelper.RegionConnection>();
                            foreach (var c in subregion)
                            {
                                if(string.IsNullOrEmpty(c.Key))
                                    continue; // Skip placeholders 
                                if (c.Value.Contains("Bunny - "))
                                {
                                    regionBunnies.Add(c);
                                }
                                else if (c.Value.Contains("Gear - "))
                                {
                                    regionGears.Add(c);
                                }
                                else if (c.Value.Contains("Cheese "))
                                {
                                    regionCheeses.Add(c);
                                }
                                else if (c.Value.Contains("Chest "))
                                {
                                    regionChests.Add(c);
                                }
                                else if (c.Value.Contains("Safe "))
                                {
                                    regionSafes.Add(c);
                                }
                                else if (c.Value.Contains("Coin Bag "))
                                {
                                    regionCoinbags.Add(c);
                                }
                                else if (c.Value.Contains("Coin "))
                                {
                                    regionCoins.Add(c);
                                }
                                else if (c.Value.Contains("Checkpoint"))
                                {
                                    regionCheckpoints.Add(c);
                                }
                                else if (c.Value.EndsWith("Gear"))
                                {
                                    regionGears.Add(c);
                                }
                                else if (c.Value.EndsWith("Bunny"))
                                {
                                    regionBunnies.Add(c);
                                }
                                else
                                {
                                    Plugin.Log($"WARNING: COULD NOT SORT \"{c.Value}\"");
                                }
                            }

                            if (DebugLocationHelper.RegionConnections.ContainsKey(regionName))
                            {
                                foreach (var connection in DebugLocationHelper.RegionConnections[regionName])
                                {
                                    switch (connection.ConnectingType)
                                    {
                                        case DebugLocationHelper.ConnectionType.Connection:
                                            regionConnections.Add(connection);
                                            break;
                                        case DebugLocationHelper.ConnectionType.Subwarp:
                                            regionSubwarps.Add(connection);
                                            break;
                                        case DebugLocationHelper.ConnectionType.Warp:
                                            regionWarps.Add(connection);
                                            break;
                                        default:
                                            throw new ArgumentOutOfRangeException();
                                    }
                                }
                            }
                            else
                            {
                                Plugin.Log($"WARNING: NO CONNECTION DATA FOUND FOR \"{regionName}\"");
                            }

                            json += $"\n  \"{modifiedRegionName}\": {{";
                            json += $"\n    \"name\": \"{regionName}\",";
                            json += $"\n    \"level\": \"{GameplayMaster.instance.levelId.ToString()}\",";
                            json += $"\n    \"sublevel\": \"{sublevelname}\",";
                            json += "\n    \"gears\": {";
                            if (regionGears.Any())
                            {
                                json = regionGears.Aggregate(json, (current, v) => current + $"\n      \"{v.Value}\": {ulong.Parse(v.Key.Replace("_", ""))},");
                                json = json.TrimEnd(',') + "\n    ";
                            }
                            json += "},";
                            json += "\n    \"bunnies\": {";
                            if (regionBunnies.Any())
                            {
                                json = regionBunnies.Aggregate(json, (current, v) => current + $"\n      \"{v.Value}\": {ulong.Parse(v.Key.Replace("_", ""))},");
                                json = json.TrimEnd(',') + "\n    ";
                            }
                            json += "},";
                            json += "\n    \"safes\": {";
                            if (regionSafes.Any())
                            {
                                json = regionSafes.Aggregate(json, (current, v) => current + $"\n      \"{v.Value}\": {ulong.Parse(v.Key.Replace("_", ""))},");
                                json = json.TrimEnd(',') + "\n    ";
                            }
                            json += "},";
                            json += "\n    \"chests\": {";
                            if (regionChests.Any())
                            {
                                json = regionChests.Aggregate(json, (current, v) => current + $"\n      \"{v.Value}\": {ulong.Parse(v.Key.Replace("_", ""))},");
                                json = json.TrimEnd(',') + "\n    ";
                            }
                            json += "},";
                            json += "\n    \"coinbags\": {";
                            if (regionCoinbags.Any())
                            {
                                json = regionCoinbags.Aggregate(json, (current, v) => current + $"\n      \"{v.Value}\": {ulong.Parse(v.Key.Replace("_", ""))},");
                                json = json.TrimEnd(',') + "\n    ";
                            }
                            json += "},";
                            json += "\n    \"coins\": {";
                            if (regionCoins.Any())
                            {
                                json = regionCoins.Aggregate(json, (current, v) => current + $"\n      \"{v.Value}\": {ulong.Parse(v.Key.Replace("_", ""))},");
                                json = json.TrimEnd(',') + "\n    ";
                            }
                            json += "},";
                            json += "\n    \"checkpoints\": {";
                            if (regionCheckpoints.Any())
                            {
                                json = regionCheckpoints.Aggregate(json, (current, v) => current + $"\n      \"{v.Value}\": {ulong.Parse(v.Key.Replace("_", ""))},");
                                json = json.TrimEnd(',') + "\n    ";
                            }
                            json += "},";
                            json += "\n    \"cheeses\": {";
                            if (regionCheeses.Any())
                            {
                                json = regionCheeses.Aggregate(json, (current, v) => current + $"\n      \"{v.Value}\": {ulong.Parse(v.Key.Replace("_", ""))},");
                                json = json.TrimEnd(',') + "\n    ";
                            }
                            json += "},";
                            json += "\n    \"connections\": {";
                            if (regionConnections.Any())
                            {
                                json = regionConnections.Aggregate(json, (current, v) => current + $"\n      \"{v.DestinationRegion}\": \"{v.Rules}\",");
                                json = json.TrimEnd(',') + "\n    ";
                            }
                            json += "},";
                            json += "\n    \"subwarps\": {";
                            if (regionSubwarps.Any())
                            {
                                json = regionSubwarps.Aggregate(json, (current, v) => current + $"\n      \"{v.Name}\": [\"{v.DestinationRegion}\", \"{v.Rules}\"],");
                                json = json.TrimEnd(',') + "\n    ";
                            }
                            json += "},";
                            json += "\n    \"warps\": {";
                            if (regionWarps.Any())
                            {
                                json = regionWarps.Aggregate(json, (current, v) => current + $"\n      \"{v.Name}\": [\"{v.DestinationRegion}\", \"{v.Rules}\"],");
                                json = json.TrimEnd(',') + "\n    ";
                            }
                            json += "}";
                            json += "\n  },";
                        }
                        json = json.TrimEnd(',');

                        json += "\n}";
                        GUIUtility.systemCopyBuffer = json;
                        Plugin.Log("JSON Generation successful");
                    }
                }
                else
                {
                    GUIUtility.systemCopyBuffer = GameplayMaster.instance.levelId.ToString();
                }
                Plugin.Log($"{GameplayMaster.instance.levelId}: {trimmedName}. There are {bonuses.Count} AP collectables, {checkpoints.Count} checkpoints, and {cheeses} remaining cheeses in all subareas here for a total of {bonuses.Count + cheeses + checkpoints.Count} likely checks. Currently {documentedChecks} have been sorted into regions.");

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
