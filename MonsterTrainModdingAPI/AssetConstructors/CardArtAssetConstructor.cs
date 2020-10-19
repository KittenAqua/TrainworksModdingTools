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
    public class CardArtAssetConstructor : MonsterTrainModdingAPI.Interfaces.IAssetConstructor
    {
        public GameObject Construct(AssetReference assetRef)
        {
            return CreateCardGameObject(assetRef);
        }

        /// <summary>
        /// Create a GameObject for the custom card with the AssetReference and Sprite
        /// </summary>
        /// <param name="assetRef">Reference containing the asset information</param>
        /// <param name="sprite">Sprite to create card with</param>
        /// <returns>The GameObject for the card</returns>
        private static GameObject CreateCardGameObject(AssetReference assetRef, Sprite sprite)
        {
            // Create a new card GameObject from scratch
            // Cards are simple enough that we can get away with doing this
            GameObject cardGameObject = new GameObject();
            cardGameObject.name = assetRef.RuntimeKey.ToString();
            Image newImage = cardGameObject.AddComponent<Image>();
            newImage.sprite = sprite;

            GameObject.DontDestroyOnLoad(cardGameObject);

            return cardGameObject;
        }

        /// <summary>
        /// Create a GameObject for the custom card with the given asset reference.
        /// Used for loading custom card art.
        /// </summary>
        /// <param name="assetRef">Reference containing the asset information</param>
        /// <returns>The GameObject for the custom card specified by the asset reference</returns>
        public static GameObject CreateCardGameObject(AssetReference assetRef)
        {
            Sprite sprite = CustomAssetManager.LoadSpriteFromRuntimeKey(assetRef.RuntimeKey);
            if (sprite != null)
            {
                return CreateCardGameObject(assetRef, sprite);
            }
            return null;
        }

        public GameObject Construct(AssetReference assetRef, BundleAssetLoadingInfo bundleInfo)
        {
            var asset = BundleManager.LoadAssetFromBundle(bundleInfo, bundleInfo.SpriteName);
            if (asset.GetType() == typeof(Texture2D))
            {
                // "Type checking ew"
                // You're thinking it, I can tell.
                // Through all the horrible things we've done with this project,
                // type checking is where you draw the line?
                var tex = asset as Texture2D;
                if (tex != null)
                {
                    Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 128f);
                    return CreateCardGameObject(assetRef, sprite);
                }
                return null;
            }
            else
            {
                var gameObject = asset as GameObject;
                if (gameObject != null)
                {
                    GameObject.DontDestroyOnLoad(gameObject);
                    return gameObject;
                }
            }
            API.Log(BepInEx.Logging.LogLevel.Warning, "Invalid asset type " + asset.GetType() + " when loading asset: " + bundleInfo.SpriteName);
            return null;
        }
    }
}
