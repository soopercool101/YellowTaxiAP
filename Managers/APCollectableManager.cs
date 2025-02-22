using System;
using System.Linq;
using UnityEngine;
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
            //On.BonusScript.CoinPickedUpGet += BonusScript_CoinPickedUpGet;
            //On.BonusScript.CoinPickedUpSet += BonusScript_CoinPickedUpSet;
            //On.BonusScript.OnDestroy += BonusScript_OnDestroy;

            On.MapArea.MarkDiscovered += MapArea_MarkDiscovered;
            //On.Level.StartLoadedScene += Level_StartLoadedScene;

            On.PlayerScript.OnTriggerStay += PlayerScript_OnTriggerStay;
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
                var bonuses = Object.FindObjectsOfType<BonusScript>(true).Count(o =>
                    o.myIdentity is BonusScript.Identity.coin or BonusScript.Identity.gear or BonusScript.Identity.bunny
                        or BonusScript.Identity.bigCoin10 or BonusScript.Identity.bigCoin25
                        or BonusScript.Identity.bigCoin100);
                var cheeses = Object.FindObjectsOfType<CheeseScript>(true).Length;
                //var activeBonuses = Object.FindObjectsOfType<BonusScript>(false).Count(o =>
                //    o.myIdentity is BonusScript.Identity.coin or BonusScript.Identity.gear or BonusScript.Identity.bunny
                //        or BonusScript.Identity.bigCoin10 or BonusScript.Identity.bigCoin25
                //        or BonusScript.Identity.bigCoin100);

                Plugin.DoubleLog($"{GameplayMaster.instance.levelId}: {trimmedName}. There are {bonuses} AP collectables and {cheeses} cheeses in all subareas here (not counting NPCs)");
            }
            orig(self);
        }

        private void PlayerScript_OnTriggerStay(On.PlayerScript.orig_OnTriggerStay orig, PlayerScript self, Collider other)
        {
            if (other.gameObject.layer == 17)
            {
                var bonusScr = other.GetComponent<BonusScript>();
                var id = GetID(bonusScr);
                if (!string.IsNullOrEmpty(id))
                {
                    Print_ID(bonusScr.myIdentity, id);
                }
            }

            orig(self, other);
        }

        private void BonusScript_CoinPickedUpSet(On.BonusScript.orig_CoinPickedUpSet orig, BonusScript self)
        {
            string id = GetID(self);
            if (!string.IsNullOrEmpty(id))
            {
                Print_ID(self.myIdentity, id);
            }
            orig(self);
        }

        private bool BonusScript_CoinPickedUpGet(On.BonusScript.orig_CoinPickedUpGet orig, BonusScript self)
        {
            return false;
        }

        public const int GEAR_ID = 1;
        public const int BUNNY_ID = 2;
        public const int COIN_ID = 3;
        private void BonusScript_OnDestroy(On.BonusScript.orig_OnDestroy orig, BonusScript self)
        {
            bool isCoin = self.myIdentity is BonusScript.Identity.coin or BonusScript.Identity.bigCoin10
                or BonusScript.Identity.bigCoin25 or BonusScript.Identity.bigCoin100;
            if (!isCoin)
            {
                string id = GetID(self);
                if (!string.IsNullOrEmpty(id))
                {
                    Print_ID(self.myIdentity, id);
                }
            }
            orig(self);
        }

        public string GetID(BonusScript item)
        {
            string s = (int)GameplayMaster.instance.levelId + "_";
            switch (item.myIdentity)
            {
                case BonusScript.Identity.coin:
                case BonusScript.Identity.bigCoin10:
                case BonusScript.Identity.bigCoin25:
                case BonusScript.Identity.bigCoin100:
                    if (item.coinIndex < 0)
                        return null;
                    s += COIN_ID.ToString("D2") + "_" + item.coinIndex.ToString("D5");
                    break;
                case BonusScript.Identity.gear:
                    s += GEAR_ID.ToString("D2") + "_" + item.gearArrayIndex.ToString("D5");
                    break;
                case BonusScript.Identity.bunny:
                    s += BUNNY_ID.ToString("D2") + "_" + item.bunnyIndex.ToString("D5");
                    break;
                default:
                    return null;
            }
            return s;
        }

        public void Print_ID(BonusScript.Identity type, string id)
        {
            Plugin.DoubleLog($"Picked up item of type \"{type}\". ID: {id}");
            GUIUtility.systemCopyBuffer = id;
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
