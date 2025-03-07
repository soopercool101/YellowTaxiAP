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
            // Cheese scripts
            On.CheeseScript.GetCheeseIdString += CheeseScript_GetCheeseIdString;
            On.CheeseScript.MarkPickedUp += CheeseScript_MarkPickedUp;
        }

        private void DialogueScript_SpecialMethod_OnAnswerYes_PickupRat(On.DialogueScript.orig_SpecialMethod_OnAnswerYes_PickupRat orig, DialogueScript self)
        {
            if (!AP_RatRando)
            {
                RatPersonScript.RatPickUp();
            }
            else
            {
                // TODO: Send rat location
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
