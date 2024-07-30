using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace YellowTaxiAP.Managers
{
    public class CollectableManager
    {
        public static bool GoldenPropellerActive = false;
        public static bool GoldenSpringActive = false;

        public CollectableManager()
        {
            // BonusScript hooks
            On.BonusScript.Update += Update_AP;
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
