using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using YellowTaxiAP.Managers;

namespace YellowTaxiAP.Behaviours
{
    public abstract class RenderableAndColliderUpdater : MonoBehaviour
    {
        public abstract bool EnabledState { get; }
        public bool? currentState;

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
        public override bool EnabledState => APCollectableManager.GoldenPropellerActive;
    }

    public class GoldenSpringUpdater : RenderableAndColliderUpdater
    {
        public override bool EnabledState => APCollectableManager.GoldenSpringActive;
    }
}
