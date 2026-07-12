using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;
using YellowTaxiAP.Archipelago;
using YellowTaxiAP.Behaviours;
using Object = UnityEngine.Object;

namespace YellowTaxiAP.Managers
{
    public class APBossManager
    {
        public APBossManager()
        {
            On.BossAlienMosk1Script.BossPhaseSpawn += BossAlienMosk1Script_BossPhaseSpawn;
            On.BossAlienMosk1Script.BossWindowWalkAnimation += BossAlienMosk1Script_BossWindowWalkAnimation;
        }

        public static bool AlienMoskBeaten { get; set; }
        private void BossAlienMosk1Script_BossWindowWalkAnimation(On.BossAlienMosk1Script.orig_BossWindowWalkAnimation orig, BossAlienMosk1Script self)
        {
            orig(self);
            if (!AlienMoskBeaten)
            {
                AlienMoskBeaten = true; // Prevent doing all this every frame while he's walking out the window
                if (Plugin.SlotData.ShuffleGoldenSpring)
                    Plugin.ArchipelagoClient.SendLocation(1_00_00000 * (long)GameplayMaster.instance.levelId +
                                 (long)Identifiers.NotableLocations.HubGoldenSpring);
                else if (!(Plugin.SlotData.Goal == YTGVSlotData.GoalType.ToslaOffices && Plugin.SlotData.RemoveGoalPortalLocations))
                    APSaveController.MiscSave.HasGoldenSpring = true;

                if (Plugin.SlotData.Goal == YTGVSlotData.GoalType.ToslaOffices)
                    Plugin.ArchipelagoClient.Win();
            }
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
