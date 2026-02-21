using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YellowTaxiAP.Archipelago;
using YellowTaxiAP.Behaviours;
using YellowTaxiAP.Helpers;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace YellowTaxiAP.Managers
{
    public class APDialogueManager
    {
        public static DyslexiaFontController dyslexiaFontControllerInstance;
        public APDialogueManager()
        {
            // Dyslexia Font Controller
            On.DyslexiaFontController.Awake += (orig, self) =>
            {
                dyslexiaFontControllerInstance = self;
                orig(self);
            };

            // DialogueScript hooks
            On.DialogueScript.Start += DialogueScript_Start;
            On.DialogueScript.SpecialMethod_OnDialogueEnd_ShowDoubleBoostPrompt += DialogueScript_OnShowDoubleBoostPrompt;
            On.DialogueScript.SpecialMethod_OnDialogueEnd_ShowJumpPrompt += DialogueScript_OnShowJumpPrompt;
            On.DialogueScript.SpecialMethod_OnDialogueEnd_ShowFlipPrompt += DialogueScript_OnShowFlipPrompt;
            On.DialogueScript.SpecialMethod_OnDialogueEnd_ShowBackflipPrompt += DialogueScript_OnShowBackflipPrompt;
            On.DialogueScript.SpecialMethod_OnDialogueEnd_ShowGlidePrompt += DialogueScript_OnShowGlidePrompt;
            On.DialogueScript.SpecialMethod_OnDialogueEnd_ShowQuickTurnPrompt += DialogueScript_OnShowQuickTurnPrompt;
            On.DialogueScript.SpecialMethod_OnBeforeDialogueCapsuleImport_StuckDoggoTalk_StillInTheLab += DoggoLabDialogueTree;
            On.DialogueScript.SpecialMethod_OnAnswerNo_AlienMoskGood_Question1 += DialogueScript_SpecialMethod_OnAnswerNo_AlienMoskGood_Question1;

            On.PersonParent.Awake += PersonParent_Awake;
            
            // Morio Dialogue Overrides
            On.PersonScenziato_FlipOWillUnlock.Awake += PersonScenziato_FlipOWillUnlock_Awake;
            On.PersonScenziatoV2.Update += PersonScenziatoV2_Update;
            On.PersonScenziatoV2.ChooseDialogue += PersonScenziatoV2_ChooseDialogue;
            On.DialogueScript.SpecialMethod_OnBeforeDialogueCapsuleImport_MorioSpikes1 += DialogueScript_SpecialMethod_OnBeforeDialogueCapsuleImport_MorioSpikes1;
        }

        private void DialogueScript_SpecialMethod_OnAnswerNo_AlienMoskGood_Question1(On.DialogueScript.orig_SpecialMethod_OnAnswerNo_AlienMoskGood_Question1 orig, DialogueScript self)
        {
            // Don't ask the moon follow-up question
            self.SpecialMethod_OnAnswerNo_AlienMoskGood_Question2();
        }

        private void PersonParent_Awake(On.PersonParent.orig_Awake orig, PersonParent self)
        {
            if (Plugin.SlotData.EarlyRat &&
                GameplayMaster.instance.levelId == Data.LevelId.Hub && self.myId == 512 &&
                (Plugin.SlotData.ShuffleRat
                    ? !Plugin.ArchipelagoClient.AllClearedLocations.Contains((int)Identifiers.NotableLocations.Michele)
                    : RatPersonScript.IsRatPickedUp()))
            {
                // Convert a lawyer into a rat, giving a rat location on bomboss goal
                var sourceRenderer = Resources.FindObjectsOfTypeAll<SkinnedMeshRenderer>()
                    .First(r => r.name.Equals("Rats 1"));

                var anims = Resources.FindObjectsOfTypeAll<AnimationClip>();
                var deadAnim = anims.First(o => o.name.Equals("Rat Dies"));
                var idleAnim = anims.First(o => o.name.Equals("Rat Idle.001"));
                var walkAnim = anims.First(o => o.name.Equals("Rat Walk"));
                // Rat talking and scared animations are not loaded in the hub. Make do.
                //var talkAnim = anims.First(o => o.name.Equals("Rat Talk"));
                var talkAnim = idleAnim;
                //var scaredAnim = anims.First(o => o.name.Equals("ArmaturaRat_Rat Bump"));
                var scaredAnim = idleAnim;
                self.transform.parent = null;
                self.gameObject.transform.position = new Vector3(675, 20, -95);
                self.dialoguePickup = AssetMaster.GetPrefab("Dialogue Rat Pickup Question");
                self.cannotDie = true;
                self.forceCannotRunAway = true;
                self.gameRelevantPerson = true;
                self.gameObject.AddComponent<RatPersonScript>();

                var animator = self.modelHolder.GetComponentInChildren<Animator>(true);
                var animatorController = animator.runtimeAnimatorController;
                var sourceAnimator = sourceRenderer.GetComponentInParent<Animator>(true);

                var newAnimator = Object.Instantiate(sourceAnimator, self.modelHolder);
                newAnimator.runtimeAnimatorController = animatorController;
                var animatorOverride = new AnimatorOverrideController(newAnimator.runtimeAnimatorController);
                
                var overrides = new List<KeyValuePair<AnimationClip, AnimationClip>>();
                animatorOverride.GetOverrides(overrides);
                for (var i = 0; i < animatorOverride.overridesCount; i++)
                {
                    var oldClip = overrides[i].Key;
                    var newClip = idleAnim;
                    if (oldClip.name.Contains("Talk"))
                        newClip = talkAnim;
                    else if (oldClip.name.Contains("Morte"))
                        newClip = deadAnim;
                    else if (oldClip.name.Contains("Spavento"))
                        newClip = scaredAnim;
                    else if (oldClip.name.Contains("Walk") || oldClip.name.Contains("Run"))
                        newClip = walkAnim;
                    overrides[i] = new KeyValuePair<AnimationClip, AnimationClip>(oldClip, newClip);
                }
                animatorOverride.ApplyOverrides(overrides);
                newAnimator.runtimeAnimatorController = animatorOverride;

                ObjectHelper.DestroyImmediateRecursive(animator.transform);
            }

            orig(self);
        }


        private void DoggoLabDialogueTree(On.DialogueScript.orig_SpecialMethod_OnBeforeDialogueCapsuleImport_StuckDoggoTalk_StillInTheLab orig, DialogueScript self)
        {
            if (Plugin.ArchipelagoClient.AllClearedLocations.Contains((long)Identifiers.NotableLocations.Doggo) || APSaveController.MiscSave.HasDoggo)
            {
                self.dialgoueCapsuleKey = "DIALOGUE_GRANNY_ISLAND_LAB_DOGGO_STUCK_AFTER_STILL_IN_LAB";
            }
        }

        private void DialogueScript_SpecialMethod_OnBeforeDialogueCapsuleImport_MorioSpikes1(On.DialogueScript.orig_SpecialMethod_OnBeforeDialogueCapsuleImport_MorioSpikes1 orig, DialogueScript self)
        {
            if (APCollectableManager.GoldenSpringActive)
                self.dialgoueCapsuleKey = "DIALOGUE_MORIO_LAB_SPIKES_ACCESS_POST_TOSLA";
            else
                self.dialgoueCapsuleKey = "DIALOGUE_MORIO_LAB_SPIKES_ACCESS_PRE_TOSLA";
        }

        /// <summary>
        /// Normally, Morio will hide the initial gears until he's given the first one.
        /// This isn't very fun in a multiworld context, and results in only one check prior to an extremely early BK unless single coinsanity is on.
        ///
        /// Remove the initialgears list to ensure all these gears remain active
        /// </summary>
        private void PersonScenziatoV2_Update(On.PersonScenziatoV2.orig_Update orig, PersonScenziatoV2 self)
        {
            self.initialGears = [];
            orig(self);
        }

        /// <summary>
        /// Normally, Morio will not give you your first gear if you already have gears.
        /// This is bad in a multiworld context.
        ///
        /// Better solution later, but for now just always give the gear
        /// </summary>
        private void PersonScenziatoV2_ChooseDialogue(On.PersonScenziatoV2.orig_ChooseDialogue orig, PersonScenziatoV2 self)
        {
            // TODO: Only set this if the gear location hasn't been checked, otherwise run normal dialogue
            self.dialoguePickup = self.dialogue_initialNoGears;
        }

        public string CurrentFont
        {
            get
            {
                try
                {
                    var s = Settings.dyslexiaFontEnabled
                        ? dyslexiaFontControllerInstance.dyslexicFont.name
                        : dyslexiaFontControllerInstance.initialFont.name;
                    return s.Substring(0, s.IndexOf(" Black", StringComparison.Ordinal));
                }
                catch
                {
                    return "Psycho Taxi 1 SDF";
                }
            }
        }

        /// <summary>
        /// Keeps the Morio Flip O' Will tutorial in Morio's Island, rather than allowing it to despawn after Flip O' Will is unlocked.
        /// </summary>
        private void PersonScenziato_FlipOWillUnlock_Awake(On.PersonScenziato_FlipOWillUnlock.orig_Awake orig, PersonScenziato_FlipOWillUnlock self)
        {
            PersonScenziato_FlipOWillUnlock.hasSeenAPlayerDeath = true; // Normally this variable is used to ensure despawning doesn't happen after a death
            orig(self);
        }

        private void DialogueScript_OnShowJumpPrompt(On.DialogueScript.orig_SpecialMethod_OnDialogueEnd_ShowJumpPrompt orig, DialogueScript self)
        {
            if (!Plugin.SlotData.ShuffleFlipOWill)
            {
                orig(self);
            }
        }

        private void DialogueScript_OnShowQuickTurnPrompt(On.DialogueScript.orig_SpecialMethod_OnDialogueEnd_ShowQuickTurnPrompt orig, DialogueScript self)
        {
            if (!Plugin.SlotData.ShuffleFlipOWill)
            {
                orig(self);
            }
        }

        private void DialogueScript_OnShowGlidePrompt(On.DialogueScript.orig_SpecialMethod_OnDialogueEnd_ShowGlidePrompt orig, DialogueScript self)
        {
            if (!Plugin.SlotData.ShuffleGlide)
            {
                orig(self);
            }
        }

        private void DialogueScript_OnShowFlipPrompt(On.DialogueScript.orig_SpecialMethod_OnDialogueEnd_ShowFlipPrompt orig, DialogueScript self)
        {
            if (!Plugin.SlotData.ShuffleFlipOWill)
            {
                orig(self);
            }
        }

        private void DialogueScript_OnShowDoubleBoostPrompt(On.DialogueScript.orig_SpecialMethod_OnDialogueEnd_ShowDoubleBoostPrompt orig, DialogueScript self)
        {
            if (!Plugin.SlotData.ShuffleFlipOWill)
            {
                orig(self);
            }
        }

        private void DialogueScript_OnShowBackflipPrompt(On.DialogueScript.orig_SpecialMethod_OnDialogueEnd_ShowBackflipPrompt orig, DialogueScript self)
        {
            if (!Plugin.SlotData.ShuffleFlipOWill)
            {
                orig(self);
            }
        }

        private void DialogueScript_Start(On.DialogueScript.orig_Start orig, DialogueScript self)
        {
            var dialogueCapsule = !DialogueCapsule.dictionary.ContainsKey(self.dialgoueCapsuleKey)
                ? DialogueCapsule.dictionary["DEFAULT"]
                : DialogueCapsule.dictionary[self.dialgoueCapsuleKey.ToUpper()];
            if (dialogueCapsule != null)
            {
                Plugin.Log($"Initiating dialogue with key: {dialogueCapsule.key}  {self.textSoundNames[0]}");

                var moveRandoID = -1;
                switch (dialogueCapsule.key)
                {
                    case "DISCLAIMER_CASUAL_REFERENCES":
                        self.names[0] = "AP Lawyer";
                        self.dialogues[0] = Random.Range(0, 2) == 1 ? "&legalrom" : "&eyepatch";
                        break;
                    case "DIALOGUE_MORIO_MORIOS_ISLAND_FLIP_O_WILL_UNLOCK": // Normally unlocks Flip O' Will
                        if (!Plugin.SlotData.ShuffleFlipOWill)
                            break;
                        var morioFont = CurrentFont;
                        self.dialogues =
                        [
                            $"Normally, I'd teach you how to {SetTextColor("boost", DialogueColors.OrangeYellow)} using your {SetTextColor("Flip O' Will", DialogueColors.OrangeYellow)}!",
                            APPlayerManager.BoostLevel > 0
                                ? "It seems that's unnecessary, since you already know how."
                                : "Unfortunately, I've forgotten how to do that...",
                            "Instead, here's an item from the multiworld!"
                        ];
                        moveRandoID = Identifiers.BOOST_ID;
                        break;
                    case "DIALOGUE_PICI_COMPUTER_MAN_DOUBLE_DASH": // Normally super boost tutorial
                        if (!Plugin.SlotData.ShuffleFlipOWill || GameplayMaster.instance.levelId != Data.LevelId.Hub)
                            break;
                        self.dialogues =
                        [
                            GetMoveDialogue("Super Boost", APPlayerManager.BoostLevel > 1),
                            "Instead, here's an item from the multiworld!"
                        ];
                        moveRandoID = Identifiers.SUPERBOOST_ID;
                        break;
                    case "DIALOGUE_PICI_COMPUTER_MAN_FLIP_ABORT" when GameplayMaster.instance.levelId == Data.LevelId.Hub: // Normally jump tutorial
                        if (!Plugin.SlotData.ShuffleFlipOWill || GameplayMaster.instance.levelId != Data.LevelId.Hub)
                            break;
                        self.dialogues =
                        [
                            GetMoveDialogue("Flip", APPlayerManager.JumpLevel > 0),
                            "Instead, here's an item from the multiworld!"
                        ];
                        moveRandoID = Identifiers.JUMP_ID;
                        break;
                    case "DIALOGUE_PICI_COMPUTER_MAN_FLIP_ABORT" when GameplayMaster.instance.levelId == Data.LevelId.L6_Gym:
                    case "DIALOGUE_PICI_COMPUTER_MAN_BACKFLIP" when GameplayMaster.instance.levelId == Data.LevelId.Hub: // Normally backflip tutorial
                        if (!Plugin.SlotData.ShuffleFlipOWill)
                            break;
                        // Gym Gears backflip location is only for early goals
                        if (GameplayMaster.instance.levelId == Data.LevelId.L6_Gym && !Plugin.SlotData.EarlyBackflip)
                            break;
                        self.dialogues =
                        [
                            GetMoveDialogue("Backflip", APPlayerManager.JumpLevel > 1),
                            "Instead, here's an item from the multiworld!"
                        ];
                        moveRandoID = Identifiers.BACKFLIP_ID;
                        break;
                    case "DIALOGUE_PICI_COMPUTER_MAN_DOUBLE_TAP_GLIDE": // Normally glide tutorial
                        if (!Plugin.SlotData.ShuffleGlide || GameplayMaster.instance.levelId != Data.LevelId.Hub)
                            break;
                        self.dialogues =
                        [
                            GetMoveDialogue("Glide", APPlayerManager.GlideEnabled, null),
                            "Instead, here's an item from the multiworld!"
                        ];
                        moveRandoID = Identifiers.GLIDE_ID;
                        break;
                    case "DIALOGUE_PICI_COMPUTER_MAN_QUICK_TURN": // Normally quick turn tutorial. Repurposed for Spin Attack
                        if (!Plugin.SlotData.ShuffleFlipOWill || GameplayMaster.instance.levelId != Data.LevelId.Hub)
                            break;
                        self.dialogues =
                        [
                            GetMoveDialogue("Attack", APPlayerManager.SpinAttackEnabled),
                            "Instead, here's an item from the multiworld!"
                        ];
                        moveRandoID = Identifiers.SPIN_ID;
                        break;
                    case "DIALOGUE_RAT_PICKUP_QUESTION":
                        if (!Plugin.SlotData.ShuffleRat)
                            break;

                        self.dialogues =
                        [
                            APRatManager.ReceivedRatItem
                                ? "Squit squit! Who's your handsome friend?!?"
                                : "Squit squit! I was looking for cheese, but found a check!!!",
                            "Would you like this shiny thing I found?!?"
                        ];
                        break;
                    case "DIALOGUE_RAT_PICKUP_ANWER_YES":
                        if (!Plugin.SlotData.ShuffleRat)
                            break;
                        try
                        {
                            self.dialogues =
                            [
                                $"Michele handed you a particularly smelly {GetItemText((int)Identifiers.NotableLocations.Michele, true, false)} before scurrying back to the sewers!"
                            ];
#if DEBUG
                            DebugLocationHelper.CheckLocation("Michele", "2_21_99999");
#endif
                            Plugin.ArchipelagoClient.SendLocation((int)Identifiers.NotableLocations.Michele);
                        }
                        catch (Exception ex)
                        {
                            Plugin.BepinLogger.LogWarning(ex);
                        }
                        break;
                    case "DIALOGUE_RAT_PICKUP_ANWER_NO":
                        if (!Plugin.SlotData.ShuffleRat)
                            break;
                        self.dialogues =
                        [
                            "You monster!!! That could've been someone's unwall!!!"
                        ];
                        break;
                    case "DIALOGUE_MOON_END":
                        Plugin.ArchipelagoClient.Win();
                        break;
                    case "DIALOGUE_GRANNY_ISLAND_LAB_DOGGO_STUCK":
                        if (Plugin.SlotData.EarlyDoggo)
                        {
                            self.dialogues =
                            [
                                "Woff woff woff! (You're not supposed to be able to get here!)",
                                "Woff woff woff! (You should consider turning up your expert level!)"
                            ];
                            break;
                        }
                        if (!Plugin.SlotData.ShuffleDoggo)
                        {
                            APSaveController.MiscSave.HasDoggo = true;
                            break;
                        }

                        self.dialogues =
                        [
                            "Woff woff woff! " + (APAreaStateManager.DoggoReceived ? "(Hey, you found my house keys!)" : "(Have you seen my house keys?)"),
                            $"Woff woff woff! (I looked where I last left them, but found this {GetItemText((int)Identifiers.NotableLocations.Doggo, true, false)} instead!)",
                            "Woff woff woff! " + (APAreaStateManager.DoggoReceived ? "(You can have it as a reward! Go visit my home on Granny's Island!)" : "(I suppose you need it more than me! If you find my house keys meet me at my home on Granny's Island!)"),
                        ];

                        Plugin.ArchipelagoClient.SendLocation((int)Identifiers.NotableLocations.Doggo);
                        break;
                    case "DIALOGUE_GRANNY_ISLAND_LAB_DOGGO_STUCK_AFTER_STILL_IN_LAB":
                        if (Plugin.SlotData.EarlyDoggo)
                        {
                            self.dialogues =
                            [
                                "Woff woff woff! (You're not supposed to be able to get here!)",
                                "Woff woff woff! (You should consider turning up your expert level!)"
                            ];
                            break;
                        }
                        if (APAreaStateManager.DoggoReceived)
                        {
                            break;
                        }

                        self.dialogues =
                        [
                            "Woff woff woff! (Still no luck finding my house keys?)",
                            "Woff woff woff! (If you find them, please meet me at my home on Granny's Island!)"
                        ];

                        break;
                    case "DIALOGUE_GRANNY_ISLAND_LAB_DOGGO_STUCK_AFTER":
                        if (!Plugin.SlotData.EarlyDoggo || !Plugin.SlotData.ShuffleDoggo)
                            break;

                        if (!Plugin.ArchipelagoClient.AllClearedLocations.Contains(
                                (long)Identifiers.NotableLocations.Doggo))
                        {
                            self.textSoundNames =
                            [
                                self.textSoundNames[0],
                                self.textSoundNames[0],
                                self.textSoundNames[0],
                            ];
                            self.names =
                            [
                                self.names[0],
                                self.names[0],
                                self.names[0],
                            ];
                            self.dialogues =
                            [
                                "Woff woff woff! " + (APAreaStateManager.DoggoReceived ? "(Hey, you found my house keys!)" : "(Have you seen my house keys?)"),
                                $"Woff woff woff! (I looked where I last left them, but found this {GetItemText((int)Identifiers.NotableLocations.Doggo, true, false)} instead!)",
                                "Woff woff woff! " + (APAreaStateManager.DoggoReceived ? "(You can have it as a reward! Please, feel free to visit my home!)" : "(I suppose you need it more than me! If you find my house keys meet me back here at my home!)"),
                            ];
                            Plugin.ArchipelagoClient.SendLocation((int)Identifiers.NotableLocations.PizzaKing);
                        }
                        else
                        {
                            self.dialogues =
                            [
                                "Woff woff woff! " + (APAreaStateManager.DoggoReceived ? "(Thanks for finding my house keys!)" : "(No luck finding my house keys?)"),
                                "Woff woff woff! " + (APAreaStateManager.DoggoReceived ? "(Please, feel free to visit my home!)" : "(If you find my house keys meet me back here at my home!)"),
                            ];
                        }

                        break;
                    case "DIALOGUE_NARRATOR_DEMO_TRUE_WALL":
                        if (Plugin.ArchipelagoClient.AllClearedLocations.Contains((long)Identifiers.NotableLocations.DemoWall))
                            break;
                        self.dialogues =
                        [
                            APAreaStateManager.FullGameUnlocked
                                ? "Congratulations on reaching the full game!"
                                : self.dialogues[0],
                            APAreaStateManager.FullGameUnlocked
                                ? $"As a reward, here's {GetItemText((long)Identifiers.NotableLocations.DemoWall)}!"
                                : $"You are stuck here now, but as consolation you can have this {GetItemText((long)Identifiers.NotableLocations.DemoWall)}!",
                        ];
                        Plugin.ArchipelagoClient.SendLocation((long)Identifiers.NotableLocations.DemoWall);
                        break;
                    case "DIALOGUE_GRANNY_ISLAND_NICK_JUST_TALK":
                        if (APPlayerManager.BoostLevel < 2 && APPlayerManager.JumpLevel < 2)
                            self.dialogues[1] = "With your moveset it must have been tricky!";

                        if (!Plugin.SlotData.EarlyGoldenPropeller || !Plugin.SlotData.ShuffleGoldenPropeller)
                            break;
                        self.textSoundNames =
                        [
                            self.textSoundNames[0],
                            self.textSoundNames[0],
                            self.textSoundNames[0],
                        ];
                        self.names =
                        [
                            self.names[0],
                            self.names[0],
                            self.names[0],
                        ];
                        self.dialogues =
                        [
                            APPlayerManager.BoostLevel >= 2 && APPlayerManager.JumpLevel >= 2 ?
                                "Hehe, congrats for getting up here! With your moveset there is no place you cannot reach!" :
                                "Hehe, congrats for getting up here! With your moveset it must have been tricky!",
                            APCollectableManager.GoldenPropellerActive ?
                                $"That {SetTextColor("Golden Propeller", DialogueColors.OrangeYellow)} certainly helps!" :
                                $"A {SetTextColor("Golden Propeller", DialogueColors.OrangeYellow)} could help you reach greater heights!",
                        ];
                        if (!Plugin.ArchipelagoClient.AllClearedLocations.Contains((long) Identifiers.NotableLocations
                                .GoldenPropeller))
                        {
                            self.dialogues =
                            [
                                self.dialogues[0],
                                self.dialogues[1],
                                $"For managing to get up here{(APCollectableManager.GoldenPropellerActive ? string.Empty : " without one")}, here's {GetItemText((long) Identifiers.NotableLocations.GoldenPropeller)}!"
                            ];
                            Plugin.ArchipelagoClient.SendLocation((long)Identifiers.NotableLocations.GoldenPropeller);
                        }
                        break;
                    case "DIALOGUE_GRANNY_ISLAND_ALIEN_MOSK_QEUSTION_1":
                        // Don't actually ask a question, don't want Mosk to take you anywhere directly
                        self.askQuestion = false;
                        self.askingQuestion = false;
                        if (!Plugin.ArchipelagoClient.AllClearedLocations.Contains((long)Identifiers.NotableLocations.MosksRocket))
                        {
                            self.dialogues =
                                APAreaStateManager.RocketEnabled ?
                                    [
                                        "I know I shouldn't be here yet, but... hey those are the keys to my rocket!",
                                        $"As a reward, you can have {GetItemText((long)Identifiers.NotableLocations.MosksRocket)}!",
                                        "If you want to check out my rocket, use the front door!",
                                    ] :
                                    [
                                        "I know I shouldn't be here yet, but I lost the keys to my rocket. Keep an eye out for them!",
                                        $"All I found instead was {GetItemText((long)Identifiers.NotableLocations.MosksRocket)}, but you can have it.",
                                    ];
                            Plugin.ArchipelagoClient.SendLocation((long)Identifiers.NotableLocations.MosksRocket);
                        }
                        else
                        {
                            self.dialogues =
                                APAreaStateManager.RocketEnabled
                                    ?
                                    [
                                        "If you want to check out my rocket, use the front door!",
                                    ]
                                    :
                                    [
                                        "Shouldn't you be trying to thwart me right now?",
                                    ];
                        }
                        break;
                    case "DIALOGUE_GRANNY_ISLAND_PIZZA_KING_JUST_TALK":
                        if (!Plugin.SlotData.EarlyPizzaKing || !Plugin.SlotData.ShufflePizzaKing)
                            break;
                        if (!Plugin.ArchipelagoClient.AllClearedLocations.Contains(
                                (long) Identifiers.NotableLocations.PizzaKing))
                        {
                            self.dialogues = APAreaStateManager.PizzaKingReceived ?
                            [
                                "Hello friendly knight! Those keys you have... those are my vacation home keys!",
                                $"As a reward for finding them, take {GetItemText((long)Identifiers.NotableLocations.PizzaKing)}!",
                            ]:
                            [
                                "Hello friendly knight! Have you seen my vacation home keys anywhere? I appear to have misplaced them.",
                                $"All I found instead is {GetItemText((long)Identifiers.NotableLocations.PizzaKing)}, but you can have it!",
                            ];
                            Plugin.ArchipelagoClient.SendLocation((int)Identifiers.NotableLocations.PizzaKing);
                        }
                        else
                        {
                            self.dialogues = APAreaStateManager.PizzaKingReceived ?
                            [
                                "Hello friendly knight! Thank you for finding my keys!",
                                self.dialogues[1]
                            ] :
                            [
                                "Hello friendly knight! Any luck finding my vacation home keys?",
                            ];
                        }
                        break;
                    case "PSYCHO_TAXI_CABINET_NO_UNLOCK":
                        if (!Plugin.SlotData.EarlyPsychoTaxi || !Plugin.SlotData.ShufflePsychoTaxi)
                            break;

                        if (!Plugin.ArchipelagoClient.AllClearedLocations.Contains(
                                (long) Identifiers.NotableLocations.PsychoTaxi))
                        {
                            self.textSoundNames =
                            [
                                self.textSoundNames[0],
                                self.textSoundNames[0],
                            ];
                            self.names =
                            [
                                self.names[0],
                                self.names[0],
                            ];
                            self.dialogues =
                            [
                                self.dialogues[0],
                                $"What's this? You found {GetItemText((long) Identifiers.NotableLocations.PsychoTaxi, true, false)} wedged in the cartridge slot!",
                            ];
                            Plugin.ArchipelagoClient.SendLocation((long)Identifiers.NotableLocations.PsychoTaxi);
                        }
                        break;
                    case "PSYCHO_TAXI_CABINET_PLAY_QUESTION":
                        if (!Plugin.SlotData.EarlyPsychoTaxi || !Plugin.SlotData.ShufflePsychoTaxi)
                            break;

                        if (!Plugin.ArchipelagoClient.AllClearedLocations.Contains(
                                (long)Identifiers.NotableLocations.PsychoTaxi))
                        {
                            self.textSoundNames =
                            [
                                self.textSoundNames[0],
                                self.textSoundNames[0],
                            ];
                            self.names =
                            [
                                self.names[0],
                                self.names[0],
                            ];
                            self.dialogues =
                            [
                                $"What's this? You found {GetItemText((long) Identifiers.NotableLocations.PsychoTaxi, true, false)} next to the machine!",
                                self.dialogues[0],
                            ];
                            Plugin.ArchipelagoClient.SendLocation((long)Identifiers.NotableLocations.PsychoTaxi);
                        }
                        break;
                    case "DIALOGUE_MORIO_LAB_SECRET_BEDROOM":
                        if (!Plugin.SlotData.EarlyMoriosPassword || Plugin.ArchipelagoClient.AllClearedLocations.Contains((int) Identifiers.NotableLocations.MoriosPassword))
                            break;

                        self.dialogues[1] =
                            $"And take {GetItemText((int) Identifiers.NotableLocations.MoriosPassword)} with you!";
                        Plugin.ArchipelagoClient.SendLocation((int)Identifiers.NotableLocations.MoriosPassword);

                        break;
                    case "DIALOGUE_GRANNY_ISLAND_OCRA_TAXI_MINIGAME_2":
                        if (!Plugin.SlotData.EarlyOrangeSwitch ||
                            Plugin.ArchipelagoClient.AllClearedLocations.Contains((int) Identifiers.NotableLocations.OrangeSwitch))
                            break;
                        self.dialogues[1] =
                            $"As a reward, please take {GetItemText((int) Identifiers.NotableLocations.OrangeSwitch)}!";
                        Plugin.ArchipelagoClient.SendLocation((int)Identifiers.NotableLocations.OrangeSwitch);
                        break;
                    case "DIALOGUE_MORIO_DREAM_MACHINE_INACTIVE":
                        // TODO: If Morio's Mind is a level, dialogue should probably be tweaked here somewhat anyway
                        if (!Plugin.SlotData.OverworldMoriosPassword)
                            break;

                        Plugin.BepinLogger.LogWarning(self.dialogues[1]);
                        self.dialogues =
                        [
                            self.dialogues[0],
                            APAreaStateManager.MindPasswordReceived ? $"Looks like you already found the {SetTextColor("password", DialogueColors.OrangeYellow)} to this safety door!" : self.dialogues[1],
                            $"On an unrelated note, this machine will give you {GetItemText((int) Identifiers.NotableLocations.MoriosPassword)}!",
                            $"I hope it's worth it, because this is quite painful!",
                        ];

                        break;
                    case "DIALOGUE_MORIO_DREAM_MACHINE_ACTIVE_AFTER_PASSWORD":
                        if (!Plugin.SlotData.OverworldMoriosPassword)
                            break;

                        self.dialogues = [
                            self.dialogues[0],
                        ];
                        break;
                    case "DIALOGUE_GRANNY_ISLAND_GELATAIO_THANKS":
                        if (!Plugin.SlotData.EarlyGelaToni)
                            break;

                        if (!Plugin.ArchipelagoClient.AllClearedLocations.Contains(
                                (long) Identifiers.NotableLocations.GelaToni))
                        {
                            self.dialogues = APAreaStateManager.GelaToniReceived ?
                                [
                                    self.dialogues[0],
                                    $"As a reward, you can have {GetItemText((long) Identifiers.NotableLocations.GelaToni)}!",
                                ] :
                                [
                                    "Hey hey! Have you seen an ice cream truck around here?",
                                    $"I thought I parked it here, but found {GetItemText((long) Identifiers.NotableLocations.GelaToni)} in its place! It's yours if you want!",
                                ];
                            Plugin.ArchipelagoClient.SendLocation((long)Identifiers.NotableLocations.GelaToni);
                        }
                        else if (!APAreaStateManager.GelaToniReceived)
                        {
                            self.dialogues = 
                                [
                                    "Hey hey! Have you been keeping an eye out for my ice cream truck?",
                                ];
                        }
                        break;
#if DEBUG
                        case "NARRATOR_BACK_TO_HUB_QUESTION":
                        case "DIALOGUE_NARRATOR_BACK_TO_HUB_QUESTION_LAB_ALT":
                            break;
                        default:
                            GUIUtility.systemCopyBuffer = dialogueCapsule.key;
                            break;
#endif
                        }

                if (moveRandoID > 0)
                {
                    try
                    {
                        self.dialogues[self.dialogues.Length - 1] = $"Instead, I'll give you {GetItemText(8_00000 + moveRandoID)}!";
                        Plugin.ArchipelagoClient.SendLocation(800000 + moveRandoID);
                    }
                    catch(Exception ex)
                    {
                        Plugin.BepinLogger.LogWarning(ex);
                    }
#if DEBUG
                    DebugLocationHelper.CheckLocation("Move Rando", $"0_{Identifiers.NPC_ID:D2}_{moveRandoID:D5}");
#endif
                }
            }
            orig(self);
        }

        private enum DialogueColors
        {
            Black,
            Yellow,
            GreenYellow,
            OrangeYellow,
            RedYellow,
            FullRed,
        }

        private string SetTextColor(string text, DialogueColors color)
        {
            var font = CurrentFont;
            return $"<font=\"{font} Black\" material=\"{font} {color}\">{text}</font>";
        }

        private string GetItemText(long itemId, bool includePlayer = true, bool includePrefixes = true)
        {
            ScoutedItemInfo item;
            try
            {
                item = Plugin.ArchipelagoClient.ScoutedLocations[itemId];
            }
            catch (Exception ex)
            {

                Plugin.BepinLogger.LogWarning(ex);
                return includePrefixes ? "an item from the multiworld" : "item from the multiworld";
            }

            var material = DialogueColors.Yellow;
            if ((item.Flags & ItemFlags.Advancement) == ItemFlags.Advancement)
            {
                material = DialogueColors.GreenYellow;
            }
            else if ((item.Flags & ItemFlags.NeverExclude) == ItemFlags.NeverExclude)
            {
                material = DialogueColors.RedYellow;
            }
            else if ((item.Flags & ItemFlags.Trap) == ItemFlags.Trap)
            {
                material = DialogueColors.FullRed;
            }
            var itemText = SetTextColor(item.ItemDisplayName, material);
            if (includePlayer)
            {
                if (item.Player.Name.Equals(ArchipelagoClient.ServerData.SlotName))
                {
                    itemText = (includePrefixes ? "your " : string.Empty) + itemText;
                }
                else
                {
                    itemText = (includePrefixes ? "this " : string.Empty) + itemText +
                               $" for {SetTextColor(item.Player.Name, DialogueColors.OrangeYellow)}";
                }
            }
            else if (includePrefixes)
            {
                itemText = "this " + itemText;
            }

            return itemText;
        }

        private string GetMoveDialogue(string moveName, bool moveUnlocked, string flipOwillConnection = "using your")
        {
            var secondHalf = moveUnlocked
                ? "it appears you already know how."
                : "I appear to have lost the instructions.";
            var flipOwillText = string.Empty;
            if (!string.IsNullOrEmpty(flipOwillConnection))
            {
                flipOwillText =
                    $" {flipOwillConnection} {SetTextColor("Flip O' Will", DialogueColors.OrangeYellow)}";
            }
            return
                $"Beep boop! I was supposed to teach you how to {SetTextColor(moveName, DialogueColors.OrangeYellow)}{flipOwillText} but {secondHalf}";
        }
    }
}
