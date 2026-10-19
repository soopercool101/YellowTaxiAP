using YellowTaxiAP.Archipelago;
using YellowTaxiAP.Behaviours;
using Object = UnityEngine.Object;

namespace YellowTaxiAP.Managers
{
    public class APEnemyManager
    {
        public APEnemyManager()
        {
            On.BossAlienMosk1Script.BossPhaseSpawn += BossAlienMosk1Script_BossPhaseSpawn;
            On.BossAlienMosk1Script.BossWindowWalkAnimation += BossAlienMosk1Script_BossWindowWalkAnimation;
            On.GigaMorioScript.Update += GigaMorioScript_Update;
        }

        public static bool AllowLaser
        {
            get
            {
                if (Plugin.CheatsEnabled)
                {
                    return Plugin.AllowLaser;
                }

                switch (Plugin.SlotData.MindLaserBehavior)
                {
                    case YTGVSlotData.MoriosMindLaserBehavior.Enabled:
                        return true;
                    case YTGVSlotData.MoriosMindLaserBehavior.Disabled:
                        return false;
                    case YTGVSlotData.MoriosMindLaserBehavior.MoriosBrainHat:
                        return (Data.Hat)Data.currentHat[Data.gameDataIndex] != Data.Hat.Hat28_Brain;
                    case YTGVSlotData.MoriosMindLaserBehavior.InceptionTopHat:
                        return (Data.Hat)Data.currentHat[Data.gameDataIndex] != Data.Hat.Hat44_Trottola;
                    case YTGVSlotData.MoriosMindLaserBehavior.MoriosBrainOrInceptionTopHat:
                        return (Data.Hat)Data.currentHat[Data.gameDataIndex] != Data.Hat.Hat28_Brain &&
                               (Data.Hat)Data.currentHat[Data.gameDataIndex] != Data.Hat.Hat44_Trottola;
                    default:
                        return true;
                }
            }
        }

        private void GigaMorioScript_Update(On.GigaMorioScript.orig_Update orig, GigaMorioScript self)
        {
            if (!AllowLaser)
            {
                for (var index = self.myLasers.Count - 1; index >= 0; --index)
                {
                    Pool.Destroy(self.myLasers[index].gameObject);
                    self.myLasers.RemoveAt(index);
                }
                self.laserCD = self.laserCooldown;
                self.myAnimator.ResetTrigger("ChargeLaser");
                self.myAnimator.SetTrigger("LaserFireEnd");
            }
            orig(self);
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
