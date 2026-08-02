using System;
using System.Collections.Generic;
using Archipelago.MultiClient.Net.MessageLog.Messages;
using Archipelago.MultiClient.Net.Models;

namespace YellowTaxiAP.Archipelago
{
    public class ConsoleMessage
    {
        public LogMessage Message { get; set; }
        public string StrMessage { get; set; }

        public static Dictionary<Color, Color> ColorblindColors = new()
        {
            { Color.Green, new Color(0, 0xC5, 0x1B) }, // Found location
            { Color.Magenta, new Color(0xFF, 0x87, 0xD7) }, // Player
            { Color.Blue, new Color(0x64, 0x95, 0xED) }, // Entrance
            { Color.Yellow, new Color(0x5F, 0xAF, 0xFF) }, // Other Player
            { Color.Cyan, new Color(0xB2, 0xB2, 0xB2) }, // Filler
            { Color.SlateBlue, new Color(0xAF, 0xD7, 0x5F) }, // Useful
            { Color.Plum, new Color(0xFF, 0xC5, 0) }, // Progression
            { Color.Salmon, new Color(0xFA, 0x80, 0x72) }, // Trap
            { Color.White, new Color(0xFF, 0xFF, 0xFF) },
        };

        public ConsoleMessage(LogMessage message)
        {
            Message = message;
            StrMessage = null;
        }

        public ConsoleMessage(string message)
        {
            Message = null;
            StrMessage = message;
        }

        public override string ToString()
        {
            try
            {
                if (Message != null)
                {
                    var s = string.Empty;
                    foreach (var part in Message.Parts)
                    {
                        var color = part.Color;
                        if (Plugin.PluginLoaded)
                        {
                            if (Settings.colorblindModeEnabled && ColorblindColors.ContainsKey(color))
                            {
                                color = ColorblindColors[color];
                            }
                        }
                        // default green is unreadable in many instances, brighten it to improve this
                        if (color.Equals(new Color(0, 128, 0)))
                        {
                            color = new Color(0, 200, 0);
                        }

                        s += $"<color=#{color.R:X2}{color.G:X2}{color.B:X2}>";
                        s += part.Text;
                        s += "</color>";
                    }

                    return s;
                }
            }
            catch (Exception ex)
            {
                Plugin.BepinLogger.LogWarning(ex);
            }
            return SimpleString;
        }

        public string SimpleString => Message?.ToString() ?? StrMessage;

        public static implicit operator ConsoleMessage(LogMessage message) => new(message);
        public static implicit operator ConsoleMessage(string message) => new(message);
    }
}
