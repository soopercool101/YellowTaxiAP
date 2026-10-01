using UnityEngine;

namespace YellowTaxiAP.Behaviours
{
    public class TimedDestroy : MonoBehaviour
    {
        public float DestructionTimer = 5f;
        public bool OnlyTimeOnTrapTimer = true;

        public void Update()
        {
            if (OnlyTimeOnTrapTimer && APTrapController.ShouldNotUpdateTraps)
                return;
            DestructionTimer -= Tick.Time;
            if (DestructionTimer <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
