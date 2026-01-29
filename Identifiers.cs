namespace YellowTaxiAP
{
    public static class Identifiers
    {
        public const int PASSWORD_ID = 0; // Only used by one level in vanilla, will never be used for level 0 so good fit
        public const int GEAR_ID = 1;
        public const int BUNNY_ID = 2;
        public const int COIN_ID = 3;
        public const int COINBAG_ID = 3;
        public const int CHEST_ID = 3;
        public const int SAFE_ID = 3;
        public const int HAT_ID = 7;
        public const int NPC_ID = 8;
        public const int CHECKPOINT_ID = 9;
        public const int PSYCHO_ID = 20;
        public const int CHEESE_ID = 21;

        // Move rando IDs
        public const int BOOST_ID = 1;
        public const int SUPERBOOST_ID = 2;
        public const int JUMP_ID = 3;
        public const int BACKFLIP_ID = 4;
        public const int SPIN_ID = 5;
        public const int GLIDE_ID = 6;

        public enum ItemID
        {
            Gear = 1,
            Bunny = 2,
            BunnyMoriosLab = 2_00,
            BunnyBombeach = 2_01,
            BunnyPizzaTime = 2_02,
            BunnyMoriosHome = 2_03,
            BunnyArcadePanik = 2_04,
            BunnyToslasOffices = 2_05,
            BunnyGymGears = 2_06,
            BunnyFecalMatters = 2_07,
            BunnyFlushedAway = 2_08,
            BunnyMauriziosCity = 2_09,
            BunnyCrashTestIndustries = 2_10,
            BunnyDemo = 2_11,
            BunnyMoriosMind = 2_12,
            BunnyRuinedObservatory = 2_13,
            BunnyToslaHQ = 2_14,
            BunnyMoon = 2_15,
            Coin1 = 3,
            Coins10 = 4, 
            Coins25 = 5,
            Coins100 = 6,
            FlipOWill = 8_0_0,
            ProgressiveJump = 8_0_1,
            ProgressiveBoost = 8_0_2,
            SpinAttack = 8_0_3,
            Glide = 8_0_4,
            GoldenSpringUnlock = 8_1_0,
            GoldenPropellerUnlock = 8_2_0,
            GelaToni = 9_01,
            PizzaKing = 9_02,
            Doggo = 9_07,
            OrangeSwitch = 9_10,
            FullGameUnlock = 9_11,
            MoriosPassword = 9_12,
            MosksRocket = 9_16,
            PsychoTaxiCartridge = 20_01,
            Michele = 20_02,
        }
}
}
