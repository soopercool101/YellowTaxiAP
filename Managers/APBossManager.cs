using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace YellowTaxiAP.Managers
{
    public class APBossManager
    {
        public APBossManager()
        {
            On.BossAlienMosk1Script.BossPhaseSpawn += BossAlienMosk1Script_BossPhaseSpawn;
        }

        public BonusScript[] SpawnedInvincibilities;

        private void BossAlienMosk1Script_BossPhaseSpawn(On.BossAlienMosk1Script.orig_BossPhaseSpawn orig, BossAlienMosk1Script self)
        {
            orig(self);
            if (Plugin.SlotData.EasyAlienMosk)
            {
                if (self.spawnedInvincibility)
                {
                    Object.Destroy(self.spawnedInvincibility.gameObject);
                }
                if (self.life == 3)
                {
                    foreach (var transform in self.powerupSpawnPoints)
                    {
                        Spawn.Instance("BonusInvincibilitySpring", transform.position,
                            transform.parent).GetComponent<BonusScript>();
                    }
                }
            }
        }
    }
}
