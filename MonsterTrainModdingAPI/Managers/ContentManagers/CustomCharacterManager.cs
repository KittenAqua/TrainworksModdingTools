using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using BepInEx.Logging;
using MonsterTrainModdingAPI.Builders;
using HarmonyLib;
using UnityEngine;
using ShinyShoe;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using MonsterTrainModdingAPI.Utilities;

namespace MonsterTrainModdingAPI.Managers
{
    /// <summary>
    /// Handles registration and storage of custom character data.
    /// </summary>
    public class CustomCharacterManager
    {
        /// <summary>
        /// Maps custom character IDs to their respective CharacterData.
        /// </summary>
        public static IDictionary<string, CharacterData> CustomCharacterData { get; } = new Dictionary<string, CharacterData>();
        /// <summary>
        /// A default character prefab which is cloned to create custom characters.
        /// Essential for custom character art. Set during game startup.
        /// </summary>
        public static GameObject TemplateCharacter { get; set; }


        /// <summary>
        /// Register a custom character with the manager, allowing it to show up in game.
        /// </summary>
        /// <param name="data">The custom character data to register</param>
        public static bool RegisterCustomCharacter(CharacterData data)
        {
            if (!CustomCharacterData.ContainsKey(data.GetID()))
            {
                CustomCharacterData.Add(data.GetID(), data);
                ProviderManager.SaveManager.GetAllGameData().GetAllCharacterData().Add(data);
                return true;
            }
            else
            {
                API.Log(LogLevel.Warning, "Attempted to register duplicate character data with name: " + data.name);
                return false;
            }
        }

        /// <summary>
        /// Get the character data corresponding to the given ID
        /// </summary>
        /// <param name="characterID">ID of the character to get</param>
        /// <returns>The character data for the given ID</returns>
        public static CharacterData GetCharacterDataByID(string characterID)
        {
            // Search for custom character matching ID
            var guid = GUIDGenerator.GenerateDeterministicGUID(characterID);
            if (CustomCharacterData.ContainsKey(guid))
            {
                return CustomCharacterData[guid];
            }

            // No custom card found; search for vanilla character matching ID
            var vanillaChar = ProviderManager.SaveManager.GetAllGameData().GetAllCharacterData().Find((chara) => {
                return chara.GetID() == characterID;
            });
            if (vanillaChar == null)
            {
                API.Log(LogLevel.All, "Couldn't find character: " + characterID + " - This will cause crashes.");
            }
            return vanillaChar;
        }

        /// <summary>
        /// Maps custom subtypes IDs to their respective SubtypeData.
        /// </summary>
        public static IDictionary<string, SubtypeData> CustomSubtypeData { get; } = new Dictionary<string, SubtypeData>();
        /// <summary>
        /// Registers a subtype, making it available for localization
        /// </summary>
        /// <param name="ID">The key used for assigning the subtype, and for its localization</param>
        public static void RegisterSubtype(string ID)
        {
            CustomSubtypeData.Add(ID, new SubtypeDataBuilder { _Subtype = ID }.Build());
        }


        public static void LoadTemplateCharacter(SaveManager saveManager)
        {
            var characterData = saveManager.GetAllGameData().GetAllCharacterData()[0];
            //var characterData = new CharacterData();
            var loadOperation = characterData.characterPrefabVariantRef.LoadAsset<GameObject>();
            loadOperation.Completed += TemplateCharacterLoadingComplete;
        }

        private static void TemplateCharacterLoadingComplete(IAsyncOperation<GameObject> asyncOperation)
        {
            TemplateCharacter = asyncOperation.Result;
            if (TemplateCharacter == null)
            {
                API.Log(LogLevel.Warning, "Failed to load character template");
            }
        }
    }
}
