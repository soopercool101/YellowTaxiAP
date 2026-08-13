using System;
using System.Linq;
using YellowTaxiAP.Managers;

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

    public class YTGVPortalSave : YTGVSaveUInt
    {
        public YTGVPortalSave(uint save) : base(save) { }

        public int GetLevelOrderIndex(Data.LevelId level)
        {
            return Array.IndexOf(APPortalManager.OriginalPortalLevelOrder, level);
        }

        public bool IsLevelPortalUnlocked(Data.LevelId level)
        {
            if (level == Data.LevelId.L11_HubDemo)
                return false;
            return level == Data.LevelId.noone || GetBit(GetLevelOrderIndex(level));
        }

        public void SetLevelPortalUnlocked(Data.LevelId level, bool value = true)
        {
            if (!APPortalManager.OriginalPortalLevelOrder.Contains(level))
            {
                Plugin.BepinLogger.LogWarning($"{level} was attempted to be unlocked, but was not found in level order!");
                return;
            }
            SetBit(GetLevelOrderIndex(level), value);
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

        // Currently Equipped Hat. Reserved bits 0-5.
        // Biggest hat is 52, or 0b00110100
        public Data.Hat CurrentHat
        {
            get => (Data.Hat)(SaveData & 0b111111);
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
            SaveData &= ~(uint)0b111111;
        }

        // Michele typically doesn't get saved to a save file, but this makes things less annoying for cheesesanity without shuffle rat
        public bool HasRat
        {
            get => GetBit(6);
            set => SetBit(6, value);
        }

        public bool HasGelaToni
        {
            get => GetBit(7);
            set => SetBit(7, value);
        }

        public bool HasPizzaKing
        {
            get => GetBit(8);
            set => SetBit(8, value);
        }

        public bool HasDoggo
        {
            get => GetBit(9);
            set => SetBit(9, value);
        }

        public bool HasMoriosMindPassword
        {
            get => GetBit(10);
            set => SetBit(10, value);
        }

        public bool HasRocket
        {
            get => GetBit(11);
            set => SetBit(11, value);
        }

        public bool HasGoldenSpring
        {
            get => GetBit(12);
            set => SetBit(12, value);
        }

        public bool HasGoldenPropeller
        {
            get => GetBit(13);
            set => SetBit(13, value);
        }

        public bool HasOrangeSwitch
        {
            get => GetBit(14);
            set => SetBit(14, value);
        }

        public bool HasPsychoTaxi
        {
            get => GetBit(15);
            set => SetBit(15, value);
        }
    }
}
