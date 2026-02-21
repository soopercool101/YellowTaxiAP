using System;
using System.Collections.Generic;
using System.Linq;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Models;
using UnityEngine;
using YellowTaxiAP.Archipelago;
using YellowTaxiAP.Behaviours;
using YellowTaxiAP.Helpers;
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
            On.GearAnimationScript.OnDestroy += GearAnimationScript_OnDestroy;

            On.BunnyTv.Start += (_, _) =>
            {
                // Do nothing instead of deleting bunny tvs prior to final boss defeated
            };
        }

        private void GearAnimationScript_OnDestroy(On.GearAnimationScript.orig_OnDestroy orig, GearAnimationScript self)
        {
            if (GearAnimationScript.instance == self)
                GearAnimationScript.instance = null;
            HudMasterScript.instance.UpdateGearsText();
        }

        private void BonusScript_GearAlreadyPickedUpRefresh(On.BonusScript.orig_GearAlreadyPickedUpRefresh orig, BonusScript self)
        {
            if (GameplayMaster.instance.timeAttackLevel)
            {
                self.gearHasPickedupUpTexture = false;
                self.GearDaltonicTextureRefresh();
                return;
            }
#if DEBUG
            if (DebugLocationHelper.Enabled)
            {
                if (DebugLocationHelper.KnownIDs.Any(o => o.Item2.ContainsKey(GetIDString(self))))
                {
                    self.GetComponentInChildren<MeshRenderer>().sharedMaterial = self.gearUsedMaterial;
                    self.gearHasPickedupUpTexture = true;
                    self.gearOutlineTr.gameObject.SetActive(false);
                    return;
                }
                else
                {
                    self.gearHasPickedupUpTexture = false;
                    self.GearDaltonicTextureRefresh();
                    return;
                }
            }
#endif
            var id = GetID(self);
            if (id != null && (Plugin.ArchipelagoClient.AllClearedLocations.Contains(id.Value) || !Plugin.ArchipelagoClient.AllLocations.Contains(id.Value)))
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
#if DEBUG
            if (DebugLocationHelper.Enabled)
            {
                self.myMeshRend.sharedMaterial =
                    DebugLocationHelper.KnownIDs.Any(o => o.Item2.ContainsKey(GetIDString(self)))
                        ? self.bunnyPickedUpMaterial
                        : self.bunnyDefaultMaterial;
                return;
            }
#endif
            if (Plugin.SlotData.Bunnysanity)
            {
                var id = GetID(self).GetValueOrDefault();
                Plugin.Log($"Bunny {id} setting material Picked up: \"{self.bunnyPickedUpMaterial}\" | Not Picked up: \"{self.bunnyDefaultMaterial}\"");
                self.myMeshRend.sharedMaterial = Plugin.ArchipelagoClient.AllClearedLocations.Contains(id) || !Plugin.ArchipelagoClient.AllLocations.Contains(id) ? self.bunnyPickedUpMaterial : self.bunnyDefaultMaterial;
            }
            else
            {
                self.myMeshRend.sharedMaterial = APSaveController.BunnySave.HasBunny(GameplayMaster.instance.levelId, self.bunnyIndex) ? self.bunnyPickedUpMaterial : self.bunnyDefaultMaterial;
            }
        }

        private void BonusScript_Awake(On.BonusScript.orig_Awake orig, BonusScript self)
        {
            // A small number of coins are accidentally set as children of other coins, causing them to remove themselves when the parent is collected.
            if (self.transform.parent && self.transform.parent.gameObject.GetComponent<BonusScript>())
            {
                Plugin.Log($"Warning! {self.myIdentity} ({GetIDString(self)}) has a parent object ({self.transform.parent.gameObject.name})! Removing parent");
                self.transform.parent = null;
            }

            var bunnyNeedsRemoval = false;
            if (GameplayMaster.instance.levelId == Data.LevelId.Hub && self.myIdentity == BonusScript.Identity.bunny &&
                ((self.bunnyIndex == 1 && Plugin.SlotData.ExcludeSpikeBunny) || (self.bunnyIndex == 2 && Plugin.SlotData.ExcludeTopBunny)))
            {
                bunnyNeedsRemoval = true;
            }

            if (self.smallDdemoPositionOffset != new Vector3(0,0,0))
            {
#if DEBUG
                var id = GetIDString(self);
                var itemArea = DebugLocationHelper.GetKnownItemNameArea(id);
                var item = itemArea?.Item1 ?? "Unknown Item";
                var area = itemArea?.Item2 ?? "Unknown Area";
                Plugin.Log($"In a demo, {item} ({id}) in {area} will be moved from {self.transform.position} to {self.transform.position + self.smallDdemoPositionOffset}");
#endif
                if (Plugin.SlotData.ExtraDemoCollectables)
                {
                    BonusScript demoBonus;
                    if (bunnyNeedsRemoval)
                    {
                        demoBonus = self;
                        self.transform.position += self.smallDdemoPositionOffset;
                        self.smallDdemoPositionOffset = new Vector3(0, 0, 0);
                        bunnyNeedsRemoval = false;
                    }
                    else
                    {
                        var demoOffset = self.smallDdemoPositionOffset;
                        self.smallDdemoPositionOffset = new Vector3(0, 0, 0);
                        var duplicated = Object.Instantiate(self.gameObject, self.transform.position + demoOffset, self.transform.rotation, self.transform.parent);
                        demoBonus = duplicated.GetComponent<BonusScript>();
                        self.smallDemoZoneMaster = -1;
                    }
                    switch (demoBonus.myIdentity)
                    {
                        case BonusScript.Identity.gear:
                            demoBonus.gearArrayIndex += 10000;
                            demoBonus.GearAlreadyPickedUpRefresh();
                            break;
                        case BonusScript.Identity.bunny:
                            demoBonus.bunnyIndex = demoBonus.bunnyIndex switch
                            {
                                1 => 4,
                                2 => 3,
                                _ => demoBonus.bunnyIndex + 10000
                            };
                            break;
                        default:
                            demoBonus.coinIndex += 10000;
                            break;
                    }
                }
            }

            if (bunnyNeedsRemoval)
            {
                ObjectHelper.DestroyImmediateRecursive(self.transform);
                return;
            }
            orig(self);
            if (self.smallDemoZoneMaster >= 0 && self.smallDdemoPositionOffset == new Vector3(0, 0, 0))
            {
                var id = GetIDString(self);
                Plugin.Log($"Changing zone master for {self.name}");
                var withZoneMasterIndex = self.GetComponent<HideWithZoneMasterIndex>();
                withZoneMasterIndex.hideWhenZoneMasterId =
                [
                    self.smallDemoZoneMaster
                ];
            }

            if (self.myIdentity == BonusScript.Identity.invincibilitySpring)
            {
                self.gameObject.AddComponent<GoldenSpringUpdater>();
            }
            else if (self.myIdentity == BonusScript.Identity.goldenPropeller)
            {
                self.gameObject.AddComponent<GoldenPropellerUpdater>();
            }
        }

        /// <summary>
        /// Disable zoom to Morio on 3 gears, send location when showing the text
        /// </summary>
        private void GearAnimationScript_Update(On.GearAnimationScript.orig_Update orig, GearAnimationScript self)
        {
            if (self.done)
                return;
            self.initialGearCameraZoomOnPortal = false;
            if (self.timer <= Tick.Time * self.timeSpeed && self.newLevelSplashText1 != null)
            {
                var pickup = self.GetComponent<PickupInfo>();
                if (pickup)
                {
                    if (pickup.Scout.Flags.HasFlag(ItemFlags.Advancement))
                    {
                        self.newLevelSplashSound = "SoundNewLevelUnlockSplash";
                    }
                    else if (pickup.Scout.Flags.HasFlag(ItemFlags.NeverExclude))
                    {
                        self.newLevelSplashSound = "SoundSmallMissionClear";
                    }
                    else if (pickup.Scout.Flags.HasFlag(ItemFlags.Trap))
                    {
                        self.newLevelSplashSound = "SoundMenuError";
                    }
                    else if (pickup.Scout.Flags.HasFlag(ItemFlags.None))
                    {
                        self.newLevelSplashSound = "SoundLevelCollectiblePickup";
                    }

                    GiudgementScript.SpawnCustom(self.newLevelSplashText1, self.newLevelSplashText2, self.newLevelSplashSound, true, 1f);
                    self.newLevelSplashText1 = null;
                    Plugin.ArchipelagoClient.SendLocation(pickup.ID);
                }
            }
            orig(self);
        }
        
        private bool BonusScript_CoinPickedUpGet(On.BonusScript.orig_CoinPickedUpGet orig, BonusScript self)
        {
#if DEBUG
            if (DebugLocationHelper.Enabled)
            {
                return self.coinIndex >= 0 && DebugLocationHelper.KnownIDs.Any(o => o.Item2.ContainsKey(GetIDString(self)));
            }
#endif
            var id = GetID(self);
            switch (self.myIdentity)
            {
                case BonusScript.Identity.coin:
                case BonusScript.Identity.bigCoin10:
                case BonusScript.Identity.bigCoin25:
                case BonusScript.Identity.bigCoin100:
                    return !id.HasValue || (Plugin.ArchipelagoClient.AllClearedLocations.Contains(id.Value) || !Plugin.ArchipelagoClient.AllLocations.Contains(id.Value));
                default:
                    return false;
            }
        }

#if DEBUG
        private string _previousSubarea = string.Empty;
        private void MapArea_MarkDiscovered(On.MapArea.orig_MarkDiscovered orig, MapArea self)
        {
            if (DebugLocationHelper.Enabled && !_previousSubarea.Equals(self.areaNameKey))
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
                var unknownBonuses = bonuses.Where(o => !DebugLocationHelper.CheckLocation(string.Empty, GetIDString(o))).ToList();
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
                    var id = APCheckpointManager.GetCheckpointStringID(checkpoint);
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
                    if (DebugLocationHelper.Enabled)
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
                            var regionItems = new List<KeyValuePair<string, string>>();
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
                            json += GameplayMaster.instance.levelId == Data.LevelId.Hub
                                ? "\n    \"level\": \"Hub\","
                                : $"\n    \"level\": \"{Data.levelDataList[(int) GameplayMaster.instance.levelId].GetName()}\",";
                            json += $"\n    \"sublevel\": \"{sublevelname}\",";
                            json += "\n    \"gears\": {";
                            if (regionGears.Any())
                            {
                                json = regionGears.Aggregate(json, (current, v) => current + $"\n      \"{v.Value}\": {ulong.Parse(v.Key.Replace("_", ""))},");
                                json = json.TrimEnd(',') + "\n    ";
                                regionItems.AddRange(regionGears);
                            }
                            json += "},";
                            json += "\n    \"bunnies\": {";
                            if (regionBunnies.Any())
                            {
                                json = regionBunnies.Aggregate(json, (current, v) => current + $"\n      \"{v.Value}\": {ulong.Parse(v.Key.Replace("_", ""))},");
                                json = json.TrimEnd(',') + "\n    ";
                                regionItems.AddRange(regionBunnies);
                            }
                            json += "},";
                            json += "\n    \"safes\": {";
                            if (regionSafes.Any())
                            {
                                json = regionSafes.Aggregate(json, (current, v) => current + $"\n      \"{v.Value}\": {ulong.Parse(v.Key.Replace("_", ""))},");
                                json = json.TrimEnd(',') + "\n    ";
                                regionItems.AddRange(regionSafes);
                            }
                            json += "},";
                            json += "\n    \"chests\": {";
                            if (regionChests.Any())
                            {
                                json = regionChests.Aggregate(json, (current, v) => current + $"\n      \"{v.Value}\": {ulong.Parse(v.Key.Replace("_", ""))},");
                                json = json.TrimEnd(',') + "\n    ";
                                regionItems.AddRange(regionChests);
                            }
                            json += "},";
                            json += "\n    \"coinbags\": {";
                            if (regionCoinbags.Any())
                            {
                                json = regionCoinbags.Aggregate(json, (current, v) => current + $"\n      \"{v.Value}\": {ulong.Parse(v.Key.Replace("_", ""))},");
                                json = json.TrimEnd(',') + "\n    ";
                                regionItems.AddRange(regionCoinbags);
                            }
                            json += "},";
                            json += "\n    \"coins\": {";
                            if (regionCoins.Any())
                            {
                                json = regionCoins.Aggregate(json, (current, v) => current + $"\n      \"{v.Value}\": {ulong.Parse(v.Key.Replace("_", ""))},");
                                json = json.TrimEnd(',') + "\n    ";
                                regionItems.AddRange(regionCoins);
                            }
                            json += "},";
                            json += "\n    \"checkpoints\": {";
                            if (regionCheckpoints.Any())
                            {
                                json = regionCheckpoints.Aggregate(json, (current, v) => current + $"\n      \"{v.Value}\": {ulong.Parse(v.Key.Replace("_", ""))},");
                                json = json.TrimEnd(',') + "\n    ";
                                regionItems.AddRange(regionCheckpoints);
                            }
                            json += "},";
                            json += "\n    \"cheeses\": {";
                            if (regionCheeses.Any())
                            {
                                json = regionCheeses.Aggregate(json, (current, v) => current + $"\n      \"{v.Value}\": {ulong.Parse(v.Key.Replace("_", ""))},");
                                json = json.TrimEnd(',') + "\n    ";
                                regionItems.AddRange(regionCheeses);
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
                            json += "},";
                            json += "\n    \"specialrules\": {";
                            if (DebugLocationHelper.SpecialRules.ContainsKey(GameplayMaster.instance.levelId.ToString()))
                            {
                                var specialRules = DebugLocationHelper.SpecialRules[GameplayMaster.instance.levelId.ToString()].Where(ruleItem =>
                                    regionItems.Any(regionItem => regionItem.Value == ruleItem.Key)).ToArray();
                                if (specialRules.Any())
                                {
                                    json = specialRules.Aggregate(json, (current, v) => current + $"\n      \"{v.Key}\": \"{v.Value}\",");
                                    json = json.TrimEnd(',') + "\n    ";
                                }
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
                    //GUIUtility.systemCopyBuffer = GameplayMaster.instance.levelId.ToString();
                }
                Plugin.Log($"{GameplayMaster.instance.levelId}: {trimmedName}. There are {bonuses.Count} AP collectables, {checkpoints.Count} checkpoints, and {cheeses} remaining cheeses in all subareas here for a total of {bonuses.Count + cheeses + checkpoints.Count} likely checks. Currently {documentedChecks} have been sorted into regions.");


            }
            orig(self);
        }
#endif

        private void PlayerScript_OnTriggerStay(On.PlayerScript.orig_OnTriggerStay orig, PlayerScript self, Collider other)
        {
            if (!Tick.IsGameRunning)
                return;
            if (other.gameObject.layer == 17)
            {
                var pickup = other.GetComponent<BonusScript>();
                if (pickup)
                {
                    if (pickup.pickupDelay > 0.0 || GameplayMaster.instance.gameOver || pickup.IsRestoring() || (DialogueScript.instance && pickup.myIdentity == BonusScript.Identity.gear))
                        return;
                    var id = GetID(pickup);
                    var alreadyTaken = !id.HasValue || Plugin.ArchipelagoClient.AllClearedLocations.Contains(id.Value) ||
                                       !Plugin.ArchipelagoClient.AllLocations.Contains(id.Value);
#if DEBUG
                    if (DebugLocationHelper.Enabled && alreadyTaken)
                    {
                        alreadyTaken = !id.HasValue;
                    }
#endif
                    switch (pickup.myIdentity)
                    {
                        case BonusScript.Identity.gear when !GameplayMaster.instance.timeAttackLevel && id.HasValue:
                            string str1 = null;
                            var str2 = "";
                            ScoutedItemInfo info = null;
                            if (!alreadyTaken)
                            {
                                try
                                {
                                    info = Plugin.ArchipelagoClient.ScoutedLocations[id.Value];
                                    str1 = $"Found {info.ItemDisplayName}";
                                    str2 = info.Player.Name == ArchipelagoClient.ServerData.SlotName
                                        ? string.Empty
                                        : $"For {info.Player}";
                                }
                                catch (Exception ex)
                                {
                                    Plugin.BepinLogger.LogWarning("Scout Failure");
                                    Plugin.BepinLogger.LogError(ex);
                                }
                            }
                            else
                            {
                                pickup.pickupDelay = 10;
                                for (var index = 0; index < 10; ++index)
                                    BonusScript.SpawnCoinMoving(
                                        self.transform.position + new Vector3(0.0f, 0.5f, 0.0f),
                                        Utility.AngleToAxis3D(36 * index, 75f) * 24f);
                            }

                            GameplayMaster.instance.UpdateLevelCollectedGearsNumber();
                            if (!alreadyTaken && !GameplayMaster.instance.timeAttackLevel)
                                MenuEventLeaderboard.GearsCollectedAdd(1);
                            if (!Data.IsLevelIdHub(GameplayMaster.instance.levelId))
                            {
                                foreach (var portal in PortalScript.list.Where(portal =>
                                             portal.targetLevel is Levels.Index.level_hub))
                                {
                                    portal.gameObject.SetActive(true);
                                    _ = (double) portal.transform.SetYAngle(
                                        CameraGame.instance.transform.GetYAngle());
                                }
                            }

                            Sound.Play_Unpausable("SoundGearPickup");
                            if (alreadyTaken)
                            {
                                GenericPickupAnimationScript.SpawnNew("PickupVisualizer_AlreadyTakenGear",
                                    freezePlayer: false);
                            }
                            else
                            {
#if DEBUG
                                DebugLocationHelper.CheckLocation(pickup.ToString(), GetIDString(pickup));
#endif
                                Tick.Paused = true;
                                var obj = Spawn.FromPool("GearPickupAnimationObject",
                                    PlayerScript.instance.transform.position);
                                var pickupInfo = obj.AddComponent<PickupInfo>();
                                pickupInfo.ID = id.Value;
                                pickupInfo.Scout = info;
                                var component = obj.GetComponent<GearAnimationScript>();
                                component.dialogueText = null;
                                component.newLevelGoBackHubQuestion = false;
                                component.newLevelSplashText1 = str1;
                                component.newLevelSplashText2 = str2;
                                component.newLevelSplashSound = "SoundNewLevelUnlockSplash";
                                component.initialGearCameraZoomOnPortal = false;
                                component.itWasANeverTakenGear = true;
                                HudMasterScript.instance.gearShowCollectAnimation = true;
                            }

                            if (GameplayMaster.instance.timeAttackLevel)
                                HudMasterScript.instance.UpdateGearsText();
                            Controls.SetVibration(self.playerIndex, 0.5f);
                            if (ModMaster.instance.ModEnableGet())
                                ModMaster.instance.OnPlayerOnGearCollect(alreadyTaken);
                            pickup.KillMe();
                            return;
                        case BonusScript.Identity.bunny when pickup.bunnyIndex >= 0:
                            if (Plugin.SlotData.Bunnysanity)
                            {
                                var bunnyId = GetID(pickup);
                                if (!bunnyId.HasValue)
                                {
                                    Plugin.BepinLogger.LogError($"Could not get id for bunny {GameplayMaster.instance.levelId} {pickup.bunnyIndex}");
                                }
                                else
                                {
#if DEBUG
                                    DebugLocationHelper.CheckLocation(pickup.ToString(), GetIDString(pickup));
#endif
                                    Plugin.ArchipelagoClient.SendLocation(bunnyId.Value);
                                }
                            }
                            else
                            {
                                APSaveController.BunnySave.SetBunny(GameplayMaster.instance.levelId, pickup.bunnyIndex);
                            }
                            HudMasterScript.instance.UpdateBunniesTotalText();
                            MenuEventLeaderboard.BunniesCollectedAdd(1);
                            Sound.Play("SoundGoldenBunnyPickup");
                            if (!pickup.skipGenericPickupAnimation)
                            {
                                GenericPickupAnimationScript.SpawnNew("PickupVisualizer_GoldenBunny").GetComponentInChildren<MeshRenderer>().sharedMaterial = pickup.myMeshRend.sharedMaterial;
                            }
                            Controls.SetVibration(self.playerIndex, 0.5f);
                            pickup.KillMe();
                            break;
                        case BonusScript.Identity.coin:
                        case BonusScript.Identity.bigCoin10:
                        case BonusScript.Identity.bigCoin25:
                        case BonusScript.Identity.bigCoin100:
                            PickupCoinLocation(pickup, alreadyTaken);
                            return;
                        case BonusScript.Identity.morioMindPassword:
                            if (!pickup.skipGenericPickupAnimation)
                                GenericPickupAnimationScript.SpawnNew("PickupVisualizer_MorioMindKey");
                            Sound.Play("SoundLevelCollectiblePickup");
                            Controls.SetVibration(self.playerIndex, 0.5f);
                            if (ModMaster.instance.ModEnableGet())
                                ModMaster.instance.OnPlayerOnMorioMindKeyPickup();
#if DEBUG
                            DebugLocationHelper.CheckLocation("MindPassword", "12_00_00000");
#endif
                            if (Plugin.SlotData.ShuffleMoriosPassword)
                            {
                                Plugin.ArchipelagoClient.SendLocation((int)Identifiers.NotableLocations.MoriosPassword);
                            }
                            else
                            {
                                APSaveController.MiscSave.HasMoriosMindPassword = true;
                            }

                            pickup.KillMe();
                            return;
                    }
                }
            }
            orig(self, other);
        }

        public static void PickupCoinLocation(BonusScript coin, bool alreadyPickedUp)
        {
            coin.pickupDelay = 1;
            var amount = 1;
            switch (coin.myIdentity)
            {
                case BonusScript.Identity.coin:
                    Spawn.FromPool("Pt Star Yellow - Rnd Small", coin.transform.position + new Vector3(0.0f, 2f, 0.0f), Pool.instance.transform);
                    Controls.SetVibration(PlayerScript.instance.playerIndex, 0.25f);
                    break;
                case BonusScript.Identity.bigCoin10:
                    amount = 10;
                    Spawn.FromPool("Pt Star Yellow - Rnd Small", coin.transform.position + new Vector3(0.0f, 2f, 0.0f), Pool.instance.transform);
                    Controls.SetVibration(PlayerScript.instance.playerIndex, 0.35f);
                    break;
                case BonusScript.Identity.bigCoin25:
                    amount = 25;
                    Spawn.FromPool("Pt Star Rnbw - Rnd", coin.transform.position + new Vector3(0.0f, 0.5f, 0.0f), Pool.instance.transform);
                    Controls.SetVibration(PlayerScript.instance.playerIndex, 0.5f);
                    break;
                case BonusScript.Identity.bigCoin100:
                    amount = 100;
                    Spawn.FromPool("Pt Star Rnbw - Rnd", coin.transform.position + new Vector3(0.0f, 0.5f, 0.0f), Pool.instance.transform);
                    Controls.SetVibration(PlayerScript.instance.playerIndex, 0.5f);
                    break;
            }

            if (alreadyPickedUp)
            {
                // Coins that are not checks will give coins. Less coin grinding.
                switch (coin.myIdentity)
                {
                    case BonusScript.Identity.coin:
                        Spawn.FromPool("EffectCoinPickedup", PlayerScript.instance.transform.position,
                                Pool.instance.transform).GetComponentInChildren<MeshRenderer>().sharedMaterial =
                            coin.dobulePickupPreventionMaterial_Picked;
                        Sound.Play("SoundCoin");
                        break;
                    case BonusScript.Identity.bigCoin10:
                        amount += Data.CoinsLostGetBack(10);
                        Spawn.FromPool("EffectCoin10Pickedup", PlayerScript.instance.transform.position,
                            Pool.instance.transform).GetComponent<EffectScript>().CoinPickedEffectSetCoins(amount);
                        Sound.Play("SoundCoin10");
                        break;
                    case BonusScript.Identity.bigCoin25:
                        amount += Data.CoinsLostGetBack(25);
                        Spawn.FromPool("EffectCoin25Pickedup", PlayerScript.instance.transform.position,
                            Pool.instance.transform).GetComponent<EffectScript>().CoinPickedEffectSetCoins(amount);
                        Sound.Play("SoundCoin25");
                        break;
                    case BonusScript.Identity.bigCoin100:
                        amount += Data.CoinsLostGetBack(100);
                        Spawn.FromPool("EffectCoin100Pickedup", PlayerScript.instance.transform.position,
                            Pool.instance.transform).GetComponent<EffectScript>().CoinPickedEffectSetCoins(amount);
                        Sound.Play("SoundCoin25");
                        break;
                }
                Data.coinsCollected[Data.gameDataIndex] += amount;
                MenuEventLeaderboard.CoinsCollectedAdd(amount);
                if (ModMaster.instance.ModEnableGet())
                    ModMaster.instance.OnPlayerOnCoinCollect(amount);
            }
            else
            {
                var id = GetID(coin);
                if (id.HasValue)
                {
#if DEBUG
                    DebugLocationHelper.CheckLocation(coin.myIdentity.ToString(), GetIDString(coin));
#endif
                    Plugin.ArchipelagoClient.SendLocation(id.Value);
                    PlayerScript.instance.CoinsSand(coin.transform);
                }
            }

            GameplayMaster.instance.coinsCollectedTimerBonusCounter += amount;
            PlayerScript.instance.CoinsLimit25Reached();

            coin.KillMe();
        }

        public static long? GetID(BonusScript item)
        {
            var baseID = (long)GameplayMaster.instance.levelId * 1_00_00000;
            return item.myIdentity switch
            {
                BonusScript.Identity.morioMindPassword => 12_00_00000,
                BonusScript.Identity.coin when item.coinIndex >= 0 => baseID + 300000 + item.coinIndex,
                BonusScript.Identity.bigCoin10 when item.coinIndex >= 0 => baseID + 300000 + item.coinIndex,
                BonusScript.Identity.bigCoin25 when item.coinIndex >= 0 => baseID + 300000 + item.coinIndex,
                BonusScript.Identity.bigCoin100 when item.coinIndex >= 0 => baseID + 300000 + item.coinIndex,
                BonusScript.Identity.gear => baseID + 100000 + item.gearArrayIndex,
                BonusScript.Identity.bunny => baseID + 200000 + item.bunnyIndex,
                _ => null
            };
        }

        public static string GetIDString(BonusScript item)
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
            if ((self.myIdentity == BonusScript.Identity.goldenPropeller && !GoldenPropellerActive) ||
                (self.myIdentity == BonusScript.Identity.invincibilitySpring && !GoldenSpringActive))
            {
                return;
            }
            orig(self);
        }
    }
}
