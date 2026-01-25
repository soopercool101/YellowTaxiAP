using UnityEngine;
using YellowTaxiAP.Managers;

namespace YellowTaxiAP.Behaviours
{
    public abstract class RenderableAndColliderUpdater : MonoBehaviour
    {
        protected abstract bool EnabledState { get; }
        private bool? currentState;

        public void FixedUpdate()
        {
            if (currentState == EnabledState)
                return;

            gameObject.GetComponent<Collider>().enabled = EnabledState;
            foreach (var renderer in gameObject.GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = EnabledState;
            }

            currentState = EnabledState;
        }
    }

    public class GoldenPropellerUpdater : RenderableAndColliderUpdater
    {
        protected override bool EnabledState => APCollectableManager.GoldenPropellerActive;
    }

    public class GoldenSpringUpdater : RenderableAndColliderUpdater
    {
        protected override bool EnabledState => APCollectableManager.GoldenSpringActive;
    }
}
