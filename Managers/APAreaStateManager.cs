using System;
using System.Linq;
using UnityEngine;
using YellowTaxiAP.Behaviours;
using Object = UnityEngine.Object;

namespace YellowTaxiAP.Managers
{
    public class APAreaStateManager
    {
        public static bool RocketEnabled = false;
        public static bool MindPasswordReceived = false;
        public static bool GelaToniReceived = false;
        public static bool PizzaKingReceived = false;
        public static bool DoggoReceived = false;
        public static bool FullGameUnlocked = false;
        public static bool LabDoorUnlocked = false;
        public static bool GymMembership = false;

        public APAreaStateManager()
        {
            On.PlayerScript.Awake += PlayerScript_Awake;

            On.DisableAreaScript_BeatedFinalBoss.Start += DisableAreaScript_BeatedFinalBoss_Start;
            On.DisableAreaScript_GoldenSpring.Start += DisableAreaScript_GoldenSpring_Start;
            On.DisableAreaScript_MorioMindPassword.Start += DisableAreaScript_MorioMindPassword_Start;
            On.MorioDreamMachineScript.FixedUpdate += MorioDreamMachineScript_FixedUpdate;
            On.DisableAreaScript_GearsNumber.Start += DisableAreaScript_GearsNumber_Start;
            On.DisableAreaScript_EventMode.Start += DisableAreaScript_EventMode_Start;
            On.DisableAreaScript_GrannyIsland_IceCream.Start += DisableAreaScript_GrannyIsland_IceCream_Start;
            On.DisableAreaScript_GrannyIsland_KingPizza.Start += DisableAreaScript_GrannyIsland_KingPizza_Start;
            On.DisableAreaScript_StuckDoggoTalk.Awake += DisableAreaScript_StuckDoggoTalk_Awake;
            On.DisableAreaScript_StuckDoggoTalk.DisableEnable += DisableAreaScript_StuckDoggoTalk_DisableEnable;
            On.DisableAreaScript_Demo.Start += DisableAreaScript_Demo_Start;
            On.TrueDemoWallScript.OnCollisionEnter += TrueDemoWallScript_OnCollisionEnter;
            On.RainbowArrowScript.Awake += RainbowArrowScript_Awake;
        }

        private void PlayerScript_Awake(On.PlayerScript.orig_Awake orig, PlayerScript self)
        {
            // Hubworld state changes
            if (GameplayMaster.instance.levelId == Data.LevelId.Hub)
            {
                // Open Granny's Island
                if (Plugin.SlotData.OpenGrannysIsland)
                {
                    var count = 0;
                    foreach (var mesh in Object.FindObjectsOfType<MeshFilter>())
                    {
                        if (mesh.gameObject.name == "ModelTilesCuboDiagonale" && mesh.transform.parent.gameObject.name == "Tile Grass Cubo Diagonale")
                        {
                            if (mesh.mesh.name.Equals(" Instance"))
                            {
                                mesh.transform.parent.position = Math.Floor(mesh.transform.parent.position.x) switch
                                {
                                    65 => new Vector3(127, 10, 0),
                                    75 => new Vector3(142, 10, 0),
                                    _ => mesh.transform.parent.position
                                };
                                //mesh.transform.localRotation = new Quaternion(-45, 0, 0, 0);
                                mesh.transform.Rotate(0, 0, -225);
                                Plugin.Log($"Rotation: {mesh.transform.rotation}");
                                Plugin.Log($"Local Rotation: {mesh.transform.localRotation}");
                                Plugin.Log(mesh.mesh.name + " Moved!");
                                count++;

                                if (count >= 2)
                                    break;
                            }
                        }
                    }
                }

                // Locked lab
                if (Plugin.SlotData.LockedMoriosLab && !LabDoorUnlocked)
                {
                    var sign = Object.FindObjectsOfType<MeshFilter>()
                        .Last(o => o.gameObject.name.Equals("ModelObjectSign") && o.transform.parent.name.Equals("Sign Right"));
                    Plugin.Log(sign.transform.parent.parent.gameObject.name);
                    var noSign = Object.FindObjectsOfType<DisableAreaScript_EventMode>()[0].enableThisAreaWhenActive[0].transform.GetChild(0);
                    Plugin.Log(sign.gameObject.GetComponent<MeshRenderer>()?.material.name ?? "<NULL>");
                    Plugin.Log(sign.name);
                    foreach (var component in sign.gameObject.GetComponents<Object>())
                    {
                        Plugin.Log($"  - {component.GetType()}");
                    }
                    Plugin.Log(noSign.name);
                    foreach (var component in noSign.gameObject.GetComponents<Object>())
                    {
                        Plugin.Log($"  - {component.GetType()}");
                    }
                    //Plugin.Log(noSign.gameObject.GetComponent<MeshRenderer>()?.material.name ?? "<NULL>");
                    sign.gameObject.GetComponent<MeshRenderer>().material = noSign.gameObject.GetComponent<MeshRenderer>().material;
                    sign.transform.parent.localPosition = new Vector3(-120, 10, 40);
                    sign.transform.Rotate(0, 0, 90);
                    sign.transform.parent.gameObject.GetComponent<Collider>().enabled = false;

                    var labEnableDisable = Object.FindObjectOfType<DisableAreaScript_EventMode>();

                    var enable = labEnableDisable.enableThisAreaWhenActive.ToList();
                    enable.Add(sign.transform.parent.gameObject);
                    var disable = labEnableDisable.disableThisAreaWhenActive.ToList();
                    disable.Add(Object.FindObjectsOfType<PortalScript>().First(p => p.name.Equals("Portal Lab to Garden")).gameObject);
                    var locker = self.gameObject.AddComponent<AreaStateOverride_LabLocked>();
                    locker.toDisable = disable.ToArray();
                    locker.toEnable = enable.ToArray();
                }
            }
            orig(self);
        }

        private void TrueDemoWallScript_OnCollisionEnter(On.TrueDemoWallScript.orig_OnCollisionEnter orig, TrueDemoWallScript self, Collision collision)
        {
            if (!(collision.gameObject == PlayerScript.instance.gameObject) || self.waitTimer > 0.0 || DialogueScript.instance)
                return;

            orig(self, collision);
        }

        private void RainbowArrowScript_Awake(On.RainbowArrowScript.orig_Awake orig, RainbowArrowScript self)
        {
            orig(self);
            if (self.disableInDemo)
            {
                self.gameObject.AddComponent<RainbowArrowDemo>();
            }
        }

        private void DisableAreaScript_Demo_Start(On.DisableAreaScript_Demo.orig_Start orig, DisableAreaScript_Demo self)
        {
            if (self.gameObject.name.Equals("DisableArea Demo Gym"))
            {
                self.gameObject.AddComponent<AreaStateOverride_GymMembership>();
            }
            else
            {
                self.gameObject.AddComponent<AreaStateOverride_Demo>();
            }
        }

        /// <summary>
        /// Replace with new script
        /// </summary>
        private void DisableAreaScript_StuckDoggoTalk_Awake(On.DisableAreaScript_StuckDoggoTalk.orig_Awake orig, DisableAreaScript_StuckDoggoTalk self)
        {
            DisableAreaScript_StuckDoggoTalk.instance = self; // Need to set this or doggo throws an error when talking
            self.gameObject.AddComponent<AreaStateOverride_Doggo>();
        }

        private void DisableAreaScript_StuckDoggoTalk_DisableEnable(On.DisableAreaScript_StuckDoggoTalk.orig_DisableEnable orig, DisableAreaScript_StuckDoggoTalk self)
        {
            // Do nothing. Handled by the override script
        }

        /// <summary>
        /// Replace with new script
        /// </summary>
        private void DisableAreaScript_GrannyIsland_KingPizza_Start(On.DisableAreaScript_GrannyIsland_KingPizza.orig_Start orig, DisableAreaScript_GrannyIsland_KingPizza self)
        {
            self.gameObject.AddComponent<AreaStateOverride_PizzaKing>();
        }

        /// <summary>
        /// Replace with new script
        /// </summary>
        private void DisableAreaScript_GrannyIsland_IceCream_Start(On.DisableAreaScript_GrannyIsland_IceCream.orig_Start orig, DisableAreaScript_GrannyIsland_IceCream self)
        {
            self.gameObject.AddComponent<AreaStateOverride_GelaToni>();
        }

        private void DisableAreaScript_EventMode_Start(On.DisableAreaScript_EventMode.orig_Start orig, DisableAreaScript_EventMode self)
        {
            if (GameplayMaster.instance.levelId != Data.LevelId.Hub || !self.gameObject.GetComponent<AreaStateOverride_LabLocked>())
            {
                Plugin.Log(self.gameObject.name + " is attempting to disable and enable areas (EventMode)");
                orig(self);
            }
        }

        /// <summary>
        /// Disables the hat wardrobe in the earlygame.
        /// Bizarrely, also disables the coins/gear on top of the rocket. Would think that would be handled by the rocket handler.
        ///
        /// Override and enable everything in all contexts
        /// </summary>
        private void DisableAreaScript_GearsNumber_Start(On.DisableAreaScript_GearsNumber.orig_Start orig, DisableAreaScript_GearsNumber self)
        {
            Plugin.Log(self.gameObject.name + $" is attempting to disable and enable areas (Expected Gears: {self.activeWhenGearsLowerThanThis})");
            orig(self);
            foreach (var toDisable in self.disableThisAreaWhenActive)
            {
                toDisable?.SetActive(true);
                //var bonus = toDisable.GetComponent<BonusScript>();
                //Plugin.Log(bonus != null
                //    ? $"ToDisable: {toDisable.name} | ID: {APCollectableManager.GetID(bonus)}"
                //    : $"ToDisable: {toDisable.name}");
            }

            foreach (var toEnable in self.enableThisAreaWhenActive)
            {
                Plugin.Log($"ToEnable: {toEnable?.name ?? "<null>"}");
                toEnable?.SetActive(true);
            }
        }

        /// <summary>
        /// Re-implementation to allow Morio's Mind Password to be shuffled
        /// </summary>
        private void MorioDreamMachineScript_FixedUpdate(On.MorioDreamMachineScript.orig_FixedUpdate orig, MorioDreamMachineScript self)
        {
            if (!Tick.IsGameRunning)
                return;
            self.labWallTransform.gameObject.SetActive(!MindPasswordReceived);
            if (Data.morioMindDreamMachineUsedOnce[Data.gameDataIndex]) // todo: remove save file call
            {
                if (!self.lightBulb.enabled)
                    self.lightBulb.enabled = true;
            }
            else
                self.lightBulb.enabled = false;
            self.labWallText.enabled = Utility.AngleSin((float)(Tick.PassedTimePausable * 360.0 * 4.0)) < 0.89999997615814209;
        }

        /// <summary>
        /// Replace with new script
        /// </summary>
        private void DisableAreaScript_MorioMindPassword_Start(On.DisableAreaScript_MorioMindPassword.orig_Start orig, DisableAreaScript_MorioMindPassword self)
        {
            self.gameObject.AddComponent<AreaStateOverride_MorioPassword>();
        }

        /// <summary>
        /// I'm not convinced this one is used anywhere?
        /// </summary>
        private void DisableAreaScript_GoldenSpring_Start(On.DisableAreaScript_GoldenSpring.orig_Start orig, DisableAreaScript_GoldenSpring self)
        {
            Plugin.Log(self.gameObject.name + " is attempting to disable and enable areas (GoldenSpring)");
            orig(self);
            foreach (var toDisable in self.disableThisAreaWhenActive)
            {
                Plugin.Log($"ToDisable: {toDisable?.name ?? "<null>"}");
                //toDisable.SetActive(true);
            }

            foreach (var toEnable in self.enableThisAreaWhenActive)
            {
                Plugin.Log($"ToEnable: {toEnable?.name ?? "<null>"}");
                //toEnable.SetActive(true);
            }
        }

        /// <summary>
        /// Replace with new script
        /// </summary>
        private void DisableAreaScript_BeatedFinalBoss_Start(On.DisableAreaScript_BeatedFinalBoss.orig_Start orig, DisableAreaScript_BeatedFinalBoss self)
        {
            self.gameObject.AddComponent<AreaStateOverride_Rocket>();
        }
    }
}
