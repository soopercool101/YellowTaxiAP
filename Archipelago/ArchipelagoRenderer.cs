using BepInEx;
using UnityEngine;

namespace YellowTaxiAP.Archipelago
{
    public class ArchipelagoRenderer : MonoBehaviour
    {
        public void OnGUI()
        {
            // Improve visibility of connection window, title screen is white
            if (!ArchipelagoClient.Authenticated)
            {
                GUI.Box(new Rect(0, 0, 317, 170), string.Empty);
                GUI.Box(new Rect(0, 0, 317, 170), string.Empty);
                GUI.Box(new Rect(0, 0, 317, 170), string.Empty);
            }
            // show the mod is currently loaded in the corner
            GUI.Label(new Rect(16, 16, 300, 20), Plugin.ModDisplayInfo);
            ArchipelagoConsole.OnGUI();

            string statusMessage;
            // show the Archipelago Version and whether we're connected or not
            if (ArchipelagoClient.Authenticated)
            {
                // show cursor only in menus
                Cursor.visible = !PlayerScript.instance || MenuV2Script.instance;
                statusMessage = " Status: Connected";
                GUI.Label(new Rect(16, 50, 300, 20), Plugin.APDisplayInfo + statusMessage);
            }
            else
            {
                // show cursor always if not connected
                Cursor.visible = true;

                statusMessage = " Status: Disconnected";
                GUI.Label(new Rect(16, 50, 300, 20), Plugin.APDisplayInfo + statusMessage);
                GUI.Label(new Rect(16, 70, 150, 20), "Host: ");
                GUI.Label(new Rect(16, 90, 150, 20), "Player Name: ");
                GUI.Label(new Rect(16, 110, 150, 20), "Password: ");


                if (!Plugin.ArchipelagoClient.AttemptingConnection)
                {
                    GUI.SetNextControlName("APClientTextbox-Server");
                    ArchipelagoClient.ServerData.Uri = GUI.TextField(new Rect(150, 70, 150, 20),
                        ArchipelagoClient.ServerData.Uri);
                    GUI.SetNextControlName("APClientTextbox-SlotName");
                    ArchipelagoClient.ServerData.SlotName = GUI.TextField(new Rect(150, 90, 150, 20),
                        ArchipelagoClient.ServerData.SlotName);
                    GUI.SetNextControlName("APClientTextbox-Password");
                    ArchipelagoClient.ServerData.Password = GUI.PasswordField(new Rect(150, 110, 150, 20),
                        ArchipelagoClient.ServerData.Password, '*');
                }
                else
                {
                    GUI.Label(new Rect(150, 70, 150, 20), ArchipelagoClient.ServerData.Uri);
                    GUI.Label(new Rect(150, 90, 150, 20), ArchipelagoClient.ServerData.SlotName);
                    GUI.Label(new Rect(150, 110, 150, 20), new string('*', ArchipelagoClient.ServerData.Password.Length));
                }

                var enteringInfo = GUI.GetNameOfFocusedControl().StartsWith("APClientTextbox-");
                var enterPressed = Event.current.type == EventType.KeyDown && Event.current.character == '\n';
                // requires that the player at least puts *something* in the slot name
                if (Plugin.ArchipelagoClient.AttemptingConnection)
                {
                    GUI.Label(new Rect(16, 130, 100, 20), "Connecting...");
                    MenuV2Script.instance?.suspendInputs = false;
                }
                else if ((GUI.Button(new Rect(16, 130, 100, 20), "Connect") || (enteringInfo && enterPressed)) &&
                    !ArchipelagoClient.ServerData.SlotName.IsNullOrWhiteSpace())
                {
                    Plugin.ArchipelagoClient.Connect();
                    MenuV2Script.instance?.suspendInputs = false;
                }
                else if (enteringInfo)
                {
                    MenuV2Script.instance?.suspendInputs = true;
                }
            }
            // this is a good place to create and add a bunch of debug buttons
        }
    }
}
