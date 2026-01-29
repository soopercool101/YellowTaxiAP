using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace YellowTaxiAP
{
#if DEBUG
    public static class DebugLocationHelper
    {
        public static bool Enabled => false;

        #region Hub

        [Description("Granny's Island - Starting Area")]
        public static Dictionary<string, string> GrannysIslandStart = new()
        {
            { "0_03_00037", "Granny's Island - Coin in Starting Area #1" },
            { "0_03_00038", "Granny's Island - Coin in Starting Area #2" },
            { "0_03_00086", "Granny's Island - Coin in Starting Area #3" },
            { "0_03_00098", "Granny's Island - Coin in Starting Area #4" },
            { "0_03_00117", "Granny's Island - Coin in Starting Area #5" },
            { "0_03_00149", "Granny's Island - Coin in Starting Area #6" },
        };

        [Description("Granny's Island - Hat World")]
        public static Dictionary<string, string> HubHatWorld = new()
        {
            { string.Empty, nameof(HubHatWorld) + " - !PLACEHOLDER!" }
        };

        [Description("Granny's Island - Moat")]
        public static Dictionary<string, string> GrannysIslandMoat = new()
        {
            { "0_03_00151", "Granny's Island - Coin in Moat #1" },
            { "0_03_00100", "Granny's Island - Coin in Moat #2" },
            { "0_03_00088", "Granny's Island - Coin in Moat #3" },
            { "0_03_00040", "Granny's Island - Coin in Moat #4" },
            { "0_03_00021", "Granny's Island - Coin in Moat #5" },
            { "0_03_00023", "Granny's Island - Coin in Moat #6" },
            { "0_03_00025", "Granny's Island - Coin in Moat #7" },
            { "0_03_00027", "Granny's Island - Coin in Moat #8" },
            { "0_03_00029", "Granny's Island - Coin in Moat #9" },
            { "0_03_00031", "Granny's Island - Coin in Moat #10" },
            { "0_03_00036", "Granny's Island - Coin in Moat #11" },
            { "0_03_00035", "Granny's Island - Coin in Moat #12" },
            { "0_03_00034", "Granny's Island - Coin in Moat #13" },
            { "0_03_00033", "Granny's Island - Coin in Moat #14" },
            { "0_03_00032", "Granny's Island - Coin in Moat #15" },
            { "0_03_00030", "Granny's Island - Coin in Moat #16" },
            { "0_03_00028", "Granny's Island - Coin in Moat #17" },
            { "0_03_00026", "Granny's Island - Coin in Moat #18" },
            { "0_03_00024", "Granny's Island - Coin in Moat #19" },
            { "0_03_00022", "Granny's Island - Coin in Moat #20" },
            { "0_03_00020", "Granny's Island - Coin in Moat #21" },
            { "0_03_00039", "Granny's Island - Coin in Moat #22" },
            { "0_03_00087", "Granny's Island - Coin in Moat #23" },
            { "0_03_00099", "Granny's Island - Coin in Moat #24" },
            { "0_03_00150", "Granny's Island - Coin in Moat #25" },
            { "0_21_00004", "Granny's Island - Cheese in Moat" },
        };

        [Description("Granny's Island - Main Area")]
        public static Dictionary<string, string> GrannysIslandMain = new()
        {
            // Granny's Island - Main Area
            { "0_03_00153", "Granny's Island - Coin Bag on Road #1" },
            { "0_03_00384", "Granny's Island - Coin Bag on Road #2" },
            { "0_03_00154", "Granny's Island - Coin on Road #1" },
            { "0_03_00156", "Granny's Island - Coin on Road #2" },
            { "0_03_00157", "Granny's Island - Coin on Road #3" },
            { "0_03_00158", "Granny's Island - Coin on Road #4" },
            { "0_03_00159", "Granny's Island - Coin on Road #5" },
            { "0_03_00165", "Granny's Island - Coin on Road #6" },
            { "0_03_00164", "Granny's Island - Coin on Road #7" },
            { "0_03_00163", "Granny's Island - Coin on Road #8" },
            { "0_03_00162", "Granny's Island - Coin on Road #9" },
            { "0_03_00161", "Granny's Island - Coin on Road #10" },
            { "0_03_00166", "Granny's Island - Coin on Road #11" },
            { "0_03_00167", "Granny's Island - Coin on Road #12" },
            { "0_03_00168", "Granny's Island - Coin on Road #13" },
            { "0_03_00169", "Granny's Island - Coin on Road #14" },
            { "0_03_00170", "Granny's Island - Coin on Road #15" },
            { "0_03_00172", "Granny's Island - Coin on Road #16" },
            { "0_03_00173", "Granny's Island - Coin on Road #17" },
            { "0_03_00174", "Granny's Island - Coin on Road #18" },
            { "0_03_00175", "Granny's Island - Coin on Road #19" },
            { "0_03_00176", "Granny's Island - Coin on Road #20" },
            { "0_03_00177", "Granny's Island - Coin on Road #21" },
            { "0_03_00178", "Granny's Island - Coin on Road #22" },
            { "0_03_00391", "Granny's Island - Coin on Road #23" },
            { "0_03_00390", "Granny's Island - Coin on Road #24" },
            { "0_03_00389", "Granny's Island - Coin on Road #25" },
            { "0_03_00388", "Granny's Island - Coin on Road #26" },
            { "0_03_00387", "Granny's Island - Coin on Road #27" },
            { "0_03_00386", "Granny's Island - Coin on Road #28" },
            { "0_03_00385", "Granny's Island - Coin on Road #29" },
            { "0_03_00597", "Granny's Island - Coin on Mountain Road #1" },
            { "0_03_00598", "Granny's Island - Coin on Mountain Road #2" },
            { "0_03_00611", "Granny's Island - Coin on Mountain Road #3" },
            { "0_03_00613", "Granny's Island - Coin on Mountain Road #4" },
            { "0_03_00630", "Granny's Island - Coin on Mountain Road #5" },
            { "0_09_00300", "Granny's Island - Checkpoint on Road" },
            { "0_21_00002", "Granny's Island - Cheese Near Tonzi Patonzi" },
            { "0_03_00241", "Granny's Island - Coin in UFO Crash Site #1" },
            { "0_03_00239", "Granny's Island - Coin in UFO Crash Site #2" },
            { "0_03_00237", "Granny's Island - Coin in UFO Crash Site #3" },
            { "0_03_00234", "Granny's Island - Coin in UFO Crash Site #4" },
            { "0_03_00232", "Granny's Island - Coin in UFO Crash Site #5" },
            { "0_03_00230", "Granny's Island - Coin in UFO Crash Site #6" },
            { "0_03_00228", "Granny's Island - Coin in UFO Crash Site #7" },
            { "0_03_00227", "Granny's Island - Coin in UFO Crash Site #8" },
            { "0_03_00226", "Granny's Island - Coin in UFO Crash Site #9" },
            { "0_03_00225", "Granny's Island - Coin in UFO Crash Site #10" },
            { "0_03_00224", "Granny's Island - Coin in UFO Crash Site #11" },
            { "0_03_00229", "Granny's Island - Coin in UFO Crash Site #12" },
            { "0_03_00231", "Granny's Island - Coin in UFO Crash Site #13" },
            { "0_03_00233", "Granny's Island - Coin in UFO Crash Site #14" },
            { "0_03_00235", "Granny's Island - Coin in UFO Crash Site #15" },
            { "0_03_00238", "Granny's Island - Coin in UFO Crash Site #16" },
            { "0_03_00240", "Granny's Island - Coin in UFO Crash Site #17" },
            { "0_03_00242", "Granny's Island - Coin in UFO Crash Site #18" },
            { "0_03_00244", "Granny's Island - Coin in UFO Crash Site #19" },
            { "0_03_00245", "Granny's Island - Coin in UFO Crash Site #20" },
            { "0_03_00246", "Granny's Island - Coin in UFO Crash Site #21" },
            { "0_03_00247", "Granny's Island - Coin in UFO Crash Site #22" },
            { "0_03_00248", "Granny's Island - Coin in UFO Crash Site #23" },
            { "0_03_00243", "Granny's Island - Coin in UFO Crash Site #24" },
            { "0_03_00236", "Granny's Island - Chest in UFO Crash Site" },
            { "0_21_00005", "Granny's Island - Cheese in Construction Site" },
            { "0_21_00006", "Granny's Island - Cheese Near Construction Site" },
            { "0_21_00000", "Granny's Island - Cheese Behind City" },
            { "0_03_00185", "Granny's Island - Coin Behind City #1" },
            { "0_03_00184", "Granny's Island - Coin Behind City #2" },
            { "0_03_00183", "Granny's Island - Coin Behind City #3" },
            { "0_03_00181", "Granny's Island - Coin Behind City #4" },
            { "0_03_00180", "Granny's Island - Coin Behind City #5" },
            { "0_03_00179", "Granny's Island - Coin Behind City #6" },
            { "0_03_00182", "Granny's Island - Coin Bag Behind City" },
            { "0_03_00271", "Granny's Island - Coin Bag on Fenced City Cliff #1" },
            { "0_03_00272", "Granny's Island - Coin Bag on Fenced City Cliff #2" },
            { "0_03_00269", "Granny's Island - Coin Near Oil Pump #1" },
            { "0_03_00299", "Granny's Island - Coin Near Oil Pump #2" },
            { "0_03_00310", "Granny's Island - Coin Near Oil Pump #3" },
            { "0_03_00317", "Granny's Island - Coin Near Oil Pump #4" },
            { "0_03_00379", "Granny's Island - Coin Near Oil Pump #5" },
            { "0_03_00160", "Granny's Island - Chest by Poop House" },
            { "0_03_00018", "Granny's Island - Coin in Underwater Maze #1" },
            { "0_03_00013", "Granny's Island - Coin in Underwater Maze #2" },
            { "0_03_00011", "Granny's Island - Coin in Underwater Maze #3" },
            { "0_03_00009", "Granny's Island - Coin in Underwater Maze #4" },
            { "0_03_00007", "Granny's Island - Coin in Underwater Maze #5" },
            { "0_03_00006", "Granny's Island - Coin in Underwater Maze #6" },
            { "0_03_00004", "Granny's Island - Coin in Underwater Maze #7" },
            { "0_03_00003", "Granny's Island - Coin in Underwater Maze #8" },
            { "0_03_00002", "Granny's Island - Coin in Underwater Maze #9" },
            { "0_03_00001", "Granny's Island - Coin in Underwater Maze #10" },
            { "0_03_00005", "Granny's Island - Coin in Underwater Maze #11" },
            { "0_03_00008", "Granny's Island - Coin in Underwater Maze #12" },
            { "0_03_00010", "Granny's Island - Coin in Underwater Maze #13" },
            { "0_03_00012", "Granny's Island - Coin in Underwater Maze #14" },
            { "0_03_00014", "Granny's Island - Coin in Underwater Maze #15" },
            { "0_03_00015", "Granny's Island - Coin in Underwater Maze #16" },
            { "0_03_00016", "Granny's Island - Coin in Underwater Maze #17" },
            { "0_03_00017", "Granny's Island - Coin in Underwater Maze #18" },
            { "0_01_00013", "Granny's Island - Gear - Underwater Maze" },
            { "0_03_00045", "Granny's Island - Coin on Pier #1" },
            { "0_03_00046", "Granny's Island - Coin on Pier #2" },
            { "0_03_00047", "Granny's Island - Coin on Pier #3" },
            { "0_03_00048", "Granny's Island - Coin on Pier #4" },
            { "0_03_00049", "Granny's Island - Coin on Pier #5" },
            { "0_09_00640", "Granny's Island - Checkpoint on Pier" },
            { "0_21_00003", "Granny's Island - Cheese on Pier" },
            { "0_03_00105", "Granny's Island - Coin Bag in Construction Site #1" },
            { "0_03_00104", "Granny's Island - Coin in Construction Site #1" },
            { "0_03_00103", "Granny's Island - Coin in Construction Site #2" },
            { "0_03_00114", "Granny's Island - Coin in Construction Site #3" },
            { "0_03_00111", "Granny's Island - Coin in Construction Site #4" },
            { "0_03_00108", "Granny's Island - Coin in Construction Site #5" },
            { "0_03_00113", "Granny's Island - Coin in Construction Site #6" },
            { "0_03_00110", "Granny's Island - Coin Bag in Construction Site #2" },
            { "0_03_00107", "Granny's Island - Coin in Construction Site #7" },
            { "0_03_00112", "Granny's Island - Coin in Construction Site #8" },
            { "0_03_00109", "Granny's Island - Coin in Construction Site #9" },
            { "0_03_00106", "Granny's Island - Coin in Construction Site #10" },
            { "0_01_00017", "Granny's Island - Gear - Morco Chauffeur" },
            { "0_03_00309", "Granny's Island - Coin on Roofs Near Hat World #1" },
            { "0_03_00308", "Granny's Island - Coin on Roofs Near Hat World #2" },
            { "0_03_00316", "Granny's Island - Coin on Roofs Near Hat World #3" },
            { "0_03_00374", "Granny's Island - Coin on Roofs Near Hat World #4" },
            { "0_03_00373", "Granny's Island - Coin on Roofs Near Hat World #5" },
            { "0_03_00375", "Granny's Island - Coin on Roofs Near Hat World #6" },
            { "0_03_00376", "Granny's Island - Coin on Roofs Near Hat World #7" },
            { "0_03_00377", "Granny's Island - Coin on Roofs Near Hat World #8" },
            { "0_03_00378", "Granny's Island - Coin Bag on Roofs Near Hat World #1" },
            { "0_03_00312", "Granny's Island - Coin Above Law Firm" },
            { "0_03_00171", "Granny's Island - Coin Across From Pizza Oven #1" },
            { "0_03_00251", "Granny's Island - Coin Across From Pizza Oven #2" },
            { "0_03_00261", "Granny's Island - Coin Across From Pizza Oven #3" },
            { "0_03_00262", "Granny's Island - Coin Across From Pizza Oven #4" },
            { "0_03_00263", "Granny's Island - Coin Across From Pizza Oven #5" },
            { "0_03_00264", "Granny's Island - Coin Across From Pizza Oven #6" },
            { "0_03_00265", "Granny's Island - Coin Across From Pizza Oven #7" },
            { "0_03_00266", "Granny's Island - Coin Across From Pizza Oven #8" },
            { "0_03_00267", "Granny's Island - Coin Across From Pizza Oven #9" },
            { "0_03_00268", "Granny's Island - Chest Across From Pizza Oven" },
            { "0_03_00209", "Granny's Island - Coin on Beach Near Cabins #1" },
            { "0_03_00208", "Granny's Island - Coin on Beach Near Cabins #2" },
            { "0_03_00212", "Granny's Island - Coin on Beach Near Cabins #3" },
            { "0_03_00211", "Granny's Island - Coin on Beach Near Cabins #4" },
            { "0_03_00215", "Granny's Island - Coin on Beach Near Cabins #5" },
            { "0_03_00214", "Granny's Island - Coin on Beach Near Cabins #6" },
            { "0_03_00218", "Granny's Island - Coin on Beach Near Cabins #7" },
            { "0_03_00217", "Granny's Island - Coin on Beach Near Cabins #8" },
            { "0_03_00221", "Granny's Island - Coin on Beach Near Cabins #9" },
            { "0_03_00220", "Granny's Island - Coin on Beach Near Cabins #10" },
            { "0_03_00295", "Granny's Island - Coin on Beachside Cliff #1" },
            { "0_03_00294", "Granny's Island - Coin on Beachside Cliff #2" },
            { "0_03_00293", "Granny's Island - Coin on Beachside Cliff #3" },
            { "0_03_00292", "Granny's Island - Coin on Beachside Cliff #4" },
            { "0_03_00291", "Granny's Island - Coin Bag on Beachside Cliff" },
            { "0_03_00200", "Granny's Island - Coin Under Bomb Bridge #1" },
            { "0_03_00195", "Granny's Island - Coin Under Bomb Bridge #2" },
            { "0_03_00190", "Granny's Island - Coin Under Bomb Bridge #3" },
            { "0_03_00199", "Granny's Island - Coin Under Bomb Bridge #4" },
            { "0_03_00194", "Granny's Island - Coin Under Bomb Bridge #5" },
            { "0_03_00189", "Granny's Island - Coin Under Bomb Bridge #6" },
            { "0_03_00198", "Granny's Island - Coin Under Bomb Bridge #7" },
            { "0_03_00193", "Granny's Island - Coin Under Bomb Bridge #8" },
            { "0_03_00188", "Granny's Island - Coin Under Bomb Bridge #9" },
            { "0_03_00197", "Granny's Island - Coin Under Bomb Bridge #10" },
            { "0_03_00192", "Granny's Island - Coin Under Bomb Bridge #11" },
            { "0_03_00187", "Granny's Island - Coin Under Bomb Bridge #12" },
            { "0_03_00196", "Granny's Island - Coin Under Bomb Bridge #13" },
            { "0_03_00191", "Granny's Island - Coin Under Bomb Bridge #14" },
            { "0_03_00186", "Granny's Island - Coin Under Bomb Bridge #15" },
            { "0_03_00576", "Granny's Island - Coin on City Tree #1" },
            { "0_03_00575", "Granny's Island - Coin on City Tree #2" },
            { "0_03_00579", "Granny's Island - Coin on City Tree #3" },
            { "0_03_00578", "Granny's Island - Coin on City Tree #4" },
            { "0_03_00577", "Granny's Island - Chest on City Tree" },
            { "0_03_00202", "Granny's Island - Coin Around Big Mountain #1" },
            { "0_03_00203", "Granny's Island - Coin Around Big Mountain #2" },
            { "0_03_00204", "Granny's Island - Coin Around Big Mountain #3" },
            { "0_03_00205", "Granny's Island - Coin Around Big Mountain #4" },
            { "0_03_00206", "Granny's Island - Coin Around Big Mountain #5" },
            { "0_03_00207", "Granny's Island - Coin Around Big Mountain #6" },
            { "0_03_00210", "Granny's Island - Coin Around Big Mountain #7" },
            { "0_03_00213", "Granny's Island - Coin Around Big Mountain #8" },
            { "0_03_00216", "Granny's Island - Coin Around Big Mountain #9" },
            { "0_03_00219", "Granny's Island - Coin Around Big Mountain #10" },
            { "0_03_00372", "Granny's Island - Coin on Ramp House Towards Sewers #1" },
            { "0_03_00371", "Granny's Island - Coin on Ramp House Towards Sewers #2" },
            { "0_03_00370", "Granny's Island - Coin on Ramp House Towards Sewers #3" },
            { "0_03_00412", "Granny's Island - Coin on Ramp House Towards Sewers #4" },
            { "0_03_00459", "Granny's Island - Coin on Ramp House Towards Sewers #5" },
            { "0_03_00115", "Granny's Island - Chest in UFO Crash Site Near City" },    // Typically, this gets removed when the rocket is created. This has been fixed.
            // Special rules
            { "0_03_00280", "Granny's Island - Safe on Ocean Pillar" },                 // Region Granny's Island Main & (B2 | (B1 & GP))
            { "0_03_00755", "Granny's Island - Safe on Granny's Statue" },              // Region Granny's Island Main & (J2 | (J1 & GP))
            { "0_03_00589", "Granny's Island - Coin Bag on Pillar Towards Sewer" },     // Region Granny's Island Main & (B1 | GP)
            { "0_01_00003", "Granny's Island - Gear - Oil Pump" },                      // Region Granny's Island Main & (SP | EX2)
            { "0_01_00014", "Granny's Island - Gear - Inside Spin Blocks" },            // Region Granny's Island Main & SP
        };

        [Description("Granny's Island - Pipe Area")]
        public static Dictionary<string, string> GrannysIslandPipeArea = new()
        {
            { "0_03_00346", "Granny's Island - Coin Near Pipe on Hill by Lab #1" },
            { "0_03_00347", "Granny's Island - Coin Near Pipe on Hill by Lab #2" },
            { "0_03_00350", "Granny's Island - Coin Near Pipe on Hill by Lab #3" },
            { "0_03_00354", "Granny's Island - Coin Near Pipe on Hill by Lab #4" },
            { "0_03_00358", "Granny's Island - Coin Near Pipe on Hill by Lab #5" },
            { "0_03_00362", "Granny's Island - Coin Near Pipe on Hill by Lab #6" },
            { "0_03_00365", "Granny's Island - Coin Near Pipe on Hill by Lab #7" },
        };

        [Description("Granny's Island - Broken Pier Shallow")]
        public static Dictionary<string, string> GrannysIslandBrokenPierShallow = new()
        {
            { "0_03_00050", "Granny's Island - Coin on Broken Pier #1" },
            { "0_03_00051", "Granny's Island - Coin on Broken Pier #2" },
            { "0_03_00052", "Granny's Island - Coin on Broken Pier #3" },
            { "0_03_00053", "Granny's Island - Coin on Broken Pier #4" },
            { "0_03_00054", "Granny's Island - Coin on Broken Pier #5" },
            { "0_03_00055", "Granny's Island - Coin on Broken Pier #6" },
            { "0_03_00056", "Granny's Island - Coin on Broken Pier #7" },
            { "0_03_00057", "Granny's Island - Coin on Broken Pier #8" },
            { "0_03_00058", "Granny's Island - Coin on Broken Pier #9" },
        };

        [Description("Granny's Island - Broken Pier Deep")]
        public static Dictionary<string, string> GrannysIslandBrokenPierDeep = new()
        {
            { "0_03_00059", "Granny's Island - Coin on Broken Pier #10" },
            { "0_03_00060", "Granny's Island - Coin on Broken Pier #11" },
            { "0_03_00061", "Granny's Island - Coin on Broken Pier #12" },
            { "0_03_00063", "Granny's Island - Coin on Broken Pier #13" },
        };

        [Description("Granny's Island - Broken Pier Extra Deep")]
        public static Dictionary<string, string> GrannysIslandBrokenPierExtraDeep = new()
        {
            { "0_03_00042", "Granny's Island - Coin on Broken Pier #14" },
            { "0_03_00041", "Granny's Island - Coin on Broken Pier #15" },
            { "0_03_00044", "Granny's Island - Coin on Broken Pier #16" },
            { "0_03_00062", "Granny's Island - Coin on Broken Pier #17" },
            { "0_01_00011", "Granny's Island - Gear - Broken Pier" },
        };

        [Description("Granny's Island - Lab Hill Expert1 High Ground")]
        public static Dictionary<string, string> GrannysIslandLabHillExpert1HighGround = new() // Region Granny's Island Main & (EX1 | J1 | B1 | GP)
        {
            { "0_03_00363", "Granny's Island - Coin on Hill Near Lab #1" },
            { "0_03_00359", "Granny's Island - Coin on Hill Near Lab #2" },
            { "0_03_00364", "Granny's Island - Coin on Hill Near Lab #3" },
            { "0_03_00360", "Granny's Island - Coin on Hill Near Lab #4" },
            { "0_03_00355", "Granny's Island - Coin on Hill Near Lab #5" },
            { "0_03_00361", "Granny's Island - Coin on Hill Near Lab #6" },
            { "0_03_00356", "Granny's Island - Coin on Hill Near Lab #7" },
            { "0_03_00351", "Granny's Island - Coin on Hill Near Lab #8" },
            { "0_03_00357", "Granny's Island - Coin on Hill Near Lab #9" },
            { "0_03_00352", "Granny's Island - Coin on Hill Near Lab #10" },
            { "0_03_00348", "Granny's Island - Coin on Hill Near Lab #11" },
            { "0_03_00353", "Granny's Island - Coin on Hill Near Lab #12" },
            { "0_03_00349", "Granny's Island - Coin on Hill Near Lab #13" },
        };

        [Description("Granny's Island - Lab Hill High Ground")]
        public static Dictionary<string, string> GrannysIslandLabHillHighGround = new()
        {
            { "0_03_00539", "Granny's Island - Coin on Starting Area Walls #1" },
            { "0_03_00540", "Granny's Island - Coin on Starting Area Walls #2" },
            { "0_03_00541", "Granny's Island - Coin on Starting Area Walls #3" },
            { "0_03_00542", "Granny's Island - Coin on Starting Area Walls #4" },
            { "0_03_00533", "Granny's Island - Coin on Starting Area Walls #5" },
            { "0_03_00531", "Granny's Island - Coin on Starting Area Walls #6" },
            { "0_03_00529", "Granny's Island - Coin on Starting Area Walls #7" },
            { "0_03_00527", "Granny's Island - Coin on Starting Area Walls #8" },
            { "0_03_00525", "Granny's Island - Coin on Starting Area Walls #9" },
            { "0_03_00523", "Granny's Island - Coin on Starting Area Walls #10" },
            { "0_03_00521", "Granny's Island - Coin on Starting Area Walls #11" },
            { "0_03_00519", "Granny's Island - Coin on Starting Area Walls #12" },
            { "0_03_00517", "Granny's Island - Coin on Starting Area Walls #13" },
            { "0_03_00515", "Granny's Island - Coin on Starting Area Walls #14" },
            { "0_03_00513", "Granny's Island - Coin on Starting Area Walls #15" },
            { "0_03_00511", "Granny's Island - Coin on Starting Area Walls #16" },
            { "0_03_00509", "Granny's Island - Coin on Starting Area Walls #17" },
            { "0_03_00507", "Granny's Island - Coin on Starting Area Walls #18" },
            { "0_03_00505", "Granny's Island - Coin on Starting Area Walls #19" },
            { "0_03_00502", "Granny's Island - Coin on Starting Area Walls #20" },
            { "0_03_00501", "Granny's Island - Coin on Starting Area Walls #21" },
            { "0_03_00500", "Granny's Island - Coin on Starting Area Walls #22" },
            { "0_03_00499", "Granny's Island - Coin on Starting Area Walls #23" },
            { "0_03_00498", "Granny's Island - Coin on Starting Area Walls #24" },
            { "0_03_00497", "Granny's Island - Coin on Starting Area Walls #25" },
            { "0_03_00496", "Granny's Island - Coin on Starting Area Walls #26" },
            { "0_03_00495", "Granny's Island - Coin on Starting Area Walls #27" },
            { "0_03_00493", "Granny's Island - Coin on Starting Area Walls #28" },
            { "0_03_00492", "Granny's Island - Coin on Starting Area Walls #29" },
            { "0_03_00491", "Granny's Island - Coin on Starting Area Walls #30" },
            { "0_03_00490", "Granny's Island - Coin on Starting Area Walls #31" },
            { "0_03_00489", "Granny's Island - Coin on Starting Area Walls #32" },
            { "0_03_00488", "Granny's Island - Coin on Starting Area Walls #33" },
            { "0_03_00487", "Granny's Island - Coin on Starting Area Walls #34" },
            { "0_03_00486", "Granny's Island - Coin on Starting Area Walls #35" },
            { "0_03_00504", "Granny's Island - Coin on Starting Area Walls #36" },
            { "0_03_00506", "Granny's Island - Coin on Starting Area Walls #37" },
            { "0_03_00508", "Granny's Island - Coin on Starting Area Walls #38" },
            { "0_03_00510", "Granny's Island - Coin on Starting Area Walls #39" },
            { "0_03_00512", "Granny's Island - Coin on Starting Area Walls #40" },
            { "0_03_00514", "Granny's Island - Coin on Starting Area Walls #41" },
            { "0_03_00516", "Granny's Island - Coin on Starting Area Walls #42" },
            { "0_03_00518", "Granny's Island - Coin on Starting Area Walls #43" },
            { "0_03_00520", "Granny's Island - Coin on Starting Area Walls #44" },
            { "0_03_00522", "Granny's Island - Coin on Starting Area Walls #45" },
            { "0_03_00524", "Granny's Island - Coin on Starting Area Walls #46" },
            { "0_03_00526", "Granny's Island - Coin on Starting Area Walls #47" },
            { "0_03_00528", "Granny's Island - Coin on Starting Area Walls #48" },
            { "0_03_00530", "Granny's Island - Coin on Starting Area Walls #49" },
            { "0_03_00532", "Granny's Island - Coin on Starting Area Walls #50" },
            { "0_03_00535", "Granny's Island - Coin on Starting Area Walls #51" },
            { "0_03_00536", "Granny's Island - Coin on Starting Area Walls #52" },
            { "0_03_00537", "Granny's Island - Coin on Starting Area Walls #53" },
            { "0_03_00538", "Granny's Island - Coin on Starting Area Walls #54" },
            { "0_03_00543", "Granny's Island - Coin Bag on Starting Area Walls #1" },
            { "0_03_00503", "Granny's Island - Coin Bag on Starting Area Walls #2" },
            { "0_03_00494", "Granny's Island - Coin Bag on Starting Area Walls #3" },
            { "0_03_00485", "Granny's Island - Coin Bag on Starting Area Walls #4" },
            { "0_03_00534", "Granny's Island - Coin Bag on Starting Area Walls #5" },
            { "0_03_00297", "Granny's Island - Coin on Cliff Behind Starting Area #1" },
            { "0_03_00314", "Granny's Island - Coin on Cliff Behind Starting Area #2" },
            { "0_03_00257", "Granny's Island - Coin on Cliff Behind Starting Area #3" },
            { "0_03_00256", "Granny's Island - Coin on Cliff Behind Starting Area #4" },
            { "0_03_00255", "Granny's Island - Coin on Cliff Behind Starting Area #5" },
            { "0_03_00313", "Granny's Island - Coin on Cliff Behind Starting Area #6" },
            { "0_03_00296", "Granny's Island - Coin on Cliff Behind Starting Area #7" },
        };

        [Description("Granny's Island - Expert1 High Ground")]
        public static Dictionary<string, string> GrannysIslandExpert1HighGround = new() // Region Granny's Island Main & (EX1 | J1 | B1 | GP)
        {
            { "0_03_00270", "Granny's Island - Coin Bag on Power Shovel" },
            { "0_03_00300", "Granny's Island - Coin on Roofs Behind Granny's Statue"},
            { "0_03_00548", "Granny's Island - Coin Bag on Brown Cliffside Cabin" },
            { "0_03_00311", "Granny's Island - Coin Bag on Green Cliffside Cabin" },
            // Everything below this requires an explosion bounce.
            // Not *too* precise but definitely silly to require moveless
            // Some of these are decently tricky with just B1 but not too crazy
            { "0_03_00662", "Granny's Island - Coin in Mountain Tunnel #1" },
            { "0_03_00661", "Granny's Island - Coin in Mountain Tunnel #2" },
            { "0_03_00660", "Granny's Island - Coin in Mountain Tunnel #3" },
            { "0_03_00659", "Granny's Island - Coin in Mountain Tunnel #4" },
            { "0_03_00658", "Granny's Island - Coin in Mountain Tunnel #5" },
            { "0_03_00657", "Granny's Island - Coin in Mountain Tunnel #6" },
            { "0_03_00656", "Granny's Island - Coin in Mountain Tunnel #7" },
            { "0_03_00701", "Granny's Island - Coin After Mountain Tunnel #1" },
            { "0_03_00698", "Granny's Island - Coin After Mountain Tunnel #2" },
            { "0_03_00697", "Granny's Island - Coin After Mountain Tunnel #3" },
            { "0_03_00682", "Granny's Island - Coin After Mountain Tunnel #4" },
            { "0_03_00700", "Granny's Island - Coin After Mountain Tunnel #5" },
            { "0_03_00696", "Granny's Island - Coin After Mountain Tunnel #6" },
            { "0_03_00681", "Granny's Island - Coin After Mountain Tunnel #7" },
            { "0_03_00699", "Granny's Island - Coin After Mountain Tunnel #8" },
            { "0_03_00695", "Granny's Island - Coin After Mountain Tunnel #9" },
            { "0_03_00708", "Granny's Island - Coin After Mountain Tunnel #10" },
            { "0_03_00713", "Granny's Island - Coin After Mountain Tunnel #11" },
            { "0_03_00712", "Granny's Island - Coin After Mountain Tunnel #12" },
            { "0_03_00711", "Granny's Island - Coin After Mountain Tunnel #13" },
            { "0_03_00710", "Granny's Island - Coin After Mountain Tunnel #14" },
            { "0_03_00709", "Granny's Island - Coin After Mountain Tunnel #15" },
            { "0_03_00749", "Granny's Island - Coin Bag on Mountain Ramp House" },
            { "0_03_00610", "Granny's Island - Coin on Grassy Beachside Cliff #1" },
            { "0_03_00609", "Granny's Island - Coin on Grassy Beachside Cliff #2" },
            { "0_03_00608", "Granny's Island - Coin on Grassy Beachside Cliff #3" },
            { "0_03_00607", "Granny's Island - Coin on Grassy Beachside Cliff #4" },
            { "0_03_00606", "Granny's Island - Coin on Grassy Beachside Cliff #5" },
            { "0_03_00605", "Granny's Island - Coin on Grassy Beachside Cliff #6" },
            { "0_03_00612", "Granny's Island - Coin on Grassy Beachside Cliff #7" },
            { "0_03_00625", "Granny's Island - Coin on Grassy Beachside Cliff #8" },
            { "0_03_00622", "Granny's Island - Coin on Grassy Beachside Cliff #9" },
            { "0_03_00619", "Granny's Island - Coin on Grassy Beachside Cliff #10" },
            { "0_03_00624", "Granny's Island - Coin in Cliff Face #1" },
            { "0_03_00621", "Granny's Island - Coin in Cliff Face #2" },
            { "0_03_00620", "Granny's Island - Coin in Cliff Face #3" },
            { "0_03_00623", "Granny's Island - Coin in Cliff Face #4" },
            { "0_03_00626", "Granny's Island - Coin in Cliff Face #5" },
            { "0_03_00628", "Granny's Island - Coin in Cliff Face #6" },
            { "0_03_00629", "Granny's Island - Coin in Cliff Face #7" },
            { "0_01_00008", "Granny's Island - Gear - In Cliff Face" },
            { "0_21_00001", "Granny's Island - Cheese on Mountain" },
            // Requires short-range bomb luring of repeatable bombs
            { "0_03_00318", "Granny's Island - Coin Bag on Roofs Behind Granny's Statue" },
            { "0_03_00396", "Granny's Island - Chest on Roofs Behind Granny's Statue" },
        };

        [Description("Granny's Island - Expert2 High Ground")]
        public static Dictionary<string, string> GrannysIslandExpert2HighGround = new() // Region Granny's Island Main & (EX2 | J1 | B1 | GP)
        {
            // Requires one-off Bomb luring
            { "0_03_00424", "Granny's Island - Coin on Hill By Cloro-Phil #1" },
            { "0_03_00423", "Granny's Island - Coin on Hill By Cloro-Phil #2" },
            { "0_03_00427", "Granny's Island - Coin on Hill By Cloro-Phil #3" },
            { "0_03_00472", "Granny's Island - Coin on Hill By Cloro-Phil #4" },
            { "0_03_00471", "Granny's Island - Coin on Hill By Cloro-Phil #5" },
            { "0_03_00470", "Granny's Island - Coin on Hill By Cloro-Phil #6" },
            { "0_03_00426", "Granny's Island - Coin on Hill By Cloro-Phil #7" },
            // Moveless requires a one-off bomb bounce and then requires precise air movement to reach this cliff face
            // If you mess up, you have to die to respawn the bomb
            { "0_01_00018", "Granny's Island - Gear - From MMA Champion" },
            { "0_03_00706", "Granny's Island - Coin Near MMA Champion #1" },
            { "0_03_00705", "Granny's Island - Coin Near MMA Champion #2" },
            { "0_03_00703", "Granny's Island - Coin Near MMA Champion #3" },
            { "0_03_00702", "Granny's Island - Coin Near MMA Champion #4" },
            { "0_03_00704", "Granny's Island - Coin Bag Near MMA Champion" },
        };

        [Description("Granny's Island - High Ground")]
        public static Dictionary<string, string> GrannysIslandHighGround = new()
        {
            { "0_03_00273", "Granny's Island - Coin on Stone Island #1" },
            { "0_03_00274", "Granny's Island - Coin on Stone Island #2" },
            { "0_03_00275", "Granny's Island - Coin on Stone Island #3" },
            { "0_03_00277", "Granny's Island - Coin on Stone Island #4" },
            { "0_03_00278", "Granny's Island - Coin on Stone Island #5" },
            { "0_03_00279", "Granny's Island - Coin on Stone Island #6" },
            { "0_03_00276", "Granny's Island - Coin Bag on Stone Island" },
            { "0_03_00465", "Granny's Island - Coin on Path to Granny's Statue #1" },
            { "0_03_00466", "Granny's Island - Coin on Path to Granny's Statue #2" },
            { "0_03_00467", "Granny's Island - Coin on Path to Granny's Statue #3" },
            { "0_03_00469", "Granny's Island - Coin on Path to Granny's Statue #4" },
            { "0_03_00468", "Granny's Island - Coin on Path to Granny's Statue #5" },
            { "0_03_00547", "Granny's Island - Coin on Path to Granny's Statue #6" },
            { "0_03_00574", "Granny's Island - Coin on Path to Granny's Statue #7" },
            { "0_03_00573", "Granny's Island - Coin Bag on Path to Granny's Statue" },
            { "0_01_00005", "Granny's Island - Gear - Granny's Statue" },
            { "0_01_00012", "Granny's Island - Gear - Gym Gears Roof" },
            { "0_03_00381", "Granny's Island - Coin on Roof Ramp Towards Gym Gears #1" },
            { "0_03_00380", "Granny's Island - Coin on Roof Ramp Towards Gym Gears #2" },
            { "0_03_00393", "Granny's Island - Coin on Roof Ramp Towards Gym Gears #3" },
            { "0_03_00413", "Granny's Island - Coin on Roof Ramp Towards Gym Gears #4" },
            { "0_03_00425", "Granny's Island - Coin on Roof Ramp Towards Gym Gears #5" },
            { "0_03_00562", "Granny's Island - Coin Above Pipe on Hill by Lab #1" },
            { "0_03_00565", "Granny's Island - Coin Above Pipe on Hill by Lab #2" },
            { "0_03_00568", "Granny's Island - Coin Above Pipe on Hill by Lab #3" },
            { "0_03_00563", "Granny's Island - Coin Above Pipe on Hill by Lab #4" },
            { "0_03_00566", "Granny's Island - Coin Above Pipe on Hill by Lab #5" },
            { "0_03_00569", "Granny's Island - Coin Above Pipe on Hill by Lab #6" },
            { "0_03_00564", "Granny's Island - Coin Above Pipe on Hill by Lab #7" },
            { "0_03_00567", "Granny's Island - Coin Above Pipe on Hill by Lab #8" },
            { "0_03_00570", "Granny's Island - Coin Above Pipe on Hill by Lab #9" },
            { "0_03_00462", "Granny's Island - Coin on Roof in Front of Granny's Statue #1" },
            { "0_03_00463", "Granny's Island - Coin on Roof in Front of Granny's Statue #2" },
            { "0_03_00473", "Granny's Island - Coin on Roof in Front of Granny's Statue #3" },
            { "0_03_00464", "Granny's Island - Coin on Roof in Front of Granny's Statue #4" },
            { "0_03_00474", "Granny's Island - Coin Bag on Roof in Front of Granny's Statue" },
            { "0_03_00767", "Granny's Island - Coin on Stone Arch on Top of Mountain #1" },
            { "0_03_00765", "Granny's Island - Coin on Stone Arch on Top of Mountain #2" },
            { "0_03_00764", "Granny's Island - Coin on Stone Arch on Top of Mountain #3" },
            { "0_03_00763", "Granny's Island - Coin on Stone Arch on Top of Mountain #4" },
            { "0_03_00791", "Granny's Island - Coin on Stone Arch on Top of Mountain #5" },
            { "0_03_00795", "Granny's Island - Coin on Stone Arch on Top of Mountain #6" },
            { "0_03_00794", "Granny's Island - Coin on Stone Arch on Top of Mountain #7" },
            { "0_03_00298", "Granny's Island - Coin on Roof Next to Pizza Oven #1" },
            { "0_03_00307", "Granny's Island - Coin on Roof Next to Pizza Oven #2" },
            { "0_03_00315", "Granny's Island - Coin on Roof Next to Pizza Oven #3" },
            { "0_03_00368", "Granny's Island - Coin on Roof Next to Pizza Oven #4" },
            { "0_03_00369", "Granny's Island - Coin Bag on Roof Next to Pizza Oven" },
            { "0_03_00453", "Granny's Island - Coin on Stone Arch on Beach #1" },
            { "0_03_00455", "Granny's Island - Coin on Stone Arch on Beach #2" },
            { "0_03_00458", "Granny's Island - Coin on Stone Arch on Beach #3" },
            { "0_03_00457", "Granny's Island - Coin on Stone Arch on Beach #4" },
            { "0_03_00456", "Granny's Island - Coin on Stone Arch on Beach #5" },
            { "0_03_00454", "Granny's Island - Coin Bag on Stone Arch on Beach" },
            { "0_03_00366", "Granny's Island - Chest on Stone Arch in Water" },
            { "0_03_00367", "Granny's Island - Coin on Stone Arch in Water #1" },
            { "0_03_00392", "Granny's Island - Coin on Stone Arch in Water #2" },
            { "0_03_00779", "Granny's Island - Coin Near Lighthouse #1" },
            { "0_03_00780", "Granny's Island - Coin Near Lighthouse #2" },
            { "0_03_00781", "Granny's Island - Coin Near Lighthouse #3" },
            { "0_03_00782", "Granny's Island - Coin Near Lighthouse #4" },
            { "0_03_00783", "Granny's Island - Coin Near Lighthouse #5" },
            { "0_03_00784", "Granny's Island - Coin Near Lighthouse #6" },
            { "0_03_00785", "Granny's Island - Coin Near Lighthouse #7" },
            { "0_03_00786", "Granny's Island - Coin Near Lighthouse #8" },
            { "0_03_00787", "Granny's Island - Coin Near Lighthouse #9" },
            { "0_03_00788", "Granny's Island - Coin Near Lighthouse #10" },
            { "0_03_00768", "Granny's Island - Coin Under Ramp Towards Lighthouse #1" },
            { "0_03_00769", "Granny's Island - Coin Under Ramp Towards Lighthouse #2" },
            { "0_03_00770", "Granny's Island - Coin Under Ramp Towards Lighthouse #3" },
            { "0_03_00771", "Granny's Island - Coin Under Ramp Towards Lighthouse #4" },
            { "0_03_00772", "Granny's Island - Coin Under Ramp Towards Lighthouse #5" },
            { "0_03_00773", "Granny's Island - Coin Under Ramp Towards Lighthouse #6" },
            { "0_03_00774", "Granny's Island - Coin Under Ramp Towards Lighthouse #7" },
            { "0_03_00775", "Granny's Island - Coin Under Ramp Towards Lighthouse #8" },
            { "0_03_00776", "Granny's Island - Coin Under Ramp Towards Lighthouse #9" },
            { "0_03_00777", "Granny's Island - Coin Under Ramp Towards Lighthouse #10" },
            { "0_03_00778", "Granny's Island - Coin Under Ramp Towards Lighthouse #11" },
            { "0_03_00766", "Granny's Island - Coin on Ramp Towards Lighthouse #1" },
            { "0_03_00792", "Granny's Island - Coin on Ramp Towards Lighthouse #2" },
            { "0_03_00796", "Granny's Island - Coin on Ramp Towards Lighthouse #3" },
            { "0_03_00799", "Granny's Island - Coin on Ramp Towards Lighthouse #4" },
            { "0_03_00801", "Granny's Island - Coin on Ramp Towards Lighthouse #5" },
            { "0_03_00802", "Granny's Island - Coin on Ramp Towards Lighthouse #6" },
            { "0_03_00804", "Granny's Island - Coin on Ramp Towards Lighthouse #7" },
            { "0_03_00422", "Granny's Island - Coin in Beach Cove #1" },
            { "0_03_00421", "Granny's Island - Coin in Beach Cove #2" },
            { "0_03_00420", "Granny's Island - Coin in Beach Cove #3" },
            { "0_03_00418", "Granny's Island - Coin in Beach Cove #4" },
            { "0_03_00417", "Granny's Island - Coin in Beach Cove #5" },
            { "0_03_00416", "Granny's Island - Coin in Beach Cove #6" },
            { "0_01_00002", "Granny's Island - Gear - Beach Cove" },
            { "0_03_00546", "Granny's Island - Coin on Roof by Oil Pump #1" },
            { "0_03_00545", "Granny's Island - Coin on Roof by Oil Pump #2" },
            { "0_03_00544", "Granny's Island - Coin on Roof by Oil Pump #3" },
            { "0_01_00009", "Granny's Island - Gear - On Roof by Oil Pump" },
            { "0_03_00414", "Granny's Island - Coin on Roof by Construction Site #1" },
            { "0_03_00415", "Granny's Island - Coin on Roof by Construction Site #2" },
            { "0_03_00460", "Granny's Island - Coin on Roof by Construction Site #3" },
            { "0_03_00461", "Granny's Island - Coin Bag on Roof by Construction Site" },
            { "0_03_00394", "Granny's Island - Coin Bag on Roof by Spin Blocks #1" },
            { "0_03_00395", "Granny's Island - Coin Bag on Roof by Spin Blocks #2" },
            { "0_03_00397", "Granny's Island - Coin Bag on Roof by Spin Blocks #3" },
            { "0_03_00382", "Granny's Island - Coin on Roof by Spin Blocks #1" },
            { "0_03_00383", "Granny's Island - Coin on Roof by Spin Blocks #2" },
            // Special Rules
            { "0_01_00001", "Granny's Island - Gear - Lighthouse" },                    // Region Granny's Island Higher Ground & (B2 | (B1 & GP))
            { "0_01_00015", "Granny's Island - Gear - In the Clouds" },                 // Region Granny's Island Higher Ground & ((B1 & GP) | (B2 & J1))
        };

        [Description("Granny's Island - Arch Near Construction Site")]
        public static Dictionary<string, string> GrannysIslandConstructionArch = new()
        {
            { "0_03_00592", "Granny's Island - Coin on Arch Near Construction Site #1" },
            { "0_03_00593", "Granny's Island - Coin on Arch Near Construction Site #2" },
            { "0_03_00594", "Granny's Island - Coin on Arch Near Construction Site #3" },
            { "0_03_00595", "Granny's Island - Coin on Arch Near Construction Site #4" },
            { "0_03_00596", "Granny's Island - Chest on Arch Near Construction Site" },
        };

        [Description("Granny's Island - Ocean Pillar")]
        public static Dictionary<string, string> GrannysIslandOceanPillar = new()
        {
            { "0_03_00588", "Granny's Island - Coin on Ocean Pillar Towards Sewer #1" },
            { "0_03_00587", "Granny's Island - Coin on Ocean Pillar Towards Sewer #2" },
            { "0_03_00586", "Granny's Island - Coin on Ocean Pillar Towards Sewer #3" },
            { "0_03_00585", "Granny's Island - Coin on Ocean Pillar Towards Sewer #4" },
            { "0_01_00006", "Granny's Island - Gear - On Ocean Pillar Towards Sewer" },
        };

        [Description("Granny's Island - Top of Rocket")]
        public static Dictionary<string, string> GrannysIslandRocketTop = new()
        {
            { "0_03_00798", "Granny's Island - Chest on Top of Rocket" },
            { "0_03_00800", "Granny's Island - Coin on Top of Rocket #1" },
            { "0_03_00803", "Granny's Island - Coin on Top of Rocket #2" },
            { "0_03_00807", "Granny's Island - Coin on Top of Rocket #3" },
            { "0_03_00808", "Granny's Island - Coin on Top of Rocket #4" },
            { "0_03_00834", "Granny's Island - Coin on Top of Rocket #5" },
            { "0_01_00020", "Granny's Island - Gear - On Top of Rocket" },
        };

        [Description("Granny's Island - First Island Towards Sewer")]
        public static Dictionary<string, string> GrannysIslandTowardsSewerIsland1 = new()
        {
            { "0_03_00411", "Granny's Island - Coin on First Island Towards Sewers #1" },
            { "0_03_00410", "Granny's Island - Coin on First Island Towards Sewers #2" },
            { "0_03_00407", "Granny's Island - Coin on First Island Towards Sewers #3" },
            { "0_03_00406", "Granny's Island - Coin on First Island Towards Sewers #4" },
            { "0_03_00409", "Granny's Island - Coin Bag on First Island Towards Sewers #1" },
            { "0_03_00408", "Granny's Island - Coin Bag on First Island Towards Sewers #2" },
        };

        [Description("Granny's Island - Second Island Towards Sewer")]
        public static Dictionary<string, string> GrannysIslandTowardsSewerIsland2 = new()
        {
            { "0_03_00094", "Granny's Island - Coin on Second Island Towards Sewer #1" },
            { "0_03_00091", "Granny's Island - Coin on Second Island Towards Sewer #2" },
            { "0_03_00090", "Granny's Island - Coin on Second Island Towards Sewer #3" },
            { "0_03_00093", "Granny's Island - Coin on Second Island Towards Sewer #4" },
            { "0_03_00092", "Granny's Island - Coin Bag on Second Island Towards Sewer" },
        };

        [Description("Granny's Island - Tree on Second Island Towards Sewer")]
        public static Dictionary<string, string> GrannysIslandTowardsSewerIsland2Tree = new()
        {
            { "0_03_00305", "Granny's Island - Coin on Tree on Second Island Towards Sewer #1" },
            { "0_03_00306", "Granny's Island - Coin on Tree on Second Island Towards Sewer #2" },
            { "0_03_00303", "Granny's Island - Coin on Tree on Second Island Towards Sewer #3" },
            { "0_03_00302", "Granny's Island - Coin on Tree on Second Island Towards Sewer #4" },
            { "0_03_00304", "Granny's Island - Chest on Tree on Second Island Towards Sewer" },
        };

        [Description("Granny's Island - Sewer Island")]
        public static Dictionary<string, string> GrannysIslandSewerIsland = new()
        {
            { "0_03_00095", "Granny's Island - Coin on Sewer Island #1" },
            { "0_03_00102", "Granny's Island - Coin on Sewer Island #2" },
            { "0_03_00155", "Granny's Island - Coin on Sewer Island #3" },
            { "0_03_00250", "Granny's Island - Coin on Sewer Island #4" },
            { "0_03_00260", "Granny's Island - Coin on Sewer Island #5" },
            { "0_03_00258", "Granny's Island - Coin on Sewer Island #6" },
            { "0_03_00249", "Granny's Island - Coin on Sewer Island #7" },
            { "0_03_00152", "Granny's Island - Coin on Sewer Island #8" },
            { "0_03_00101", "Granny's Island - Coin on Sewer Island #9" },
            { "0_03_00089", "Granny's Island - Coin on Sewer Island #10" },
            { "0_03_00259", "Granny's Island - Chest on Sewer Island" },
        };

        [Description("Granny's Island - Sewer Island Upper")]
        public static Dictionary<string, string> GrannysIslandSewerIslandUpper = new()
        {
            { string.Empty, nameof(GrannysIslandSewerIslandUpper) + " - !PLACEHOLDER!" }
        };

        [Description("Granny's Island - Island Past Cloro-Phil")]
        public static Dictionary<string, string> GrannysIslandCloroPhilIsland = new()
        {
            { "0_03_00287", "Granny's Island - Coin on Island Past Cloro-Phil #1" },
            { "0_03_00286", "Granny's Island - Coin on Island Past Cloro-Phil #2" },
            { "0_03_00290", "Granny's Island - Coin on Island Past Cloro-Phil #3" },
            { "0_03_00289", "Granny's Island - Coin on Island Past Cloro-Phil #4" },
            { "0_03_00285", "Granny's Island - Coin on Island Past Cloro-Phil #5" },
            { "0_03_00284", "Granny's Island - Coin on Island Past Cloro-Phil #6" },
            { "0_03_00282", "Granny's Island - Coin on Island Past Cloro-Phil #7" },
            { "0_03_00283", "Granny's Island - Coin on Island Past Cloro-Phil #8" },
            { "0_03_00288", "Granny's Island - Coin Bag on Island Past Cloro-Phil" },
            { "0_03_00281", "Granny's Island - Chest on Island Past Cloro-Phil" },
        };

        [Description("Granny's Island - High Pillar by Lab")]
        public static Dictionary<string, string> GrannysIslandHighPillarByLab = new()
        {
            { "0_03_00633", "Granny's Island - Coin on High Pillar by Lab #1" },
            { "0_03_00638", "Granny's Island - Coin on High Pillar by Lab #2" },
            { "0_03_00643", "Granny's Island - Coin on High Pillar by Lab #3" },
            { "0_03_00634", "Granny's Island - Coin on High Pillar by Lab #4" },
            { "0_03_00639", "Granny's Island - Coin on High Pillar by Lab #5" },
            { "0_03_00644", "Granny's Island - Coin on High Pillar by Lab #6" },
            { "0_03_00635", "Granny's Island - Coin on High Pillar by Lab #7" },
            { "0_03_00640", "Granny's Island - Coin on High Pillar by Lab #8" },
            { "0_03_00645", "Granny's Island - Coin on High Pillar by Lab #9" },
            { "0_03_00636", "Granny's Island - Coin on High Pillar by Lab #10" },
            { "0_03_00641", "Granny's Island - Coin on High Pillar by Lab #11" },
            { "0_03_00646", "Granny's Island - Coin on High Pillar by Lab #12" },
            { "0_03_00637", "Granny's Island - Coin on High Pillar by Lab #13" },
            { "0_03_00642", "Granny's Island - Coin on High Pillar by Lab #14" },
            { "0_03_00647", "Granny's Island - Coin on High Pillar by Lab #15" },
            // Special Rules
            { "0_01_00007", "Granny's Island - Gear - High Pillar by Lab" },            // Region Granny's Island - High Pillar by Lab & (J1 | GP)
        };

        [Description("Granny's Island - Crash Again Island")]
        public static Dictionary<string, string> GrannysIslandCrashAgainIsland = new()
        {
            { string.Empty, nameof(GrannysIslandCrashAgainIsland) + " - !PLACEHOLDER!" }
        };

        [Description("Granny's Island - Crash Again Roof")]
        public static Dictionary<string, string> GrannysIslandCrashAgainRoof = new()
        {
            { string.Empty, nameof(GrannysIslandCrashAgainRoof) + " - !PLACEHOLDER!" }
        };

        [Description("Crash Again - Entrance")]
        public static Dictionary<string, string> HubCrashAgain = new()
        {
            { "0_09_00613", "Crash Again - Checkpoint" },
            // Special Rules
            { "0_01_00010", "Crash Again - Gear" },                                     // Region Crash Again & ((SP & (B1 | EX1)) | EX2)
        };

        [Description("Law Firm - Entrance")]
        public static Dictionary<string, string> HubLawFirm = new()
        {
            { string.Empty, nameof(HubLawFirm) + " - !PLACEHOLDER!" }
        };

        [Description("Law Firm - Ledges")]
        public static Dictionary<string, string> HubLawFirmJump = new()
        {
            { "0_03_00399", "Law Firm - Coin #1" },
            { "0_03_00400", "Law Firm - Coin #2" },
            { "0_03_00401", "Law Firm - Coin #3" },
            { "0_03_00402", "Law Firm - Coin #4" },
            { "0_03_00403", "Law Firm - Coin #5" },
            { "0_03_00404", "Law Firm - Coin #6" },
            { "0_03_00405", "Law Firm - Coin #7" },
            { "0_03_00452", "Law Firm - Coin #8" },
            { "0_03_00451", "Law Firm - Coin #9" },
            { "0_03_00450", "Law Firm - Coin #10" },
            { "0_03_00449", "Law Firm - Coin #11" },
            { "0_03_00448", "Law Firm - Coin #12" },
            { "0_03_00447", "Law Firm - Coin #13" },
            { "0_03_00446", "Law Firm - Coin #14" },
            { "0_03_00484", "Law Firm - Coin #15" },
            { "0_03_00483", "Law Firm - Coin #16" },
            { "0_03_00482", "Law Firm - Coin #17" },
            { "0_03_00481", "Law Firm - Coin #18" },
            { "0_03_00480", "Law Firm - Coin #19" },
            { "0_03_00479", "Law Firm - Coin #20" },
            { "0_03_00478", "Law Firm - Coin #21" },
            { "0_03_00555", "Law Firm - Coin #22" },
            { "0_03_00556", "Law Firm - Coin #23" },
            { "0_03_00557", "Law Firm - Coin #24" },
            { "0_03_00559", "Law Firm - Coin #25" },
            { "0_03_00560", "Law Firm - Coin #26" },
            { "0_03_00561", "Law Firm - Coin #27" },
            { "0_01_00023", "Law Firm - Gear" },
        };

        [Description("Ice Cream Truck - Lower Path")]
        public static Dictionary<string, string> HubIceCreamTruckBase = new()
        {
            { "0_09_01440", "Ice Cream Truck - Checkpoint" },
            { "0_03_00075", "Ice Cream Truck - Coin on First Island #1" },
            { "0_03_00080", "Ice Cream Truck - Coin on First Island #2" },
            { "0_03_00074", "Ice Cream Truck - Coin on First Island #3" },
            { "0_03_00079", "Ice Cream Truck - Coin on First Island #4" },
            { "0_03_00084", "Ice Cream Truck - Coin on First Island #5" },
            { "0_03_00073", "Ice Cream Truck - Coin on First Island #6" },
            { "0_03_00078", "Ice Cream Truck - Coin on First Island #7" },
            { "0_03_00083", "Ice Cream Truck - Coin on First Island #8" },
            { "0_03_00072", "Ice Cream Truck - Coin on First Island #9" },
            { "0_03_00077", "Ice Cream Truck - Coin on First Island #10" },
            { "0_03_00082", "Ice Cream Truck - Coin on First Island #11" },
            { "0_03_00071", "Ice Cream Truck - Coin on First Island #12" },
            { "0_03_00076", "Ice Cream Truck - Coin on First Island #13" },
            { "0_03_00081", "Ice Cream Truck - Coin on First Island #14" },
            { "0_03_00132", "Ice Cream Truck - Coin on Second Island #1" },
            { "0_03_00128", "Ice Cream Truck - Coin on Second Island #2" },
            { "0_03_00124", "Ice Cream Truck - Coin on Second Island #3" },
            { "0_03_00131", "Ice Cream Truck - Coin on Second Island #4" },
            { "0_03_00127", "Ice Cream Truck - Coin on Second Island #5" },
            { "0_03_00123", "Ice Cream Truck - Coin on Second Island #6" },
            { "0_03_00130", "Ice Cream Truck - Coin on Second Island #7" },
            { "0_03_00126", "Ice Cream Truck - Coin on Second Island #8" },
            { "0_03_00122", "Ice Cream Truck - Coin on Second Island #9" },
            { "0_03_00129", "Ice Cream Truck - Coin on Second Island #10" },
            { "0_03_00125", "Ice Cream Truck - Coin on Second Island #11" },
            { "0_03_00121", "Ice Cream Truck - Coin on Second Island #12" },
            { "0_03_00134", "Ice Cream Truck - Coin Circling Lower Gear #1" },
            { "0_03_00135", "Ice Cream Truck - Coin Circling Lower Gear #2" },
            { "0_03_00137", "Ice Cream Truck - Coin Circling Lower Gear #3" },
            { "0_03_00139", "Ice Cream Truck - Coin Circling Lower Gear #4" },
            { "0_03_00141", "Ice Cream Truck - Coin Circling Lower Gear #5" },
            { "0_03_00143", "Ice Cream Truck - Coin Circling Lower Gear #6" },
            { "0_03_00145", "Ice Cream Truck - Coin Circling Lower Gear #7" },
            { "0_03_00148", "Ice Cream Truck - Coin Circling Lower Gear #8" },
            { "0_03_00147", "Ice Cream Truck - Coin Circling Lower Gear #9" },
            { "0_03_00146", "Ice Cream Truck - Coin Circling Lower Gear #10" },
            { "0_03_00144", "Ice Cream Truck - Coin Circling Lower Gear #11" },
            { "0_03_00142", "Ice Cream Truck - Coin Circling Lower Gear #12" },
            { "0_03_00140", "Ice Cream Truck - Coin Circling Lower Gear #13" },
            { "0_03_00138", "Ice Cream Truck - Coin Circling Lower Gear #14" },
            { "0_03_00136", "Ice Cream Truck - Coin Circling Lower Gear #15" },
            { "0_03_00133", "Ice Cream Truck - Coin Circling Lower Gear #16" },
            { "0_01_00022", "Ice Cream Truck - Gear - Lower Path in Bomb Block" }
        };

        [Description("Ice Cream Truck - Upper Path")]
        public static Dictionary<string, string> HubIceCreamTruckHighGround = new()
        {
            { "0_03_00330", "Ice Cream Truck - Coin on Upper Grassy Area #1" },
            { "0_03_00326", "Ice Cream Truck - Coin on Upper Grassy Area #2" },
            { "0_03_00332", "Ice Cream Truck - Coin on Upper Grassy Area #3" },
            { "0_03_00329", "Ice Cream Truck - Coin on Upper Grassy Area #4" },
            { "0_03_00325", "Ice Cream Truck - Coin on Upper Grassy Area #5" },
            { "0_03_00322", "Ice Cream Truck - Coin on Upper Grassy Area #6" },
            { "0_03_00331", "Ice Cream Truck - Coin on Upper Grassy Area #7" },
            { "0_03_00328", "Ice Cream Truck - Coin on Upper Grassy Area #8" },
            { "0_03_00324", "Ice Cream Truck - Coin on Upper Grassy Area #9" },
            { "0_03_00321", "Ice Cream Truck - Coin on Upper Grassy Area #10" },
            { "0_03_00327", "Ice Cream Truck - Coin on Upper Grassy Area #11" },
            { "0_03_00323", "Ice Cream Truck - Coin on Upper Grassy Area #12" },
            { "0_03_00338", "Ice Cream Truck - Coin Circling Upper Gear #1" },
            { "0_03_00336", "Ice Cream Truck - Coin Circling Upper Gear #2" },
            { "0_03_00333", "Ice Cream Truck - Coin Circling Upper Gear #3" },
            { "0_03_00334", "Ice Cream Truck - Coin Circling Upper Gear #4" },
            { "0_03_00335", "Ice Cream Truck - Coin Circling Upper Gear #5" },
            { "0_03_00337", "Ice Cream Truck - Coin Circling Upper Gear #6" },
            { "0_03_00340", "Ice Cream Truck - Coin Circling Upper Gear #7" },
            { "0_03_00342", "Ice Cream Truck - Coin Circling Upper Gear #8" },
            { "0_03_00345", "Ice Cream Truck - Coin Circling Upper Gear #9" },
            { "0_03_00344", "Ice Cream Truck - Coin Circling Upper Gear #10" },
            { "0_03_00343", "Ice Cream Truck - Coin Circling Upper Gear #11" },
            { "0_03_00341", "Ice Cream Truck - Coin Circling Upper Gear #12" },
            { "0_01_00021", "Ice Cream Truck - Gear - Upper Path" },
        };

        [Description("Pizza Oven - Entrance")]
        public static Dictionary<string, string> HubPizzaOven = new()
        {
            // Special Rules
            { "0_01_00004", "Pizza Oven - Gear" },      // Region Pizza Oven & B1 & (J1 | EX1)
        };

        [Description("Pizza Oven - Pillar")]
        public static Dictionary<string, string> HubPizzaOvenPillar = new()
        {
            { "0_03_00739", "Pizza Oven - Coin on Top of Pillar #1" },
            { "0_03_00738", "Pizza Oven - Coin on Top of Pillar #2" },
            { "0_03_00741", "Pizza Oven - Coin on Top of Pillar #3" },
            { "0_03_00733", "Pizza Oven - Coin on Top of Pillar #4" },
            { "0_03_00736", "Pizza Oven - Coin on Top of Pillar #5" },
            { "0_03_00735", "Pizza Oven - Coin on Top of Pillar #6" },
            { "0_03_00734", "Pizza Oven - Coin Bag on Top of Pillar #1" },
            { "0_03_00740", "Pizza Oven - Coin Bag on Top of Pillar #2" },
            { "0_03_00737", "Pizza Oven - Chest on Top of Pillar" },
        };

        [Description("Morio's Lab - Ground Floor")]
        public static Dictionary<string, string> MoriosLabGroundFloor = new()
        {
            { "0_01_00000", "Morio's Lab - Gear - From Morio" },
            { "0_03_00064", "Morio's Lab - Coin Near Entrance #1" },
            { "0_03_00096", "Morio's Lab - Coin Near Entrance #2" },
            { "0_03_00097", "Morio's Lab - Coin Near Entrance #3" },
            { "0_03_00116", "Morio's Lab - Coin Near Entrance #4" },
            { "0_03_00119", "Morio's Lab - Coin Near Entrance #5" },
            { "0_03_00120", "Morio's Lab - Coin Near Entrance #6" },
            { "0_01_00026", "Morio's Lab - Gear - Entrance Alcove" },
            { "0_03_00253", "Morio's Lab - Coin Near Entrance #7" },
            { "0_03_00252", "Morio's Lab - Coin Near Entrance #8" },
            { "0_03_00301", "Morio's Lab - Coin Near Entrance #9" },
            { "0_03_00320", "Morio's Lab - Coin Near Entrance #10" },
            { "0_03_00398", "Morio's Lab - Coin Near Entrance #11" },
            { "0_03_00445", "Morio's Lab - Coin Near Entrance #12" },
            { "0_03_00444", "Morio's Lab - Coin Near Entrance #13" },
            { "0_03_00443", "Morio's Lab - Coin Near Entrance #14" },
            { "0_03_00442", "Morio's Lab - Coin Near Entrance #15" },
            { "0_03_00441", "Morio's Lab - Coin Near Entrance #16" },
            { "0_03_00440", "Morio's Lab - Coin Near Entrance #17" },
            { "0_03_00439", "Morio's Lab - Coin Near Entrance #18" },
            { "0_01_00025", "Morio's Lab - Gear - Fall off Ramp" },
        };

        [Description("Morio's Wardrobe")]
        public static Dictionary<string, string> MoriosWardrobe = new()
        {
            { "0_03_00069", "Morio's Wardrobe - Coin #1" },
            { "0_03_00068", "Morio's Wardrobe - Coin #2" },
            { "0_03_00067", "Morio's Wardrobe - Coin #3" },
            { "0_03_00066", "Morio's Wardrobe - Coin #4" },
            { "0_03_00065", "Morio's Wardrobe - Coin #5" },
        };

        [Description("Morio's Lab - Ground Floor Wrenches")]
        public static Dictionary<string, string> MoriosLabGroundFloorWrenches = new()
        {
            { "0_03_00603", "Morio's Lab - Coin on Ground Floor Wrench #1" },
            { "0_03_00602", "Morio's Lab - Coin on Ground Floor Wrench #2" },
            { "0_03_00601", "Morio's Lab - Coin on Ground Floor Wrench #3" },
            { "0_03_00600", "Morio's Lab - Coin on Ground Floor Wrench #4" },
            { "0_03_00599", "Morio's Lab - Coin on Ground Floor Wrench #5" },
            { "0_03_00618", "Morio's Lab - Coin on Ground Floor Wrench #6" },
            { "0_03_00617", "Morio's Lab - Coin on Ground Floor Wrench #7" },
            { "0_03_00616", "Morio's Lab - Coin on Ground Floor Wrench #8" },
            { "0_03_00615", "Morio's Lab - Coin on Ground Floor Wrench #9" },
            { "0_03_00614", "Morio's Lab - Coin on Ground Floor Wrench #10" },
        };

        [Description("Morio's Lab - Ground Floor Lowest Bolt")]
        public static Dictionary<string, string> MoriosLabGroundFloorLowestBolt = new()
        {
            { "0_03_00632", "Morio's Lab - Coin on Bolts Above Ground Floor #1" },
        };

        [Description("Morio's Lab - Ground Floor Bolts")]
        public static Dictionary<string, string> MoriosLabGroundFloorBolts = new()
        {
            { "0_03_00649", "Morio's Lab - Coin on Bolts Above Ground Floor #2" },
            { "0_03_00663", "Morio's Lab - Coin Bag in Nut Above Ground Floor #1" },
            { "0_03_00648", "Morio's Lab - Coin Bag in Nut Above Ground Floor #2" },
            { "0_03_00631", "Morio's Lab - Coin Bag in Nut Above Ground Floor #3" },
            { "0_01_00027", "Morio's Lab - Gear - Alcove Above Ground Floor" },
            { "0_08_00006", "Morio's Lab - PICI Glide Tutorial" },
        };

        [Description("Morio's Lab - Ground Floor Orange Blocks")]
        public static Dictionary<string, string> MoriosLabGroundFloorOrangeBlocks = new()
        {
            { string.Empty, nameof(MoriosLabGroundFloorOrangeBlocks) + " - !PLACEHOLDER!" }
        };

        [Description("Morio's Lab - Bunny Ledge")]
        public static Dictionary<string, string> MoriosLabBunnyLedge = new()
        {
            { "0_02_00000", "Morio's Lab - Bunny - Ledge Above Ground Floor" },         // Region Morio's Lab Ground Floor Bolts & (J1 | (EX1 & B1)) | Region Morio's Lab Final Floor High Pipes
        };

        [Description("Morio's Lab - Second Floor")]
        public static Dictionary<string, string> MoriosLabSecondFloor = new()
        {
            { "0_03_00433", "Morio's Lab - Coin on Second Floor #1" },
            { "0_03_00435", "Morio's Lab - Coin on Second Floor #2" },
            { "0_03_00436", "Morio's Lab - Coin on Second Floor #3" },
            { "0_03_00437", "Morio's Lab - Coin on Second Floor #4" },
            { "0_03_00438", "Morio's Lab - Coin on Second Floor #5" },
        };

        [Description("Morio's Lab - Second Floor Above Demo Wall")]
        public static Dictionary<string, string> MoriosLabSecondFloorAboveDemoWall = new()
        {
            { string.Empty, nameof(MoriosLabSecondFloorAboveDemoWall) + " - !PLACEHOLDER!" }
        };

        [Description("Morio's Lab - Second Floor After Demo Wall")]
        public static Dictionary<string, string> MoriosLabSecondFloorAfterDemoWall = new()
        {
            { "0_03_00434", "Morio's Lab - Coin on Second Floor After Pizza Time Portal #1" },
            { "0_03_00432", "Morio's Lab - Coin on Second Floor After Pizza Time Portal #2" },
            // Special Rules
            { "0_03_00431", "Morio's Lab - Coin on Second Floor After Pizza Time Portal #3" },  // Region Morio's Lab Second Floor After Demo Wall & (X1 | FGU)
        };

        [Description("Morio's Lab - Second Floor Inside True Demo Wall")]
        public static Dictionary<string, string> MoriosLabSecondFloorInTrueDemoWall = new()
        {
            { "0_03_00430", "Morio's Lab - Coin on Second Floor After Pizza Time Portal #4" },
            { "0_03_00429", "Morio's Lab - Coin on Second Floor After Pizza Time Portal #5" },
            { "0_03_00428", "Morio's Lab - Coin Bag on Second Floor" },
        };

        [Description("Morio's Lab - Second Floor After True Demo Wall")]
        public static Dictionary<string, string> MoriosLabSecondFloorAfterTrueDemoWall = new()
        {
            { "0_03_00475", "Morio's Lab - Coin on Second to Third Floor Stairway #1"},
            { "0_03_00477", "Morio's Lab - Coin on Second to Third Floor Stairway #2"},
            { "0_03_00549", "Morio's Lab - Coin on Second to Third Floor Stairway #3"},
            { "0_03_00551", "Morio's Lab - Coin on Second to Third Floor Stairway #4"},
            { "0_03_00476", "Morio's Lab - Coin Bag on Second to Third Floor Stairway #1"},
            { "0_03_00550", "Morio's Lab - Coin Bag on Second to Third Floor Stairway #2"},
            { "0_08_00003", "Morio's Lab - PICI Flip Tutorial" },
        };

        [Description("Morio's Lab - Second Floor Falling From Shortcut Pipe")]
        public static Dictionary<string, string> MoriosLabSecondFloorShortcutPipeFalling = new()
        {
            { string.Empty, nameof(MoriosLabSecondFloorShortcutPipeFalling) + " - !PLACEHOLDER!" }
        };

        [Description("Morio's Lab - Second Floor Access to Shortcut Pipe")]
        public static Dictionary<string, string> MoriosLabSecondFloorShortcutPipe = new()
        {
            { string.Empty, nameof(MoriosLabSecondFloorShortcutPipe) + " - !PLACEHOLDER!" }
        };

        [Description("Morio's Lab - Path to Morio's Room")]
        public static Dictionary<string, string> MoriosLabPathToMoriosRoom = new()
        {
            { "0_03_00552", "Morio's Lab - Coin on Ledge Under Superboost PICI #1"},
            { "0_03_00553", "Morio's Lab - Coin on Ledge Under Superboost PICI #2"},
            { "0_03_00554", "Morio's Lab - Coin on Ledge Under Superboost PICI #3"},
            { "0_03_00693", "Morio's Lab - Coin on Ledge Near Superboost PICI #1"},
            { "0_03_00692", "Morio's Lab - Coin on Ledge Near Superboost PICI #2"},
            { "0_03_00691", "Morio's Lab - Coin on Ledge Near Superboost PICI #3"},
            { "0_03_00690", "Morio's Lab - Coin on Ledge Near Superboost PICI #4"},
            { "0_03_00689", "Morio's Lab - Coin on Ledge Near Superboost PICI #5"},
            { "0_08_00002", "Morio's Lab - PICI Superboost Tutorial"},
            { "0_03_00669", "Morio's Lab - Coin on Corner Ledge Towards Morio's Room #1" },
            { "0_03_00670", "Morio's Lab - Coin on Corner Ledge Towards Morio's Room #2" },
            { "0_03_00671", "Morio's Lab - Coin on Corner Ledge Towards Morio's Room #3" },
            { "0_03_00673", "Morio's Lab - Coin on Corner Ledge Towards Morio's Room #4" },
            { "0_03_00672", "Morio's Lab - Coin on Corner Ledge Towards Morio's Room #5" },
            { "0_03_00676", "Morio's Lab - Coin on Corner Ledge Towards Morio's Room #6" },
            { "0_03_00674", "Morio's Lab - Coin on Corner Ledge Towards Morio's Room #7" },
            { "0_03_00679", "Morio's Lab - Coin on Corner Ledge Towards Morio's Room #8" },
            { "0_03_00678", "Morio's Lab - Coin on Corner Ledge Towards Morio's Room #9" },
            { "0_03_00677", "Morio's Lab - Coin on Corner Ledge Towards Morio's Room #10" },
            { "0_03_00680", "Morio's Lab - Coin on Corner Ledge Towards Morio's Room #11" },
            { "0_03_00694", "Morio's Lab - Coin on Corner Ledge Towards Morio's Room #12" },
            { "0_03_00675", "Morio's Lab - Coin Bag on Corner Ledge Towards Morio's Room" },
            { "0_03_00747", "Morio's Lab - Coin Near Morio's Room #1" },
            { "0_03_00746", "Morio's Lab - Coin Near Morio's Room #2" },
            { "0_03_00745", "Morio's Lab - Coin Near Morio's Room #3" },
            { "0_03_00744", "Morio's Lab - Coin Near Morio's Room #4" },
            { "0_03_00743", "Morio's Lab - Coin Near Morio's Room #5" },
        };

        [Description("Morio's Lab - Morio's Room")]
        public static Dictionary<string, string> MoriosLabMoriosRoomJump = new()
        {
            { "0_03_00748", "Morio's Lab - Coin in Morio's Room #1"},
            { "0_03_00751", "Morio's Lab - Coin in Morio's Room #2"},
            { "0_03_00753", "Morio's Lab - Coin in Morio's Room #3"},
            { "0_03_00754", "Morio's Lab - Coin Bag in Morio's Room #1"},
            { "0_03_00752", "Morio's Lab - Coin Bag in Morio's Room #2"},
            { "0_01_00016", "Morio's Lab - Gear - Morio's Room"},
        };

        [Description("Morio's Lab - Third Floor")]
        public static Dictionary<string, string> MoriosLabThirdFloor = new()
        {
            { "0_03_00580", "Morio's Lab - Coin on Second to Third Floor Stairway #5"},
            { "0_03_00582", "Morio's Lab - Coin on Second to Third Floor Stairway #6"},
            { "0_03_00581", "Morio's Lab - Chest on Second to Third Floor Stairway"},
            { "0_09_00080", "Morio's Lab - Checkpoint Before Spikes" },
            { "0_08_00005", "Morio's Lab - PICI Spin Attack Tutorial" },
        };

        [Description("Morio's Lab - Third Floor Wrenches Lower")]
        public static Dictionary<string, string> MoriosLabThirdFloorWrenchesLower = new()
        {
            { "0_03_00650", "Morio's Lab - Coin on Third Floor Wrenches #1" },
            { "0_03_00651", "Morio's Lab - Coin on Third Floor Wrenches #2" },
            { "0_03_00652", "Morio's Lab - Coin on Third Floor Wrenches #3" },
            { "0_03_00653", "Morio's Lab - Coin on Third Floor Wrenches #4" },
            { "0_03_00654", "Morio's Lab - Coin on Third Floor Wrenches #5" },
        };


        [Description("Morio's Lab - Third Floor Wrenches Middle")]
        public static Dictionary<string, string> MoriosLabThirdFloorWrenchesMiddle = new()
        {
            { "0_03_00664", "Morio's Lab - Coin on Third Floor Wrenches #6" },
            { "0_03_00666", "Morio's Lab - Coin on Third Floor Wrenches #7" },
            { "0_03_00668", "Morio's Lab - Coin on Third Floor Wrenches #8" },
            { "0_03_00667", "Morio's Lab - Coin on Third Floor Wrenches #9" },
            { "0_03_00665", "Morio's Lab - Coin on Third Floor Wrenches #10" },
        };

        [Description("Morio's Lab - Third Floor Wrenches Upper")]
        public static Dictionary<string, string> MoriosLabThirdFloorWrenchesUpper = new()
        {
            { "0_03_00685", "Morio's Lab - Coin on Third Floor Wrenches #11" },
            { "0_03_00687", "Morio's Lab - Coin on Third Floor Wrenches #12" },
            { "0_03_00686", "Morio's Lab - Coin on Third Floor Wrenches #13" },
            { "0_03_00684", "Morio's Lab - Coin on Third Floor Wrenches #14" },
            { "0_03_00683", "Morio's Lab - Coin on Third Floor Wrenches #15" },
            // Special Rules
            { "0_01_00024", "Morio's Lab - Gear - Third Floor Wrenches" },              // Region Morio's Lab Third Floor Wrenches Upper & J1
        };

        [Description("Morio's Lab - Fourth Floor")]
        public static Dictionary<string, string> MoriosLabFourthFloor = new()
        {
            { string.Empty, nameof(MoriosLabFourthFloor) + " - !PLACEHOLDER!" }
        };

        [Description("Morio's Lab - Fourth Floor Jump Spikes")]
        public static Dictionary<string, string> MoriosLabFourthFloorJumpSpikes = new()
        {
            { "0_01_00019", "Morio's Lab - Gear - On Spiky Cliffs" },
            { "0_03_00789", "Morio's Lab - Coin Bag in Spiky Alcove" },
            { "0_02_00001", "Morio's Lab - Bunny - In Spiky Alcove" },
        };

        [Description("Morio's Lab - Ledge Above Maurizio's City Portal")]
        public static Dictionary<string, string> MoriosLabLedgeAboveMauriziosCity = new()
        {
            { "0_03_00793", "Morio's Lab - Coin Bag Above Maurizio's City Portal" },    // (Region Morio's Lab Fourth Floor & J2) | (Region Morio's Lab Fifth Floor Ruined Observatory Area & J1 & Password)
        };

        [Description("Morio's Lab - Fifth Floor Crash Test Area")]
        public static Dictionary<string, string> MoriosLabFifthFloorCrashTestArea = new()
        {
            { "0_03_00761", "Morio's Lab - Coin Near Crash Test Portal #1" },
            { "0_03_00760", "Morio's Lab - Coin Near Crash Test Portal #2" },
            { "0_03_00759", "Morio's Lab - Coin Near Crash Test Portal #3" },
            { "0_03_00758", "Morio's Lab - Coin Near Crash Test Portal #4" },
            { "0_03_00757", "Morio's Lab - Coin Near Crash Test Portal #5" },
            { "0_03_00716", "Morio's Lab - Coin in Orange Block Shortcut Tunnel #1" },
            { "0_03_00717", "Morio's Lab - Coin in Orange Block Shortcut Tunnel #2" },
            { "0_03_00718", "Morio's Lab - Coin in Orange Block Shortcut Tunnel #3" },
            { "0_03_00719", "Morio's Lab - Coin in Orange Block Shortcut Tunnel #4" },
            { "0_03_00720", "Morio's Lab - Coin in Orange Block Shortcut Tunnel #5" },
            { "0_03_00722", "Morio's Lab - Coin in Orange Block Shortcut Tunnel #6" },
            { "0_03_00723", "Morio's Lab - Coin in Orange Block Shortcut Tunnel #7" },
            { "0_03_00724", "Morio's Lab - Coin in Orange Block Shortcut Tunnel #8" },
            { "0_03_00725", "Morio's Lab - Coin in Orange Block Shortcut Tunnel #9" },
            { "0_03_00726", "Morio's Lab - Coin in Orange Block Shortcut Tunnel #10" },
            { "0_03_00727", "Morio's Lab - Coin in Orange Block Shortcut Tunnel #11" },
            { "0_03_00728", "Morio's Lab - Coin in Orange Block Shortcut Tunnel #12" },
            { "0_03_00729", "Morio's Lab - Coin in Orange Block Shortcut Tunnel #13" },
            { "0_03_00730", "Morio's Lab - Coin in Orange Block Shortcut Tunnel #14" },
            { "0_03_00731", "Morio's Lab - Coin in Orange Block Shortcut Tunnel #15" },
            { "0_03_00721", "Morio's Lab - Coin Bag in Orange Block Shortcut Tunnel" }
        };

        [Description("Morio's Lab - Fifth Floor Morio's Mind Area")]
        public static Dictionary<string, string> MoriosLabFifthFloorMoriosMindArea = new()
        {
            { string.Empty, nameof(MoriosLabFifthFloorMoriosMindArea) + " - !PLACEHOLDER!" }
        };

        [Description("Morio's Lab - Fifth Floor Ruined Observatory Area")]
        public static Dictionary<string, string> MoriosLabFifthFloorRuinedObservatoryArea = new()
        {
            { string.Empty, nameof(MoriosLabFifthFloorRuinedObservatoryArea) + " - !PLACEHOLDER!" }
        };

        [Description("Morio's Lab - Fifth Floor Golden Propeller")]
        public static Dictionary<string, string> MoriosLabFifthFloorGoldenPropeller = new()
        {
            { string.Empty, nameof(MoriosLabFifthFloorGoldenPropeller) + " - !PLACEHOLDER!" }
        };

        [Description("Morio's Lab - Fifth Floor Inside Shortcut Pipe")]
        public static Dictionary<string, string> MoriosLabFifthFloorShortcutPipe = new()
        {
            { string.Empty, nameof(MoriosLabFifthFloorShortcutPipe) + " - !PLACEHOLDER!" }
        };

        [Description("Morio's Lab - Ledge Above Ruined Observatory Portal")]
        public static Dictionary<string, string> MoriosLabFifthFloorLowerLedge = new()
        {
            { "0_03_00833", "Morio's Lab - Coin on Ledge Above Ruined Observatory Portal #1" },
            { "0_03_00832", "Morio's Lab - Coin on Ledge Above Ruined Observatory Portal #2" },
            { "0_03_00831", "Morio's Lab - Coin on Ledge Above Ruined Observatory Portal #3" },
            { "0_03_00830", "Morio's Lab - Coin on Ledge Above Ruined Observatory Portal #4" },
            { "0_03_00829", "Morio's Lab - Coin on Ledge Above Ruined Observatory Portal #5" },
            { "0_03_00828", "Morio's Lab - Coin on Ledge Above Ruined Observatory Portal #6" },
            { "0_03_00827", "Morio's Lab - Coin on Ledge Above Ruined Observatory Portal #7" },
            { "0_03_00826", "Morio's Lab - Coin on Ledge Above Ruined Observatory Portal #8" },
            { "0_03_00825", "Morio's Lab - Coin on Ledge Above Ruined Observatory Portal #9" },
            { "0_03_00824", "Morio's Lab - Coin on Ledge Above Ruined Observatory Portal #10" },
            { "0_03_00823", "Morio's Lab - Coin on Ledge Above Ruined Observatory Portal #11" },
            { "0_03_00822", "Morio's Lab - Coin on Ledge Above Ruined Observatory Portal #12" },
            { "0_03_00821", "Morio's Lab - Coin on Ledge Above Ruined Observatory Portal #13" },
            { "0_03_00820", "Morio's Lab - Coin on Ledge Above Ruined Observatory Portal #14" },
            { "0_03_00816", "Morio's Lab - Coin on Ledge Above Ruined Observatory Portal #15" },
            { "0_03_00812", "Morio's Lab - Coin Bag on Ledge Above Ruined Observatory Portal" },
            { "0_03_00819", "Morio's Lab - Coin on Pillar Near Ledge Above Ruined Observatory Portal #1" },
            { "0_03_00815", "Morio's Lab - Coin on Pillar Near Ledge Above Ruined Observatory Portal #2" },
            { "0_03_00811", "Morio's Lab - Coin on Pillar Near Ledge Above Ruined Observatory Portal #3" },
            { "0_03_00818", "Morio's Lab - Coin on Pillar Near Ledge Above Ruined Observatory Portal #4" },
            { "0_03_00814", "Morio's Lab - Coin on Pillar Near Ledge Above Ruined Observatory Portal #5" },
            { "0_03_00810", "Morio's Lab - Coin on Pillar Near Ledge Above Ruined Observatory Portal #6" },
            { "0_03_00817", "Morio's Lab - Coin on Pillar Near Ledge Above Ruined Observatory Portal #7" },
            { "0_03_00813", "Morio's Lab - Coin on Pillar Near Ledge Above Ruined Observatory Portal #8" },
            { "0_03_00809", "Morio's Lab - Coin on Pillar Near Ledge Above Ruined Observatory Portal #9" },
        };

        [Description("Morio's Lab - Ledge Below Tosla HQ Portal")]
        public static Dictionary<string, string> MoriosLabFifthToSixthFloorStair = new()
        {
            { "0_03_00851", "Morio's Lab - Coin Bag on Ledge Below Tosla HQ Portal #1" },
            { "0_03_00852", "Morio's Lab - Coin Bag on Ledge Below Tosla HQ Portal #2" },
            { "0_03_00853", "Morio's Lab - Coin Bag on Ledge Below Tosla HQ Portal #3" },
        };

        [Description("Morio's Lab - Fifth Floor Low Pillars")]
        public static Dictionary<string, string> MoriosLabFifthFloorLowPillars = new()
        {
            { "0_03_00840", "Morio's Lab - Coin on Medium Pillar Above Ruined Observatory Portal #1" },
            { "0_03_00843", "Morio's Lab - Coin on Medium Pillar Above Ruined Observatory Portal #2" },
            { "0_03_00841", "Morio's Lab - Coin on Medium Pillar Above Ruined Observatory Portal #3" },
            { "0_03_00844", "Morio's Lab - Coin on Medium Pillar Above Ruined Observatory Portal #4" },
            { "0_03_00842", "Morio's Lab - Chest on Medium Pillar Above Ruined Observatory Portal" },
        };

        [Description("Morio's Lab - Fifth Floor High Pillars")]
        public static Dictionary<string, string> MoriosLabFifthFloorHighPillars = new()
        {
            { "0_03_00855", "Morio's Lab - Coin on High Pillar Above Ruined Observatory Portal #1" },
            { "0_03_00858", "Morio's Lab - Coin on High Pillar Above Ruined Observatory Portal #2" },
            { "0_03_00857", "Morio's Lab - Coin on High Pillar Above Ruined Observatory Portal #3" },
            { "0_03_00856", "Morio's Lab - Coin on High Pillar Above Ruined Observatory Portal #4" },
            { "0_03_00859", "Morio's Lab - Coin on High Pillar Above Ruined Observatory Portal #5" },
            { "0_03_00835", "Morio's Lab - Coin on Pillar Attached to Wall Above Ruined Observatory Portal #1" },
            { "0_03_00838", "Morio's Lab - Coin on Pillar Attached to Wall Above Ruined Observatory Portal #2" },
            { "0_03_00839", "Morio's Lab - Coin on Pillar Attached to Wall Above Ruined Observatory Portal #3" },
            { "0_03_00836", "Morio's Lab - Coin on Pillar Attached to Wall Above Ruined Observatory Portal #4" },
        };

        [Description("Morio's Lab - Final Floor")]
        public static Dictionary<string, string> MoriosLabFinalFloor = new()
        {
            { "0_08_00004", "Morio's Lab - PICI Backflip Tutorial" },
        };

        [Description("Morio's Lab - Final Floor Pipes")]
        public static Dictionary<string, string> MoriosLabFinalFloorPipes = new()
        {
            { "0_03_00860", "Morio's Lab - Coin Bag on Pipes on Final Floor #1" },
            { "0_03_00861", "Morio's Lab - Coin Bag on Pipes on Final Floor #2" },
            { "0_03_00862", "Morio's Lab - Chest on Pipes on Final Floor #1" },
            { "0_03_00863", "Morio's Lab - Chest on Pipes on Final Floor #2" },
            { "0_02_00002", "Morio's Lab - Bunny - Shortcut From Final Floor"},
            { "0_03_00845", "Morio's Lab - Coin in Shortcut From Final Floor #1" },
            { "0_03_00846", "Morio's Lab - Coin in Shortcut From Final Floor #2" },
            { "0_03_00847", "Morio's Lab - Coin in Shortcut From Final Floor #3" },
            { "0_03_00848", "Morio's Lab - Coin in Shortcut From Final Floor #4" },
            { "0_03_00849", "Morio's Lab - Coin in Shortcut From Final Floor #5" },
        };

        [Description("Morio's Lab - Final Floor Catwalk")]
        public static Dictionary<string, string> MoriosLabFinalFloorCatwalk = new()
        {
            { "0_03_00867", "Morio's Lab - Coin on Catwalk Above Tosla HQ Portal #1" },
            { "0_03_00868", "Morio's Lab - Coin on Catwalk Above Tosla HQ Portal #2" },
            { "0_03_00869", "Morio's Lab - Coin on Catwalk Above Tosla HQ Portal #3" },
            { "0_03_00873", "Morio's Lab - Coin on Catwalk Above Tosla HQ Portal #4" },
            { "0_03_00874", "Morio's Lab - Coin on Catwalk Above Tosla HQ Portal #5" },
            { "0_03_00875", "Morio's Lab - Coin on Catwalk Above Tosla HQ Portal #6" },
            { "0_03_00870", "Morio's Lab - Coin Bag on Catwalk Above Tosla HQ Portal #1" },
            { "0_03_00871", "Morio's Lab - Coin Bag on Catwalk Above Tosla HQ Portal #2" },
            { "0_03_00872", "Morio's Lab - Coin Bag on Catwalk Above Tosla HQ Portal #3" },
        };

        [Description("Hub - Special Rules")]
        public static Dictionary<string, string> HubSpecialRules = new()
        {
            { "Granny's Island - Safe on Ocean Pillar", "B2 | B1+GP" },
            { "Granny's Island - Safe on Granny's Statue", "J2 | J1+GP" },
            { "Granny's Island - Coin Bag on Pillar Towards Sewer", "B1/GP" },
            { "Granny's Island - Gear - Oil Pump", "SP | X2" },
            { "Granny's Island - Gear - Inside Spin Blocks", "SP" },
            { "Granny's Island - Gear - Lighthouse", "B2 | B1+GP" },
            { "Granny's Island - Gear - In the Clouds", "B1+GP | B2+J1" },
            { "Granny's Island - Gear - High Pillar by Lab", "J1/GP" },
            { "Crash Again - Gear" , "SP+B1 | X1+SP | X2" },
            { "Pizza Oven - Gear", "B1 & J1/X1" },
            { "Morio's Lab - Gear - Third Floor Wrenches", "J1" },
            { "Morio's Lab - Coin on Second Floor After Pizza Time Portal #3", "X1/FGU" },
            { "Morio's Lab - Coin on Second Floor After Pizza Time Portal #4", "FGU" },
            { "Morio's Lab - Coin on Second Floor After Pizza Time Portal #5", "FGU" },
            { "Morio's Lab - Coin Bag on Second Floor", "FGU" },
        };

        #endregion

        #region Morio's Island

        [Description("Morio's Island - Starting Area")]
        public static Dictionary<string, string> MoriosIslandStartingArea = new()
        {
            { "3_03_00094", "Morio's Island - Coin in Starting Area Arrow #1" },
            { "3_03_00095", "Morio's Island - Coin in Starting Area Arrow #2" },
            { "3_03_00097", "Morio's Island - Coin in Starting Area Arrow #3" },
            { "3_03_00098", "Morio's Island - Coin in Starting Area Arrow #4" },
            { "3_03_00109", "Morio's Island - Coin in Starting Area Arrow #5" },
            { "3_03_00112", "Morio's Island - Coin in Starting Area Arrow #6" },
            { "3_03_00114", "Morio's Island - Coin in Starting Area Arrow #7" },
            { "3_03_00110", "Morio's Island - Coin in Starting Area Arrow #8" },
            { "3_03_00113", "Morio's Island - Coin in Starting Area Arrow #9" },
            { "3_03_00111", "Morio's Island - Coin in Starting Area Arrow #10" },
            { "3_03_00108", "Morio's Island - Coin in Starting Area Arrow #11" },
            { "3_03_00157", "Morio's Island - Coin in Starting Area Back Row #1" },
            { "3_03_00158", "Morio's Island - Coin in Starting Area Back Row #2" },
            { "3_03_00159", "Morio's Island - Coin in Starting Area Back Row #3" },
            { "3_03_00160", "Morio's Island - Coin in Starting Area Back Row #4" },
            { "3_03_00161", "Morio's Island - Coin in Starting Area Back Row #5" },
            { "3_03_00175", "Morio's Island - Coin in Starting Area Left Triangle #1" },
            { "3_03_00177", "Morio's Island - Coin in Starting Area Left Triangle #2" },
            { "3_03_00179", "Morio's Island - Coin in Starting Area Left Triangle #3" },
            { "3_03_00182", "Morio's Island - Coin in Starting Area Left Triangle #4" },
            { "3_03_00189", "Morio's Island - Coin in Starting Area Left Triangle #5" },
            { "3_03_00174", "Morio's Island - Coin in Starting Area Right Triangle #1" },
            { "3_03_00176", "Morio's Island - Coin in Starting Area Right Triangle #2" },
            { "3_03_00178", "Morio's Island - Coin in Starting Area Right Triangle #3" },
            { "3_03_00181", "Morio's Island - Coin in Starting Area Right Triangle #4" },
            { "3_03_00188", "Morio's Island - Coin in Starting Area Right Triangle #5" },
            { "3_03_00116", "Morio's Island - Coin in Starting Area Forward Row #1" },
            { "3_03_00117", "Morio's Island - Coin in Starting Area Forward Row #2" },
            { "3_03_00118", "Morio's Island - Coin in Starting Area Forward Row #3" },
            { "3_03_00119", "Morio's Island - Coin in Starting Area Forward Row #4" },
            { "3_03_00120", "Morio's Island - Coin in Starting Area Forward Row #5" },
            { "3_03_00124", "Morio's Island - Coin in Starting Area Forward Row #6" },
            { "3_03_00125", "Morio's Island - Coin in Starting Area Forward Row #7" },
            { "3_03_00126", "Morio's Island - Coin in Starting Area Forward Row #8" },
            { "3_03_00127", "Morio's Island - Coin in Starting Area Forward Row #9" },
            //{ "0_08_00001", "Morio's Island - Talk to Morio" },
        };

        [Description("Morio's Island - First Hurdle")]
        public static Dictionary<string, string> MoriosIslandFirstHurdle = new()
        {
            { "3_03_00063", "Morio's Island - Coin in Row Before First Checkpoint #1" },
            { "3_03_00064", "Morio's Island - Coin in Row Before First Checkpoint #2" },
            { "3_03_00065", "Morio's Island - Coin in Row Before First Checkpoint #3" },
            { "3_09_01175", "Morio's Island - Checkpoint After First Hurdle" },
            { "3_03_00066", "Morio's Island - Coin on First Hurdle Ramp #1" },
            { "3_03_00067", "Morio's Island - Coin on First Hurdle Ramp #2" },
            { "3_03_00142", "Morio's Island - Coin on First Hurdle Ramp #3" },
            { "3_03_00146", "Morio's Island - Coin on First Hurdle Ramp #4" },
            { "3_03_00203", "Morio's Island - Coin on First Hurdle Ramp #5" },
            { "3_21_00001", "Morio's Island - Cheese Behind First Hurdle Ramp" },
            { "3_03_00046", "Morio's Island - Coin Behind First Hurdle Ramp Left #1" },
            { "3_03_00048", "Morio's Island - Coin Behind First Hurdle Ramp Left #2" },
            { "3_03_00050", "Morio's Island - Coin Behind First Hurdle Ramp Left #3" },
            { "3_03_00052", "Morio's Island - Coin Behind First Hurdle Ramp Left #4" },
            { "3_03_00056", "Morio's Island - Coin Behind First Hurdle Ramp Left #5" },
            { "3_03_00055", "Morio's Island - Coin Behind First Hurdle Ramp Left #6" },
            { "3_03_00045", "Morio's Island - Coin Behind First Hurdle Ramp Right #1" },
            { "3_03_00047", "Morio's Island - Coin Behind First Hurdle Ramp Right #2" },
            { "3_03_00049", "Morio's Island - Coin Behind First Hurdle Ramp Right #3" },
            { "3_03_00051", "Morio's Island - Coin Behind First Hurdle Ramp Right #4" },
            { "3_03_00053", "Morio's Island - Coin Behind First Hurdle Ramp Right #5" },
            { "3_03_00054", "Morio's Island - Coin Behind First Hurdle Ramp Right #6" },
        };

        [Description("Morio's Island - High Ground")]
        public static Dictionary<string, string> MoriosIslandHighGround = new()
        {
            { "3_03_00314", "Morio's Island - Coin in Triangle on Starting Area Left Cliff #1" },
            { "3_03_00318", "Morio's Island - Coin in Triangle on Starting Area Left Cliff #2" },
            { "3_03_00324", "Morio's Island - Coin in Triangle on Starting Area Left Cliff #3" },
            { "3_03_00328", "Morio's Island - Coin in Triangle on Starting Area Left Cliff #4" },
            { "3_03_00330", "Morio's Island - Coin in Triangle on Starting Area Left Cliff #5" },
            { "3_03_00317", "Morio's Island - Coin in Triangle on Starting Area Left Cliff #6" },
            { "3_03_00323", "Morio's Island - Coin in Triangle on Starting Area Left Cliff #7" },
            { "3_03_00327", "Morio's Island - Coin in Triangle on Starting Area Left Cliff #8" },
            { "3_03_00322", "Morio's Island - Coin in Triangle on Starting Area Left Cliff #9" },
            { "3_03_00335", "Morio's Island - Coin in Row on Starting Area Left Cliff #1" },
            { "3_03_00337", "Morio's Island - Coin in Row on Starting Area Left Cliff #2" },
            { "3_03_00339", "Morio's Island - Coin in Row on Starting Area Left Cliff #3" },
            { "3_03_00341", "Morio's Island - Coin in Row on Starting Area Left Cliff #4" },
            { "3_03_00343", "Morio's Island - Coin in Row on Starting Area Left Cliff #5" },
            { "3_03_00345", "Morio's Island - Coin Bag on Starting Area Left Cliff" },
            { "3_03_00313", "Morio's Island - Coin in Triangle on Starting Area Right Cliff #1" },
            { "3_03_00315", "Morio's Island - Coin in Triangle on Starting Area Right Cliff #2" },
            { "3_03_00319", "Morio's Island - Coin in Triangle on Starting Area Right Cliff #3" },
            { "3_03_00325", "Morio's Island - Coin in Triangle on Starting Area Right Cliff #4" },
            { "3_03_00329", "Morio's Island - Coin in Triangle on Starting Area Right Cliff #5" },
            { "3_03_00316", "Morio's Island - Coin in Triangle on Starting Area Right Cliff #6" },
            { "3_03_00320", "Morio's Island - Coin in Triangle on Starting Area Right Cliff #7" },
            { "3_03_00326", "Morio's Island - Coin in Triangle on Starting Area Right Cliff #8" },
            { "3_03_00321", "Morio's Island - Coin in Triangle on Starting Area Right Cliff #9" },
            { "3_03_00334", "Morio's Island - Coin in Row on Starting Area Right Cliff #1" },
            { "3_03_00336", "Morio's Island - Coin in Row on Starting Area Right Cliff #2" },
            { "3_03_00338", "Morio's Island - Coin in Row on Starting Area Right Cliff #3" },
            { "3_03_00340", "Morio's Island - Coin in Row on Starting Area Right Cliff #4" },
            { "3_03_00342", "Morio's Island - Coin in Row on Starting Area Right Cliff #5" },
            { "3_03_00344", "Morio's Island - Coin Bag on Starting Area Right Cliff" },
            { "3_01_00004", "Morio's Island - Gear - After First Ramp" },
            { "3_03_00425", "Morio's Island - Coin on First Stone Arch #1" },
            { "3_03_00424", "Morio's Island - Coin Bag on First Stone Arch #1" },
            { "3_03_00423", "Morio's Island - Chest on First Stone Arch" },
            { "3_03_00422", "Morio's Island - Coin Bag on First Stone Arch #2" },
            { "3_03_00421", "Morio's Island - Coin on First Stone Arch #2" },
            { "3_03_00213", "Morio's Island - Coin on First Stone Ramp #1" },
            { "3_03_00212", "Morio's Island - Coin on First Stone Ramp #2" },
            { "3_03_00211", "Morio's Island - Coin on First Stone Ramp #3" },
            { "3_03_00210", "Morio's Island - Coin on First Stone Ramp #4" },
            { "3_03_00221", "Morio's Island - Coin on First Stone Ramp #5" },
            { "3_03_00292", "Morio's Island - Coin on First Stone Ramp #6" },
            { "3_03_00355", "Morio's Island - Coin on First Stone Ramp #7" },
            { "3_03_00387", "Morio's Island - Coin on First Stone Ramp #8" },
            { "3_03_00394", "Morio's Island - Coin on First Stone Ramp #9" },
            { "3_03_00400", "Morio's Island - Coin on First Stone Ramp #10" },
            { "3_03_00426", "Morio's Island - Coin on First Stone Ramp #11" },
            { "3_03_00429", "Morio's Island - Coin on First Stone Ramp #12" },
            { "3_03_00428", "Morio's Island - Coin on First Stone Ramp #13" },
            { "3_03_00427", "Morio's Island - Coin on First Stone Ramp #14" },
            { "3_21_00003", "Morio's Island - Cheese Behind First Stone Ramp" },
            { "3_03_00218", "Morio's Island - Coin Behind First Stone Ramp #1" },
            { "3_03_00216", "Morio's Island - Coin Behind First Stone Ramp #2" },
            { "3_03_00214", "Morio's Island - Coin Behind First Stone Ramp #3" },
            { "3_03_00209", "Morio's Island - Coin Behind First Stone Ramp #4" },
            { "3_03_00208", "Morio's Island - Coin Behind First Stone Ramp #5" },
            { "3_03_00206", "Morio's Island - Coin Behind First Stone Ramp #6" },
            { "3_03_00217", "Morio's Island - Coin Behind First Stone Ramp #7" },
            { "3_03_00215", "Morio's Island - Coin Behind First Stone Ramp #8" },
            { "3_03_00207", "Morio's Island - Coin Behind First Stone Ramp #9" },
            { "3_03_00205", "Morio's Island - Coin Behind First Stone Ramp #10" },
            { "3_03_00350", "Morio's Island - Coin Before Second Checkpoint #1" },
            { "3_03_00349", "Morio's Island - Coin Before Second Checkpoint #2" },
            { "3_03_00348", "Morio's Island - Coin Before Second Checkpoint #3" },
            { "3_03_00347", "Morio's Island - Coin Before Second Checkpoint #4" },
            { "3_03_00346", "Morio's Island - Coin Before Second Checkpoint #5" },
            { "3_09_01125", "Morio's Island - Checkpoint Before Stone Section" },
            { "3_01_00010", "Morio's Island - Gear - On Stone Pedestal" },
            { "3_03_00384", "Morio's Island - Coin on Ramp Island #1" },
            { "3_03_00383", "Morio's Island - Coin on Ramp Island #2" },
            { "3_03_00386", "Morio's Island - Coin on Ramp Island #3" },
            { "3_03_00391", "Morio's Island - Coin on Ramp Island #4" },
            { "3_03_00289", "Morio's Island - Coin in Arrow Towards Home Island #1" },
            { "3_03_00288", "Morio's Island - Coin in Arrow Towards Home Island #2" },
            { "3_03_00287", "Morio's Island - Coin in Arrow Towards Home Island #3" },
            { "3_03_00286", "Morio's Island - Coin in Arrow Towards Home Island #4" },
            { "3_03_00283", "Morio's Island - Coin in Arrow Towards Home Island #5" },
            { "3_03_00285", "Morio's Island - Coin in Arrow Towards Home Island #6" },
            { "3_03_00282", "Morio's Island - Coin in Arrow Towards Home Island #7" },
            { "3_03_00284", "Morio's Island - Coin in Arrow Towards Home Island #8" },
            { "3_03_00281", "Morio's Island - Coin in Arrow Towards Home Island #9" },
            { "3_03_00279", "Morio's Island - Coin in Arrow Towards Home Island #10" },
            { "3_03_00280", "Morio's Island - Coin in Arrow Towards Home Island #11" },
            { "3_03_00278", "Morio's Island - Coin in Arrow Towards Home Island #12" },
            { "3_03_00277", "Morio's Island - Coin in Arrow Towards Home Island #13" },
            { "3_03_00135", "Morio's Island - Coin in Zigzag Leading to Morio's Home #1" },
            { "3_03_00132", "Morio's Island - Coin in Zigzag Leading to Morio's Home #2" },
            { "3_03_00131", "Morio's Island - Coin in Zigzag Leading to Morio's Home #3" },
            { "3_03_00130", "Morio's Island - Coin in Zigzag Leading to Morio's Home #4" },
            { "3_03_00129", "Morio's Island - Coin in Zigzag Leading to Morio's Home #5" },
            { "3_03_00128", "Morio's Island - Coin in Zigzag Leading to Morio's Home #6" },
            { "3_01_00014", "Morio's Island - Gear - Stone Island Above Morio's Home Island" },
            { "3_03_00333", "Morio's Island - Coin Near Fence on Island Leading to Morio's Home #1" },
            { "3_03_00332", "Morio's Island - Coin Near Fence on Island Leading to Morio's Home #2" },
            { "3_03_00331", "Morio's Island - Coin Bag Near Fence on Island Leading to Morio's Home" },
            { "3_03_00198", "Morio's Island - Coin on Gem-Shaped Island #1" },
            { "3_03_00197", "Morio's Island - Coin on Gem-Shaped Island #2" },
            { "3_03_00196", "Morio's Island - Coin on Gem-Shaped Island #3" },
            { "3_03_00195", "Morio's Island - Coin on Gem-Shaped Island #4" },
            { "3_03_00194", "Morio's Island - Coin on Gem-Shaped Island #5" },
            { "3_03_00193", "Morio's Island - Coin on Gem-Shaped Island #6" },
            { "3_03_00192", "Morio's Island - Coin on Gem-Shaped Island #7" },
            { "3_03_00084", "Morio's Island - Coin on Umbrella #1" },
            { "3_03_00085", "Morio's Island - Coin on Umbrella #2" },
            { "3_03_00086", "Morio's Island - Coin on Umbrella #3" },
            { "3_03_00082", "Morio's Island - Coin on Umbrella #4" },
            { "3_03_00083", "Morio's Island - Coin on Umbrella #5" },
            { "3_03_00079", "Morio's Island - Coin on Umbrella #6" },
            { "3_03_00080", "Morio's Island - Coin on Umbrella #7" },
            { "3_03_00081", "Morio's Island - Coin on Umbrella #8" },
            { "3_03_00143", "Morio's Island - Coin Bag on Umbrella" },
            { "3_03_00090", "Morio's Island - Coin on Morio's Garage #1" },
            { "3_03_00091", "Morio's Island - Coin on Morio's Garage #2" },
            { "3_03_00092", "Morio's Island - Coin on Morio's Garage #3" },
            { "3_03_00087", "Morio's Island - Coin on Morio's Garage #4" },
            { "3_03_00088", "Morio's Island - Coin on Morio's Garage #5" },
            { "3_03_00089", "Morio's Island - Coin on Morio's Garage #6" },
            { "3_01_00000", "Morio's Island - Gear - On Morio's Roof" },
            { "3_03_00290", "Morio's Island - Coin Bag on Home Island Stone Pillar #2" },
            { "3_03_00385", "Morio's Island - Chest on Home Island Stone Pillar" },
            { "3_01_00009", "Morio's Island - Gear - On the Rubber Ducks" },
            { "3_03_00014", "Morio's Island - Coin on Low Stone Island After Rubber Ducks #1" },
            { "3_03_00013", "Morio's Island - Coin on Low Stone Island After Rubber Ducks #2" },
            { "3_03_00012", "Morio's Island - Coin on Low Stone Island After Rubber Ducks #3" },
            { "3_03_00011", "Morio's Island - Coin on Low Stone Island After Rubber Ducks #4" },
            { "3_03_00010", "Morio's Island - Coin on Low Stone Island After Rubber Ducks #5" },
            { "3_03_00009", "Morio's Island - Coin on Low Stone Island After Rubber Ducks #6" },
            { "3_03_00008", "Morio's Island - Coin on Low Stone Island After Rubber Ducks #7" },
            { "3_03_00007", "Morio's Island - Coin on Low Stone Island After Rubber Ducks #8" },
            { "3_03_00006", "Morio's Island - Coin on Low Stone Island After Rubber Ducks #9" },
            { "3_03_00005", "Morio's Island - Coin on Low Stone Island After Rubber Ducks #10" },
            { "3_03_00311", "Morio's Island - Coin in Oval on High Stone Island on Rubber Duck Path #1" },
            { "3_03_00308", "Morio's Island - Coin in Oval on High Stone Island on Rubber Duck Path #2" },
            { "3_03_00305", "Morio's Island - Coin in Oval on High Stone Island on Rubber Duck Path #3" },
            { "3_03_00312", "Morio's Island - Coin in Oval on High Stone Island on Rubber Duck Path #4" },
            { "3_03_00310", "Morio's Island - Coin in Oval on High Stone Island on Rubber Duck Path #5" },
            { "3_03_00307", "Morio's Island - Coin in Oval on High Stone Island on Rubber Duck Path #6" },
            { "3_03_00304", "Morio's Island - Coin in Oval on High Stone Island on Rubber Duck Path #7" },
            { "3_03_00302", "Morio's Island - Coin in Oval on High Stone Island on Rubber Duck Path #8" },
            { "3_03_00309", "Morio's Island - Coin in Oval on High Stone Island on Rubber Duck Path #9" },
            { "3_03_00306", "Morio's Island - Coin in Oval on High Stone Island on Rubber Duck Path #10" },
            { "3_03_00303", "Morio's Island - Coin in Oval on High Stone Island on Rubber Duck Path #11" },
            { "3_03_00372", "Morio's Island - Coin in Line on High Stone Island on Rubber Duck Path #1" },
            { "3_03_00371", "Morio's Island - Coin in Line on High Stone Island on Rubber Duck Path #2" },
            { "3_03_00370", "Morio's Island - Coin in Line on High Stone Island on Rubber Duck Path #3" },
            { "3_03_00369", "Morio's Island - Coin in Line on High Stone Island on Rubber Duck Path #4" },
            { "3_03_00368", "Morio's Island - Coin in Line on High Stone Island on Rubber Duck Path #5" },
            { "3_21_00000", "Morio's Island - Cheese on High Stone Island on Rubber Duck Path" },
            { "3_03_00269", "Morio's Island - Coin on High Grass Island on Rubber Duck Path #1" },
            { "3_03_00268", "Morio's Island - Coin on High Grass Island on Rubber Duck Path #2" },
            { "3_03_00267", "Morio's Island - Coin on High Grass Island on Rubber Duck Path #3" },
            { "3_03_00265", "Morio's Island - Coin on High Grass Island on Rubber Duck Path #4" },
            { "3_03_00263", "Morio's Island - Coin on High Grass Island on Rubber Duck Path #5" },
            { "3_03_00170", "Morio's Island - Coin on Low Grass Island on Rubber Duck Path #1" },
            { "3_03_00169", "Morio's Island - Coin on Low Grass Island on Rubber Duck Path #2" },
            { "3_03_00168", "Morio's Island - Coin on Low Grass Island on Rubber Duck Path #3" },
            { "3_03_00167", "Morio's Island - Coin on Low Grass Island on Rubber Duck Path #4" },
            { "3_03_00166", "Morio's Island - Coin on Low Grass Island on Rubber Duck Path #5" },
            { "3_03_00015", "Morio's Island - Coin Bag on Low Stone Island Near Mori-O-Tron #1" },
            { "3_03_00016", "Morio's Island - Coin on Low Stone Island Near Mori-O-Tron #1" },
            { "3_03_00017", "Morio's Island - Coin on Low Stone Island Near Mori-O-Tron #2" },
            { "3_03_00018", "Morio's Island - Coin on Low Stone Island Near Mori-O-Tron #3" },
            { "3_03_00019", "Morio's Island - Coin on Low Stone Island Near Mori-O-Tron #4" },
            { "3_03_00020", "Morio's Island - Coin on Low Stone Island Near Mori-O-Tron #5" },
            { "3_03_00021", "Morio's Island - Coin Bag on Low Stone Island Near Mori-O-Tron #2" },
        };

        [Description("Morio's Island - Morio's Home Island")]
        public static Dictionary<string, string> MoriosIslandHomeIsland = new()
        {
            { "3_09_00436", "Morio's Island - Checkpoint Near Morio's Home" },
            { "3_03_00043", "Morio's Island - Coin on Home Island Stone Pillar #1" },
            { "3_03_00057", "Morio's Island - Coin on Home Island Stone Pillar #2" },
            { "3_03_00059", "Morio's Island - Coin on Home Island Stone Pillar #3" },
            { "3_03_00058", "Morio's Island - Coin on Home Island Stone Pillar #4" },
            { "3_03_00044", "Morio's Island - Coin on Home Island Stone Pillar #5" },
            { "3_03_00093", "Morio's Island - Coin Bag on Home Island Stone Pillar #1" },
            { "3_03_00003", "Morio's Island - Coin in Ocean #1" },
            { "3_03_00004", "Morio's Island - Coin in Ocean #2" },
            { "3_03_00000", "Morio's Island - Coin in Ocean #3" },
            { "3_03_00001", "Morio's Island - Coin in Ocean #4" },
            { "3_03_00002", "Morio's Island - Coin Bag in Ocean" },
            { "3_21_00002", "Morio's Island - Cheese Near Morio's Home" },
        };

        [Description("Morio's Island - Stone Arch in Ocean Near Morio's Home")]
        public static Dictionary<string, string> MoriosIslandLowStoneArch = new()
        {
            { "3_03_00149", "Morio's Island - Coin on Stone Arch Near Morio's Home #1" },
            { "3_03_00150", "Morio's Island - Coin on Stone Arch Near Morio's Home #2" },
            { "3_03_00153", "Morio's Island - Coin on Stone Arch Near Morio's Home #3" },
            { "3_03_00152", "Morio's Island - Coin on Stone Arch Near Morio's Home #4" },
            { "3_03_00151", "Morio's Island - Coin on Stone Arch Near Morio's Home #5" },
            { "3_03_00156", "Morio's Island - Coin on Stone Arch Near Morio's Home #6" },
            { "3_03_00155", "Morio's Island - Coin Bag on Stone Arch Near Morio's Home" },
            { "3_03_00154", "Morio's Island - Chest Stone Arch Near Morio's Home" },
        };

        #endregion

        public static List<Tuple<string, Dictionary<string, string>>> KnownIDs =
        [
            // Hub IDs
            new(GetDescription(nameof(GrannysIslandStart)), GrannysIslandStart),
            new(GetDescription(nameof(GrannysIslandMoat)), GrannysIslandMoat),
            new(GetDescription(nameof(GrannysIslandMain)), GrannysIslandMain),
            new(GetDescription(nameof(GrannysIslandPipeArea)), GrannysIslandPipeArea),
            new(GetDescription(nameof(GrannysIslandBrokenPierShallow)), GrannysIslandBrokenPierShallow),
            new(GetDescription(nameof(GrannysIslandBrokenPierDeep)), GrannysIslandBrokenPierDeep),
            new(GetDescription(nameof(GrannysIslandBrokenPierExtraDeep)), GrannysIslandBrokenPierExtraDeep),
            new(GetDescription(nameof(GrannysIslandLabHillExpert1HighGround)), GrannysIslandLabHillExpert1HighGround),
            new(GetDescription(nameof(GrannysIslandLabHillHighGround)), GrannysIslandLabHillHighGround),
            new(GetDescription(nameof(GrannysIslandExpert1HighGround)), GrannysIslandExpert1HighGround),
            new(GetDescription(nameof(GrannysIslandExpert2HighGround)), GrannysIslandExpert2HighGround),
            new(GetDescription(nameof(GrannysIslandHighGround)), GrannysIslandHighGround),
            new(GetDescription(nameof(GrannysIslandRocketTop)), GrannysIslandRocketTop),
            new(GetDescription(nameof(GrannysIslandConstructionArch)), GrannysIslandConstructionArch),
            new(GetDescription(nameof(GrannysIslandOceanPillar)), GrannysIslandOceanPillar),
            new(GetDescription(nameof(GrannysIslandTowardsSewerIsland1)), GrannysIslandTowardsSewerIsland1),
            new(GetDescription(nameof(GrannysIslandTowardsSewerIsland2)), GrannysIslandTowardsSewerIsland2),
            new(GetDescription(nameof(GrannysIslandTowardsSewerIsland2Tree)), GrannysIslandTowardsSewerIsland2Tree),
            new(GetDescription(nameof(GrannysIslandSewerIsland)), GrannysIslandSewerIsland),
            new(GetDescription(nameof(GrannysIslandSewerIslandUpper)), GrannysIslandSewerIslandUpper),
            new(GetDescription(nameof(GrannysIslandCloroPhilIsland)), GrannysIslandCloroPhilIsland),
            new(GetDescription(nameof(GrannysIslandHighPillarByLab)), GrannysIslandHighPillarByLab),
            new(GetDescription(nameof(GrannysIslandCrashAgainIsland)), GrannysIslandCrashAgainIsland),
            new(GetDescription(nameof(GrannysIslandCrashAgainRoof)), GrannysIslandCrashAgainRoof),

            new(GetDescription(nameof(HubLawFirm)), HubLawFirm),
            new(GetDescription(nameof(HubLawFirmJump)), HubLawFirmJump),
            new(GetDescription(nameof(HubCrashAgain)), HubCrashAgain),
            new(GetDescription(nameof(HubIceCreamTruckBase)), HubIceCreamTruckBase),
            new(GetDescription(nameof(HubIceCreamTruckHighGround)), HubIceCreamTruckHighGround),
            new(GetDescription(nameof(HubPizzaOven)), HubPizzaOven),
            new(GetDescription(nameof(HubPizzaOvenPillar)), HubPizzaOvenPillar),
            new(GetDescription(nameof(HubHatWorld)), HubHatWorld),

            new(GetDescription(nameof(MoriosLabGroundFloor)), MoriosLabGroundFloor),
            new(GetDescription(nameof(MoriosWardrobe)), MoriosWardrobe),
            new(GetDescription(nameof(MoriosLabGroundFloorWrenches)), MoriosLabGroundFloorWrenches),
            new(GetDescription(nameof(MoriosLabGroundFloorLowestBolt)), MoriosLabGroundFloorLowestBolt),
            new(GetDescription(nameof(MoriosLabGroundFloorBolts)), MoriosLabGroundFloorBolts),
            new(GetDescription(nameof(MoriosLabGroundFloorOrangeBlocks)), MoriosLabGroundFloorOrangeBlocks),
            new(GetDescription(nameof(MoriosLabBunnyLedge)), MoriosLabBunnyLedge),
            new(GetDescription(nameof(MoriosLabSecondFloor)), MoriosLabSecondFloor),
            new(GetDescription(nameof(MoriosLabSecondFloorAboveDemoWall)), MoriosLabSecondFloorAboveDemoWall),
            new(GetDescription(nameof(MoriosLabSecondFloorAfterDemoWall)), MoriosLabSecondFloorAfterDemoWall),
            new(GetDescription(nameof(MoriosLabSecondFloorInTrueDemoWall)), MoriosLabSecondFloorInTrueDemoWall),
            new(GetDescription(nameof(MoriosLabSecondFloorAfterTrueDemoWall)), MoriosLabSecondFloorAfterTrueDemoWall),
            new(GetDescription(nameof(MoriosLabSecondFloorShortcutPipe)), MoriosLabSecondFloorShortcutPipe),
            new(GetDescription(nameof(MoriosLabSecondFloorShortcutPipeFalling)), MoriosLabSecondFloorShortcutPipeFalling),
            new(GetDescription(nameof(MoriosLabPathToMoriosRoom)), MoriosLabPathToMoriosRoom),
            new(GetDescription(nameof(MoriosLabMoriosRoomJump)), MoriosLabMoriosRoomJump),
            new(GetDescription(nameof(MoriosLabThirdFloor)), MoriosLabThirdFloor),
            new(GetDescription(nameof(MoriosLabThirdFloorWrenchesLower)), MoriosLabThirdFloorWrenchesLower),
            new(GetDescription(nameof(MoriosLabThirdFloorWrenchesMiddle)), MoriosLabThirdFloorWrenchesMiddle),
            new(GetDescription(nameof(MoriosLabThirdFloorWrenchesUpper)), MoriosLabThirdFloorWrenchesUpper),
            new(GetDescription(nameof(MoriosLabFourthFloor)), MoriosLabFourthFloor),
            new(GetDescription(nameof(MoriosLabFourthFloorJumpSpikes)), MoriosLabFourthFloorJumpSpikes),
            new(GetDescription(nameof(MoriosLabLedgeAboveMauriziosCity)), MoriosLabLedgeAboveMauriziosCity),
            new(GetDescription(nameof(MoriosLabFifthFloorCrashTestArea)), MoriosLabFifthFloorCrashTestArea),
            new(GetDescription(nameof(MoriosLabFifthFloorMoriosMindArea)), MoriosLabFifthFloorMoriosMindArea),
            new(GetDescription(nameof(MoriosLabFifthFloorRuinedObservatoryArea)), MoriosLabFifthFloorRuinedObservatoryArea),
            new(GetDescription(nameof(MoriosLabFifthFloorGoldenPropeller)), MoriosLabFifthFloorGoldenPropeller),
            new(GetDescription(nameof(MoriosLabFifthFloorShortcutPipe)), MoriosLabFifthFloorShortcutPipe),
            new(GetDescription(nameof(MoriosLabFifthFloorLowerLedge)), MoriosLabFifthFloorLowerLedge),
            new(GetDescription(nameof(MoriosLabFifthToSixthFloorStair)), MoriosLabFifthToSixthFloorStair),
            new(GetDescription(nameof(MoriosLabFifthFloorLowPillars)), MoriosLabFifthFloorLowPillars),
            new(GetDescription(nameof(MoriosLabFifthFloorLowPillars)), MoriosLabFifthFloorLowPillars),
            new(GetDescription(nameof(MoriosLabFifthFloorHighPillars)), MoriosLabFifthFloorHighPillars),
            new(GetDescription(nameof(MoriosLabFinalFloor)), MoriosLabFinalFloor),
            new(GetDescription(nameof(MoriosLabFinalFloorPipes)), MoriosLabFinalFloorPipes),
            new(GetDescription(nameof(MoriosLabFinalFloorCatwalk)), MoriosLabFinalFloorCatwalk),

            // Morio's Island Areas
            new(GetDescription(nameof(MoriosIslandStartingArea)), MoriosIslandStartingArea),
            new(GetDescription(nameof(MoriosIslandFirstHurdle)), MoriosIslandFirstHurdle),
            new(GetDescription(nameof(MoriosIslandHighGround)), MoriosIslandHighGround),
            new(GetDescription(nameof(MoriosIslandHomeIsland)), MoriosIslandHomeIsland),
            new(GetDescription(nameof(MoriosIslandLowStoneArch)), MoriosIslandLowStoneArch),
        ];

        public static Dictionary<string, Dictionary<string, string>> SpecialRules = new()
        {
            { nameof(Data.LevelId.Hub), HubSpecialRules },
        };

        public static Dictionary<string, List<Dictionary<string, string>>> PerLevelIDs = new()
        {
            {
                nameof(Data.LevelId.Hub),
                [
                    GrannysIslandStart,
                    GrannysIslandMoat,
                    GrannysIslandMain,
                    GrannysIslandPipeArea,
                    GrannysIslandBrokenPierShallow,
                    GrannysIslandBrokenPierDeep,
                    GrannysIslandBrokenPierExtraDeep,
                    GrannysIslandLabHillExpert1HighGround,
                    GrannysIslandLabHillHighGround,
                    GrannysIslandExpert1HighGround,
                    GrannysIslandExpert2HighGround,
                    GrannysIslandHighGround,
                    GrannysIslandRocketTop,
                    GrannysIslandConstructionArch,
                    GrannysIslandOceanPillar,
                    GrannysIslandTowardsSewerIsland1,
                    GrannysIslandTowardsSewerIsland2,
                    GrannysIslandTowardsSewerIsland2Tree,
                    GrannysIslandSewerIsland,
                    GrannysIslandSewerIslandUpper,
                    GrannysIslandCloroPhilIsland,
                    GrannysIslandHighPillarByLab,
                    GrannysIslandCrashAgainIsland,
                    GrannysIslandCrashAgainRoof,

                    HubLawFirm,
                    HubLawFirmJump,
                    HubCrashAgain,
                    HubIceCreamTruckBase,
                    HubIceCreamTruckHighGround,
                    HubPizzaOven,
                    HubPizzaOvenPillar,
                    HubHatWorld,

                    MoriosLabGroundFloor,
                    MoriosWardrobe,
                    MoriosLabGroundFloorWrenches,
                    MoriosLabGroundFloorLowestBolt,
                    MoriosLabGroundFloorBolts,
                    MoriosLabGroundFloorOrangeBlocks,
                    MoriosLabBunnyLedge,
                    MoriosLabSecondFloor,
                    MoriosLabSecondFloorAboveDemoWall,
                    MoriosLabSecondFloorAfterDemoWall,
                    MoriosLabSecondFloorInTrueDemoWall,
                    MoriosLabSecondFloorAfterTrueDemoWall,
                    MoriosLabSecondFloorShortcutPipeFalling,
                    MoriosLabSecondFloorShortcutPipe,
                    MoriosLabPathToMoriosRoom,
                    MoriosLabMoriosRoomJump,
                    MoriosLabThirdFloor,
                    MoriosLabThirdFloorWrenchesLower,
                    MoriosLabThirdFloorWrenchesMiddle,
                    MoriosLabThirdFloorWrenchesUpper,
                    MoriosLabFourthFloor,
                    MoriosLabFourthFloorJumpSpikes,
                    MoriosLabLedgeAboveMauriziosCity,
                    MoriosLabFifthFloorCrashTestArea,
                    MoriosLabFifthFloorMoriosMindArea,
                    MoriosLabFifthFloorRuinedObservatoryArea,
                    MoriosLabFifthFloorGoldenPropeller,
                    MoriosLabFifthFloorShortcutPipe,
                    MoriosLabFifthFloorLowerLedge,
                    MoriosLabFifthToSixthFloorStair,
                    MoriosLabFifthFloorLowPillars,
                    MoriosLabFifthFloorHighPillars,
                    MoriosLabFinalFloor,
                    MoriosLabFinalFloorPipes,
                    MoriosLabFinalFloorCatwalk,
                ]
            },
            {
                nameof(Data.LevelId.L3_MoriosHome),
                [
                    MoriosIslandStartingArea,
                    MoriosIslandHighGround,
                    MoriosIslandHomeIsland,
                ]
            },
        };

        public enum ConnectionType
        {
            Connection,
            Subwarp,
            Warp,
        }

        public class RegionConnection
        {
            public string Name;
            public string DestinationRegion;
            public ConnectionType ConnectingType;
            public string Rules;

            public RegionConnection(string name, Dictionary<string, string> destinationRegion, ConnectionType type, string rules = "")
            {
                Name = name;
                DestinationRegion = KnownIDs.First(o => o.Item2.Equals(destinationRegion)).Item1;
                ConnectingType = type;
                Rules = rules;
            }
            public RegionConnection(Dictionary<string, string> destinationRegion, string rules = "")
            {
                Name = string.Empty;
                DestinationRegion = KnownIDs.First(o => o.Item2.Equals(destinationRegion)).Item1;
                ConnectingType = ConnectionType.Connection;
                Rules = rules;
            }
        }

        public static Dictionary<string, List<RegionConnection>> RegionConnections = new()
        {
            #region Hub Connections
            // Granny's Island
            {
                GetDescription(nameof(GrannysIslandStart)),
                [
                    new RegionConnection("Granny's Island - Morio's Lab Front Door", MoriosLabGroundFloor, ConnectionType.Subwarp),
                    new RegionConnection(GrannysIslandPipeArea, "J1/B1"),
                ]
            },
            {
                GetDescription(nameof(GrannysIslandMoat)),
                [
                    new RegionConnection("Granny's Island - Morio's Lab Back Door", MoriosLabGroundFloor, ConnectionType.Subwarp),
                    new RegionConnection(GrannysIslandStart),
                    new RegionConnection(GrannysIslandPipeArea, "J1/B1"),
                ]
            },
            {
                GetDescription(nameof(GrannysIslandMain)),
                [
                    new RegionConnection(GrannysIslandMoat),
                    new RegionConnection("Granny's Island - Beach Pipe", GrannysIslandPipeArea, ConnectionType.Subwarp),
                    new RegionConnection(GrannysIslandBrokenPierShallow, "B1+X1 | J1 | GP"),
                    new RegionConnection(GrannysIslandBrokenPierDeep, "J1 & B1/X1 | GP | J2 | B2+X1"),
                    new RegionConnection(GrannysIslandBrokenPierExtraDeep, "J1 & B1/X1 | GP | J2"),
                    new RegionConnection(GrannysIslandExpert1HighGround, "X1/J1/B1/GP"),
                    new RegionConnection(GrannysIslandExpert2HighGround, "X2/J1/B1/GP"),
                    new RegionConnection(GrannysIslandHighGround, "J1/B1/GP"),
                    new RegionConnection(GrannysIslandRocketTop, "Rocket+B1"),
                    new RegionConnection(GrannysIslandConstructionArch, "B1 | GP+J1"),
                    new RegionConnection(GrannysIslandOceanPillar, "B1 | GP+OS"),
                    new RegionConnection(GrannysIslandTowardsSewerIsland1, "B1 | GP+OS & J1/X2"),
                    new RegionConnection(GrannysIslandTowardsSewerIsland2, "B1 & OS/GP/J1"),
                    new RegionConnection(GrannysIslandCloroPhilIsland, "GP | B2 | X1 & B1/J2"),
                    new RegionConnection(GrannysIslandHighPillarByLab, "B1 & X1/GP/J1"),
                    new RegionConnection(GrannysIslandCrashAgainIsland, "B2+GP"),
                    new RegionConnection(GrannysIslandCrashAgainRoof, "B2+GP+X1 | GP & OS & J1/B1"),
                    new RegionConnection("Granny's Island - Law Firm Roof Entrance", HubLawFirm, ConnectionType.Subwarp),
                    new RegionConnection("Granny's Island - Pizza Oven Entrance", HubPizzaOven, ConnectionType.Subwarp, "PizzaKing"),
                    new RegionConnection("Granny's Island - Ice Cream Truck Entrance", HubIceCreamTruckBase, ConnectionType.Subwarp, "GelaToni"),
                    new RegionConnection("Granny's Island - Hat World Entrance", HubHatWorld, ConnectionType.Subwarp),
                    // TODO: GYM GEARS CONNECTION //new RegionConnection("Granny's Island - Gym Gears Front Door", GymGearsEntrance, ConnectionType.Warp)
                ]
            },
            {
                GetDescription(nameof(GrannysIslandPipeArea)),
                [
                    new RegionConnection(GrannysIslandMain),
                    new RegionConnection("Granny's Island - Hillside Pipe", GrannysIslandMain, ConnectionType.Subwarp),
                ]
            },
            {
                GetDescription(nameof(GrannysIslandBrokenPierShallow)),
                [
                    // Connections to other piers are handled by Granny's Island Main
                ]
            },
            {
                GetDescription(nameof(GrannysIslandBrokenPierDeep)),
                [
                    // Connections to other piers are handled by Granny's Island Main
                ]
            },
            {
                GetDescription(nameof(GrannysIslandBrokenPierExtraDeep)),
                [
                    // Connections to other piers are handled by Granny's Island Main
                ]
            },
            {
                GetDescription(nameof(GrannysIslandLabHillExpert1HighGround)),
                [
                    // Doesn't connect anywhere
                ]
            },
            {
                GetDescription(nameof(GrannysIslandLabHillHighGround)),
                [
                    new RegionConnection(GrannysIslandCrashAgainIsland, "OS"),
                    new RegionConnection(GrannysIslandMain),
                    new RegionConnection(GrannysIslandLabHillExpert1HighGround),
                ]
            },
            {
                GetDescription(nameof(GrannysIslandExpert1HighGround)),
                [
                    new RegionConnection(GrannysIslandLabHillExpert1HighGround)
                ]
            },
            {
                GetDescription(nameof(GrannysIslandExpert2HighGround)),
                [
                    // Doesn't connect anywhere
                ]
            },
            {
                GetDescription(nameof(GrannysIslandHighGround)),
                [
                    new RegionConnection(GrannysIslandLabHillHighGround),
                ]
            },
            {
                GetDescription(nameof(GrannysIslandRocketTop)),
                [
                    new RegionConnection(GrannysIslandTowardsSewerIsland1, "B1"),
                    new RegionConnection(GrannysIslandTowardsSewerIsland2Tree, "B1+GP"),
                ]
            },
            {
                GetDescription(nameof(GrannysIslandConstructionArch)),
                [
                    // Doesn't connect anywhere
                ]
            },
            {
                GetDescription(nameof(GrannysIslandOceanPillar)),
                [
                    // Doesn't connect anywhere
                ]
            },
            {
                GetDescription(nameof(GrannysIslandTowardsSewerIsland1)),
                [
                    new RegionConnection(GrannysIslandMain, "J1/B1"),
                    new RegionConnection(GrannysIslandTowardsSewerIsland2, "OS+GP"),
                ]
            },
            {
                GetDescription(nameof(GrannysIslandTowardsSewerIsland2)),
                [
                    new RegionConnection(GrannysIslandTowardsSewerIsland1, "OS+J1 | B1"),
                    new RegionConnection(GrannysIslandTowardsSewerIsland2Tree, "J1"),
                    new RegionConnection(GrannysIslandSewerIsland, "OS"),
                ]
            },
            {
                GetDescription(nameof(GrannysIslandTowardsSewerIsland2Tree)),
                [
                    new RegionConnection(GrannysIslandTowardsSewerIsland2),
                ]
            },
            {
                GetDescription(nameof(GrannysIslandSewerIsland)),
                [
                    new RegionConnection(GrannysIslandTowardsSewerIsland2, "OS"),
                    new RegionConnection(GrannysIslandSewerIslandUpper, "X1/J1/B1")
                ]
            },
            {
                GetDescription(nameof(GrannysIslandSewerIslandUpper)),
                [
                    // Doesn't connect anywhere
                ]
            },
            {
                GetDescription(nameof(GrannysIslandCloroPhilIsland)),
                [
                    // Doesn't connect anywhere
                ]
            },
            {
                GetDescription(nameof(GrannysIslandHighPillarByLab)),
                [
                    // Doesn't connect anywhere
                ]
            },
            {
                GetDescription(nameof(GrannysIslandCrashAgainIsland)),
                [
                    new RegionConnection("Crash Again - Entrance", HubCrashAgain, ConnectionType.Subwarp),
                    new RegionConnection(GrannysIslandCrashAgainRoof, "J2"),
                    new RegionConnection(GrannysIslandLabHillHighGround, "OS"),
                ]
            },
            {
                GetDescription(nameof(GrannysIslandCrashAgainRoof)),
                [
                    // Doesn't connect anywhere
                ]
            },
            // Granny's Island Subareas
            {
                GetDescription(nameof(HubLawFirm)),
                [
                    new RegionConnection("Law Firm - Exit", GrannysIslandMain, ConnectionType.Subwarp),
                    new RegionConnection(HubLawFirmJump, "J1"),
                ]
            },
            {
                GetDescription(nameof(HubLawFirmJump)),
                [
                    // Doesn't connect anywhere
                ]
            },
            {
                GetDescription(nameof(HubCrashAgain)),
                [
                    new RegionConnection("Crash Again - Exit", GrannysIslandMain, ConnectionType.Subwarp),
                ]
            },
            {
                GetDescription(nameof(HubIceCreamTruckBase)),
                [
                    new RegionConnection("Ice Cream Truck - Exit", GrannysIslandMain, ConnectionType.Subwarp),
                    new RegionConnection(HubIceCreamTruckHighGround, "B1/J2"),
                ]
            },
            {
                GetDescription(nameof(HubIceCreamTruckHighGround)),
                [
                    // Doesn't connect anywhere
                ]
            },
            {
                GetDescription(nameof(HubHatWorld)),
                [
                    new RegionConnection("Granny's Island Hat World - Exit", GrannysIslandMain, ConnectionType.Subwarp),
                ]
            },
            {
                GetDescription(nameof(HubPizzaOven)),
                [
                    new RegionConnection("Pizza Oven - Exit", GrannysIslandMain, ConnectionType.Subwarp),
                    new RegionConnection(HubPizzaOvenPillar, "B2 & J1/X1"),
                ]
            },
            {
                GetDescription(nameof(HubPizzaOvenPillar)),
                [
                    // Doesn't connect anywhere
                ]
            },
            // Morio's Lab
            {
                GetDescription(nameof(MoriosLabGroundFloor)),
                [
                    new RegionConnection("Morio's Lab - Front Door", GrannysIslandStart, ConnectionType.Subwarp),
                    new RegionConnection("Morio's Lab - Back Door", GrannysIslandMoat, ConnectionType.Subwarp),
                    new RegionConnection("Morio's Lab - Wardrobe Entrance", MoriosWardrobe, ConnectionType.Subwarp),
                    new RegionConnection(MoriosLabGroundFloorOrangeBlocks, "B2"),
                    new RegionConnection(MoriosLabGroundFloorWrenches, "B1/J2"),
                    new RegionConnection(MoriosLabSecondFloor, "J1/B1"),
                    new RegionConnection(MoriosLabFifthFloorCrashTestArea, "B2+X2+FGU"),
                ]
            },
            {
                GetDescription(nameof(MoriosWardrobe)),
                [
                    new RegionConnection("Morio's Wardrobe - Exit", MoriosLabGroundFloor, ConnectionType.Subwarp),
                ]
            },
            {
                GetDescription(nameof(MoriosLabGroundFloorWrenches)),
                [
                    new RegionConnection(MoriosLabFifthFloorCrashTestArea, "J1+OS+FGU"),
                    new RegionConnection(MoriosLabGroundFloorBolts, "J1"),
                    new RegionConnection(MoriosLabGroundFloor),
                    new RegionConnection(MoriosLabGroundFloorOrangeBlocks, "J1+OS | J2"),
                ]
            },
            {
                GetDescription(nameof(MoriosLabGroundFloorLowestBolt)),
                [
                    // No connections
                ]
            },
            {
                GetDescription(nameof(MoriosLabGroundFloorBolts)),
                [
                    new RegionConnection(MoriosLabGroundFloorLowestBolt),
                    new RegionConnection(MoriosLabBunnyLedge, "J1 | X1+B1"),
                ]
            },
            {
                GetDescription(nameof(MoriosLabGroundFloorOrangeBlocks)),
                [
                    // No connections
                ]
            },
            {
                GetDescription(nameof(MoriosLabSecondFloor)),
                [
                    new RegionConnection(MoriosLabPathToMoriosRoom, "B1"),
                    new RegionConnection(MoriosLabGroundFloor),
                    new RegionConnection(MoriosLabSecondFloorShortcutPipe, "X1+B1+J1"),
                    new RegionConnection(MoriosLabSecondFloorAboveDemoWall, "B2"),
                    new RegionConnection(MoriosLabSecondFloorAfterDemoWall, "FGU"),
                ]
            },
            {
                GetDescription(nameof(MoriosLabSecondFloorAboveDemoWall)),
                [
                    new RegionConnection(MoriosLabSecondFloorAfterDemoWall),
                ]
            },
            {
                GetDescription(nameof(MoriosLabSecondFloorAfterDemoWall)),
                [
                    new RegionConnection(MoriosLabSecondFloor, "FGU"),
                    new RegionConnection(MoriosLabSecondFloorInTrueDemoWall),
                ]
            },
            {
                GetDescription(nameof(MoriosLabSecondFloorInTrueDemoWall)),
                [
                    new RegionConnection(MoriosLabSecondFloorAfterDemoWall, "FGU"),
                    new RegionConnection(MoriosLabSecondFloorAfterTrueDemoWall, "FGU"),
                ]
            },
            {
                GetDescription(nameof(MoriosLabSecondFloorAfterTrueDemoWall)),
                [
                    new RegionConnection(MoriosLabSecondFloorInTrueDemoWall),
                    new RegionConnection(MoriosLabThirdFloor, "J1/B1"),
                ]
            },
            {
                GetDescription(nameof(MoriosLabSecondFloorShortcutPipeFalling)),
                [
                    new RegionConnection(MoriosLabSecondFloor),
                    new RegionConnection(MoriosLabSecondFloorShortcutPipe, "X1+J1"),
                ]
            },
            {
                GetDescription(nameof(MoriosLabSecondFloorShortcutPipe)),
                [
                    new RegionConnection("Morio's Lab - Second Floor Shortcut Pipe", MoriosLabFifthFloorShortcutPipe, ConnectionType.Subwarp),
                ]
            },
            {
                GetDescription(nameof(MoriosLabPathToMoriosRoom)),
                [
                    new RegionConnection(MoriosLabMoriosRoomJump, "J1+MorioHat"),
                    new RegionConnection(MoriosLabGroundFloorBolts, "B1"),
                    new RegionConnection(MoriosLabGroundFloorWrenches),
                    new RegionConnection(MoriosLabThirdFloorWrenchesLower, "FGU+B2"),
                    new RegionConnection(MoriosLabThirdFloorWrenchesMiddle, "FGU+B2"),
                    new RegionConnection(MoriosLabThirdFloorWrenchesUpper, "FGU+B2"),
                    new RegionConnection(MoriosLabSecondFloorAboveDemoWall, "J2"),
                ]
            },
            {
                GetDescription(nameof(MoriosLabMoriosRoomJump)),
                [
                    // Doesn't connect anywhere
                ]
            },
            {
                GetDescription(nameof(MoriosLabThirdFloor)),
                [
                    new RegionConnection(MoriosLabThirdFloorWrenchesLower, "J1"),
                    new RegionConnection(MoriosLabFourthFloor, "GS+B1"),
                    new RegionConnection(MoriosLabSecondFloor),
                ]
            },
            {
                GetDescription(nameof(MoriosLabThirdFloorWrenchesLower)),
                [
                    new RegionConnection(MoriosLabThirdFloorWrenchesMiddle, "J1+FGU"),
                    new RegionConnection(MoriosLabThirdFloor),
                ]
            },
            {
                GetDescription(nameof(MoriosLabThirdFloorWrenchesMiddle)),
                [
                    new RegionConnection(MoriosLabThirdFloorWrenchesUpper, "J1"),
                ]
            },
            {
                GetDescription(nameof(MoriosLabThirdFloorWrenchesUpper)),
                [
                    // Doesn't connect anywhere
                ]
            },
            {
                GetDescription(nameof(MoriosLabFourthFloor)),
                [
                    new RegionConnection(MoriosLabFourthFloorJumpSpikes, "GS+J1"),
                    new RegionConnection(MoriosLabFifthFloorCrashTestArea, "B1/J1"),
                    new RegionConnection(MoriosLabLedgeAboveMauriziosCity, "J2"),
                    new RegionConnection(MoriosLabThirdFloor, "GS"),
                ]
            },
            {
                GetDescription(nameof(MoriosLabFourthFloorJumpSpikes)),
                [
                    // Doesn't connect anywhere
                ]
            },
            {
                GetDescription(nameof(MoriosLabLedgeAboveMauriziosCity)),
                [
                    // Doesn't connect anywhere
                ]
            },
            {
                GetDescription(nameof(MoriosLabFifthFloorCrashTestArea)),
                [
                    new RegionConnection(MoriosLabFifthFloorMoriosMindArea, "OS/J2"),
                    new RegionConnection(MoriosLabGroundFloorBolts, "B1/J1"),
                    new RegionConnection(MoriosLabFourthFloor),
                    new RegionConnection(MoriosLabSecondFloor, "FGU"),
                    new RegionConnection(MoriosLabGroundFloorLowestBolt, "FGU"),
                    new RegionConnection(MoriosLabGroundFloorOrangeBlocks, "FGU"),
                    new RegionConnection(MoriosLabGroundFloorWrenches, "FGU"),
                ]
            },
            {
                GetDescription(nameof(MoriosLabFifthFloorMoriosMindArea)),
                [
                    new RegionConnection(MoriosLabFifthFloorRuinedObservatoryArea, "Password"),
                    new RegionConnection(MoriosLabFifthFloorCrashTestArea),
                ]
            },
            {
                GetDescription(nameof(MoriosLabFifthFloorRuinedObservatoryArea)),
                [
                    new RegionConnection(MoriosLabFifthFloorGoldenPropeller, "J1+GP"),
                    new RegionConnection(MoriosLabFifthFloorShortcutPipe, "J1+Password"),
                    new RegionConnection(MoriosLabFifthFloorLowerLedge, "B2 | B1 & X1/J1 | J2 | J1+X1"),
                    new RegionConnection(MoriosLabFifthFloorMoriosMindArea, "Password"),
                ]
            },
            {
                GetDescription(nameof(MoriosLabFifthFloorGoldenPropeller)),
                [
                    new RegionConnection(MoriosLabFifthToSixthFloorStair),
                    new RegionConnection(MoriosLabFifthFloorHighPillars),
                    new RegionConnection(MoriosLabFinalFloor),
                    new RegionConnection(MoriosLabFifthFloorShortcutPipe, "Password"),
                    new RegionConnection(MoriosLabPathToMoriosRoom, "Password+FGU"),
                    new RegionConnection(MoriosLabMoriosRoomJump, "Password+FGU+MorioHat"),
                    new RegionConnection(MoriosLabThirdFloorWrenchesLower, "Password"),
                    new RegionConnection(MoriosLabThirdFloorWrenchesMiddle, "Password+FGU"),
                    new RegionConnection(MoriosLabGroundFloorBolts, "Password"),
                    new RegionConnection(MoriosLabSecondFloorShortcutPipe, "Password"),
                    new RegionConnection(MoriosLabLedgeAboveMauriziosCity, "Password"),
                ]
            },
            {
                GetDescription(nameof(MoriosLabFifthFloorShortcutPipe)),
                [
                    new RegionConnection(MoriosLabFifthFloorRuinedObservatoryArea, "Password"),
                    new RegionConnection("Morio's Lab - Fifth Floor Shortcut Pipe", MoriosLabSecondFloorShortcutPipeFalling, ConnectionType.Subwarp)
                ]
            },
            {
                GetDescription(nameof(MoriosLabFifthFloorLowerLedge)),
                [
                    new RegionConnection(MoriosLabFifthToSixthFloorStair, "J1"),
                    new RegionConnection(MoriosLabFifthFloorLowPillars, "J1"),
                    new RegionConnection(MoriosLabFifthFloorRuinedObservatoryArea),
                ]
            },
            {
                GetDescription(nameof(MoriosLabFifthToSixthFloorStair)),
                [
                    new RegionConnection(MoriosLabFinalFloor, "J2 | X1+J1"),
                    new RegionConnection(MoriosLabFifthFloorLowerLedge),
                ]
            },
            {
                GetDescription(nameof(MoriosLabFifthFloorLowPillars)),
                [
                    // Doesn't connect anywhere
                ]
            },
            {
                GetDescription(nameof(MoriosLabFifthFloorHighPillars)),
                [
                    new RegionConnection(MoriosLabFifthFloorGoldenPropeller, "GP"),
                ]
            },
            {
                GetDescription(nameof(MoriosLabFinalFloor)),
                [
                    new RegionConnection(MoriosLabFifthFloorHighPillars, "J1 | B1+X1"),
                    new RegionConnection(MoriosLabFinalFloorPipes, "J1"),
                    new RegionConnection(MoriosLabFinalFloorCatwalk, "J2 | J1+X1"),
                    new RegionConnection(MoriosLabFifthToSixthFloorStair),
                ]
            },
            {
                GetDescription(nameof(MoriosLabFinalFloorPipes)),
                [
                    new RegionConnection(MoriosLabGroundFloorWrenches, "FGU"),
                    new RegionConnection(MoriosLabBunnyLedge, "FGU"),
                ]
            },
            {
                GetDescription(nameof(MoriosLabFinalFloorCatwalk)),
                [
                    // Doesn't connect anywhere
                ]
            },

            #endregion

            #region Morio's Island Connections

            {
                GetDescription(nameof(MoriosIslandStartingArea)),
                [
                    new RegionConnection(MoriosIslandFirstHurdle, "X2 | J1/B1"),
                ]
            },
            {
                GetDescription(nameof(MoriosIslandFirstHurdle)),
                [
                    new RegionConnection(MoriosIslandHighGround, "J1/B1"),
                ]
            },
            {
                GetDescription(nameof(MoriosIslandHighGround)),
                [
                    new RegionConnection(MoriosIslandHomeIsland),
                ]
            },
            {
                GetDescription(nameof(MoriosIslandHomeIsland)),
                [
                    new RegionConnection(MoriosIslandStartingArea, "J1/B1"),
                ]
            },

            #endregion
        };

        public static string GetRegionJsonName(Dictionary<string, string> region)
        {
            return GetRegionJsonName(KnownIDs.First(o => o.Item2.Equals(region)).Item1);
        }

        public static string GetRegionJsonName(string description)
        {
            var end = string.Empty;
            if (!description.Contains(" - "))
            {
                end = "/MAIN";
            }
            return "REGION_" + description.Replace(" - ", "/").Replace("'", "").Replace(" ", "_").ToUpper() + end;
        }

        public static bool CheckLocation(string type, string id)
        {
            if (!Enabled)
                return false;
            var areas = KnownIDs.Where(area => area.Item2.ContainsKey(id)).ToList();
            switch (areas.Count)
            {
                case 1:
                    if (!string.IsNullOrEmpty(type))
                        Plugin.Log($"[KNOWN] Picked up \"{areas[0].Item2[id]}\" From area \"{areas[0].Item1}\". ID: {id}");
                    return true;
                case > 1:
                    if (!string.IsNullOrEmpty(type))
                        Plugin.Log($"ERROR: ITEM WITH ID {id} IS FOUND IN MULTIPLE AREAS!");
                    break;
                default:
                    if (!string.IsNullOrEmpty(type))
                        Plugin.Log($"Picked up unknown item of type \"{type}\". ID: {id}");
                    break;
            }
            if (!string.IsNullOrEmpty(type))
                GUIUtility.systemCopyBuffer = id;
            return false;
        }

        public static Tuple<string, string> GetKnownItemNameArea(string id)
        {
            var areas = KnownIDs.Where(area => area.Item2.ContainsKey(id)).ToList();
            return areas.Count >= 1 ? new Tuple<string, string>(areas[0].Item2[id], areas[0].Item1) : null;
        }

        public static string GetDescription(string fieldName)
        {
            string result;
            FieldInfo fi = typeof(DebugLocationHelper).GetField(fieldName);
            if (fi != null)
            {
                try
                {
                    object[] descriptionAttrs = fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    DescriptionAttribute description = (DescriptionAttribute)descriptionAttrs[0];
                    result = (description.Description);
                }
                catch
                {
                    result = null;
                }
            }
            else
            {
                result = null;
            }

            return result;
        }
    }
#endif
}
