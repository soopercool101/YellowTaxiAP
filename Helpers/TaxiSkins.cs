using System.Collections.Generic;
using UnityEngine;
using YellowTaxiAP.Managers;

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

        public static bool TaxiSkinEverLoaded;

        public static void LoadTaxiSkin(bool showImmediately = false)
        {
            // Random every flip taxi skin should only change when taxi skin is needed
            if (Plugin.SlotData.TaxiSkin > 1000 && TaxiSkinEverLoaded)
                return;
            TaxiSkinEverLoaded = true;
            if (Plugin.SlotData.TaxiSkin % 10 == 9)
            {
                LoadRandomTaxiSkin(showImmediately);
            }
            else
            {
                APPlayerManager.CurrentTaxiSkin = Plugin.SlotData.TaxiSkin;
                if (showImmediately)
                    PlayerScript.PlayerHatsRenderingUpdate();
            }
        }

        public static void LoadRandomTaxiSkin(bool showImmediately = false)
        {
            switch ((Plugin.SlotData.TaxiSkin % 1000 - 9) / 10)
            {
                case 0:
                    APPlayerManager.CurrentTaxiSkin =
                        ValidDefaultSkins[Random.RandomRangeInt(0, ValidDefaultSkins.Length)];
                    break;
                case 1:
                    APPlayerManager.CurrentTaxiSkin =
                        ValidSkeletonSkins[Random.RandomRangeInt(0, ValidSkeletonSkins.Length)];
                    break;
                case 2:
                    APPlayerManager.CurrentTaxiSkin =
                        ValidGoldenSkins[Random.RandomRangeInt(0, ValidGoldenSkins.Length)];
                    break;
                case 3:
                    APPlayerManager.CurrentTaxiSkin =
                        ValidPrototypeSkins[Random.RandomRangeInt(0, ValidPrototypeSkins.Length)];
                    break;
                case 4:
                    APPlayerManager.CurrentTaxiSkin =
                        ValidCarSkins[Random.RandomRangeInt(0, ValidCarSkins.Length)];
                    break;
                default:
                    var allList = new List<int>();
                    allList.AddRange(ValidDefaultSkins);
                    allList.AddRange(ValidSkeletonSkins);
                    allList.AddRange(ValidGoldenSkins);
                    allList.AddRange(ValidPrototypeSkins);
                    allList.AddRange(ValidCarSkins);
                    allList.AddRange([
                        50,
                        60,
                        61,
                        70,
                        71,
                        80,
                        81,
                        90,
                        100,
                        110,
                    ]);
                    if (CustomTaxiTexture)
                    {
                        allList.Add(1000);
                    }
                    APPlayerManager.CurrentTaxiSkin = allList[Random.RandomRangeInt(0, allList.Count)];
                    break;
            }
            if (showImmediately)
                PlayerScript.PlayerHatsRenderingUpdate();
        }
    }
}
