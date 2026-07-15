using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Models;
using Steamworks;
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
            On.PersonPizzaCheff.Awake += PersonPizzaCheff_Awake;
            On.PersonParent.JustTalkDefaultCoroutine += PersonParent_JustTalkDefaultCoroutine;
            
            // Morio Dialogue Overrides
            On.PersonScenziato_FlipOWillUnlock.Awake += PersonScenziato_FlipOWillUnlock_Awake;
            On.PersonScenziatoV2.Update += PersonScenziatoV2_Update;
            On.PersonScenziatoV2.ChooseDialogue += PersonScenziatoV2_ChooseDialogue;
            On.DialogueScript.SpecialMethod_OnBeforeDialogueCapsuleImport_MorioSpikes1 += DialogueScript_SpecialMethod_OnBeforeDialogueCapsuleImport_MorioSpikes1;
            On.DialogueScript.SpecialMethod_OnCapsuleImport_AtToslaHqPortalMorio += DialogueScript_SpecialMethod_OnCapsuleImport_AtToslaHqPortalMorio;
        }

        private void DialogueScript_SpecialMethod_OnCapsuleImport_AtToslaHqPortalMorio(On.DialogueScript.orig_SpecialMethod_OnCapsuleImport_AtToslaHqPortalMorio orig, DialogueScript self)
        {
            // TODO: Implement the remaining dialogue when adding final portal
            self.dialgoueCapsuleKey = "DIALOGUE_MORIO_AT_TOSLA_HQ_PORTAL_LOCKED";
        }

        public static bool IsCloningPizzaMan = false;
        private void PersonPizzaCheff_Awake(On.PersonPizzaCheff.orig_Awake orig, PersonPizzaCheff self)
        {
            if (GameplayMaster.instance.levelId == Data.LevelId.Hub && (self.myId == 10000 || IsCloningPizzaMan))
            {
                // Need to blank out the pepperoni list for the clone or the minigame will fail to work on the original
                self.myPepperonies = [];
            }
            orig(self);
            if (GameplayMaster.instance.levelId == Data.LevelId.Hub)
            {
                if (self.myId == 10000 || IsCloningPizzaMan)
                {
                    Plugin.Log("Finalizing Pizza Man");
                    self.pickupCorotuineOverride = null;
                    self.modelHolder.GetChild(2).gameObject.SetActive(false);
                }
                else if (Plugin.SlotData.EarlyPizzaWheels)
                {
                    var newZoneMaster = Object
                        .FindObjectsByType<ZoneMaster>(FindObjectsInactive.Include, FindObjectsSortMode.None)
                        .First(o => o.name.Equals("ZM   X: -4   Z: 5")).transform;
                    if (Plugin.SlotData.EarlyPizzaKing)
                    {
                        // If the pizza king is accessible, then we need to clone the pizza man
                        Plugin.Log("Cloning Pizza Man");
                        IsCloningPizzaMan = true; // Make sure the cloning process is executed once, clone shouldn't create more clones!
                        var newPizzaMan = Object.Instantiate(self, newZoneMaster);
                        newPizzaMan.myId = 10000;
                        newPizzaMan.transform.localPosition = new Vector3(-80, 50, 10);
                        IsCloningPizzaMan = false;
                    }
                    else
                    {
                        // If the pizza king is not accessible, we can move the pizza man
                        self.transform.parent = newZoneMaster;
                        self.transform.localPosition = new Vector3(-80, 50, 10);
                        self.pickupCorotuineOverride = null;
                        self.modelHolder.GetChild(2).gameObject.SetActive(false);
                    }
                }
            }
        }

        private System.Collections.IEnumerator PersonParent_JustTalkDefaultCoroutine(On.PersonParent.orig_JustTalkDefaultCoroutine orig, PersonParent self)
        {
            Plugin.Log($"Talking to {self.gameObject.name} (ID: {self.myId})");
            return orig(self);
        }

        private void DialogueScript_SpecialMethod_OnAnswerNo_AlienMoskGood_Question1(On.DialogueScript.orig_SpecialMethod_OnAnswerNo_AlienMoskGood_Question1 orig, DialogueScript self)
        {
            // Don't ask the moon follow-up question
            self.SpecialMethod_OnAnswerNo_AlienMoskGood_Question2();
        }

        /// <summary>
        /// Turns a lawyer into a rat
        /// </summary>
        /// <param name="original"></param>
        private void Ratify(PersonParent original)
        {
            Plugin.Log($"Beginning Ratification process on {original.gameObject.name}");
            // Convert a lawyer into a rat, giving a rat location on bomboss goal
            var sourceRenderer = Resources.FindObjectsOfTypeAll<SkinnedMeshRenderer>()
                .First(r => r.name.Equals("Rats 1"));

            var anims = Resources.FindObjectsOfTypeAll<AnimationClip>();
            var deadAnim = anims.First(o => o.name.Equals("Rat Dies"));
            var idleAnim = anims.First(o => o.name.Equals("Rat Idle.001"));
            var walkAnim = anims.First(o => o.name.Equals("Rat Walk"));
            // Rat talking and scared animations are not loaded outside of Pizza Time. Make do.
            //var talkAnim = anims.First(o => o.name.Equals("Rat Talk"));
            var talkAnim = idleAnim;
            //var scaredAnim = anims.First(o => o.name.Equals("ArmaturaRat_Rat Bump"));
            var scaredAnim = idleAnim;
            original.gameObject.AddComponent<RatPersonScript>();
            original.dialoguePickup = AssetMaster.GetPrefab("Dialogue Rat Pickup Question");
            original.cannotDie = true;
            original.forceCannotRunAway = true;
            original.gameRelevantPerson = true;
            var animator = original.modelHolder.GetComponentInChildren<Animator>(true);
            var animatorController = animator.runtimeAnimatorController;
            var sourceAnimator = sourceRenderer.GetComponentInParent<Animator>(true);

            var newAnimator = Object.Instantiate(sourceAnimator, original.modelHolder);
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
            Plugin.Log("Ratification completed");
        }

        private void PersonParent_Awake(On.PersonParent.orig_Awake orig, PersonParent self)
        {
            if (Plugin.SlotData.EarlyRat &&
                GameplayMaster.instance.levelId == Data.LevelId.Hub && self.myId == 512 &&
                (Plugin.SlotData.ShuffleRat
                    ? !Plugin.ArchipelagoClient.AllClearedLocations.Contains((int)Identifiers.NotableLocations.HubMichele)
                    : !RatPersonScript.IsRatPickedUp()))
            {
                Ratify(self);
                self.transform.parent = Object.FindObjectsByType<ZoneMaster>(FindObjectsInactive.Include, FindObjectsSortMode.None).First(o => o.gameObject.name.Equals("ZM   X: 4   Z: -1")).transform;
                self.transform.position = new Vector3(675, 20, -95);
            }
            else if (Plugin.SlotData.FlushedAwayUnlockCondition == YTGVSlotData.LevelUnlockCondition.Item &&
                     GameplayMaster.instance.levelId == Data.LevelId.L8_Sewers && self.myId == 54 &&
                     !Plugin.ArchipelagoClient.AllClearedLocations.Contains((int)Identifiers.NotableLocations.FlushedAwayMichele))
            {
                Ratify(self);
            }
            else if (Plugin.SlotData.EarlySewerIsland && GameplayMaster.instance.levelId == Data.LevelId.Hub &&
                     self.myId == 474)
            {
                Plugin.Log("Moving Bob");
                self.transform.parent = Object.FindObjectsByType<ZoneMaster>(FindObjectsInactive.Include, FindObjectsSortMode.None).First(o => o.gameObject.name.Equals("ZM   X: 1   Z: -3")).transform;
                self.transform.position = new Vector3(167, 10, -460);
                self.gameRelevantPerson = true;
            }
            else if (Plugin.SlotData.EarlyBackflip && GameplayMaster.instance.levelId == Data.LevelId.Hub &&
                     self.myId == 535 && self.dialoguePickup.name.Equals("00 Dialogue Pici Computer Man Backflip"))
            {
                //Plugin.Log($"{self.transform.position} {self.dialoguePickup.name}");
                self.transform.position = new Vector3(-850, 130, 470);
            }
            else if (GameplayMaster.instance.levelId == Data.LevelId.L2_PizzaTime && self.myId == 91 &&
                     Plugin.SlotData.PizzaWheels != YTGVSlotData.PizzaWheelsMode.Disabled)
            {
                self.gameRelevantPerson = !Plugin.ArchipelagoClient.AllClearedLocations.Contains(1_00_00000 * (long)GameplayMaster.instance.levelId +
                    (long)Identifiers.NotableLocations.HubPizzaWheels);
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
            if (APCollectableManager.GoldenSpringReceived)
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

        public static string CurrentFont
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
                    Plugin.Log($"Fallback font needed!");
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

        public enum DialogueTrapType
        {
            None,
            Literature,
            Spam,
            Phone,
        }

        #region Literature Traps

        // Also includes ebook # from project gutenberg and a gag line for spam traps
        public static readonly Tuple<int, string[], string>[] LiteratureTraps =
        [
            // Moby Dick
            // Spam Trap Gag: Fishmael instead of Ishmael
            new(2701, [
                "Moby Dick; Or, The Whale\nBy Herman Melville",
                "CHAPTER 1.\nLoomings.",
                "Call me Ishmael.",
                "Some years ago—never mind how long precisely—having little or no money in my purse, and nothing particular to interest me on shore, I thought I would sail about a little and see the watery part of the world.",
                "It is a way I have of driving off the spleen and regulating the circulation.",
                "Whenever I find myself growing grim about the mouth; whenever it is a damp, drizzly November in my soul; whenever I find myself involuntarily pausing before coffin warehouses, and bringing up the rear of every funeral I meet; and especially whenever my hypos get such an upper hand of me, that it requires a strong moral principle to prevent me from deliberately stepping into the street, and methodically knocking people’s hats off—then, I account it high time to get to sea as soon as I can.",
                "This is my substitute for pistol and ball.",
                "With a philosophical flourish Cato throws himself upon his sword; I quietly take to the ship.",
                "There is nothing surprising in this.",
                "If they but knew it, almost all men in their degree, some time or other, cherish very nearly the same feelings towards the ocean with me.",
            ], "Call me Fishmael."),
            // Alice’s Adventures in Wonderland
            // Spam Trap Gag: First line of Disney movie
            new(11, [
                "Alice’s Adventures in Wonderland\nBy Lewis Carroll",
                "CHAPTER I.\nDown the Rabbit-Hole",
                "Alice was beginning to get very tired of sitting by her sister on the bank, and of having nothing to do: once or twice she had peeped into the book her sister was reading, but it had no pictures or conversations in it, “and what is the use of a book,” thought Alice “without pictures or conversations?”",
                "So she was considering in her own mind (as well as she could, for the hot day made her feel very sleepy and stupid), whether the pleasure of making a daisy-chain would be worth the trouble of getting up and picking the daisies, when suddenly a White Rabbit with pink eyes ran close by her.",
                "There was nothing so very remarkable in that; nor did Alice think it so very much out of the way to hear the Rabbit say to itself, “Oh dear! Oh dear! I shall be late!”",
                "(when she thought it over afterwards, it occurred to her that she ought to have wondered at this, but at the time it all seemed quite natural); but when the Rabbit actually took a watch out of its waistcoat-pocket, and looked at it, and then hurried on, Alice started to her feet, for it flashed across her mind that she had never before seen a rabbit with either a waistcoat-pocket, or a watch to take out of it, and burning with curiosity, she ran across the field after it, and fortunately was just in time to see it pop down a large rabbit-hole under the hedge.",
                "In another moment down went Alice after it, never once considering how in the world she was to get out again.",
                "The rabbit-hole went straight on like a tunnel for some way, and then dipped suddenly down, so suddenly that Alice had not a moment to think about stopping herself before she found herself falling down a very deep well.",
            ], "“…leaders, and had been of late much accustomed to usurpation and conquest. Edwin and Morcar, the earls of Mercia and Northumbria declared for him, and even Stigand… Alice!”"),
            // The Great Gatsby
            // Spam Trap Gag: Intro to "The Wolf of Wall Street" movie.
            new(64317, [
                "The Great Gatsby\nBy F. Scott Fitzgerald",
                "I",
                "In my younger and more vulnerable years my father gave me some advice that I’ve been turning over in my mind ever since.",
                "“Whenever you feel like criticizing anyone,” he told me, “just remember that all the people in this world haven’t had the advantages that you’ve had.”",
                "He didn’t say any more, but we’ve always been unusually communicative in a reserved way, and I understood that he meant a great deal more than that.",
                "In consequence, I’m inclined to reserve all judgements, a habit that has opened up many curious natures to me and also made me the victim of not a few veteran bores.",
                "The abnormal mind is quick to detect and attach itself to this quality when it appears in a normal person, and so it came about that in college I was unjustly accused of being a politician, because I was privy to the secret griefs of wild, unknown men.",
                "Most of the confidences were unsought—frequently I have feigned sleep, preoccupation, or a hostile levity when I realized by some unmistakable sign that an intimate revelation was quivering on the horizon; for the intimate revelations of young men, or at least the terms in which they express them, are usually plagiaristic and marred by obvious suppressions.",
                "Reserving judgements is a matter of infinite hope.",
                "I am still a little afraid of missing something if I forget that, as my father snobbishly suggested, and I snobbishly repeat, a sense of the fundamental decencies is parcelled out unequally at birth.",
            ], "The world of investing can be a jungle. Bulls. Bears. Danger at every turn. That's why we at Stratton Oakmont pride ourselves on being the best. Trained professionals to guide you through the financial wilderness. Stratton Oakmont. Stability. Integrity. Pride."),
            // The Picture of Dorian Gray
            // Spam Trap Gag: Excerpt a contemporary review
            new(174, [
                "The Picture of Dorian Gray\nBy Oscar Wilde",
                "CHAPTER I.",
                "The studio was filled with the rich odour of roses, and when the light summer wind stirred amidst the trees of the garden, there came through the open door the heavy scent of the lilac, or the more delicate perfume of the pink-flowering thorn.",
                "From the corner of the divan of Persian saddle-bags on which he was lying, smoking, as was his custom, innumerable cigarettes, Lord Henry Wotton could just catch the gleam of the honey-sweet and honey-coloured blossoms of a laburnum, whose tremulous branches seemed hardly able to bear the burden of a beauty so flamelike as theirs; and now and then the fantastic shadows of birds in flight flitted across the long tussore-silk curtains that were stretched in front of the huge window, producing a kind of momentary Japanese effect, and making him think of those pallid, jade-faced painters of Tokyo who, through the medium of an art that is necessarily immobile, seek to convey the sense of swiftness and motion.",
                "The sullen murmur of the bees shouldering their way through the long unmown grass, or circling with monotonous insistence round the dusty gilt horns of the straggling woodbine, seemed to make the stillness more oppressive.",
                "The dim roar of London was like the bourdon note of a distant organ.",
                "In the centre of the room, clamped to an upright easel, stood the full-length portrait of a young man of extraordinary personal beauty, and in front of it, some little distance away, was sitting the artist himself, Basil Hallward, whose sudden disappearance some years ago caused, at the time, such public excitement and gave rise to so many strange conjectures.",
                "As the painter looked at the gracious and comely form he had so skilfully mirrored in his art, a smile of pleasure passed across his face, and seemed about to linger there.",
                "But he suddenly started up, and closing his eyes, placed his fingers upon the lids, as though he sought to imprison within his brain some curious dream from which he feared he might awake.",
            ], "This novel contains one element which will taint every young mind that comes in contact with it."),
            // Frankenstein
            // Spam Trap Gag: Intro warning from the 1931 film
            new(84, [
                "Frankenstein; or, the Modern Prometheus\nBy Mary Wollstonecraft (Godwin) Shelley",
                "Chapter 1",
                "I am by birth a Genevese, and my family is one of the most distinguished of that republic.",
                "My ancestors had been for many years counsellors and syndics, and my father had filled several public situations with honour and reputation.",
                "He was respected by all who knew him for his integrity and indefatigable attention to public business.",
                "He passed his younger days perpetually occupied by the affairs of his country; a variety of circumstances had prevented his marrying early, nor was it until the decline of life that he became a husband and the father of a family.",
                "As the circumstances of his marriage illustrate his character, I cannot refrain from relating them.",
                "One of his most intimate friends was a merchant who, from a flourishing state, fell, through numerous mischances, into poverty.",
                "This man, whose name was Beaufort, was of a proud and unbending disposition and could not bear to live in poverty and oblivion in the same country where he had formerly been distinguished for his rank and magnificence.",
                "Having paid his debts, therefore, in the most honourable manner, he retreated with his daughter to the town of Lucerne, where he lived unknown and in wretchedness.",
                "My father loved Beaufort with the truest friendship and was deeply grieved by his retreat in these unfortunate circumstances.",
                "He bitterly deplored the false pride which led his friend to a conduct so little worthy of the affection that united them.",
                "He lost no time in endeavouring to seek him out, with the hope of persuading him to begin the world again through his credit and assistance.",
            ], "How do you do? Mr. Carl Laemmle feels it would be a little unkind to present this picture without just a word of friendly warning. We are about to unfold the story of Frankenstein, a man of science who sought to create a man after his own image, without reckoning upon God."),
            // Pride and Prejudice
            // Spam Trap Gag: First line from Pride and Prejudice and Zombies
            new(1342, [
                "Pride and Prejudice\nBy Jane Austen",
                "Chapter I.",
                "It is a truth universally acknowledged, that a single man in possession of a good fortune must be in want of a wife.",
                "However little known the feelings or views of such a man may be on his first entering a neighbourhood, this truth is so well fixed in the minds of the surrounding families, that he is considered as the rightful property of some one or other of their daughters.",
                "“My dear Mr. Bennet,” said his lady to him one day, “have you heard that Netherfield Park is let at last?”",
                "Mr. Bennet replied that he had not.",
                "“But it is,” returned she; “for Mrs. Long has just been here, and she told me all about it.”",
                "Mr. Bennet made no answer.",
                "“Do not you want to know who has taken it?” cried his wife, impatiently.",
                "“You want to tell me, and I have no objection to hearing it.”",
            ], "It is a truth universally acknowledged that a zombie in possession of brains must be in want of more brains."),
            // Little Women
            // Spam Trap Gag: Simpsons line
            new(37106, [
                "Little Women; or Meg, Jo, Beth and Amy\nBy Louisa M. Alcott",
                "I.\nPlaying Pilgrims.",
                "“Christmas won't be Christmas without any presents,” grumbled Jo, lying on the rug.",
                "“It's so dreadful to be poor!” sighed Meg, looking down at her old dress.",
                "“I don't think it's fair for some girls to have plenty of pretty things, and other girls nothing at all,” added little Amy, with an injured sniff.",
                "“We've got father and mother and each other,” said Beth contentedly, from her corner.",
                "The four young faces on which the firelight shone brightened at the cheerful words, but darkened again as Jo said sadly,—",
                "“We haven't got father, and shall not have him for a long time.” She didn't say \"perhaps never,\" but each silently added it, thinking of father far away, where the fighting was.",
            ], "…and then they realized, they were no longer little girls, they were Little Women."),
            // Romeo and Juliet
            // Spam Trap Gag: Lyrics from "Check Yes Juliet" by We the Kings
            new(1513,[
                "The Tragedy of Romeo and Juliet\nBy William Shakespeare",
                "Act II, Scene II\nCapulet’s Garden.",
                """
                Romeo:
                He jests at scars that never felt a wound.
                But soft, what light through yonder window breaks?
                It is the east, and Juliet is the sun!
                Arise fair sun and kill the envious moon,
                Who is already sick and pale with grief,
                That thou her maid art far more fair than she.
                Be not her maid since she is envious;
                Her vestal livery is but sick and green,
                And none but fools do wear it; cast it off.
                It is my lady, O it is my love!
                O, that she knew she were!
                She speaks, yet she says nothing. What of that?
                Her eye discourses, I will answer it.
                I am too bold, ’tis not to me she speaks.
                Two of the fairest stars in all the heaven,
                Having some business, do entreat her eyes
                To twinkle in their spheres till they return.
                What if her eyes were there, they in her head?
                The brightness of her cheek would shame those stars,
                As daylight doth a lamp; her eyes in heaven
                Would through the airy region stream so bright
                That birds would sing and think it were not night.
                See how she leans her cheek upon her hand.
                O that I were a glove upon that hand,
                That I might touch that cheek.
                """,
                "Juliet:\nAy me.",
                """
                Romeo:
                She speaks.
                O speak again bright angel, for thou art
                As glorious to this night, being o’er my head,
                As is a winged messenger of heaven
                Unto the white-upturned wondering eyes
                Of mortals that fall back to gaze on him
                When he bestrides the lazy-puffing clouds
                And sails upon the bosom of the air.
                """,
                """
                Juliet:
                O Romeo, Romeo, wherefore art thou Romeo?
                Deny thy father and refuse thy name.
                Or if thou wilt not, be but sworn my love,
                And I’ll no longer be a Capulet.
                """
            ], "Check yes, Juliet, are you with me?\nRain is falling down on the sidewalk.\nI won't go until you come outside."),
        ];

        #endregion

        #region Spam Traps

        public static Tuple<string, string, string[], string>[] SpamTraps =>
        [
            new("HAWT TAXIS NEAR YOU", "SoundTextNarrator", [$"Looking for single taxis in your area? Click {SetTextColor("here", DialogueColors.FullRed)} to browse our latest selection!"], "https://www.google.com/search?q=taxi&udm=2"),
            new("HAWT TAXIS NEAR YOU", "SoundTextNarrator", [$"Want to see what makes these taxis tick? Click {SetTextColor("here", DialogueColors.FullRed)}!"], "https://en.wikipedia.org/wiki/Taxi"),
            new("Shameless Plug", "SoundTextNarrator", [$"Did you know there's an official {SetTextColor("Yellow Taxi Goes Vroom", DialogueColors.OrangeYellow)} {SetTextColor("Taxi Plush", DialogueColors.GreenYellow)}? Buy now!"], "https://www.symbiotestudios.com/products/yellow-taxi-goes-vroom-taxi-plush"),
            new("Shameless Plug", "SoundTextNarrator", [$"Did you know there's an official {SetTextColor("Yellow Taxi Goes Vroom", DialogueColors.OrangeYellow)} {SetTextColor("Morio Pin", DialogueColors.GreenYellow)}? Buy now!"], "https://www.symbiotestudios.com/products/yellow-taxi-goes-vroom-morio-enamel-pin"),
            new("Shameless Plug", "SoundTextNarrator", [$"{SetTextColor("CloverPit", DialogueColors.OrangeYellow)} is now available! Buy today!"], "https://store.steampowered.com/app/3314790/CloverPit/"),
            new("Shameless Plug", "SoundTextNarrator", [$"Did you know I used to mod Brawl before this? Check out {SetTextColor("BrawlCrate", DialogueColors.OrangeYellow)}!"], "https://github.com/soopercool101/BrawlCrate"),
            new("Shameless Plug", "SoundTextNarrator", [$"This game can also be purchased on {SetTextColor(Plugin.IsSteam ? "GOG" : "Steam", DialogueColors.GreenYellow)}, why not double dip?"], Plugin.IsSteam ? "https://www.gog.com/en/game/yellow_taxi_goes_vroom" : "https://store.steampowered.com/app/2011780/Yellow_Taxi_Goes_Vroom/"),
            new("Archipelago", "SoundTextNarrator", ["Are you sure you're mod is up too date? I might of fixed the grammar in this spam trap!"], "https://github.com/soopercool101/YellowTaxiAP/releases/latest"),
            new("Important!", "SoundTextNarrator", [$"The fate of the universe relies on you clicking {SetTextColor("this specific link", DialogueColors.FullRed)}!"], "https://www.youtube.com/watch?v=dQw4w9WgXcQ")
        ];

        public static string[] FakeLiteratureTrapMessages =
        [
            "Take a look, it's in a book!",
            "This'll learn you to skip through fine literature!",
            "Lol jk this is a spam trap, are you skipping the text?",
            "Your free trial of this book has expired! Read more?",
            "Visit your local library today!",
            "Reading is succeeding!",
        ];

        #endregion

        #region Phone Traps

        public static CharacterPhoneTraps[] PhoneTraps =>
        [
            new(Strings.I2_LocalizationReformatted("NAME_MORIO"), "SoundDialogueScenziatoVoice", GetMorioPhoneTraps()),
            new(Strings.I2_LocalizationReformatted("NAME_RAT"), "SoundRatTalk", [
                ["Squit Squit! I'm looking sharper than before! I doubt there's a rat as cool as this guy travelling with you!"],
                ["Squit Squit! I'm different from regular rats! It's like I'm in the top percentage of all rats!"],
            ]),
            new(Strings.I2_LocalizationReformatted("NAME_MACKANZIE"), "SoundTextFemaleDefault", [
                [Strings.I2_LocalizationReformatted("DIALOGUE_PIZZA_TIME_MACKENZIE_MANIFEST_JUST_TALK_1"), "Have you joined the Pizza King's cause? All Pizza Men are required to help increase pizza production!"],
                [Strings.I2_LocalizationReformatted("DIALOGUE_PIZZA_TIME_MACKENZIE_MANIFEST_JUST_TALK_1"), "Did you know that prior to the Pizza King's rule, the level was just called \"Time\"?"],
            ]),
            new(Strings.I2_LocalizationReformatted("NAME_PIZZA_KING"), "SoundDialoguePizzaKingVoice", [
                [$"Hello noble knight! Are you keeping your eye out for more {SetTextColor("pizza slices", DialogueColors.OrangeYellow)} <sprite name=\"Pizza\">?", "I believe they're all together but things are so mixed up these days!"],
                ["Hello powerful knight! When you're through with your quest, perhaps a profession in pizza delivery would suit you?"],
            ]),
            new(Strings.I2_LocalizationReformatted("NAME_HAT_SELLER"), "SoundDialogueCappellaioVoice", [
                [Plugin.SlotData.Hatsanity == YTGVSlotData.HatsanityType.Disabled ? "Hats not being checks this run doesn't mean you shouldn't still buy them!" : $"You should buy my wares, what if I have {GetRandomWare()}?"],
                Plugin.SlotData.Hatsanity == YTGVSlotData.HatsanityType.Shopsanity ? ["I still don't know where these question marks came from!", "Who cares, they fetch a nice price!!!"] : [$"You should enable {SetTextColor("Shopsanity", DialogueColors.OrangeYellow)} next run!", "I need you to buy more!!!"],
                ["My shop's been in such high demand these days!", "I might start charging a membership fee!!!"]
            ]),
            new(Strings.I2_LocalizationReformatted("NAME_LAWYER_TAG"), "SoundTextMaleDefault", [
                [Strings.I2_LocalizationReformatted("DISCLAIMER_CASUAL_REFERENCES")],
            ]),
            new(Strings.I2_LocalizationReformatted("NAME_ALIEN_MOSK"), "<MOSK>", [
                ["Is your refrigerator running?", $"It should be running on {SetTextColor("oil", DialogueColors.OrangeYellow)}!"],
                [$"Morio claims I stole the blueprints for the {SetTextColor("Golden Spring", DialogueColors.OrangeYellow)}, but he couldn't be more wrong!", "I got them from an esteemed colleague!"],
                [$"Your wind-up power stands no chance against my {SetTextColor("oil", DialogueColors.OrangeYellow)}!", "The superior energy source will win in the end!"]
            ]),
        ];

        public static string[][] GetMorioPhoneTraps()
        {
            var hintIntro = (Plugin.SlotData.ShuffleFlipOWill || Plugin.SlotData.ShuffleGlide)
                ? "Hello my creature! Have you learned all your moves yet?"
                : "Hello my creature! Have you been using your moves to their fullest?";
            var dialogues = new List<string[]>();
            if (APPlayerManager.BoostLevel >= 2)
            {
                dialogues.Add([
                    hintIntro,
                    $"Did you know you can perform a {SetTextColor("super boost", DialogueColors.OrangeYellow)}?",
                    $"Press the {SetTextColor("acceleration button", DialogueColors.OrangeYellow)} right {SetTextColor("after", DialogueColors.OrangeYellow)} you start dashing forward!",
                    "You can launch to new heights beyond what a regular boost can do!",
                ]);
            }

            if (APPlayerManager.BoostLevel >= 1)
            {
                dialogues.Add([
                    hintIntro,
                    $"Remember that holding the {SetTextColor("acceleration button", DialogueColors.OrangeYellow)} while boosting will let you go further!",
                ]);
            }

            if (APPlayerManager.JumpLevel >= 2)
            {
                dialogues.Add([
                    hintIntro,
                    $"Did you know you can {SetTextColor("backflip", DialogueColors.OrangeYellow)}?",
                    $"Press the {SetTextColor("brake button", DialogueColors.OrangeYellow)} when performing a {SetTextColor("Flip O' Will", DialogueColors.OrangeYellow)}!",
                    $"You can even perform a {SetTextColor("sideflip", DialogueColors.OrangeYellow)} if you hold left or right while you do so!"
                ]);
            }

            if (APPlayerManager.JumpLevel >= 1)
            {
                dialogues.Add([
                    hintIntro,
                    $"Did you know you can interrupt a {SetTextColor("Flip O' Will", DialogueColors.OrangeYellow)}?",
                    $"This will let you {SetTextColor("flip upwards", DialogueColors.OrangeYellow)} to reach new heights!",
                    $"I've heard that it's possible to do one shortly after {SetTextColor("leaving the ground", DialogueColors.OrangeYellow)} or even {SetTextColor("boosting", DialogueColors.OrangeYellow)}! I'd bet you could go far with that!",
                ]);
            }

            if (APPlayerManager.SpinAttackEnabled)
            {
                dialogues.Add([
                    hintIntro,
                    $"See that trail around you when you spin during your {SetTextColor("Flip O' Will", DialogueColors.OrangeYellow)}?",
                    $"That {SetTextColor("spin attack", DialogueColors.OrangeYellow)} can knock some enemies back and break certain blocks!",
                    $"You can even break corrupted {SetTextColor("oil pumps ®", DialogueColors.GreenYellow)}!"
                ]);
            }

            if (APPlayerManager.GlideEnabled)
            {
                dialogues.Add([
                    hintIntro,
                    $"You can {SetTextColor("glide", DialogueColors.OrangeYellow)} by tapping the {SetTextColor("acceleration button", DialogueColors.OrangeYellow)} repeatedly in the air!",
                    $"It may not seem like much at first, but these days it's {SetTextColor("critical", DialogueColors.GreenYellow)} for {SetTextColor("advanced techniques", DialogueColors.OrangeYellow)}!",
                ]);
            }

            if (dialogues.Count == 0)
            {
                dialogues.Add([
                    hintIntro,
                    "What? You haven't learned any?",
                    $"Keep looking, though it's possible you may need to ask someone {SetTextColor("off world", DialogueColors.OrangeYellow)}!",
                    $"I've been told that you can {SetTextColor("!hint", DialogueColors.OrangeYellow)} {SetTextColor("Move", DialogueColors.GreenYellow)}, but I don't know what that means!",
                ]);
            }

            dialogues.Add([$"Hello my creature! Have you been keeping an eye out for {SetTextColor("Gears ®", DialogueColors.GreenYellow)}?", "Feels like they could be anywhere these days!"]);

            return dialogues.ToArray();
        }

        public static string GetRandomWare()
        {
            var wares = new List<string>
            {
                $"someone's {SetTextColor("Master Sword", DialogueColors.GreenYellow)}",
                $"someone's {SetTextColor("Oak's Parcel", DialogueColors.GreenYellow)}",
                $"someone's {SetTextColor("HM03 Surf", DialogueColors.GreenYellow)}",
                $"someone's {SetTextColor("Hard Mode", DialogueColors.FullRed)}",
                $"someone's {SetTextColor("Progressive Stage", DialogueColors.GreenYellow)}",
                $"someone's {SetTextColor("Progressive Era", DialogueColors.GreenYellow)}",
                $"someone's {SetTextColor("Progressive Powerup", DialogueColors.GreenYellow)}",
                $"someone's {SetTextColor("Progressive Key", DialogueColors.GreenYellow)}",
                $"someone's {SetTextColor("Progressive Tools", DialogueColors.GreenYellow)}",
                $"someone's {SetTextColor("Progressive Cup", DialogueColors.GreenYellow)}",
                $"someone's {SetTextColor("Yoshi", DialogueColors.GreenYellow)}",
                $"someone's {SetTextColor("Long Gump", DialogueColors.GreenYellow)}… I mean {SetTextColor("Long Jump", DialogueColors.GreenYellow)}",
                $"someone's {SetTextColor("Cluster 29", DialogueColors.GreenYellow)}",
                $"someone's {SetTextColor("Swim", DialogueColors.GreenYellow)}",
                $"someone's {SetTextColor("Ocarina", DialogueColors.GreenYellow)}",
                $"someone's {SetTextColor("Upgraded Portal Gun", DialogueColors.GreenYellow)}",
                $"someone's {SetTextColor("Moon Pearl", DialogueColors.GreenYellow)}",
                $"someone's {SetTextColor("Cruise Bubble", DialogueColors.GreenYellow)}",
                $"someone's {SetTextColor("Gun", DialogueColors.GreenYellow)}",
            };

            if (APPlayerManager.BoostLevel < 2)
            {
                wares.Add($"your {SetTextColor("Progressive Boost", DialogueColors.OrangeYellow)}");
            }

            if (APPlayerManager.JumpLevel < 2)
            {
                wares.Add($"your {SetTextColor("Progressive Jump", DialogueColors.OrangeYellow)}");
            }

            if (!APPlayerManager.SpinAttackEnabled)
            {
                wares.Add($"your {SetTextColor("Spin Attack", DialogueColors.OrangeYellow)}");
            }

            if (!APPlayerManager.GlideEnabled)
            {
                wares.Add($"your {SetTextColor("Glide", DialogueColors.OrangeYellow)}");
            }

            return wares[Random.RandomRangeInt(0, wares.Count)];
        }

        public static Tuple<int,string>[] PhoneTrapRings =
        [
            new(6, "Ring!"),
            new(3, "Ringring!"),
            new(2, "Brrrrrriiiing!"),
        ];

        public string GetRandomPhoneRing()
        {
            var ringData = PhoneTrapRings[Random.RandomRangeInt(0, PhoneTrapRings.Length)];
            var ringString = ringData.Item2;
            for (var i = 0; i < Random.RandomRangeInt(0, ringData.Item1); i++)
            {
                ringString += $" {ringData.Item2}";
            }

            return ringString;
        }

        public class CharacterPhoneTraps
        {
            public string CharacterName;
            public string DialogueSound;
            public string[][] Messages;

            public CharacterPhoneTraps(string name, string sound, string[][] msg)
            {
                CharacterName = name;
                DialogueSound = sound;
                Messages = msg;
            }
        }

        #endregion


        public static DialogueTrapType ActiveDialogueTrapType = DialogueTrapType.None;

        public const int GymMembershipPrice = 500;
        private void DialogueScript_Start(On.DialogueScript.orig_Start orig, DialogueScript self)
        {
            var dialogueCapsule = !DialogueCapsule.dictionary.ContainsKey(self.dialgoueCapsuleKey)
                ? DialogueCapsule.dictionary["DEFAULT"]
                : DialogueCapsule.dictionary[self.dialgoueCapsuleKey.ToUpper()];
            if (dialogueCapsule != null)
            {
                Plugin.Log($"Initiating dialogue with key: {dialogueCapsule.key}  {self.textSoundNames[0]}");

                if (DebugLocationHelper.Enabled)
                {
                    GUIUtility.systemCopyBuffer = dialogueCapsule.key;
                    for (var i = 0; i < self.dialogues.Length; i++)
                    {
                        if (i < dialogueCapsule.subKeysNames.Length)
                            Plugin.Log(dialogueCapsule.subKeysNames[i]);
                        Plugin.Log(dialogueCapsule.subKeysDialogues[i], false);
                        Plugin.Log(self.dialogues[i], false);
                    }
                }

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
                        if (!Plugin.SlotData.ShuffleFlipOWill)
                            break;
                        self.dialogues =
                        [
                            GetMoveDialogue("Super Boost", APPlayerManager.BoostLevel > 1),
                            "Instead, here's an item from the multiworld!"
                        ];
                        moveRandoID = Identifiers.SUPERBOOST_ID;
                        break;
                    case "DIALOGUE_PICI_COMPUTER_MAN_FLIP_ABORT" when GameplayMaster.instance.levelId == Data.LevelId.Hub: // Normally jump tutorial
                        if (!Plugin.SlotData.ShuffleFlipOWill)
                            break;
                        self.dialogues =
                        [
                            GetMoveDialogue("Flip", APPlayerManager.JumpLevel > 0),
                            "Instead, here's an item from the multiworld!"
                        ];
                        moveRandoID = Identifiers.JUMP_ID;
                        break;
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
                    case "DIALOGUE_RAT_PICKUP_QUESTION" when GameplayMaster.instance.levelId == Data.LevelId.L8_Sewers || Plugin.SlotData.ShuffleRat:
                        self.dialogues =
                        [
                            APRatManager.ReceivedRatItem
                                ? "Squit squit! Who's your handsome friend?!?"
                                : "Squit squit! I was looking for cheese, but found a check!!!",
                            "Would you like this shiny thing I found?!?"
                        ];
                        break;
                    case "DIALOGUE_RAT_PICKUP_ANWER_YES" when ActiveDialogueTrapType == DialogueTrapType.Literature:
                        ActiveDialogueTrapType = DialogueTrapType.None;
                        self.dialogues = LiteratureTraps[Random.RandomRangeInt(0, LiteratureTraps.Length)].Item2;
                        self.requiresSilence = true;
                        self.names = ["Literature Trap"];
                        var litNarratorSound = self.textSoundNames[0];
                        self.overrideCamera = false;
                        self.textSoundNames = new string[self.dialogues.Length];
                        self.unskippableDialogues = new bool[self.dialogues.Length];
                        //self.forceEndWait = new float[self.dialogues.Length];
                        self.endDialogueDelay = 0.5f;
                        for (var i = 0; i < self.textSoundNames.Length; i++)
                        {
                            self.textSoundNames[i] = litNarratorSound;
                            //self.unskippableDialogues[i] = true;
                            //self.forceEndWait[i] = 2;
                        }
                        break;
                    case "DIALOGUE_RAT_PICKUP_ANWER_YES" when ActiveDialogueTrapType == DialogueTrapType.Spam:
                        ActiveDialogueTrapType = DialogueTrapType.None;
                        var index = Random.RandomRangeInt(-3, SpamTraps.Length);
                        string url;
                        self.askQuestion = true;
                        self.overrideCamera = false;
                        if (index < 0) // Fake literature trap
                        {
                            var lit = LiteratureTraps[Random.RandomRangeInt(0, LiteratureTraps.Length)];
                            self.dialogues =
                            [
                                lit.Item2[0],
                                lit.Item2[1],
                                lit.Item3,
                                FakeLiteratureTrapMessages[Random.RandomRangeInt(0, FakeLiteratureTrapMessages.Length)],
                            ];
                            self.names =
                            [
                                "Litareture Trap",
                                "Litareture Trap",
                                "Litareture Trap",
                                "Spam Trap!",
                            ];
                            self.textSoundNames =
                            [
                                "SoundTextNarrator",
                                "SoundTextNarrator",
                                "SoundTextNarrator",
                                "SoundTextNarrator",
                            ];
                            url = $"https://www.gutenberg.org/ebooks/{lit.Item1}";
                        }
                        else
                        {
                            var spam = SpamTraps[index];
                            self.dialogues = spam.Item3;
                            self.names = [spam.Item1];
                            self.textSoundNames = new string[self.dialogues.Length];
                            for (var i = 0; i < self.textSoundNames.Length; i++)
                            {
                                self.textSoundNames[i] = spam.Item2;
                            }

                            url = spam.Item4;
                        }

                        self.onAnswerYes.AddListener(SpamYesAnswer);
                        break;

                        void SpamYesAnswer()
                        {
                            if (Plugin.IsSteam)
                            {
                                try
                                {
                                    SteamFriends.OpenWebOverlay(url);
                                    return;
                                }
                                catch
                                {
                                    // Do nothing
                                }
                            }
                            Application.OpenURL(url);
                        }
                    case "DIALOGUE_RAT_PICKUP_ANWER_YES" when ActiveDialogueTrapType == DialogueTrapType.Phone:
                        ActiveDialogueTrapType = DialogueTrapType.None;
                        var phoneTrapIndex = Random.RandomRangeInt(-2, PhoneTraps.Length);
                        if (phoneTrapIndex < 0) // Weight morio phone calls higher
                        {
                            phoneTrapIndex = 0;
                        }

                        var phoneTrap = PhoneTraps[phoneTrapIndex];
                        self.names = ["Phone Trap", phoneTrap.CharacterName];
                        var phoneDialogues = phoneTrap.Messages[Random.RandomRangeInt(0, phoneTrap.Messages.Length)].ToList();
                        phoneDialogues.Insert(0, GetRandomPhoneRing());
                        self.dialogues = phoneDialogues.ToArray();
                        self.textSoundNames = new string[self.dialogues.Length];
                        self.unskippableDialogues = new bool[self.dialogues.Length];
                        self.endDialogueDelay = 0.5f;
                        self.textSoundNames[0] = "SoundCoin10";
                        var dialogueSound = phoneTrap.DialogueSound;
                        if (dialogueSound.Equals("<MOSK>")) // Mosk's voice isn't in all levels. Make do.
                        {
                            if (AssetMaster.GetSound("SoundDialogueAlienMoskVoice"))
                            {
                                dialogueSound = "SoundDialogueAlienMoskVoice";
                            }
                            else if (AssetMaster.GetSound("SoundDialogueAlienMoskGoodVoice"))
                            {
                                dialogueSound = "SoundDialogueAlienMoskGoodVoice";
                            }
                            else
                            {
                                dialogueSound = "SoundTextMaleDefault";
                            }
                        }
                        if (!AssetMaster.GetSound(dialogueSound)) // Use default sound if sound isn't present. Otherwise dialogue fails to load past the first character.
                        {
                            dialogueSound = "SoundTextMaleDefault";
                        }
                        for (var i = 1; i < self.textSoundNames.Length; i++)
                        {
                            self.textSoundNames[i] = dialogueSound;
                            self.unskippableDialogues[i] = true;
                        }

                        break;
                    case "DIALOGUE_RAT_PICKUP_ANWER_YES" when GameplayMaster.instance.levelId == Data.LevelId.L6_Gym:

                        if (APWalletManager.ServerCoins < GymMembershipPrice)
                        {
                            self.dialogues =
                            [
                                "You cannot possibly afford that!"
                            ];
                            break;
                        }

                        MenuEventLeaderboard.CoinsSpentAdd(GymMembershipPrice);
                        self.dialogues =
                        [
                            $"You received {GetItemText((long)Identifiers.NotableLocations.UltraChadMembership, true, false)} as a gift! Now to wait 4-7 business weeks for your membership card!"
                        ];
                        QueuedItem = (long)Identifiers.NotableLocations.UltraChadMembership;
                        break;
                    case "DIALOGUE_RAT_PICKUP_ANWER_YES" when GameplayMaster.instance.levelId == Data.LevelId.L8_Sewers:
                        self.dialogues =
                        [
                            $"Michele handed you a particularly smelly {GetItemText((int)Identifiers.NotableLocations.FlushedAwayMichele, true, false)} before scurrying back into the sewage!"
                        ];
#if DEBUG
                        DebugLocationHelper.CheckLocation("Michele (Flushed Away)", "8_10_00008");
#endif
                        QueuedItem = (int)Identifiers.NotableLocations.FlushedAwayMichele;
                        break;
                    case "DIALOGUE_RAT_PICKUP_ANWER_YES":
                        if (!Plugin.SlotData.ShuffleRat)
                        {
                            // Michele gets saved to the save file now, remove reference to that not being the case.
                            self.dialogues =
                            [
                                "Michele joined you in your adventure!!!"
                            ];
                            break;
                        }
                        var itemToSend = (long)GameplayMaster.instance.levelId * 1_00_00000 + (long)Identifiers.NotableLocations.HubMichele;
                        self.dialogues =
                        [
                            $"Michele handed you a particularly smelly {GetItemText(itemToSend, true, false)} before scurrying back to the sewers!"
                        ];
#if DEBUG
                        DebugLocationHelper.CheckLocation("Michele", "21_99999");
#endif
                        QueuedItem = itemToSend;
                        break;
                    case "DIALOGUE_RAT_PICKUP_ANWER_NO" when GameplayMaster.instance.levelId == Data.LevelId.L8_Sewers:
                    case "DIALOGUE_RAT_PICKUP_ANWER_NO" when GameplayMaster.instance.levelId == Data.LevelId.L6_Gym:
                    case "DIALOGUE_RAT_PICKUP_ANWER_NO" when Plugin.SlotData.ShuffleRat:
                        self.dialogues =
                        [
                            "You monster!!! That could've been someone's unwall!!!"
                        ];
                        break;
                    case "DIALOGUE_GRANNY_ISLAND_LAB_DOGGO_STUCK":
                        switch (Plugin.SlotData.FecalMattersUnlockCondition)
                        {
                            case YTGVSlotData.LevelUnlockCondition.Special: // Vanilla
                                APSaveController.MiscSave.HasDoggo = true;
                                break;
                            case YTGVSlotData.LevelUnlockCondition.Item:
                                self.dialogues =
                                [
                                    "Woff Woff Woff! " + (APAreaStateManager.DoggoReceived ? "(Hey, you found my house keys!)" : "(Have you seen my house keys?)"),
                                    $"Woff Woff Woff! (I looked where I last left them, but found this {GetItemText((long)Identifiers.NotableLocations.Doggo, true, false)} instead!)",
                                    "Woff Woff Woff! " + (APAreaStateManager.DoggoReceived ? "(You can have it as a reward! Go visit my home on Granny's Island!)" : "(I suppose you need it more than me! If you find my house keys meet me at my home on Granny's Island!)"),
                                ];
                                QueuedItem = (long)Identifiers.NotableLocations.Doggo;
                                break;
                            case YTGVSlotData.LevelUnlockCondition.Open:
                            case YTGVSlotData.LevelUnlockCondition.FullGame:
                            case YTGVSlotData.LevelUnlockCondition.Exclude:
                            default:
                                self.dialogues =
                                [
                                    "Woff Woff Woff! (You didn't need to talk to me this seed!)",
                                    "Woff Woff Woff! (Nice of you to do so anyway!)",
                                ];
                                break;
                        }
                        break;
                    case "DIALOGUE_GRANNY_ISLAND_LAB_DOGGO_STUCK_AFTER_STILL_IN_LAB":
                        if (APAreaStateManager.DoggoReceived || APSaveController.MiscSave.HasDoggo)
                        {
                            break;
                        }

                        self.dialogues =
                        [
                            "Woff Woff Woff! (Still no luck finding my house keys?)",
                            "Woff Woff Woff! (If you find them, please meet me at my home on Granny's Island!)"
                        ];

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
                                : $"You are stuck here now, but as consolation you can have {GetItemText((long)Identifiers.NotableLocations.DemoWall)}!",
                        ];
                        QueuedItem = (long)Identifiers.NotableLocations.DemoWall;
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
                                .HubGoldenPropeller))
                        {
                            self.dialogues =
                            [
                                self.dialogues[0],
                                self.dialogues[1],
                                $"For managing to get up here{(APCollectableManager.GoldenPropellerActive ? string.Empty : " without one")}, here's {GetItemText((long) Identifiers.NotableLocations.HubGoldenPropeller)}!"
                            ];
                            QueuedItem = (long)Identifiers.NotableLocations.HubGoldenPropeller;
                        }
                        break;
                    case "DIALOGUE_MORI_O_TRON_HAT_ARMADIO" when Plugin.ArchipelagoClient.LocationUncleared((long)Identifiers.NotableLocations.WardrobeMoriotron):
                        self.dialogues[1] =
                            $"Also, while cleaning, I managed to find {GetItemText((long)Identifiers.NotableLocations.WardrobeMoriotron)}!";
                        QueuedItem = (long)Identifiers.NotableLocations.WardrobeMoriotron;
                        break;
                    case "DIALOGUE_GRANNY_ISLAND_ALIEN_MOSK_QEUSTION_1":
                        // Don't actually ask a question, don't want Mosk to take you anywhere directly
                        self.askQuestion = false;
                        self.askingQuestion = false;
                        if (!Plugin.ArchipelagoClient.AllClearedLocations.Contains((long)Identifiers.NotableLocations.HubMosksRocket))
                        {
                            self.dialogues =
                                APAreaStateManager.RocketEnabled ?
                                    [
                                        "I know I shouldn't be here yet, but... hey those are the keys to my rocket!",
                                        $"As a reward, you can have {GetItemText((long)Identifiers.NotableLocations.HubMosksRocket)}!",
                                        "If you want to check out my rocket, use the front door!",
                                    ] :
                                    [
                                        "I know I shouldn't be here yet, but I lost the keys to my rocket. Keep an eye out for them!",
                                        $"All I found instead was {GetItemText((long)Identifiers.NotableLocations.HubMosksRocket)}, but you can have it.",
                                    ];
                            QueuedItem = (long)Identifiers.NotableLocations.HubMosksRocket;
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
                                (long) Identifiers.NotableLocations.HubPizzaKing))
                        {
                            self.dialogues = APAreaStateManager.PizzaKingReceived ?
                            [
                                "Hello friendly knight! Those keys you have... those are my vacation home keys!",
                                $"As a reward for finding them, take {GetItemText((long)Identifiers.NotableLocations.HubPizzaKing)}!",
                            ]:
                            [
                                "Hello friendly knight! Have you seen my vacation home keys anywhere? I appear to have misplaced them.",
                                $"All I found instead is {GetItemText((long)Identifiers.NotableLocations.HubPizzaKing)}, but you can have it!",
                            ];
                            QueuedItem = (long)Identifiers.NotableLocations.HubPizzaKing;
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
                    case "DIALOGUE_PIZZA_TIME_PIZZA_KING_REWARD":
                        if (!Plugin.SlotData.ShufflePizzaKing)
                        {
                            APSaveController.MiscSave.HasPizzaKing = true;
                            break;
                        }
                        var pizzaKingReward = (long)GameplayMaster.instance.levelId * 1_00_00000 +
                                              (long)Identifiers.NotableLocations.HubPizzaKing;
                        if (!Plugin.SlotData.ShufflePizzaKing ||
                            Plugin.ArchipelagoClient.AllClearedLocations.Contains(pizzaKingReward))
                            break;
                        QueuedItem = pizzaKingReward;
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
                            self.dialogues[0],
                            self.dialogues[1],
                            $"I also have {GetItemText(pizzaKingReward)}, take it as well!"
                        ];
                        break;
                    case "PSYCHO_TAXI_CABINET_NO_UNLOCK":
                        if (!Plugin.SlotData.EarlyPsychoTaxi || !Plugin.SlotData.ShufflePsychoTaxi)
                            break;

                        if (!Plugin.ArchipelagoClient.AllClearedLocations.Contains(
                                (long) Identifiers.NotableLocations.HubPsychoTaxi))
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
                                $"What's this? You found {GetItemText((long) Identifiers.NotableLocations.HubPsychoTaxi, true, false)} wedged in the cartridge slot!",
                            ];
                            QueuedItem = (long)Identifiers.NotableLocations.HubPsychoTaxi;
                        }
                        break;
                    case "PSYCHO_TAXI_CABINET_PLAY_QUESTION":
                        if (!Plugin.SlotData.EarlyPsychoTaxi || !Plugin.SlotData.ShufflePsychoTaxi)
                            break;

                        if (!Plugin.ArchipelagoClient.AllClearedLocations.Contains(
                                (long)Identifiers.NotableLocations.HubPsychoTaxi))
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
                                $"What's this? You found {GetItemText((long) Identifiers.NotableLocations.HubPsychoTaxi, true, false)} next to the machine!",
                                self.dialogues[0],
                            ];
                            QueuedItem = (long)Identifiers.NotableLocations.HubPsychoTaxi;
                        }
                        break;
                    case "DIALOGUE_GRANNY_ISLAND_OCRA_TAXI_MINIGAME_2":
                        if (!Plugin.SlotData.EarlyOrangeSwitch ||
                            Plugin.ArchipelagoClient.AllClearedLocations.Contains((int) Identifiers.NotableLocations.HubOrangeSwitch))
                            break;
                        self.dialogues[1] =
                            $"As a reward, please take {GetItemText((int) Identifiers.NotableLocations.HubOrangeSwitch)}!";
                        QueuedItem = (long)Identifiers.NotableLocations.HubOrangeSwitch;
                        break;
                    case "DIALOGUE_MORIO_DREAM_MACHINE_INACTIVE":
                        // TODO: If Morio's Mind is a level, dialogue should probably be tweaked here somewhat anyway
                        if (!Plugin.SlotData.EarlyMoriosPassword)
                            break;

                        Plugin.BepinLogger.LogWarning(self.dialogues[1]);
                        self.dialogues =
                        [
                            self.dialogues[0],
                            APAreaStateManager.MindPasswordReceived ? $"Looks like you already found the {SetTextColor("password", DialogueColors.OrangeYellow)} to this safety door!" : self.dialogues[1],
                            $"On an unrelated note, this machine will give you {GetItemText((int) Identifiers.NotableLocations.HubMoriosPassword)}!",
                            $"I hope it's worth it, because this is quite painful!",
                        ];

                        break;
                    case "DIALOGUE_MORIO_DREAM_MACHINE_ACTIVE_AFTER_PASSWORD":
                        if (!Plugin.SlotData.EarlyMoriosPassword)
                            break;

                        self.dialogues =
                        [
                            self.dialogues[0],
                        ];
                        break;
                    case "DIALOGUE_PIZZA_TIME_MACKENZIE_MANIFEST_JUST_TALK":
                        if (Plugin.SlotData.PizzaWheels == YTGVSlotData.PizzaWheelsMode.Disabled)
                            break;

                        var pizzaWheelLocation = (1_00_00000 * (long)GameplayMaster.instance.levelId) +
                                                 (long)Identifiers.NotableLocations.HubPizzaWheels;

                        if (!Plugin.ArchipelagoClient.AllClearedLocations.Contains(pizzaWheelLocation))
                        {
                            QueuedItem = pizzaWheelLocation;
                            self.dialogues[2] = Master.cheat_PizzaWheels ?
                                $"You're the chosen one with pizzas for wheels! Take {GetItemText(pizzaWheelLocation)}!" :
                                $"Keep an eye out for the fabled pizza wheels! Perhaps {GetItemText(pizzaWheelLocation)} will help your search!";
                        }
                        break;
                    case "DIALOGUE_GRANNY_ISLAND_GELATAIO_THANKS":
                        if (!Plugin.SlotData.EarlyGelaToni)
                            break;

                        if (!Plugin.ArchipelagoClient.AllClearedLocations.Contains(
                                (long) Identifiers.NotableLocations.HubGelaToni))
                        {
                            self.dialogues = APAreaStateManager.GelaToniReceived ?
                                [
                                    self.dialogues[0],
                                    $"As a reward, you can have {GetItemText((long) Identifiers.NotableLocations.HubGelaToni)}!",
                                ] :
                                [
                                    "Hey hey! Have you seen an ice cream truck around here?",
                                    $"I thought I parked it here, but found {GetItemText((long) Identifiers.NotableLocations.HubGelaToni)} in its place! It's yours if you want!",
                                ];
                            QueuedItem = (long)Identifiers.NotableLocations.HubGelaToni;
                        }
                        else if (!APAreaStateManager.GelaToniReceived)
                        {
                            self.dialogues = 
                            [
                                "Hey hey! Have you been keeping an eye out for my ice cream truck?",
                            ];
                        }
                        break;
                    case "DIALOGUE_MORIO_LAB_SPIKES_ACCESS_PRE_TOSLA":
                    case "DIALOGUE_MORIO_LAB_SPIKES_ACCESS_POST_TOSLA":
                        if (!Plugin.SlotData.EarlyGoldenSpring || Plugin.ArchipelagoClient.AllClearedLocations.Contains((long)Identifiers.NotableLocations.HubGoldenSpring))
                            break;

                        self.dialogues[1] = $"On an unrelated note, I have {GetItemText((long)Identifiers.NotableLocations.HubGoldenSpring)}, please take it!";
                        QueuedItem = (long)Identifiers.NotableLocations.HubGoldenSpring;
                        break;
                    case "BOBOMBOSS_2":
                        if (!Plugin.SlotData.EarlyGelaToni)
                        {
                            if (Plugin.SlotData.ShuffleGelaToni)
                                QueuedItem = 1_00_00000 + (long)Identifiers.NotableLocations.HubGelaToni;
                            else
                                APSaveController.MiscSave.HasGelaToni = true;
                        }
                        if (Plugin.SlotData.Goal == YTGVSlotData.GoalType.Bombeach)
                            Plugin.ArchipelagoClient.Win();
                        break;
                    case "DIALOGUE_MOON_END":
                        if (Plugin.SlotData.Goal == YTGVSlotData.GoalType.Moon)
                            Plugin.ArchipelagoClient.Win();
                        break;
                    case "DIALOGUE_GYM_GIGACHAD":
                        if (Plugin.SlotData.GymGearsUnlockCondition != YTGVSlotData.LevelUnlockCondition.Item ||
                            Plugin.ArchipelagoClient.AllClearedLocations.Contains((long) Identifiers.NotableLocations
                                .UltraChadMembership))
                            break;
                        self.askQuestion = true;
                        self.dialogues =
                        [
                            $"Hey King! If you're really serious about working out, you should upgrade to our {SetTextColor("Super Deluxe Membership", DialogueColors.OrangeYellow)}!",
                            $"It's only {SetTextColor($"{GymMembershipPrice} coins", DialogueColors.Yellow)} and comes with a {SetTextColor("Free Gift", DialogueColors.OrangeYellow)}, interested?",
                        ];
                        self.onAnswerYes.AddListener(SpecialMethod_OnUltraChadAnswerYes);
                        self.onAnswerNo.AddListener(SpecialMethod_OnUltraChadAnswerNo);
                        break;
                    case "DIALOGUE_MORIO_LAB_SECRET_BEDROOM":
                        if (!Plugin.SlotData.LockedMoriosLab ||
                            Plugin.ArchipelagoClient.AllClearedLocations.Contains((long) Identifiers.NotableLocations
                                .LabKey))
                            break;
                        self.dialogues =
                        [
                            "Get out! Now!",
                            $"And take {GetItemText((long) Identifiers.NotableLocations.LabKey)} with you!",
                        ];
                        QueuedItem = (long)Identifiers.NotableLocations.LabKey;
                        break;
                    case "DIALOGUE_NARRATOR_TIME_ATTACK_ASK_RETRY_NEGATIVE":
                        self.dialogues =
                        [
                            "Would you like to try again?",
                        ];
                        break;
                    case "DIALOUGE_GRANNY_ISLAND_OPERAIO_BUILDING_STUFF_1" when Plugin.SlotData.EarlySewerIsland:
                        self.dialogues =
                        [
                            "Sorry, the bridge is out for the foreseeable future.",
                            "I can bring you across if you want, just don't ask specifics of how it works!"
                        ];
                        self.askQuestion = true;
                        self.onAnswerYes.AddListener(SpecialMethod_OnBobMattoneAnswerYes);
                        break;
                    case "DIALOGUE_PIZZA_TIME_PIZZA_CHEFF_INITIAL_ADD_TIME" when MapArea.IsPlayerInsideLab():
                        if (Plugin.ArchipelagoClient.AllClearedLocations.Contains((long)Identifiers.NotableLocations
                                .HubPizzaWheels))
                        {
                            self.dialogues =
                            [
                                $" {(Master.cheat_PizzaWheels ? "Grazie mille my friend! Those pizza wheels look great on you!" : "Keep searching for my pizza wheels!")}"
                            ];
                            break;
                        }
                        QueuedItem = (long)Identifiers.NotableLocations.HubPizzaWheels;
                        self.dialogues =
                        [
                            Master.cheat_PizzaWheels ? "Buongiornissimo!!! I see you found my pizza wheels!" : "Buongiornissimo!!! I cooked up some delicious pizza wheels, but lost them!",
                            $"Perhaps {GetItemText((long)Identifiers.NotableLocations.HubPizzaWheels)} will help you on your quest!"
                        ];
                        break;
                    case "DIALOGUE_MORIO_AT_TOSLA_HQ_PORTAL_LOCKED":
                        // TODO: Update dialogue when v1.0.0 comes out. Hopefully this year.
                        self.dialogues[1] = "Surely v1.0.0 will be out this year!";
                        break;
#if DEBUG
                    case "NARRATOR_BACK_TO_HUB_QUESTION":
                    case "DIALOGUE_NARRATOR_BACK_TO_HUB_QUESTION_LAB_ALT":
                        break;
#endif
                }

                if (moveRandoID > 0)
                {
                    try
                    {
                        var id = ((long) GameplayMaster.instance.levelId * 1_00_00000) + 8_00000 + moveRandoID;
                        if (Plugin.ArchipelagoClient.LocationUncleared(id))
                        {
                            self.dialogues[self.dialogues.Length - 1] = $"Instead, I'll give you {GetItemText(id)}!";
                            QueuedItem = id;
                        }
                        else
                        {
                            self.dialogues = self.dialogues.Take(self.dialogues.Length - 1).ToArray();
                        }
                    }
                    catch(Exception ex)
                    {
                        Plugin.BepinLogger.LogWarning(ex);
                    }
#if DEBUG
                    DebugLocationHelper.CheckLocation("Move Rando", $"{(int)GameplayMaster.instance.levelId}_{Identifiers.NPC_ID:D2}_{moveRandoID:D5}");
#endif
                }

                if (QueuedItem != null)
                {
                    self.onDialogueEnd.AddListener(SpecialMethod_SendQueuedItem);
                }
            }
            orig(self);
        }

        public static long? QueuedItem;
        public void SpecialMethod_SendQueuedItem()
        {
            if (QueuedItem != null)
            {
                Plugin.ArchipelagoClient.SendLocation(QueuedItem.Value);
                QueuedItem = null;
            }
        }

        public void SpecialMethod_OnBobMattoneAnswerYes()
        {
            //, 270, 0, true, true, "SoundtrackHubOutside", "Background Sea and Sky", "LEVEL_NAME_GRANNY_ISLAND"),
            var transitionScript = PortalTransitionScript.Spawn(new Vector3(175f, 10f, -695f), 90);
            transitionScript.songChange = "SoundtrackHubOutside";
            transitionScript.backgroundChange = "Background Sea and Sky";
            transitionScript.desiredWaterState = true;
            transitionScript.desiredLightState = true;
            transitionScript.desiredZoneId = 0;
        }
        public void SpecialMethod_OnUltraChadAnswerYes()
        {
            Spawn.Instance("Dialogue Rat Pickup Answer Yes", Vector3.zero);
        }

        public void SpecialMethod_OnUltraChadAnswerNo()
        {
            Spawn.Instance("Dialogue Rat Pickup Answer No", Vector3.zero);
        }

        public enum DialogueColors
        {
            Black,
            Acqua,
            Yellow,
            GreenYellow,
            OrangeYellow,
            RedYellow,
            FullRed,
        }

        public static string SetTextColor(string text, DialogueColors color)
        {
            var font = CurrentFont;
            return $"<font=\"{font} Black\" material=\"{font} {color}\">{text}</font>";
        }

        public static string GetItemText(long itemId, bool includePlayer = true, bool includePrefixes = true)
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
