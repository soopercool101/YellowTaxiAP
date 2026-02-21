using System;
using System.Linq;

namespace YellowTaxiAP.Archipelago
{
    public abstract class YTGVAPSaveData<T>
    {
        public T SaveData;

        public bool NeedsSave;
        public bool NeedsLoad;

        public YTGVAPSaveData(T save)
        {
            SaveData = save;
        }
    }

    public abstract class YTGVSaveULong : YTGVAPSaveData<ulong>
    {
        public YTGVSaveULong(ulong save) : base(save) { }

        protected bool GetBit(int bit)
        {
            return (SaveData & ((ulong)1 << bit)) != 0;
        }

        protected void SetBit(int bit, bool value = true)
        {
            if (value == GetBit(bit))
                return;
            if (value)
                SaveData |= (ulong)1 << bit;
            else
                SaveData &= ~((ulong)1 << bit);
            NeedsSave = true;
        }
    }
    public abstract class YTGVSaveUInt : YTGVAPSaveData<uint>
    {
        public YTGVSaveUInt(uint save) : base(save) { }

        protected bool GetBit(int bit)
        {
            return (SaveData & ((uint)1 << bit)) != 0;
        }

        protected void SetBit(int bit, bool value = true)
        {
            if (value == GetBit(bit))
                return;
            if (value)
                SaveData |= (uint)1 << bit;
            else
                SaveData &= ~((uint)1 << bit);
            NeedsSave = true;
        }
    }


    public class YTGVHatSave : YTGVSaveULong
    {
        public YTGVHatSave(ulong save) : base(save) { }

        public bool GetHatUnlocked(Data.Hat hat)
        {
            return GetBit((int)hat);
        }

        public void SetHatUnlocked(Data.Hat hat, bool value = true)
        {
            SetBit((int)hat, value);
        }
    }

    public class YTGVBunnySave : YTGVSaveULong
    {
        public YTGVBunnySave(ulong save) : base(save) { }

        public bool HasBunny(Data.LevelId level, int bunnyIndex)
        {
            if (level == Data.LevelId.noone)
                return false;

            if (level == Data.LevelId.Hub)
            {
                return bunnyIndex > 2 ?
                    HasBunny(Data.LevelId.L11_HubDemo, bunnyIndex - 3) : GetBit(bunnyIndex);
            }

            //Plugin.Log($"Getting bunny: {GameplayMaster.instance.levelId} {bunnyIndex} {SaveData:X16}");
            // 3 bunnies per level
            return GetBit((int) level * 3 + bunnyIndex);
        }

        public int GetBunnyTotal()
        {
            return Enum.GetValues(typeof(Data.LevelId)).Cast<Data.LevelId>().Sum(GetBunnyCount);
        }

        public int GetBunnyCount(Data.LevelId level)
        {
            if (level is Data.LevelId.noone or Data.LevelId.L11_HubDemo)
                return 0;

            var count = 0;
            // 3 bunnies per level
            for (var i = 0; i < 3; i++)
            {
                if(HasBunny(level, i))
                    count++;
            }

            if (level == Data.LevelId.Hub)
            {
                // Also include demo bunnies
                for (var i = 0; i < 3; i++)
                {
                    if (HasBunny(Data.LevelId.L11_HubDemo, i))
                        count++;
                }
            }

            return count;
        }

        public void SetBunny(Data.LevelId level, int bunnyIndex, bool value = true)
        {
            if (level == Data.LevelId.noone)
                return;

            if (level == Data.LevelId.Hub)
            {
                if (bunnyIndex > 2)
                {
                    SetBunny(Data.LevelId.L11_HubDemo, bunnyIndex - 3);
                }
                else
                {
                    SetBit(bunnyIndex, value);
                }
                return;
            }

            // 3 for each level
            SetBit((int) level * 3 + bunnyIndex, value);
        }
    }

    public class YTGVMiscSave : YTGVSaveUInt
    {
        public YTGVMiscSave(uint save) : base(save) { }

        // Currently Equipped Hat. Reserved bits 0-7.
        public Data.Hat CurrentHat
        {
            get => (Data.Hat)(SaveData & 0xFF);
            set
            {
                Plugin.Log($"Setting Current Hat {CurrentHat}->{value}");
                if (CurrentHat == value)
                    return;
                RemoveHat();
                SaveData += (uint)value;
                Plugin.Log("Saving Current Hat");
                NeedsSave = true;
            }
        }
        private void RemoveHat()
        {
            SaveData &= ~(uint)0xFF;
        }

        // Portal unlocks. Somewhat reserves bits 8-27. Unused portal states are reused as other bools.
        // All unused portals are reserved for other bools below, excluding Moon which may be added as an extra portal.
        public bool HasLevelPortalUnlocked(Data.LevelId level)
        {
            return level == Data.LevelId.noone || GetBit((int)level + 8);
        }
        public void SetLevelPortalUnlocked(Data.LevelId level, bool value = true)
        {
            if (level == Data.LevelId.noone)
                return;
            SetBit((int)level + 8, value);
        }

        // Hub does not have a traditional portal which needs to do the unlock anim. Reuse bit.
        public bool HasFlipOWill
        {
            get => HasLevelPortalUnlocked(Data.LevelId.Hub);
            set => SetLevelPortalUnlocked(Data.LevelId.Hub, value);
        }

        // Gym Gears does not have a traditional portal which needs to do the unlock anim. Reuse bit.
        public bool HasGelaToni
        {
            get => HasLevelPortalUnlocked(Data.LevelId.L6_Gym);
            set => SetLevelPortalUnlocked(Data.LevelId.L6_Gym, value);
        }

        // Flushed Away does not have a traditional portal which needs to do the unlock anim. Reuse bit.
        public bool HasPizzaKing
        {
            get => HasLevelPortalUnlocked(Data.LevelId.L8_Sewers);
            set => SetLevelPortalUnlocked(Data.LevelId.L8_Sewers, value);
        }

        // Fecal Matters does not have a traditional portal which needs to do the unlock anim. Reuse bit.
        public bool HasDoggo
        {
            get => HasLevelPortalUnlocked(Data.LevelId.L7_PoopWorld);
            set => SetLevelPortalUnlocked(Data.LevelId.L7_PoopWorld, value);
        }

        // Hub demo is unused. Reuse bit.
        public bool HasMoriosMindPassword
        {
            get => HasLevelPortalUnlocked(Data.LevelId.L11_HubDemo);
            set => SetLevelPortalUnlocked(Data.LevelId.L11_HubDemo, value);
        }

        // Rocket does not have a traditional portal which needs to do the unlock anim. Reuse bit.
        public bool HasRocket
        {
            get => HasLevelPortalUnlocked(Data.LevelId.L16_Rocket);
            set => SetLevelPortalUnlocked(Data.LevelId.L16_Rocket, value);
        }

        // Time attacks do not have a traditional portal which needs to do the unlock anim. Reuse bit.
        public bool HasGoldenSpring
        {
            get => HasLevelPortalUnlocked(Data.LevelId.L17_TimeAttack01);
            set => SetLevelPortalUnlocked(Data.LevelId.L17_TimeAttack01, value);
        }

        // Time attacks do not have a traditional portal which needs to do the unlock anim. Reuse bit.
        public bool HasGoldenPropeller
        {
            get => HasLevelPortalUnlocked(Data.LevelId.L18_TimeAttack02);
            set => SetLevelPortalUnlocked(Data.LevelId.L18_TimeAttack02, value);
        }

        // Time attacks do not have a traditional portal which needs to do the unlock anim. Reuse bit.
        public bool HasOrangeSwitch
        {
            get => HasLevelPortalUnlocked(Data.LevelId.L19_TimeAttack03);
            set => SetLevelPortalUnlocked(Data.LevelId.L19_TimeAttack03, value);
        }

        // Psycho Taxi does not have a traditional portal which needs to do the unlock anim. Reuse bit.
        public bool HasPsychoTaxi
        {
            get => HasLevelPortalUnlocked(Data.LevelId.L20_PsychoTaxi);
            set => SetLevelPortalUnlocked(Data.LevelId.L20_PsychoTaxi, value);
        }
    }
}
