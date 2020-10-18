using TrainworksModdingTools.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TrainworksModdingTools.Interfaces
{
    public interface IAssetConstructor
    {
        GameObject Construct(AssetReference assetRef);
        GameObject Construct(AssetReference assetRef, BundleAssetLoadingInfo bundleInfo);
    }
}
