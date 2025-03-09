using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace YellowTaxiAP.Managers
{
    public class APDataManager
    {
        public APDataManager()
        {
            //On.Data.LoadGame += Data_LoadGame;
            //On.Data.SaveGame += Data_SaveGame;
        }

        private void Data_LoadGame(On.Data.orig_LoadGame orig)
        {
            orig();
        }

        /// <summary>
        /// TODO: Disable saving when using the mod
        /// </summary>
        private void Data_SaveGame(On.Data.orig_SaveGame orig, bool forceSave)
        {
            orig(forceSave);
        }
    }
}
