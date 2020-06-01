using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using BepInEx.Configuration;
using System.IO;
using System.Reflection;

namespace MonsterTrainModdingAPI.Utilities
{
    public class AssetBundleUtils
    {
        private static string _pluginFolderPath;
        public static string PluginFolderPath
        {
            get
            {
                if(_pluginFolderPath == null) _pluginFolderPath = Directory.GetCurrentDirectory() + @"\BepInEx\plugins";
                return _pluginFolderPath;
            }
        }

        public static T LoadAssetFromPath<T>(string bundleName, string assetName, string pathExtension = "") where T : UnityEngine.Object
        {
            return (T)LoadAssetFromPath(bundleName, assetName, pathExtension);
        }


        private static UnityEngine.Object LoadAssetFromPath(string bundleName, string assetName, string path = "")
        {
            return LoadAssetBundleFromPath(bundleName, path).LoadAsset(assetName);
        }

        private static AssetBundle LoadAssetBundleFromPath(string bundleName, string path = "")
        {
            string AccessPath;
            if(path == null || path == "")
            {
                AccessPath = PluginFolderPath + @"\" + bundleName;
            }
            else
            {
                AccessPath = PluginFolderPath + @"\" + path + @"\" + bundleName;
            }
            Console.WriteLine(AccessPath);
            return AssetBundle.LoadFromFile(AccessPath);
        }

    }
}
