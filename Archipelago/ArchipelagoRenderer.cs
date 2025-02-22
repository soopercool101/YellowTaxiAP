using BepInEx;
using UnityEngine;
using YellowTaxiAP.Utils;

namespace YellowTaxiAP.Archipelago
{
    public class ArchipelagoRenderer : MonoBehaviour
    {
        public void OnGUI()
        {
            // show the mod is currently loaded in the corner
            GUI.Label(new Rect(16, 16, 300, 20), Plugin.ModDisplayInfo);
            ArchipelagoConsole.OnGUI();

            string statusMessage;
            // show the Archipelago Version and whether we're connected or not
            if (ArchipelagoClient.Authenticated)
            {
                // if your game doesn't usually show the cursor this line may be necessary
                Cursor.visible = false;

                statusMessage = " Status: Connected";
                GUI.Label(new Rect(16, 50, 300, 20), Plugin.APDisplayInfo + statusMessage);
            }
            else
            {
                // if your game doesn't usually show the cursor this line may be necessary
                Cursor.visible = true;

                statusMessage = " Status: Disconnected";
                GUI.Label(new Rect(16, 50, 300, 20), Plugin.APDisplayInfo + statusMessage);
                GUI.Label(new Rect(16, 70, 150, 20), "Host: ");
                GUI.Label(new Rect(16, 90, 150, 20), "Player Name: ");
                GUI.Label(new Rect(16, 110, 150, 20), "Password: ");

                ArchipelagoClient.ServerData.Uri = GUI.TextField(new Rect(150, 70, 150, 20),
                    ArchipelagoClient.ServerData.Uri);
                ArchipelagoClient.ServerData.SlotName = GUI.TextField(new Rect(150, 90, 150, 20),
                    ArchipelagoClient.ServerData.SlotName);
                ArchipelagoClient.ServerData.Password = GUI.TextField(new Rect(150, 110, 150, 20),
                    ArchipelagoClient.ServerData.Password);

                // requires that the player at least puts *something* in the slot name
                if (GUI.Button(new Rect(16, 130, 100, 20), "Connect") &&
                    !ArchipelagoClient.ServerData.SlotName.IsNullOrWhiteSpace())
                {
                    Plugin.ArchipelagoClient.Connect();
                }
            }
            // this is a good place to create and add a bunch of debug buttons
        }
    }
}
