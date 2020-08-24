using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using HarmonyLib;
using MonsterTrainModdingAPI.Managers;
using MonsterTrainModdingAPI.Utilities;

namespace MonsterTrainModdingAPI.AssetConstructors
{
    public class CharacterAssetConstructor : MonsterTrainModdingAPI.Interfaces.IAssetConstructor
    {
        public GameObject Construct(AssetReference assetRef)
        {
            return CreateCharacterGameObject(assetRef);
        }

        /// <summary>
        /// Create a GameObject for the custom character with the given asset reference.
        /// Used for loading custom character art.
        /// </summary>
        /// <param name="assetRef">Reference containing the asset information</param>
        /// <returns>The GameObject for the custom character specified by the asset reference</returns>
        public static GameObject CreateCharacterGameObject(AssetReference assetRef)
        {
            Sprite sprite = CustomAssetManager.LoadSpriteFromRuntimeKey(assetRef.RuntimeKey);
            if (sprite != null)
            {
                var charObj = CreateCharacterGameObject(assetRef, sprite);
                GameObject.DontDestroyOnLoad(charObj);
                return charObj;
            }
            return null;
        }

        /// <summary>
        /// Create a GameObject for the custom character with the AssetReference and Sprite
        /// </summary>
        /// <param name="assetRef">Reference containing the asset information</param>
        /// <param name="sprite">Sprite to create character with</param>
        /// <returns>The GameObject for the character</returns>
        private static GameObject CreateCharacterGameObject(AssetReference assetRef, Sprite sprite)
        {
            // Create a new character GameObject by cloning an existing, working character
            var characterGameObject = GameObject.Instantiate(CustomCharacterManager.TemplateCharacter.GetCharacterPrefabVariant());

            characterGameObject.name = "Character_" + assetRef.RuntimeKey;

            // Set aside its CharacterState and CharacterUI components for later use
            var characterState = characterGameObject.GetComponentInChildren<CharacterState>();
            var characterUI = characterGameObject.GetComponentInChildren<CharacterUI>();

            // Make its MeshRenderer active; this is what enables the sprite we're about to attach to show up
            characterGameObject.GetComponentInChildren<MeshRenderer>(true).gameObject.SetActive(true);

            // Set states in the CharacterState and CharacterUI to the sprite to show it ingame
            AccessTools.Field(typeof(CharacterState), "sprite").SetValue(characterState, sprite);
            characterUI.GetSpriteRenderer().sprite = sprite;

            return characterGameObject;
        }

        /// <summary>
        /// Create a GameObject for the custom character with the AssetReference and Sprite
        /// </summary>
        /// <param name="assetRef">Reference containing the asset information</param>
        /// <param name="sprite">Sprite to create character with</param>
        /// <param name="skeletonData">GameObject containing the necessary Spine animation data</param>
        /// <returns>The GameObject for the character</returns>
        private static GameObject CreateCharacterGameObject(AssetReference assetRef, Sprite sprite, GameObject skeletonData)
        {
            // Create a new character GameObject by cloning an existing, working character
            var characterGameObject = GameObject.Instantiate(CustomCharacterManager.TemplateCharacter.GetCharacterPrefabVariant());

            characterGameObject.name = "Character_" + assetRef.RuntimeKey;

            // Set aside its CharacterState and CharacterUI components for later use
            var characterState = characterGameObject.GetComponentInChildren<CharacterState>();
            var characterUI = characterGameObject.GetComponentInChildren<CharacterUI>();

            // Hide the quad and show the spine mesh
            var quadDefault = characterUI.transform.Find("Quad_Default");
            quadDefault.gameObject.SetActive(false);
            var spineMeshes = characterUI.transform.Find("SpineMeshes");
            spineMeshes.gameObject.SetActive(true);

            // Make its MeshRenderer active; this is what enables the sprite we're about to attach to show up
            characterGameObject.GetComponentInChildren<MeshRenderer>(true).gameObject.SetActive(true);
            AccessTools.Field(typeof(CharacterState), "sprite").SetValue(characterState, sprite);
            characterUI.GetSpriteRenderer().sprite = sprite;

            // Add the skeleton data to the spine mesh
            var characterSkeletonAnimation = GameObject.Instantiate(skeletonData);
            characterSkeletonAnimation.transform.SetParent(spineMeshes.transform);

            return characterGameObject;
        }

        public GameObject Construct(AssetReference assetRef, BundleAssetLoadingInfo bundleInfo)
        {
            var tex = BundleManager.LoadAssetFromBundle(bundleInfo, bundleInfo.SpriteName) as Texture2D;
            if (tex != null)
            {
                Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 128f);
                
                GameObject gameObject;
                if (bundleInfo.ObjectName != null)
                {
                    gameObject = BundleManager.LoadAssetFromBundle(bundleInfo, bundleInfo.ObjectName) as GameObject;
                    if (gameObject != null)
                    {
                        var spineObj = CreateCharacterGameObject(assetRef, sprite, gameObject);
                        GameObject.DontDestroyOnLoad(spineObj);
                        return spineObj;
                    }
                }
                var charObj = CreateCharacterGameObject(assetRef, sprite);
                GameObject.DontDestroyOnLoad(charObj);
                return charObj;
            }
            API.Log(BepInEx.Logging.LogLevel.Warning, "Invalid sprite name when loading asset: " + bundleInfo.SpriteName);
            return null;
        }
    }
}
