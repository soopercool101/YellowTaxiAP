using UnityEngine;

namespace YellowTaxiAP.Managers
{
    public class APControlsManager
    {
        public static bool InvertControls { get; set; } = false;

        public APControlsManager()
        {
            On.Controls.GameAxis += Controls_GameAxis;
        }


        private Vector2 Controls_GameAxis(On.Controls.orig_GameAxis orig, int playerIndex)
        {
            var vec = orig(playerIndex);
            if (InvertControls)
            {
                vec = new Vector2(-1f * vec.x, -1f * vec.y);
            }
            return vec;
        }
    }
}
