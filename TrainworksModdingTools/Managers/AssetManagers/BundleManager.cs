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
    /// Handles loading custom asset bundles.
    /// Bundles should be placed inside the plugin's own folder.
    /// </summary>
    public class BundleManager
    {
        /// <summary>
        /// Maps asset runtime key (from GUID) to the asset bundle info
        /// </summary>
        public static IDictionary<Hash128, BundleAssetLoadingInfo> RuntimeKeyToBundleInfo { get; } = new Dictionary<Hash128, BundleAssetLoadingInfo>();

        /// <summary>
        /// Maps path to asset bundle
        /// </summary>
        public static Dictionary<string, AssetBundle> LoadedAssetBundles { get; } = new Dictionary<string, AssetBundle>();

        public static void RegisterBundle(string assetGUID, BundleAssetLoadingInfo bundleInfo)
        {
            string path = bundleInfo.FullPath;
            if (!LoadedAssetBundles.ContainsKey(path))
            {
                if (File.Exists(path))
                {
                    LoadedAssetBundles[path] = AssetBundle.LoadFromFile(path);
                }
                else
                {
                    Trainworks.Log(BepInEx.Logging.LogLevel.Warning, "Custom asset bundle failed to load from path: " + path);
                }
            }
            var runtimeKey = Hash128.Parse(assetGUID);
            RuntimeKeyToBundleInfo[runtimeKey] = bundleInfo;
        }

        public static void ApplyImportSettings<T>(BundleAssetLoadingInfo info, ref T @asset) where T : UnityEngine.Object
        {
            if (info.ImportSettings != null)
            {
                ((ISettings<T>)info.ImportSettings).ApplySettings(ref @asset);
            }
        }

        /// <summary>
        /// Load an asset from an asset bundle
        /// </summary>
        /// <param name="info">Loading info for the asset</param>
        /// <param name="assetName">Name of the asset to load from the bundle</param>
        /// <returns>The asset specified by the given info as a UnityEngine object</returns>
        public static UnityEngine.Object LoadAssetFromBundle(BundleAssetLoadingInfo info, string assetName)
        {
            if (LoadedAssetBundles.ContainsKey(info.FullPath))
            {
                var asset = LoadedAssetBundles[info.FullPath].LoadAsset(assetName);
                if (asset == null)
                {
                    Trainworks.Log(BepInEx.Logging.LogLevel.Warning, "Custom asset: " + assetName + " failed to load from bundle: " + info.FullPath);
                }
                ApplyImportSettings(info, ref asset);
                return asset;
            }
            Trainworks.Log(BepInEx.Logging.LogLevel.Warning, "Attempting to load asset from non-existent bundle: " + info.FullPath);
            return null;
        }

        /// <summary>
        /// Unload an asset bundle.
        /// If you no longer need the assets stored in the bundle,
        /// you should unload it to free up memory.
        /// </summary>
        /// <param name="bundleName">Name of the asset bundle to unload</param>
        /// <param name="unloadAllLoadedObjects">If true, also unloads all instances of assets in the bundle</param>
        public static void UnloadAssetBundle(string bundleName, bool unloadAllLoadedObjects = true)
        {
            if (LoadedAssetBundles.ContainsKey(bundleName))
            {
                LoadedAssetBundles[bundleName].Unload(unloadAllLoadedObjects);
                LoadedAssetBundles.Remove(bundleName);
            }
        }
    }
}
