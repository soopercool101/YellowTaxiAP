using System;
using Archipelago.MultiClient.Net.MessageLog.Messages;
using Archipelago.MultiClient.Net.Models;

namespace YellowTaxiAP.Archipelago
{
    public class ConsoleMessage
    {
        public LogMessage Message { get; set; }
        public string StrMessage { get; set; }

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
