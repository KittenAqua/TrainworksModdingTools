using Trainworks.Builders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Trainworks.Utilities
{
    public class AssetLoadingInfo
    {
        /// <summary>
        /// Path relative to your plugin
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// This will get set for you. Don't set it manually.
        /// </summary>
        public string PluginPath { get; set; }

        /// <summary>
        /// The asset type to load
        /// </summary>
        public AssetRefBuilder.AssetTypeEnum AssetType { get; set; }

        /// <summary>
        /// Concatenates plugin path and bundle path
        /// </summary>
        public string FullPath
        {
            get
            {
                return Path.Combine(PluginPath, FilePath);
            }
        }
    }

    /// <summary>
    /// Helper class to aid in loading asset bundles.
    /// </summary>
    public class BundleAssetLoadingInfo : AssetLoadingInfo
    {
        public ISettings ImportSettings { get; set; }

        /// <summary>
        /// Path to the asset sprite in the bundle.
        /// Even if you're making a spine character, you *must* set this for the preview sprite.
        /// </summary>
        public string SpriteName { get; set; }

        /// <summary>
        /// Optional path to the spine skeleton data in the bundle.
        /// </summary>
        public string ObjectName { get; set; }
    }
}
