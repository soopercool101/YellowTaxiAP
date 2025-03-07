using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace YellowTaxiAP.Managers
{
    public class APCheckpointManager
    {
        public APCheckpointManager()
        {
            On.CheckpointScript.OnTriggerEnter += CheckpointScript_OnTriggerEnter;
        }

        private void CheckpointScript_OnTriggerEnter(On.CheckpointScript.orig_OnTriggerEnter orig, CheckpointScript self, UnityEngine.Collider other)
        {
            if (self.triggerOncePerFrame || other.gameObject != PlayerScript.instance.gameObject)
                return;
            if (CheckpointScript.enabledInstance != self)
            {
                var id = GetCheckpointID(self);
                DebugLocationHelper.CheckLocation("checkpoint", id);
            }
            orig(self, other);
        }

        public static string GetCheckpointID(CheckpointScript checkpoint)
        {
            // *Extremely* rudimentary hashing. Should hopefully be good enough for per-level unique ids
            var hashedPos = Mathf.RoundToInt(Mathf.Abs(checkpoint.transform.position.x) +
                                             Mathf.Abs(checkpoint.transform.position.y) +
                                             Mathf.Abs(checkpoint.transform.position.z)) % 100000;
            return $"{(int)GameplayMaster.instance.levelId}_{Identifiers.CHECKPOINT_ID:D2}_{hashedPos:D5}";
        }
    }
}
