using UnityEngine;

namespace YellowTaxiAP.Behaviours
{
    internal class TruePortalId : MonoBehaviour
    {
        public Data.LevelId OriginalLevel { get; private set; }
        public Data.LevelId OriginalKaizoLevel { get; private set; }

        public void Awake()
        {
            var portal = gameObject.GetComponent<PortalScript>();
            OriginalLevel = portal.targetLevelId;
            OriginalKaizoLevel = portal.kaizoLevelId;
        }
    }
}
