namespace YellowTaxiAP.Managers
{
    /// <summary>
    /// THIS ONLY SUPPORTS ORANGE SWITCH, THE GAME IS SET UP TO HANDLE MORE.
    /// IF MORE ARE ADDED, THIS WILL NEED TO BE EDITED.
    /// </summary>
    public class OrangeSwitchManager
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
        public OrangeSwitchManager()
        {
            On.SwitchBlockScript.ChangeState += ChangeState_AP;
            On.SwitchBlockScript.SetState += SetState_AP;
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
