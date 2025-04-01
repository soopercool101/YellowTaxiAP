using UnityEngine;

namespace YellowTaxiAP.Managers
{
    /// <summary>
    /// THIS ONLY SUPPORTS ORANGE SWITCH, THE GAME IS SET UP TO HANDLE MORE.
    /// IF MORE ARE ADDED, THIS WILL NEED TO BE EDITED.
    /// </summary>
    public class APSwitchManager
    {
        private static bool _orangeSwitchUnlocked;
        public static bool OrangeSwitchUnlocked
        {
            get => _orangeSwitchUnlocked;
            set
            {
                _orangeSwitchUnlocked = value;
                foreach (var block in SwitchBlockScript.list)
                {
                    block.ChangeState();
                }
            }
        }

        public static bool SwitchPressed = false;
        public APSwitchManager()
        {
            // Orange Switch Blocks
            On.SwitchBlockScript.ChangeState += ChangeState_AP;
            On.SwitchBlockScript.SetState += SetState_AP;
            // Orange Switch
            On.GiantSwitchScript.Start += GiantSwitchScript_Start;
            On.GiantSwitchScript.VisualSwitch += GiantSwitchScript_VisualSwitch;
            On.GiantSwitchScript.SwitchMeOn += GiantSwitchScript_SwitchMeOn;

            // Panik Arcade Switches
            On.PanikBlockScript.Switch += PanikBlockScript_Switch;
        }

        private static bool _greenSwitchUnlocked = true;
        public static bool GreenSwitchUnlocked
        {
            get => _greenSwitchUnlocked;
            set
            {
                _greenSwitchUnlocked = value;
                PanikBlockScript.SwitchAll(PanikSwitchScript.switchState);
            }
        }

        private static bool _purpleSwitchUnlocked = true;
        public static bool PurpleSwitchUnlocked
        {
            get => _purpleSwitchUnlocked;
            set
            {
                _purpleSwitchUnlocked = value;
                PanikBlockScript.SwitchAll(PanikSwitchScript.switchState);
            }
        }

        /// <summary>
        /// Prevent Blocks from being solid unless the relevant switch has been unlocked.
        /// </summary>
        private void PanikBlockScript_Switch(On.PanikBlockScript.orig_Switch orig, PanikBlockScript self, bool state)
        {
            bool enabled = self.alternativeKind ? GreenSwitchUnlocked : PurpleSwitchUnlocked;
            if (self.alternativeKind) // alternativeKind = Green Block
            {
                state = !state;
            }
            self.myBlockHolder.SetActive(state && enabled);
            self.myLinesHolder.SetActive(!state && enabled);
        }

        /// <summary>
        /// Override when 
        /// </summary>
        private void GiantSwitchScript_SwitchMeOn(On.GiantSwitchScript.orig_SwitchMeOn orig, GiantSwitchScript self)
        {
            if (self.switched)
                return;
            SwitchPressed = true;
            self.VisualSwitch();
            Sound.Play("SoundSwitchTurnOn");
            DebugLocationHelper.CheckLocation("Orange Switch", "10_00_00001");
            _ = Spawn.FromPool("Pt Star Rnbw Big - 8 Ring", self.transform.position, Pool.instance.transform).transform.SetYAngle(Random.value * 360f);
        }

        private void GiantSwitchScript_Start(On.GiantSwitchScript.orig_Start orig, GiantSwitchScript self)
        {
            self.VisualSwitch();
        }

        private void GiantSwitchScript_VisualSwitch(On.GiantSwitchScript.orig_VisualSwitch orig, GiantSwitchScript self)
        {
            self.switched = SwitchPressed;
            self.meshOff.enabled = !SwitchPressed;
            self.meshOn.enabled = SwitchPressed;
        }

        private void SetState_AP(On.SwitchBlockScript.orig_SetState orig, SwitchBlockScript.SwitchBlockKind switchesToAffect, bool changeState)
        {
            // Disabled. Relevant code has been moved to the OrangeSwitchUnlocked setter
        }

        private void ChangeState_AP(On.SwitchBlockScript.orig_ChangeState orig, SwitchBlockScript self)
        {
            self.visibleBlockModel.SetActive(OrangeSwitchUnlocked);
            self.invisibleBlockModel.SetActive(!OrangeSwitchUnlocked);
        }
    }
}
