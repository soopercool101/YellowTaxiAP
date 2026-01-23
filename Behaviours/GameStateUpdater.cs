using System;
using System.Linq;
using UnityEngine;
using YellowTaxiAP.Managers;

namespace YellowTaxiAP.Behaviours
{
    public class GameStateUpdater : MonoBehaviour
    {
        public static GameStateUpdater Instance { get; private set; }

        public static bool GearStateNeedsUpdate { get; set; }
        public static bool BunnyStateNeedsUpdate { get; set; }
        public static bool RatStateNeedsUpdate { get; set; }

        public void Awake()
        {
            if (Instance)
                throw new NotSupportedException("Can only have one Game State updater!");

            Instance = this;
        }

        public void FixedUpdate()
        {
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

            if (RatStateNeedsUpdate)
            {
                if (PlayerScript.instance)
                {
                    if (APRatManager.ReceivedRatItem)
                    {
                        RatPlayerScript.SpawnRat();
                        RatPlayerScript.EnsureNextPickToBeGood();
                        CheeseScript.EnableAllCheese();
                        Sound.Play("SoundRatJoined");
                    }
                    else
                    {
                        Destroy(RatPlayerScript.instance?.gameObject);
                    }
                }
                RatStateNeedsUpdate = false;
            }
        }
    }
}
