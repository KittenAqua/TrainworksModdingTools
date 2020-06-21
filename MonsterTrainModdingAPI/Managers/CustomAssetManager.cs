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
        public static T LoadAssetFromBundle<T>(string assetName, string bundlePath) where T : UnityEngine.Object
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
            public IDictionary<Type, ISettings> LoadingDictionary { get; private set; } = new Dictionary<Type, ISettings>();
            public string AssetName { get; set; }
            public string BundlePath { get; set; }
            public AssetBundleLoadingInfo(string assetName, string bundlePath)
            {
                this.AssetName = assetName;
                this.BundlePath = bundlePath;
            }

            public AssetBundleLoadingInfo AddImportSettings<T>(T ImportSettings) where T : ISettings
            {
                if (!ImportSettings.GetType().ContainsGenericParameters)
                {
                    throw new ArgumentException("");
                }
                Type type = ImportSettings.GetType().GetGenericArguments()[0];
                LoadingDictionary.Add(type, ImportSettings);
                return this;
            }
        }

        /// <summary>
        /// An Interface used to Represent Settings
        /// </summary>
        public interface ISettings { }
        /// <summary>
        /// A Generic Interface used to Represent Settings to Apply
        /// </summary>
        public interface ISettings<T> : ISettings
        {
            void ApplySettings(ref T @object);
        }

        public class GameObjectImportSettings : ISettings<GameObject>
        {
            /// <summary>
            /// A Function that is called after Settings are applied, use this to add your own Sprite Logic
            /// </summary>
            public Func<GameObject, GameObject> func { get; set; }
            public GameObjectImportSettings()
            {

            }
            public void ApplySettings(ref GameObject @object)
            {
                @object = func(@object);
            }
        }

        public class Texture2DImportSettings : ISettings<Texture2D>
        {
            /// <summary>
            /// A Function that is called after Settings are applied, use this to add your own Sprite Logic
            /// </summary>
            public Func<Texture2D, Texture2D> func { get; set; }
            public Texture2DImportSettings()
            {

            }
            public void ApplySettings(ref Texture2D @object)
            {
                //To Add: Logic
                
                //
                @object = func(@object);
            }
        }
        /// <summary>
        /// Import Settings for Sprites
        /// </summary>
        public class SpriteImportSettings : ISettings<Sprite>
        {
            /// <summary>
            /// A Function that is called after Settings are applied, use this to add your own Sprite Logic
            /// </summary>
            public Func<Sprite, Sprite> func { get; set; }
            /// <summary>
            /// Rectangular section of the texture to use for the sprite relative to the textures width and height
            /// </summary>
            public Rect rect { get; set; }
            /// <summary>
            /// Sprite's pivot point relative to its graphic rectangle
            /// </summary>
            public Vector2 pivot { get; set; }
            /// <summary>
            /// The number of pixels in the sprite that correspond to one unit in world space.
            /// </summary>
            public float pixelPerUnit { get; set; }
            /// <summary>
            /// Amount by which the sprite mesh should be expanded outwards.
            /// </summary>
            public uint extrude { get; set; }
            /// <summary>
            /// Controls the type of mesh generated for the sprite.
            /// </summary>
            public SpriteMeshType meshType { get; set; }
            /// <summary>
            /// The border sizes of the sprite (X=left, Y=bottom, Z=right, W=top).
            /// </summary>
            public Vector4 border { get; set; }
            /// <summary>
            /// Generates a default physics shape for the sprite.
            /// </summary>
            public bool generateFallbackPhysicsShape { get; set; }
            public SpriteImportSettings()
            {
                rect = new Rect(0, 0, 1, 1);
                pivot = new Vector2(0.5f, 0.5f);
                pixelPerUnit = 100f;
                extrude = 0;
                meshType = SpriteMeshType.Tight;
                border = Vector4.zero;
                generateFallbackPhysicsShape = true;
            }
            public void ApplySettings(ref Sprite @object)
            {
                //Runtime Editing of the Sprite using AccessTools May not be wise, So I elected for the creation of a new one.
                Sprite spr = Sprite.Create(
                    @object.texture,
                    new Rect(
                        @object.texture.width * rect.x,
                        @object.texture.height * rect.y,
                        @object.texture.width * rect.width,
                        @object.texture.height * rect.height),
                    pivot,
                    pixelPerUnit,
                    extrude,
                    meshType,
                    border,
                    generateFallbackPhysicsShape);
                @object = func(spr);
            }

        }
    }
}
