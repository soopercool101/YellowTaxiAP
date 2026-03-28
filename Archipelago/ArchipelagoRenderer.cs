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

        private const float REFERENCE_WIDTH = 1920f;
        private const float REFERENCE_HEIGHT = 1080f;
        private const float LOGIN_UI_SCALE = 1.2f;

        private static GUIStyle loginLabelStyle = null;
        private static GUIStyle loginTextFieldStyle = null;
        private static GUIStyle loginButtonStyle = null;
        private static int currentLoginFontSize = -1;

        private static float ScaleX(float value, bool allowUnderscaling = false)
        {
            if (!allowUnderscaling && Screen.width < REFERENCE_WIDTH)
                return value;
            return value * (Screen.width / REFERENCE_WIDTH);
        }

        private static float ScaleY(float value, bool allowUnderscaling = false)
        {
            if (!allowUnderscaling && Screen.height < REFERENCE_HEIGHT)
                return value;
            return value * (Screen.height / REFERENCE_HEIGHT);
        }

        private static Rect ScaledRect(float x, float y, float width, float height)
        {
            return new Rect(ScaleX(x), ScaleY(y), ScaleX(width), ScaleY(height));
        }

        private static Rect LoginRect(float x, float y, float width, float height)
        {
            const float anchorX = 5f;
            const float anchorY = 5f;

            var scaledX = anchorX + (x - anchorX) * LOGIN_UI_SCALE;
            var scaledY = anchorY + (y - anchorY) * LOGIN_UI_SCALE;
            var scaledWidth = width * LOGIN_UI_SCALE;
            var scaledHeight = height * LOGIN_UI_SCALE;

            return ScaledRect(scaledX, scaledY, scaledWidth, scaledHeight);
        }

        private static void UpdateLoginTextStyles()
        {
            var desiredFontSize = Mathf.RoundToInt(ScaleY(12f * LOGIN_UI_SCALE));
            if (desiredFontSize == currentLoginFontSize && loginLabelStyle != null && loginTextFieldStyle != null && loginButtonStyle != null)
            {
                return;
            }

            currentLoginFontSize = desiredFontSize;

            loginLabelStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = currentLoginFontSize
            };

            loginTextFieldStyle = new GUIStyle(GUI.skin.textField)
            {
                fontSize = currentLoginFontSize
            };

            loginButtonStyle = new GUIStyle(GUI.skin.button)
            {
                fontSize = currentLoginFontSize
            };
        }

        public void OnGUI()
        {
            ArchipelagoConsole.OnGUI();

            string statusMessage;
            // show the Archipelago Version and whether we're connected or not
            if (ArchipelagoClient.Authenticated)
            {
                // show cursor only in menus
                Cursor.visible = !PlayerScript.instance || MenuV2Script.instance;
                statusMessage = " Status: Connected";
                // show the mod is currently loaded in the corner
                GUI.Label(new Rect(16, 16, 300, 20), Plugin.ModDisplayInfo);
                GUI.Label(new Rect(16, 50, 300, 20), Plugin.APDisplayInfo + statusMessage);
                if (_lastClosedKeyboard > 0.01f) // Prevent repeated gamepad keyboard entry in same textbox
                {
                    _lastClosedKeyboard = 0.0f;
                    GUI.FocusControl(null);
                }
            }
            else
            {
                UpdateLoginTextStyles();

                // show cursor always if not connected
                Cursor.visible = true;

                statusMessage = " Status: Disconnected";
                // Improve visibility of connection window, title screen is white
                GUI.Box(LoginRect(0, 0, 317, 170), string.Empty);
                GUI.Box(LoginRect(0, 0, 317, 170), string.Empty);
                GUI.Box(LoginRect(0, 0, 317, 170), string.Empty);
                // show the mod is currently loaded in the corner
                GUI.Label(LoginRect(16, 16, 300, 20), Plugin.ModDisplayInfo, loginLabelStyle);
                GUI.Label(LoginRect(16, 50, 300, 20), Plugin.APDisplayInfo + statusMessage, loginLabelStyle);
                GUI.Label(LoginRect(16, 70, 150, 20), "Host: ", loginLabelStyle);
                GUI.Label(LoginRect(16, 90, 150, 20), "Player Name: ", loginLabelStyle);
                GUI.Label(LoginRect(16, 110, 150, 20), "Password: ", loginLabelStyle);


                if (!Plugin.ArchipelagoClient.AttemptingConnection)
                {
                    GUI.SetNextControlName("APClientTextbox-Server");
                    ArchipelagoClient.ServerData.Uri = GUI.TextField(LoginRect(150, 70, 150, 20),
                        ArchipelagoClient.ServerData.Uri, loginTextFieldStyle);
                    GUI.SetNextControlName("APClientTextbox-SlotName");
                    ArchipelagoClient.ServerData.SlotName = GUI.TextField(LoginRect(150, 90, 150, 20),
                        ArchipelagoClient.ServerData.SlotName, loginTextFieldStyle);
                    GUI.SetNextControlName("APClientTextbox-Password");
                    ArchipelagoClient.ServerData.Password = GUI.PasswordField(LoginRect(150, 110, 150, 20),
                        ArchipelagoClient.ServerData.Password, '*', loginTextFieldStyle);
                }
                else
                {
                    GUI.Label(LoginRect(150, 70, 150, 22), ArchipelagoClient.ServerData.Uri, loginLabelStyle);
                    GUI.Label(LoginRect(150, 90, 150, 22), ArchipelagoClient.ServerData.SlotName, loginLabelStyle);
                    GUI.Label(LoginRect(150, 110, 150, 22), new string('*', ArchipelagoClient.ServerData.Password.Length), loginLabelStyle);
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
                    GUI.Label(LoginRect(16, 130, 100, 20), "Connecting...", loginLabelStyle);
                    MenuV2Script.instance?.suspendInputs = false;
                }
                else if ((GUI.Button(LoginRect(16, 130, 100, 20), "Connect", loginButtonStyle) || (enteringInfo && enterPressed) ||
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
