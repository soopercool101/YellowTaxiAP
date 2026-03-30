using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using YellowTaxiAP.Archipelago;
using YellowTaxiAP.Managers;

namespace YellowTaxiAP.Behaviours
{
    /// <summary>
    /// For some reason this needs to be done as MonoBehaviours, doing it directly when receiving an item crashes the game.
    /// Simple low-effort generic-ish workaround
    /// </summary>
    public abstract class AreaStateOverride : MonoBehaviour
    {
        protected abstract bool ExpectedState { get; }
        protected bool? state;
        public GameObject[] toDisable = [];
        public GameObject[] toEnable = [];

        public abstract void Awake(); // Need to properly populate toDisable/toEnable

        public virtual void FixedUpdate()
        {
            if (state == ExpectedState)
                return;

            foreach (var disable in toDisable)
            {
                if (!disable)
                    continue;
                disable.SetActive(!ExpectedState || disable.GetComponent<BonusScript>());
            }

            foreach (var enable in toEnable)
            {
                if (!enable)
                    continue;
                enable.SetActive(ExpectedState || enable.GetComponent<BonusScript>());
            }

            state = ExpectedState;
        }
    }

    public class AreaStateOverride_GelaToni : AreaStateOverride
    {
        protected override bool ExpectedState => APAreaStateManager.GelaToniReceived;

        public override void Awake()
        {
            var orig = gameObject.GetComponent<DisableAreaScript_GrannyIsland_IceCream>();
            if (orig)
            {
                toDisable = orig.disableThisAreaWhenActive;
                toEnable = orig.enableThisAreaWhenActive;
            }

            if (Plugin.SlotData.EarlyGelaToni)
            {
                foreach (var enable in toEnable)
                {
                    var personScript = enable.transform.GetComponentInChildren<PersonParent>(true);
                    if (!personScript)
                        continue;
                    var gelaToni = enable.transform.GetComponentInChildren<PersonParent>().gameObject;
                    if (!gelaToni)
                        continue;
                    gelaToni.transform.parent = enable.transform.parent;
                    gelaToni.SetActive(true);
                }
            }
        }
    }

    public class AreaStateOverride_PizzaKing : AreaStateOverride
    {
        protected override bool ExpectedState => APAreaStateManager.PizzaKingReceived;

        public override void Awake()
        {
            var orig = gameObject.GetComponent<DisableAreaScript_GrannyIsland_KingPizza>();
            if (orig)
            {
                toDisable = orig.disableThisAreaWhenActive;
                toEnable = orig.enableThisAreaWhenActive;
            }

            if (Plugin.SlotData.EarlyPizzaKing)
            {
                var newToEnable = new List<GameObject>();
                foreach (var enable in toEnable)
                {
                    if (enable.name.Equals("Person Pizza King Granny'sIsland"))
                    {
                        enable.SetActive(true);
                    }
                    else
                    {
                        newToEnable.Add(enable);
                    }
                }

                toEnable = newToEnable.ToArray();
            }
        }
    }

    public class AreaStateOverride_Doggo : AreaStateOverride
    {

        protected override bool ExpectedState
        {
            get
            {
                return Plugin.SlotData.GymGearsUnlockCondition switch
                {
                    YTGVSlotData.LevelUnlockCondition.Exclude => false,
                    YTGVSlotData.LevelUnlockCondition.Open => true,
                    YTGVSlotData.LevelUnlockCondition.Special or YTGVSlotData.LevelUnlockCondition.Item =>
                        APAreaStateManager.DoggoReceived,
                    YTGVSlotData.LevelUnlockCondition.FullGame => APAreaStateManager.FullGameUnlocked,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }

        public override void Awake()
        {
            var orig = gameObject.GetComponent<DisableAreaScript_StuckDoggoTalk>();
            if (orig)
            {
                toDisable = orig.disableThisAreaWhenActive;
                toEnable = orig.enableThisAreaWhenActive;
            }
        }
    }

    public class AreaStateOverride_MorioPassword : AreaStateOverride
    {
        protected override bool ExpectedState => APAreaStateManager.MindPasswordReceived;

        public override void Awake()
        {
            var orig = gameObject.GetComponent<DisableAreaScript_MorioMindPassword>();
            if (orig)
            {
                toDisable = orig.disableThisAreaWhenActive;
                toEnable = orig.enableThisAreaWhenActive;
            }
        }
    }

    public class AreaStateOverride_Rocket : AreaStateOverride
    {
        protected override bool ExpectedState => APAreaStateManager.RocketEnabled;

        public override void Awake()
        {
            var orig = gameObject.GetComponent<DisableAreaScript_BeatedFinalBoss>();
            if (orig)
            {
                toDisable = orig.disableThisAreaWhenActive;
                toEnable = orig.enableThisAreaWhenActive;
            }
        }

        public override void FixedUpdate()
        {
            if (state == ExpectedState)
                return;

            foreach (var disable in toDisable)
            {
                if (!disable)
                    continue;
                if (disable.name.Equals("Dettaglio Albero1")) // Keep this tree. Mosk normally replaces it.
                {
                    if (Plugin.SlotData.EarlyRocket) // Disable it if early rocket
                        disable.SetActive(false);
                }
                else
                {
                    disable.SetActive(!ExpectedState || disable.GetComponent<BonusScript>());
                }
            }

            foreach (var enable in toEnable)
            {
                if (!enable)
                    continue;
                if (enable.name.Equals("Person Alien Mosk Good"))
                {
                    // Put Mosk here if he is needed for a location check
                    enable.SetActive(Plugin.SlotData.EarlyRocket);
                }
                else
                {
                    enable.SetActive(ExpectedState || enable.GetComponent<BonusScript>());
                }
            }

            state = ExpectedState;
        }
    }

    public class AreaStateOverride_Demo : AreaStateOverride
    {
        protected override bool ExpectedState => !APAreaStateManager.FullGameUnlocked;

        public override void Awake()
        {
            var orig = gameObject.GetComponent<DisableAreaScript_Demo>();
            if (orig)
            {
                toDisable = orig.disableThisAreaWhenActive;
                toEnable = orig.enableThisAreaWhenActive;
            }
        }

        public override void FixedUpdate()
        {
            if (state == ExpectedState)
                return;

            foreach (var disable in toDisable)
            {
                if (!disable)
                    continue;
                disable.SetActive(!ExpectedState || disable.GetComponent<BonusScript>());
            }

            foreach (var enable in toEnable)
            {
                if (!enable)
                    continue;
                // Psycho taxi gets disabled by demo mode... Do I want this to be the case?
                //if (enable.gameObject.name.Equals("Tile Lab Cubo Spicchio") ||
                //    enable.gameObject.name.Equals("LabPareteQuad Diagonale") ||
                //    enable.gameObject.name.Equals("Tile Lab Cubo Spicchio (4)") ||
                //    enable.gameObject.name.Equals("LabPareteQuad Diagonale (1)") ||
                //    enable.gameObject.name.Equals("Tile Lab Cubo Spicchio (5)"))
                //{
                //    enable.SetActive(false);
                //    continue;
                //}
                if (enable.GetComponent<TrueDemoWallScript>()) // True demo wall needs to send the item, even in non-demo mode
                {
                    enable.SetActive(true);
                    enable.gameObject.GetComponent<Collider>().isTrigger = !ExpectedState;
                    if (!ExpectedState && !enable.gameObject.GetComponent<TrueDemoWallFullGame>())
                    {
                        enable.gameObject.AddComponent<TrueDemoWallFullGame>();
                    }
                }
                else
                {
                    enable.SetActive(ExpectedState || enable.GetComponent<BonusScript>());
                }
            }

            foreach (var portal in PortalScript.list.Where(p => !p.USE_IN_DEMO_))
            {
                // Don't toggle state of levels that are directly unlocked by other items, or Morio's Mind (you already can't talk to Dream Machine Morio to activate the portal in demo mode)
                // Use names rather than target level ids to avoid entrance rando shenanigans down the line
                if (portal.gameObject.name.Equals("Portal Level Psycho Taxi 1") ||
                    portal.gameObject.name.Equals("Portal Level Poop World") ||
                    portal.gameObject.name.Equals("Portal Level Sewers Fogne") ||
                    portal.gameObject.name.Equals("Portal Level Morio Mind") ||
                    portal.gameObject.name.Equals("Portal Level Rocket"))
                {
                    continue;
                }
                Plugin.Log($"Setting portal state: {portal.name} | {!ExpectedState}");
                portal.gameObject.SetActive(!ExpectedState);
            }

            state = ExpectedState;
        }
    }

    public class AreaStateOverride_GymMembership : AreaStateOverride_Demo
    {
        protected override bool ExpectedState {
            get
            {
                switch (Plugin.SlotData.GymGearsUnlockCondition)
                {
                    case YTGVSlotData.LevelUnlockCondition.Exclude:
                        return true;
                    case YTGVSlotData.LevelUnlockCondition.Open:
                        return false;
                    case YTGVSlotData.LevelUnlockCondition.Item:
                        return !APAreaStateManager.GymMembership;
                    case YTGVSlotData.LevelUnlockCondition.FullGame:
                        return !APAreaStateManager.FullGameUnlocked;
                    case YTGVSlotData.LevelUnlockCondition.Special:
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public override void FixedUpdate()
        {
            if (state == ExpectedState)
                return;

            foreach (var disable in toDisable)
            {
                if (!disable)
                    continue;
                disable.SetActive(!ExpectedState || disable.GetComponent<BonusScript>());
            }

            foreach (var enable in toEnable)
            {
                if (!enable)
                    continue;
                enable.SetActive(ExpectedState || enable.GetComponent<BonusScript>());
            }

            state = ExpectedState;
        }
    }

    public class AreaStateOverride_LabLocked : AreaStateOverride
    {
        protected override bool ExpectedState => Plugin.SlotData.LockedMoriosLab && !APAreaStateManager.LabDoorUnlocked;

        public Collider[] LabOutsideDoorColliders;
        public MeshRenderer LabRenderer;
        public Texture LabOutsideUnlockedTexture;
        public Texture LabOutsideLockedTexture;

        public override void Awake()
        {
            LabOutsideDoorColliders = FindObjectsOfType<PortalScript>()
                .Where(p => p.name.Equals("Portal Garden to Lab") || p.name.Equals("Portal Outside to Lab"))
                .Select(p => p.gameObject.GetComponent<Collider>()).ToArray();

            LabRenderer = FindObjectsOfType<MeshRenderer>().First(o => o.name.Equals("MODELlab"));
            LabOutsideUnlockedTexture = LabRenderer.material.mainTexture;

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("YellowTaxiAP.Resources.lab_door_closed.png"))
            {
                Plugin.Log("Reading Texture");
                var data = new byte[stream.Length];
                stream.Read(data, 0, data.Length);

                var texture = new Texture2D(1024, 1024);
                texture.LoadImage(data);
                texture.filterMode = LabOutsideUnlockedTexture.filterMode;
                LabOutsideLockedTexture = texture;
            }
        }

        public override void FixedUpdate()
        {
            if (state == ExpectedState)
                return;

            foreach (var disable in toDisable)
            {
                if (!disable)
                    continue;
                disable.SetActive(!ExpectedState || disable.GetComponent<BonusScript>());
            }

            foreach (var enable in toEnable)
            {
                if (!enable)
                    continue;
                enable.SetActive(ExpectedState || enable.GetComponent<BonusScript>());
            }

            foreach (var collider in LabOutsideDoorColliders)
            {
                collider.isTrigger = !ExpectedState;
            }

            LabRenderer.material.mainTexture = ExpectedState ? LabOutsideLockedTexture : LabOutsideUnlockedTexture;

            state = ExpectedState;
        }
    }

    public class AreaStateOverride_Wardrobe : AreaStateOverride
    {
        protected override bool ExpectedState => Plugin.SlotData.LockedMoriosWardrobe && !APAreaStateManager.WardrobeUnlocked;
        public override void Awake()
        {
            var orig = gameObject.GetComponent<DisableAreaScript_GearsNumber>();
            if (orig)
            {
                toDisable = orig.disableThisAreaWhenActive.Where(o => !o.GetComponent<BonusScript>()).ToArray();
                toEnable = orig.enableThisAreaWhenActive;
            }
        }
    }

    public class AreaStateOverride_Sewer : AreaStateOverride
    {
        protected override bool ExpectedState => SewerUnlocked;

        public static bool SewerUnlocked
        {
            get
            {
                return Plugin.SlotData.GymGearsUnlockCondition switch
                {
                    YTGVSlotData.LevelUnlockCondition.Exclude => false,
                    YTGVSlotData.LevelUnlockCondition.Open => true,
                    YTGVSlotData.LevelUnlockCondition.Item => APAreaStateManager.SewerKeyReceived,
                    YTGVSlotData.LevelUnlockCondition.FullGame => APAreaStateManager.FullGameUnlocked,
                    YTGVSlotData.LevelUnlockCondition.Special => APSwitchManager.OrangeSwitchUnlocked &&
                                                                 APAreaStateManager.FullGameUnlocked,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }
        public override void Awake()
        {
            // Do nothing. This doesn't correspond to a real DisableAreaScript
        }
    }

    public class RainbowArrowDemo : MonoBehaviour
    {
        private bool ExpectedState => APAreaStateManager.FullGameUnlocked;
        private bool? state;

        private void FixedUpdate()
        {
            if (state == ExpectedState)
                return;
            
            GetComponentInChildren<LineRenderer>(true).gameObject.SetActive(ExpectedState);
            state = ExpectedState;
        }
    }
}
