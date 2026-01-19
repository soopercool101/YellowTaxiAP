using UnityEngine;
using YellowTaxiAP.Managers;

namespace YellowTaxiAP.Behaviours
{
    public class OrangeSwitchController : MonoBehaviour
    {
        public bool ExpectedState => APSwitchManager.OrangeSwitchUnlocked;
        public bool? state;

        private void FixedUpdate()
        {
            if (ExpectedState == state)
                return;

            foreach (var block in SwitchBlockScript.list)
            {
                block.ChangeState();
            }

            state = ExpectedState;
        }
    }
}
