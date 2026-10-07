using UnityEngine;

namespace YellowTaxiAP.Helpers
{
    public class TaxiSkins
    {
        public static readonly int[] ValidDefaultSkins = [0, 1, 2, 3, 4];
        public static readonly int[] ValidSkeletonSkins = [10, 11, 12, 13, 14, 15];
        public static readonly int[] ValidGoldenSkins = [20, 21, 22, 23, 24];
        public static readonly int[] ValidPrototypeSkins = [30, 31, 32, 33, 34, 35];
        public static Texture[] RegularCars = [];
        public static readonly int[] ValidCarSkins = [40, 41, 42, 43, 44, 45];
        public static Texture AngryTaxiTexture;
        public static Texture GrannysTexture;
        public static Texture GrannysCorruptedTexture;
        public static Texture PinkFlamesTexture;
        public static Texture PinkFlamesCorruptedTexture;
        public static Texture PoliceCarTexture;
        public static Texture PoliceCarCorruptedTexture;
        public static Texture StarsAndStripesTexture;
        public static Texture DestroyedTaxiTexture;
        public static Texture CityTaxiTexture;
        public static bool CustomTaxiTextureLoaded;
        public static Texture2D CustomTaxiTexture;
    }
}
