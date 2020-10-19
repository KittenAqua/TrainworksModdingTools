using Trainworks.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Trainworks.Interfaces
{
    public interface IAssetConstructor
    {
        GameObject Construct(AssetReference assetRef);
        GameObject Construct(AssetReference assetRef, BundleAssetLoadingInfo bundleInfo);
    }
}
