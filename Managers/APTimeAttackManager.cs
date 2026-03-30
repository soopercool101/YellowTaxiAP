using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using I2.Loc;
using UnityEngine;
using UnityEngine.Events;

namespace YellowTaxiAP.Managers
{
    public class APTimeAttackManager
    {
        public APTimeAttackManager()
        {
            On.GameplayMaster.TimeAttackEnd += GameplayMaster_TimeAttackEnd;
            On.GameplayMaster.TimeAttackEndCoroutine += GameplayMaster_TimeAttackEndCoroutine;

            On.Data.TimeAttackData_GetCurrent += Data_TimeAttackData_GetCurrent;
            On.Data.TimeAttackData_SaveCurrent += Data_TimeAttackData_SaveCurrent;
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
