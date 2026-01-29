using BepInEx;
using Steamworks;
using Steamworks.Data;
using UnityEngine;

namespace YellowTaxiAP.Archipelago
{
    public class ArchipelagoRenderer : MonoBehaviour
    {
        public static bool AutomaticGamepadInput;
        public static bool InGamepadInput;
        private static float _lastClosedKeyboard;
        public static int GamepadInputStep = -1;
        public static bool AttemptedConnectionOnce;

        public static bool CheckTimeElapsedSinceLastClosedKeyboard(float deltaTime)
        {
            return Time.realtimeSinceStartup - _lastClosedKeyboard >= deltaTime;
        }

        public void Awake()
        {
            if (Plugin.IsSteam && Plugin.EnableSteamKeyboard)
            {
                Dispatch.Install<GamepadTextInputDismissed_t>(OnKeyboardDismissed, server: false);
            }
        }

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
                if (_lastClosedKeyboard > 0.01f) // Prevent repeated gamepad keyboard entry in same textbox
                {
                    _lastClosedKeyboard = 0.0f;
                    GUI.FocusControl(null);
                }
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
                    GUI.Label(new Rect(150, 70, 150, 22), ArchipelagoClient.ServerData.Uri);
                    GUI.Label(new Rect(150, 90, 150, 22), ArchipelagoClient.ServerData.SlotName);
                    GUI.Label(new Rect(150, 110, 150, 22), new string('*', ArchipelagoClient.ServerData.Password.Length));
                }

                var enteringInfo = GUI.GetNameOfFocusedControl().StartsWith("APClientTextbox-");
                var enterPressed = Event.current.type == EventType.KeyDown && Event.current.character == '\n';
                if (Plugin.IsSteam && Plugin.EnableSteamKeyboard)
                {
                    if (!InGamepadInput && CheckTimeElapsedSinceLastClosedKeyboard(0.4f))
                    {
                        if (AutomaticGamepadInput)
                        {
                            GamepadInputStep++;
                            ShowSteamGamepadKeyboard();
                        }
                        else if (_lastClosedKeyboard > 0.01f) // Prevent repeated gamepad keyboard entry in same textbox
                        {
                            _lastClosedKeyboard = 0.0f;
                            GUI.FocusControl(null);
                            enteringInfo = false;
                        }
                        else if (enteringInfo)
                        {
                            GamepadInputStep = GUI.GetNameOfFocusedControl() switch
                            {
                                "APClientTextbox-Server" => 0,
                                "APClientTextbox-SlotName" => 1,
                                "APClientTextbox-Password" => 2,
                                _ => GamepadInputStep
                            };
                            ShowSteamGamepadKeyboard();
                        }
                    }
                }
                
                if (Plugin.ArchipelagoClient.AttemptingConnection)
                {
                    GUI.Label(new Rect(16, 130, 100, 20), "Connecting...");
                    MenuV2Script.instance?.suspendInputs = false;
                }
                else if ((GUI.Button(new Rect(16, 130, 100, 20), "Connect") || (enteringInfo && enterPressed) ||
                          (AutomaticGamepadInput && GamepadInputStep == 3)) &&
                         !ArchipelagoClient.ServerData.SlotName.IsNullOrWhiteSpace())
                {
                    Plugin.ArchipelagoClient.Connect();
                    AttemptedConnectionOnce = true;
                    AutomaticGamepadInput = false;
                    MenuV2Script.instance?.suspendInputs = false;
                }
                else
                {
                    MenuV2Script.instance?.suspendInputs = enteringInfo;
                }
            }
        }

        private void OnKeyboardDismissed(GamepadTextInputDismissed_t callback)
        {
            if (callback.Submitted)
            {
                var text = SteamUtils.GetEnteredGamepadText();
                //Plugin.Log($"Keyboard submitted: {callback.SubmittedText} | \"{text ?? "<null>"}\"");
                if (!ArchipelagoClient.Authenticated)
                {
                    switch (GamepadInputStep)
                    {
                        case 0:
                            ArchipelagoClient.ServerData.Uri = text;
                            break;
                        case 1:
                            ArchipelagoClient.ServerData.SlotName = text;
                            break;
                        case 2:
                            ArchipelagoClient.ServerData.Password = text;
                            break;
                    }
                }
                else
                {
                    // If we're not in setup, we're in the console
                    ArchipelagoConsole.SendMessage(text);
                }
            }
            else
            {
                Plugin.Log("Keyboard dismissed");
                AutomaticGamepadInput = false;
            }

            InGamepadInput = false;
            _lastClosedKeyboard = Time.realtimeSinceStartup;
        }

        private void ShowSteamGamepadKeyboard()
        {
            if (Plugin.ArchipelagoClient.AttemptingConnection || InGamepadInput || GamepadInputStep is > 2 or < 0)
                return;
            var description = GamepadInputStep switch
            {
                0 => "Host",
                1 => "Slot Name",
                2 => "Password",
                _ => string.Empty
            };
            var startingText = GamepadInputStep switch
            {
                0 => ArchipelagoClient.ServerData.Uri,
                1 => ArchipelagoClient.ServerData.SlotName,
                2 => ArchipelagoClient.ServerData.Password,
                _ => string.Empty
            };
            if (SteamUtils.ShowGamepadTextInput(
                    GamepadInputStep == 2 ? GamepadTextInputMode.Password : GamepadTextInputMode.Normal,
                    GamepadTextInputLineMode.SingleLine, description, int.MaxValue, startingText))
            {
                InGamepadInput = true;
            }
            else
            {
                Plugin.Log("Failed to open keyboard");
                Plugin.EnableSteamKeyboard = false;
                InGamepadInput = false;
                AutomaticGamepadInput = false;
            }
        }
    }
}
