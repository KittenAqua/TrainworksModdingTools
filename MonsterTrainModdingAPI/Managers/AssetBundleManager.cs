using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using BepInEx.Configuration;
using System.IO;
using System.Reflection;

namespace MonsterTrainModdingAPI.Managers
{
    public class AssetBundleManager
    {

        public static string BepInExFolderPath
        {
            get
            {
                return Path.Combine(Assembly.GetExecutingAssembly().Location, @"..\..\");
            }
        }
        public static string PluginFolderPath
        {
            get
            {
                return Path.Combine(Assembly.GetExecutingAssembly().Location, @"..\");
            }
        }

        public static T LoadAssetFromLocalPath<T>(string localPath, string bundleName, string assetName) where T : UnityEngine.Object
        {
            return (T)LoadAssetBundleFromLocalPath(localPath, bundleName).LoadAsset(assetName);
        }

        public static T LoadAssetFromGlobalPath<T>(string globalPath, string bundleName, string assetName) where T : UnityEngine.Object
        {
            return (T)LoadAssetBundleFromGlobalPath(globalPath, bundleName).LoadAsset(assetName);
        }


        public static UnityEngine.Object LoadAssetFromLocalPath(string localPath, string bundleName, string assetName)
        {
            return LoadAssetBundleFromLocalPath(localPath, bundleName).LoadAsset(assetName);
        }

        public static UnityEngine.Object LoadAssetFromGlobalPath(string globalPath, string bundleName, string assetName)
        {
            return LoadAssetBundleFromGlobalPath(globalPath, bundleName).LoadAsset(assetName);
        }

        public static AssetBundle LoadAssetBundleFromGlobalPath(string globalPath, string bundleName)
        {
            return AssetBundle.LoadFromFile(globalPath + bundleName);
        }

        public static AssetBundle LoadAssetBundleFromLocalPath(string localPath, string bundleName)
        {
            return LoadAssetBundleFromGlobalPath(PluginFolderPath, bundleName);
        }

    }
}
