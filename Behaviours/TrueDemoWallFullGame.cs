using UnityEngine;
using YellowTaxiAP.Managers;

namespace YellowTaxiAP.Behaviours
{
    internal class TrueDemoWallFullGame : MonoBehaviour
    {
        private TrueDemoWallScript origScript;


        private void Awake()
        {
            // Deactivate object if full game unlock already sent or full game isn't shuffled
            if (!Plugin.SlotData.ShuffleFullGame || (APAreaStateManager.FullGameUnlocked && Plugin.ArchipelagoClient.AllClearedLocations.Contains(10_00011)))
                gameObject.SetActive(false);

            origScript = gameObject.GetComponent<TrueDemoWallScript>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject != PlayerScript.instance.gameObject)
                return;

            Instantiate(origScript.dialogue);

            gameObject.SetActive(false);
        }
    }
}
