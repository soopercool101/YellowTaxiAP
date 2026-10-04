using System.Collections.Generic;
using I2.Loc;
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
            if (Plugin.SlotData.RandomizeSkyboxes == YTGVSlotData.CosmeticLoadOption.RandomEveryLoad)
                self.backgroundChange = "random"; // Make sure background gets set even when it would normally not be
            if (Plugin.SlotData.RandomizeMusic == YTGVSlotData.CosmeticLoadOption.RandomEveryLoad)
                self.songChange = GetRandomizedMusic(); // Gotta change song rando if random per load
            else if (Plugin.SlotData.RandomizeMusic == YTGVSlotData.CosmeticLoadOption.Special)
                self.songChange = string.Empty;
        }

        private int _radioIndex = -1;
        private bool _isInBoss = false;
        private void GameplayMaster_SoundtrackRoutine(On.GameplayMaster.orig_SoundtrackRoutine orig, GameplayMaster self)
        {
            var levelSoundtrack = self.levelSoundtrack;
            var bossSoundtrack = self.bossSoundtrack;
            var radioSoundtracks = self.radioSountracks;
            var isInBossOld = _isInBoss;
            if (Plugin.SlotData.RandomizeMusic == YTGVSlotData.CosmeticLoadOption.Special ||
                (Plugin.SlotData.RandomizeMusic == YTGVSlotData.CosmeticLoadOption.RandomEveryLoad &&
                 self.levelId == Data.LevelId.L20_PsychoTaxi))
            {
                if (!SoundtrackRandomized)
                {
                    self.levelSoundtrack = self.bossSoundtrack = string.Empty;
                    Plugin.Log("Initial Radio Randomization");
                    SetRadioTracks(self, GetRandomizedMusic(), false);
                    self.radioMusicIndex = -1;
                    if (HudMasterScript.instance)
                    {
                        HudMasterScript.instance.musicRadioDisabled = false;
                        HudMasterScript.instance.musicRadioText.gameObject.SetActive(true);
                    }

                    SoundtrackRandomized = true;
                }

                if (!Tick.Paused)
                {
                    // Have to do some hackiness with boss music to make it still count for the radio
                    var musicCapsule2 = Music.FindPaused(self.bossSoundtrack) ?? Music.FindPaused(self.levelSoundtrack);
                    if ((EnemyCarScript.bossInstance != null || EnemyGenericScript.bossGenericScriptInstance != null))
                    {
                        _isInBoss = true;
                        if (string.IsNullOrEmpty(self.bossSoundtrack) || !Music.IsPlaying(self.bossSoundtrack))
                        {
                            Plugin.Log($"Boss Radio Randomization (Boss soundtrack: {self.bossSoundtrack})");
                            do
                            {
                                self.bossSoundtrack = GetCompletelyRandomBossSong();
                            } while (self.bossSoundtrack == self.levelSoundtrack);
                            if (Music.IsPlaying(self.levelSoundtrack))
                                Music.Stop(self.levelSoundtrack);
                            if ((musicCapsule2 == null || !musicCapsule2.paused) && !Music.IsPlaying(self.bossSoundtrack))
                                Music.Play(self.bossSoundtrack);
                            Music.Find(self.bossSoundtrack).myAudioSource.loop = false;
                            self.radioSoundtrackTranslations[self.radioMusicIndex] = GetRadioName(self.bossSoundtrack);
                        }
                    }
                    else if ((EnemyCarScript.bossInstance == null && EnemyGenericScript.bossGenericScriptInstance == null) && !string.IsNullOrEmpty(self.bossSoundtrack))
                    {
                        _isInBoss = false;
                        Plugin.Log($"Non-Boss Radio Randomization (Boss soundtrack: {self.bossSoundtrack})");
                        if (Music.IsPlaying(self.bossSoundtrack))
                            Music.Stop(self.bossSoundtrack);
                        self.bossSoundtrack = string.Empty;
                        if ((musicCapsule2 == null || !musicCapsule2.paused) && !Music.IsPlaying(self.levelSoundtrack))
                            Music.Play(self.levelSoundtrack);
                        Music.Find(self.levelSoundtrack).myAudioSource.loop = false;
                        self.radioSoundtrackTranslations[self.radioMusicIndex] = GetRadioName(self.levelSoundtrack);
                    }
                }
            }
            else if (Plugin.SlotData.RandomizeMusic == YTGVSlotData.CosmeticLoadOption.RandomEveryLoad)
            {
                // For random every load, I need to make sure I don't constantly reroll music after it's loaded, so only load once per load here.
                if (!SoundtrackRandomized)
                {
                    self.levelSoundtrack = GetRandomizedMusic();
                    self.bossSoundtrack = string.Empty;
                    Plugin.Log($"Soundtrack initially randomized to: {self.levelSoundtrack}");
                    SoundtrackRandomized = true;
                }
                // Music will fail to play if boss music matches level music, so need to pretend the other one doesn't exist.
                if ((EnemyCarScript.bossInstance != null || EnemyGenericScript.bossGenericScriptInstance != null) && string.IsNullOrEmpty(self.bossSoundtrack))
                {
                    do
                    {
                        self.bossSoundtrack = GetCompletelyRandomBossSong();
                    } while (self.bossSoundtrack == self.levelSoundtrack);
                    Plugin.Log($"Boss Soundtrack randomized to: {self.bossSoundtrack}");
                }
                else if ((EnemyCarScript.bossInstance == null && EnemyGenericScript.bossGenericScriptInstance == null) && !string.IsNullOrEmpty(self.bossSoundtrack))
                {
                    do
                    {
                        self.levelSoundtrack = GetRandomizedMusic();
                    } while (self.bossSoundtrack == self.levelSoundtrack);
                    self.bossSoundtrack = string.Empty;
                    Plugin.Log($"Soundtrack re-randomized to: {self.levelSoundtrack}");
                }
            }
            else if (Plugin.SlotData.RandomizeMusic == YTGVSlotData.CosmeticLoadOption.Consistent)
            {
                // For consistent, I need to effectively trick the game into thinking the vanilla song is playing
                // Otherwise things like checkpoint respawns will roll different music
                self.levelSoundtrack = GetRandomizedMusic(self.levelSoundtrack);
                self.bossSoundtrack = GetRandomizedMusic(self.bossSoundtrack);
                for (var i = 0; i < self.radioSountracks.Length; i++)
                {
                    self.radioSountracks[i] = GetRandomizedMusic(self.radioSountracks[i]);
                    if (!SoundtrackRandomized)
                        self.radioSoundtrackTranslations[i] = GetRadioName(self.radioSountracks[i]);
                }

                SoundtrackRandomized = true;
            }
            orig(self);
            if (Plugin.SlotData.RandomizeMusic == YTGVSlotData.CosmeticLoadOption.Special ||
                 (Plugin.SlotData.RandomizeMusic == YTGVSlotData.CosmeticLoadOption.RandomEveryLoad &&
                  self.levelId == Data.LevelId.L20_PsychoTaxi))
            {
                if (self.radioMusicIndex > 0)
                {
                    SetRadioTracks(self, self.radioSountracks[self.radioMusicIndex], _isInBoss);
                }
            }
            if (Plugin.SlotData.RandomizeMusic != YTGVSlotData.CosmeticLoadOption.Consistent)
                return;
            self.levelSoundtrack = levelSoundtrack;
            self.bossSoundtrack = bossSoundtrack;
            self.radioSountracks = radioSoundtracks;
        }

        public static void SetRadioTracks(GameplayMaster self, string initialSong, bool bossMusic)
        {
            Plugin.Log($"Rolling radio tracks (Current radio id {self.radioMusicIndex})");
            self.radioSountracks = new string[2];
            self.radioSoundtrackTranslations = new string[2];
            self.radioSountracks[0] = initialSong;
            self.radioSoundtrackTranslations[0] = GetRadioName(self.radioSountracks[0]);
            do
            {
                self.radioSountracks[1] = bossMusic ? GetCompletelyRandomBossSong() : GetRandomizedMusic();
            } while (self.radioSountracks[1] == self.radioSountracks[0]);
            self.radioSoundtrackTranslations[1] = GetRadioName(self.radioSountracks[1]);
            self.radioMusicIndex = 0;
            Plugin.Log($"Radio tracks finalized ({self.radioSountracks[0]} | {self.radioSountracks[1]})");
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
                    YTGVSlotData.CosmeticLoadOption.RandomEveryLoad or YTGVSlotData.CosmeticLoadOption.Special => Plugin.ValidSongs[
                        Random.RandomRangeInt(0, Plugin.ValidSongs.Count)],
                    _ => originalMusic
                };
            }
            catch
            {
                return originalMusic;
            }
        }

        public static string GetCompletelyRandomBossSong()
        {
            return Plugin.ValidBossSongs[Random.RandomRangeInt(0, Plugin.ValidBossSongs.Count)];
        }

        public static Dictionary<string, string> ConsistentSkyboxMap = new();
        public static string GetRandomizedSkybox(string originalSkybox = "")
        {
            try
            {
                return Plugin.SlotData.RandomizeSkyboxes switch
                {
                    YTGVSlotData.CosmeticLoadOption.Consistent => ConsistentSkyboxMap[originalSkybox],
                    YTGVSlotData.CosmeticLoadOption.RandomEveryLoad => Plugin.ValidBGs[
                        Random.RandomRangeInt(0, Plugin.ValidBGs.Count)],
                    _ => originalSkybox
                };
            }
            catch
            {
                return originalSkybox;
            }
        }

        public static string GetRadioName(string song)
        {
            return song switch
            {
                "SoundtrackHatShop" => "Hat World",
                "SoundtrackBonusLevel" => "Bonus Area",
                "SoundtrackHubOutside" => LocalizationManager.GetTermTranslation("LEVEL_NAME_GRANNY_ISLAND"),
                "SoundtrackHubInside" => LocalizationManager.GetTermTranslation("MAP_AREA_NAME_GRANNY_ISLAND_LAB"),
                "SoundtrackBombeach" => LocalizationManager.GetTermTranslation("LEVEL_NAME_BOMBEACH"),
                "SoundtrackPizzaTime" => LocalizationManager.GetTermTranslation("LEVEL_NAME_PIZZA_TIME"),
                "SoundtrackMoriosHomeInternal" => LocalizationManager.GetTermTranslation(
                    "MAP_AREA_NAME_MORIO_HOME_INSIDE"),
                "SoundtrackMoriosHome" => LocalizationManager.GetTermTranslation("MAP_AREA_NAME_MORIO_HOME_OUTSIDE"),
                "SoundtrackArcadePanik" => LocalizationManager.GetTermTranslation("LEVEL_NAME_ARCADE_PANIK"),
                "SoundtrackToslaOffices" => LocalizationManager.GetTermTranslation("LEVEL_NAME_TOSLA_OFFICES"),
                "SoundtrackGym" => LocalizationManager.GetTermTranslation("LEVEL_NAME_GYM"),
                "SoundtrackPoopWorld" => LocalizationManager.GetTermTranslation("LEVEL_NAME_POOP_WORLD"),
                "SoundtrackSewers" => LocalizationManager.GetTermTranslation("LEVEL_NAME_SEWERS"),
                "SoundtrackCityLevel" => LocalizationManager.GetTermTranslation("LEVEL_NAME_CITY"),
                "SoundtrackCrashTestIndustries" => LocalizationManager.GetTermTranslation(
                    "LEVEL_NAME_CRASH_TEST_INDUSTRIES"),
                "SoundtrackMoriosMind" => LocalizationManager.GetTermTranslation("LEVEL_NAME_MORIOS_MIND"),
                "SoundtrackRuinedObservatory" => LocalizationManager.GetTermTranslation("LEVEL_NAME_STARMAN_CASTLE"),
                "SoundtrackToslaHQ" => LocalizationManager.GetTermTranslation("LEVEL_NAME_TOSLA_HQ"),
                "SoundtrackMoonTheme" => LocalizationManager.GetTermTranslation("LEVEL_NAME_MOON"),
                "SoundtrackRocket" => LocalizationManager.GetTermTranslation("LEVEL_NAME_ROCKET"),
                "SoundtrackTimeAttack" => LocalizationManager.GetTermTranslation("TIME_ATTACK_MENU_TIME_ATTACK"),
                "MEGA_RAN_-_TAXI_REFERENCE" => LocalizationManager.GetTermTranslation("MUSIC_RADIO_DATA_MEGARAN"),
                "Fasten_your_Seatbelt_MASTER Silence Cut" => LocalizationManager.GetTermTranslation(
                    "MUSIC_RADIO_DATA_GAME&SOUND_COVER"),
                "CrGuitarfasten_your_seatbelts_wav" => LocalizationManager.GetTermTranslation(
                    "MUSIC_RADIO_DATA_CRGUITAR_COVER"),
                "SoundtrackBossFight1" => LocalizationManager.GetTermTranslation("NAME_BOSS_BOBOMBOSS"),
                "SoundtrackBossFightImportant" => LocalizationManager.GetTermTranslation("NAME_ALIEN_MOSK"),
                "SoundtrackBossFightFinal" => LocalizationManager.GetTermTranslation("DIALOGUE_BOSS_FIGHT_STARTING_6"),
                "SoundtrackMainMenu" => $"Fasten Your Seatbelt ({LocalizationManager.GetTermTranslation("MENU_SUB_TITLE_MAIN_MENU")})",
                "SoundtrackCredits" => LocalizationManager.GetTermTranslation("CREDITS_TITLE"),
                _ => song
            };
        }
    }
}
