using UnityEngine;
using Object = UnityEngine.Object;

namespace YellowTaxiAP.Helpers
{
    public static class ObjectHelper
    {
        public static void DestroyImmediateRecursive(Transform obj, bool destroyThis = true)
        {
            for (var i = 0; i < obj.childCount; i++)
            {
                DestroyImmediateRecursive(obj.GetChild(i));
            }

            if (destroyThis)
            {
                Object.DestroyImmediate(obj.gameObject);
            }
        }
    }
}
