using System;
using System.Collections.Generic;
using System.Text;

namespace MonsterTrainModdingAPI
{
    public class AssetBundleLoadingInfo
    {
        public string bundleName;
        public string assetName;
        public string pathExtension;
        public AssetBundleLoadingInfo(string bundleName, string assetName, string pathExtension = "")
        {
            this.assetName = assetName;
            this.bundleName = bundleName;
            this.pathExtension = pathExtension;
        }
    }
}
