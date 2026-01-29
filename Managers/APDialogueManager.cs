using System;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Models;
using UnityEngine;
using YellowTaxiAP.Archipelago;
using YellowTaxiAP.Behaviours;
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
            
            // Morio Dialogue Overrides
            On.PersonScenziato_FlipOWillUnlock.Awake += PersonScenziato_FlipOWillUnlock_Awake;
            On.PersonScenziatoV2.Update += PersonScenziatoV2_Update;
            On.PersonScenziatoV2.ChooseDialogue += PersonScenziatoV2_ChooseDialogue;
            On.DialogueScript.SpecialMethod_OnBeforeDialogueCapsuleImport_MorioSpikes1 += DialogueScript_SpecialMethod_OnBeforeDialogueCapsuleImport_MorioSpikes1;
        }

        private void DoggoLabDialogueTree(On.DialogueScript.orig_SpecialMethod_OnBeforeDialogueCapsuleImport_StuckDoggoTalk_StillInTheLab orig, DialogueScript self)
        {
            if (Plugin.ArchipelagoClient.AllClearedLocations.Contains(10_00007) || APSaveController.MiscSave.HasDoggo)
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
                        var font = CurrentFont;
                        self.dialogues =
                        [
                            $"Normally, I'd teach you how to <font=\"{font} Black\" material=\"{font} OrangeYellow\">boost</font> using your <font=\"{font} Black\" material=\"{font} OrangeYellow\">Flip O' Will</font>!",
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
                    case "DIALOGUE_PICI_COMPUTER_MAN_FLIP_ABORT": // Normally jump tutorial
                        if (!Plugin.SlotData.ShuffleFlipOWill || GameplayMaster.instance.levelId != Data.LevelId.Hub)
                            break;
                        self.dialogues =
                        [
                            GetMoveDialogue("Flip", APPlayerManager.JumpLevel > 0),
                            "Instead, here's an item from the multiworld!"
                        ];
                        moveRandoID = Identifiers.JUMP_ID;
                        break;
                    case "DIALOGUE_PICI_COMPUTER_MAN_BACKFLIP": // Normally backflip tutorial
                        if (!Plugin.SlotData.ShuffleFlipOWill || GameplayMaster.instance.levelId != Data.LevelId.Hub)
                            break;
                        self.dialogues =
                        [
                            GetMoveDialogue("Backflip", APPlayerManager.JumpLevel > 1),
                            "Instead, here's an item from the multiworld!"
                        ];
                        moveRandoID = Identifiers.BACKFLIP_ID;
                        Plugin.ArchipelagoClient.Win();
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
                            var ratItem = Plugin.ArchipelagoClient.ScoutedLocations[2_21_99999];
                            self.dialogues =
                            [
                                $"Michele handed you a particularly smelly {GetItemText(ratItem, true, false)}!!!"
                            ];
#if DEBUG
                            DebugLocationHelper.CheckLocation("Michele", "2_21_99999");
#endif
                            Plugin.ArchipelagoClient.SendLocation(2_21_99999);
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
                        if (!Plugin.SlotData.ShuffleDoggo)
                        {
                            APSaveController.MiscSave.HasDoggo = true;
                            break;
                        }

                        ScoutedItemInfo scoutedDoggo;
                        try
                        {
                            scoutedDoggo = Plugin.ArchipelagoClient.ScoutedLocations[10_00007];
                        }
                        catch (Exception ex)
                        {
                            Plugin.BepinLogger.LogWarning(ex);
                            Plugin.ArchipelagoClient.SendLocation(10_00007);
                            break;
                        }

                        self.dialogues =
                        [
                            "Woff woff woff! " + (APAreaStateManager.DoggoReceived ? "(Hey, you found my house keys!)" : "(Have you seen my house keys?)"),
                            $"Woff woff woff! (I looked where I last left them, but found this {GetItemText(scoutedDoggo, true, false)} instead!)",
                            "Woff woff woff! " + (APAreaStateManager.DoggoReceived ? "(You can have it as a reward! Go visit my home on Granny's Island!)" : "(I suppose you need it more than me! If you find my house keys meet me at my home on Granny's Island!)"),
                        ];

                        Plugin.ArchipelagoClient.SendLocation(10_00007);
                        break;
                    case "DIALOGUE_GRANNY_ISLAND_LAB_DOGGO_STUCK_AFTER_STILL_IN_LAB":
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
                    case "DIALOGUE_NARRATOR_DEMO_TRUE_WALL":
                        ScoutedItemInfo scoutedWall;
                        try
                        {
                            scoutedWall = Plugin.ArchipelagoClient.ScoutedLocations[10_00011];
                        }
                        catch (Exception ex)
                        {
                            Plugin.BepinLogger.LogWarning(ex);
                            Plugin.ArchipelagoClient.SendLocation(10_00011);
                            break;
                        }

                        self.dialogues =
                        [
                            APAreaStateManager.FullGameUnlocked
                                ? "Congratulations on reaching the full game!"
                                : self.dialogues[0],
                            APAreaStateManager.FullGameUnlocked
                                ? $"As a reward, here's {GetItemText(scoutedWall)}!"
                                : $"You are stuck here now, but as consolation you can have this {GetItemText(scoutedWall)}!",
                        ];
                        Plugin.ArchipelagoClient.SendLocation(10_00011);
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
                        var scoutItem = Plugin.ArchipelagoClient.ScoutedLocations[8_00000 + moveRandoID];
                        self.dialogues[self.dialogues.Length - 1] = $"Instead, I'll give you {GetItemText(scoutItem)}!";
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

        private string GetItemText(ScoutedItemInfo item, bool includePlayer = true, bool includePrefixes = true)
        {
            var font = CurrentFont;
            var material = "Yellow";
            if ((item.Flags & ItemFlags.Advancement) == ItemFlags.Advancement)
            {
                material = "GreenYellow";
            }
            else if ((item.Flags & ItemFlags.NeverExclude) == ItemFlags.NeverExclude)
            {
                material = "RedYellow";
            }
            else if ((item.Flags & ItemFlags.Trap) == ItemFlags.Trap)
            {
                material = "FullRed";
            }
            var itemText = $"<font=\"{font} Black\" material=\"{font} {material}\">{item.ItemDisplayName}</font>";
            if (includePlayer)
            {
                if (item.Player.Name.Equals(ArchipelagoClient.ServerData.SlotName))
                {
                    itemText = (includePrefixes ? "your " : string.Empty) + itemText;
                }
                else
                {
                    itemText = (includePrefixes ? "this " : string.Empty) + itemText +
                               $" for <font=\"{font} Black\" material=\"{font} OrangeYellow\">{item.Player.Name}</font>";
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
            var font = CurrentFont;
            var secondHalf = moveUnlocked
                ? "it appears you already know how."
                : "I appear to have lost the instructions.";
            var flipOwillText = string.Empty;
            if (!string.IsNullOrEmpty(flipOwillConnection))
            {
                flipOwillText =
                    $" {flipOwillConnection} <font=\"{font} Black\" material=\"{font} OrangeYellow\">Flip O' Will</font>";
            }
            return
                $"Beep boop! I was supposed to teach you how to <font=\"{font} Black\" material=\"{font} OrangeYellow\">{moveName}</font>{flipOwillText} but {secondHalf}";
        }
    }
}
