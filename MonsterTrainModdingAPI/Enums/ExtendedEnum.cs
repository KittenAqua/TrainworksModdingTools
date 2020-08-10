using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonsterTrainModdingAPI.Enums
{
    /// <summary>
    /// An abstract class used to create classes that can safely extend onto existing enumerators
    /// </summary>
    /// <typeparam name="TExtendedEnum"></typeparam>
    /// <typeparam name="TEnum"></typeparam>
    public abstract class ExtendedEnum<TExtendedEnum, TEnum>
        where TExtendedEnum : ExtendedEnum<TExtendedEnum, TEnum>
        where TEnum : Enum
    {
        protected static Dictionary<int, TExtendedEnum> IntToExtendedEnumMap = new Dictionary<int, TExtendedEnum>();
        protected static Dictionary<string, TExtendedEnum> NameToExtendedEnumMap = new Dictionary<string, TExtendedEnum>();
        protected static List<int> ReservedIDs = ((int[])Enum.GetValues(typeof(TEnum))).ToList();
        protected int ID;
        protected string Name;
        /// <summary>
        /// Base Constructor for creating an Extended Enumerator
        /// </summary>
        /// <param name="Name">Name of new Enum Value</param>
        /// <param name="ID">ID of new Enum Value</param>
        public ExtendedEnum(string Name, int ID)
        {
            this.ID = ID;
            this.Name = Name;
            if (NameToExtendedEnumMap.ContainsKey(this.Name))
            {
                MonsterTrainModdingAPI.API.Log(BepInEx.Logging.LogLevel.Warning, $"Name: {this.Name} Conflict in domain, {typeof(TExtendedEnum).Name}");
            }
            if (IntToExtendedEnumMap.ContainsKey(this.ID))
            {
                MonsterTrainModdingAPI.API.Log(BepInEx.Logging.LogLevel.Warning, $"ID#{this.ID} Conflict between {Name} and {IntToExtendedEnumMap[this.ID].GetName()} in domain, {typeof(TExtendedEnum).Name}");
            }
            if (ReservedIDs.Contains(this.ID))
            {
                MonsterTrainModdingAPI.API.Log(BepInEx.Logging.LogLevel.Warning, $"ID#{this.ID} is Reserved and can't be set for {Name}");
            }
            NameToExtendedEnumMap[Name] = (TExtendedEnum)this;
            IntToExtendedEnumMap[ID] = (TExtendedEnum)this;
        }
        /// <summary>
        /// Returns the ID of the new ExtendedEnum
        /// </summary>
        /// <returns></returns>
        public int GetID() => ID;
        /// <summary>
        /// Returns the Enum equivalent of the new ExtendedEnum
        /// </summary>
        /// <returns></returns>
        public virtual TEnum GetEnum() => (TEnum)Enum.ToObject(typeof(TEnum), ID);
        /// <summary>
        /// Returns the Variable name of the new ExtendedEnum
        /// </summary>
        /// <returns></returns>
        public string GetName() => Name;
        /// <summary>
        /// Returns all IDs of all ExtendedEnum classes
        /// </summary>
        /// <returns></returns>
        public static int[] GetAllIDs() => IntToExtendedEnumMap.Keys.ToArray();
        /// <summary>
        /// Returns all Names of All ExtendedEnum classes
        /// </summary>
        /// <returns></returns>
        public static string[] GetAllNames() => NameToExtendedEnumMap.Keys.ToArray();
        /// <summary>
        /// Returns the Value given a key or default
        /// </summary>
        /// <param name="Key">String Key to get Value</param>
        /// <returns></returns>
        public static TExtendedEnum GetValueOrDefault(string Key) => NameToExtendedEnumMap.GetValueOrDefault(Key);
        /// <summary>
        /// Returns the Value given a key or default
        /// </summary>
        /// <param name="Key">Int Key to get Value</param>
        /// <returns></returns>
        public static TExtendedEnum GetValueOrDefault(int Key) => IntToExtendedEnumMap.GetValueOrDefault(Key);
        /// <summary>
        /// Returns a Generated Variant of TEnum that can be used for API functions
        /// </summary>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static TExtendedEnum Convert(TEnum @enum)
        {
            int id = System.Convert.ToInt32((Enum)@enum);
            if (IntToExtendedEnumMap.ContainsKey(id))
            {
                TExtendedEnum @extendedEnum = (TExtendedEnum)Activator.CreateInstance(typeof(TExtendedEnum));
                @extendedEnum.ID = id;
                @extendedEnum.Name = "Generated_" + Enum.GetName(typeof(TEnum), @enum);
                NameToExtendedEnumMap[@extendedEnum.Name] = @extendedEnum;
                IntToExtendedEnumMap[@extendedEnum.ID] = @extendedEnum;
                return @extendedEnum;
            }
            else
            {
                return IntToExtendedEnumMap[id];
            }
        }
    }
}
