using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace YellowTaxiAP.Helpers
{
    /// <summary>
    /// Normally, the game will not load most assets that are not part of the current level.
    ///
    /// This script loads every level and adds anything relevant to the randomizer to the global asset master at startup.
    /// Total hack job but *IT WORKS*
    /// </summary>
    public static class AssetLoadinator
    {
        public static bool Loaded { get; private set; }
        public static bool FullyLoaded { get; private set; }

        public static Dictionary<string, GameObject> SpecialBackgrounds { get; private set; } = new();

        public static int level = 3;
        public static void LoadAssets()
        {
            if (FullyLoaded)
                return;
            var name = SceneManager.GetActiveScene().name;
            Plugin.Log($"Scene: {name}");
            LoadGenericImportantAssets(out _);

            var nextLevel = ++level;
            // Skip Time trials past the first, don't need anything from them
            if (nextLevel == (int)Levels.Index.level_time_attack_02)
            {
                level = nextLevel = (int)Levels.Index.level_psycho_taxi;
            }
            // For some reason Fecal Matters has everything from gym loaded, so that's skippable
            else if (nextLevel == (int)Levels.Index.level_Gym)
            {
                nextLevel = ++level;
            }
            // Loop back after psycho taxi, we're done loading things
            else if (nextLevel > (int)Levels.Index.level_psycho_taxi)
            {
                nextLevel = 1;
                FullyLoaded = true;
            }
            Loaded = true;
            SceneManager.LoadScene(nextLevel);
        }

        public static void LoadGenericImportantAssets(out List<AssetMaster> assetMasters)
        {
            assetMasters = [];
            var loadedAssets = 0;

            foreach (var assetMaster in Object.FindObjectsByType<AssetMaster>(FindObjectsInactive.Include,
                         FindObjectsSortMode.None))
            {
                assetMasters.Add(assetMaster);
                Plugin.BepinLogger.LogMessage("=== Asset Master: " + assetMaster.name + " ===");
                if (assetMaster.name.Contains("Specific"))
                {
                    Plugin.BepinLogger.LogMessage("Songs:");
                    foreach (var song in assetMaster.musics)
                    {
                        Plugin.Log(" - " + song.name);
                        // Bonus level is already in the all levels asset master, don't need it
                        if (song.name.Equals("SoundtrackBonusLevel"))
                            continue;
                        try
                        {
                            AssetMaster.AddMusic(song);
                            if (!Plugin.KnownSongs.Contains(song.name))
                            {
                                Plugin.BepinLogger.LogWarning($"Unknown Song: {song.name}");
                            }

                            loadedAssets++;
                        }
                        catch (Exception e)
                        {
                            Plugin.BepinLogger.LogWarning(e);
                        }
                    }
                    Plugin.BepinLogger.LogMessage("Prefabs:");
                    foreach (var prefab in assetMaster.prefabs)
                    {
                        Plugin.BepinLogger.LogMessage(" - " + prefab.name);
                        // Background Soffitto Castello is a duplicate of Background Black
                        if (prefab.name.Equals("HudExtra_RadioactivePizza") || (prefab.name.Contains("Background") && !prefab.name.Equals("Background Soffitto Castello") && !prefab.name.Equals("Background Soffitto ToslaHQ")))
                        {
                            try
                            {
                                AssetMaster.AddPrefab(prefab);
                                loadedAssets++;
                            }
                            catch (Exception e)
                            {
                                Plugin.BepinLogger.LogWarning(e);
                            }
                        }
                    }

                    // Poopworld and time trials don't have prefabs as bgs
                    if (level is (int)Levels.Index.level_PoopWorld or (int)Levels.Index.level_time_attack_01)
                    {
                        Plugin.BepinLogger.LogMessage("Backgrounds (special):");
                        var bgs = Object.FindObjectsByType<BackgroundMaster>(FindObjectsInactive.Include, FindObjectsSortMode.None);

                        foreach (var bg in bgs)
                        {
                            if (bg && !SpecialBackgrounds.ContainsKey(bg.name))
                            {
                                Plugin.Log($"{bg.name} is not a prefab. Dealing with it!");
                                bg.gameObject.SetActive(false);
                                Object.DontDestroyOnLoad(bg);
                                SpecialBackgrounds.Add(bg.name, bg.gameObject);

                                loadedAssets++;
                            }
                        }
                    }

                    if (loadedAssets == 0)
                    {
                        Plugin.BepinLogger.LogError("Don't need to load this one actually!");
                    }
                    else
                    {
                        Plugin.BepinLogger.LogWarning($"Loaded {loadedAssets} assets!");
                    }
                }
            }
        }
    }
}
