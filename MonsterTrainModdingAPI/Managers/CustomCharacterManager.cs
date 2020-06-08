using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using BepInEx.Logging;
using MonsterTrainModdingAPI.Builder;
using HarmonyLib;
using UnityEngine;
using ShinyShoe;
using UnityEngine.AddressableAssets;

namespace MonsterTrainModdingAPI.Managers
{

    class CustomCharacterManager
    {
        public static IDictionary<string, CharacterData> CustomCharacterData { get; } = new Dictionary<string, CharacterData>();

        public static IDictionary<string, Sprite> SpriteLibrary { get; } = new Dictionary<string, Sprite>();
        public static FallbackData FallbackData { get; set; }
        public static SaveManager SaveManager { get; set; }

        public static bool RegisterCustomCharacter(CharacterData data)
        {
            CustomCharacterData.Add(data.GetID(), data);
            return true;
        }

        public static bool RegisterCustomCharacter(CharacterData data, Sprite sprite)
        {
            SpriteLibrary.Add(data.GetID(), sprite);
            return RegisterCustomCharacter(data);
        }

        public static void FinishCustomCharacterRegistration()
        {
            FallbackData = (FallbackData)AccessTools.Field(typeof(CharacterData), "fallbackData")
                .GetValue(SaveManager.GetAllGameData().GetAllCharacterData()[0]);
        }

        public static CharacterData GetCharacterDataByID(string characterID)
        {
            if (CustomCharacterData.ContainsKey(characterID))
            {
                return CustomCharacterData[characterID];
            }
            return null;
        }

        public static GameObject CreateCharacterGameObject(CharacterData characterData, Sprite characterSprite)
        {

            // Create a new character GameObject by cloning the default one in FallbackData
            var characterGameObject = GameObject.Instantiate(CustomCharacterManager.FallbackData.GetDefaultCharacterPrefab());

            // Set aside its CharacterState and CharacterUI components for later use
            var characterState = characterGameObject.GetComponentInChildren<CharacterState>();
            var characterUI = characterGameObject.GetComponentInChildren<CharacterUI>();

            // Make its MeshRenderer active; this is what enables the sprite we're about to attach to show up
            characterGameObject.GetComponentInChildren<MeshRenderer>(true).gameObject.SetActive(true);

            // Set states in the CharacterState and CharacterUI to the sprite to show it ingame
            AccessTools.Field(typeof(CharacterState), "sprite").SetValue(characterState, characterSprite);
            characterUI.GetSpriteRenderer().sprite = characterSprite;

            // Tell the asset reference that the GameObject has already been loaded
            // This circumvents an issue where the game attempts to load the asset but fails
            AccessTools.Field(typeof(AssetReference), "m_LoadedAsset").SetValue(characterData.characterPrefabVariantRef, characterGameObject);

            return characterGameObject;
        }
        public static GameObject CreateCharacterGameObject(CharacterData characterData)
        {
            //If Sprite is not already registered, try getting appropriate data
            if (!SpriteLibrary.ContainsKey(characterData.GetID()))
            {
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
                    SpriteLibrary.Add(characterData.GetID(), sprite);
                    return CreateCharacterGameObject(characterData, sprite);
                }
            }
            else
            {
                return CreateCharacterGameObject(characterData, SpriteLibrary[characterData.GetID()]);
            }
            return null;
        }
        public static GameObject CreateCharacterGameObject(string characterID)
        {
            CharacterData characterData = CustomCharacterData[characterID];
            return CreateCharacterGameObject(characterData);
        }
    }
}
