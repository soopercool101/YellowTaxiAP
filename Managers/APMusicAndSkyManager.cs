using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using YellowTaxiAP.Archipelago;
using YellowTaxiAP.Helpers;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace YellowTaxiAP.Managers
{
    public class APMusicAndSkyManager
    {
        public APMusicAndSkyManager()
        {
            On.MegarunSongScript.Awake += MegarunSongScript_Awake;
            On.GameplayMaster.Awake += GameplayMaster_Awake;
            On.GameplayMaster.Start += GameplayMaster_Start;
            On.GameplayMaster.SoundtrackRoutine += GameplayMaster_SoundtrackRoutine;
            On.PortalTransitionScript.Start += PortalTransitionScript_Start;
            On.BackgroundMaster.Change += BackgroundMaster_Change;
        }

        private void GameplayMaster_Start(On.GameplayMaster.orig_Start orig, GameplayMaster self)
        {
            orig(self);
            BackgroundMaster.Change(BackgroundMaster.instance?.name ?? string.Empty);
        }

        public static string currentBG;
        public static bool SuppressBGRando { get; set; }
        private void BackgroundMaster_Change(On.BackgroundMaster.orig_Change orig, string backgroundName)
        {
            var newBackgroundName = SuppressBGRando ? backgroundName : GetRandomizedSkybox(backgroundName);
            // Empty background, just destroy
            if (string.IsNullOrEmpty(newBackgroundName))
            {
                if (BackgroundMaster.instance != null)
                    Object.Destroy(BackgroundMaster.instance.gameObject);
            }
            // Some backgrounds aren't prefabs, so I keep them loaded for just such an occasion
            else if (AssetLoadinator.SpecialBackgrounds.ContainsKey(newBackgroundName))
            {
                if (BackgroundMaster.instance != null)
                    Object.Destroy(BackgroundMaster.instance.gameObject);
                var newBg = Object.Instantiate(AssetLoadinator.SpecialBackgrounds[newBackgroundName]);
                newBg.name = backgroundName;
                newBg.SetActive(true);
            }
            else
            {
                orig(newBackgroundName);
                BackgroundMaster.instance.name = backgroundName;
            }
            currentBG = newBackgroundName;
        }

        private void PortalTransitionScript_Start(On.PortalTransitionScript.orig_Start orig, PortalTransitionScript self)
        {
            orig(self);
            // Gotta change song rando if random per load
            SoundtrackRandomized = false;
        }

        private void GameplayMaster_SoundtrackRoutine(On.GameplayMaster.orig_SoundtrackRoutine orig, GameplayMaster self)
        {
            var levelSoundtrack = self.levelSoundtrack;
            var bossSoundtrack = self.bossSoundtrack;
            var radioSoundtracks = self.radioSountracks;
            if (Plugin.SlotData.RandomizeMusic == YTGVSlotData.CosmeticLoadOption.RandomEveryLoad)
            {
                // For random every load, I need to make sure I don't constantly reroll music after it's loaded, so only load once per load here.
                if (!SoundtrackRandomized)
                {
                    self.levelSoundtrack = GetRandomizedMusic();
                    SoundtrackRandomized = true;
                }
                // Music will fail to play if boss music matches level music, so need to pretend the other one doesn't exist.
                if (EnemyCarScript.bossInstance != null && string.IsNullOrEmpty(self.bossSoundtrack))
                {
                    do
                    {
                        self.bossSoundtrack = GetRandomizedMusic();
                    } while (self.bossSoundtrack == self.levelSoundtrack);
                }
                else if (EnemyCarScript.bossInstance == null && !string.IsNullOrEmpty(self.bossSoundtrack))
                {
                    do
                    {
                        self.levelSoundtrack = GetRandomizedMusic();
                    } while (self.bossSoundtrack == self.levelSoundtrack);
                    self.bossSoundtrack = string.Empty;
                }
            }
            else
            {
                // For consistent, I need to effectively trick the game into thinking the vanilla song is playing
                // Otherwise things like checkpoint respawns will roll different music
                self.levelSoundtrack = GetRandomizedMusic(self.levelSoundtrack);
                self.bossSoundtrack = GetRandomizedMusic(self.bossSoundtrack);
                for (var i = 0; i < self.radioSountracks.Length; i++)
                {
                    self.radioSountracks[i] = GetRandomizedMusic(self.radioSountracks[i]);
                }
            }
            orig(self);
            self.levelSoundtrack = levelSoundtrack;
            self.bossSoundtrack = bossSoundtrack;
            self.radioSountracks = radioSoundtracks;
        }

        public static bool SoundtrackRandomized { get; private set; }

        private void GameplayMaster_Awake(On.GameplayMaster.orig_Awake orig, GameplayMaster self)
        {
            orig(self);
            SoundtrackRandomized = false;
        }

        private void MegarunSongScript_Awake(On.MegarunSongScript.orig_Awake orig, MegarunSongScript self)
        {
            orig(self);
            self.audioSource.clip = AssetMaster.GetMusic(GetRandomizedMusic("MEGA_RAN_-_TAXI_REFERENCE"));
        }

        public static Dictionary<string, string> ConsistentMusicMap = new();

        public static string GetRandomizedMusic(string originalMusic = "")
        {
            try
            {
                return Plugin.SlotData.RandomizeMusic switch
                {
                    YTGVSlotData.CosmeticLoadOption.Consistent => ConsistentMusicMap[originalMusic],
                    YTGVSlotData.CosmeticLoadOption.RandomEveryLoad => Plugin.KnownSongs[
                        Random.RandomRangeInt(0, Plugin.KnownSongs.Length)],
                    _ => originalMusic
                };
            }
            catch
            {
                return originalMusic;
            }
        }

        public static Dictionary<string, string> ConsistentSkyboxMap = new();
        public static string GetRandomizedSkybox(string originalSkybox = "")
        {
            try
            {
                return Plugin.SlotData.RandomizeMusic switch
                {
                    YTGVSlotData.CosmeticLoadOption.Consistent => ConsistentSkyboxMap[originalSkybox],
                    YTGVSlotData.CosmeticLoadOption.RandomEveryLoad => Plugin.KnownBGs[
                        Random.RandomRangeInt(0, Plugin.KnownBGs.Length)],
                    _ => originalSkybox
                };
            }
            catch
            {
                return originalSkybox;
            }
        }
    }
}
