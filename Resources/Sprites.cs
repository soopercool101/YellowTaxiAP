using UnityEngine;

namespace YellowTaxiAP.Resources
{
    public static class Sprites
    {
        private static Sprite _ta1MapLocked;
        public static Sprite Ta1MapLocked => _ta1MapLocked ??= ReadSprite("ta1map_locked", 256, 256, FilterMode.Point);
        private static Sprite _ta2MapLocked;
        public static Sprite Ta2MapLocked => _ta2MapLocked ??= ReadSprite("ta2map_locked", 256, 256, FilterMode.Point);
        private static Sprite _ta3MapLocked;
        public static Sprite Ta3MapLocked => _ta3MapLocked ??= ReadSprite("ta3map_locked", 256, 256, FilterMode.Point);
        private static Sprite _psychoMapLocked;
        public static Sprite PsychoMapLocked => _psychoMapLocked ??= ReadSprite("psychotaximap_locked", 256, 256, FilterMode.Point);
        private static Sprite _psychoMapUnlocked;
        public static Sprite PsychoMapUnlocked => _psychoMapUnlocked ??= ReadSprite("psychotaximap", 256, 256, FilterMode.Point);

        private static Sprite ReadSprite(string resourceName, int width, int height, FilterMode filter)
        {
            return Sprite.Create(Textures.ReadTexture(resourceName, width, height, filter),
                new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f), 100f);
        }
    }
}
