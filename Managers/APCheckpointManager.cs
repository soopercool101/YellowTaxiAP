using UnityEngine;

namespace YellowTaxiAP.Managers
{
    public class APCheckpointManager
    {
        public APCheckpointManager()
        {
            On.CheckpointScript.Awake += CheckpointScript_Awake;
            On.CheckpointScript.OnTriggerEnter += CheckpointScript_OnTriggerEnter;
            On.PersonParent.Awake += PersonParent_Awake;
            On.PersonParent.LineRendererAlreadyPickedUpRefresh += PersonParent_LineRendererAlreadyPickedUpRefresh;
        }

        private void CheckpointScript_Awake(On.CheckpointScript.orig_Awake orig, CheckpointScript self)
        {
            orig(self);
            if (!Plugin.SlotData.Checkpointsanity ||
                Plugin.ArchipelagoClient.AllClearedLocations.Contains(long.Parse(GetCheckpointID(self).Replace("_", string.Empty))))
            {
                self.myCircleRend.material = new Material(GameplayMaster.instance.rainbowMaterials[0]);
                self.myCircleRend.material.SetColor("_Color", new Color(1f, 1f, 1f, 0.5f));
            }
        }

        private void PersonParent_LineRendererAlreadyPickedUpRefresh(On.PersonParent.orig_LineRendererAlreadyPickedUpRefresh orig, PersonParent self)
        {
            orig(self);
            if (self.alreadyPickedUp)
            {
                Plugin.Log("alreadypickedupmat: " + self.alreadyPickedUpMatCopy.name);
            }
        }

        private void PersonParent_Awake(On.PersonParent.orig_Awake orig, PersonParent self)
        {
            orig(self);
        }

        private void CheckpointScript_OnTriggerEnter(On.CheckpointScript.orig_OnTriggerEnter orig, CheckpointScript self, Collider other)
        {
            if (self.triggerOncePerFrame || other.gameObject != PlayerScript.instance.gameObject)
                return;
            if (CheckpointScript.enabledInstance != self)
            {
                var id = GetCheckpointID(self);
#if DEBUG
                DebugLocationHelper.CheckLocation("checkpoint", id);
#endif
                Plugin.ArchipelagoClient.SendLocation(long.Parse(id.Replace("_", string.Empty)));

                self.myCircleRend.material = new Material(GameplayMaster.instance.rainbowMaterials[0]);
                self.myCircleRend.material.SetColor("_Color", new Color(1f, 1f, 1f, 0.5f));
            }
            orig(self, other);
        }

        public static string GetCheckpointID(CheckpointScript checkpoint)
        {
            // *Extremely* rudimentary hashing. Should hopefully be good enough for per-level unique ids
            var hashedPos = Mathf.Abs(Mathf.RoundToInt(checkpoint.transform.position.x) +
                                             Mathf.RoundToInt(checkpoint.transform.position.z)) % 100000;
            return $"{(int)GameplayMaster.instance.levelId}_{Identifiers.CHECKPOINT_ID:D2}_{hashedPos:D5}";
        }
    }
}
