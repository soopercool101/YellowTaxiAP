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
        public static bool Enabled => true;

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
            { "0_01_00017", "Granny's Island - Gear - Escort Morco" },
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

        [Description("Crash Again - Starting Area")]
        public static Dictionary<string, string> HubCrashAgainStartingArea = new()
        {
            { "0_09_00613", "Crash Again - Checkpoint" },
        };

        [Description("Crash Again - End")]
        public static Dictionary<string, string> HubCrashAgainEnd = new()
        {
            { "0_01_00010", "Crash Again - Gear" },
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

        [Description("Morio's Lab - Psycho Taxi Arcade Machine")]
        public static Dictionary<string, string> MoriosLabPsychoTaxi = new()
        {
            { string.Empty, nameof(MoriosLabPsychoTaxi) + " - !PLACEHOLDER!" }
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
        };

        [Description("Morio's Lab - Outside Morio's Room")]
        public static Dictionary<string, string> MoriosLabMoriosRoomOutside = new()
        {
            { "0_03_00747", "Morio's Lab - Coin Near Morio's Room #1" },
            { "0_03_00746", "Morio's Lab - Coin Near Morio's Room #2" },
            { "0_03_00745", "Morio's Lab - Coin Near Morio's Room #3" },
            { "0_03_00744", "Morio's Lab - Coin Near Morio's Room #4" },
            { "0_03_00743", "Morio's Lab - Coin Near Morio's Room #5" },
        };

        [Description("Morio's Lab - Inside Morio's Room Jump")]
        public static Dictionary<string, string> MoriosLabMoriosRoomInsideJump = new()
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
            { string.Empty, nameof(MoriosLabSecondFloorShortcutPipe) + " - !PLACEHOLDER!" }
        };

        [Description("Morio's Lab - Fourth Floor Expert Jump Spikes")]
        public static Dictionary<string, string> MoriosLabFourthFloorExpertJumpSpikes = new()
        {
            { "0_01_00019", "Morio's Lab - Gear - On Spiky Cliffs" },
        };

        [Description("Morio's Lab - Fourth Floor Jump Spikes")]
        public static Dictionary<string, string> MoriosLabFourthFloorJumpSpikes = new()
        {
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

        [Description("Morio's Lab - Fifth Floor Golden Propeller (Password)")]
        public static Dictionary<string, string> MoriosLabFifthFloorGoldenPropellerPassword = new()
        {
            { string.Empty, nameof(MoriosLabFifthFloorGoldenPropellerPassword) + " - !PLACEHOLDER!" }
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
            { "Granny's Island - Gear - High Pillar by Lab", "X2/J1/GP" },
            { "Pizza Oven - Gear", "B1 & X1/J1" },
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

        [Description("Morio's Island - First Bunny Arch")]
        public static Dictionary<string, string> MoriosIslandFirstBunnyArch = new()
        {
            { "3_03_00431", "Morio's Island - Coin on First Bunny Arch #1" },
            { "3_03_00432", "Morio's Island - Coin on First Bunny Arch #2" },
            { "3_03_00433", "Morio's Island - Coin on First Bunny Arch #3" },
            { "3_03_00434", "Morio's Island - Coin on First Bunny Arch #4" },
            { "3_03_00435", "Morio's Island - Coin on First Bunny Arch #5" },
            { "3_03_00436", "Morio's Island - Coin on First Bunny Arch #6" },
            { "3_02_00000", "Morio's Island - Bunny - On First Optional Path" },
        };

        [Description("Morio's Island - Second Bunny Arch")]
        public static Dictionary<string, string> MoriosIslandSecondBunnyArch = new()
        {
            { "3_03_00417", "Morio's Island - Coin on Second Bunny Arch #1" },
            { "3_03_00420", "Morio's Island - Coin on Second Bunny Arch #2" },
            { "3_03_00419", "Morio's Island - Coin on Second Bunny Arch #3" },
            { "3_03_00418", "Morio's Island - Coin on Second Bunny Arch #4" },
            { "3_03_00412", "Morio's Island - Coin on Second Bunny Arch #5" },
            { "3_03_00411", "Morio's Island - Coin on Second Bunny Arch #6" },
            { "3_03_00410", "Morio's Island - Coin on Second Bunny Arch #7" },
            { "3_03_00413", "Morio's Island - Coin on Second Bunny Arch #8" },
            { "3_03_00416", "Morio's Island - Coin Bag on Second Bunny Arch #1" },
            { "3_03_00414", "Morio's Island - Coin Bag on Second Bunny Arch #2" },
            { "3_02_00001", "Morio's Island - Bunny - On Second Optional Path" },
        };

        [Description("Morio's Island - Center Stone Island")]
        public static Dictionary<string, string> MoriosIslandCenterIsland = new()
        {
            { "3_03_00062", "Morio's Island - Coin on Center Stone Island #1" },
            { "3_03_00061", "Morio's Island - Coin on Center Stone Island #2" },
            { "3_03_00060", "Morio's Island - Coin on Center Stone Island #3" },
            { "3_03_00141", "Morio's Island - Coin on Center Stone Island #4" },
            { "3_03_00140", "Morio's Island - Coin on Center Stone Island #5" },
            { "3_03_00138", "Morio's Island - Coin on Center Stone Island #6" },
            { "3_03_00139", "Morio's Island - Coin on Center Stone Island #7" },
            { "3_03_00137", "Morio's Island - Coin on Center Stone Island #8" },
            { "3_03_00136", "Morio's Island - Coin on Center Stone Island #9" },
            { "3_03_00134", "Morio's Island - Coin on Center Stone Island #10" },
            { "3_03_00133", "Morio's Island - Coin on Center Stone Island #11" },
            { "3_03_00199", "Morio's Island - Coin on Center Stone Island #12" },
            { "3_03_00200", "Morio's Island - Coin on Center Stone Island #13" },
            { "3_03_00202", "Morio's Island - Coin on Center Stone Island #14" },
            { "3_03_00201", "Morio's Island - Coin on Center Stone Island #15" },
            { "3_03_00276", "Morio's Island - Coin on Center Stone Island #16" },
            { "3_03_00275", "Morio's Island - Coin on Center Stone Island #17" },
            { "3_03_00272", "Morio's Island - Coin on Center Stone Island #18" },
            { "3_03_00270", "Morio's Island - Coin on Center Stone Island #19" },
            { "3_03_00271", "Morio's Island - Coin on Center Stone Island #20" },
            { "3_03_00274", "Morio's Island - Coin Bag on Center Stone Island" },
            { "3_03_00273", "Morio's Island - Chest on Center Stone Island" },
        };

        [Description("Morio's Island - Highest Ground")]
        public static Dictionary<string, string> MoriosIslandHighestGround = new()
        {
            { "3_03_00487", "Morio's Island - Coin on Dirt Arch #1" },
            { "3_03_00486", "Morio's Island - Coin on Dirt Arch #2" },
            { "3_03_00482", "Morio's Island - Coin on Dirt Arch #3" },
            { "3_03_00481", "Morio's Island - Coin on Dirt Arch #4" },
            { "3_03_00485", "Morio's Island - Coin Bag on Dirt Arch #1" },
            { "3_03_00483", "Morio's Island - Coin Bag on Dirt Arch #2" },
            { "3_03_00484", "Morio's Island - Chest on Dirt Arch" },
            { "3_03_00462", "Morio's Island - Coin on Highest Center Stone Pillar #1" },
            { "3_03_00461", "Morio's Island - Coin on Highest Center Stone Pillar #2" },
            { "3_03_00460", "Morio's Island - Coin on Highest Center Stone Pillar #3" },
            { "3_03_00459", "Morio's Island - Coin on Highest Center Stone Pillar #4" },
            { "3_03_00458", "Morio's Island - Coin on Highest Center Stone Pillar #5" },
            { "3_03_00457", "Morio's Island - Coin on Highest Center Stone Pillar #6" },
            { "3_03_00456", "Morio's Island - Coin on Highest Center Stone Pillar #7" },
            { "3_03_00409", "Morio's Island - Coin on Center Stone Cliffside #1" },
            { "3_03_00408", "Morio's Island - Coin on Center Stone Cliffside #2" },
            { "3_03_00407", "Morio's Island - Coin Bag on Center Stone Cliffside" },
            { "3_03_00442", "Morio's Island - Coin Bag on Center Stone Clifftop #1" },
            { "3_03_00441", "Morio's Island - Coin Bag on Center Stone Clifftop #2" },
            { "3_03_00440", "Morio's Island - Coin Bag on Center Stone Clifftop #3" },
        };

        [Description("Morio's Home - Starting Area")]
        public static Dictionary<string, string> MoriosHomeStartingArea = new()
        {
            { "3_03_00078", "Morio's Home - Coin on Garage Ramp #1" },
            { "3_03_00144", "Morio's Home - Coin on Garage Ramp #2" },
            { "3_03_00162", "Morio's Home - Coin on Garage Ramp #3" },
            { "3_03_00219", "Morio's Home - Coin on Garage Ramp #4" },
            { "3_03_00252", "Morio's Home - Coin on Garage Ramp #5" },
            { "3_03_00163", "Morio's Home - Coin on Garage Workbench #1" },
            { "3_03_00165", "Morio's Home - Coin on Garage Workbench #2" },
            { "3_03_00171", "Morio's Home - Coin on Garage Workbench #3" },
            { "3_03_00172", "Morio's Home - Coin on Garage Workbench #4" },
            { "3_03_00173", "Morio's Home - Coin on Garage Workbench #5" },
            { "3_03_00183", "Morio's Home - Coin in Living Room #1" },
            { "3_03_00184", "Morio's Home - Coin in Living Room #2" },
            { "3_03_00185", "Morio's Home - Coin in Living Room #3" },
            { "3_03_00186", "Morio's Home - Coin in Living Room #4" },
            { "3_03_00187", "Morio's Home - Coin on Living Room Couch #1" },
            { "3_03_00266", "Morio's Home - Coin on Living Room Couch #2" },
            { "3_03_00301", "Morio's Home - Coin on Living Room Couch #3" },
            { "3_03_00295", "Morio's Home - Coin on Bookshelf #1" },
            { "3_03_00294", "Morio's Home - Coin on Bookshelf #2" },
            { "3_03_00293", "Morio's Home - Coin Bag on Bookshelf" },
            { "3_01_00002", "Morio's Home - Gear - Living Room Nook" },
            { "3_02_00002", "Morio's Home - Bunny - In Hidden Room in Hallway" },
            { "3_03_00107", "Morio's Home - Coin in Hallway #1" },
            { "3_03_00106", "Morio's Home - Coin in Hallway #2" },
            { "3_03_00105", "Morio's Home - Coin in Hallway #3" },
            { "3_03_00104", "Morio's Home - Coin in Hallway #4" },
            { "3_03_00103", "Morio's Home - Coin in Hallway #5" },
            { "3_03_00102", "Morio's Home - Coin in Hallway #6" },
            { "3_03_00101", "Morio's Home - Coin in Hallway #7" },
            { "3_03_00100", "Morio's Home - Coin in Hallway #8" },
            { "3_03_00099", "Morio's Home - Coin in Hallway #9" },
            { "3_03_00096", "Morio's Home - Coin in Hallway #10" },
            { "3_03_00145", "Morio's Home - Coin in Hallway #11" },
            { "3_03_00180", "Morio's Home - Coin in Hallway #12" },
            { "3_03_00220", "Morio's Home - Coin in Hallway #13" },
            { "3_03_00262", "Morio's Home - Coin in Hallway #14" },
            { "3_03_00261", "Morio's Home - Coin in Hallway #15" },
            { "3_03_00259", "Morio's Home - Coin in Hallway #16" },
            { "3_03_00257", "Morio's Home - Coin in Hallway #17" },
            { "3_03_00255", "Morio's Home - Coin in Hallway #18" },
            { "3_03_00254", "Morio's Home - Coin in Hallway #19" },
            { "3_03_00253", "Morio's Home - Coin in Hallway #20" },
            { "3_03_00256", "Morio's Home - Coin in Hallway #21" },
            { "3_03_00258", "Morio's Home - Coin in Hallway #22" },
            { "3_03_00260", "Morio's Home - Coin in Hallway #23" },
            { "3_03_00373", "Morio's Home - Coin in Bedroom Before Checkpoint #1" },
            { "3_03_00374", "Morio's Home - Coin in Bedroom Before Checkpoint #2" },
            { "3_03_00375", "Morio's Home - Coin in Bedroom Before Checkpoint #3" },
            { "3_03_00376", "Morio's Home - Coin in Bedroom Before Checkpoint #4" },
            { "3_03_00377", "Morio's Home - Coin in Bedroom Before Checkpoint #5" },
            { "3_09_00380", "Morio's Home - Checkpoint in Bedroom" },
            { "3_03_00378", "Morio's Home - Coin Leading to Weird Tunnels Door #1" },
            { "3_03_00379", "Morio's Home - Coin Leading to Weird Tunnels Door #2" },
            { "3_03_00380", "Morio's Home - Coin Leading to Weird Tunnels Door #3" },
            { "3_03_00390", "Morio's Home - Coin on Bump in Bedroom #1" },
            { "3_03_00388", "Morio's Home - Coin on Bump in Bedroom #2" },
            { "3_03_00389", "Morio's Home - Coin Bag on Bump in Bedroom" },
            { "3_03_00406", "Morio's Home - Coin on First Bed in Bedroom #1" },
            { "3_03_00430", "Morio's Home - Coin on First Bed in Bedroom #2" },
            { "3_03_00439", "Morio's Home - Coin on Second Bed in Bedroom #1" },
            { "3_03_00447", "Morio's Home - Coin on Second Bed in Bedroom #2" },
            { "3_03_00454", "Morio's Home - Coin on Second Bed in Bedroom #3" },
            { "3_03_00480", "Morio's Home - Coin on Highest Bed in Bedroom #1" },
            { "3_03_00491", "Morio's Home - Coin on Highest Bed in Bedroom #2" },
            { "3_03_00493", "Morio's Home - Coin on Highest Bed in Bedroom #3" },
            { "3_03_00500", "Morio's Home - Coin Bag on Upper Bedroom Platform #1" },
            { "3_03_00498", "Morio's Home - Coin on Upper Bedroom Platform #1" },
            { "3_03_00495", "Morio's Home - Coin on Upper Bedroom Platform #2" },
            { "3_03_00496", "Morio's Home - Coin Bag on Upper Bedroom Platform #2" },
            { "3_03_00497", "Morio's Home - Coin on Upper Bedroom Platform #3" },
            { "3_03_00499", "Morio's Home - Coin on Upper Bedroom Platform #4" },
            { "3_03_00502", "Morio's Home - Coin Bag on Upper Bedroom Platform #3" },
            { "3_03_00504", "Morio's Home - Coin on Upper Bedroom Platform #5" },
            { "3_03_00507", "Morio's Home - Coin on Upper Bedroom Platform #6" },
            { "3_03_00506", "Morio's Home - Coin Bag on Upper Bedroom Platform #4" },
            { "3_03_00505", "Morio's Home - Coin on Upper Bedroom Platform #7" },
            { "3_03_00503", "Morio's Home - Coin on Upper Bedroom Platform #8" },
            { "3_01_00003", "Morio's Home - Gear - Upper Bedroom Platform" },
            { "3_03_00476", "Morio's Home - Coin on Bedroom Path #1" },
            { "3_03_00455", "Morio's Home - Coin on Bedroom Path #2" },
            { "3_03_00448", "Morio's Home - Coin Bag on Bedroom Path" },
            { "3_03_00438", "Morio's Home - Coin on Bedroom Bed Wall #1" },
            { "3_03_00446", "Morio's Home - Coin on Bedroom Bed Wall #2" },
            { "3_03_00453", "Morio's Home - Coin on Bedroom Bed Wall #3" },
            { "3_03_00475", "Morio's Home - Coin on Bedroom Bed Wall #4" },
            { "3_03_00473", "Morio's Home - Coin on Bedroom Bed Wall #5" },
            { "3_03_00451", "Morio's Home - Coin on Bedroom Bed Wall #6" },
            { "3_03_00470", "Morio's Home - Coin on Bedroom Bed Wall #7" },
            { "3_03_00478", "Morio's Home - Coin on Bedroom Bed Wall #8" },
            { "3_03_00490", "Morio's Home - Coin on Bedroom Bed Wall #9" },
            { "3_03_00489", "Morio's Home - Coin on Bedroom Bed Wall #10" },
            { "3_03_00405", "Morio's Home - Coin on Wooden Beam in Bedroom #1"},
            { "3_03_00404", "Morio's Home - Coin on Wooden Beam in Bedroom #2"},
            { "3_03_00402", "Morio's Home - Coin on Wooden Beam in Bedroom #3"},
            { "3_03_00401", "Morio's Home - Coin on Wooden Beam in Bedroom #4"},
            { "3_03_00403", "Morio's Home - Coin Bag on Wooden Beam in Bedroom"},
            { "3_01_00007", "Morio's Home - Gear - In Bedroom Corner" },
            // Special rules
            { "3_03_00395", "Morio's Home - Coin Bag on Garage Wall Shelf" },       // J1
            { "3_01_00013", "Morio's Home - Gear - In Hidden Room Near Garage" },   // X2/J1/B1
        };

        [Description("Morio's Home - Expert 1 High Ground")]
        public static Dictionary<string, string> MoriosHomeExpert1 = new()
        {
            { "3_03_00356", "Morio's Home - Coin on Garage Shelf #1" },
            { "3_03_00357", "Morio's Home - Coin on Garage Shelf #2" },
            { "3_03_00358", "Morio's Home - Coin on Garage Shelf #3" },
            { "3_03_00366", "Morio's Home - Coin on Garage Shelf #4" },
            { "3_03_00264", "Morio's Home - Safe in Hidden Room Near Garage" },
        };

        [Description("Morio's Home - High Ground")]
        public static Dictionary<string, string> MoriosHomeHighGround = new()
        {
            { "3_01_00015", "Morio's Home - Gear - On Garage Shelf" },
            { "3_03_00297", "Morio's Home - Coin on Garage Shelf #5" },
            { "3_03_00298", "Morio's Home - Coin on Garage Shelf #6" },
            { "3_03_00299", "Morio's Home - Coin Bag on Garage Standing Shelf" },
            { "3_03_00300", "Morio's Home - Chest on Garage Shelf" },
            { "3_03_00365", "Morio's Home - Coin Bag on Hallway Ledge #1" },
            { "3_03_00364", "Morio's Home - Coin on Hallway Ledge #1" },
            { "3_03_00363", "Morio's Home - Coin on Hallway Ledge #2" },
            { "3_03_00362", "Morio's Home - Coin on Hallway Ledge #3" },
            { "3_03_00361", "Morio's Home - Coin on Hallway Ledge #4" },
            { "3_03_00360", "Morio's Home - Coin on Hallway Ledge #5" },
            { "3_03_00359", "Morio's Home - Coin Bag on Hallway Ledge #2" },
            { "3_01_00011", "Morio's Home - Gear - Above Hallway" },
        };

        // Only relevant due to Mori-O-Tron
        [Description("Morio's Home - Before Bed Pillars")]
        public static Dictionary<string, string> MoriosHomeBeforeBedPillars = new()
        {
            { "3_09_00425", "Morio's Home - Checkpoint Before Bed Pillars" },
            { "3_03_00443", "Morio's Home - Coin on Bed Pillar #1" },
            { "3_03_00449", "Morio's Home - Coin on Bed Pillar #2" },
            { "3_03_00463", "Morio's Home - Coin on Bed Pillar #3" },
        };

        [Description("Morio's Home - Bed Pillars")]
        public static Dictionary<string, string> MoriosHomeBedPillars = new()
        {
            { "3_03_00469", "Morio's Home - Coin After Bed Pillars #1" },
            { "3_03_00472", "Morio's Home - Coin After Bed Pillars #2" },
            { "3_03_00471", "Morio's Home - Coin After Bed Pillars #3" },
            { "3_03_00467", "Morio's Home - Coin After Bed Pillars #4" },
            { "3_03_00465", "Morio's Home - Coin After Bed Pillars #5" },
            { "3_03_00466", "Morio's Home - Coin After Bed Pillars #6" },
            { "3_01_00012", "Morio's Home - Gear - After Bed Pillars" },
            { "3_03_00494", "Morio's Home - Safe After Bed Pillars" },
        };

        [Description("Morio's Home - Loft")]
        public static Dictionary<string, string> MoriosHomeLoft = new()
        {
            { "3_03_00511", "Morio's Home - Coin in Loft #1" },
            { "3_03_00516", "Morio's Home - Coin in Loft #2" },
            { "3_03_00521", "Morio's Home - Coin in Loft #3" },
            { "3_03_00526", "Morio's Home - Coin in Loft #4" },
            { "3_03_00531", "Morio's Home - Coin in Loft #5" },
            { "3_03_00508", "Morio's Home - Coin in Loft #6" },
            { "3_03_00512", "Morio's Home - Coin in Loft #7" },
            { "3_03_00517", "Morio's Home - Coin in Loft #8" },
            { "3_03_00518", "Morio's Home - Coin in Loft #9" },
            { "3_03_00527", "Morio's Home - Coin in Loft #10" },
            { "3_03_00532", "Morio's Home - Coin in Loft #11" },
            { "3_03_00536", "Morio's Home - Coin in Loft #12" },
            { "3_03_00509", "Morio's Home - Coin in Loft #13" },
            { "3_03_00522", "Morio's Home - Coin in Loft #14" },
            { "3_03_00528", "Morio's Home - Coin in Loft #15" },
            { "3_03_00537", "Morio's Home - Coin in Loft #16" },
            { "3_03_00510", "Morio's Home - Coin in Loft #17" },
            { "3_03_00514", "Morio's Home - Coin in Loft #18" },
            { "3_03_00519", "Morio's Home - Coin in Loft #19" },
            { "3_03_00524", "Morio's Home - Coin in Loft #20" },
            { "3_03_00525", "Morio's Home - Coin in Loft #21" },
            { "3_03_00534", "Morio's Home - Coin in Loft #23" },
            { "3_03_00538", "Morio's Home - Coin in Loft #24" },
            { "3_03_00515", "Morio's Home - Coin in Loft #25" },
            { "3_03_00520", "Morio's Home - Coin in Loft #26" },
            { "3_03_00529", "Morio's Home - Coin in Loft #27" },
            { "3_03_00530", "Morio's Home - Coin in Loft #28" },
            { "3_03_00535", "Morio's Home - Coin in Loft #29" },
            { "3_03_00513", "Morio's Home - Coin in Loft #30" },
            { "3_03_00523", "Morio's Home - Coin Bag in Loft #1" },
            { "3_03_00533", "Morio's Home - Coin Bag in Loft #2" },
        };

        [Description("Morio's Home - Kitchen")]
        public static Dictionary<string, string> MoriosHomeKitchen = new()
        {
            { "3_03_00190", "Morio's Home - Coin on Kitchen Sink" },
            { "3_03_00191", "Morio's Home - Coin on Kitchen Stove" },
            { "3_03_00121", "Morio's Home - Coin in Kitchen Hallway #1" },
            { "3_03_00122", "Morio's Home - Coin in Kitchen Hallway #2" },
            { "3_03_00123", "Morio's Home - Coin in Kitchen Hallway #3" },
            { "3_03_00147", "Morio's Home - Coin on Dining Room Cupboard #1" },
            { "3_03_00148", "Morio's Home - Coin on Dining Room Cupboard #2" },
            { "3_01_00001", "Morio's Home - Gear - In Dining Room Nook" },
        };

        [Description("Morio's Home - Kitchen Expert 1")]
        public static Dictionary<string, string> MoriosHomeKitchenExpert1 = new()
        {
            { "3_03_00222", "Morio's Home - Coin on Dining Room Pantry #1" },
            { "3_03_00223", "Morio's Home - Coin on Dining Room Pantry #2" },
            { "3_03_00224", "Morio's Home - Chest on Dining Room Pantry" },
        };

        [Description("Morio's Home - Kitchen Cabinets")]
        public static Dictionary<string, string> MoriosHomeKitchenCabinets = new()
        {
            { "3_03_00354", "Morio's Home - Coin on Kitchen Cabinet #1" },
            { "3_03_00353", "Morio's Home - Coin Bag on Kitchen Cabinet #1" },
            { "3_03_00352", "Morio's Home - Coin Bag on Kitchen Cabinet #2" },
            { "3_03_00351", "Morio's Home - Coin on Kitchen Cabinet #2" },
        };

        [Description("Morio's Home - Kitchen High Ground")]
        public static Dictionary<string, string> MoriosHomeKitchenHighGround = new()
        {
            { "3_03_00396", "Morio's Home - Coin on Path Above Kitchen #1" },
            { "3_03_00397", "Morio's Home - Coin Bag on Path Above Kitchen" },
            { "3_03_00398", "Morio's Home - Coin on Path Above Kitchen #2" },
            { "3_03_00399", "Morio's Home - Coin on Path Above Kitchen #3" },
            { "3_03_00444", "Morio's Home - Coin Bag Above Dining Room" },
            { "3_03_00445", "Morio's Home - Coin on Ramp Above Dining Room #1" },
            { "3_03_00450", "Morio's Home - Coin on Ramp Above Dining Room #2" },
            { "3_03_00452", "Morio's Home - Coin on Ramp Above Dining Room #3" },
            { "3_03_00474", "Morio's Home - Coin on Ramp Above Dining Room #4" },
            { "3_03_00477", "Morio's Home - Coin on Ramp Above Dining Room #5" },
            { "3_03_00479", "Morio's Home - Coin on Ramp Above Dining Room #6" },
            { "3_01_00008", "Morio's Home - Gear - Above Kitchen" },
        };

        [Description("Weird Tunnels - Entrance Area")]
        public static Dictionary<string, string> WeirdTunnelsEntrance = new()
        {
            { "3_03_00068", "Weird Tunnels - Coin on Path Before First Boosts #1" },
            { "3_03_00069", "Weird Tunnels - Coin on Path Before First Boosts #2" },
            { "3_03_00070", "Weird Tunnels - Coin on Path Before First Boosts #3" },
            { "3_03_00071", "Weird Tunnels - Coin on Path Before First Boosts #4" },
            { "3_03_00072", "Weird Tunnels - Coin on Path Before First Boosts #5" },
        };

        [Description("Weird Tunnels - Expert 1")]
        public static Dictionary<string, string> WeirdTunnelsExpert1 = new()
        {
            { "3_03_00073", "Weird Tunnels - Coin on Path Before Second Boosts #1" },
            { "3_03_00074", "Weird Tunnels - Coin on Path Before Second Boosts #2" },
            { "3_03_00075", "Weird Tunnels - Coin on Path Before Second Boosts #3" },
            { "3_03_00076", "Weird Tunnels - Coin on Path Before Second Boosts #4" },
            { "3_03_00077", "Weird Tunnels - Coin on Path Before Second Boosts #5" },
        };

        [Description("Weird Tunnels - High Ground")]
        public static Dictionary<string, string> WeirdTunnelsHighGround = new()
        {
            { "3_03_00225", "Weird Tunnels - Coin on Left Upper Path #1" },
            { "3_03_00226", "Weird Tunnels - Coin on Left Upper Path #2" },
            { "3_03_00227", "Weird Tunnels - Coin on Left Upper Path #3" },
            { "3_03_00228", "Weird Tunnels - Coin on Left Upper Path #4" },
            { "3_03_00229", "Weird Tunnels - Coin on Left Upper Path #5" },
            { "3_03_00234","Weird Tunnels - Coin on Right Upper Path #1" },
            { "3_03_00233","Weird Tunnels - Coin on Right Upper Path #2" },
            { "3_03_00232","Weird Tunnels - Coin on Right Upper Path #3" },
            { "3_03_00231","Weird Tunnels - Coin on Right Upper Path #4" },
            { "3_03_00230","Weird Tunnels - Coin on Right Upper Path #5" },
            { "3_03_00235", "Weird Tunnels - Coin Bag on Right Upper Path #1" },
            { "3_03_00238", "Weird Tunnels - Coin on Right Upper Path #6" },
            { "3_03_00237", "Weird Tunnels - Coin on Right Upper Path #7" },
            { "3_03_00236", "Weird Tunnels - Coin on Right Upper Path #8" },
            { "3_03_00241", "Weird Tunnels - Coin on Right Upper Path #9" },
            { "3_03_00240", "Weird Tunnels - Coin on Right Upper Path #10" },
            { "3_03_00239", "Weird Tunnels - Coin on Right Upper Path #11" },
            { "3_03_00244", "Weird Tunnels - Coin on Right Upper Path #12" },
            { "3_01_00006", "Weird Tunnels - Gear - On Upper Path" },
            { "3_03_00242", "Weird Tunnels - Coin on Right Upper Path #13" },
            { "3_03_00247", "Weird Tunnels - Coin on Right Upper Path #14" },
            { "3_03_00246", "Weird Tunnels - Coin on Right Upper Path #15" },
            { "3_03_00245", "Weird Tunnels - Coin on Right Upper Path #16" },
            { "3_03_00250", "Weird Tunnels - Coin on Right Upper Path #17" },
            { "3_03_00249", "Weird Tunnels - Coin on Right Upper Path #18" },
            { "3_03_00248", "Weird Tunnels - Coin on Right Upper Path #19" },
            { "3_03_00251", "Weird Tunnels - Coin Bag on Right Upper Path #2" },
            { "3_03_00022", "Weird Tunnels - Coin Surrounding Lower Gear #1" },
            { "3_03_00025", "Weird Tunnels - Coin Surrounding Lower Gear #2" },
            { "3_03_00024", "Weird Tunnels - Coin Bag Near Lower Gear #1" },
            { "3_03_00023", "Weird Tunnels - Coin Surrounding Lower Gear #3" },
            { "3_03_00028", "Weird Tunnels - Coin Surrounding Lower Gear #4" },
            { "3_03_00027", "Weird Tunnels - Coin Surrounding Lower Gear #5" },
            { "3_03_00026", "Weird Tunnels - Coin Surrounding Lower Gear #6" },
            { "3_03_00031", "Weird Tunnels - Coin Surrounding Lower Gear #7" },
            { "3_01_00005", "Weird Tunnels - Gear - On Lower Path" },
            { "3_03_00029", "Weird Tunnels - Coin Surrounding Lower Gear #8" },
            { "3_03_00034", "Weird Tunnels - Coin Surrounding Lower Gear #9" },
            { "3_03_00033", "Weird Tunnels - Coin Surrounding Lower Gear #10" },
            { "3_03_00032", "Weird Tunnels - Coin Surrounding Lower Gear #11" },
            { "3_03_00037", "Weird Tunnels - Coin Surrounding Lower Gear #12" },
            { "3_03_00036", "Weird Tunnels - Coin Bag Near Lower Gear #2" },
            { "3_03_00035", "Weird Tunnels - Coin Surrounding Lower Gear #13" },
            { "3_03_00038", "Weird Tunnels - Coin Surrounding Lower Gear #14" },
            { "3_03_00039", "Weird Tunnels - Coin Leading Towards Exit #1" },
            { "3_03_00040", "Weird Tunnels - Coin Leading Towards Exit #2" },
            { "3_03_00041", "Weird Tunnels - Coin Leading Towards Exit #3" },
            { "3_03_00042", "Weird Tunnels - Coin Leading Towards Exit #4" },
        };


        [Description("Morio's Island - Special Rules")]
        public static Dictionary<string, string> MoriosIslandSpecialRules = new()
        {
            { "Morio's Home - Coin Bag on Garage Wall Shelf", "J1" },
            { "Morio's Home - Gear - In Hidden Side Room", "X2/J1/B1" },
        };

        #endregion

        #region Bombeach

        [Description("Bombeach - Starting Area")]
        public static Dictionary<string, string> BombeachStartingArea = new()
        {
            { "1_03_00008", "Bombeach - Coin Surrounding Ocean Gear Behind Start #1" },
            { "1_03_00009", "Bombeach - Coin Surrounding Ocean Gear Behind Start #2" },
            { "1_03_00010", "Bombeach - Coin Surrounding Ocean Gear Behind Start #3" },
            { "1_03_00003", "Bombeach - Coin Surrounding Ocean Gear Behind Start #4" },
            { "1_03_00004", "Bombeach - Coin Surrounding Ocean Gear Behind Start #5" },
            { "1_01_00000", "Bombeach - Gear - Ocean Behind Start" },
            { "1_03_00006", "Bombeach - Coin Surrounding Ocean Gear Behind Start #6" },
            { "1_03_00007", "Bombeach - Coin Surrounding Ocean Gear Behind Start #7" },
            { "1_03_00000", "Bombeach - Coin Surrounding Ocean Gear Behind Start #8" },
            { "1_03_00001", "Bombeach - Coin Surrounding Ocean Gear Behind Start #9" },
            { "1_03_00002", "Bombeach - Coin Surrounding Ocean Gear Behind Start #10" },
            { "1_03_00017", "Bombeach - Coin in Ocean Row Left of Start #1" },
            { "1_03_00018", "Bombeach - Coin in Ocean Row Left of Start #2" },
            { "1_03_00019", "Bombeach - Coin in Ocean Row Left of Start #3" },
            { "1_03_00020", "Bombeach - Coin in Ocean Row Left of Start #4" },
            { "1_03_00021", "Bombeach - Coin in Ocean Row Left of Start #5" },
            { "1_03_00025", "Bombeach - Coin in Ocean Ring Left of Start #1" },
            { "1_03_00024", "Bombeach - Coin in Ocean Ring Left of Start #2" },
            { "1_03_00023", "Bombeach - Coin in Ocean Ring Left of Start #3" },
            { "1_03_00028", "Bombeach - Coin in Ocean Ring Left of Start #4" },
            { "1_03_00026", "Bombeach - Coin in Ocean Ring Left of Start #5" },
            { "1_03_00031", "Bombeach - Coin in Ocean Ring Left of Start #6" },
            { "1_03_00030", "Bombeach - Coin in Ocean Ring Left of Start #7" },
            { "1_03_00029", "Bombeach - Coin in Ocean Ring Left of Start #8" },
            { "1_03_00012", "Bombeach - Coin in Ocean Behind Rocks Right of Start #1" },
            { "1_03_00013", "Bombeach - Coin in Ocean Behind Rocks Right of Start #2" },
            { "1_03_00014", "Bombeach - Chest in Ocean Behind Rocks Right of Start" },
            { "1_03_00015", "Bombeach - Coin in Ocean Behind Rocks Right of Start #3" },
            { "1_03_00016", "Bombeach - Coin in Ocean Behind Rocks Right of Start #4" },
            { "1_03_00151", "Bombeach - Coin on Starting Island #1" },
            { "1_03_00152", "Bombeach - Coin on Starting Island #2" },
            { "1_03_00153", "Bombeach - Coin on Starting Island #3" },
            { "1_03_00154", "Bombeach - Coin on Starting Island #4" },
            { "1_03_00155", "Bombeach - Coin on Starting Island #5" },
            { "1_03_00156", "Bombeach - Coin on Starting Island #6" },
            { "1_03_00157", "Bombeach - Coin on Starting Island #7" },
            { "1_03_00158", "Bombeach - Coin on Starting Island #8" },
            { "1_01_00001", "Bombeach - Gear - Escort Gonono" },
            { "1_21_00001", "Bombeach - Cheese Behind First Houses Before Beach Hotel" },
            { "1_03_00179", "Bombeach - Coin on Main Road Before Beach Hotel #1" },
            { "1_03_00180", "Bombeach - Coin on Main Road Before Beach Hotel #2" },
            { "1_03_00181", "Bombeach - Coin on Main Road Before Beach Hotel #3" },
            { "1_03_00182", "Bombeach - Coin on Main Road Before Beach Hotel #4" },
            { "1_03_00183", "Bombeach - Coin on Main Road Before Beach Hotel #5" },
            { "1_03_00185", "Bombeach - Coin on Main Road Before Beach Hotel #6" },
            { "1_03_00186", "Bombeach - Coin on Main Road Before Beach Hotel #7" },
            { "1_03_00187", "Bombeach - Coin on Main Road Before Beach Hotel #8" },
            { "1_03_00188", "Bombeach - Coin on Main Road Before Beach Hotel #9" },
            { "1_03_00190", "Bombeach - Coin on Main Road Before Bombable Wall #1" },
            { "1_03_00189", "Bombeach - Coin on Main Road Before Bombable Wall #2" },
            { "1_03_00192", "Bombeach - Coin on Main Road Before Bombable Wall #3" },
            { "1_03_00193", "Bombeach - Coin on Main Road Before Bombable Wall #4" },
            { "1_03_00194", "Bombeach - Coin on Main Road Before Bombable Wall #5" },
            { "1_03_00195", "Bombeach - Coin on Main Road Before Bombable Wall #6" },
            { "1_03_00196", "Bombeach - Coin on Main Road Before Bombable Wall #7" },
            { "1_03_00197", "Bombeach - Coin on Main Road Before Bombable Wall #8" },
            { "1_03_00198", "Bombeach - Coin on Main Road Before Bombable Wall #9" },
            { "1_03_00199", "Bombeach - Coin on Main Road Before Bombable Wall #10" },
            { "1_03_00200", "Bombeach - Coin on Main Road Before Bombable Wall #11" },
            { "1_03_00204", "Bombeach - Coin on Main Road Before Bombable Wall #12" },
            { "1_03_00205", "Bombeach - Coin on Main Road Before Bombable Wall #13" },
            { "1_03_00206", "Bombeach - Coin on Main Road Before Bombable Wall #14" },
            { "1_03_00208", "Bombeach - Coin on Main Road Before Bombable Wall #15" },
            { "1_03_00210", "Bombeach - Coin on Main Road Before Bombable Wall #16" },
            { "1_03_00212", "Bombeach - Coin on Main Road Before Bombable Wall #17" },
            { "1_03_00214", "Bombeach - Coin on Main Road Before Bombable Wall #18" },
            { "1_21_00005", "Bombeach - Cheese by Main Road Bombable Wall" },
            { "1_03_00323", "Bombeach - Coin on Beach Ramp to Bunny Island #1" },
            { "1_03_00315", "Bombeach - Coin on Beach Ramp to Bunny Island #2" },
            { "1_03_00308", "Bombeach - Coin on Beach Ramp to Bunny Island #3" },
            { "1_03_00162", "Bombeach - Coin in Bomb Block Alcove on First Beach #1" },
            { "1_03_00203", "Bombeach - Coin in Bomb Block Alcove on First Beach #2" },
            { "1_03_00160", "Bombeach - Coin in Bomb Block Alcove on First Beach #3" },
            { "1_03_00201", "Bombeach - Coin in Bomb Block Alcove on First Beach #4" },
            { "1_03_00159", "Bombeach - Coin on Spiral Hill Ramp on First Beach #1" },
            { "1_03_00184", "Bombeach - Coin on Spiral Hill Ramp on First Beach #2" },
            { "1_03_00297", "Bombeach - Coin on Spiral Hill Ramp on First Beach #3" },
            { "1_03_00357", "Bombeach - Coin on Spiral Hill Ramp on First Beach #4" },
            { "1_03_00358", "Bombeach - Coin on Spiral Hill Ramp on First Beach #5" },
            { "1_03_00534", "Bombeach - Coin Surrounding Gear on Spiral Hill #1" },
            { "1_03_00537", "Bombeach - Coin Surrounding Gear on Spiral Hill #2" },
            { "1_03_00533", "Bombeach - Coin Surrounding Gear on Spiral Hill #3" },
            { "1_03_00529", "Bombeach - Coin Surrounding Gear on Spiral Hill #4" },
            { "1_03_00538", "Bombeach - Coin Surrounding Gear on Spiral Hill #5" },
            { "1_03_00536", "Bombeach - Coin Surrounding Gear on Spiral Hill #6" },
            { "1_01_00009", "Bombeach - Gear - Top of Spiral Hill on First Beach" },
            { "1_03_00528", "Bombeach - Coin Surrounding Gear on Spiral Hill #7" },
            { "1_03_00526", "Bombeach - Coin Surrounding Gear on Spiral Hill #8" },
            { "1_03_00535", "Bombeach - Coin Surrounding Gear on Spiral Hill #9" },
            { "1_03_00531", "Bombeach - Coin Surrounding Gear on Spiral Hill #10" },
            { "1_03_00527", "Bombeach - Coin Surrounding Gear on Spiral Hill #11" },
            { "1_03_00530", "Bombeach - Coin Surrounding Gear on Spiral Hill #12" },
            { "1_03_00472", "Bombeach - Coin On Plateau Left of Starting Area #1" },
            { "1_03_00473", "Bombeach - Coin On Plateau Left of Starting Area #2" },
            { "1_03_00474", "Bombeach - Coin On Plateau Left of Starting Area #3" },
            { "1_03_00477", "Bombeach - Coin on Plateau Behind Beach Hotel #1" },
            { "1_03_00476", "Bombeach - Coin on Plateau Behind Beach Hotel #2" },
            { "1_03_00478", "Bombeach - Coin on Plateau Behind Beach Hotel #3" },
            { "1_03_00479", "Bombeach - Coin on Plateau Behind Beach Hotel #4" },
            { "1_03_00480", "Bombeach - Coin on Plateau Behind Beach Hotel #5" },
            { "1_03_00483", "Bombeach - Coin on Plateau Behind Beach Hotel #6" },
            { "1_03_00482", "Bombeach - Coin on Plateau Ramp Behind Beach Hotel #1" },
            { "1_03_00521", "Bombeach - Coin on Plateau Ramp Behind Beach Hotel #2" },
            { "1_03_00522", "Bombeach - Coin on Plateau Ramp Behind Beach Hotel #3" },
            { "1_03_00539", "Bombeach - Coin on Plateau Ramp Behind Beach Hotel #4" },
            { "1_03_00546", "Bombeach - Coin on Plateau Ramp Behind Beach Hotel #5" },
            { "1_03_00545", "Bombeach - Coin on Plateau Ramp Behind Beach Hotel #6" },
            { "1_03_00547", "Bombeach - Coin on Plateau Ramp Behind Beach Hotel #7" },
            { "1_09_01200", "Bombeach - Checkpoint After Bombable Wall" },
            { "1_01_00004", "Bombeach - Gear - In Bombable Beach Alcove" },
            { "1_03_00163", "Bombeach - Coin on Pier #1" },
            { "1_03_00164", "Bombeach - Coin on Pier #2" },
            { "1_03_00165", "Bombeach - Coin on Pier #3" },
            { "1_03_00166", "Bombeach - Coin on Pier #4" },
            { "1_03_00167", "Bombeach - Coin on Pier #5" },
            { "1_01_00011", "Bombeach - Gear - On Pier" },
            { "1_03_00316", "Bombeach - Coin on Ocean Ramp Towards Bunny Island #1" },
            { "1_03_00324", "Bombeach - Coin on Ocean Ramp Towards Bunny Island #2" },
            { "1_03_00328", "Bombeach - Coin on Ocean Ramp Towards Bunny Island #3" },
            { "1_03_00067", "Bombeach - Coin in Ocean Under Bridge Road #1" },
            { "1_03_00069", "Bombeach - Coin in Ocean Under Bridge Road #2" },
            { "1_03_00073", "Bombeach - Coin in Ocean Under Bridge Road #3" },
            { "1_03_00072", "Bombeach - Coin in Ocean Under Bridge Road #4" },
            { "1_03_00071", "Bombeach - Coin in Ocean Under Bridge Road #5" },
            { "1_03_00079", "Bombeach - Coin in Ocean Under Bridge Road #6" },
            { "1_03_00078", "Bombeach - Coin in Ocean Under Bridge Road #7" },
            { "1_01_00012", "Bombeach - Gear - In Ocean Under Bridge Road" },
            { "1_03_00076", "Bombeach - Coin in Ocean Under Bridge Road #8" },
            { "1_03_00075", "Bombeach - Coin in Ocean Under Bridge Road #9" },
            { "1_03_00083", "Bombeach - Coin in Ocean Under Bridge Road #10" },
            { "1_03_00082", "Bombeach - Coin in Ocean Under Bridge Road #11" },
            { "1_03_00081", "Bombeach - Coin in Ocean Under Bridge Road #12" },
            { "1_03_00085", "Bombeach - Coin in Ocean Under Bridge Road #13" },
            { "1_03_00087", "Bombeach - Coin in Ocean Under Bridge Road #14" },
            { "1_03_00226", "Bombeach - Coin on Main Road After Bombable Wall #1" },
            { "1_03_00228", "Bombeach - Coin on Main Road After Bombable Wall #2" },
            { "1_03_00230", "Bombeach - Coin on Main Road After Bombable Wall #3" },
            { "1_03_00231", "Bombeach - Coin on Main Road After Bombable Wall #4" },
            { "1_03_00232", "Bombeach - Coin on Main Road After Bombable Wall #5" },
            { "1_03_00367", "Bombeach - Coin on Main Road by Tosla Billboard #1" },
            { "1_03_00371", "Bombeach - Coin on Main Road by Tosla Billboard #2" },
            { "1_03_00374", "Bombeach - Coin on Main Road by Tosla Billboard #3" },
            { "1_03_00375", "Bombeach - Coin on Main Road by Tosla Billboard #4" },
            { "1_03_00376", "Bombeach - Coin on Main Road by Tosla Billboard #5" },
            { "1_03_00380", "Bombeach - Coin on Main Road by Hot Dog Stand #1" },
            { "1_03_00382", "Bombeach - Coin on Main Road by Hot Dog Stand #2" },
            { "1_03_00384", "Bombeach - Coin on Main Road by Hot Dog Stand #3" },
            { "1_03_00387", "Bombeach - Coin on Main Road by Hot Dog Stand #4" },
            { "1_03_00391", "Bombeach - Coin on Main Road by Hot Dog Stand #5" },
            { "1_03_00386", "Bombeach - Coin in Alley by Hot Dog Stand #1" },
            { "1_03_00389", "Bombeach - Coin in Alley by Hot Dog Stand #2" },
            { "1_03_00394", "Bombeach - Coin in Alley by Hot Dog Stand #3" },
            { "1_03_00390", "Bombeach - Coin in Alley by Hot Dog Stand #4" },
            { "1_03_00395", "Bombeach - Coin in Alley by Hot Dog Stand #5" },
            { "1_03_00396", "Bombeach - Coin Bag in Alley by Hot Dog Stand" },
            { "1_03_00422", "Bombeach - Coin on Road to Ocean by Hot Dog Stand #1" },
            { "1_03_00421", "Bombeach - Coin on Road to Ocean by Hot Dog Stand #2" },
            { "1_03_00420", "Bombeach - Coin on Road to Ocean by Hot Dog Stand #3" },
            { "1_03_00419", "Bombeach - Coin on Road to Ocean by Hot Dog Stand #4" },
            { "1_03_00418", "Bombeach - Coin on Road to Ocean by Hot Dog Stand #5" },
            { "1_03_00416", "Bombeach - Coin on Ocean Bridge Road #1" },
            { "1_03_00415", "Bombeach - Coin on Ocean Bridge Road #2" },
            { "1_03_00414", "Bombeach - Coin on Ocean Bridge Road #3" },
            { "1_03_00413", "Bombeach - Coin on Ocean Bridge Road #4" },
            { "1_03_00412", "Bombeach - Coin on Ocean Bridge Road #5" },
            { "1_03_00411", "Bombeach - Coin on Ocean Bridge Road #6" },
            { "1_03_00410", "Bombeach - Coin on Ocean Bridge Road #7" },
            { "1_03_00409", "Bombeach - Coin on Ocean Bridge Road #8" },
            { "1_03_00408", "Bombeach - Coin on Ocean Bridge Road #9" },
            { "1_03_00407", "Bombeach - Coin on Ocean Bridge Road #10" },
            { "1_03_00496", "Bombeach - Coin on Ocean Bridge House Roof #1" },
            { "1_03_00495", "Bombeach - Coin on Ocean Bridge House Roof #2" },
            { "1_03_00525", "Bombeach - Coin on Ocean Bridge House Roof #3" },
            { "1_03_00544", "Bombeach - Coin on Ocean Bridge House Roof #4" },
            { "1_03_00543", "Bombeach - Chest on Ocean Bridge House Roof" },
            { "1_03_00406", "Bombeach - Coin on End of Ocean Bridge #1" },
            { "1_03_00404", "Bombeach - Coin on End of Ocean Bridge #2" },
            { "1_03_00402", "Bombeach - Coin on End of Ocean Bridge #3" },
            { "1_03_00400", "Bombeach - Coin on End of Ocean Bridge #4" },
            { "1_03_00398", "Bombeach - Coin on End of Ocean Bridge #5" },
            { "1_03_00393", "Bombeach - Coin on End of Ocean Bridge #6" },
            { "1_03_00433", "Bombeach - Coin on Main Road After Hat World #1" },
            { "1_03_00434", "Bombeach - Coin on Main Road After Hat World #2" },
            { "1_03_00435", "Bombeach - Coin on Main Road After Hat World #3" },
            { "1_03_00436", "Bombeach - Coin on Main Road After Hat World #4" },
            { "1_03_00437", "Bombeach - Coin on Main Road After Hat World #5" },
            { "1_03_00438", "Bombeach - Coin on Main Road After Hat World #6" },
            { "1_03_00439", "Bombeach - Coin on Main Road After Hat World #7" },
            { "1_03_00510", "Bombeach - Coin on Main Road Before Mario's Pizza #1" },
            { "1_03_00511", "Bombeach - Coin on Main Road Before Mario's Pizza #2" },
            { "1_03_00512", "Bombeach - Coin on Main Road Before Mario's Pizza #3" },
            { "1_03_00513", "Bombeach - Coin on Main Road Before Mario's Pizza #4" },
            { "1_03_00514", "Bombeach - Coin on Main Road Before Mario's Pizza #5" },
            { "1_03_00515", "Bombeach - Coin Behind Mario's Pizza #1" },
            { "1_03_00516", "Bombeach - Coin Behind Mario's Pizza #2" },
            { "1_03_00517", "Bombeach - Coin Behind Mario's Pizza #3" },
            { "1_03_00518", "Bombeach - Coin Behind Mario's Pizza #4" },
            { "1_03_00519", "Bombeach - Coin Behind Mario's Pizza #5" },
            { "1_21_00004", "Bombeach - Cheese Near Mario's Pizza" },
            { "1_01_00010", "Bombeach - Gear - Inside Bomb Block Cage" },
            { "1_03_00505", "Bombeach - Coin on Main Road After Mario's Pizza #1" },
            { "1_03_00506", "Bombeach - Coin on Main Road After Mario's Pizza #2" },
            { "1_03_00507", "Bombeach - Coin on Main Road After Mario's Pizza #3" },
            { "1_03_00508", "Bombeach - Coin on Main Road After Mario's Pizza #4" },
            { "1_03_00509", "Bombeach - Coin on Main Road After Mario's Pizza #5" },
            { "1_03_00562", "Bombeach - Coin on Main Road Before Mountain Tunnel #1" },
            { "1_03_00561", "Bombeach - Coin on Main Road Before Mountain Tunnel #2" },
            { "1_03_00560", "Bombeach - Coin on Main Road Before Mountain Tunnel #3" },
            { "1_03_00559", "Bombeach - Coin on Main Road Before Mountain Tunnel #4" },
            { "1_03_00558", "Bombeach - Coin on Main Road Before Mountain Tunnel #5" },
            { "1_03_00624", "Bombeach - Coin on Main Road in Mountain Tunnel #1" },
            { "1_03_00623", "Bombeach - Coin on Main Road in Mountain Tunnel #2" },
            { "1_03_00622", "Bombeach - Coin on Main Road in Mountain Tunnel #3" },
            { "1_03_00621", "Bombeach - Coin on Main Road in Mountain Tunnel #4" },
            { "1_03_00620", "Bombeach - Coin on Main Road in Mountain Tunnel #5" },
            { "1_03_00627", "Bombeach - Coin Surrounding Timer in Mountain Tunnel Alcove #1" },
            { "1_03_00626", "Bombeach - Coin Surrounding Timer in Mountain Tunnel Alcove #2" },
            { "1_03_00625", "Bombeach - Coin Surrounding Timer in Mountain Tunnel Alcove #3" },
            { "1_03_00630", "Bombeach - Coin Surrounding Timer in Mountain Tunnel Alcove #4" },
            { "1_03_00628", "Bombeach - Coin Surrounding Timer in Mountain Tunnel Alcove #5" },
            { "1_03_00633", "Bombeach - Coin Surrounding Timer in Mountain Tunnel Alcove #6" },
            { "1_03_00632", "Bombeach - Coin Surrounding Timer in Mountain Tunnel Alcove #7" },
            { "1_03_00631", "Bombeach - Coin Surrounding Timer in Mountain Tunnel Alcove #8" },
            { "1_03_00613", "Bombeach - Coin Surrounding Gear in Mountain Tunnel Alcove #1" },
            { "1_03_00614", "Bombeach - Coin Surrounding Gear in Mountain Tunnel Alcove #2" },
            { "1_03_00615", "Bombeach - Coin Surrounding Gear in Mountain Tunnel Alcove #3" },
            { "1_03_00609", "Bombeach - Coin Surrounding Gear in Mountain Tunnel Alcove #4" },
            { "1_01_00006", "Bombeach - Gear - In Mountain Tunnel Alcove" },
            { "1_03_00611", "Bombeach - Coin Surrounding Gear in Mountain Tunnel Alcove #5" },
            { "1_03_00606", "Bombeach - Coin Surrounding Gear in Mountain Tunnel Alcove #6" },
            { "1_03_00607", "Bombeach - Coin Surrounding Gear in Mountain Tunnel Alcove #7" },
            { "1_03_00608", "Bombeach - Coin Surrounding Gear in Mountain Tunnel Alcove #8" },
            { "1_03_00619", "Bombeach - Coin on Main Road After Mountain Tunnel #1" },
            { "1_03_00618", "Bombeach - Coin on Main Road After Mountain Tunnel #2" },
            { "1_03_00617", "Bombeach - Coin on Main Road After Mountain Tunnel #3" },
            { "1_03_00616", "Bombeach - Coin on Main Road After Mountain Tunnel #4" },
            { "1_03_00612", "Bombeach - Coin on Main Road After Mountain Tunnel #5" },
            { "1_03_00637", "Bombeach - Coin on Main Road Before Mountain Hotel #1" },
            { "1_03_00638", "Bombeach - Coin on Main Road Before Mountain Hotel #2" },
            { "1_03_00639", "Bombeach - Coin on Main Road Before Mountain Hotel #3" },
            { "1_03_00640", "Bombeach - Coin on Main Road Before Mountain Hotel #4" },
            { "1_03_00641", "Bombeach - Coin on Main Road Before Mountain Hotel #5" },
            { "1_21_00002", "Bombeach - Cheese Behind Mountain Hotel" },
            { "1_09_01555", "Bombeach - Checkpoint on Top of Mountain" },
            { "1_03_00661", "Bombeach - Coin on Cliff on Mountain Top #1" },
            { "1_03_00662", "Bombeach - Coin on Cliff on Mountain Top #2" },
            { "1_03_00664", "Bombeach - Coin on Cliff on Mountain Top #3" },
            { "1_03_00666", "Bombeach - Coin on Cliff on Mountain Top #4" },
            { "1_03_00668", "Bombeach - Coin on Cliff on Mountain Top #5" },
            { "1_03_00665", "Bombeach - Coin After Checkpoint on Mountain Top #1" },
            { "1_03_00667", "Bombeach - Coin After Checkpoint on Mountain Top #2" },
            { "1_03_00669", "Bombeach - Coin After Checkpoint on Mountain Top #3" },
            { "1_03_00670", "Bombeach - Coin After Checkpoint on Mountain Top #4" },
            { "1_03_00671", "Bombeach - Coin After Checkpoint on Mountain Top #5" },
            { "1_03_00676", "Bombeach - Coin Before Gela-Toni on Mountain Top #1" },
            { "1_03_00675", "Bombeach - Coin Before Gela-Toni on Mountain Top #2" },
            { "1_03_00674", "Bombeach - Coin Before Gela-Toni on Mountain Top #3" },
            { "1_03_00673", "Bombeach - Coin Before Gela-Toni on Mountain Top #4" },
            { "1_03_00672", "Bombeach - Coin Before Gela-Toni on Mountain Top #5" },
            { "1_03_00033", "Bombeach - Coin in Ocean Near Giant Ramp #1" },
            { "1_03_00034", "Bombeach - Coin in Ocean Near Giant Ramp #2" },
            { "1_03_00035", "Bombeach - Coin in Ocean Near Giant Ramp #3" },
            { "1_03_00036", "Bombeach - Coin in Ocean Near Giant Ramp #4" },
            { "1_03_00037", "Bombeach - Coin in Ocean Near Giant Ramp #5" },
            { "1_03_00038", "Bombeach - Coin in Ocean Near Giant Ramp #6" },
            { "1_03_00039", "Bombeach - Coin in Ocean Near Giant Ramp #7" },
            { "1_03_00040", "Bombeach - Coin in Ocean Near Giant Ramp #8" },
            { "1_03_00041", "Bombeach - Coin in Ocean Near Giant Ramp #9" },
            { "1_03_00042", "Bombeach - Coin in Ocean Near Giant Ramp #10" },
            { "1_03_00043", "Bombeach - Coin in Ocean Near Giant Ramp #11" },
            { "1_03_00044", "Bombeach - Coin in Ocean Near Giant Ramp #12" },
            { "1_03_00045", "Bombeach - Coin in Ocean Near Giant Ramp #13" },
            { "1_03_00046", "Bombeach - Coin in Ocean Near Giant Ramp #14" },
            { "1_03_00047", "Bombeach - Coin in Ocean Near Giant Ramp #15" },
            { "1_03_00057", "Bombeach - Coin in Ocean Ring by Pier #1" },
            { "1_03_00056", "Bombeach - Coin in Ocean Ring by Pier #2" },
            { "1_03_00055", "Bombeach - Coin in Ocean Ring by Pier #3" },
            { "1_03_00061", "Bombeach - Coin in Ocean Ring by Pier #4" },
            { "1_03_00059", "Bombeach - Coin in Ocean Ring by Pier #5" },
            { "1_03_00065", "Bombeach - Coin in Ocean Ring by Pier #6" },
            { "1_03_00064", "Bombeach - Coin in Ocean Ring by Pier #7" },
            { "1_03_00063", "Bombeach - Coin in Ocean Ring by Pier #8" },
            { "1_03_00048", "Bombeach - Coin Behind Ocean Rocks Near Pier #1" },
            { "1_03_00049", "Bombeach - Coin Behind Ocean Rocks Near Pier #2" },
            { "1_03_00050", "Bombeach - Coin Behind Ocean Rocks Near Pier #3" },
            { "1_03_00051", "Bombeach - Chest Behind Ocean Rocks Near Pier" },
            { "1_03_00052", "Bombeach - Coin Behind Ocean Rocks Near Pier #4" },
            { "1_03_00053", "Bombeach - Coin Behind Ocean Rocks Near Pier #5" },
            { "1_03_00054", "Bombeach - Coin Behind Ocean Rocks Near Pier #6" },
            { "1_21_00000", "Bombeach - Cheese in Nook Between Beach and Hat World" },
            { "1_03_00377", "Bombeach - Coin on Sidewalk Ledge by Hat World #1" },
            { "1_03_00378", "Bombeach - Coin on Sidewalk Ledge by Hat World #2" },
            { "1_03_00379", "Bombeach - Coin on Sidewalk Ledge by Hat World #3" },
            { "1_03_00383", "Bombeach - Coin on Sidewalk Ledge by Hat World #4" },
            { "1_03_00385", "Bombeach - Coin on Sidewalk Ledge by Hat World #5" },
            { "1_03_00388", "Bombeach - Coin on Sidewalk Ledge by Hat World #6" },
            { "1_03_00392", "Bombeach - Coin on Sidewalk Ledge by Hat World #7" },
            { "1_03_00397", "Bombeach - Coin on Sidewalk Ledge by Hat World #8" },
            { "1_03_00399", "Bombeach - Coin on Sidewalk Ledge by Hat World #9" },
            { "1_03_00401", "Bombeach - Coin on Sidewalk Ledge by Hat World #10" },
            { "1_03_00403", "Bombeach - Coin on Sidewalk Ledge by Hat World #11" },
            { "1_03_00405", "Bombeach - Coin on Sidewalk Ledge by Hat World #12" },
            { "1_03_00423", "Bombeach - Coin on Sidewalk Ledge by Hat World #13" },
            { "1_03_00424", "Bombeach - Coin on Sidewalk Ledge by Hat World #14" },
            { "1_03_00425", "Bombeach - Coin on Sidewalk Ledge by Hat World #15" },
            { "1_03_00426", "Bombeach - Chest on Sidewalk Ledge by Hat World" },
            { "1_03_00171", "Bombeach - Coin on Gonono's Desert Island #1" },
            { "1_03_00170", "Bombeach - Coin on Gonono's Desert Island #2" },
            { "1_03_00169", "Bombeach - Coin on Gonono's Desert Island #3" },
            { "1_03_00172", "Bombeach - Coin on Gonono's Desert Island #4" },
            { "1_03_00173", "Bombeach - Coin on Gonono's Desert Island #5" },
            { "1_03_00174", "Bombeach - Coin on Gonono's Desert Island #6" },
            { "1_03_00175", "Bombeach - Coin on Gonono's Desert Island #7" },
            { "1_03_00178", "Bombeach - Coin on Gonono's Desert Island #8" },
            { "1_03_00177", "Bombeach - Coin on Gonono's Desert Island #9" },
            { "1_03_00176", "Bombeach - Coin on Gonono's Desert Island #10" },
            { "1_03_00229", "Bombeach - Coin on Grass Next to Fence by Beach #1" },
            { "1_03_00227", "Bombeach - Coin on Grass Next to Fence by Beach #2" },
            { "1_03_00225", "Bombeach - Coin on Grass Next to Fence by Beach #3" },
            { "1_03_00224", "Bombeach - Coin on Grass Next to Fence by Beach #4" },
            { "1_03_00223", "Bombeach - Coin on Grass Next to Fence by Beach #5" },
            { "1_03_00222", "Bombeach - Coin on Grass Next to Fence by Beach #6" },
            { "1_03_00221", "Bombeach - Coin Bag on Grass Next to Fence by Beach" },
            { "1_03_00220", "Bombeach - Coin on Grass Next to Fence by Beach #7" },
            { "1_03_00219", "Bombeach - Coin on Grass Next to Fence by Beach #8" },
            { "1_03_00218", "Bombeach - Coin on Grass Next to Fence by Beach #9" },
            { "1_03_00217", "Bombeach - Coin on Grass Next to Fence by Beach #10" },
            { "1_03_00216", "Bombeach - Coin on Grass Next to Fence by Beach #11" },
            { "1_03_00215", "Bombeach - Coin on Grass Next to Fence by Beach #12" },
            { "1_03_00213", "Bombeach - Coin on Grass Next to Fence by Beach #13" },
            { "1_03_00211", "Bombeach - Coin on Grass Next to Fence by Beach #14" },
            { "1_03_00209", "Bombeach - Coin on Grass Next to Fence by Beach #15" },
            { "1_03_00207", "Bombeach - Coin on Grass Next to Fence by Beach #16" },
            { "1_03_00062", "Bombeach - Coin in Ocean Near Gonono's Desert Island #1" },
            { "1_03_00066", "Bombeach - Coin in Ocean Near Gonono's Desert Island #2" },
            { "1_03_00068", "Bombeach - Coin in Ocean Near Gonono's Desert Island #3" },
            { "1_03_00070", "Bombeach - Coin in Ocean Near Gonono's Desert Island #4" },
            { "1_03_00074", "Bombeach - Coin in Ocean Near Gonono's Desert Island #5" },
            { "1_03_00080", "Bombeach - Coin in Ocean Near Gonono's Desert Island #6" },
            { "1_03_00084", "Bombeach - Coin in Ocean Near Gonono's Desert Island #7" },
            { "1_03_00086", "Bombeach - Coin in Ocean Near Gonono's Desert Island #8" },
            { "1_03_00088", "Bombeach - Coin in Ocean Near Gonono's Desert Island #9" },
            { "1_03_00089", "Bombeach - Coin in Ocean Near Gonono's Desert Island #10" },
            { "1_03_00494", "Bombeach - Coin on Roof Near Hat World #1" },
            { "1_03_00493", "Bombeach - Coin on Roof Near Hat World #2" },
            { "1_03_00524", "Bombeach - Coin on Roof Near Hat World #3" },
            { "1_03_00542", "Bombeach - Coin on Roof Near Hat World #4" },
            { "1_03_00541", "Bombeach - Coin Bag on Roof Near Hat World" },
            { "1_03_00540", "Bombeach - Coin on Roof Near Hat World #5" },
            { "1_03_00523", "Bombeach - Coin on Roof Near Hat World #6" },
            { "1_03_00432", "Bombeach - Coin Behind Houses Across From Hat World #1" },
            { "1_03_00431", "Bombeach - Coin Behind Houses Across From Hat World #2" },
            { "1_03_00430", "Bombeach - Coin Behind Houses Across From Hat World #3" },
            { "1_03_00429", "Bombeach - Coin Behind Houses Across From Hat World #4" },
            { "1_03_00428", "Bombeach - Coin Behind Houses Across From Hat World #5" },
            { "1_03_00427", "Bombeach - Coin Bag Behind Houses Across From Hat World" },
            { "1_21_00006", "Bombeach - Cheese Behind Houses Across From Hat World" },
            { "1_03_00502", "Bombeach - Coin Behind Bomb Block Cage #1" },
            { "1_03_00499", "Bombeach - Coin Behind Bomb Block Cage #2" },
            { "1_03_00503", "Bombeach - Coin Behind Bomb Block Cage #3" },
            { "1_03_00500", "Bombeach - Coin Bag Behind Bomb Block Cage" },
            { "1_03_00497", "Bombeach - Coin Behind Bomb Block Cage #4" },
            { "1_03_00501", "Bombeach - Coin Behind Bomb Block Cage #5" },
            { "1_03_00498", "Bombeach - Coin Behind Bomb Block Cage #6" },
            { "1_21_00003", "Bombeach - Cheese Behind Bomb Block Cage" },
            { "1_03_00107", "Bombeach - Coin in Ocean Ring Near Cave Entrance #1" },
            { "1_03_00110", "Bombeach - Coin in Ocean Ring Near Cave Entrance #2" },
            { "1_03_00113", "Bombeach - Coin in Ocean Ring Near Cave Entrance #3" },
            { "1_03_00108", "Bombeach - Coin in Ocean Ring Near Cave Entrance #4" },
            { "1_03_00114", "Bombeach - Coin in Ocean Ring Near Cave Entrance #5" },
            { "1_03_00109", "Bombeach - Coin in Ocean Ring Near Cave Entrance #6" },
            { "1_03_00112", "Bombeach - Coin in Ocean Ring Near Cave Entrance #7" },
            { "1_03_00115", "Bombeach - Coin in Ocean Ring Near Cave Entrance #8" },
            { "1_03_00090", "Bombeach - Coin in Ocean Path Between Rocks #1" },
            { "1_03_00091", "Bombeach - Coin in Ocean Path Between Rocks #2" },
            { "1_03_00092", "Bombeach - Coin in Ocean Path Between Rocks #3" },
            { "1_03_00094", "Bombeach - Coin in Ocean Path Between Rocks #4" },
            { "1_03_00096", "Bombeach - Coin in Ocean Path Between Rocks #5" },
            { "1_03_00098", "Bombeach - Chest in Ocean Path Between Rocks" },
            { "1_03_00100", "Bombeach - Coin in Ocean Path Between Rocks #6" },
            { "1_03_00102", "Bombeach - Coin in Ocean Path Between Rocks #7" },
            { "1_03_00104", "Bombeach - Coin in Ocean Path Between Rocks #8" },
            { "1_03_00105", "Bombeach - Coin in Ocean Path Between Rocks #9" },
            { "1_03_00106", "Bombeach - Coin in Ocean Path Between Rocks #10" },
            { "1_03_00242", "Bombeach - Coin on Island Near Cave Entrance #1" },
            { "1_03_00243", "Bombeach - Coin on Island Near Cave Entrance #2" },
            { "1_03_00248", "Bombeach - Coin on Island Near Cave Entrance #3" },
            { "1_03_00247", "Bombeach - Coin on Island Near Cave Entrance #4" },
            { "1_03_00245", "Bombeach - Coin on Island Near Cave Entrance #5" },
            { "1_03_00244", "Bombeach - Coin on Island Near Cave Entrance #6" },
            { "1_03_00249", "Bombeach - Coin on Island Near Cave Entrance #7" },
            { "1_03_00250", "Bombeach - Coin on Island Near Cave Entrance #8" },
            { "1_03_00095", "Bombeach - Coin in Ocean Near 3 Times MMA World Champion! #1" },
            { "1_03_00097", "Bombeach - Coin in Ocean Near 3 Times MMA World Champion! #2" },
            { "1_03_00099", "Bombeach - Coin in Ocean Near 3 Times MMA World Champion! #3" },
            { "1_03_00101", "Bombeach - Coin in Ocean Near 3 Times MMA World Champion! #4" },
            { "1_03_00103", "Bombeach - Coin in Ocean Near 3 Times MMA World Champion! #5" },
            { "1_03_00116", "Bombeach - Coin in Ocean Near Dave Diver #1" },
            { "1_03_00117", "Bombeach - Coin in Ocean Near Dave Diver #2" },
            { "1_03_00118", "Bombeach - Coin in Ocean Near Dave Diver #3" },
            { "1_03_00119", "Bombeach - Coin in Ocean Near Dave Diver #4" },
            { "1_03_00120", "Bombeach - Coin in Ocean Near Dave Diver #5" },
            { "1_03_00126", "Bombeach - Coin in Ocean Near Gem-Shaped Islands #1" },
            { "1_03_00127", "Bombeach - Coin in Ocean Near Gem-Shaped Islands #2" },
            { "1_03_00128", "Bombeach - Coin in Ocean Near Gem-Shaped Islands #3" },
            { "1_03_00129", "Bombeach - Coin in Ocean Near Gem-Shaped Islands #4" },
            { "1_03_00130", "Bombeach - Coin in Ocean Near Gem-Shaped Islands #5" },
            { "1_03_00131", "Bombeach - Coin in Ocean Behind Island Near Cave Entrance #1" },
            { "1_03_00132", "Bombeach - Coin in Ocean Behind Island Near Cave Entrance #2" },
            { "1_03_00133", "Bombeach - Coin in Ocean Behind Island Near Cave Entrance #3" },
            { "1_03_00134", "Bombeach - Coin in Ocean Behind Island Near Cave Entrance #4" },
            { "1_03_00135", "Bombeach - Coin in Ocean Behind Island Near Cave Entrance #5" },
            { "1_03_00136", "Bombeach - Coin in Ocean Behind Island Near Cave Entrance #6" },
            { "1_03_00121", "Bombeach - Coin in Ocean Behind Island Near Cave Entrance #7" },
            { "1_03_00122", "Bombeach - Coin in Ocean Behind Island Near Cave Entrance #8" },
            { "1_03_00123", "Bombeach - Coin in Ocean Behind Island Near Cave Entrance #9" },
            { "1_03_00124", "Bombeach - Coin in Ocean Behind Island Near Cave Entrance #10" },
            { "1_03_00125", "Bombeach - Coin in Ocean Behind Island Near Cave Entrance #11" },
            { "1_01_00015", "Bombeach - Gear - Defeat Bomboss" }
        };

        [Description("Bombeach - Easy Bomb Jumps")]
        public static Dictionary<string, string> BombeachEasyBombJumps = new()
        {
            // All near cannons, repeatable bomb sources
            { "1_01_00013", "Bombeach - Gear - Desert Island Pedestal" },
            { "1_03_00322", "Bombeach - Coin on Bunny Island #1" },
            { "1_03_00314", "Bombeach - Coin on Bunny Island #2" },
            { "1_03_00307", "Bombeach - Coin on Bunny Island #3" },
            { "1_03_00301", "Bombeach - Coin on Bunny Island #4" },
            { "1_03_00321", "Bombeach - Coin on Bunny Island #5" },
            { "1_03_00313", "Bombeach - Coin on Bunny Island #6" },
            { "1_03_00306", "Bombeach - Coin on Bunny Island #7" },
            { "1_03_00300", "Bombeach - Coin on Bunny Island #8" },
            { "1_03_00320", "Bombeach - Coin on Bunny Island #9" },
            { "1_03_00312", "Bombeach - Coin on Bunny Island #10" },
            { "1_03_00305", "Bombeach - Coin on Bunny Island #11" },
            { "1_03_00299", "Bombeach - Coin on Bunny Island #12" },
            { "1_03_00327", "Bombeach - Coin on Bunny Island #13" },
            { "1_03_00319", "Bombeach - Coin on Bunny Island #14" },
            { "1_02_00002", "Bombeach - Bunny - On Island" },
            { "1_03_00304", "Bombeach - Coin on Bunny Island #15" },
            { "1_03_00326", "Bombeach - Coin on Bunny Island #16" },
            { "1_03_00318", "Bombeach - Coin on Bunny Island #17" },
            { "1_03_00310", "Bombeach - Coin on Bunny Island #18" },
            { "1_03_00303", "Bombeach - Coin on Bunny Island #19" },
            { "1_03_00325", "Bombeach - Coin on Bunny Island #20" },
            { "1_03_00317", "Bombeach - Coin on Bunny Island #21" },
            { "1_03_00309", "Bombeach - Coin on Bunny Island #22" },
            { "1_03_00302", "Bombeach - Coin on Bunny Island #23" },
            { "1_03_00590", "Bombeach - Chest on Top of Hat World" },
            { "1_03_00490", "Bombeach - Coin on Gem-Shaped Platform Near Bombable Wall #1" },
            { "1_03_00487", "Bombeach - Coin on Gem-Shaped Platform Near Bombable Wall #2" },
            { "1_03_00491", "Bombeach - Coin on Gem-Shaped Platform Near Bombable Wall #3" },
            { "1_01_00005", "Bombeach - Gear - On Gem-Shaped Platform Near Bombable Wall" },
            { "1_03_00485", "Bombeach - Coin on Gem-Shaped Platform Near Bombable Wall #4" },
            { "1_03_00489", "Bombeach - Coin on Gem-Shaped Platform Near Bombable Wall #5" },
            { "1_03_00486", "Bombeach - Coin on Gem-Shaped Platform Near Bombable Wall #6" },
            { "1_01_00002", "Bombeach - Gear - Top of Hill to Right of Bombable Wall" },
            { "1_03_00556", "Bombeach - Coin on Hill to Right of Bombable Wall #1" },
            { "1_03_00555", "Bombeach - Coin on Hill to Right of Bombable Wall #2" },
            { "1_03_00554", "Bombeach - Coin on Hill to Right of Bombable Wall #3" },
            { "1_03_00553", "Bombeach - Coin on Hill to Right of Bombable Wall #4" },
            { "1_03_00552", "Bombeach - Coin on Hill to Right of Bombable Wall #5" },
            { "1_03_00551", "Bombeach - Coin on Hill to Right of Bombable Wall #6" },
            { "1_03_00550", "Bombeach - Coin on Hill to Right of Bombable Wall #7" },
            { "1_03_00549", "Bombeach - Coin on Hill to Right of Bombable Wall #8" },
            { "1_03_00603", "Bombeach - Coin on Ramp on Hill to Right of Bombable Wall #1" },
            { "1_03_00604", "Bombeach - Coin on Ramp on Hill to Right of Bombable Wall #2" },
            { "1_03_00605", "Bombeach - Coin on Ramp on Hill to Right of Bombable Wall #3" },
            { "1_03_00356", "Bombeach - Coin on Slanted Cliff Off Hill to Right of Bombable Wall #1" },
            { "1_03_00355", "Bombeach - Coin on Slanted Cliff Off Hill to Right of Bombable Wall #2" },
            { "1_03_00354", "Bombeach - Coin on Slanted Cliff Off Hill to Right of Bombable Wall #3" },
            { "1_03_00364", "Bombeach - Coin on Slanted Cliff Off Hill to Right of Bombable Wall #4" },
            { "1_03_00362", "Bombeach - Coin on Slanted Cliff Off Hill to Right of Bombable Wall #5" },
            { "1_03_00450", "Bombeach - Coin on Slanted Cliff Off Hill to Right of Bombable Wall #6" },
            { "1_03_00449", "Bombeach - Coin on Slanted Cliff Off Hill to Right of Bombable Wall #7" },
            { "1_03_00448", "Bombeach - Coin on Slanted Cliff Off Hill to Right of Bombable Wall #8" },

            // Not near repeatable bomb sources, but basically right next to bombs
            { "1_03_00295", "Bombeach - Coin on Platform on Right Side of First Beach #1" },
            { "1_03_00296", "Bombeach - Coin on Platform on Right Side of First Beach #2" },
            { "1_03_00353", "Bombeach - Coin on Platform on Right Side of First Beach #3" },
            { "1_03_00359", "Bombeach - Coin on Platform on Right Side of First Beach #4" },
            { "1_03_00360", "Bombeach - Coin on Platform on Right Side of First Beach #5" },
        };

        // These require bomb luring, one-offs, or other outside-the-box thinking
        // Otherwise these *must be* obtainable with either boost or jump
        [Description("Bombeach - Expert 1 Bomb Jumps")]
        public static Dictionary<string, string> BombeachExpert1BombJumps = new()
        {
            { "1_03_00703", "Bombeach - Coin on Pedestal in Bomboss Arena #1" },
            { "1_03_00700", "Bombeach - Coin on Pedestal in Bomboss Arena #2" },
            { "1_03_00702", "Bombeach - Coin on Pedestal in Bomboss Arena #3" },
            { "1_03_00699", "Bombeach - Coin on Pedestal in Bomboss Arena #4" },
            { "1_03_00492", "Bombeach - Coin Bag Above Hot Dog Stand" },
            { "1_03_00601", "Bombeach - Chest by Orange Block Bridge" },
            { "1_03_00594", "Bombeach - Coin on Orange Block Bridge #7" },
            { "1_03_00593", "Bombeach - Coin on Orange Block Bridge #8" },
            { "1_03_00592", "Bombeach - Coin on Orange Block Bridge #9" },
            { "1_03_00365", "Bombeach - Coin on Gem-Shaped Platform Near Tosla Billboard #1" },
            { "1_03_00366", "Bombeach - Coin on Gem-Shaped Platform Near Tosla Billboard #2" },
            { "1_03_00368", "Bombeach - Coin on Gem-Shaped Platform Near Tosla Billboard #3" },
            { "1_03_00370", "Bombeach - Coin on Gem-Shaped Platform Near Tosla Billboard #4" },
            { "1_03_00372", "Bombeach - Coin on Gem-Shaped Platform Near Tosla Billboard #5" },
            { "1_03_00373", "Bombeach - Coin on Gem-Shaped Platform Near Tosla Billboard #6" },
            { "1_03_00330", "Bombeach - Coin on Gem-Shaped Platform Near Morco #1" },
            { "1_03_00331", "Bombeach - Coin on Gem-Shaped Platform Near Morco #2" },
            { "1_03_00335", "Bombeach - Coin on Gem-Shaped Platform Near Morco #3" },
            { "1_03_00337", "Bombeach - Coin on Gem-Shaped Platform Near Morco #4" },
            { "1_03_00341", "Bombeach - Coin on Gem-Shaped Platform Near Morco #5" },
            { "1_03_00342", "Bombeach - Coin on Gem-Shaped Platform Near Morco #6" },
            { "1_03_00647", "Bombeach - Coin in Ring on First High Ocean Pillar #1" },
            { "1_03_00650", "Bombeach - Coin in Ring on First High Ocean Pillar #2" },
            { "1_03_00648", "Bombeach - Coin in Ring on First High Ocean Pillar #3" },
            { "1_03_00652", "Bombeach - Coin in Ring on First High Ocean Pillar #4" },
            { "1_03_00649", "Bombeach - Coin in Ring on First High Ocean Pillar #5" },
            { "1_03_00655", "Bombeach - Coin in Ring on First High Ocean Pillar #6" },
            { "1_03_00651", "Bombeach - Coin in Ring on First High Ocean Pillar #7" },
            { "1_03_00657", "Bombeach - Coin in Ring on First High Ocean Pillar #8" },
            { "1_03_00654", "Bombeach - Coin in Ring on First High Ocean Pillar #9" },
            { "1_03_00658", "Bombeach - Coin in Ring on First High Ocean Pillar #10" },
            { "1_03_00656", "Bombeach - Coin in Ring on First High Ocean Pillar #11" },
            { "1_03_00659", "Bombeach - Coin in Ring on First High Ocean Pillar #12" },
            { "1_03_00466", "Bombeach - Coin on Gem-Shaped Island With Timer #1" },
            { "1_03_00469", "Bombeach - Coin on Gem-Shaped Island With Timer #2" },
            { "1_03_00465", "Bombeach - Coin on Gem-Shaped Island With Timer #3" },
            { "1_03_00471", "Bombeach - Coin on Gem-Shaped Island With Timer #4" },
            { "1_03_00467", "Bombeach - Coin on Gem-Shaped Island With Timer #5" },
            { "1_03_00470", "Bombeach - Coin on Gem-Shaped Island With Timer #6" },
            { "1_03_00447", "Bombeach - Coin on Gem-Shaped Island With Gear #1" },
            { "1_03_00446", "Bombeach - Coin on Gem-Shaped Island With Gear #2" },
            { "1_03_00445", "Bombeach - Coin on Gem-Shaped Island With Gear #3" },
            { "1_01_00007", "Bombeach - Gear - On Gem-Shaped Island" },
            { "1_03_00443", "Bombeach - Coin on Gem-Shaped Island With Gear #4" },
            { "1_03_00442", "Bombeach - Coin on Gem-Shaped Island With Gear #5" },
            { "1_03_00441", "Bombeach - Coin on Gem-Shaped Island With Gear #6" },
            { "1_03_00352", "Bombeach - Coin Bag on Gem-Shaped Island With Chest #1" },
            { "1_03_00351", "Bombeach - Coin on Gem-Shaped Island With Chest #1" },
            { "1_03_00350", "Bombeach - Coin on Gem-Shaped Island With Chest #2" },
            { "1_03_00349", "Bombeach - Chest on Gem-Shaped Island" },
            { "1_03_00348", "Bombeach - Coin on Gem-Shaped Island With Chest #3" },
            { "1_03_00347", "Bombeach - Coin on Gem-Shaped Island With Chest #4" },
            { "1_03_00346", "Bombeach - Coin Bag on Gem-Shaped Island With Chest #2" },
            { "1_03_00345", "Bombeach - Coin in Ring on First Island Near Ocean Bridge #1" },
            { "1_03_00340", "Bombeach - Coin in Ring on First Island Near Ocean Bridge #2" },
            { "1_03_00334", "Bombeach - Coin in Ring on First Island Near Ocean Bridge #3" },
            { "1_03_00344", "Bombeach - Coin in Ring on First Island Near Ocean Bridge #4" },
            { "1_03_00333", "Bombeach - Coin in Ring on First Island Near Ocean Bridge #5" },
            { "1_03_00343", "Bombeach - Coin in Ring on First Island Near Ocean Bridge #6" },
            { "1_03_00338", "Bombeach - Coin in Ring on First Island Near Ocean Bridge #7" },
            { "1_03_00332", "Bombeach - Coin in Ring on First Island Near Ocean Bridge #8" },
            { "1_03_00239", "Bombeach - Coin Near Gear on Island Near Ocean Bridge #1" },
            { "1_03_00241", "Bombeach - Coin Near Gear on Island Near Ocean Bridge #2" },
            { "1_03_00238", "Bombeach - Coin Near Gear on Island Near Ocean Bridge #3" },
            { "1_03_00234", "Bombeach - Coin Near Gear on Island Near Ocean Bridge #4" },
            { "1_03_00240", "Bombeach - Coin Near Gear on Island Near Ocean Bridge #5" },
            { "1_03_00237", "Bombeach - Coin Near Gear on Island Near Ocean Bridge #6" },
            { "1_03_00233", "Bombeach - Coin Near Gear on Island Near Ocean Bridge #7" },
            { "1_03_00236", "Bombeach - Coin Near Gear on Island Near Ocean Bridge #8" },
            { "1_01_00003", "Bombeach - Gear - Island Near Ocean Bridge" },
        };

        [Description("Bombeach - Jump or Boost")]
        public static Dictionary<string, string> BombeachJumpOrBoost = new()
        {
            { "1_02_00001", "Bombeach - Bunny - Under Mountain Hotel" },
            { "1_03_00141", "Bombeach - Coin in Secret Ocean Area Past Gem-Shaped Islands #1" },
            { "1_03_00146", "Bombeach - Coin in Secret Ocean Area Past Gem-Shaped Islands #2" },
            { "1_03_00149", "Bombeach - Coin in Secret Ocean Area Past Gem-Shaped Islands #3" },
            { "1_03_00142", "Bombeach - Coin in Secret Ocean Area Past Gem-Shaped Islands #4" },
            { "1_03_00147", "Bombeach - Coin in Secret Ocean Area Past Gem-Shaped Islands #5" },
            { "1_03_00138", "Bombeach - Coin in Secret Ocean Area Past Gem-Shaped Islands #6" },
            { "1_03_00143", "Bombeach - Coin Bag in Secret Ocean Area Past Gem-Shaped Islands #1" },
            { "1_03_00148", "Bombeach - Coin in Secret Ocean Area Past Gem-Shaped Islands #7" },
            { "1_03_00139", "Bombeach - Coin Bag in Secret Ocean Area Past Gem-Shaped Islands #2" },
            { "1_03_00144", "Bombeach - Coin Bag in Secret Ocean Area Past Gem-Shaped Islands #3" },
            { "1_03_00642", "Bombeach - Coin in Row on Second High Ocean Pillar #1" },
            { "1_03_00643", "Bombeach - Coin in Row on Second High Ocean Pillar #2" },
            { "1_03_00644", "Bombeach - Coin in Row on Second High Ocean Pillar #3" },
            { "1_03_00645", "Bombeach - Coin in Row on Second High Ocean Pillar #4" },
            { "1_03_00646", "Bombeach - Coin in Row on Second High Ocean Pillar #5" },
            { "1_03_00660", "Bombeach - Coin in Row on Second High Ocean Pillar #6" },
            { "1_03_00677", "Bombeach - Coin in Row on Second High Ocean Pillar #7" },
            { "1_03_00681", "Bombeach - Coin in Ring Around High Ocean Pillar Gear #1" },
            { "1_03_00680", "Bombeach - Coin in Ring Around High Ocean Pillar Gear #2" },
            { "1_03_00679", "Bombeach - Coin in Ring Around High Ocean Pillar Gear #3" },
            { "1_03_00683", "Bombeach - Coin in Ring Around High Ocean Pillar Gear #4" },
            { "1_03_00682", "Bombeach - Coin in Ring Around High Ocean Pillar Gear #5" },
            { "1_03_00686", "Bombeach - Coin in Ring Around High Ocean Pillar Gear #6" },
            { "1_01_00008", "Bombeach - Gear - High Ocean Pillar" },
            { "1_03_00684", "Bombeach - Coin in Ring Around High Ocean Pillar Gear #7" },
            { "1_03_00688", "Bombeach - Coin in Ring Around High Ocean Pillar Gear #8" },
            { "1_03_00687", "Bombeach - Coin in Ring Around High Ocean Pillar Gear #9" },
            { "1_03_00691", "Bombeach - Coin in Ring Around High Ocean Pillar Gear #10" },
            { "1_03_00690", "Bombeach - Coin in Ring Around High Ocean Pillar Gear #11" },
            { "1_03_00689", "Bombeach - Coin in Ring Around High Ocean Pillar Gear #12" },
            { "1_03_00567", "Bombeach - Coin on Distant High Island #1" },
            { "1_03_00566", "Bombeach - Coin on Distant High Island #2" },
            { "1_03_00565", "Bombeach - Coin on Distant High Island #3" },
            { "1_03_00570", "Bombeach - Coin on Distant High Island #4" },
            { "1_03_00569", "Bombeach - Coin on Distant High Island #5" },
            { "1_03_00568", "Bombeach - Coin on Distant High Island #6" },
            { "1_03_00573", "Bombeach - Coin on Distant High Island #7" },
            { "1_03_00572", "Bombeach - Coin on Distant High Island #8" },
            { "1_03_00571", "Bombeach - Coin on Distant High Island #9" },
            { "1_03_00580", "Bombeach - Coin on Distant High Island #10" },
            { "1_03_00579", "Bombeach - Coin on Distant High Island #11" },
            { "1_03_00578", "Bombeach - Coin on Distant High Island #12" },
            { "1_03_00576", "Bombeach - Coin on Distant High Island #13" },
            { "1_03_00575", "Bombeach - Coin on Distant High Island #14" },
            { "1_03_00574", "Bombeach - Coin on Distant High Island #15" },
            { "1_03_00583", "Bombeach - Coin on Distant High Island #16" },
            { "1_03_00582", "Bombeach - Coin on Distant High Island #17" },
            { "1_03_00581", "Bombeach - Coin on Distant High Island #18" },
            { "1_03_00586", "Bombeach - Coin on Distant High Island #19" },
            { "1_03_00585", "Bombeach - Coin on Distant High Island #20" },
            { "1_03_00584", "Bombeach - Coin on Distant High Island #21" },
            { "1_03_00589", "Bombeach - Coin on Distant High Island #22" },
            { "1_03_00588", "Bombeach - Coin on Distant High Island #23" },
            { "1_03_00587", "Bombeach - Coin on Distant High Island #24" },
        };

        [Description("Bombeach - Orange Block Bridge")]
        public static Dictionary<string, string> BombeachOrangeBlockBridge = new()
        {
            { "1_03_00600", "Bombeach - Coin on Orange Block Bridge #1" },
            { "1_03_00599", "Bombeach - Coin on Orange Block Bridge #2" },
            { "1_03_00598", "Bombeach - Coin on Orange Block Bridge #3" },
            { "1_03_00597", "Bombeach - Coin on Orange Block Bridge #4" },
            { "1_03_00596", "Bombeach - Coin on Orange Block Bridge #5" },
            { "1_03_00595", "Bombeach - Coin on Orange Block Bridge #6" },
        };

        [Description("Bombeach - Superboost Only")]
        public static Dictionary<string, string> BombeachSuperboost = new()
        {
            { "1_03_00695", "Bombeach - Coin Bag on Floating Orange Blocks #1" },
            { "1_03_00697", "Bombeach - Coin on Floating Orange Blocks #1" },
            { "1_03_00696", "Bombeach - Coin on Floating Orange Blocks #2" },
            { "1_03_00698", "Bombeach - Coin Bag on Floating Orange Blocks #2" },
            { "1_02_00000", "Bombeach - Bunny - On Ocean Pillar" },
        };

        [Description("Cave")]
        public static Dictionary<string, string> Cave = new()
        {
            { "1_09_00790", "Cave - Checkpoint" },
            { "1_03_00278", "Cave - Coin on First Bomb Block Bridge #1" },
            { "1_03_00277", "Cave - Coin on First Bomb Block Bridge #2" },
            { "1_03_00276", "Cave - Coin on First Bomb Block Bridge #3" },
            { "1_03_00275", "Cave - Coin on First Bomb Block Bridge #4" },
            { "1_03_00274", "Cave - Coin on First Bomb Block Bridge #5" },
            { "1_03_00272", "Cave - Coin After First Bomb Block Bridge #1" },
            { "1_03_00271", "Cave - Coin After First Bomb Block Bridge #2" },
            { "1_03_00270", "Cave - Coin After First Bomb Block Bridge #3" },
            { "1_03_00269", "Cave - Coin After First Bomb Block Bridge #4" },
            { "1_03_00268", "Cave - Coin After First Bomb Block Bridge #5" },
            { "1_03_00280", "Cave - Coin at Fork in Path #1" },
            { "1_03_00279", "Cave - Coin at Fork in Path #2" },
            { "1_03_00267", "Cave - Coin at Fork in Path #3" },
            { "1_03_00266", "Cave - Coin at Fork in Path #4" },
            { "1_03_00265", "Cave - Coin at Fork in Path #5" },
            { "1_03_00291", "Cave - Coin on Left Pathway #1" },
            { "1_03_00290", "Cave - Coin on Left Pathway #2" },
            { "1_03_00294", "Cave - Coin on Left Pathway #3" },
            { "1_03_00289", "Cave - Coin on Left Pathway #4" },
            { "1_03_00286", "Cave - Coin on Left Pathway #5" },
            { "1_03_00293", "Cave - Coin on Left Pathway #6" },
            { "1_03_00285", "Cave - Coin on Left Pathway #7" },
            { "1_03_00283", "Cave - Coin on Left Pathway #8" },
            { "1_03_00282", "Cave - Coin on Left Pathway #9" },
            { "1_03_00292", "Cave - Coin on Left Pathway #10" },
            { "1_03_00287", "Cave - Coin on Left Pathway #11" },
            { "1_03_00284", "Cave - Coin on Left Pathway #12" },
            { "1_03_00258", "Cave - Coin on Right Pathway #1" },
            { "1_03_00257", "Cave - Coin on Right Pathway #2" },
            { "1_03_00261", "Cave - Coin on Right Pathway #3" },
            { "1_03_00256", "Cave - Coin on Right Pathway #4" },
            { "1_03_00253", "Cave - Coin on Right Pathway #5" },
            { "1_03_00263", "Cave - Coin on Right Pathway #6" },
            { "1_03_00262", "Cave - Coin on Right Pathway #7" },
            { "1_03_00260", "Cave - Coin on Right Pathway #8" },
            { "1_03_00252", "Cave - Coin on Right Pathway #9" },
            { "1_03_00259", "Cave - Coin on Right Pathway #10" },
            { "1_03_00254", "Cave - Coin on Right Pathway #11" },
            { "1_03_00251", "Cave - Coin on Right Pathway #12" },
            { "1_03_00464", "Cave - Coin on Final Island #1" },
            { "1_03_00460", "Cave - Coin on Final Island #2" },
            { "1_03_00454", "Cave - Coin on Final Island #3" },
            { "1_03_00463", "Cave - Coin on Final Island #4" },
            { "1_03_00459", "Cave - Coin on Final Island #5" },
            { "1_03_00453", "Cave - Coin on Final Island #6" },
            { "1_03_00462", "Cave - Coin on Final Island #7" },
            { "1_03_00457", "Cave - Coin on Final Island #8" },
            { "1_03_00452", "Cave - Coin on Final Island #9" },
            { "1_03_00461", "Cave - Coin on Final Island #10" },
            { "1_03_00456", "Cave - Coin on Final Island #11" },
            { "1_03_00451", "Cave - Coin on Final Island #12" },
            { "1_01_00014", "Cave - Gear" },
        };

        #endregion

        #region Arcade Panik

            [Description("Arcade Plaza - Starting Area")]
        public static Dictionary<string, string> ArcadePlazaStartingArea = new()
        {
            { string.Empty, nameof(HubHatWorld) + " - !PLACEHOLDER!" },
        };

        [Description("Arcade Plaza - Outskirts")]
        public static Dictionary<string, string> ArcadePlazaOutskirts = new()
        {
            { "4_03_00157", "Arcade Plaza - Coin on Arcade Roof #1" },
            { "4_03_00152", "Arcade Plaza - Coin on Arcade Roof #2" },
            { "4_03_00148", "Arcade Plaza - Coin on Arcade Roof #3" },
            { "4_03_00158", "Arcade Plaza - Coin on Arcade Roof #4" },
            { "4_03_00153", "Arcade Plaza - Coin on Arcade Roof #5" },
            { "4_03_00149", "Arcade Plaza - Coin on Arcade Roof #6" },
            { "4_03_00154", "Arcade Plaza - Chest on Arcade Roof" },
            { "4_03_00159", "Arcade Plaza - Coin on Arcade Roof #7" },
            { "4_03_00155", "Arcade Plaza - Coin on Arcade Roof #8" },
            { "4_03_00150", "Arcade Plaza - Coin on Arcade Roof #9" },
            { "4_03_00160", "Arcade Plaza - Coin on Arcade Roof #10" },
            { "4_03_00156", "Arcade Plaza - Coin on Arcade Roof #11" },
            { "4_03_00151", "Arcade Plaza - Coin on Arcade Roof #12" },
            { "4_03_00011", "Arcade Plaza - Coin on Track Ramp Left #1" },
            { "4_03_00080", "Arcade Plaza - Coin on Track Ramp Left #2" },
            { "4_03_00082", "Arcade Plaza - Coin on Track Ramp Left #3" },
            { "4_03_00088", "Arcade Plaza - Coin on Track Ramp Left #4" },
            { "4_03_00090", "Arcade Plaza - Coin on Track Ramp Left #5" },
            { "4_03_00042", "Arcade Plaza - Coin on Inner Track Ring Left #1" },
            { "4_03_00038", "Arcade Plaza - Coin on Inner Track Ring Left #2" },
            { "4_03_00034", "Arcade Plaza - Coin on Inner Track Ring Left #3" },
            { "4_03_00030", "Arcade Plaza - Coin on Inner Track Ring Left #4" },
            { "4_03_00026", "Arcade Plaza - Coin on Inner Track Ring Left #5" },
            { "4_03_00022", "Arcade Plaza - Coin on Inner Track Ring Left #6" },
            { "4_03_00018", "Arcade Plaza - Coin on Inner Track Ring Left #7" },
            { "4_03_00014", "Arcade Plaza - Coin Bag on Inner Track Ring Left"},
            { "4_03_00041", "Arcade Plaza - Coin Bag on Outer Track Ring Left"},
            { "4_03_00037", "Arcade Plaza - Coin on Outer Track Ring Left #1" },
            { "4_03_00033", "Arcade Plaza - Coin on Outer Track Ring Left #2" },
            { "4_03_00029", "Arcade Plaza - Coin on Outer Track Ring Left #3" },
            { "4_03_00025", "Arcade Plaza - Coin on Outer Track Ring Left #4" },
            { "4_03_00021", "Arcade Plaza - Coin on Outer Track Ring Left #5" },
            { "4_03_00017", "Arcade Plaza - Coin on Outer Track Ring Left #6" },
            { "4_03_00013", "Arcade Plaza - Coin on Outer Track Ring Left #7" },
            { "4_03_00045", "Arcade Plaza - Coin Bag on Inner Track Ring Back #1" },
            { "4_03_00046", "Arcade Plaza - Coin on Inner Track Ring Back #1" },
            { "4_03_00047", "Arcade Plaza - Coin on Inner Track Ring Back #2" },
            { "4_03_00048", "Arcade Plaza - Coin on Inner Track Ring Back #3" },
            { "4_03_00049", "Arcade Plaza - Coin on Inner Track Ring Back #4" },
            { "4_03_00050", "Arcade Plaza - Coin on Inner Track Ring Back #5" },
            { "4_03_00051", "Arcade Plaza - Coin Bag on Inner Track Ring Back #2" },
            { "4_03_00052", "Arcade Plaza - Coin on Inner Track Ring Back #6" },
            { "4_03_00053", "Arcade Plaza - Coin on Inner Track Ring Back #7" },
            { "4_03_00054", "Arcade Plaza - Coin on Inner Track Ring Back #8" },
            { "4_03_00055", "Arcade Plaza - Coin on Inner Track Ring Back #9" },
            { "4_03_00056", "Arcade Plaza - Coin on Inner Track Ring Back #10" },
            { "4_03_00057", "Arcade Plaza - Coin Bag on Inner Track Ring Back #3" },
            { "4_03_00058", "Arcade Plaza - Coin Bag on Outer Track Ring Back #1" },
            { "4_03_00059", "Arcade Plaza - Coin on Outer Track Ring Back #1" },
            { "4_03_00060", "Arcade Plaza - Coin on Outer Track Ring Back #2" },
            { "4_03_00061", "Arcade Plaza - Coin on Outer Track Ring Back #3" },
            { "4_03_00062", "Arcade Plaza - Coin on Outer Track Ring Back #4" },
            { "4_03_00063", "Arcade Plaza - Coin on Outer Track Ring Back #5" },
            { "4_03_00064", "Arcade Plaza - Coin Bag on Outer Track Ring Back #2" },
            { "4_03_00065", "Arcade Plaza - Coin on Outer Track Ring Back #6" },
            { "4_03_00066", "Arcade Plaza - Coin on Outer Track Ring Back #7" },
            { "4_03_00067", "Arcade Plaza - Coin on Outer Track Ring Back #8" },
            { "4_03_00068", "Arcade Plaza - Coin on Outer Track Ring Back #9" },
            { "4_03_00069", "Arcade Plaza - Coin on Outer Track Ring Back #10" },
            { "4_03_00070", "Arcade Plaza - Coin Bag on Outer Track Ring Back #3" },
            { "4_03_00012", "Arcade Plaza - Coin on Track Ramp Right #1" },
            { "4_03_00081", "Arcade Plaza - Coin on Track Ramp Right #2" },
            { "4_03_00083", "Arcade Plaza - Coin on Track Ramp Right #3" },
            { "4_03_00089", "Arcade Plaza - Coin on Track Ramp Right #4" },
            { "4_03_00091", "Arcade Plaza - Coin on Track Ramp Right #5" },
            { "4_03_00043", "Arcade Plaza - Coin on Inner Track Ring Right #1" },
            { "4_03_00039", "Arcade Plaza - Coin on Inner Track Ring Right #2" },
            { "4_03_00035", "Arcade Plaza - Coin on Inner Track Ring Right #3" },
            { "4_03_00031", "Arcade Plaza - Coin on Inner Track Ring Right #4" },
            { "4_03_00027", "Arcade Plaza - Coin on Inner Track Ring Right #5" },
            { "4_03_00023", "Arcade Plaza - Coin on Inner Track Ring Right #6" },
            { "4_03_00019", "Arcade Plaza - Coin on Inner Track Ring Right #7" },
            { "4_03_00015", "Arcade Plaza - Coin Bag on Inner Track Ring Right" },
            { "4_03_00044", "Arcade Plaza - Coin Bag on Outer Track Ring Right" },
            { "4_03_00040", "Arcade Plaza - Coin on Outer Track Ring Right #1" },
            { "4_03_00036", "Arcade Plaza - Coin on Outer Track Ring Right #2" },
            { "4_03_00032", "Arcade Plaza - Coin on Outer Track Ring Right #3" },
            { "4_03_00028", "Arcade Plaza - Coin on Outer Track Ring Right #4" },
            { "4_03_00024", "Arcade Plaza - Coin on Outer Track Ring Right #5" },
            { "4_03_00020", "Arcade Plaza - Coin on Outer Track Ring Right #6" },
            { "4_03_00016", "Arcade Plaza - Coin on Outer Track Ring Right #7" },
        };

        [Description("Arcade Panik Hat World")]
        public static Dictionary<string, string> ArcadePanikHatWorld = new()
        {
            { string.Empty, nameof(ArcadePanikHatWorld) + " - !PLACEHOLDER!" },
        };

        [Description("Arcade Panik - Starting Area")]
        public static Dictionary<string, string> ArcadePanikStartingArea = new()
        {
            { "4_03_00101", "Arcade Panik - Coin by Big Bowling #1" },
            { "4_03_00102", "Arcade Panik - Coin by Big Bowling #2" },
            { "4_03_00103", "Arcade Panik - Coin by Big Bowling #3" },
            { "4_03_00104", "Arcade Panik - Coin by Big Bowling #4" },
            { "4_03_00105", "Arcade Panik - Coin by Big Bowling #5" },
            { "4_03_00092", "Arcade Panik - Coin by Rental Shoes #1" },
            { "4_03_00093", "Arcade Panik - Coin by Rental Shoes #2" },
            { "4_03_00094", "Arcade Panik - Coin by Rental Shoes #3" },
            { "4_03_00095", "Arcade Panik - Coin by Rental Shoes #4" },
            { "4_03_00096", "Arcade Panik - Coin by Rental Shoes #5" },
            { "4_03_00097", "Arcade Panik - Coin by Rental Shoes #6" },
            { "4_03_00098", "Arcade Panik - Coin by Rental Shoes #7" },
            { "4_03_00099", "Arcade Panik - Coin by Rental Shoes #8" },
            { "4_03_00100", "Arcade Panik - Coin by Rental Shoes #9" },
            { "4_03_00106", "Arcade Panik - Coin in Big Bowling Center Lane #1" },
            { "4_03_00107", "Arcade Panik - Coin in Big Bowling Center Lane #2" },
            { "4_03_00108", "Arcade Panik - Coin in Big Bowling Center Lane #3" },
            { "4_03_00109", "Arcade Panik - Coin in Big Bowling Center Lane #4" },
            { "4_03_00110", "Arcade Panik - Coin in Big Bowling Center Lane #5" },
            { "4_01_00001", "Arcade Panik - Gear - Big Bowling Center Lane Strike" },
            { "4_03_00112", "Arcade Panik - Coin on Left Side of Big Bowling Lanes #1" },
            { "4_03_00114", "Arcade Panik - Coin on Left Side of Big Bowling Lanes #2" },
            { "4_03_00116", "Arcade Panik - Coin on Left Side of Big Bowling Lanes #3" },
            { "4_03_00118", "Arcade Panik - Coin on Left Side of Big Bowling Lanes #4" },
            { "4_03_00120", "Arcade Panik - Coin on Left Side of Big Bowling Lanes #5" },
            { "4_03_00111", "Arcade Panik - Coin on Right Side of Big Bowling Lanes #1" },
            { "4_03_00113", "Arcade Panik - Coin on Right Side of Big Bowling Lanes #2" },
            { "4_03_00115", "Arcade Panik - Coin on Right Side of Big Bowling Lanes #3" },
            { "4_03_00117", "Arcade Panik - Coin on Right Side of Big Bowling Lanes #4" },
            { "4_03_00119", "Arcade Panik - Coin on Right Side of Big Bowling Lanes #5" },
            { "4_03_00077", "Arcade Panik - Coin Behind Big Bowling Lanes #1" },
            { "4_03_00076", "Arcade Panik - Coin Behind Big Bowling Lanes #2" },
            { "4_03_00075", "Arcade Panik - Coin Behind Big Bowling Lanes #3" },
            { "4_03_00074", "Arcade Panik - Coin Behind Big Bowling Lanes #4" },
            { "4_03_00073", "Arcade Panik - Coin Behind Big Bowling Lanes #5" },
            { "4_03_00072", "Arcade Panik - Coin Behind Big Bowling Lanes #6" },
            { "4_03_00071", "Arcade Panik - Coin Behind Big Bowling Lanes #7" },
            { "4_03_00010", "Arcade Panik - Coin on Ramp Behind Big Bowling #1" },
            { "4_03_00078", "Arcade Panik - Coin on Ramp Behind Big Bowling #2" },
            { "4_03_00084", "Arcade Panik - Coin on Ramp Behind Big Bowling #3" },
            { "4_03_00125", "Arcade Panik - Coin on Ramp Behind Big Bowling #4" },
            { "4_03_00132", "Arcade Panik - Coin on Ramp Behind Big Bowling #5" },
            { "4_03_00133", "Arcade Panik - Coin on Ramp Behind Big Bowling #6" },
            { "4_03_00134", "Arcade Panik - Coin on Ramp Behind Big Bowling #7" },
            { "4_03_00135", "Arcade Panik - Coin on Ramp Behind Big Bowling #8" },
            { "4_03_00130", "Arcade Panik - Coin on Ramp Behind Big Bowling #9" },
            { "4_01_00021", "Arcade Panik - Gear - Under Purple Bridge to Big Bowling" },
            { "4_09_00065", "Arcade Panik - Checkpoint Near Big Bowling" },
            { "4_01_00012", "Arcade Panik - Gear - Above Center Island" },
            { "4_03_00006", "Arcade Panik - Chest on Big Bowling Pipe" },
            { "4_03_00007", "Arcade Panik - Chest on Arcade Zone Pipe" },
            { "4_03_00008", "Arcade Panik - Chest on Go Karts Pipes #1" },
            { "4_03_00079", "Arcade Panik - Chest on Go Karts Pipes #2" },
            { "4_03_00131", "Arcade Panik - Chest on Go Karts Pipes #3" },
            { "4_01_00022", "Arcade Panik - Gear - Crazy Ballz Short Strike" },
            { "4_03_00279", "Arcade Panik - Coin on Switch Overlooking Crazy Ballz Long Strike #1" },
            { "4_03_00287", "Arcade Panik - Coin on Switch Overlooking Crazy Ballz Long Strike #2" },
            { "4_03_00290", "Arcade Panik - Coin Bag on Switch Overlooking Crazy Ballz Long Strike #1" },
            { "4_03_00292", "Arcade Panik - Coin Bag on Switch Overlooking Crazy Ballz Long Strike #2" },
            { "4_01_00014", "Arcade Panik - Gear - Crazy Ballz Long Strike" },
            { "4_03_00141", "Arcade Panik - Coin on Crazy Ballz Path #1" },
            { "4_03_00142", "Arcade Panik - Coin on Crazy Ballz Path #2" },
            { "4_03_00143", "Arcade Panik - Coin on Crazy Ballz Path #3" },
            { "4_03_00144", "Arcade Panik - Coin on Crazy Ballz Path #4" },
            { "4_03_00145", "Arcade Panik - Coin on Crazy Ballz Path #5" },
            { "4_03_00146", "Arcade Panik - Coin on Crazy Ballz Path #6" },
            { "4_03_00147", "Arcade Panik - Coin on Crazy Ballz Path #7" },
            { "4_03_00280", "Arcade Panik - Coin on Crazy Ballz Final Switch #1" },
            { "4_03_00288", "Arcade Panik - Coin on Crazy Ballz Final Switch #2" },
            { "4_03_00291", "Arcade Panik - Coin on Crazy Ballz Final Switch #3" },
            { "4_03_00293", "Arcade Panik - Coin on Crazy Ballz Final Switch #4" },
            { "4_03_00294", "Arcade Panik - Coin on Crazy Ballz Final Switch #5" },
            { "4_01_00015", "Arcade Panik - Gear - Crazy Ballz Left Prize" },
            { "4_01_00019", "Arcade Panik - Gear - Crazy Ballz Middle Prize" },
            { "4_01_00004", "Arcade Panik - Gear - Crazy Ballz Right Prize" },
            { "4_03_00178", "Arcade Panik - Coin Near Go Karts Entrance #1" },
            { "4_03_00177", "Arcade Panik - Coin Near Go Karts Entrance #2" },
            { "4_03_00176", "Arcade Panik - Coin Near Go Karts Entrance #3" },
            { "4_03_00175", "Arcade Panik - Coin Near Go Karts Entrance #4" },
            { "4_03_00174", "Arcade Panik - Coin Near Go Karts Entrance #5" },
            { "4_03_00173", "Arcade Panik - Coin on Go Karts Path #1" },
            { "4_03_00172", "Arcade Panik - Coin on Go Karts Path #2" },
            { "4_03_00171", "Arcade Panik - Coin on Go Karts Path #3" },
            { "4_03_00170", "Arcade Panik - Coin on Go Karts Path #4" },
            { "4_03_00169", "Arcade Panik - Coin on Go Karts Path #5" },
            { "4_03_00164", "Arcade Panik - Coin on Go Karts Ramp to Switch #1" },
            { "4_03_00165", "Arcade Panik - Coin on Go Karts Ramp to Switch #2" },
            { "4_03_00166", "Arcade Panik - Coin on Go Karts Ramp to Switch #3" },
            { "4_03_00186", "Arcade Panik - Coin on Go Karts Ramp to Switch #4" },
            { "4_03_00187", "Arcade Panik - Coin on Go Karts Ramp to Switch #5" },
            { "4_03_00190", "Arcade Panik - Coin on Go Karts Ramp to Switch #6" },
            { "4_03_00192", "Arcade Panik - Coin on Go Karts Ramp to Switch #7" },
            { "4_03_00191", "Arcade Panik - Coin on Go Karts Switch #1" },
            { "4_03_00201", "Arcade Panik - Coin on Go Karts Switch #2" },
            { "4_03_00204", "Arcade Panik - Coin on Go Karts Switch #3" },
            { "4_03_00247", "Arcade Panik - Coin on Go Karts Switch #4" },
            { "4_03_00260", "Arcade Panik - Coin on Go Karts Switch #5" },
            { "4_01_00020", "Arcade Panik - Gear - Go Karts Strike" },
            { "4_03_00140", "Arcade Panik - Coin on Go Karts Curved Ramp #1" },
            { "4_03_00139", "Arcade Panik - Coin on Go Karts Curved Ramp #2" },
            { "4_03_00138", "Arcade Panik - Coin on Go Karts Curved Ramp #3" },
            { "4_03_00137", "Arcade Panik - Coin on Go Karts Curved Ramp #4" },
            { "4_03_00136", "Arcade Panik - Coin on Go Karts Curved Ramp #5" },
            { "4_09_00415", "Arcade Panik - Checkpoint by Go Karts" },
            { "4_03_00220", "Arcade Panik - Coin Before Go Karts Obstacle Course #1" },
            { "4_03_00221", "Arcade Panik - Coin Before Go Karts Obstacle Course #2" },
            { "4_03_00222", "Arcade Panik - Coin Before Go Karts Obstacle Course #3" },
            { "4_03_00223", "Arcade Panik - Coin Before Go Karts Obstacle Course #4" },
            { "4_03_00224", "Arcade Panik - Coin Before Go Karts Obstacle Course #5" },
            { "4_01_00008", "Arcade Panik - Gear - Go Karts Obstacle Course" },
            { "4_03_00179", "Arcade Panik - Coin on Path to Arcade Zone #1" },
            { "4_03_00180", "Arcade Panik - Coin on Path to Arcade Zone #2" },
            { "4_03_00181", "Arcade Panik - Coin on Path to Arcade Zone #3" },
            { "4_03_00182", "Arcade Panik - Coin on Path to Arcade Zone #4" },
            { "4_03_00183", "Arcade Panik - Coin on Path to Arcade Zone #5" },
            { "4_01_00018", "Arcade Panik - Gear - Left Switch Path" },
            { "4_01_00013", "Arcade Panik - Gear - Right Switch Path" },
            //{ "4_20_99999", "Arcade Panik - Psycho Taxi Cartridge" }, // Irrelevant to this, placed by special handling
        };

        [Description("Arcade Panik - Expert 1 or Jump")]
        public static Dictionary<string, string> ArcadePanikExpert1Jump = new()
        {
            { "4_03_00005", "Arcade Panik - Chest on Crazy Ballz Pipes #1" },
            { "4_03_00009", "Arcade Panik - Chest on Crazy Ballz Pipes #2" },
            { "4_03_00167", "Arcade Panik - Chest Behind Arcade Zone Entrance Left Arcade Machine" },
            { "4_03_00185", "Arcade Panik - Chest Behind Arcade Zone Entrance Right Arcade Machine" },
        };

        public static Dictionary<string, string> ArcadePanikSpecialRules = new()
        {

        };

        #endregion

        #region Gym Gears

        [Description("Gym Gears - Starting Area")]
        public static Dictionary<string, string> GymGearsStartingArea = new()
        {
            { "6_03_00000", "Gym Gears - Coin in Entrance Hallway #1" },
            { "6_03_00001", "Gym Gears - Coin in Entrance Hallway #2" },
            { "6_03_00002", "Gym Gears - Coin in Entrance Hallway #3" },
            { "6_03_00003", "Gym Gears - Coin in Entrance Hallway #4" },
            { "6_03_00004", "Gym Gears - Coin in Entrance Hallway #5" },
            { "6_03_00006", "Gym Gears - Coin in Entrance Hallway #6" },
            { "6_03_00005", "Gym Gears - Coin Inside Entrance Pillar" },
            { "6_03_00012", "Gym Gears - Coin Between Entrance Pillars #1" },
            { "6_03_00016", "Gym Gears - Coin Bag Between Entrance Pillars" },
            { "6_03_00017", "Gym Gears - Coin Between Entrance Pillars #2" },
            { "6_03_00015", "Gym Gears - Coin in Entrance Weightlifting Area #1" },
            { "6_03_00011", "Gym Gears - Coin in Entrance Weightlifting Area #2" },
            { "6_03_00014", "Gym Gears - Coin in Entrance Weightlifting Area #3" },
            { "6_03_00010", "Gym Gears - Coin in Entrance Weightlifting Area #4" },
            { "6_03_00013", "Gym Gears - Coin in Entrance Weightlifting Area #5" },
            { "6_03_00009", "Gym Gears - Coin in Entrance Weightlifting Area #6" },
            { "6_03_00008", "Gym Gears - Coin in Entrance Weightlifting Area #7" },
            { "6_03_00007", "Gym Gears - Coin in Entrance Weightlifting Area #8" },
            { "6_21_00002", "Gym Gears - Cheese in Entrance Weightlifting Area" },
            { "6_03_00018", "Gym Gears - Coin Inside Entrance Weightlifting Area Exit Hallway Pillar #1" },
            { "6_03_00026", "Gym Gears - Coin Inside Entrance Weightlifting Area Exit Hallway Pillar #2" },
            { "6_03_00019", "Gym Gears - Coin Between Entrance Weightlifting Area Exit Hallway Pillars #1" },
            { "6_03_00020", "Gym Gears - Coin Between Entrance Weightlifting Area Exit Hallway Pillars #2" },
            { "6_03_00021", "Gym Gears - Coin Between Entrance Weightlifting Area Exit Hallway Pillars #3" },
            { "6_03_00022", "Gym Gears - Coin Between Entrance Weightlifting Area Exit Hallway Pillars #4" },
            { "6_03_00024", "Gym Gears - Coin Between Entrance Weightlifting Area Exit Hallway Pillars #5" },
            { "6_03_00027", "Gym Gears - Coin in Hallway to Main Area #1" },
            { "6_03_00029", "Gym Gears - Coin in Hallway to Main Area #2" },
            { "6_03_00030", "Gym Gears - Coin in Hallway to Main Area #3" },
            { "6_03_00048", "Gym Gears - Coin in Hallway to Main Area #4" },
            { "6_03_00049", "Gym Gears - Coin in Hallway to Main Area #5" },
            { "6_03_00050", "Gym Gears - Coin in Hallway to Main Area #6" },
            { "6_03_00051", "Gym Gears - Coin in Hallway to Main Area #7" },
            { "6_09_00170", "Gym Gears - Checkpoint in Hallway to Main Area" },
            { "6_03_00052", "Gym Gears - Coin in Hallway to Main Area #8" },
            { "6_03_00053", "Gym Gears - Coin in Hallway to Main Area #9" },
            { "6_03_00054", "Gym Gears - Coin in Hallway to Main Area #10" },
            { "6_03_00031", "Gym Gears - Coin in Left Row in Main Area #1" },
            { "6_03_00032", "Gym Gears - Coin in Left Row in Main Area #2" },
            { "6_03_00033", "Gym Gears - Coin in Left Row in Main Area #3" },
            { "6_03_00034", "Gym Gears - Coin in Left Row in Main Area #4" },
            { "6_03_00035", "Gym Gears - Coin in Left Row in Main Area #5" },
            { "6_03_00036", "Gym Gears - Coin in Left Row in Main Area #6" },
            { "6_03_00037", "Gym Gears - Coin in Left Row in Main Area #7" },
            { "6_03_00038", "Gym Gears - Coin in Left Row in Main Area #8" },
            { "6_03_00039", "Gym Gears - Coin in Left Row in Main Area #9" },
            { "6_03_00040", "Gym Gears - Coin in Left Row in Main Area #10" },
            { "6_03_00041", "Gym Gears - Coin in Left Row in Main Area #11" },
            { "6_03_00042", "Gym Gears - Coin in Left Row in Main Area #12" },
            { "6_03_00043", "Gym Gears - Coin in Left Row in Main Area #13" },
            { "6_03_00044", "Gym Gears - Coin in Left Row in Main Area #14" },
            { "6_03_00045", "Gym Gears - Coin in Left Row in Main Area #15" },
            { "6_03_00046", "Gym Gears - Coin in Left Row in Main Area #16" },
            { "6_03_00047", "Gym Gears - Coin in Left Row in Main Area #17" },
            { "6_03_00058", "Gym Gears - Coin in Right Row in Main Area #1" },
            { "6_03_00059", "Gym Gears - Coin in Right Row in Main Area #2" },
            { "6_03_00060", "Gym Gears - Coin in Right Row in Main Area #3" },
            { "6_03_00061", "Gym Gears - Coin in Right Row in Main Area #4" },
            { "6_03_00062", "Gym Gears - Coin in Right Row in Main Area #5" },
            { "6_03_00063", "Gym Gears - Coin in Right Row in Main Area #6" },
            { "6_03_00064", "Gym Gears - Coin in Right Row in Main Area #7" },
            { "6_03_00065", "Gym Gears - Coin in Right Row in Main Area #8" },
            { "6_03_00066", "Gym Gears - Coin in Right Row in Main Area #9" },
            { "6_03_00067", "Gym Gears - Coin in Right Row in Main Area #10" },
            { "6_03_00068", "Gym Gears - Coin in Right Row in Main Area #11" },
            { "6_03_00069", "Gym Gears - Coin in Right Row in Main Area #12" },
            { "6_03_00070", "Gym Gears - Coin in Right Row in Main Area #13" },
            { "6_03_00071", "Gym Gears - Coin in Right Row in Main Area #14" },
            { "6_03_00072", "Gym Gears - Coin in Right Row in Main Area #15" },
            { "6_03_00073", "Gym Gears - Coin in Right Row in Main Area #16" },
            { "6_03_00074", "Gym Gears - Coin in Right Row in Main Area #17" },
            { "6_03_00055", "Gym Gears - Chest in Center of Main Area" },
            { "6_03_00082", "Gym Gears - Coin Near Rock Climbing Wall #1" },
            { "6_03_00086", "Gym Gears - Coin Near Rock Climbing Wall #2" },
            { "6_03_00090", "Gym Gears - Coin Near Rock Climbing Wall #3" },
            { "6_03_00083", "Gym Gears - Coin Near Rock Climbing Wall #4" },
            { "6_03_00087", "Gym Gears - Coin Near Rock Climbing Wall #5" },
            { "6_03_00091", "Gym Gears - Coin Near Rock Climbing Wall #6" },
            { "6_03_00084", "Gym Gears - Coin Near Rock Climbing Wall #7" },
            { "6_03_00088", "Gym Gears - Coin Near Rock Climbing Wall #8" },
            { "6_03_00092", "Gym Gears - Coin Near Rock Climbing Wall #9" },
            { "6_03_00057", "Gym Gears - Coin Near Back Window #1" },
            { "6_03_00076", "Gym Gears - Coin Near Back Window #2" },
            { "6_03_00078", "Gym Gears - Coin Near Back Window #3" },
            { "6_03_00080", "Gym Gears - Coin Near Back Window #4" },
            { "6_03_00085", "Gym Gears - Coin Near Back Window #5" },
            { "6_03_00081", "Gym Gears - Coin Near Front Window #1" },
            { "6_03_00079", "Gym Gears - Coin Near Front Window #2" },
            { "6_03_00077", "Gym Gears - Coin Near Front Window #3" },
            { "6_03_00075", "Gym Gears - Coin Near Front Window #4" },
            { "6_03_00056", "Gym Gears - Coin Near Front Window #5" },
            { "6_03_00023", "Gym Gears - Coin in Main Area Poster Corner #1" },
            { "6_03_00025", "Gym Gears - Coin in Main Area Poster Corner #2" },
            { "6_03_00028", "Gym Gears - Coin in Main Area Poster Corner #3" },
            { "6_21_00001", "Gym Gears - Cheese Under Spin Blocks #1" },
            { "6_21_00000", "Gym Gears - Cheese Under Spin Blocks #2" },
            { "6_03_00089", "Gym Gears - Coin in Ultra Chad Hallway #1" },
            { "6_03_00093", "Gym Gears - Coin in Ultra Chad Hallway #2" },
            { "6_03_00094", "Gym Gears - Coin in Ultra Chad Hallway #3" },
            { "6_03_00095", "Gym Gears - Coin in Ultra Chad Hallway #4" },
            { "6_03_00096", "Gym Gears - Coin in Ultra Chad Hallway #5" },
            { "6_03_00097", "Gym Gears - Coin in Ultra Chad Hallway #6" },
            { "6_03_00098", "Gym Gears - Coin in Ultra Chad Hallway #7" },
        };

        [Description("Gym Gears - Expert 1")]
        public static Dictionary<string, string> GymGearsExpert1 = new()
        {
            { "6_03_00099", "Gym Gears - Coin on Steps in Entrance Weightlifting Area #1" },
            { "6_03_00100", "Gym Gears - Coin on Left Side Steps in Main Area #1" },
        };

        [Description("Gym Gears - Expert 2")]
        public static Dictionary<string, string> GymGearsExpert2 = new()
        {
            { "6_03_00102", "Gym Gears - Coin on Left Side Steps in Main Area #2" },
            { "6_03_00105", "Gym Gears - Coin on Left Side Steps 20 Tons Block in Main Area #1" },
            { "6_03_00111", "Gym Gears - Coin on Left Side Steps 20 Tons Block in Main Area #2" },
            { "6_03_00118", "Gym Gears - Coin on Left Side Steps 20 Tons Block in Main Area #3" },
        };

        [Description("Gym Gears - Jump")]
        public static Dictionary<string, string> GymGearsJump = new()
        {
            { "6_03_00101", "Gym Gears - Coin on Steps in Entrance Weightlifting Area #2" },
            { "6_03_00104", "Gym Gears - Coin on 20 Tons Block Near Hat in Entrance Weightlifting Area #1" },
            { "6_03_00110", "Gym Gears - Coin on 20 Tons Block Near Hat in Entrance Weightlifting Area #2" },
            { "6_03_00117", "Gym Gears - Coin on 20 Tons Block Near Hat in Entrance Weightlifting Area #3" },
            { "6_03_00108", "Gym Gears - Coin on 20 Tons Block Steps in Entrance Weightlifting Area #1" },
            { "6_03_00114", "Gym Gears - Coin on 20 Tons Block Steps in Entrance Weightlifting Area #2" },
            { "6_03_00121", "Gym Gears - Coin on 20 Tons Block Steps in Entrance Weightlifting Area #3" },
            { "6_03_00126", "Gym Gears - Coin Bag on 20 Tons Block in Entrance Weightlifting Area" },
            { "6_03_00122", "Gym Gears - Coin on 20 Tons Block Steps in Entrance Weightlifting Area #4" },
            { "6_03_00127", "Gym Gears - Coin on 20 Tons Block Steps in Entrance Weightlifting Area #5" },
            { "6_03_00133", "Gym Gears - Coin on 20 Tons Block Steps in Entrance Weightlifting Area #6" },
            { "6_03_00138", "Gym Gears - Chest on 20 Tons Block in Entrance Weightlifting Area" },
            { "6_03_00130", "Gym Gears - Coin on Beam Above Entrance Weightlifting Area #1" },
            { "6_01_00000", "Gym Gears - Gear - Beam Above Entrance Weightlifting Area" },
            { "6_03_00132", "Gym Gears - Coin on Beam Above Entrance Weightlifting Area #2" },
            { "6_03_00106", "Gym Gears - Coin on Left Center 20 Tons Block in Main Area #1" },
            { "6_03_00112", "Gym Gears - Coin on Left Center 20 Tons Block in Main Area #2" },
            { "6_03_00119", "Gym Gears - Coin on Left Center 20 Tons Block in Main Area #3" },
            { "6_01_00001", "Gym Gears - Gear - Back Left Corner of Main Area" },
            { "6_03_00134", "Gym Gears - Coin on 20 Tons Block Towards Bunny #1" },
            { "6_03_00139", "Gym Gears - Coin on 20 Tons Block Towards Bunny #2" },
            { "6_03_00152", "Gym Gears - Coin on 20 Tons Block Towards Bunny #3" },
            { "6_03_00135", "Gym Gears - Coin on 20 Tons Block Towards Bunny #4" },
            { "6_03_00140", "Gym Gears - Coin on 20 Tons Block Towards Bunny #5" },
            { "6_03_00153", "Gym Gears - Coin on 20 Tons Block Towards Bunny #6" },
            { "6_09_00360", "Gym Gears - Checkpoint Near Bunny" },
            { "6_02_00001", "Gym Gears - Bunny - Back of Main Area" },
            { "6_03_00107", "Gym Gears - Coin on Left Back 20 Tons Block in Main Area #1" },
            { "6_03_00113", "Gym Gears - Coin on Left Back 20 Tons Block in Main Area #2" },
            { "6_03_00120", "Gym Gears - Coin on Left Back 20 Tons Block in Main Area #3" },
            { "6_03_00109", "Gym Gears - Coin on 20 Tons Block Stairway to Upper Area #1" },
            { "6_03_00115", "Gym Gears - Coin on 20 Tons Block Stairway to Upper Area #2" },
            { "6_03_00123", "Gym Gears - Coin on 20 Tons Block Stairway to Upper Area #3" },
            { "6_03_00124", "Gym Gears - Coin on 20 Tons Block Stairway to Upper Area #4" },
            { "6_03_00128", "Gym Gears - Coin on 20 Tons Block Stairway to Upper Area #5" },
            { "6_03_00136", "Gym Gears - Coin on 20 Tons Block Stairway to Upper Area #6" },
            { "6_03_00137", "Gym Gears - Coin on 20 Tons Block Stairway to Upper Area #7" },
            { "6_03_00150", "Gym Gears - Coin on 20 Tons Block Stairway to Upper Area #8" },
            { "6_03_00154", "Gym Gears - Coin on 20 Tons Block Stairway to Upper Area #9" },
            { "6_03_00157", "Gym Gears - Coin Bag on 20 Tons Block Stairway to Upper Area #1" },
            { "6_03_00155", "Gym Gears - Coin on 20 Tons Block Stairway to Upper Area #10" },
            { "6_03_00158", "Gym Gears - Coin on 20 Tons Block Stairway to Upper Area #11" },
            { "6_03_00162", "Gym Gears - Coin on 20 Tons Block Stairway to Upper Area #12" },
            { "6_03_00156", "Gym Gears - Coin on 20 Tons Block Stairway to Upper Area #13" },
            { "6_03_00159", "Gym Gears - Coin on 20 Tons Block Stairway to Upper Area #14" },
            { "6_03_00163", "Gym Gears - Coin on 20 Tons Block Stairway to Upper Area #15" },
            { "6_09_00350", "Gym Gears - Checkpoint Halfway to Upper Area" },
            { "6_03_00160", "Gym Gears - Coin on 20 Tons Block Stairway to Upper Area #16" },
            { "6_03_00164", "Gym Gears - Coin on 20 Tons Block Stairway to Upper Area #17" },
            { "6_03_00168", "Gym Gears - Coin on 20 Tons Block Stairway to Upper Area #18" },
            { "6_03_00173", "Gym Gears - Coin Bag on 20 Tons Block Stairway to Upper Area #2" },
            { "6_03_00165", "Gym Gears - Coin on 20 Tons Block Stairway to Upper Area #19" },
            { "6_03_00169", "Gym Gears - Coin on 20 Tons Block Stairway to Upper Area #20" },
            { "6_03_00174", "Gym Gears - Coin on 20 Tons Block Stairway to Upper Area #21" },
            { "6_03_00178", "Gym Gears - Chest on 20 Tons Block Stairway to Upper Area" },
            { "6_03_00170", "Gym Gears - Coin on 20 Tons Block Stairway to Upper Area #22" },
            { "6_03_00175", "Gym Gears - Coin on 20 Tons Block Stairway to Upper Area #23" },
            { "6_03_00179", "Gym Gears - Coin on 20 Tons Block Stairway to Upper Area #24" },
            { "6_01_00003", "Gym Gears - Gear - Top of 20 Tons Block Stairway to Upper Area" },
            { "6_03_00185", "Gym Gears - Coin on Upper Area Stairway #1" },
            { "6_03_00190", "Gym Gears - Coin on Upper Area Stairway #2" },
            { "6_03_00194", "Gym Gears - Coin on Upper Area Stairway #3" },
            { "6_03_00195", "Gym Gears - Coin on Upper Area 20 Tons Block #1" },
            { "6_03_00196", "Gym Gears - Coin on Upper Area 20 Tons Block #2" },
            { "6_03_00197", "Gym Gears - Coin on Upper Area 20 Tons Block #3" },
            { "6_03_00198", "Gym Gears - Coin on Upper Area 20 Tons Block #4" },
            { "6_03_00231", "Gym Gears - Coin on Upper Area 20 Tons Block #5" },
            { "6_01_00005", "Gym Gears - Gear - Highest Point" },
            { "6_03_00217", "Gym Gears - Coin on Beam Above Main Area #1" },
            { "6_03_00216", "Gym Gears - Coin on Beam Above Main Area #2" },
            { "6_03_00215", "Gym Gears - Coin on Beam Above Main Area #3" },
            { "6_03_00214", "Gym Gears - Coin on Beam Above Main Area #4" },
            { "6_03_00213", "Gym Gears - Coin on Beam Above Main Area #5" },
            { "6_03_00212", "Gym Gears - Coin on Beam Above Main Area #6" },
            { "6_03_00211", "Gym Gears - Coin on Beam Above Main Area #7" },
            { "6_03_00210", "Gym Gears - Coin on Beam Above Main Area #8" },
            { "6_03_00209", "Gym Gears - Coin on Beam Above Main Area #9" },
            { "6_03_00208", "Gym Gears - Chest on Beam Above Main Area" },
            { "6_03_00207", "Gym Gears - Coin on Beam Above Main Area #10" },
            { "6_03_00206", "Gym Gears - Coin on Beam Above Main Area #11" },
            { "6_03_00205", "Gym Gears - Coin on Beam Above Main Area #12" },
            { "6_03_00204", "Gym Gears - Coin on Beam Above Main Area #13" },
            { "6_03_00203", "Gym Gears - Coin on Beam Above Main Area #14" },
            { "6_03_00202", "Gym Gears - Coin on Beam Above Main Area #15" },
            { "6_03_00201", "Gym Gears - Coin on Beam Above Main Area #16" },
            { "6_03_00200", "Gym Gears - Coin on Beam Above Main Area #17" },
            { "6_03_00199", "Gym Gears - Coin on Beam Above Main Area #18" },
            { "6_03_00218", "Gym Gears - Coin on Beam Above Main Area #19" },
            { "6_03_00219", "Gym Gears - Coin on Beam Above Main Area #20" },
            { "6_03_00220", "Gym Gears - Coin on Beam Above Main Area #21" },
            { "6_03_00221", "Gym Gears - Coin on Beam Above Main Area #22" },
            { "6_03_00222", "Gym Gears - Coin on Beam Above Main Area #23" },
            { "6_03_00223", "Gym Gears - Coin on Beam Above Main Area #24" },
            { "6_03_00224", "Gym Gears - Coin Bag on Beam Above Main Area #1" },
            { "6_03_00225", "Gym Gears - Coin Bag on Beam Above Main Area #2" },
            { "6_03_00227", "Gym Gears - Coin on Beam Above Main Area #25" },
            { "6_03_00228", "Gym Gears - Coin on Beam Above Main Area #26" },
            { "6_03_00229", "Gym Gears - Coin on Beam Above Main Area #27" },
            { "6_03_00230", "Gym Gears - Coin on Beam Above Main Area #28" },
            { "6_03_00232", "Gym Gears - Coin on Beam Above Main Area #29" },
            { "6_03_00233", "Gym Gears - Coin on Beam Above Main Area #30" },
            { "6_02_00000", "Gym Gears - Bunny - On Center Pillar" },
            { "6_03_00149", "Gym Gears - Coin in Alcove Above Entrance to Main Area #1" },
            { "6_03_00146", "Gym Gears - Coin in Alcove Above Entrance to Main Area #2" },
            { "6_03_00143", "Gym Gears - Coin in Alcove Above Entrance to Main Area #3" },
            { "6_03_00148", "Gym Gears - Coin in Alcove Above Entrance to Main Area #4" },
            { "6_03_00145", "Gym Gears - Coin in Alcove Above Entrance to Main Area #5" },
            { "6_03_00142", "Gym Gears - Coin in Alcove Above Entrance to Main Area #6" },
            { "6_03_00147", "Gym Gears - Coin in Alcove Above Entrance to Main Area #7" },
            { "6_03_00144", "Gym Gears - Coin in Alcove Above Entrance to Main Area #8" },
            { "6_03_00141", "Gym Gears - Coin in Alcove Above Entrance to Main Area #9" },
            { "6_03_00161", "Gym Gears - Coin on 20 Tons Block in Alcove Above Entrance to Main Area #1" },
            { "6_03_00166", "Gym Gears - Coin on 20 Tons Block in Alcove Above Entrance to Main Area #2" },
            { "6_03_00172", "Gym Gears - Coin on 20 Tons Block in Alcove Above Entrance to Main Area #3" },
            { "6_03_00177", "Gym Gears - Coin Bag on 20 Tons Block in Alcove Above Entrance to Main Area #1" },
            { "6_03_00171", "Gym Gears - Coin on 20 Tons Block in Alcove Above Entrance to Main Area #4" },
            { "6_03_00176", "Gym Gears - Coin on 20 Tons Block in Alcove Above Entrance to Main Area #5" },
            { "6_03_00180", "Gym Gears - Coin on 20 Tons Block in Alcove Above Entrance to Main Area #6" },
            { "6_03_00183", "Gym Gears - Coin Bag on 20 Tons Block in Alcove Above Entrance to Main Area #2" },
            { "6_03_00181", "Gym Gears - Coin on 20 Tons Block in Alcove Above Entrance to Main Area #7" },
            { "6_03_00184", "Gym Gears - Coin on 20 Tons Block in Alcove Above Entrance to Main Area #8" },
            { "6_03_00187", "Gym Gears - Coin on 20 Tons Block in Alcove Above Entrance to Main Area #9" },
            { "6_03_00189", "Gym Gears - Chest on 20 Tons Block in Alcove Above Entrance to Main Area" },
            { "6_03_00186", "Gym Gears - Coin on 20 Tons Block in Alcove Above Entrance to Main Area #10" },
            { "6_03_00188", "Gym Gears - Coin on 20 Tons Block in Alcove Above Entrance to Main Area #11" },
            { "6_03_00191", "Gym Gears - Coin on 20 Tons Block in Alcove Above Entrance to Main Area #12" },
            { "6_01_00002", "Gym Gears - Gear - Alcove Above Entrance to Main Area" },
            { "6_03_00103", "Gym Gears - Coin Bag on Broken Pillar in Ultra Chad Room" },
            { "6_03_00116", "Gym Gears - Coin Bag on Pillar in Ultra Chad Room" },
            { "6_01_00004", "Gym Gears - Gear - Ultra Chad Room" },
            { "6_02_00002", "Gym Gears - Bunny - Ultra Chad Room" },
        };

        public static Dictionary<string, string> GymGearsSpecialRules = new()
        {
            { "Gym Gears - Bunny - Ultra Chad Room", "X1/J2" },
        };

        #endregion

        #region Fecal Matters

        [Description("Fecal Matters - Starting Area")]
        public static Dictionary<string, string> FecalMattersStartingArea = new()
        {
            { "7_03_00003", "Fecal Matters - Coin in Row Between Roundabout and Entrance #1" },
            { "7_03_00004", "Fecal Matters - Coin in Row Between Roundabout and Entrance #2" },
            { "7_03_00005", "Fecal Matters - Coin in Row Between Roundabout and Entrance #3" },
            { "7_03_00006", "Fecal Matters - Coin in Row Between Roundabout and Entrance #4" },
            { "7_03_00007", "Fecal Matters - Coin in Row Between Roundabout and Entrance #5" },
            { "7_03_00008", "Fecal Matters - Coin in Row Between Roundabout and Entrance #6" },
            { "7_03_00009", "Fecal Matters - Coin in Row Between Roundabout and Entrance #7" },
            { "7_03_00016", "Fecal Matters - Coin in Row From Roundabout Towards Village Island #1" },
            { "7_03_00015", "Fecal Matters - Coin in Row From Roundabout Towards Village Island #2" },
            { "7_03_00014", "Fecal Matters - Coin in Row From Roundabout Towards Village Island #3" },
            { "7_03_00013", "Fecal Matters - Coin in Row From Roundabout Towards Village Island #4" },
            { "7_03_00012", "Fecal Matters - Coin in Row From Roundabout Towards Village Island #5" },
            { "7_03_00022", "Fecal Matters - Coin in Row From Roundabout Towards Mountain #1" },
            { "7_03_00024", "Fecal Matters - Coin in Row From Roundabout Towards Mountain #2" },
            { "7_03_00026", "Fecal Matters - Coin in Row From Roundabout Towards Mountain #3" },
            { "7_03_00028", "Fecal Matters - Coin in Row From Roundabout Towards Mountain #4" },
            { "7_03_00030", "Fecal Matters - Coin in Row From Roundabout Towards Mountain #5" },
            { "7_03_00017", "Fecal Matters - Coin in Row From Roundabout Towards Cul-de-sac #1" },
            { "7_03_00018", "Fecal Matters - Coin in Row From Roundabout Towards Cul-de-sac #2" },
            { "7_03_00019", "Fecal Matters - Coin in Row From Roundabout Towards Cul-de-sac #3" },
            { "7_03_00020", "Fecal Matters - Coin in Row From Roundabout Towards Cul-de-sac #4" },
            { "7_03_00021", "Fecal Matters - Coin in Row From Roundabout Towards Cul-de-sac #5" },
            { "7_21_00005", "Fecal Matters - Cheese by Roundabout" },
            { "7_03_00023", "Fecal Matters - Coin on Road Towards Cul-de-sac #1" },
            { "7_03_00025", "Fecal Matters - Coin on Road Towards Cul-de-sac #2" },
            { "7_03_00027", "Fecal Matters - Coin on Road Towards Cul-de-sac #3" },
            { "7_03_00029", "Fecal Matters - Coin on Road Towards Cul-de-sac #4" },
            { "7_03_00031", "Fecal Matters - Coin on Road Towards Cul-de-sac #5" },
            { "7_03_00032", "Fecal Matters - Coin on Road Towards Cul-de-sac #6" },
            { "7_03_00033", "Fecal Matters - Coin on Road Towards Cul-de-sac #7" },
            { "7_03_00034", "Fecal Matters - Coin on Road Towards Cul-de-sac #8" },
            { "7_03_00035", "Fecal Matters - Coin on Road Towards Cul-de-sac #9" },
            { "7_03_00036", "Fecal Matters - Coin on Road Towards Cul-de-sac #10" },
            { "7_03_00037", "Fecal Matters - Coin on Road Towards Cul-de-sac #11" },
            { "7_03_00038", "Fecal Matters - Coin on Road Towards Cul-de-sac #12" },
            { "7_03_00039", "Fecal Matters - Coin on Road Towards Cul-de-sac #13" },
            { "7_03_00040", "Fecal Matters - Coin on Road Towards Cul-de-sac #14" },
            { "7_03_00041", "Fecal Matters - Coin on Road Towards Cul-de-sac #15" },
            { "7_03_00042", "Fecal Matters - Coin on Road Towards Cul-de-sac #16" },
            { "7_03_00043", "Fecal Matters - Coin on Road Towards Cul-de-sac #17" },
            { "7_03_00044", "Fecal Matters - Coin on Road Towards Cul-de-sac #18" },
            { "7_03_00045", "Fecal Matters - Coin on Road Towards Cul-de-sac #19" },
            { "7_21_00000", "Fecal Matters - Cheese by Cul-de-sac" },
        };

        [Description("Fecal Matters - Roundabout Hill Tier 1")]
        public static Dictionary<string, string> FecalMattersRoundaboutHillTier1 = new()
        {
            { "7_03_00046", "Fecal Matters - Coin on Hill Above Roundabout #1" },
            { "7_03_00047", "Fecal Matters - Coin on Hill Above Roundabout #2" },
            { "7_03_00049", "Fecal Matters - Coin on Hill Above Roundabout #3" },
            { "7_03_00048", "Fecal Matters - Coin on Hill Above Roundabout #4" },
            { "7_03_00050", "Fecal Matters - Coin on Hill Above Roundabout #5" },
            { "7_03_00052", "Fecal Matters - Coin on Hill Above Roundabout #6" },
            { "7_03_00051", "Fecal Matters - Coin on Hill Above Roundabout #7" },
            { "7_03_00053", "Fecal Matters - Coin on Hill Above Roundabout #8" },
        };

        // Tier 2 is part of greater "Boost or Jump" region

        [Description("Fecal Matters - Roundabout Hill Tier 3")]
        public static Dictionary<string, string> FecalMattersRoundaboutHillTier3 = new()
        {
            { "7_01_00008", "Fecal Matters - Gear - Escort Bulldog Doggo on Roundabout Hill" },
            { "7_02_00000", "Fecal Matters - Bunny - Above Roundabout" },
        };

        [Description("Fecal Matters - Boost or Jump")]
        public static Dictionary<string, string> FecalMattersBoostOrJump = new()
        {
            { "7_03_00064", "Fecal Matters - Coin on Hill Above Roundabout #9" },
            { "7_03_00063", "Fecal Matters - Coin on Hill Above Roundabout #10" },
            { "7_03_00062", "Fecal Matters - Coin on Hill Above Roundabout #11" },
            { "7_03_00079", "Fecal Matters - Coin on Ledge Above Cul-de-sac #1" },
            { "7_03_00078", "Fecal Matters - Coin on Ledge Above Cul-de-sac #2" },
            { "7_03_00077", "Fecal Matters - Coin on Ledge Above Cul-de-sac #3" },
            { "7_03_00076", "Fecal Matters - Coin on Ledge Above Cul-de-sac #4" },
            { "7_01_00003", "Fecal Matters - Gear - Ledge Above Cul-de-sac" },
            { "7_03_00074", "Fecal Matters - Coin on Ledge Above Cul-de-sac #5" },
            { "7_03_00073", "Fecal Matters - Coin on Ledge Above Cul-de-sac #6" },
            { "7_03_00072", "Fecal Matters - Coin on Ledge Above Cul-de-sac #7" },
            { "7_03_00071", "Fecal Matters - Coin on Ledge Above Cul-de-sac #8" },
            { "7_21_00003", "Fecal Matters - Cheese in Tunnel on Ramp Over Main Road" },
            { "7_03_00054", "Fecal Matters - Coin on Ramp Over Main Road #1" },
            { "7_03_00056", "Fecal Matters - Coin on Ramp Over Main Road #2" },
            { "7_03_00058", "Fecal Matters - Coin on Ramp Over Main Road #3" },
            { "7_03_00055", "Fecal Matters - Coin on Ramp Over Main Road #4" },
            { "7_03_00057", "Fecal Matters - Chest on Ramp Over Main Road" },
            { "7_03_00059", "Fecal Matters - Coin on Ramp Over Main Road #5" },
            { "7_03_00088", "Fecal Matters - Coin on Island Overlooking Main Road #1" },
            { "7_03_00091", "Fecal Matters - Coin on Island Overlooking Main Road #2" },
            { "7_03_00094", "Fecal Matters - Coin on Island Overlooking Main Road #3" },
            { "7_03_00086", "Fecal Matters - Coin on Island Overlooking Main Road #4" },
            { "7_03_00089", "Fecal Matters - Coin on Island Overlooking Main Road #5" },
            { "7_03_00092", "Fecal Matters - Coin on Island Overlooking Main Road #6" },
            { "7_03_00095", "Fecal Matters - Coin on Island Overlooking Main Road #7" },
            { "7_03_00099", "Fecal Matters - Coin on Island Overlooking Main Road #8" },
            { "7_03_00087", "Fecal Matters - Coin on Island Overlooking Main Road #9" },
            { "7_03_00090", "Fecal Matters - Coin on Island Overlooking Main Road #10" },
            { "7_03_00093", "Fecal Matters - Coin on Island Overlooking Main Road #11" },
            { "7_03_00100", "Fecal Matters - Coin on Island Overlooking Main Road #12" },
            { "7_03_00104", "Fecal Matters - Coin on Island Overlooking Main Road #13" },
            { "7_03_00096", "Fecal Matters - Coin on Island Overlooking Main Road #14" },
            { "7_03_00101", "Fecal Matters - Coin on Island Overlooking Main Road #15" },
            { "7_03_00105", "Fecal Matters - Coin on Island Overlooking Main Road #16" },
            { "7_03_00097", "Fecal Matters - Coin on Island Overlooking Main Road #17" },
            { "7_03_00102", "Fecal Matters - Coin on Island Overlooking Main Road #18" },
            { "7_03_00106", "Fecal Matters - Coin on Island Overlooking Main Road #19" },
            { "7_03_00098", "Fecal Matters - Coin on Island Overlooking Main Road #20" },
            { "7_03_00103", "Fecal Matters - Coin on Island Overlooking Main Road #21" },
            { "7_03_00128", "Fecal Matters - Coin on Island Overlooking Main Road #22" },
            { "7_03_00125", "Fecal Matters - Coin on Island Overlooking Main Road #23" },
            { "7_03_00129", "Fecal Matters - Coin on Island Overlooking Main Road #24" },
            { "7_03_00123", "Fecal Matters - Coin on Island Overlooking Main Road #25" },
            { "7_03_00126", "Fecal Matters - Coin Bag on Overlooking Main Road" },
            { "7_03_00130", "Fecal Matters - Coin on Island Overlooking Main Road #26" },
            { "7_03_00124", "Fecal Matters - Coin on Island Overlooking Main Road #27" },
            { "7_03_00127", "Fecal Matters - Coin on Island Overlooking Main Road #28" },
            { "7_03_00066", "Fecal Matters - Coin Near Poop Hat #1" },
            { "7_03_00067", "Fecal Matters - Coin Near Poop Hat #2" },
            { "7_03_00069", "Fecal Matters - Coin Near Poop Hat #3" },
            { "7_03_00068", "Fecal Matters - Coin Near Poop Hat #4" },
            { "7_03_00070", "Fecal Matters - Coin Near Poop Hat #5" },
            { "7_01_00002", "Fecal Matters - Gear - Homing Beacons" },
        };

        [Description("Fecal Matters - Boost or Backflip")]
        public static Dictionary<string, string> FecalMattersBoostOrBackflip = new()
        {
            { "7_01_00006", "Fecal Matters - Gear - Center of Village Island" },
            { "7_21_00004", "Fecal Matters - Cheese on Village Island" },
            { "7_01_00007", "Fecal Matters - Gear - Escort Bulldog Doggo on Village Island" },
            { "7_01_00004", "Fecal Matters - Gear - Fire Hydrant on Mountain" },
            { "7_21_00002", "Fecal Matters - Cheese on Mountain" },
            { "7_01_00001", "Fecal Matters - Gear - Talk to Skeletrone" },
            { "7_01_00005", "Fecal Matters - Gear - House on Top of Mountain" },
            { "7_03_00084", "Fecal Matters - Coin Near Dog Food Hat #1" },
            { "7_03_00083", "Fecal Matters - Coin Near Dog Food Hat #2" },
            { "7_03_00082", "Fecal Matters - Coin Near Dog Food Hat #3" },
            { "7_03_00081", "Fecal Matters - Coin Near Dog Food Hat #4" },
            { "7_21_00001", "Fecal Matters - Cheese on Island" },
        };

        [Description("Fecal Matters - Highest Ground")]
        public static Dictionary<string, string> FecalMattersHighestGround = new()
        {
            { "7_03_00134", "Fecal Matters - Coin on Top of Mountain Above Orange Switch Block #1" },
            { "7_03_00135", "Fecal Matters - Coin on Top of Mountain Above Orange Switch Block #2" },
            { "7_03_00137", "Fecal Matters - Coin on Top of Mountain Above Orange Switch Block #3" },
            { "7_03_00145", "Fecal Matters - Coin on Top of Mountain Above Orange Switch Block #4" },
            { "7_03_00153", "Fecal Matters - Coin on Top of Mountain Above Orange Switch Block #5" },
            { "7_03_00157", "Fecal Matters - Coin on Top of Mountain Above Orange Switch Block #6" },
            { "7_03_00159", "Fecal Matters - Coin on Top of Mountain Above Orange Switch Block #7" },
            { "7_03_00160", "Fecal Matters - Coin on Top of Mountain Above Orange Switch Block #8" },
            { "7_03_00136", "Fecal Matters - Coin on Top of Mountain by Bunny #1" },
            { "7_03_00140", "Fecal Matters - Coin on Top of Mountain by Bunny #2" },
            { "7_03_00139", "Fecal Matters - Coin on Top of Mountain by Bunny #3" },
            { "7_03_00138", "Fecal Matters - Coin on Top of Mountain by Bunny #4" },
            { "7_03_00144", "Fecal Matters - Coin on Top of Mountain by Bunny #5" },
            { "7_03_00143", "Fecal Matters - Coin Bag on Top of Mountain by Bunny #1" },
            { "7_03_00142", "Fecal Matters - Coin on Top of Mountain by Bunny #6" },
            { "7_03_00148", "Fecal Matters - Coin on Top of Mountain by Bunny #7" },
            { "7_02_00002", "Fecal Matters - Bunny - On Top of Mountain" },
            { "7_03_00146", "Fecal Matters - Coin on Top of Mountain by Bunny #8" },
            { "7_03_00152", "Fecal Matters - Coin on Top of Mountain by Bunny #9" },
            { "7_03_00151", "Fecal Matters - Coin Bag on Top of Mountain by Bunny #2" },
            { "7_03_00150", "Fecal Matters - Coin on Top of Mountain by Bunny #10" },
            { "7_03_00156", "Fecal Matters - Coin on Top of Mountain by Bunny #11" },
            { "7_03_00155", "Fecal Matters - Coin on Top of Mountain by Bunny #12" },
            { "7_03_00154", "Fecal Matters - Coin on Top of Mountain by Bunny #13" },
            { "7_03_00158", "Fecal Matters - Coin on Top of Mountain by Bunny #14" },
            { "7_02_00001", "Fecal Matters - Bunny - Off Cliff" },
            { "7_03_00122", "Fecal Matters - Coin on Narrow Cliff #1" },
            { "7_03_00121", "Fecal Matters - Coin on Narrow Cliff #2" },
            { "7_03_00120", "Fecal Matters - Coin on Narrow Cliff #3" },
            { "7_03_00119", "Fecal Matters - Coin on Narrow Cliff #4" },
            { "7_03_00118", "Fecal Matters - Coin on Narrow Cliff #5" },
            { "7_03_00117", "Fecal Matters - Coin on Narrow Cliff #6" },
            { "7_03_00116", "Fecal Matters - Coin on Narrow Cliff #7" },
            { "7_03_00115", "Fecal Matters - Coin on Narrow Cliff #8" },
            { "7_03_00114", "Fecal Matters - Coin on Narrow Cliff #9" },
            { "7_03_00113", "Fecal Matters - Coin on Narrow Cliff #10" },
            { "7_03_00112", "Fecal Matters - Coin on Narrow Cliff #11" },
            { "7_03_00111", "Fecal Matters - Coin on Narrow Cliff #12" },
            { "7_03_00110", "Fecal Matters - Coin on Narrow Cliff #13" },
            { "7_01_00000", "Fecal Matters - Gear - On Narrow Cliff" },
        };

        public static Dictionary<string, string> FecalMattersSpecialRules = new()
        {
            { "Fecal Matters - Gear - Escort Bulldog Doggo on Roundabout Hill", $"{{{GetDescription(nameof(FecalMattersBoostOrBackflip))}}}" },
        };

        #endregion

        #region Mosk's Rocket

        [Description("Mosk's Rocket - Starting Area")]
        public static Dictionary<string, string> RocketStartingArea = new()
        {
            { "16_21_00004", "Mosk's Rocket - Cheese Near Welcoming Climbs Portal" },
            { "16_03_00225", "Mosk's Rocket - Coin Bag Between Lab Memories and Bomb-it Portals" },
            { "16_03_00224", "Mosk's Rocket - Coin Between Lab Memories and Bomb-it Portals #1" },
            { "16_03_00223", "Mosk's Rocket - Coin Between Lab Memories and Bomb-it Portals #2" },
            { "16_03_00222", "Mosk's Rocket - Coin Between Lab Memories and Bomb-it Portals #3" },
            { "16_03_00221", "Mosk's Rocket - Coin Between Lab Memories and Bomb-it Portals #4" },
            { "16_03_00220", "Mosk's Rocket - Coin Between Lab Memories and Bomb-it Portals #5" },
            { "16_03_00219", "Mosk's Rocket - Coin Between Lab Memories and Bomb-it Portals #6" },
            { "16_03_00218", "Mosk's Rocket - Coin Between Lab Memories and Bomb-it Portals #7" },
            { "16_03_00217", "Mosk's Rocket - Coin Between Lab Memories and Bomb-it Portals #8" },
            { "16_03_00216", "Mosk's Rocket - Coin Between Lab Memories and Bomb-it Portals #9" },
        };

        [Description("Mosk's Rocket - Bombeach/Arcade Panik/Pizza Time Area")]
        public static Dictionary<string, string> RocketBombeachArcadePizzaArea = new()
        {
            { "16_21_00003", "Mosk's Rocket - Cheese Near Buttons Smashing Portal" },
        };

        [Description("Mosk's Rocket - Tosla's Offices Area")]
        public static Dictionary<string, string> RocketToslaOfficesArea = new()
        {
            { "16_03_00338", "Mosk's Rocket - Coin Near Stealthy Portal #1" },
            { "16_03_00339", "Mosk's Rocket - Coin Near Stealthy Portal #2" },
            { "16_03_00340", "Mosk's Rocket - Coin Near Stealthy Portal #3" },
            { "16_03_00341", "Mosk's Rocket - Coin Bag Near Stealthy Portal" },
        };

        [Description("Mosk's Rocket - Gym Gears Area")]
        public static Dictionary<string, string> RocketGymGearsArea = new()
        {
            { "16_03_00334", "Mosk's Rocket - Coin Near Podium Portal #1" },
            { "16_03_00335", "Mosk's Rocket - Coin Near Podium Portal #2" },
            { "16_03_00336", "Mosk's Rocket - Coin Near Podium Portal #3" },
            { "16_03_00337", "Mosk's Rocket - Coin Bag Near Podium Portal" },
        };

        [Description("Mosk's Rocket - Fecal Matters/Flushed Away Area")]
        public static Dictionary<string, string> RocketFecalFlushedArea = new()
        {
            { "16_03_00353", "Mosk's Rocket - Coin Near Smelly Slimes Portal #1" },
            { "16_03_00352", "Mosk's Rocket - Coin Near Smelly Slimes Portal #2" },
            { "16_03_00351", "Mosk's Rocket - Coin Near Smelly Slimes Portal #3" },
            { "16_03_00350", "Mosk's Rocket - Coin Near Smelly Slimes Portal #4" },
            { "16_03_00349", "Mosk's Rocket - Coin Near Smelly Slimes Portal #5" },
            { "16_21_00001", "Mosk's Rocket - Cheese Under Homing Beacons" }
        };

        [Description("Mosk's Rocket - Maurizio's City/Crash Test Industries/Morio's Mind Area")]
        public static Dictionary<string, string> RocketCityCrashMindArea = new()
        {
            { "16_21_00002", "Mosk's Rocket - Cheese Near Conveyor Belts Portal" },
        };

        [Description("Mosk's Rocket - Coins Leading to Final Floor")]
        public static Dictionary<string, string> RocketCoinsToFinalFloor = new()
        {
            { "16_03_00404", "Mosk's Rocket - Coin on Steps to Final Floor #1" },
            { "16_03_00426", "Mosk's Rocket - Coin on Steps to Final Floor #2" },
            { "16_03_00427", "Mosk's Rocket - Coin on Steps to Final Floor #3" },
            { "16_03_00428", "Mosk's Rocket - Coin on Steps to Final Floor #4" },
            { "16_03_00429", "Mosk's Rocket - Coin on Steps to Final Floor #5" },
        };

        [Description("Mosk's Rocket - Tosla HQ Area")]
        public static Dictionary<string, string> RocketToslaHQArea = new()
        {
            { "16_03_00430", "Mosk's Rocket - Coin Bag on Steps to Final Floor" },
        };

        [Description("Mosk's Rocket - Final Floor Cheese")]
        public static Dictionary<string, string> RocketFinalFloorCheese = new()
        {
            { "16_21_00000", "Mosk's Rocket - Cheese Near Top of Rocket" },
        };

        [Description("Mosk's Rocket - Moon Area")]
        public static Dictionary<string, string> RocketMoonArea = new()
        {
            { string.Empty, nameof(RocketMoonArea) + " - !PLACEHOLDER!" },
        };

        [Description("Lab Memories - Starting Area")]
        public static Dictionary<string, string> LabMemoriesStartingArea = new()
        {
            { "16_09_01925", "Lab Memories - Checkpoint" },
        };

        [Description("Lab Memories - First Step")]
        public static Dictionary<string, string> LabMemoriesFirstStep = new()
        {
            { "16_03_00157", "Lab Memories - Coin on Spiky Steps #1" },
            { "16_03_00158", "Lab Memories - Coin on Spiky Steps #2" },
            { "16_03_00159", "Lab Memories - Coin on Spiky Steps #3" },
            { "16_03_00162", "Lab Memories - Coin on Spiky Steps #4" },
            { "16_03_00164", "Lab Memories - Coin Bag on Spiky Steps #1" },
            { "16_03_00167", "Lab Memories - Coin on Spiky Steps #5" },
            { "16_03_00166", "Lab Memories - Coin on Spiky Steps #6" },
            { "16_03_00165", "Lab Memories - Coin on Spiky Steps #7" },
            { "16_03_00199", "Lab Memories - Coin on Spiky Steps #8" },
            { "16_03_00226", "Lab Memories - Coin on Spiky Steps #9" },
            { "16_03_00246", "Lab Memories - Coin on Spiky Steps #10" },
        };

        [Description("Lab Memories - High Ground")]
        public static Dictionary<string, string> LabMemoriesHighGround = new()
        {
            { "16_03_00268", "Lab Memories - Coin Bag on Spiky Steps #2" },
            { "16_03_00290", "Lab Memories - Coin on Spiky Steps #11" },
            { "16_03_00292", "Lab Memories - Coin on Spiky Steps #12" },
            { "16_03_00309", "Lab Memories - Coin on Spiky Steps #13" },
            { "16_03_00308", "Lab Memories - Coin on Spiky Steps #14" },
            { "16_03_00307", "Lab Memories - Coin Bag on Spiky Steps #3" },
            { "16_03_00281", "Lab Memories - Coin Surrounding First Gear #1" },
            { "16_03_00278", "Lab Memories - Coin Bag Surrounding First Gear #1" },
            { "16_03_00282", "Lab Memories - Coin Surrounding First Gear #2" },
            { "16_03_00286", "Lab Memories - Coin Bag Surrounding First Gear #2" },
            { "16_03_00277", "Lab Memories - Coin Surrounding First Gear #3" },
            { "16_03_00279", "Lab Memories - Coin Surrounding First Gear #4" },
            { "16_01_00000", "Lab Memories - Gear - First Gear" },
            { "16_03_00287", "Lab Memories - Coin Surrounding First Gear #5" },
            { "16_03_00289", "Lab Memories - Coin Surrounding First Gear #6" },
            { "16_03_00280", "Lab Memories - Coin Bag Surrounding First Gear #3" },
            { "16_03_00284", "Lab Memories - Coin Surrounding First Gear #7" },
            { "16_03_00288", "Lab Memories - Coin Bag Surrounding First Gear #4" },
            { "16_03_00285", "Lab Memories - Coin Surrounding First Gear #8" },

            { "16_03_00314", "Lab Memories - Coin on First Island Towards Second Gear #1" },
            { "16_03_00313", "Lab Memories - Coin on First Island Towards Second Gear #2" },
            { "16_03_00312", "Lab Memories - Coin on First Island Towards Second Gear #3" },
            { "16_03_00317", "Lab Memories - Coin on First Island Towards Second Gear #4" },
            { "16_03_00316", "Lab Memories - Coin Bag on First Island Towards Second Gear" },
            { "16_03_00315", "Lab Memories - Coin on First Island Towards Second Gear #5" },
            { "16_03_00320", "Lab Memories - Coin on First Island Towards Second Gear #6" },
            { "16_03_00319", "Lab Memories - Coin on First Island Towards Second Gear #7" },
            { "16_03_00318", "Lab Memories - Coin on First Island Towards Second Gear #8" },
            { "16_03_00321", "Lab Memories - Coin Bag on Second Island Towards Second Gear" },
            { "16_03_00324", "Lab Memories - Coin by Second Gear" },
            { "16_03_00323", "Lab Memories - Coin Bag by Second Gear" },
            { "16_01_00001", "Lab Memories - Gear - Second Gear" },
        };

        [Description("Welcoming Climbs - Starting Area")]
        public static Dictionary<string, string> WelcomingClimbsStartingArea = new()
        {
            { "16_09_01600", "Welcoming Climbs - Checkpoint" },
            { "16_03_00118", "Welcoming Climbs - Coin on Left Side of Starting Platform #1" },
            { "16_03_00120", "Welcoming Climbs - Coin on Left Side of Starting Platform #2" },
            { "16_03_00122", "Welcoming Climbs - Coin on Left Side of Starting Platform #3" },
            { "16_03_00124", "Welcoming Climbs - Coin on Left Side of Starting Platform #4" },
            { "16_03_00126", "Welcoming Climbs - Coin Bag on Left Side of Starting Platform" },
            { "16_03_00117", "Welcoming Climbs - Coin on Right Side of Starting Platform #1" },
            { "16_03_00119", "Welcoming Climbs - Coin on Right Side of Starting Platform #2" },
            { "16_03_00121", "Welcoming Climbs - Coin on Right Side of Starting Platform #3" },
            { "16_03_00123", "Welcoming Climbs - Coin on Right Side of Starting Platform #4" },
            { "16_03_00125", "Welcoming Climbs - Coin Bag on Right Side of Starting Platform" },
            { "16_03_00127", "Welcoming Climbs - Coin on Front Side of Starting Platform #1" },
            { "16_03_00128", "Welcoming Climbs - Coin on Front Side of Starting Platform #2" },
            { "16_03_00129", "Welcoming Climbs - Coin on Front Side of Starting Platform #3" },
            { "16_03_00130", "Welcoming Climbs - Coin Bag on Front Side of Starting Platform" },
        };

        [Description("Welcoming Climbs - First Gear Area")]
        public static Dictionary<string, string> WelcomingClimbsFirstGearArea = new()
        {
            { "16_03_00408", "Welcoming Climbs - Chest at Halfway Point #1" },
            { "16_01_00002", "Welcoming Climbs - Gear - Halfway Point" },
            { "16_03_00406", "Welcoming Climbs - Chest at Halfway Point #2" },
        };

        [Description("Welcoming Climbs - Second Gear Area")]
        public static Dictionary<string, string> WelcomingClimbsSecondGearArea = new()
        {
            { "16_03_00434", "Welcoming Climbs - Coin at Highest Point #1" },
            { "16_03_00433", "Welcoming Climbs - Coin at Highest Point #2" },
            { "16_03_00432", "Welcoming Climbs - Coin at Highest Point #3" },
            { "16_03_00437", "Welcoming Climbs - Coin Bag at Highest Point #1" },
            { "16_01_00003", "Welcoming Climbs - Gear - Highest Point" },
            { "16_03_00435", "Welcoming Climbs - Coin Bag at Highest Point #2" },
            { "16_03_00440", "Welcoming Climbs - Coin at Highest Point #4" },
            { "16_03_00439", "Welcoming Climbs - Coin at Highest Point #5" },
            { "16_03_00438", "Welcoming Climbs - Coin at Highest Point #6" },
        };

        [Description("Bomb-it - Starting Area")]
        public static Dictionary<string, string> BombitStartingArea = new()
        {
            { "16_09_01270", "Bomb-it - Checkpoint" },
        };

        [Description("Bomb-it - Outer Area")]
        public static Dictionary<string, string> BombitOuterArea = new()
        {
            { "16_03_00066", "Bomb-it - Coin Near Houses #1" },
            { "16_03_00067", "Bomb-it - Coin Near Houses #2" },
            { "16_03_00064", "Bomb-it - Coin Near Houses #3" },
            { "16_03_00065", "Bomb-it - Coin Near Houses #4" },
            { "16_03_00062", "Bomb-it - Coin Near Houses #5" },
            { "16_03_00063", "Bomb-it - Coin Near Houses #6" },
            { "16_03_00060", "Bomb-it - Coin Near Houses #7" },
            { "16_03_00061", "Bomb-it - Coin Near Houses #8" },
            { "16_03_00058", "Bomb-it - Coin Near Houses #9" },
            { "16_03_00059", "Bomb-it - Coin Near Houses #10" },
            { "16_03_00056", "Bomb-it - Coin Bag Near Houses #1" },
            { "16_03_00057", "Bomb-it - Coin Bag Near Houses #2" },
            { "16_01_00004", "Bomb-it - Gear - Bomb Car Obstacle Course" },
            { "16_01_00005", "Bomb-it - Gear - Bomb Car Ramps" },
        };

        [Description("Podium - Starting Area")]
        public static Dictionary<string, string> PodiumStartingArea = new()
        {
            { "16_09_00145", "Podium - Checkpoint" },
        };

        [Description("Podium - High Ground")]
        public static Dictionary<string, string> PodiumHighGround = new()
        {
            { "16_03_00276", "Podium - Coin by First Gear #1" },
            { "16_03_00275", "Podium - Coin by First Gear #2" },
            { "16_03_00274", "Podium - Coin by First Gear #3" },
            { "16_03_00273", "Podium - Coin Bag by First Gear" },
            { "16_01_00012", "Podium - Gear - First Gear" },
            { "16_03_00331", "Podium - Coin by Hat #1" },
            { "16_03_00330", "Podium - Coin by Hat #2" },
            { "16_03_00329", "Podium - Coin by Hat #3" },
            { "16_03_00328", "Podium - Coin Bag by Hat" },
            { "16_03_00347", "Podium - Coin by Second Gear #1" },
            { "16_03_00346", "Podium - Coin by Second Gear #2" },
            { "16_03_00345", "Podium - Coin by Second Gear #3" },
            { "16_03_00344", "Podium - Coin Bag by Second Gear" },
            { "16_01_00013", "Podium - Gear - Second Gear" },
        };

        [Description("Costipation - Starting Area")]
        public static Dictionary<string, string> CostipationStartingArea = new()
        {
            { "16_09_01910", "Costipation - Checkpoint" },
        };

        [Description("Costipation - Roadway Gear Area")]
        public static Dictionary<string, string> CostipationRoadwayGearArea = new()
        {
            { "16_03_00215", "Costipation - Coin Surrounding Roadway Gear #1" },
            { "16_03_00214", "Costipation - Coin Surrounding Roadway Gear #2" },
            { "16_03_00211", "Costipation - Coin Surrounding Roadway Gear #3" },
            { "16_03_00212", "Costipation - Coin Bag Surrounding Roadway Gear #1" },
            { "16_03_00213", "Costipation - Coin Surrounding Roadway Gear #4" },
            { "16_03_00208", "Costipation - Coin Surrounding Roadway Gear #5" },
            { "16_01_00015", "Costipation - Gear - Roadway" },
            { "16_03_00210", "Costipation - Coin Surrounding Roadway Gear #6" },
            { "16_03_00205", "Costipation - Coin Surrounding Roadway Gear #7" },
            { "16_03_00206", "Costipation - Coin Bag Surrounding Roadway Gear #2" },
            { "16_03_00207", "Costipation - Coin Surrounding Roadway Gear #8" },
            { "16_03_00204", "Costipation - Coin Surrounding Roadway Gear #9" },
            { "16_03_00203", "Costipation - Coin Surrounding Roadway Gear #10" },
        };

        [Description("Costipation - Island Gear Area")]
        public static Dictionary<string, string> CostipationIslandGearArea = new()
        {
            { "16_03_00267", "Costipation - Coin on Gear Island #1" },
            { "16_03_00264", "Costipation - Coin on Gear Island #2" },
            { "16_03_00260", "Costipation - Coin on Gear Island #3" },
            { "16_03_00266", "Costipation - Coin on Gear Island #4" },
            { "16_03_00263", "Costipation - Coin Bag on Gear Island #1" },
            { "16_03_00259", "Costipation - Coin on Gear Island #5" },
            { "16_03_00255", "Costipation - Coin Bag on Gear Island #2" },
            { "16_03_00265", "Costipation - Coin on Gear Island #6" },
            { "16_03_00262", "Costipation - Coin on Gear Island #7" },
            { "16_01_00014", "Costipation - Gear - On Island" },
            { "16_03_00254", "Costipation - Coin on Gear Island #8" },
            { "16_03_00251", "Costipation - Coin on Gear Island #9" },
            { "16_03_00261", "Costipation - Coin Bag on Gear Island #3" },
            { "16_03_00257", "Costipation - Coin on Gear Island #10" },
            { "16_03_00253", "Costipation - Coin Bag on Gear Island #4" },
            { "16_03_00250", "Costipation - Coin on Gear Island #11" },
            { "16_03_00256", "Costipation - Coin on Gear Island #12" },
            { "16_03_00252", "Costipation - Coin on Gear Island #13" },
            { "16_03_00249", "Costipation - Coin on Gear Island #14" },
        };

        [Description("Smelly Slimes - Starting Area")]
        public static Dictionary<string, string> SmellySlimesStartingArea = new()
        {
            { "16_09_01448", "Smelly Slimes - Checkpoint by Entrance" },
            { "16_01_00017", "Smelly Slimes - Gear - Pipe Above Entrance" },
        };

        [Description("Smelly Slimes - Sidebars by Entrance")]
        public static Dictionary<string, string> SmellySlimesSidebars = new()
        {
            { "16_03_00093", "Smelly Slimes - Coin Left of Entrance Portal #1" },
            { "16_03_00094", "Smelly Slimes - Coin Left of Entrance Portal #2" },
            { "16_03_00095", "Smelly Slimes - Coin Left of Entrance Portal #3" },
            { "16_03_00096", "Smelly Slimes - Coin Left of Entrance Portal #4" },
            { "16_03_00097", "Smelly Slimes - Coin Left of Entrance Portal #5" },
            { "16_03_00098", "Smelly Slimes - Chest Left of Entrance Portal" },
            { "16_03_00099", "Smelly Slimes - Coin Left of Entrance Portal #6" },
            { "16_03_00100", "Smelly Slimes - Coin Left of Entrance Portal #7" },
            { "16_03_00101", "Smelly Slimes - Coin Left of Entrance Portal #8" },
            { "16_03_00102", "Smelly Slimes - Coin Left of Entrance Portal #9" },
            { "16_03_00103", "Smelly Slimes - Coin Left of Entrance Portal #10" },
            { "16_03_00105", "Smelly Slimes - Coin Right of Entrance Portal #1" },
            { "16_03_00106", "Smelly Slimes - Coin Right of Entrance Portal #2" },
            { "16_03_00107", "Smelly Slimes - Coin Right of Entrance Portal #3" },
            { "16_03_00108", "Smelly Slimes - Coin Right of Entrance Portal #4" },
            { "16_03_00109", "Smelly Slimes - Coin Right of Entrance Portal #5" },
            { "16_03_00110", "Smelly Slimes - Chest Right of Entrance Portal" },
            { "16_03_00111", "Smelly Slimes - Coin Right of Entrance Portal #6" },
            { "16_03_00112", "Smelly Slimes - Coin Right of Entrance Portal #7" },
            { "16_03_00113", "Smelly Slimes - Coin Right of Entrance Portal #8" },
            { "16_03_00114", "Smelly Slimes - Coin Right of Entrance Portal #9" },
            { "16_03_00115", "Smelly Slimes - Coin Right of Entrance Portal #10" },
        };

        [Description("Smelly Slimes - Exit Area")]
        public static Dictionary<string, string> SmellySlimesExit = new()
        {
            { "16_01_00016", "Smelly Slimes - Gear - In Slime Near Exit" },
            { "16_03_00070", "Smelly Slimes - Coin on Left of Exit Gear #1" },
            { "16_03_00071", "Smelly Slimes - Coin on Left of Exit Gear #2" },
            { "16_03_00104", "Smelly Slimes - Coin on Left of Exit Gear #3" },
            { "16_03_00152", "Smelly Slimes - Coin on Left of Exit Gear #4" },
            { "16_03_00154", "Smelly Slimes - Coin on Left of Exit Gear #5" },
            { "16_03_00160", "Smelly Slimes - Coin Bag on Left of Exit Gear" },
            { "16_03_00072", "Smelly Slimes - Coin on Right of Exit Gear #1" },
            { "16_03_00073", "Smelly Slimes - Coin on Right of Exit Gear #2" },
            { "16_03_00116", "Smelly Slimes - Coin on Right of Exit Gear #3" },
            { "16_03_00153", "Smelly Slimes - Coin on Right of Exit Gear #4" },
            { "16_03_00155", "Smelly Slimes - Coin on Right of Exit Gear #5" },
            { "16_03_00161", "Smelly Slimes - Coin Bag on Right of Exit Gear" },
            { "16_09_00998", "Smelly Slimes - Checkpoint by Exit" },
        };

        [Description("Mosk's Rocket - Special Rules")]
        public static Dictionary<string, string> RocketSpecialRules = new()
        {
            { "Mosk's Rocket - Coin Bag on Steps to Final Floor", "J1/GP" },
            { "Bomb-it - Gear - Bomb Car Ramps", "X1/J1" },
            { "Smelly Slimes - Gear - Pipe Above Entrance", "B2 & X2/J1" },
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
            new(GetDescription(nameof(HubCrashAgainStartingArea)), HubCrashAgainStartingArea),
            new(GetDescription(nameof(HubCrashAgainEnd)), HubCrashAgainEnd),
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
            new(GetDescription(nameof(MoriosLabPsychoTaxi)), MoriosLabPsychoTaxi),
            new(GetDescription(nameof(MoriosLabSecondFloorAboveDemoWall)), MoriosLabSecondFloorAboveDemoWall),
            new(GetDescription(nameof(MoriosLabSecondFloorAfterDemoWall)), MoriosLabSecondFloorAfterDemoWall),
            new(GetDescription(nameof(MoriosLabSecondFloorInTrueDemoWall)), MoriosLabSecondFloorInTrueDemoWall),
            new(GetDescription(nameof(MoriosLabSecondFloorAfterTrueDemoWall)), MoriosLabSecondFloorAfterTrueDemoWall),
            new(GetDescription(nameof(MoriosLabSecondFloorShortcutPipe)), MoriosLabSecondFloorShortcutPipe),
            new(GetDescription(nameof(MoriosLabSecondFloorShortcutPipeFalling)), MoriosLabSecondFloorShortcutPipeFalling),
            new(GetDescription(nameof(MoriosLabPathToMoriosRoom)), MoriosLabPathToMoriosRoom),
            new(GetDescription(nameof(MoriosLabMoriosRoomOutside)), MoriosLabMoriosRoomOutside),
            new(GetDescription(nameof(MoriosLabMoriosRoomInsideJump)), MoriosLabMoriosRoomInsideJump),
            new(GetDescription(nameof(MoriosLabThirdFloor)), MoriosLabThirdFloor),
            new(GetDescription(nameof(MoriosLabThirdFloorWrenchesLower)), MoriosLabThirdFloorWrenchesLower),
            new(GetDescription(nameof(MoriosLabThirdFloorWrenchesMiddle)), MoriosLabThirdFloorWrenchesMiddle),
            new(GetDescription(nameof(MoriosLabThirdFloorWrenchesUpper)), MoriosLabThirdFloorWrenchesUpper),
            new(GetDescription(nameof(MoriosLabFourthFloor)), MoriosLabFourthFloor),
            new(GetDescription(nameof(MoriosLabFourthFloorExpertJumpSpikes)), MoriosLabFourthFloorExpertJumpSpikes),
            new(GetDescription(nameof(MoriosLabFourthFloorJumpSpikes)), MoriosLabFourthFloorJumpSpikes),
            new(GetDescription(nameof(MoriosLabLedgeAboveMauriziosCity)), MoriosLabLedgeAboveMauriziosCity),
            new(GetDescription(nameof(MoriosLabFifthFloorCrashTestArea)), MoriosLabFifthFloorCrashTestArea),
            new(GetDescription(nameof(MoriosLabFifthFloorMoriosMindArea)), MoriosLabFifthFloorMoriosMindArea),
            new(GetDescription(nameof(MoriosLabFifthFloorRuinedObservatoryArea)), MoriosLabFifthFloorRuinedObservatoryArea),
            new(GetDescription(nameof(MoriosLabFifthFloorGoldenPropeller)), MoriosLabFifthFloorGoldenPropeller),
            new(GetDescription(nameof(MoriosLabFifthFloorGoldenPropellerPassword)), MoriosLabFifthFloorGoldenPropellerPassword),
            new(GetDescription(nameof(MoriosLabFifthFloorShortcutPipe)), MoriosLabFifthFloorShortcutPipe),
            new(GetDescription(nameof(MoriosLabFifthFloorLowerLedge)), MoriosLabFifthFloorLowerLedge),
            new(GetDescription(nameof(MoriosLabFifthToSixthFloorStair)), MoriosLabFifthToSixthFloorStair),
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
            new(GetDescription(nameof(MoriosIslandFirstBunnyArch)), MoriosIslandFirstBunnyArch),
            new(GetDescription(nameof(MoriosIslandSecondBunnyArch)), MoriosIslandSecondBunnyArch),
            new(GetDescription(nameof(MoriosIslandCenterIsland)), MoriosIslandCenterIsland),
            new(GetDescription(nameof(MoriosIslandHighestGround)), MoriosIslandHighestGround),

            new(GetDescription(nameof(MoriosHomeStartingArea)), MoriosHomeStartingArea),
            new(GetDescription(nameof(MoriosHomeExpert1)), MoriosHomeExpert1),
            new(GetDescription(nameof(MoriosHomeHighGround)), MoriosHomeHighGround),
            new(GetDescription(nameof(MoriosHomeBeforeBedPillars)), MoriosHomeBeforeBedPillars),
            new(GetDescription(nameof(MoriosHomeBedPillars)), MoriosHomeBedPillars),
            new(GetDescription(nameof(MoriosHomeLoft)), MoriosHomeLoft),
            new(GetDescription(nameof(MoriosHomeKitchen)), MoriosHomeKitchen),
            new(GetDescription(nameof(MoriosHomeKitchenExpert1)), MoriosHomeKitchenExpert1),
            new(GetDescription(nameof(MoriosHomeKitchenCabinets)), MoriosHomeKitchenCabinets),
            new(GetDescription(nameof(MoriosHomeKitchenHighGround)), MoriosHomeKitchenHighGround),

            new(GetDescription(nameof(WeirdTunnelsEntrance)), WeirdTunnelsEntrance),
            new(GetDescription(nameof(WeirdTunnelsExpert1)), WeirdTunnelsExpert1),
            new(GetDescription(nameof(WeirdTunnelsHighGround)), WeirdTunnelsHighGround),

            // Bombeach Areas
            new (GetDescription(nameof(BombeachStartingArea)), BombeachStartingArea),
            new (GetDescription(nameof(BombeachEasyBombJumps)), BombeachEasyBombJumps),
            new (GetDescription(nameof(BombeachExpert1BombJumps)), BombeachExpert1BombJumps),
            new (GetDescription(nameof(BombeachJumpOrBoost)), BombeachJumpOrBoost),
            new (GetDescription(nameof(BombeachSuperboost)), BombeachSuperboost),
            new (GetDescription(nameof(BombeachOrangeBlockBridge)), BombeachOrangeBlockBridge),
            new (GetDescription(nameof(Cave)), Cave),

            // Arcade Panik Areas
            new(GetDescription(nameof(ArcadePlazaStartingArea)), ArcadePlazaStartingArea),
            new(GetDescription(nameof(ArcadePlazaOutskirts)), ArcadePlazaOutskirts),
            new(GetDescription(nameof(ArcadePanikHatWorld)), ArcadePanikHatWorld),
            new(GetDescription(nameof(ArcadePanikStartingArea)), ArcadePanikStartingArea),
            new(GetDescription(nameof(ArcadePanikExpert1Jump)), ArcadePanikExpert1Jump),

            // Gym Gears Areas
            new(GetDescription(nameof(GymGearsStartingArea)), GymGearsStartingArea),
            new(GetDescription(nameof(GymGearsExpert1)), GymGearsExpert1),
            new(GetDescription(nameof(GymGearsExpert2)), GymGearsExpert2),
            new(GetDescription(nameof(GymGearsJump)), GymGearsJump),

            // Fecal Matters Areas
            new(GetDescription(nameof(FecalMattersStartingArea)), FecalMattersStartingArea),
            new(GetDescription(nameof(FecalMattersRoundaboutHillTier1)), FecalMattersRoundaboutHillTier1),
            new(GetDescription(nameof(FecalMattersRoundaboutHillTier3)), FecalMattersRoundaboutHillTier3),
            new(GetDescription(nameof(FecalMattersBoostOrJump)), FecalMattersBoostOrJump),
            new(GetDescription(nameof(FecalMattersBoostOrBackflip)), FecalMattersBoostOrBackflip),
            new(GetDescription(nameof(FecalMattersHighestGround)), FecalMattersHighestGround),

            // Rocket Areas
            new(GetDescription(nameof(RocketStartingArea)), RocketStartingArea),
            new(GetDescription(nameof(RocketBombeachArcadePizzaArea)), RocketBombeachArcadePizzaArea),
            new(GetDescription(nameof(RocketToslaOfficesArea)), RocketToslaOfficesArea),
            new(GetDescription(nameof(RocketGymGearsArea)), RocketGymGearsArea),
            new(GetDescription(nameof(RocketFecalFlushedArea)), RocketFecalFlushedArea),
            new(GetDescription(nameof(RocketCityCrashMindArea)), RocketCityCrashMindArea),
            new(GetDescription(nameof(RocketCoinsToFinalFloor)), RocketCoinsToFinalFloor),
            new(GetDescription(nameof(RocketToslaHQArea)), RocketToslaHQArea),
            new(GetDescription(nameof(RocketFinalFloorCheese)), RocketFinalFloorCheese),
            new(GetDescription(nameof(RocketMoonArea)), RocketMoonArea),
            new(GetDescription(nameof(LabMemoriesStartingArea)), LabMemoriesStartingArea),
            new(GetDescription(nameof(LabMemoriesFirstStep)), LabMemoriesFirstStep),
            new(GetDescription(nameof(LabMemoriesHighGround)), LabMemoriesHighGround),
            new(GetDescription(nameof(WelcomingClimbsStartingArea)), WelcomingClimbsStartingArea),
            new(GetDescription(nameof(WelcomingClimbsFirstGearArea)), WelcomingClimbsFirstGearArea),
            new(GetDescription(nameof(WelcomingClimbsSecondGearArea)), WelcomingClimbsSecondGearArea),
            new(GetDescription(nameof(BombitStartingArea)), BombitStartingArea),
            new(GetDescription(nameof(BombitOuterArea)), BombitOuterArea),
            new(GetDescription(nameof(PodiumStartingArea)), PodiumStartingArea),
            new(GetDescription(nameof(PodiumHighGround)), PodiumHighGround),
            new(GetDescription(nameof(CostipationStartingArea)), CostipationStartingArea),
            new(GetDescription(nameof(CostipationRoadwayGearArea)), CostipationRoadwayGearArea),
            new(GetDescription(nameof(CostipationIslandGearArea)), CostipationIslandGearArea),
            new(GetDescription(nameof(SmellySlimesStartingArea)), SmellySlimesStartingArea),
            new(GetDescription(nameof(SmellySlimesSidebars)), SmellySlimesSidebars),
            new(GetDescription(nameof(SmellySlimesExit)), SmellySlimesExit),
        ];

        public static Dictionary<string, Dictionary<string, string>> SpecialRules = new()
        {
            { nameof(Data.LevelId.Hub), HubSpecialRules },
            { nameof(Data.LevelId.L3_MoriosHome), MoriosIslandSpecialRules },
            { nameof(Data.LevelId.L4_ArcadePanik), ArcadePanikSpecialRules },
            { nameof(Data.LevelId.L6_Gym), GymGearsSpecialRules },
            { nameof(Data.LevelId.L7_PoopWorld), FecalMattersSpecialRules },
            { nameof(Data.LevelId.L16_Rocket), RocketSpecialRules },
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
                    HubCrashAgainStartingArea,
                    HubCrashAgainEnd,
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
                    MoriosLabPsychoTaxi,
                    MoriosLabSecondFloorAboveDemoWall,
                    MoriosLabSecondFloorAfterDemoWall,
                    MoriosLabSecondFloorInTrueDemoWall,
                    MoriosLabSecondFloorAfterTrueDemoWall,
                    MoriosLabSecondFloorShortcutPipeFalling,
                    MoriosLabSecondFloorShortcutPipe,
                    MoriosLabPathToMoriosRoom,
                    MoriosLabMoriosRoomOutside,
                    MoriosLabMoriosRoomInsideJump,
                    MoriosLabThirdFloor,
                    MoriosLabThirdFloorWrenchesLower,
                    MoriosLabThirdFloorWrenchesMiddle,
                    MoriosLabThirdFloorWrenchesUpper,
                    MoriosLabFourthFloor,
                    MoriosLabFourthFloorExpertJumpSpikes,
                    MoriosLabFourthFloorJumpSpikes,
                    MoriosLabLedgeAboveMauriziosCity,
                    MoriosLabFifthFloorCrashTestArea,
                    MoriosLabFifthFloorMoriosMindArea,
                    MoriosLabFifthFloorRuinedObservatoryArea,
                    MoriosLabFifthFloorGoldenPropeller,
                    MoriosLabFifthFloorGoldenPropellerPassword,
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
                    MoriosIslandFirstHurdle,
                    MoriosIslandHighGround,
                    MoriosIslandHomeIsland,
                    MoriosIslandLowStoneArch,
                    MoriosIslandFirstBunnyArch,
                    MoriosIslandSecondBunnyArch,
                    MoriosIslandCenterIsland,
                    MoriosIslandHighestGround,

                    MoriosHomeStartingArea,
                    MoriosHomeExpert1,
                    MoriosHomeHighGround,
                    MoriosHomeBeforeBedPillars,
                    MoriosHomeBedPillars,
                    MoriosHomeLoft,
                    MoriosHomeKitchen,
                    MoriosHomeKitchenExpert1,
                    MoriosHomeKitchenCabinets,
                    MoriosHomeKitchenHighGround,

                    WeirdTunnelsEntrance,
                    WeirdTunnelsExpert1,
                    WeirdTunnelsHighGround,
                ]
            },
            {
                nameof(Data.LevelId.L4_ArcadePanik),
                [
                    ArcadePlazaStartingArea,
                    ArcadePlazaOutskirts,

                    ArcadePanikHatWorld,

                    ArcadePanikStartingArea,
                    ArcadePanikExpert1Jump,
                ]
            },
            {
                nameof(Data.LevelId.L6_Gym),
                [
                    GymGearsStartingArea,
                    GymGearsExpert1,
                    GymGearsExpert2,
                    GymGearsJump,
                ]
            },
            {
                nameof(Data.LevelId.L7_PoopWorld),
                [
                    FecalMattersStartingArea,
                    FecalMattersRoundaboutHillTier1,
                    FecalMattersRoundaboutHillTier3,
                    FecalMattersBoostOrJump,
                    FecalMattersBoostOrBackflip,
                    FecalMattersHighestGround,
                ]
            },
            {
                nameof(Data.LevelId.L1_Bombeach),
                [
                    BombeachStartingArea,
                    BombeachEasyBombJumps,
                    BombeachExpert1BombJumps,
                    BombeachJumpOrBoost,
                    BombeachSuperboost,
                    BombeachOrangeBlockBridge,
                    Cave,
                ]
            },
            {
                nameof(Data.LevelId.L16_Rocket),
                [
                    RocketStartingArea,
                    RocketBombeachArcadePizzaArea,
                    RocketToslaOfficesArea,
                    RocketGymGearsArea,
                    RocketFecalFlushedArea,
                    RocketCityCrashMindArea,
                    RocketCoinsToFinalFloor,
                    RocketToslaHQArea,
                    RocketFinalFloorCheese,
                    RocketMoonArea,
                    LabMemoriesStartingArea,
                    LabMemoriesFirstStep,
                    LabMemoriesHighGround,
                    WelcomingClimbsStartingArea,
                    WelcomingClimbsFirstGearArea,
                    WelcomingClimbsSecondGearArea,
                    BombitStartingArea,
                    BombitOuterArea,
                    PodiumStartingArea,
                    PodiumHighGround,
                    CostipationStartingArea,
                    CostipationRoadwayGearArea,
                    CostipationIslandGearArea,
                    SmellySlimesStartingArea,
                    SmellySlimesSidebars,
                    SmellySlimesExit,
                ]
            }
        };

        public enum ConnectionType
        {
            Connection,
            Subwarp,
            Warp,
            MoriOTron,
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
                    new RegionConnection(GrannysIslandBrokenPierShallow, "X1+B1 | J1 | GP"),
                    new RegionConnection(GrannysIslandBrokenPierDeep, "X1+B2 | X1/B1 & J1 | GP | J2"),
                    new RegionConnection(GrannysIslandBrokenPierExtraDeep, "X1/B1 & J1 | GP | J2"),
                    new RegionConnection(GrannysIslandExpert1HighGround, "X1/J1/B1/GP"),
                    new RegionConnection(GrannysIslandExpert2HighGround, "X2/J1/B1/GP"),
                    new RegionConnection(GrannysIslandHighGround, "J1/B1/GP"),
                    new RegionConnection(GrannysIslandRocketTop, "Rocket+B2 | Rocket+B1 & X1/GP"),
                    new RegionConnection(GrannysIslandConstructionArch, "B1 | GP+J1"),
                    new RegionConnection(GrannysIslandOceanPillar, "B1 | GP+OS"),
                    new RegionConnection(GrannysIslandTowardsSewerIsland1, "B1 | X2/J1 & GP+OS"),
                    new RegionConnection(GrannysIslandTowardsSewerIsland2, "B1 & OS/GP/J1"),
                    new RegionConnection(GrannysIslandCloroPhilIsland, "GP | B2 | X1 & B1/J2"),
                    new RegionConnection(GrannysIslandHighPillarByLab, "X1/GP/J1 & B1"),
                    new RegionConnection(GrannysIslandCrashAgainIsland, "X1+B2+GP"),
                    new RegionConnection(GrannysIslandCrashAgainRoof, "X2+B2+GP | GP & OS & J2/B1"),
                    new RegionConnection("Granny's Island - Law Firm Roof Entrance", HubLawFirm, ConnectionType.Subwarp),
                    new RegionConnection("Granny's Island - Pizza Oven Entrance", HubPizzaOven, ConnectionType.Subwarp, "PizzaKing"),
                    new RegionConnection("Granny's Island - Ice Cream Truck Entrance", HubIceCreamTruckBase, ConnectionType.Subwarp, "GelaToni"),
                    new RegionConnection("Granny's Island - Hat World Entrance", HubHatWorld, ConnectionType.Subwarp),
                    new RegionConnection("Granny's Island - Gym Gears Entrance", GymGearsStartingArea, ConnectionType.Warp, "PortalGymGears"),
                    new RegionConnection(GrannysIslandSewerIsland, "X1+B2+J2+GP"),
                    new RegionConnection("Granny's Island - Poop House", FecalMattersStartingArea, ConnectionType.Warp, "Doggo"),
                    new RegionConnection("Granny's Island - Mosk's Rocket Entrance", RocketStartingArea, ConnectionType.Warp, "Rocket & J2/B1/GP | X1+Rocket+J1"),
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
                    new RegionConnection(GrannysIslandLabHillExpert1HighGround),
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
                    new RegionConnection(GrannysIslandLabHillHighGround)
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
                    new RegionConnection("Crash Again - Entrance", HubCrashAgainStartingArea, ConnectionType.Subwarp, "FGU"),
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
                GetDescription(nameof(HubCrashAgainStartingArea)),
                [
                    new RegionConnection(HubCrashAgainEnd, "X1/B1 & X2/SP"),
                    new RegionConnection("Crash Again - Exit", GrannysIslandMain, ConnectionType.Subwarp),
                ]
            },
            {
                GetDescription(nameof(HubCrashAgainEnd)),
                [
                    // Doesn't connect anywhere
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
                    new RegionConnection(HubPizzaOvenPillar, "X1/J1 & B2"),
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
                    new RegionConnection(MoriosLabFifthFloorCrashTestArea, "X2+B2+FGU | B2+J2+FGU"),
                    new RegionConnection("Morio's Lab - Morio's Home Portal", MoriosIslandStartingArea, ConnectionType.Warp, "PortalMorioHome"),
                    new RegionConnection("Morio's Lab - Bombeach Portal", BombeachStartingArea, ConnectionType.Warp, "PortalBombeach"),
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
                    new RegionConnection(MoriosLabGroundFloorOrangeBlocks, "X1+B1"),
                    new RegionConnection(MoriosLabGroundFloorBolts, "X1+B1"),
                ]
            },
            {
                GetDescription(nameof(MoriosLabGroundFloorBolts)),
                [
                    new RegionConnection(MoriosLabGroundFloorLowestBolt),
                    new RegionConnection(MoriosLabBunnyLedge, "J1 | X1+B1"),
                    new RegionConnection(MoriosLabPathToMoriosRoom, "X1+J2"),
                    new RegionConnection(MoriosLabFifthFloorCrashTestArea, "X1+J1+B2+FGU"),
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
                    new RegionConnection("Morio's Lab - Arcade Panik Portal", ArcadePlazaStartingArea, ConnectionType.Warp, "PortalArcadePanik"),
                    new RegionConnection(MoriosLabPathToMoriosRoom, "B1"),
                    new RegionConnection(MoriosLabGroundFloor),
                    new RegionConnection(MoriosLabSecondFloorShortcutPipe, "X2+B1+J1 | X1+B2+J2"),
                    new RegionConnection(MoriosLabSecondFloorAboveDemoWall, "B2"),
                    new RegionConnection(MoriosLabSecondFloorAfterDemoWall, "FGU"),
                    new RegionConnection(MoriosLabPsychoTaxi, "FGU"),
                ]
            },
            {
                GetDescription(nameof(MoriosLabPsychoTaxi)),
                [
                    new RegionConnection(MoriosLabSecondFloor, "FGU"),
                    // TODO: Connect to Psycho Taxi region whenever it becomes logically relevant
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
                    new RegionConnection(MoriosLabMoriosRoomOutside, "B1"),
                    new RegionConnection(MoriosLabGroundFloorBolts, "B1"),
                    new RegionConnection(MoriosLabGroundFloorWrenches),
                    new RegionConnection(MoriosLabThirdFloorWrenchesLower, "FGU+B2"),
                    new RegionConnection(MoriosLabThirdFloorWrenchesMiddle, "FGU+B2"),
                    new RegionConnection(MoriosLabThirdFloorWrenchesUpper, "FGU+B2"),
                    new RegionConnection(MoriosLabSecondFloorAboveDemoWall, "J2"),
                ]
            },
            {
                GetDescription(nameof(MoriosLabMoriosRoomOutside)),
                [
                    new RegionConnection(MoriosLabMoriosRoomInsideJump, "MorioHat+J1")
                ]
            },
            {
                GetDescription(nameof(MoriosLabMoriosRoomInsideJump)),
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
                    new RegionConnection(MoriosLabLedgeAboveMauriziosCity, "J1"),
                    new RegionConnection(MoriosLabThirdFloor, "GS"),
                    new RegionConnection(MoriosLabFourthFloorExpertJumpSpikes, "GS+J1 | X1+GS+B2 | X2+B2+J2"),
                ]
            },
            {
                GetDescription(nameof(MoriosLabFourthFloorExpertJumpSpikes)),
                [
                    // Doesn't connect anywhere
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
                    new RegionConnection(MoriosLabFifthFloorLowerLedge, "B2 | X1/J1 & B1 | J2 | X1+J1"),
                    new RegionConnection(MoriosLabFifthFloorMoriosMindArea, "Password"),
                ]
            },
            {
                GetDescription(nameof(MoriosLabFifthFloorGoldenPropeller)),
                [
                    new RegionConnection(MoriosLabFifthToSixthFloorStair),
                    new RegionConnection(MoriosLabFifthFloorHighPillars),
                    new RegionConnection(MoriosLabFinalFloor),
                    new RegionConnection(MoriosLabFifthFloorGoldenPropellerPassword, "Password"),
                ]
            },
            {
                GetDescription(nameof(MoriosLabFifthFloorGoldenPropellerPassword)),
                [
                    new RegionConnection(MoriosLabFifthFloorShortcutPipe),
                    new RegionConnection(MoriosLabPathToMoriosRoom, "FGU"),
                    new RegionConnection(MoriosLabMoriosRoomOutside, "FGU"),
                    new RegionConnection(MoriosLabMoriosRoomInsideJump, "FGU+MorioHat"),
                    new RegionConnection(MoriosLabThirdFloorWrenchesLower),
                    new RegionConnection(MoriosLabThirdFloorWrenchesMiddle, "FGU"),
                    new RegionConnection(MoriosLabGroundFloorBolts),
                    new RegionConnection(MoriosLabSecondFloorShortcutPipe),
                    new RegionConnection(MoriosLabLedgeAboveMauriziosCity),
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
                    new RegionConnection(MoriosIslandFirstBunnyArch, "X1/J1 & B1"),
                    new RegionConnection(MoriosIslandSecondBunnyArch, "B2"),
                    new RegionConnection(MoriosIslandCenterIsland, "B1 & J1/X2 | J2+X1"),
                    new RegionConnection(MoriosIslandHighestGround, "B2 | J2+X1"),
                ]
            },
            {
                GetDescription(nameof(MoriosIslandHomeIsland)),
                [
                    new RegionConnection(MoriosIslandStartingArea, "J1/B1"),
                    new RegionConnection(MoriosIslandLowStoneArch, "B1 | X1+J2"),
                    new RegionConnection("Morio's Island - Morio's Garage", MoriosHomeStartingArea, ConnectionType.Subwarp),
                ]
            },
            {
                GetDescription(nameof(MoriosIslandLowStoneArch)),
                [
                    // Doesn't connect anywhere
                ]
            },
            {
                GetDescription(nameof(MoriosIslandFirstBunnyArch)),
                [
                    // Doesn't connect anywhere
                ]
            },
            {
                GetDescription(nameof(MoriosIslandSecondBunnyArch)),
                [
                    // Doesn't connect anywhere
                ]
            },
            {
                GetDescription(nameof(MoriosIslandCenterIsland)),
                [
                    // Doesn't connect anywhere
                ]
            },
            {
                GetDescription(nameof(MoriosIslandHighestGround)),
                [
                    // Doesn't connect anywhere
                ]
            },
            {
                GetDescription(nameof(MoriosHomeStartingArea)),
                [
                    new RegionConnection(MoriosHomeExpert1, "X1/J1/B1"),
                    new RegionConnection(MoriosHomeHighGround, "J1/B1"),
                    new RegionConnection(MoriosHomeLoft, "J2 | B2+J1"),
                    new RegionConnection(MoriosHomeKitchen, "SP"),
                    new RegionConnection("Morio's Home - Door in Bedroom", WeirdTunnelsEntrance, ConnectionType.Subwarp),
                ]
            },
            {
                GetDescription(nameof(MoriosHomeExpert1)),
                [
                    // Doesn't connect anywhere
                ]
            },
            {
                GetDescription(nameof(MoriosHomeHighGround)),
                [
                    new RegionConnection(MoriosHomeBeforeBedPillars),
                ]
            },
            {
                GetDescription(nameof(MoriosHomeBeforeBedPillars)),
                [
                    new RegionConnection(MoriosHomeStartingArea),
                    new RegionConnection(MoriosHomeBedPillars, "B1 & X1/J1"),
                ]
            },
            {
                GetDescription(nameof(MoriosHomeBedPillars)),
                [
                    new RegionConnection("Morio's Home - Portal to Morio's Lab", MoriosLabGroundFloor, ConnectionType.Warp),
                ]
            },
            {
                GetDescription(nameof(MoriosHomeLoft)),
                [
                    new RegionConnection(MoriosHomeKitchen),
                ]
            },
            {
                GetDescription(nameof(MoriosHomeKitchen)),
                [
                    new RegionConnection(MoriosHomeKitchenExpert1, "X1/B1/J1"),
                    new RegionConnection(MoriosHomeKitchenCabinets, "B1/J1"),
                    new RegionConnection(MoriosHomeKitchenHighGround, "J1 | X1+B1"),
                ]
            },
            {
                GetDescription(nameof(MoriosHomeKitchenExpert1)),
                [
                    // Doesn't connect anywhere
                ]
            },
            {
                GetDescription(nameof(MoriosHomeKitchenCabinets)),
                [
                    // Doesn't connect anywhere
                ]
            },
            {
                GetDescription(nameof(MoriosHomeKitchenHighGround)),
                [
                    // Doesn't connect anywhere
                ]
            },
            {
                GetDescription(nameof(WeirdTunnelsEntrance)),
                [
                    new RegionConnection(WeirdTunnelsExpert1, "X1/B1/J1"),
                    new RegionConnection(WeirdTunnelsHighGround, "B1/J1"),
                ]
            },
            {
                GetDescription(nameof(WeirdTunnelsExpert1)),
                [
                    // Doesn't connect anywhere
                ]
            },
            {
                GetDescription(nameof(WeirdTunnelsHighGround)),
                [
                    // Doesn't connect anywhere
                ]
            },

            #endregion

            #region Bombeach Connections

            {
                GetDescription(nameof(BombeachStartingArea)),
                [
                    new RegionConnection("Bombeach - Portal to Morio's Lab", MoriosLabGroundFloor, ConnectionType.Warp),
                    new RegionConnection(BombeachEasyBombJumps),
                    new RegionConnection(BombeachExpert1BombJumps, "X1"),
                    new RegionConnection(BombeachJumpOrBoost, "J1/B1"),
                    new RegionConnection(BombeachSuperboost, "B2"),
                    new RegionConnection("Bombeach - Cave Entrance", Cave, ConnectionType.Subwarp),
                ]
            },
            {
                GetDescription(nameof(BombeachEasyBombJumps)),
                [
                    // Doesn't connect anywhere
                ]
            },
            {
                GetDescription(nameof(BombeachExpert1BombJumps)),
                [
                    // Doesn't connect anywhere
                    new RegionConnection(BombeachOrangeBlockBridge, "OS"),
                ]
            },
            {
                GetDescription(nameof(BombeachJumpOrBoost)),
                [
                    new RegionConnection(BombeachExpert1BombJumps),
                    new RegionConnection(BombeachOrangeBlockBridge),
                ]
            },
            {
                GetDescription(nameof(BombeachSuperboost)),
                [
                    // Doesn't connect anywhere
                ]
            },
            {
                GetDescription(nameof(BombeachOrangeBlockBridge)),
                [
                    // Doesn't connect anywhere
                ]
            },
            {
                GetDescription(nameof(Cave)),
                [
                    new RegionConnection("Cave - Cave Exit", BombeachStartingArea, ConnectionType.Subwarp),
                ]
            },

            #endregion

            #region Arcade Panik Connections

            {
                GetDescription(nameof(ArcadePlazaStartingArea)),
                [
                    new RegionConnection(ArcadePlazaOutskirts, "J1"),
                    new RegionConnection("Arcade Plaza - Hat World Entrance", ArcadePanikHatWorld, ConnectionType.Subwarp),
                    new RegionConnection("Arcade Plaza - Arcade Panik Entrance", ArcadePanikStartingArea, ConnectionType.Subwarp),
                ]
            },
            {
                GetDescription(nameof(ArcadePlazaOutskirts)),
                [
                    // Doesn't connect anywhere
                ]
            },
            {
                GetDescription(nameof(ArcadePanikHatWorld)),
                [
                    new RegionConnection("Arcade Panik Hat World - Exit", ArcadePlazaStartingArea, ConnectionType.Subwarp),
                ]
            },
            {
                GetDescription(nameof(ArcadePanikStartingArea)),
                [
                    new RegionConnection("Arcade Panik - Exit", ArcadePlazaStartingArea, ConnectionType.Subwarp),
                    new RegionConnection(ArcadePanikExpert1Jump, "X1/J1"),
                ]
            },

            #endregion

            #region Gym Gears Connections

            {
                GetDescription(nameof(GymGearsStartingArea)),
                [
                    new RegionConnection("Gym Gears - Granny's Island Portal", GrannysIslandMain, ConnectionType.Warp),
                    new RegionConnection(GymGearsExpert1 , "X1/J1"),
                    new RegionConnection(GymGearsExpert2 , "X2/J1"),
                    new RegionConnection(GymGearsJump, "J1"),
                ]
            },
            {
                GetDescription(nameof(GymGearsExpert1)),
                [
                    // Doesn't connect anywhere
                ]
            },
            {
                GetDescription(nameof(GymGearsExpert2)),
                [
                    // Doesn't connect anywhere
                ]
            },
            {
                GetDescription(nameof(GymGearsJump)),
                [
                    // Doesn't connect anywhere
                ]
            },

            #endregion

            #region Fecal Matters Connections

            {
                GetDescription(nameof(FecalMattersStartingArea)),
                [
                    new RegionConnection("Fecal Matters - Roundabout Portal to Granny's Island", GrannysIslandMain, ConnectionType.Warp),
                    new RegionConnection(FecalMattersRoundaboutHillTier1, "J1/B1 | X1+NHPR"),
                    new RegionConnection(FecalMattersRoundaboutHillTier3, "J1/B2"),
                    new RegionConnection(FecalMattersBoostOrJump, "J1/B1"),
                    new RegionConnection(FecalMattersBoostOrBackflip, "J2/B1"),
                ]
            },
            {
                GetDescription(nameof(FecalMattersRoundaboutHillTier1)),
                [
                    // Doesn't connect anywhere
                ]
            },
            {
                GetDescription(nameof(FecalMattersRoundaboutHillTier3)),
                [
                    // Doesn't connect anywhere
                ]
            },
            {
                GetDescription(nameof(FecalMattersBoostOrJump)),
                [
                    // Doesn't connect anywhere
                ]
            },
            {
                GetDescription(nameof(FecalMattersBoostOrBackflip)),
                [
                    new RegionConnection(FecalMattersHighestGround, "J1 | X1+B2"),
                ]
            },
            {
                GetDescription(nameof(FecalMattersHighestGround)),
                [
                    // Doesn't connect anywhere
                ]
            },

            #endregion

            #region Rocket Connections

            {
                GetDescription(nameof(RocketStartingArea)),
                [
                    new RegionConnection("Mosk's Rocket - Front Door", GrannysIslandMain, ConnectionType.Warp),
                    new RegionConnection(RocketBombeachArcadePizzaArea, "J1/B1"),
                    new RegionConnection("Mosk's Rocket - Lab Memories Portal", LabMemoriesStartingArea, ConnectionType.Subwarp, "Bunny-Hub"),
                    new RegionConnection("Mosk's Rocket - Welcoming Climbs Portal", WelcomingClimbsStartingArea, ConnectionType.Subwarp, "Bunny-MH"),
                ]
            },
            {
                GetDescription(nameof(RocketBombeachArcadePizzaArea)),
                [
                    new RegionConnection(RocketStartingArea),
                    new RegionConnection(RocketToslaOfficesArea, "J1 | X2+B2"),
                    new RegionConnection("Mosk's Rocket - Bomb-it Portal", BombitStartingArea, ConnectionType.Subwarp, "Bunny-BB"),
                ]
            },
            {
                GetDescription(nameof(RocketToslaOfficesArea)),
                [
                    new RegionConnection(RocketBombeachArcadePizzaArea),
                    new RegionConnection(RocketGymGearsArea, "J1"),
                ]
            },
            {
                GetDescription(nameof(RocketGymGearsArea)),
                [
                    new RegionConnection(RocketBombeachArcadePizzaArea),
                    new RegionConnection(RocketFecalFlushedArea, "B1/J2"),
                    new RegionConnection("Mosk's Rocket - Podium Portal", PodiumStartingArea, ConnectionType.Subwarp, "Bunny-GG"),
                ]
            },
            {
                GetDescription(nameof(RocketFecalFlushedArea)),
                [
                    new RegionConnection(RocketGymGearsArea),
                    new RegionConnection(RocketCityCrashMindArea, "B1 | X1+J2"),
                    new RegionConnection("Mosk's Rocket - Costipation Portal", CostipationStartingArea, ConnectionType.Subwarp, "J1/B2 & Bunny-FM"),
                    new RegionConnection("Mosk's Rocket - Smelly Slimes Portal", SmellySlimesStartingArea, ConnectionType.Subwarp, "Bunny-FA"),
                ]
            },
            {
                GetDescription(nameof(RocketCityCrashMindArea)),
                [
                    new RegionConnection(RocketFecalFlushedArea),
                    new RegionConnection(RocketCoinsToFinalFloor, "J1 | X1+B2"),
                    new RegionConnection(RocketToslaHQArea, "GP/J2"),
                ]
            },
            {
                GetDescription(nameof(RocketCoinsToFinalFloor)),
                [
                    new RegionConnection(RocketCityCrashMindArea),
                ]
            },
            {
                GetDescription(nameof(RocketToslaHQArea)),
                [
                    new RegionConnection(RocketCoinsToFinalFloor),
                    new RegionConnection(RocketFinalFloorCheese, "B1/J2 | X1+J1"),
                ]
            },
            {
                GetDescription(nameof(RocketFinalFloorCheese)),
                [
                    new RegionConnection(RocketToslaHQArea),
                    new RegionConnection(RocketMoonArea, "B2"),
                ]
            },
            {
                GetDescription(nameof(RocketMoonArea)),
                [
                    new RegionConnection(RocketFinalFloorCheese),
                ]
            },
            {
                GetDescription(nameof(LabMemoriesStartingArea)),
                [
                    new RegionConnection("Lab Memories - Mosk's Rocket Portal", RocketStartingArea, ConnectionType.Subwarp),
                    new RegionConnection(LabMemoriesFirstStep, "J1+GS"),
                ]
            },
            {
                GetDescription(nameof(LabMemoriesFirstStep)),
                [
                    new RegionConnection(LabMemoriesHighGround, "J2"),
                ]
            },
            {
                GetDescription(nameof(LabMemoriesHighGround)),
                [
                    // No connections
                ]
            },
            {
                GetDescription(nameof(WelcomingClimbsStartingArea)),
                [
                    new RegionConnection("Welcoming Climbs - Mosk's Rocket Portal", RocketStartingArea, ConnectionType.Subwarp),
                    new RegionConnection(WelcomingClimbsFirstGearArea, "B2 & X1/J1")
                ]
            },
            {
                GetDescription(nameof(WelcomingClimbsFirstGearArea)),
                [
                    new RegionConnection(WelcomingClimbsSecondGearArea, "B2+J1"),
                ]
            },
            {
                GetDescription(nameof(WelcomingClimbsSecondGearArea)),
                [
                    // No connections
                ]
            },
            {
                GetDescription(nameof(BombitStartingArea)),
                [
                    new RegionConnection(BombitOuterArea, "J1/B1"),
                ]
            },
            {
                GetDescription(nameof(BombitOuterArea)),
                [
                    new RegionConnection("Bomb-it - Mosk's Rocket Portal", RocketBombeachArcadePizzaArea, ConnectionType.Subwarp),
                ]
            },
            {
                GetDescription(nameof(PodiumStartingArea)),
                [
                    new RegionConnection(PodiumHighGround, "X2+J1 | J2"),
                    new RegionConnection("Podium - Mosk's Rocket Portal", RocketGymGearsArea, ConnectionType.Subwarp),
                ]
            },
            {
                GetDescription(nameof(PodiumHighGround)),
                [
                    // No connections
                ]
            },
            {
                GetDescription(nameof(CostipationStartingArea)),
                [
                    new RegionConnection("Costipation - Mosk's Rocket Portal", RocketFecalFlushedArea, ConnectionType.Subwarp),
                    new RegionConnection(CostipationRoadwayGearArea, "J1/B1"),
                    new RegionConnection(CostipationIslandGearArea, "B2"),
                ]
            },
            {
                GetDescription(nameof(CostipationRoadwayGearArea)),
                [
                    // No connections
                ]
            },
            {
                GetDescription(nameof(CostipationIslandGearArea)),
                [
                    // No connections
                ]
            },
            {
                GetDescription(nameof(SmellySlimesStartingArea)),
                [
                    new RegionConnection("Smelly Slimes - Entrance Mosk's Rocket Portal", RocketFecalFlushedArea, ConnectionType.Subwarp),
                    new RegionConnection(SmellySlimesSidebars, "X2/J1/B1"),
                    new RegionConnection(SmellySlimesExit, "B1+J1"),
                ]
            },
            {
                GetDescription(nameof(SmellySlimesSidebars)),
                [
                    // No connections
                ]
            },
            {
                GetDescription(nameof(SmellySlimesExit)),
                [
                    // No connections
                ]
            },

            #endregion
        };

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
