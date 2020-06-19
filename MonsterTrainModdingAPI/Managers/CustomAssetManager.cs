using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using BepInEx.Configuration;
using System.IO;
using System.Reflection;
using HarmonyLib;

namespace MonsterTrainModdingAPI.Managers
{
    /// <summary>
    /// Handles loading custom assets, both raw and from asset bundles.
    /// Assets should be placed in the BepInEx/plugins folder.
    /// </summary>
    public class CustomAssetManager
    {
        /// <summary>
        /// Maps path to asset bundle.
        /// </summary>
        private static Dictionary<string, AssetBundle> LoadedAssetBundles { get; } = new Dictionary<string, AssetBundle>();

        /// <summary>
        /// Create a sprite from texture at provided path.
        /// </summary>
        /// <param name="path">Path of the texture relative to the BepInEx/plugins folder</param>
        /// <returns>The sprite, or null if there is no texture at the given path</returns>
        public static Sprite LoadSpriteFromPath(string path)
        {
            string fullPath = "BepInEx/plugins/" + path;
            if (File.Exists(fullPath))
            {
                // Create the card sprite
                byte[] fileData = File.ReadAllBytes(fullPath);
                Texture2D tex = new Texture2D(1, 1);
                UnityEngine.ImageConversion.LoadImage(tex, fileData);
                Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 128f);
                return sprite;
            }
            return null;
        }

        /// <summary>
        /// Load an asset from an asset bundle
        /// </summary>
        /// <typeparam name="T">Type of the asset to load</typeparam>
        /// <param name="info">Loading info for the asset</param>
        /// <returns>The asset specified by the given info</returns>
        public static T LoadAssetFromBundle<T>(AssetBundleLoadingInfo info) where T : UnityEngine.Object
        {
            return LoadAssetFromBundle<T>(info.AssetName, info.BundlePath);
        }

        /// <summary>
        /// Load an asset from an asset bundle
        /// </summary>
        /// <typeparam name="T">Type of the asset to load</typeparam>
        /// <param name="assetName">Name of the asset to load</param>
        /// <param name="bundlePath">Path to the bundle containing the asset to load</param>
        /// <returns></returns>
        public static T LoadAssetFromBundle<T>(string assetName, string bundlePath = "") where T : UnityEngine.Object
        {
            return (T)LoadAssetBundleFromPath(bundlePath).LoadAsset(assetName);
        }

        /// <summary>
        /// Loads an asset bundle from the path provided.
        /// </summary>
        /// <param name="path">Path of the bundle relative to the BepInEx/plugins folder</param>
        /// <returns></returns>
        public static AssetBundle LoadAssetBundleFromPath(string path)
        {
            if (LoadedAssetBundles.ContainsKey(path))
            {
                return LoadedAssetBundles[path];
            }
            string fullPath = "BepInEx/plugins/" + path;
            AssetBundle bundle = AssetBundle.LoadFromFile(fullPath);
            LoadedAssetBundles.Add(path, bundle);
            return bundle;
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

        /// <summary>
        /// Helper class optionally used to aid in loading asset bundles.
        /// </summary>
        public class AssetBundleLoadingInfo
        {
            public string AssetName { get; set; }
            public string BundlePath { get; set; }
            public AssetBundleLoadingInfo(string assetName, string bundlePath = "")
            {
                this.AssetName = assetName;
                this.BundlePath = bundlePath;
            }
        }
    }
}
