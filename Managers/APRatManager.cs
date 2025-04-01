using UnityEngine;

namespace YellowTaxiAP.Managers
{
    public class APRatManager
    {
        public static bool AP_RatRando => true;
        public static bool AP_ReceivedRat = false;
        public static bool AP_SentRat = false;

        public APRatManager()
        {
            // Rat Person scripts
            On.RatPersonScript.IsRatPickedUp += RatPersonScript_IsRatPickedUp;
            On.RatPersonScript.RatPickUp += RatPersonScript_RatPickUp;
            On.RatPersonScript.Awake += RatPersonScript_Awake;
            // Rat Person Dialogue scripts
            On.DialogueScript.SpecialMethod_OnAnswerYes_PickupRat += DialogueScript_SpecialMethod_OnAnswerYes_PickupRat;
            // Rat Player scripts
            On.RatPlayerScript.StateBehaviour_FollowPlayer += RatPlayerScript_StateBehaviour_FollowPlayer;
            // Cheese scripts
            On.CheeseScript.GetCheeseIdString += CheeseScript_GetCheeseIdString;
            On.CheeseScript.IsAlreadyPickedUp += CheeseScript_IsAlreadyPickedUp;
            On.CheeseScript.MarkPickedUp += CheeseScript_MarkPickedUp;
        }

        /// <summary>
        /// In vanilla, the rat very often can get stuck, making Cheese pickups slightly annoying.
        /// This code makes the rat always teleport to you when you are near a cheese for easy pickups.
        /// </summary>
        private void RatPlayerScript_StateBehaviour_FollowPlayer(On.RatPlayerScript.orig_StateBehaviour_FollowPlayer orig, RatPlayerScript self)
        {
            orig(self);
            if (self.teleportDelay <= 0.0 && CheeseScript.IsPlayerNearAnyCheese())
            {
                self.transform.position = RatPlayerScript.RespawnNearPlayerPositionGet();
                self.justTeleportedTimer = 1f;
                if (!Sound.IsPlaying("SoundRatTeleport") && self.teleportSoundDelay <= 0.0)
                {
                    Sound.Play("SoundRatTeleport");
                    self.teleportSoundDelay = 30f;
                }
            }
        }

        private bool CheeseScript_IsAlreadyPickedUp(On.CheeseScript.orig_IsAlreadyPickedUp orig, CheeseScript self)
        {
            //Plugin.Log($"Cheese {self.GetCheeseIdString()} can be found at {self.transform.position.ToString()}");
            // TODO: Base this off the location being checked instead of the vanilla check
            return orig(self);
        }

        private void DialogueScript_SpecialMethod_OnAnswerYes_PickupRat(On.DialogueScript.orig_SpecialMethod_OnAnswerYes_PickupRat orig, DialogueScript self)
        {
            if (!AP_RatRando)
            {
                RatPersonScript.RatPickUp();
            }
            else
            {
                DebugLocationHelper.CheckLocation("Michele", "2_21_99999");
            }
            Spawn.Instance("Dialogue Rat Pickup Answer Yes", Vector3.zero);
        }

        private void CheeseScript_MarkPickedUp(On.CheeseScript.orig_MarkPickedUp orig, CheeseScript self)
        {
            DebugLocationHelper.CheckLocation("cheese", self.GetCheeseIdString());
            orig(self);
        }

        /// <summary>
        /// Honestly this was shockingly close to how I formatted other item IDs anyway. Matching exactly for consistency
        /// </summary>
        private string CheeseScript_GetCheeseIdString(On.CheeseScript.orig_GetCheeseIdString orig, CheeseScript self)
        {
            return self.cheeseIdStr ?? (self.cheeseIdStr = (int)GameplayMaster.instance.levelId + "_" +
                                                           Identifiers.CHEESE_ID.ToString("D2") + "_" +
                                                           self.cheeseId.ToString("D5"));
        }

        /// <summary>
        /// Reimplementation, removing certain conditions that don't match up properly and don't delete the rat person
        /// </summary>
        /// <param name="orig"></param>
        private void RatPersonScript_RatPickUp(On.RatPersonScript.orig_RatPickUp orig)
        {
            if (!AP_RatRando)
            {
                orig();
                return;
            }
            if (RatPlayerScript.instance != null)
                return;
            RatPersonScript.pickedUp = true;
            RatPlayerScript.SpawnRat();
            RatPlayerScript.EnsureNextPickToBeGood();
            CheeseScript.EnableAllCheese();
            Sound.Play("SoundRatJoined");
        }

        private bool RatPersonScript_IsRatPickedUp(On.RatPersonScript.orig_IsRatPickedUp orig)
        {
            return AP_RatRando ? AP_ReceivedRat : orig();
        }

        private void RatPersonScript_Awake(On.RatPersonScript.orig_Awake orig, RatPersonScript self)
        {
            RatPersonScript.instance = self;
            if (!AP_RatRando)
            {
                orig(self);
            }
            //else if (AP_SentRat)
            //{
            //    Object.Destroy(self.gameObject);
            //}
        }
    }
}
