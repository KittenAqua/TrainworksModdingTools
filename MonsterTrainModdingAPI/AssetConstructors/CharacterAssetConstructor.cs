using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using HarmonyLib;
using MonsterTrainModdingAPI.Managers;

namespace MonsterTrainModdingAPI.AssetConstructors
{
    public class CharacterAssetConstructor : MonsterTrainModdingAPI.Interfaces.IAssetConstructor
    {
        public GameObject Construct(AssetReference assetRef)
        {
            return CreateCharacterGameObject(assetRef);
        }

        /// <summary>
        /// Create a GameObject for the custom card with the AssetReference and Sprite
        /// </summary>
        /// <param name="assetRef">Reference containing the asset information</param>
        /// <param name="sprite">Sprite to create card with</param>
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
        /// Create a GameObject for the custom card with the given ID.
        /// Used for loading custom card art.
        /// </summary>
        /// <param name="assetRef">Reference containing the asset information</param>
        /// <returns>The GameObject for the custom character specified by the asset reference</returns>
        public static GameObject CreateCharacterGameObject(AssetReference assetRef)
        {
            Sprite sprite = CustomAssetManager.LoadSpriteFromRuntimeKey(assetRef.RuntimeKey);
            if (sprite != null)
            {
                return CreateCharacterGameObject(assetRef, sprite);
            }
            return null;
        }
    }
}
