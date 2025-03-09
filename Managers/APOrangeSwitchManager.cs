using UnityEngine;

namespace YellowTaxiAP.Managers
{
    /// <summary>
    /// THIS ONLY SUPPORTS ORANGE SWITCH, THE GAME IS SET UP TO HANDLE MORE.
    /// IF MORE ARE ADDED, THIS WILL NEED TO BE EDITED.
    /// </summary>
    public class APOrangeSwitchManager
    {
        private static bool _switchActive;
        public static bool OrangeSwitchActive
        {
            get => _switchActive;
            set
            {
                _switchActive = value;
                foreach (var block in SwitchBlockScript.list)
                {
                    block.ChangeState();
                }
            }
        }

        public static bool SwitchPressed = false;
        public APOrangeSwitchManager()
        {
            On.SwitchBlockScript.ChangeState += ChangeState_AP;
            On.SwitchBlockScript.SetState += SetState_AP;
            On.GiantSwitchScript.Start += GiantSwitchScript_Start;
            On.GiantSwitchScript.VisualSwitch += GiantSwitchScript_VisualSwitch;
            On.GiantSwitchScript.SwitchMeOn += GiantSwitchScript_SwitchMeOn;
        }

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
            // Disabled. Relevant code has been moved to the OrangeSwitchActive setter
        }

        private void ChangeState_AP(On.SwitchBlockScript.orig_ChangeState orig, SwitchBlockScript self)
        {
            self.visibleBlockModel.SetActive(OrangeSwitchActive);
            self.invisibleBlockModel.SetActive(!OrangeSwitchActive);
        }
    }
}
