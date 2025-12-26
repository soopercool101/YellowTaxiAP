using UnityEngine;
using Object = UnityEngine.Object;

namespace YellowTaxiAP.Managers
{
    public class APBackroomsManager
    {
        public static void DeleteBackroomsObstacle()
        {
            if (GameplayMaster.instance.levelId == Data.LevelId.L16_Rocket)
            {
                GameObject backroomsDoor = GameObject.Find("Poster 50 9 (3)");

                if (backroomsDoor)
                {
                    Plugin.Log("Destroying " + backroomsDoor.gameObject.name);
                    Object.Destroy(backroomsDoor);
                }
            }
        }
    }
}
