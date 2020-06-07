using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
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
        private static IDictionary<string, string> CustomCharacterAssetPath { get; } = new Dictionary<string, string>();
        public static FallbackData FallbackData { get; set; }
        public static SaveManager SaveManager { get; set; }

        public static bool RegisterCustomCharacter(CharacterData data, string assetPath)
        {
            CustomCharacterData.Add(data.GetID(), data);
            CustomCharacterAssetPath.Add(data.GetID(), assetPath);
            return true;
        }

        public static void FinishCustomCharacterRegistration()
        {
            FallbackData = (FallbackData)AccessTools.Field(typeof(CharacterData), "fallbackData")
                .GetValue(SaveManager.GetAllGameData().GetAllCharacterData()[0]);
        }

        public static GameObject CreateCharacterGameObject(string characterID)
        {
            CharacterData characterData = CustomCharacterData[characterID];

            var realCharacter = GameObject.Instantiate(CustomCharacterManager.FallbackData.GetDefaultCharacterPrefab());
            //var uiMeshSpine = realCharacter.GetComponentInChildren<CharacterUIMeshSpine>();
            var characterState = realCharacter.GetComponentInChildren<CharacterState>();
            //uiMeshSpine.gameObject.transform.parent = null;
            //GameObject.Destroy(uiMeshSpine.gameObject);
            var realCharacterUI = realCharacter.GetComponentInChildren<CharacterUI>();

            Debug.Log(realCharacter.GetComponentInChildren<MeshRenderer>(true));
            Debug.Log(realCharacter.GetComponentInChildren<MeshRenderer>(true).gameObject);
            realCharacter.GetComponentInChildren<MeshRenderer>(true).gameObject.SetActive(true);

            string assetPath = CustomCharacterAssetPath[characterID];
            string cardPath = "BepInEx/plugins/" + assetPath;
            byte[] fileData = File.ReadAllBytes(cardPath);
            Texture2D tex = new Texture2D(1, 1);
            UnityEngine.ImageConversion.LoadImage(tex, fileData);
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 128f);

            realCharacterUI.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
            AccessTools.Field(typeof(CharacterState), "sprite").SetValue(characterState, sprite);
            realCharacterUI.GetSpriteRenderer().sprite = sprite;

            AccessTools.Field(typeof(AssetReference), "m_LoadedAsset").SetValue(characterData.characterPrefabVariantRef, realCharacter);

            Debug.Log(characterData.characterPrefabVariantRef.Asset);

            Debug.Log(characterData.HasCharacterPrefabVariant());
            Debug.Log(realCharacter.GetComponentInChildren<CharacterUI>());

            return realCharacter;
        }
    }
}
