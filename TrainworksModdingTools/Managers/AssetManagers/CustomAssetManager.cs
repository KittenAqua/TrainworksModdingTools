using System;
using System.Collections.Generic;
using UnityEditor;
using System.Text;
using UnityEngine;
using BepInEx.Configuration;
using System.IO;
using System.Reflection;
using HarmonyLib;
using ShinyShoe;
using UnityEngine.AddressableAssets;
using Trainworks.Builders;
using Trainworks.AssetConstructors;
using Trainworks.Utilities;

namespace Trainworks.Managers
{
    /// <summary>
    /// Handles loading custom assets, both raw and from asset bundles.
    /// Assets should be placed inside the plugin's own folder.
    /// </summary>
    public class CustomAssetManager
    {
        /// <summary>
        /// Maps loose asset runtime key (from GUID) to its appropriate asset info
        /// </summary>
        public static IDictionary<Hash128, AssetLoadingInfo> RuntimeKeyToAssetInfo { get; } = new Dictionary<Hash128, AssetLoadingInfo>();
        /// <summary>
        /// Maps asset types to the classes used to make them
        /// </summary>
        public static IDictionary<AssetRefBuilder.AssetTypeEnum, Interfaces.IAssetConstructor> AssetTypeToAssetConstructor = new Dictionary<AssetRefBuilder.AssetTypeEnum, Interfaces.IAssetConstructor>();

        public static void InitializeAssetConstructors()
        {
            AssetTypeToAssetConstructor[AssetRefBuilder.AssetTypeEnum.CardArt] = new CardArtAssetConstructor();
            AssetTypeToAssetConstructor[AssetRefBuilder.AssetTypeEnum.Character] = new CharacterAssetConstructor();
        }

        public static void RegisterCustomAsset(string assetGUID, AssetLoadingInfo loadingInfo)
        {
            var runtimeKey = Hash128.Parse(assetGUID);
            RuntimeKeyToAssetInfo[runtimeKey] = loadingInfo;
        }

        public static GameObject LoadGameObjectFromAssetRef(AssetReference assetRef)
        {
            var runtimeKey = assetRef.RuntimeKey;
            if (BundleManager.RuntimeKeyToBundleInfo.ContainsKey(runtimeKey))
            {
                var bundleInfo = BundleManager.RuntimeKeyToBundleInfo[runtimeKey];
                Trainworks.Log("Loading " + bundleInfo.FilePath);

                var assetType = RuntimeKeyToAssetInfo[runtimeKey].AssetType;
                var assetConstructor = AssetTypeToAssetConstructor[assetType];
                return assetConstructor.Construct(assetRef, bundleInfo);
            }
            else if (RuntimeKeyToAssetInfo.ContainsKey(runtimeKey))
            {
                var assetType = RuntimeKeyToAssetInfo[runtimeKey].AssetType;
                var assetConstructor = AssetTypeToAssetConstructor[assetType];
                return assetConstructor.Construct(assetRef);
            }
            Trainworks.Log(BepInEx.Logging.LogLevel.Warning, "Runtime key is not registered with CustomAssetManager: " + runtimeKey);
            return null;
        }

        public static Sprite LoadSpriteFromRuntimeKey(Hash128 runtimeKey)
        {
            if (RuntimeKeyToAssetInfo.ContainsKey(runtimeKey))
            {
                return LoadSpriteFromPath(RuntimeKeyToAssetInfo[runtimeKey].FullPath);
            }
            Trainworks.Log(BepInEx.Logging.LogLevel.Warning, "Custom asset failed to load from runtime key: " + runtimeKey);
            return null;
        }

        /// <summary>
        /// Create a sprite from texture at provided path.
        /// </summary>
        /// <param name="path">Absolute path of the texture</param>
        /// <returns>The sprite, or null if there is no texture at the given path</returns>
        public static Sprite LoadSpriteFromPath(string path)
        {
            if (File.Exists(path))
            {
                // Create the card sprite
                byte[] fileData = File.ReadAllBytes(path);
                Texture2D tex = new Texture2D(1, 1);
                UnityEngine.ImageConversion.LoadImage(tex, fileData);
                Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 128f);
                sprite.name = Path.GetFileNameWithoutExtension(path);
                return sprite;
            }
            Trainworks.Log(BepInEx.Logging.LogLevel.Warning, "No asset found at: " + path);
            return null;
        }
    }
}
