using UnityEngine;
using Object = UnityEngine.Object;

namespace YellowTaxiAP.Helpers
{
    public static class ObjectHelper
    {
        public static void DestroyRecursive(Transform obj, bool destroyThis = true)
        {
            for (var i = obj.childCount - 1; i >= 0; i--)
            {
                DestroyRecursive(obj.GetChild(i));
            }

            if (destroyThis)
            {
                Object.Destroy(obj.gameObject);
            }
        }

        public static void DestroyImmediateRecursive(Transform obj, bool destroyThis = true)
        {
            for (var i = obj.childCount - 1; i >= 0; i--)
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
