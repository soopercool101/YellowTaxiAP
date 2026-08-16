using System.Collections;
using UnityEngine;
using YellowTaxiAP.Behaviours;
using static Data;

namespace YellowTaxiAP.Managers
{
    public class APTimeAttackManager
    {
        public APTimeAttackManager()
        {
            On.TimeAttackComputerScript.Start += TimeAttackComputerScript_Start;
            On.TimeAttackComputerScript.Update += TimeAttackComputerScript_Update;
            On.TimeAttackComputerScript.FixedUpdate += TimeAttackComputerScript_FixedUpdate;
            On.TimeAttackComputerScript.TurnOn += TimeAttackComputerScript_TurnOn;
            On.TimeAttackComputerScript.TurnOff += TimeAttackComputerScript_TurnOff;

            On.GameplayMaster.TimeAttackEnd += GameplayMaster_TimeAttackEnd;
            On.GameplayMaster.TimeAttackEndCoroutine += GameplayMaster_TimeAttackEndCoroutine;

            On.Data.TimeAttackData_GetCurrent += Data_TimeAttackData_GetCurrent;
            On.Data.TimeAttackData_SaveCurrent += Data_TimeAttackData_SaveCurrent;
        }

        private void TimeAttackComputerScript_TurnOn(On.TimeAttackComputerScript.orig_TurnOn orig, TimeAttackComputerScript self)
        {
            orig(self);
            APSaveController.PortalSave.SetLevelPortalUnlocked(LevelConverter.GetLevelId(self.timeAttackSceneIndex));
        }

        private void TimeAttackComputerScript_TurnOff(On.TimeAttackComputerScript.orig_TurnOff orig, TimeAttackComputerScript self, bool repositionPlayer)
        {
            orig(self, repositionPlayer);
            if ((self.menuOptions?.Count ?? 0) > 0)
            {
                self.normalText.text = string.Empty;
                for (var index = 0; index < self.menuOptions[self.menuIndex].Count; ++index)
                {
                    var normalText = self.normalText;
                    normalText.text = $"{normalText.text}{self.menuOptions[self.menuIndex][index]}\n";
                }
            }
        }

        private void TimeAttackComputerScript_Update(On.TimeAttackComputerScript.orig_Update orig, TimeAttackComputerScript self)
        {
            orig(self);
            self.backImage.color = self.baseImageColor;
            self.levelImage.enabled = true;
            self.titleText.enabled = true;
            self.normalText.enabled = true;
        }

        private void TimeAttackComputerScript_FixedUpdate(On.TimeAttackComputerScript.orig_FixedUpdate orig, TimeAttackComputerScript self)
        {
            if (APSaveController.PortalSave.IsLevelPortalUnlocked(LevelConverter.GetLevelId(self.timeAttackSceneIndex)))
            {
                self.textsHolderTransform.localScale = Vector3.one;
            }
            else
            {
                self.textsHolderTransform.localScale = Vector3.zero;
            }
        }

        private void TimeAttackComputerScript_Start(On.TimeAttackComputerScript.orig_Start orig, TimeAttackComputerScript self)
        {
            orig(self);
            var originalId = self.timeAttackLevelId;
            self.textsHolderTransform.localScale = Vector3.one;
            self.timeAttackLevelId = APPortalManager.GetRandomizedLevelId(self.timeAttackLevelId);
            self.levelImage.sprite = TimeTrialPortalTextureGet(self.timeAttackLevelId);
            self.MenuOptionsInit();
            self.menuIndex = 0;
            self.optionIndex = 0;
            self.backImage.color = self.baseImageColor;
            self.UpdateTexts();
            self.textsHolderTransform.localScale = new Vector3(1f, 0.0f, 1f);
            self.titleText.text =
                $"{self.menuTitles[self.menuIndex]}\n{Data.GetLevel(self.timeAttackLevelId).GetName()}";
            self.normalText.text = string.Empty;
            for (var index = 0; index < self.menuOptions[self.menuIndex].Count; ++index)
            {
                var normalText = self.normalText;
                normalText.text = $"{normalText.text}{self.menuOptions[self.menuIndex][index]}\n";
            }
        }

        public static Sprite TimeTrialPortalTextureGet(LevelId levelId)
        {
            if (levelId == LevelId.L20_PsychoTaxi)
            {
                var resource = AssetMaster.GetTexture2D("Cabinato Gigante Psycho Taxi Acceso");
                var sprite = Sprite.Create(resource, new Rect(248, 582, 107, 81), new Vector2(0.5f, 0.5f), 1);
                return sprite;
            }

            return Colors.PortalTextureGet(levelId);
        }

        private Data.TimeAttackReplayData Data_TimeAttackData_GetCurrent(On.Data.orig_TimeAttackData_GetCurrent orig, bool precedenceToDownloadData)
        {
            return null;
        }

        private void Data_TimeAttackData_SaveCurrent(On.Data.orig_TimeAttackData_SaveCurrent orig, Data.TimeAttackReplayData data)
        {
            // Do nothing
        }

        /// <summary>
        /// Reimplementation, don't save ghost data or submit to leaderboards.
        /// </summary>
        private IEnumerator GameplayMaster_TimeAttackEndCoroutine(On.GameplayMaster.orig_TimeAttackEndCoroutine orig, GameplayMaster self, bool isLocalHighscore)
        {
            GiudgementScript.Spawn(isLocalHighscore ? GiudgementScript.Kind.psychotic : GiudgementScript.Kind.mmm);
            while (GiudgementScript.instance != null)
                yield return null;
            var giveItAnotherGo = false;
            var component = UnityEngine.Object.Instantiate(AssetMaster.GetPrefab("Dialogue Time Attack Retry Negative")).GetComponent<DialogueScript>();
            component.askQuestion = true;
            component.onAnswerYes.AddListener(() => giveItAnotherGo = true);
            component.onAnswerNo.AddListener(() => giveItAnotherGo = false);
            while (DialogueScript.instance != null || MenuV2PopupScript.instance != null)
                yield return null;
            Tick.Paused = false;
            if (giveItAnotherGo)
                TransictionScript.SpawnOut(TransictionScript.Kind.horizontalFadeFromRight, null, Level.currentScene);
            else
                PortalScript.GoToLevel(Levels.GetHubIndex(), Data.GetHubLevelId());
        }

        private void GameplayMaster_TimeAttackEnd(On.GameplayMaster.orig_TimeAttackEnd orig, GameplayMaster self, bool victory)
        {
            if (victory)
            {
                Plugin.ArchipelagoClient.SendLocation((long)self.levelId * 1_00_00000);
            }

            orig(self, victory);
        }
    }
}
