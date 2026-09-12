using System.Reflection;
using UnityEngine;

namespace YellowTaxiAP.Resources
{
    public static class Textures
    {
        private static Texture2D _labDoorLocked;
        public static Texture2D LabDoorLocked => _labDoorLocked ??= ReadTexture("lab_door_closed", 1024, 1024, FilterMode.Point);

        public static Texture2D ReadTexture(string resourceName, int width, int height, FilterMode filter)
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"YellowTaxiAP.Resources.{resourceName}"))
            {
                Plugin.Log($"Reading Texture {resourceName}");
                var data = new byte[stream.Length];
                stream.Read(data, 0, data.Length);

                var texture = new Texture2D(width, height);
                texture.LoadImage(data);
                texture.filterMode = filter;
                return texture;
            }
        }
    }
}
