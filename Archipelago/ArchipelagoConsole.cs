using System.Collections.Generic;
using System.Linq;
using BepInEx;
using UnityEngine;

namespace YellowTaxiAP.Archipelago;

// shamelessly stolen from oc2-modding https://github.com/toasterparty/oc2-modding/blob/main/OC2Modding/GameLog.cs
public static class ArchipelagoConsole
{
    public static bool Hidden = true;
    public static bool Initialized = false;

    private static List<ConsoleMessage> logLines = new();
    private static Vector2 scrollView;
    private static Rect window;
    private static Rect scroll;
    private static Rect text;
    private static Rect hideShowButton;

    private static GUIStyle textStyle = new();
    private static string scrollText = "";
    private static float lastUpdateTime = Time.time;
    private const int MaxLogLines = 80;
    private const float HideTimeout = 15f;

    private static string CommandText = "!help";
    private static Rect CommandTextRect;
    private static Rect SendCommandButton;

    public static void Awake()
    {
        UpdateWindow();
    }

    public static void LogMessage(ConsoleMessage message)
    {
        if (message.ToString().IsNullOrWhiteSpace()) return;

        if (logLines.Count == MaxLogLines)
        {
            logLines.RemoveAt(0);
        }
        logLines.Add(message);
        Plugin.BepinLogger.LogMessage(message.SimpleString);
        lastUpdateTime = Time.time;
        UpdateWindow();
    }

    public static void OnGUI()
    {
        if (logLines.Count == 0) return;

        if (!Hidden || Time.time - lastUpdateTime < HideTimeout)
        {
            scrollView = GUI.BeginScrollView(window, scrollView, scroll);
            // Improve visibility, moreso on menu
            if (!PlayerScript.instance || MenuV2Script.instance)
            {
                GUI.Box(text, string.Empty);
                GUI.Box(text, string.Empty);
                GUI.Box(text, string.Empty);
                GUI.Box(text, string.Empty);
            }
            else
            {
                GUI.backgroundColor = new Color(GUI.backgroundColor.r, GUI.backgroundColor.g, GUI.backgroundColor.b, 0.8f);
                GUI.Box(text, string.Empty);
                GUI.Box(text, string.Empty);
                GUI.backgroundColor = new Color(GUI.backgroundColor.r, GUI.backgroundColor.g, GUI.backgroundColor.b, 1);
            }
            GUI.Box(text, scrollText, textStyle);
            GUI.EndScrollView();
        }

#if DEBUG
        var buttonText = Hidden ? "Show" : "Hide";
        if (PlayerScript.instance && !MenuV2Script.instance)
        {
            buttonText = PlayerScript.instance.transform.position.ToString();
        }
#else
        // Show Show/Hide button only when in menus
        if (!ArchipelagoClient.Authenticated || !PlayerScript.instance || MenuV2Script.instance)
        {
            var buttonText = Hidden ? "Show" : "Hide";
#endif
            GUI.Box(hideShowButton, string.Empty);
            if (GUI.Button(hideShowButton, buttonText))
            {
                Hidden = !Hidden;
                MenuV2Script.instance?.suspendInputs = !Hidden;
                UpdateWindow();
            }
#if !DEBUG
        }
#endif
        
        // draw client/server commands entry
        if (Hidden || !ArchipelagoClient.Authenticated || (PlayerScript.instance && !MenuV2Script.instance))
        {
            return;
        }

        GUI.Box(CommandTextRect, string.Empty);
        GUI.Box(CommandTextRect, string.Empty);
        GUI.SetNextControlName("CommandText");
        CommandText = GUI.TextField(CommandTextRect, CommandText);
        var typingCommand = GUI.GetNameOfFocusedControl().Equals("CommandText");
        MenuV2Script.instance?.suspendInputs = typingCommand;
        if (!CommandText.IsNullOrWhiteSpace())
        {
            GUI.Box(SendCommandButton, string.Empty);
            GUI.Box(SendCommandButton, string.Empty);
            var enterPressed = Event.current.type == EventType.KeyDown && Event.current.character == '\n';
            if (GUI.Button(SendCommandButton, "Send") || (typingCommand && enterPressed))
            {
                Plugin.ArchipelagoClient.SendMessage(CommandText);
                CommandText = string.Empty;
            }
        }
    }

    public static void UpdateWindow()
    {
        scrollText = "";

        if (Hidden)
        {
            if (logLines.Count > 0)
            {
                scrollText = logLines[logLines.Count - 1].ToString();
            }
        }
        else
        {
            for (var i = 0; i < logLines.Count; i++)
            {
                scrollText += "> ";
                scrollText += logLines.ElementAt(i);
                if (i < logLines.Count - 1)
                {
                    scrollText += "\n\n";
                }
            }
        }

        var width = (int)(Screen.width * 0.4f);
        int height;
        int scrollDepth;
        if (Hidden)
        {
            height = (int)(Screen.height * 0.03f);
            scrollDepth = height;
        }
        else
        {
            height = (int)(Screen.height * 0.3f);
            scrollDepth = height * 10;
        }

        window = new Rect(Screen.width / 2 - width / 2, 0, width, height);
        scroll = new Rect(0, 0, width * 0.9f, scrollDepth);
        scrollView = new Vector2(0, scrollDepth);
        text = new Rect(0, 0, width, scrollDepth);

        textStyle.alignment = TextAnchor.LowerLeft;
        textStyle.fontSize = Hidden ? (int)(Screen.height * 0.0165f) : (int)(Screen.height * 0.0185f);
        textStyle.normal.textColor = Color.white;
        textStyle.wordWrap = !Hidden;

        var xPadding = (int)(Screen.width * 0.01f);
        var yPadding = (int)(Screen.height * 0.01f);

        textStyle.padding = Hidden
            ? new RectOffset(xPadding / 2, xPadding / 2, yPadding / 2, yPadding / 2)
            : new RectOffset(xPadding, xPadding, yPadding, yPadding);

        var buttonWidth = (int)(Screen.width * 0.12f);
        var buttonHeight = (int)(Screen.height * 0.03f);

        hideShowButton = new Rect(Screen.width / 2 + width / 2 + buttonWidth / 3, Screen.height * 0.004f, buttonWidth,
            buttonHeight);

        // draw server command text field and button
        width = (int)(Screen.width * 0.4f);
        var xPos = (int)(Screen.width / 2.0f - width / 2.0f);
        var yPos = (int)(Screen.height * 0.307f);
        height = (int)(Screen.height * 0.022f);

        CommandTextRect = new Rect(xPos, yPos, width, height);

        width = (int)(Screen.width * 0.035f);
        yPos += (int)(Screen.height * 0.03f);
        SendCommandButton = new Rect(xPos, yPos, width, height);
    }
}