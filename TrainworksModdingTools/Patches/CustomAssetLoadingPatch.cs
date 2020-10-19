using System;
using System.Collections.Generic;
using BepInEx;
using BepInEx.Harmony;
using System.Reflection;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using UnityEngine.AddressableAssets;
using ShinyShoe;
using Trainworks.Managers;

namespace Trainworks.Patches
{
    /// <summary>
    /// Override the asset loading procedure when loading custom assets
    /// </summary>
    [HarmonyPatch(typeof(AssetLoadingManager), "StartLoad", new Type[] { typeof(AssetReference), typeof(IAddressableAssetOwner), typeof(bool) })]
    class CustomAssetLoadingPatch
    {
        static MethodInfo setAssetInfoAssetMethod;

        static bool Prefix(ref AssetLoadingManager __instance, 
                            ref Dictionary<Hash128, AssetLoadingManager.AssetInfo> ____assetsLoaded, 
                            ref int ____numLoadingTasksRunning, 
                            ref AssetReference assetRef, 
                            ref IAddressableAssetOwner assetOwner
                           )
        {
            if (CustomAssetManager.RuntimeKeyToAssetInfo.ContainsKey(assetRef.RuntimeKey))
            {
                if (____assetsLoaded.TryGetValue(assetRef.RuntimeKey, out AssetLoadingManager.AssetInfo info))
                {
                    info.assetCount++;
                }
                else
                {
                    info = new AssetLoadingManager.AssetInfo(assetRef);
                    ____assetsLoaded[assetRef.RuntimeKey] = info;
                }
                if (!info.Loading && !info.Loaded)
                {
                    info.status = AssetLoadingManager.AssetStatus.Loading;
                    ____numLoadingTasksRunning++;

                    // TODO: asynchronize this load
                    var asset = CustomAssetManager.LoadGameObjectFromAssetRef(assetRef);

                    ____numLoadingTasksRunning--;

                    if (setAssetInfoAssetMethod == null)
                    {
                        setAssetInfoAssetMethod = AccessTools.Method(typeof(AssetLoadingManager), "SetAssetInfoAsset");
                    }
                    setAssetInfoAssetMethod.Invoke(__instance, new object[] { info, asset });
                    info.status = AssetLoadingManager.AssetStatus.Loaded;
                }
                return false;
            }
            return true;
        }
    }
}
