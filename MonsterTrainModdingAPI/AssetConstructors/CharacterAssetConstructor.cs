using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using HarmonyLib;
using MonsterTrainModdingAPI.Managers;
using MonsterTrainModdingAPI.Utilities;
using Spine.Unity;
using Spine;
using System.Linq;
using UnityEngine.ResourceManagement.AsyncOperations;
using ShinyShoe;

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
            /*
            // This code attempts to construct a Spine Object with no animations to match the imported PNG file. This gives us better outlining, draw order, and highlighting.

            GameObject skeletonData = new GameObject();
            skeletonData.name = sprite.name;
            var s = skeletonData.AddComponent<SkeletonAnimation>();

            // All we need is a complete and valid SkeletonDataAsset... does that include the atlas primarymaterial, atlas data, skeletonjson, state, and skeleton?
            s.skeletonDataAsset = new SkeletonDataAsset();
            s.skeletonDataAsset.skeletonJSON = new TextAsset("{\"skeleton\":{\"spine\":\"3.6.0.7-beta\",\"width\":" + sprite.rect.width + ",\"height\":" + sprite.rect.height + ",\"fps\":24,\"hash\":\" \",\"name\":\"Armature\"},\"bones\":[{\"name\":\"root\"}],\"slots\":[{\"name\":\"Unit\",\"bone\":\"root\",\"attachment\":\"Unit\"}],\"skins\":{\"default\":{\"Unit\":{\"Unit\":{\"name\":\"Unit\",\"width\":" + sprite.rect.width + ",\"height\":" + sprite.rect.height + ",\"y\":" + sprite.rect.width + "}}}}}");
            s.skeletonDataAsset.atlasAssets.AddToArray<AtlasAssetBase>(new SpineAtlasAsset());
            s.skeletonDataAsset.atlasAssets[0].
            
            return CreateCharacterGameObject(assetRef, sprite, skeletonData);
            */

            API.Log(BepInEx.Logging.LogLevel.All, "Character Template: " + CustomCharacterManager.TemplateCharacter);

            // Create a new character GameObject by cloning an existing, working character
            var characterGameObject = GameObject.Instantiate(CustomCharacterManager.TemplateCharacter);

            // Set aside its CharacterState and CharacterUI components for later use
            var characterState = characterGameObject.GetComponentInChildren<CharacterState>();
            var characterUI = characterGameObject.GetComponentInChildren<CharacterUI>();

            // Set the name, and hide the UI
            characterUI.HideDetails();
            characterGameObject.name = "Character_" + sprite.name;

            // Make its MeshRenderer active; this is what enables the sprite we're about to attach to show up
            characterGameObject.GetComponentInChildren<MeshRenderer>(true).gameObject.SetActive(true);

            // Delete all the spine anims
            var spine = characterGameObject.GetComponentInChildren<ShinyShoe.CharacterUIMeshSpine>(true);
            foreach (Transform child in spine.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            spine.gameObject.SetActive(false);

            // Set states in the CharacterState and CharacterUI to the sprite to show it ingame
            Traverse.Create(characterState).Field<Sprite>("sprite").Value = sprite;
            characterUI.GetSpriteRenderer().sprite = sprite;

            // Set up the outline Sprite - well, seems like there will be problems here
            var outlineMesh = characterGameObject.GetComponentInChildren<CharacterUIOutlineMesh>(true);
            //Traverse.Create(outlineMesh).Field("outlineData").Field<Texture2D>("characterTexture").Value = sprite.texture;
            //Traverse.Create(outlineMesh).Field("outlineData").Field<Texture2D>("outlineTexture").Value = sprite.texture;
            Traverse.Create(outlineMesh).Field<CharacterOutlineData>("outlineData").Value = null; //CharacterOutlineData.Create(sprite.texture);
            //Traverse.Create(outlineMesh).Field("outlineData").Field<Texture2D>("outlineTexture").Value = sprite.texture;

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
            var characterGameObject = GameObject.Instantiate(CustomCharacterManager.TemplateCharacter);

            // Set aside its CharacterState and CharacterUI components for later use
            var characterState = characterGameObject.GetComponentInChildren<CharacterState>();
            var characterUI = characterGameObject.GetComponentInChildren<CharacterUI>();

            // Hide the UI
            characterUI.HideDetails();
            characterGameObject.name = "Character_" + skeletonData.name;

            // Hide the quad, ensure the Spine mesh is shown (it should be by default)           
            var Quad = characterGameObject.GetComponentInChildren<ShinyShoe.CharacterUIMesh>(true).gameObject;
            Quad.SetActive(false);

            // Set the sprite for the preview
            Traverse.Create(characterState).Field<Sprite>("sprite").Value = sprite;
            characterUI.GetSpriteRenderer().sprite = sprite;

            // Set the shader
            skeletonData.GetComponent<SkeletonAnimation>().addNormals = true;
            skeletonData.GetComponent<SkeletonAnimation>().skeletonDataAsset.atlasAssets[0].PrimaryMaterial.shader = Shader.Find("Shiny Shoe/Character Spine Shader");

            // Activate the SpineMesh
            var spineMeshes = characterGameObject.GetComponentInChildren<ShinyShoe.CharacterUIMeshSpine>(true);
            spineMeshes.gameObject.SetActive(true);

            // Skeleton cloning produces superior effects
            var clonedObject = characterGameObject.GetComponentInChildren<SkeletonAnimation>().gameObject;
            clonedObject.name = "Spine GameObject (" + characterGameObject.name + ")";

            var dest = clonedObject.GetComponentInChildren<SkeletonAnimation>();
            var source = skeletonData.GetComponentInChildren<SkeletonAnimation>();

            dest.skeletonDataAsset = source.skeletonDataAsset;

            // Destroy the evidence
            GameObject.Destroy(skeletonData.gameObject);

            // Now delete the pre-existing animations
            GameObject.Destroy(spineMeshes.transform.GetChild(1).gameObject);
            GameObject.Destroy(spineMeshes.transform.GetChild(2).gameObject);

            // Set googly eye positions
            // Add in visual effects such as particles

            // Remove our friends
            characterUI.GetComponent<SpriteRenderer>().forceRenderingOff = true;
            dest.gameObject.SetActive(false);

            return characterGameObject;
        }

        // This is the fix for how to hide/show character templates, because I can't figure out how vanilla accomplishes it
        [HarmonyPatch(typeof(MonsterManager), "InstantiateCharacter")]
        class ShowDisabledImages
        {
            static void Postfix(MonsterManager __instance, ref GameObject __result)
            {
                var a = __result.GetComponentInChildren<CharacterUI>(true);
                if (a != null)
                    a.GetSpriteRenderer().forceRenderingOff = true;

                var c = __result.GetComponentInChildren<SkeletonAnimation>(true);
                if (c != null)
                    c.gameObject.SetActive(true);
            }
        }

        // This is required to reset the UV colours for Mesh based Spine animations
        [HarmonyPatch(typeof(CharacterUIMeshSpine), "Setup")]
        class FixShaderPropertiesOnCustomSpineAnims
        {
            static void Postfix(CharacterUIMeshSpine __instance)
            {
                var matProp = Traverse.Create(__instance).Field<MaterialPropertyBlock>("_matProps").Value = new MaterialPropertyBlock();
                __instance.MeshRenderer.SetPropertyBlock(matProp);
            }
        }

        public GameObject Construct(AssetReference assetRef, BundleAssetLoadingInfo bundleInfo)
        {
            var tex = BundleManager.LoadAssetFromBundle(bundleInfo, bundleInfo.SpriteName) as Texture2D;
            if (tex != null)
            {
                Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 128f);
                sprite.name = "CharacterSprite_" + bundleInfo.SpriteName;
                if (bundleInfo.ObjectName != null)
                {
                    GameObject gameObject = BundleManager.LoadAssetFromBundle(bundleInfo, bundleInfo.ObjectName) as GameObject;
                    if (gameObject != null)
                    {
                        var spineObj = CreateCharacterGameObject(assetRef, sprite, gameObject);
                        GameObject.DontDestroyOnLoad(spineObj);
                        return spineObj;
                    }
                }
                var charObj = CreateCharacterGameObject(assetRef, sprite, API.TrainworksBundle.LoadAsset("assets/PNGTemplate.prefab") as GameObject);
                GameObject.DontDestroyOnLoad(charObj);
                return charObj;
            }
            API.Log(BepInEx.Logging.LogLevel.Warning, "Invalid sprite name when loading asset: " + bundleInfo.SpriteName);
            return null;
        }
    }
}
