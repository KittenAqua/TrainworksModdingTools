using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using BepInEx.Logging;
using MonsterTrainModdingAPI.Builders;
using MonsterTrainModdingAPI.Utilities;
using HarmonyLib;
using UnityEngine;
using ShinyShoe;
using UnityEngine.AddressableAssets;

namespace MonsterTrainModdingAPI.Managers
{
    /// <summary>
    /// Handles registration and storage of custom character data.
    /// </summary>
    class CustomCharacterManager
    {
        /// <summary>
        /// Maps custom character IDs to their respective CharacterData.
        /// </summary>
        public static IDictionary<string, CharacterData> CustomCharacterData { get; } = new Dictionary<string, CharacterData>();
        /// <summary>
        /// Maps custom character IDs to their respective Loading Information.
        /// </summary>
        public static IDictionary<string, AssetBundleLoadingInfo> CharacterBundleData { get; } = new Dictionary<string, AssetBundleLoadingInfo>();
        /// <summary>
        /// FallbackData contains a default character prefab which is cloned to create custom characters.
        /// Essential for custom character art. Set during game startup.
        /// </summary>
        public static FallbackData FallbackData { get; set; }
        /// <summary>
        /// Static reference to the game's SaveManager, which is necessary to register new characters.
        /// </summary>
        public static SaveManager SaveManager { get; set; }

        /// <summary>
        /// Register a custom character with the manager, allowing it to show up in game.
        /// </summary>
        /// <param name="data">The custom character data to register</param>
        /// <param name="info">The Information Used to Load an AssetBundle</param>
        public static bool RegisterCustomCharacter(CharacterData data, AssetBundleLoadingInfo info = null)
        {
            if (info != null) CharacterBundleData.Add(data.GetID(), info);
            CustomCharacterData.Add(data.GetID(), data);
            SaveManager.GetAllGameData().GetAllCharacterData().Add(data);
            return true;
        }

        /// <summary>
        /// Get the custom character data corresponding to the given ID
        /// </summary>
        /// <param name="characterID">ID of the custom character to get</param>
        /// <returns>The custom character data for the given ID</returns>
        public static CharacterData GetCharacterDataByID(string characterID)
        {
            if (CustomCharacterData.ContainsKey(characterID))
            {
                return CustomCharacterData[characterID];
            }
            return null;
        }

        /// <summary>
        /// Create a GameObject for the custom character from characterData and a sprite
        /// </summary>
        /// <param name="characterData">Data to create the Character GameObject</param>
        /// <param name="sprite">Sprite to use to create the Character GameObject</param>
        /// <returns></returns>
        private static GameObject CreateCharacterGameObject(CharacterData characterData, Sprite sprite)
        {
            // Create a new character GameObject by cloning the default one in FallbackData
            var characterGameObject = GameObject.Instantiate(CustomCharacterManager.FallbackData.GetDefaultCharacterPrefab());

            // Set aside its CharacterState and CharacterUI components for later use
            var characterState = characterGameObject.GetComponentInChildren<CharacterState>();
            var characterUI = characterGameObject.GetComponentInChildren<CharacterUI>();

            // Make its MeshRenderer active; this is what enables the sprite we're about to attach to show up
            characterGameObject.GetComponentInChildren<MeshRenderer>(true).gameObject.SetActive(true);

            // Set states in the CharacterState and CharacterUI to the sprite to show it ingame
            AccessTools.Field(typeof(CharacterState), "sprite").SetValue(characterState, sprite);
            characterUI.GetSpriteRenderer().sprite = sprite;

            // Tell the asset reference that the GameObject has already been loaded
            // This circumvents an issue where the game attempts to load the asset but fails
            AccessTools.Field(typeof(AssetReference), "m_LoadedAsset").SetValue(characterData.characterPrefabVariantRef, characterGameObject);

            return characterGameObject;
        }

        /// <summary>
        /// Create a GameObject for the custom character with the given ID.
        /// Used for loading custom character art.
        /// </summary>
        /// <param name="characterID">ID of the custom character to create the GameObject for</param>
        /// <returns>The GameObject for the custom character with given ID</returns>
        public static GameObject CreateCharacterGameObject(string characterID)
        {
            CharacterData characterData = CustomCharacterData[characterID];

            if (CharacterBundleData.ContainsKey(characterID))
            {
                Texture2D tex = AssetBundleUtils.LoadAssetFromPath<Texture2D>(CharacterBundleData[characterID]);
                Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 128f);
                return CreateCharacterGameObject(characterData, sprite);
            }

            // Get the path to the asset from the character's asset reference data
            string assetPath = (string)AccessTools.Field(typeof(AssetReferenceGameObject), "m_AssetGUID").GetValue(characterData.characterPrefabVariantRef);
            string characterPath = "BepInEx/plugins/" + assetPath;
            if (File.Exists(characterPath))
            {
                // Create the character sprite
                byte[] fileData = File.ReadAllBytes(characterPath);
                Texture2D tex = new Texture2D(1, 1);
                UnityEngine.ImageConversion.LoadImage(tex, fileData);
                Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 128f);

                return CreateCharacterGameObject(characterData, sprite);
            }
            return null;
        }
    }
}
