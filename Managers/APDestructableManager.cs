using System;
using System.Collections.Generic;
using System.Linq;
using Archipelago.Gifting.Net.Traits;
using UnityEngine;
using Archipelago.Gifting.Net.Versioning.Gifts;
using Archipelago.Gifting.Net.Versioning.Gifts.Current;

namespace YellowTaxiAP.Managers
{
    public class APDestructableManager
    {
        public APDestructableManager()
        {
            On.DetailScript.DestructiveCollision += DetailScript_DestructiveCollision;
        }

        public Tuple<string, HashSet<string>>? GetGiftFromDestructable(string particle)
        {
            switch(particle)
            {
                case "EffectTronchetto1":                       // Log (From trees)
                case "EffectTronchetto2":                       // Log (From palm trees)
                    return new Tuple<string, HashSet<string>>("Log", [GiftFlag.Wood, GiftFlag.Material, GiftFlag.Resource]);
                case "EffectWoodPlank":                         // Plank (From boxes, etc.)
                    return new Tuple<string, HashSet<string>>("Plank", ["Lumber", GiftFlag.Wood, GiftFlag.Material, GiftFlag.Resource]);
                case "EffectLeaf1":                             // Leaf (From trees, bushes)
                    return new Tuple<string, HashSet<string>>("Leaf", [GiftFlag.Grass, "Fiber", GiftFlag.Material, GiftFlag.Resource]);
                case "EffectLeafAutumn":                        // Autumn Leaf
                case "EffectLeaf2":
                    return new Tuple<string, HashSet<string>>("Autumn Leaf", [GiftFlag.Grass, "Fiber", "Fall", GiftFlag.Material, GiftFlag.Resource]);
                case "EffectPoop1":                             // Poop
                    return new Tuple<string, HashSet<string>>("Poop", ["Fertilizer", "Stinky", "Pink"]);
                case "Distrutto Lampada Da Terra 3":            // Lamp
                    return new Tuple<string, HashSet<string>>("Lamp", [GiftFlag.Light, "Furniture"]);
                case "Distrutto Wet Floor Sign":                // Wet Floor Sign
                    return new Tuple<string, HashSet<string>>("Wet Floor Sign", ["Plastic"]);
                case "EffectRinghieraLab":                      // Black/Yellow Caution Fence
                    return new Tuple<string, HashSet<string>>("Warning Fence Piece", ["Yellow", "Black", "Plastic", "Broken"]);
                case "Distrutto Paletto ToslaHQ":
                    return new Tuple<string, HashSet<string>>("Caution Pillar", ["Yellow", "Black"]);
                case "Distrutto Transenna":                     // Red/White Construction Fence
                    return new Tuple<string, HashSet<string>>("Construction Fence", ["Red", "White", GiftFlag.Metal]);
                case "Distrutto Cono Stradale":                 // Traffic Cone
                    return new Tuple<string, HashSet<string>>("Traffic Cone", ["Rubber", "Orange", "White"]);
                case "Distrutto Cartello Speed Limit 50":       // Speed Limit Sign (50)
                case "Distrutto Cartello Speed Limit 70":       // Speed Limit Sign (70)
                case "Distrutto Cartello Up Right":             // Up Right Turn Sign
                case "Distrutto Cartello Up Left":              // Up Left Turn Sign
                    return new Tuple<string, HashSet<string>>("Sign", [GiftFlag.Metal]);
                case "Distrutto Semaforo":                      // Traffic Light
                    return new Tuple<string, HashSet<string>>("Traffic Light", [GiftFlag.Light, "Green", "Yellow", "Red", GiftFlag.Metal]);
                case "Distrutto GarbageBin Small":              // Garbage can
                    return new Tuple<string, HashSet<string>>("Garbage Can", [GiftFlag.Metal, "Container", "Trash", "Litter", "Stinky"]);
                case "Distrutto Cestino Ufficio":               // Wastebasket
                    return new Tuple<string, HashSet<string>>("Wastebasket", [GiftFlag.Metal, "Container", "Trash", "Litter"]);
                case "EffectGarbage1":                          // Banana peel
                    return new Tuple<string, HashSet<string>>("Banana Peel", ["Trash", "Litter", "Stinky"]);
                case "EffectGarbage2":                          // Crumpled paper
                    return new Tuple<string, HashSet<string>>("Crumpled Paper", ["Paper", "Litter", "Trash"]);
                case "EffectGarbage3":                          // Human foot
                    return new Tuple<string, HashSet<string>>("Severed Foot", ["Bone", "Trash", "Stinky"]);
                case "EffectGarbage4":                          // Spring
                    return new Tuple<string, HashSet<string>>("Spring", [GiftFlag.Metal, "Trash", "Stinky"]);
                case "EffectBone1":                             // Bone
                    return new Tuple<string, HashSet<string>>("Bone", ["Bone"]);
                case "Distrutto Lampione":                      // Streetlight
                    return new Tuple<string, HashSet<string>>("Streetlight", [GiftFlag.Light, GiftFlag.Metal]);
                case "Distrutto Idrante":                       // Fire Hydrant
                    return new Tuple<string, HashSet<string>>("Fire Hydrant", [GiftFlag.Metal, "Red"]);
                case "Dettaglio Idrante Rotto":                 // Water spout from broken fire hydrant
                    return new Tuple<string, HashSet<string>>("Broken Fire Hydrant", ["Water", "Freshwater", "Liquid", "Broken"]);
                case "EffectSandPuff1":                         // Sand
                    return new Tuple<string, HashSet<string>>("Sand", ["Sand", "Beach", "Desert"]);
                case "Distrutto Secchiello":                    // Bucket
                    return new Tuple<string, HashSet<string>>("Bucket", ["Container", "Plastic", "Beach"]);
                case "EffectWaterDrop1":                        // Water
                    return new Tuple<string, HashSet<string>>("Water", ["Water", "Liquid"]);
                case "Distrutto Paletta Sabbia":                // Plastic Shovel
                    return new Tuple<string, HashSet<string>>("Shovel", [GiftFlag.Tool, "Plastic"]);
                case "Distrutto Alga":                          // Seaweed
                    return new Tuple<string, HashSet<string>>("Seaweed", ["Algae", "Marine", "Fiber", GiftFlag.Grass]);
                case "Distrutto Corallo 1":                     // Coral (Red)
                    return new Tuple<string, HashSet<string>>("Red Coral", ["Marine", "Beach", "Red"]);
                case "Distrutto Corallo 2":                     // Coral (Purple)
                    return new Tuple<string, HashSet<string>>("Purple Coral", ["Marine", "Beach", "Purple"]);
                case "Distrutto Corallo 3":                     // Coral (Blue)
                    return new Tuple<string, HashSet<string>>("Blue Coral", ["Marine", "Beach", "Blue"]);
                case "Distrutto Corallo 4":                     // Coral (Light Green)
                    return new Tuple<string, HashSet<string>>("Light Green Coral", ["Marine", "Beach", "YellowGreen"]);
                case "Distrutto Corallo 5":                     // Coral (Orange)
                    return new Tuple<string, HashSet<string>>("Orange Coral", ["Marine", "Beach", "Orange"]);
                case "Distrutto Corallo 6":                     // Coral (Green)
                    return new Tuple<string, HashSet<string>>("Green Coral", ["Marine", "Beach", "Green"]);
                case "Distrutto Cabina Mare":                   // Beach Changing Room
                    return new Tuple<string, HashSet<string>>("Changing Room", ["Beach"]);
                case "Distrutto Ombrellone":                    // Beach Umbrella
                    return new Tuple<string, HashSet<string>>("Umbrella", ["Furniture", "Beach"]);
                case "Distrutto Lettino":                       // Beach Chair
                    return new Tuple<string, HashSet<string>>("Beach Chair", ["Furniture", "Beach"]);
                case "EffectLetter1":                           // Letter (From mailbox)
                    return new Tuple<string, HashSet<string>>("Letter", ["Paper", "Scroll", "Book"]);
                case "Distrutto Panchina":                      // Bench
                    return new Tuple<string, HashSet<string>>("Bench", ["Furniture", "Lumber", GiftFlag.Wood]);
                case "Distrutto Proteins":                      // "Ultra Protein" supplement
                    return new Tuple<string, HashSet<string>>("Protein", ["Guts", GiftFlag.Buff, GiftFlag.Consumable]);
                case "Distrutto Cyclette":                      // Exercise Bike
                    return new Tuple<string, HashSet<string>>("Exercise Bike", ["Furniture", "Machine", GiftFlag.Buff]);
                case "Distrutto Borraccia":                     // Water bottle
                    return new Tuple<string, HashSet<string>>("Water Bottle", [GiftFlag.Drink, "Water", "Freshwater", "Liquid", GiftFlag.Consumable, "Plastic"]);
                case "Distrutto Water Dispenser":               // Water Cooler
                    return new Tuple<string, HashSet<string>>("Water Cooler", [GiftFlag.Drink, "Water", "Freshwater", "Liquid", GiftFlag.Consumable, "Furniture"]);
                case "Distrutto Gymbag":                        // Gym bag
                    return new Tuple<string, HashSet<string>>("Gym Bag", ["Container", "Cloth", "Clothing", "Stinky"]);
                case "Distrutto Dumbell":                       // Dumbbell
                    return new Tuple<string, HashSet<string>>("Dumbbell", ["Iron", GiftFlag.Metal, GiftFlag.Buff]);
                case "Distrutto Panca Piana":                   // Workout bench
                    return new Tuple<string, HashSet<string>>("Workout Bench", ["Furniture", "Stinky"]);
                case "Distrutto Portatile":                     // Laptop
                    return new Tuple<string, HashSet<string>>("Laptop", ["Electronics", "Luxury", GiftFlag.Metal]);
                case "Distrutto Desk Pc Monitor":               // Monitor
                    return new Tuple<string, HashSet<string>>("Computer Monitor", ["Electronics", GiftFlag.Metal, "Glass"]);
                case "Distrutto Tastiera":                      // Keyboard
                    return new Tuple<string, HashSet<string>>("Keyboard", ["Electronics", "Plastic"]);
                case "Distrutto Mouse":                         // Mouse
                    return new Tuple<string, HashSet<string>>("Computer Mouse", ["Electronics", "Plastic"]);
                case "Distrutto Telefono Fisso":                // Telephone
                    return new Tuple<string, HashSet<string>>("Telephone", ["Electronics", "Plastic"]);
                case "Distrutto Graffettatrice":                // Stapler
                    return new Tuple<string, HashSet<string>>("Stapler", [GiftFlag.Tool, GiftFlag.Metal]);
                case "Distrutto Porta Penne":                   // Pen holder (contains one pen and a pair of scissors)
                    return new Tuple<string, HashSet<string>>("Pen Holder", ["Plastic", "Container", GiftFlag.Tool]);
                case "Distrutto Quadernone":                    // Notebook/binder
                    return new Tuple<string, HashSet<string>>("Notebook", ["Book", "Paper"]);
                case "Distrutto Pianta da Interno":             // Houseplant
                    return new Tuple<string, HashSet<string>>("Houseplant", ["Herb", GiftFlag.Grass, "Fiber", "Furniture"]);
                case "Distrutto Pianta da Interno Morta":       // Dead Houseplant
                    return new Tuple<string, HashSet<string>>("Dead Houseplant", ["Fiber", "Furniture"]);
                case "Distrutto Giocattolo Triangle":           // Triangle Toy
                    return new Tuple<string, HashSet<string>>("Triangle Toy", ["Toy", "Pink", "Purple"]);
                case "Distrutto Giocattolo Fulmine":            // Lightning Toy
                    return new Tuple<string, HashSet<string>>("Lightning Toy", ["Toy", "Yellow", "Orange", GiftFlag.Energy]);
                case "Distrutto Giocattolo Star":               // Star Toy
                    return new Tuple<string, HashSet<string>>("Star Toy", ["Toy", "LightCyan", "Blue"]);
                case "Distrutto Giocattolo Saturn":             // Saturn Toy
                    return new Tuple<string, HashSet<string>>("Saturn Toy", ["Toy", "Pink", "Yellow"]);
                case "Distrutto Bowling Pin":                   // Bowling Pin. Apparently Bowling Pins have a wooden core so, I guess that?
                    return new Tuple<string, HashSet<string>>("Bowling Pin", ["White", "Red", GiftFlag.Wood]);
                case "Distrutto Barile Fogne":                  // Waste Barrel
                    return new Tuple<string, HashSet<string>>("Radioactive Waste Barrel", ["Radioactive", "Chemicals", "Container", GiftFlag.Metal]);
                case "Dettaglio Vetro Negozio Rotto":           // Broken shop glass. Glass doesn't yet exist in gifting, but it's worth adding because what else
                case "EffectGlassPiece":                        // Broken glass shard
                    return new Tuple<string, HashSet<string>>("Glass", ["Glass", "Broken"]);
                case "Distrutto Menu Strada":                   // Menu Sign
                    return new Tuple<string, HashSet<string>>("Menu Sign", ["Furniture"]);
                case "Distrutto HotDog Stand":                  // Hot Dog Stand
                    return new Tuple<string, HashSet<string>>("Hot Dog Stand", [GiftFlag.Meat, GiftFlag.Food, GiftFlag.Consumable, GiftFlag.Metal]);
                case "Distrutto Salame 1":                      // Salami
                case "Distrutto Salame 2":
                case "Distrutto Salame 3":
                case "Distrutto Salame 4":
                case "Distrutto Salame 5":
                    return new Tuple<string, HashSet<string>>("Salami", [GiftFlag.Meat, GiftFlag.Food, GiftFlag.Consumable]);
                case "Distrutto Formaggio 1":                   // Cheese
                case "Distrutto Formaggio 2":
                case "Distrutto Formaggio 3":
                case "Distrutto Formaggio 4":
                case "Distrutto Formaggio 5":
                    return new Tuple<string, HashSet<string>>("Cheese", ["ArtisanGood", GiftFlag.Food, GiftFlag.Consumable]);
                case "Distrutto Tavolo":                        // Table
                case "Distrutto Tavolo  Negozio":               // Shop table
                    return new Tuple<string, HashSet<string>>("Table", ["Furniture"]);
                case "Distrutto Antenna 1":                     // TV Antenna
                    return new Tuple<string, HashSet<string>>("Antenna", [GiftFlag.Metal]);
                case "Distrutto Antenna 2":                     // Satellite Dish
                    return new Tuple<string, HashSet<string>>("Satellite Dish", [GiftFlag.Metal]);
                case "EffectMorioFace":                         // Morio Face (From Morio's Dream)
                    return new Tuple<string, HashSet<string>>("Morio Face", ["Dreamlike"]);
                case "Distrutto Cassetta":                      // Toolbox
                    return new Tuple<string, HashSet<string>>("Toolbox", ["Container", GiftFlag.Metal]);
                case "Distrutto Chiave Inglese":                // Wrench
                    return new Tuple<string, HashSet<string>>("Wrench", [GiftFlag.Tool, GiftFlag.Metal]);
                case "Distrutto Bullone":                       // Bolt
                    return new Tuple<string, HashSet<string>>("Bolt", [GiftFlag.Metal, GiftFlag.Material, GiftFlag.Resource]);
                case "Distrutto Dado Metallo":                  // Nut
                    return new Tuple<string, HashSet<string>>("Nut", [GiftFlag.Metal, GiftFlag.Material, GiftFlag.Resource]);
                case "Distrutto Sedia":                         // Basic wooden chair
                case "Distrutto Sedia Bowling":                 // Bowling chair
                case "Distrutto Sedia a Rotelle":               // Office chair
                case "Distrutto Chair 50":                      // Mosk Rocket Chair
                    return new Tuple<string, HashSet<string>>("Chair", ["Furniture"]);
                case "EffectSlime":                             // Slime
                    return new Tuple<string, HashSet<string>>("Slime", ["Slime", "Goo", "Green"]);
            }
            return null;
        }

        private void DetailScript_DestructiveCollision(On.DetailScript.orig_DestructiveCollision orig, DetailScript self, UnityEngine.Vector3 velocity)
        {
            string destructableSpawns = string.Empty;
            var unkStr = "gifted";
            if (self.spawnThisOnDestruction.Length > 0)
            {
                List<GameObject> spawns = new List<GameObject>();
                int count = 0;
                var distinctSpawns = self.spawnThisOnDestruction.Distinct().ToArray();
                foreach (var spawn in distinctSpawns)
                {
                    count++;
                    var spawnName = GetGiftFromDestructable(spawn.name);
                    if (spawnName == null)
                    {
                        unkStr = "spawned new object";
                        destructableSpawns = spawn.name + ", ";
                        spawns.Add(spawn);
                        GUIUtility.systemCopyBuffer = spawn.name;
                        break;
                    }
                    else
                    {
                        destructableSpawns += $"{spawnName.Item1}, ";
                    }
                }

                destructableSpawns = destructableSpawns.Substring(0, destructableSpawns.Length - 2);

                if (spawns.Count > 0)
                {
                    if (count != distinctSpawns.Length)
                        unkStr += $" (there are still {distinctSpawns.Length - count} remaining)";
                    self.spawnThisOnDestruction = spawns.ToArray();
                }
            }
            else
                destructableSpawns = "(Nothing)";
            if(unkStr.Contains("new"))
                Plugin.Log($"Destroyed {self.gameObject.name} and {unkStr}: {destructableSpawns}");
            orig(self, velocity);
        }
    }
}
