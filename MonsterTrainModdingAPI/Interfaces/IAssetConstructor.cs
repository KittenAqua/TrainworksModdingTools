using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MonsterTrainModdingAPI.Interfaces
{
    public interface IAssetConstructor
    {
        GameObject Construct(AssetReference assetRef);
    }
}
