using System.Linq;
using UnityEngine;
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
        protected GameObject[] toDisable;
        protected GameObject[] toEnable;

        public abstract void Awake(); // Need to properly populate toDisable/toEnable

        public virtual void FixedUpdate()
        {
            if (state == ExpectedState)
                return;

            foreach (var disable in toDisable)
            {
                disable?.SetActive(!ExpectedState || disable.GetComponent<BonusScript>());
            }

            foreach (var enable in toEnable)
            {
                enable?.SetActive(ExpectedState || enable.GetComponent<BonusScript>());
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
        }
    }

    public class AreaStateOverride_Doggo : AreaStateOverride
    {
        protected override bool ExpectedState => APAreaStateManager.DoggoReceived;

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
                if (disable.name.Equals("Dettaglio Albero1")) // Keep this tree. Mosk normally replaces it.
                {
                    //disable.SetActive(true); // Don't just always enable it though. If you destroyed it this creates an invincible tree
                }
                else
                {
                    disable.SetActive(!ExpectedState || disable.GetComponent<BonusScript>());
                }
            }

            foreach (var enable in toEnable)
            {
                if (enable.name.Equals("Person Alien Mosk Good")) // Don't add Mosk. He brings you to the moon.
                {
                    enable.SetActive(false);
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
                disable?.SetActive(!ExpectedState || disable.GetComponent<BonusScript>());
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
                    portal.gameObject.name.Equals("Portal Level Morio Mind") ||
                    portal.gameObject.name.Equals("Portal Level Rocket"))
                {
                    continue;
                }
                portal.gameObject.SetActive(!ExpectedState);
            }

            state = ExpectedState;
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
