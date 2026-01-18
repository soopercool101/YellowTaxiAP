using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace YellowTaxiAP.Behaviours
{
    public class GameStateUpdater : MonoBehaviour
    {
        public static GameStateUpdater Instance;

        public static bool HatStateNeedsUpdate { get; set; }
        public static Data.Hat? EquippedHatUpdate { get; set; }
        public static bool GearStateNeedsUpdate { get; set; }
        public static bool BunnyStateNeedsUpdate { get; set; }
        public static bool OrangeSwitchStateNeedsUpdate { get; set; }

        public void Awake()
        {
            if (Instance)
                throw new NotSupportedException("Can only have one Game State updater!");

            Instance = this;
        }

        public void FixedUpdate()
        {
            if (HatStateNeedsUpdate)
            {
                if (PlayerScript.instance)
                {
                    var hat = EquippedHatUpdate ?? Data.HatGetCurrentKind();
                    HatScript.RemoveHat(false);
                    Plugin.Log(hat.ToString());
                    if (hat != Data.Hat.Noone)
                    {
                        HatScript.Instantiate(hat);
                    }
                }

                HatStateNeedsUpdate = false;
            }

            if (GearStateNeedsUpdate)
            {
                // Update portal cost text where applicable
                foreach (var t in PortalScript.list.Where(t => t))
                {
                    t.CostUpdateTry();
                    t.UpdatePortalToLevelName();
                }
                GearStateNeedsUpdate = false;
            }

            if (BunnyStateNeedsUpdate)
            {
                BunnyStateNeedsUpdate = false;
            }

            if (OrangeSwitchStateNeedsUpdate)
            {
                foreach (var block in SwitchBlockScript.list)
                {
                    block.ChangeState();
                }

                OrangeSwitchStateNeedsUpdate = false;
            }
        }
    }
}
