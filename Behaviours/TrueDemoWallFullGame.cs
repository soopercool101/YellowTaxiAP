using UnityEngine;
using YellowTaxiAP.Managers;

namespace YellowTaxiAP.Behaviours
{
    internal class TrueDemoWallFullGame : MonoBehaviour
    {

        private void Awake()
        {
            // Deactivate object if full game unlock already sent
            if (APAreaStateManager.FullGameUnlocked && Plugin.ArchipelagoClient.AllClearedLocations.Contains(10_00011))
                gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject != PlayerScript.instance.gameObject)
                return;

            Plugin.ArchipelagoClient.SendLocation(10_00011);

            gameObject.SetActive(false);
        }
    }
}
