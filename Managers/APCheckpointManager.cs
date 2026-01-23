using System;
using UnityEngine;

namespace YellowTaxiAP.Managers
{
    public class APCheckpointManager
    {
        private static readonly int Color1 = Shader.PropertyToID("_Color");

        public APCheckpointManager()
        {
            On.CheckpointScript.Awake += CheckpointScript_Awake;
            On.CheckpointScript.OnTriggerEnter += CheckpointScript_OnTriggerEnter;
        }

        private void CheckpointScript_Awake(On.CheckpointScript.orig_Awake orig, CheckpointScript self)
        {
            orig(self);
            var id = GetCheckpointID(self);
            if (!Plugin.SlotData.Checkpointsanity ||
                Plugin.ArchipelagoClient.AllClearedLocations.Contains(id) || !Plugin.ArchipelagoClient.AllLocations.Contains(id))
            {
                self.myCircleRend.material = new Material(GameplayMaster.instance.rainbowMaterials[0]);
                self.myCircleRend.material.SetColor(Color1, new Color(1f, 1f, 1f, 0.5f));
            }
        }

        private void CheckpointScript_OnTriggerEnter(On.CheckpointScript.orig_OnTriggerEnter orig, CheckpointScript self, Collider other)
        {
            if (self.triggerOncePerFrame || other.gameObject != PlayerScript.instance.gameObject)
                return;
            if (CheckpointScript.enabledInstance != self)
            {
                var id = GetCheckpointID(self);
#if DEBUG
                var strId = GetCheckpointStringID(self);
                DebugLocationHelper.CheckLocation("checkpoint", strId);
                if (long.Parse(strId.Replace("_", string.Empty)) != id)
                {
                    throw new Exception("WRONG CHECKPOINT CALCULATION");
                }
#endif
                Plugin.ArchipelagoClient.SendLocation(id);

                self.myCircleRend.material = new Material(GameplayMaster.instance.rainbowMaterials[0]);
                self.myCircleRend.material.SetColor(Color1, new Color(1f, 1f, 1f, 0.5f));
            }
            orig(self, other);
        }

        public static long GetCheckpointID(CheckpointScript checkpoint)
        {
            return (int)GameplayMaster.instance.levelId * 1_00_00000 + 9_00000 + Mathf.Abs(Mathf.RoundToInt(checkpoint.transform.position.x) +
                Mathf.RoundToInt(checkpoint.transform.position.z)) % 100000;
        }

#if DEBUG
        public static string GetCheckpointStringID(CheckpointScript checkpoint)
        {
            // *Extremely* rudimentary hashing. Should hopefully be good enough for per-level unique ids
            var hashedPos = Mathf.Abs(Mathf.RoundToInt(checkpoint.transform.position.x) +
                                             Mathf.RoundToInt(checkpoint.transform.position.z)) % 100000;
            return $"{(int)GameplayMaster.instance.levelId}_{Identifiers.CHECKPOINT_ID:D2}_{hashedPos:D5}";
        }
#endif
    }
}
