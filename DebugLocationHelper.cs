using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace YellowTaxiAP
{
    public static class DebugLocationHelper
    {
        public static Dictionary<string, string> GrandmasIslandStart = new()
        {
            { "0_03_00037", "Entrance Coin #1" },
            { "0_03_00038", "Entrance Coin #2" },
            { "0_03_00086", "Entrance Coin #3" },
            { "0_03_00098", "Entrance Coin #4" },
            { "0_03_00117", "Entrance Coin #5" },
            { "0_03_00149", "Entrance Coin #6" },
        };

        public static Dictionary<string, string> GrandmasIslandMoat = new()
        {
            { "0_03_00151", "Coin In Moat #1" },
            { "0_03_00100", "Coin In Moat #2" },
            { "0_03_00088", "Coin In Moat #3" },
            { "0_03_00040", "Coin In Moat #4" },
            { "0_03_00021", "Coin In Moat #5" },
            { "0_03_00023", "Coin In Moat #6" },
            { "0_03_00025", "Coin In Moat #7" },
            { "0_03_00027", "Coin In Moat #8" },
            { "0_03_00029", "Coin In Moat #9" },
            { "0_03_00031", "Coin In Moat #10" },
            { "0_03_00036", "Coin In Moat #11" },
            { "0_03_00035", "Coin In Moat #12" },
            { "0_03_00034", "Coin In Moat #13" },
            { "0_03_00033", "Coin In Moat #14" },
            { "0_03_00032", "Coin In Moat #15" },
            { "0_03_00030", "Coin In Moat #16" },
            { "0_03_00028", "Coin In Moat #17" },
            { "0_03_00026", "Coin In Moat #18" },
            { "0_03_00024", "Coin In Moat #19" },
            { "0_03_00022", "Coin In Moat #20" },
            { "0_03_00020", "Coin In Moat #21" },
            { "0_03_00039", "Coin In Moat #22" },
            { "0_03_00087", "Coin In Moat #23" },
            { "0_03_00099", "Coin In Moat #24" },
            { "0_03_00150", "Coin In Moat #25" },
            { "0_21_00004", "Cheese In Moat" },
        };

        public static Dictionary<string, string> GrandmasIslandMain = new()
        {
            // Grandma's Island - Main Area
            { "0_03_00153", "Coin Bag on Road #1" },
            { "0_03_00384", "Coin Bag on Road #2" },
            { "0_03_00154", "Coin on Road #1" },
            { "0_03_00156", "Coin on Road #2" },
            { "0_03_00157", "Coin on Road #3" },
            { "0_03_00158", "Coin on Road #4" },
            { "0_03_00159", "Coin on Road #5" },
            { "0_03_00165", "Coin on Road #6" },
            { "0_03_00164", "Coin on Road #7" },
            { "0_03_00163", "Coin on Road #8" },
            { "0_03_00162", "Coin on Road #9" },
            { "0_03_00161", "Coin on Road #10" },
            { "0_03_00166", "Coin on Road #11" },
            { "0_03_00167", "Coin on Road #12" },
            { "0_03_00168", "Coin on Road #13" },
            { "0_03_00169", "Coin on Road #14" },
            { "0_03_00170", "Coin on Road #15" },
            { "0_03_00172", "Coin on Road #16" },
            { "0_03_00173", "Coin on Road #17" },
            { "0_03_00174", "Coin on Road #18" },
            { "0_03_00175", "Coin on Road #19" },
            { "0_03_00176", "Coin on Road #20" },
            { "0_03_00177", "Coin on Road #21" },
            { "0_03_00178", "Coin on Road #22" },
            { "0_03_00391", "Coin on Road #23" },
            { "0_03_00390", "Coin on Road #24" },
            { "0_03_00389", "Coin on Road #25" },
            { "0_03_00388", "Coin on Road #26" },
            { "0_03_00387", "Coin on Road #27" },
            { "0_03_00386", "Coin on Road #28" },
            { "0_03_00385", "Coin on Road #29" },
            { "0_03_00597", "Coin on Mountain Road #1" },
            { "0_03_00598", "Coin on Mountain Road #2" },
            { "0_03_00611", "Coin on Mountain Road #3" },
            { "0_03_00613", "Coin on Mountain Road #4" },
            { "0_03_00630", "Coin on Mountain Road #5" },
            { "0_09_00320", "Checkpoint on Road" },
            { "0_21_00002", "Cheese Near Lab Hill" },
            { "0_03_00241", "Coin in UFO Crash Site #1" },
            { "0_03_00239", "Coin in UFO Crash Site #2" },
            { "0_03_00237", "Coin in UFO Crash Site #3" },
            { "0_03_00234", "Coin in UFO Crash Site #4" },
            { "0_03_00232", "Coin in UFO Crash Site #5" },
            { "0_03_00230", "Coin in UFO Crash Site #6" },
            { "0_03_00228", "Coin in UFO Crash Site #7" },
            { "0_03_00227", "Coin in UFO Crash Site #8" },
            { "0_03_00226", "Coin in UFO Crash Site #9" },
            { "0_03_00225", "Coin in UFO Crash Site #10" },
            { "0_03_00224", "Coin in UFO Crash Site #11" },
            { "0_03_00229", "Coin in UFO Crash Site #12" },
            { "0_03_00231", "Coin in UFO Crash Site #13" },
            { "0_03_00233", "Coin in UFO Crash Site #14" },
            { "0_03_00235", "Coin in UFO Crash Site #15" },
            { "0_03_00238", "Coin in UFO Crash Site #16" },
            { "0_03_00240", "Coin in UFO Crash Site #17" },
            { "0_03_00242", "Coin in UFO Crash Site #18" },
            { "0_03_00244", "Coin in UFO Crash Site #19" },
            { "0_03_00245", "Coin in UFO Crash Site #20" },
            { "0_03_00246", "Coin in UFO Crash Site #21" },
            { "0_03_00247", "Coin in UFO Crash Site #22" },
            { "0_03_00248", "Coin in UFO Crash Site #23" },
            { "0_03_00243", "Coin in UFO Crash Site #24" },
            { "0_03_00236", "Chest in UFO Crash Site" },
            { "0_21_00005", "Cheese in Construction Site" },
            { "0_21_00006", "Cheese Near Construction Site" },
            { "0_21_00000", "Cheese Behind City" },
            { "0_03_00185", "Coin Behind City #1" },
            { "0_03_00184", "Coin Behind City #2" },
            { "0_03_00183", "Coin Behind City #3" },
            { "0_03_00181", "Coin Behind City #4" },
            { "0_03_00180", "Coin Behind City #5" },
            { "0_03_00179", "Coin Behind City #6" },
            { "0_03_00182", "Coin Bag Behind City" },
            { "0_03_00271", "Coin Bag on Fenced City Cliff #1" },
            { "0_03_00272", "Coin Bag on Fenced City Cliff #2" },
            { "0_03_00269", "Coin Near Oil Pump #1" },
            { "0_03_00299", "Coin Near Oil Pump #2" },
            { "0_03_00310", "Coin Near Oil Pump #3" },
            { "0_03_00317", "Coin Near Oil Pump #4" },
            { "0_03_00379", "Coin Near Oil Pump #5" },
            { "0_03_00160", "Chest by Poop House" },
            { "0_03_00018", "Coin in Underwater Maze #1" },
            { "0_03_00013", "Coin in Underwater Maze #2" },
            { "0_03_00011", "Coin in Underwater Maze #3" },
            { "0_03_00009", "Coin in Underwater Maze #4" },
            { "0_03_00007", "Coin in Underwater Maze #5" },
            { "0_03_00006", "Coin in Underwater Maze #6" },
            { "0_03_00004", "Coin in Underwater Maze #7" },
            { "0_03_00003", "Coin in Underwater Maze #8" },
            { "0_03_00002", "Coin in Underwater Maze #9" },
            { "0_03_00001", "Coin in Underwater Maze #10" },
            { "0_03_00005", "Coin in Underwater Maze #11" },
            { "0_03_00008", "Coin in Underwater Maze #12" },
            { "0_03_00010", "Coin in Underwater Maze #13" },
            { "0_03_00012", "Coin in Underwater Maze #14" },
            { "0_03_00014", "Coin in Underwater Maze #15" },
            { "0_03_00015", "Coin in Underwater Maze #16" },
            { "0_03_00016", "Coin in Underwater Maze #17" },
            { "0_03_00017", "Coin in Underwater Maze #18" },
            { "0_01_00013", "Gear - Underwater Maze" },
            { "0_03_00045", "Coin on Pier #1" },
            { "0_03_00046", "Coin on Pier #2" },
            { "0_03_00047", "Coin on Pier #3" },
            { "0_03_00048", "Coin on Pier #4" },
            { "0_03_00049", "Coin on Pier #5" },
            { "0_09_00645", "Checkpoint on Pier" },
            { "0_21_00003", "Cheese on Pier" },
            { "0_03_00105", "Coin Bag In Construction Site #1" },
            { "0_03_00104", "Coin In Construction Site #1" },
            { "0_03_00103", "Coin In Construction Site #2" },
            { "0_03_00114", "Coin In Construction Site #3" },
            { "0_03_00111", "Coin In Construction Site #4" },
            { "0_03_00108", "Coin In Construction Site #5" },
            { "0_03_00113", "Coin In Construction Site #6" },
            { "0_03_00110", "Coin Bag In Construction Site #2" },
            { "0_03_00107", "Coin In Construction Site #7" },
            { "0_03_00112", "Coin In Construction Site #8" },
            { "0_03_00109", "Coin In Construction Site #9" },
            { "0_03_00106", "Coin In Construction Site #10" },
            { "0_01_00017", "Gear - Morco Chauffeur" },
            { "0_03_00309", "Coin on Roofs near Hat World #1" },
            { "0_03_00308", "Coin on Roofs near Hat World #2" },
            { "0_03_00316", "Coin on Roofs near Hat World #3" },
            { "0_03_00374", "Coin on Roofs near Hat World #4" },
            { "0_03_00373", "Coin on Roofs near Hat World #5" },
            { "0_03_00375", "Coin on Roofs near Hat World #6" },
            { "0_03_00376", "Coin on Roofs near Hat World #7" },
            { "0_03_00377", "Coin on Roofs near Hat World #8" },
            { "0_03_00378", "Coin Bag on Roofs near Hat World #1" },
            { "0_03_00312", "Coin Above Law Firm" },
            { "0_03_00171", "Coin Across from Pizza Oven #1" },
            { "0_03_00251", "Coin Across from Pizza Oven #2" },
            { "0_03_00261", "Coin Across from Pizza Oven #3" },
            { "0_03_00262", "Coin Across from Pizza Oven #4" },
            { "0_03_00263", "Coin Across from Pizza Oven #5" },
            { "0_03_00264", "Coin Across from Pizza Oven #6" },
            { "0_03_00265", "Coin Across from Pizza Oven #7" },
            { "0_03_00266", "Coin Across from Pizza Oven #8" },
            { "0_03_00267", "Coin Across from Pizza Oven #9" },
            { "0_03_00268", "Chest Across from Pizza Oven" },
            { "0_03_00209", "Coin on Beach Near Cabins #1" },
            { "0_03_00208", "Coin on Beach Near Cabins #2" },
            { "0_03_00212", "Coin on Beach Near Cabins #3" },
            { "0_03_00211", "Coin on Beach Near Cabins #4" },
            { "0_03_00215", "Coin on Beach Near Cabins #5" },
            { "0_03_00214", "Coin on Beach Near Cabins #6" },
            { "0_03_00218", "Coin on Beach Near Cabins #7" },
            { "0_03_00217", "Coin on Beach Near Cabins #8" },
            { "0_03_00221", "Coin on Beach Near Cabins #9" },
            { "0_03_00220", "Coin on Beach Near Cabins #10" },
            { "0_03_00295", "Coin on Beachside Cliff #1" },
            { "0_03_00294", "Coin on Beachside Cliff #2" },
            { "0_03_00293", "Coin on Beachside Cliff #3" },
            { "0_03_00292", "Coin on Beachside Cliff #4" },
            { "0_03_00291", "Coin Bag on Beachside Cliff" },
            { "0_03_00200", "Coin Under Bomb Bridge #1" },
            { "0_03_00195", "Coin Under Bomb Bridge #2" },
            { "0_03_00190", "Coin Under Bomb Bridge #3" },
            { "0_03_00199", "Coin Under Bomb Bridge #4" },
            { "0_03_00194", "Coin Under Bomb Bridge #5" },
            { "0_03_00189", "Coin Under Bomb Bridge #6" },
            { "0_03_00198", "Coin Under Bomb Bridge #7" },
            { "0_03_00193", "Coin Under Bomb Bridge #8" },
            { "0_03_00188", "Coin Under Bomb Bridge #9" },
            { "0_03_00197", "Coin Under Bomb Bridge #10" },
            { "0_03_00192", "Coin Under Bomb Bridge #11" },
            { "0_03_00187", "Coin Under Bomb Bridge #12" },
            { "0_03_00196", "Coin Under Bomb Bridge #13" },
            { "0_03_00191", "Coin Under Bomb Bridge #14" },
            { "0_03_00186", "Coin Under Bomb Bridge #15" },
            { "0_03_00576", "Coin on City Tree #1" },
            { "0_03_00575", "Coin on City Tree #2" },
            { "0_03_00579", "Coin on City Tree #3" },
            { "0_03_00578", "Coin on City Tree #4" },
            { "0_03_00577", "Chest on City Tree" },
            { "0_03_00202", "Coin Around Big Mountain #1" },
            { "0_03_00203", "Coin Around Big Mountain #2" },
            { "0_03_00204", "Coin Around Big Mountain #3" },
            { "0_03_00205", "Coin Around Big Mountain #4" },
            { "0_03_00206", "Coin Around Big Mountain #5" },
            { "0_03_00207", "Coin Around Big Mountain #6" },
            { "0_03_00210", "Coin Around Big Mountain #7" },
            { "0_03_00213", "Coin Around Big Mountain #8" },
            { "0_03_00216", "Coin Around Big Mountain #9" },
            { "0_03_00219", "Coin Around Big Mountain #10" },
            { "0_03_00372", "Coin on Ramp House Towards Sewers #1" },
            { "0_03_00371", "Coin on Ramp House Towards Sewers #2" },
            { "0_03_00370", "Coin on Ramp House Towards Sewers #3" },
            { "0_03_00412", "Coin on Ramp House Towards Sewers #4" },
            { "0_03_00459", "Coin on Ramp House Towards Sewers #5" },
            { "0_03_00115", "Chest in UFO Crash Site Near City" },
        };

        public static Dictionary<string, string> GrandmasIslandPipeArea = new()
        {
            { "0_03_00346", "Coin Near Pipe on Lab Hill #1" },
            { "0_03_00347", "Coin Near Pipe on Lab Hill #2" },
            { "0_03_00350", "Coin Near Pipe on Lab Hill #3" },
            { "0_03_00354", "Coin Near Pipe on Lab Hill #4" },
            { "0_03_00358", "Coin Near Pipe on Lab Hill #5" },
            { "0_03_00362", "Coin Near Pipe on Lab Hill #6" },
            { "0_03_00365", "Coin Near Pipe on Lab Hill #7" },
        };

        public static Dictionary<string, string> GrandmasIslandBrokenPier = new()
        {
            { "0_03_00050", "Coin on Broken Pier #1" },
            { "0_03_00051", "Coin on Broken Pier #2" },
            { "0_03_00052", "Coin on Broken Pier #3" },
            { "0_03_00053", "Coin on Broken Pier #4" },
            { "0_03_00054", "Coin on Broken Pier #5" },
            { "0_03_00055", "Coin on Broken Pier #6" },
            { "0_03_00056", "Coin on Broken Pier #7" },
            { "0_03_00057", "Coin on Broken Pier #8" },
            { "0_03_00058", "Coin on Broken Pier #9" },
            // All of the above have an additional rule where they can be accessed with only boost in expert mode
            { "0_03_00059", "Coin on Broken Pier #10" },
            { "0_03_00060", "Coin on Broken Pier #11" },
            { "0_03_00061", "Coin on Broken Pier #12" },
            { "0_03_00063", "Coin on Broken Pier #13" },
            { "0_03_00042", "Coin on Broken Pier #14" },
            { "0_03_00041", "Coin on Broken Pier #15" },
            { "0_03_00044", "Coin on Broken Pier #16" },
            { "0_03_00062", "Coin on Broken Pier #17" },
            { "0_01_00011", "Gear - Broken Pier" },
        };

        public static Dictionary<string, string> GrandmasIslandExpert1HighGround = new() // Region Grandma's Island Main & (EX1 | J1 | B1 | GP)
        {
            { "0_03_00270", "Coin Bag on Power Shovel" },                
            { "0_03_00300", "Coin on Roofs Behind Grandma's Statue"},
            { "0_03_00381", "Coin on Roof Ramp Towards Gym Gears #1" },
            { "0_03_00380", "Coin on Roof Ramp Towards Gym Gears #2" },
            { "0_03_00393", "Coin on Roof Ramp Towards Gym Gears #3" },
            { "0_03_00413", "Coin on Roof Ramp Towards Gym Gears #4" },
            { "0_03_00425", "Coin on Roof Ramp Towards Gym Gears #5" },
            { "0_03_00548", "Coin Bag on Brown Cliffside Cabin" },
            { "0_03_00311", "Coin Bag on Green Cliffside Cabin" },
            { "0_03_00363", "Coin on Hill Near Lab #1" },
            { "0_03_00359", "Coin on Hill Near Lab #2" },
            { "0_03_00364", "Coin on Hill Near Lab #3" },
            { "0_03_00360", "Coin on Hill Near Lab #4" },
            { "0_03_00355", "Coin on Hill Near Lab #5" },
            { "0_03_00361", "Coin on Hill Near Lab #6" },
            { "0_03_00356", "Coin on Hill Near Lab #7" },
            { "0_03_00351", "Coin on Hill Near Lab #8" },
            { "0_03_00357", "Coin on Hill Near Lab #9" },
            { "0_03_00352", "Coin on Hill Near Lab #10" },
            { "0_03_00348", "Coin on Hill Near Lab #11" },
            { "0_03_00353", "Coin on Hill Near Lab #12" },
            { "0_03_00349", "Coin on Hill Near Lab #13" },
            // Everything below this requires an explosion bounce.
            // Not *too* precise but definitely silly to require moveless
            // Some of these are decently tricky with just B1 but not too crazy
            { "0_03_00662", "Coin in Mountain Tunnel #1" },
            { "0_03_00661", "Coin in Mountain Tunnel #2" },
            { "0_03_00660", "Coin in Mountain Tunnel #3" },
            { "0_03_00659", "Coin in Mountain Tunnel #4" },
            { "0_03_00658", "Coin in Mountain Tunnel #5" },
            { "0_03_00657", "Coin in Mountain Tunnel #6" },
            { "0_03_00656", "Coin in Mountain Tunnel #7" },
            { "0_03_00701", "Coin after Mountain Tunnel #1" },
            { "0_03_00698", "Coin after Mountain Tunnel #2" },
            { "0_03_00697", "Coin after Mountain Tunnel #3" },
            { "0_03_00682", "Coin after Mountain Tunnel #4" },
            { "0_03_00700", "Coin after Mountain Tunnel #5" },
            { "0_03_00696", "Coin after Mountain Tunnel #6" },
            { "0_03_00681", "Coin after Mountain Tunnel #7" },
            { "0_03_00699", "Coin after Mountain Tunnel #8" },
            { "0_03_00695", "Coin after Mountain Tunnel #9" },
            { "0_03_00708", "Coin after Mountain Tunnel #10" },
            { "0_03_00713", "Coin after Mountain Tunnel #11" },
            { "0_03_00712", "Coin after Mountain Tunnel #12" },
            { "0_03_00711", "Coin after Mountain Tunnel #13" },
            { "0_03_00710", "Coin after Mountain Tunnel #14" },
            { "0_03_00709", "Coin after Mountain Tunnel #15" },
            { "0_03_00749", "Coin Bag on Mountain Ramp House" },
            { "0_03_00610", "Coin on Grassy Beachside Cliff #1" },
            { "0_03_00609", "Coin on Grassy Beachside Cliff #2" },
            { "0_03_00608", "Coin on Grassy Beachside Cliff #3" },
            { "0_03_00607", "Coin on Grassy Beachside Cliff #4" },
            { "0_03_00606", "Coin on Grassy Beachside Cliff #5" },
            { "0_03_00605", "Coin on Grassy Beachside Cliff #6" },
            { "0_03_00612", "Coin on Grassy Beachside Cliff #7" },
            { "0_03_00625", "Coin on Grassy Beachside Cliff #8" },
            { "0_03_00622", "Coin on Grassy Beachside Cliff #9" },
            { "0_03_00619", "Coin on Grassy Beachside Cliff #10" },
            { "0_03_00624", "Coin in Cliff Face #1" },
            { "0_03_00621", "Coin in Cliff Face #2" },
            { "0_03_00620", "Coin in Cliff Face #3" },
            { "0_03_00623", "Coin in Cliff Face #4" },
            { "0_03_00626", "Coin in Cliff Face #5" },
            { "0_03_00628", "Coin in Cliff Face #6" },
            { "0_03_00629", "Coin in Cliff Face #7" },
            { "0_01_00008", "Gear - In Cliff Face" },
        };

        public static Dictionary<string, string> GrandmasIslandExpert2HighGround = new() // Region Grandma's Island Main & (EX2 | J1 | B1 | GP)
        {
            // Moveless requires a one-off bomb bounce and then requires precise air movement to reach this cliff face
            // If you mess up, you have to die to respawn the bomb
            { "0_01_00018", "Gear - From MMA Champion" },
            { "0_03_00706", "Coin Near MMA Champion #1" },
            { "0_03_00705", "Coin Near MMA Champion #2" },
            { "0_03_00703", "Coin Near MMA Champion #3" },
            { "0_03_00702", "Coin Near MMA Champion #4" },
            { "0_03_00704", "Coin Bag Near MMA Champion" },
        };


        public static Dictionary<string, string> GrandmasIslandHighGround = new()
        {
            { "0_03_00273", "Coin on Stone Island #1" },
            { "0_03_00274", "Coin on Stone Island #2" },
            { "0_03_00275", "Coin on Stone Island #3" },
            { "0_03_00277", "Coin on Stone Island #4" },
            { "0_03_00278", "Coin on Stone Island #5" },
            { "0_03_00279", "Coin on Stone Island #6" },
            { "0_03_00276", "Coin Bag on Stone Island" },
        };

        public static Dictionary<string, string> GrandmasIslandMainSpin = new()
        {
            { "0_01_00003", "Grandma's Island - Gear - Oil Pump" },
            { "0_01_00014", "Grandma's Island - Gear - Inside Spin Blocks"},
        };

        public static Dictionary<string, string> HubSpecialRules = new()
        {
            { "0_03_00280", "Grandma's Island - Safe on Ocean Pillar" },        // Region Grandma's Island Main & (B2 | (B1 & GP))
        };

        public static List<Tuple<string, Dictionary<string, string>>> KnownIDs =
        [
            // Hub IDs
            new("Grandma's Island - Starting Area", GrandmasIslandStart),
            new("Grandma's Island - Moat", GrandmasIslandMoat),
            new("Grandma's Island - Main Area", GrandmasIslandMain),
            new("Grandma's Island - Pipe Warp Area", GrandmasIslandPipeArea),
            new("Grandma's Island - Main Area + Spin", GrandmasIslandMainSpin),
            new("Grandma's Island - Broken Pier", GrandmasIslandBrokenPier),
            new("Grandma's Island - Expert 1 High Ground", GrandmasIslandExpert1HighGround),
            new("Grandma's Island - Expert 2 High Ground", GrandmasIslandExpert2HighGround),
            new("Grandma's Island - High Ground", GrandmasIslandHighGround),
            new("Hub Area - Special Rules", HubSpecialRules),
        ];

        public static Dictionary<string, List<Dictionary<string, string>>> PerLevelIDs = new()
        {
            {
                "Hub",
                [
                    GrandmasIslandStart, 
                    GrandmasIslandMoat,
                    GrandmasIslandMain,
                    GrandmasIslandMainSpin,
                    GrandmasIslandPipeArea,
                    GrandmasIslandBrokenPier,
                    GrandmasIslandExpert1HighGround,
                    GrandmasIslandExpert2HighGround,
                    GrandmasIslandHighGround,
                    HubSpecialRules
                ]
            },
        };

        public static void CheckLocation(string type, string id)
        {
            var areas = KnownIDs.Where(area => area.Item2.ContainsKey(id)).ToList();
            switch (areas.Count)
            {
                case 1:
                    Plugin.DoubleLog($"[KNOWN] Picked up \"{areas[0].Item2[id]}\" from area \"{areas[0].Item1}\". ID: {id}");
                    return;
                case > 1:
                    Plugin.DoubleLog($"ERROR: ITEM WITH ID {id} IS FOUND IN MULTIPLE AREAS!");
                    break;
                default:
                    Plugin.DoubleLog($"Picked up unknown item of type \"{type}\". ID: {id}");
                    break;
            }
            GUIUtility.systemCopyBuffer = id;
        }
    }
}
