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
using Trainworks.Utilities;
using System.Text;

namespace Trainworks.Builders
{
    public class AssetRefBuilder
    {
        /// <summary>
        /// All necessary information to load an asset
        /// </summary>
        public AssetLoadingInfo AssetLoadingInfo { get; set; }

        /// <summary>
        /// Unique identifier for the asset. Set automatically by the builder.
        /// </summary>
        private string AssetGUID { get; set; }

        /// <summary>
        /// ID used to generate the asset GUID
        /// </summary>
        private string AssetID
        {
            get
            {
                if (this.AssetLoadingInfo != null)
                {
                    var idStringBuilder = new StringBuilder();
                    idStringBuilder.Append(this.AssetLoadingInfo.FullPath);
                    if (this.AssetLoadingInfo is BundleAssetLoadingInfo)
                    {
                        var bundleInfo = this.AssetLoadingInfo as BundleAssetLoadingInfo;
                        idStringBuilder.Append("/" + bundleInfo.SpriteName + " " + bundleInfo.ObjectName);
                    }
                    idStringBuilder.Append(this.AssetLoadingInfo.AssetType);
                    return idStringBuilder.ToString();
                }
                return "none";
            }
        }

        public AssetReferenceGameObject BuildAndRegister()
        {
            var assetRef = this.Build();
            CustomAssetManager.RegisterCustomAsset(this.AssetGUID, this.AssetLoadingInfo);
            if (this.AssetLoadingInfo is BundleAssetLoadingInfo)
            {
                BundleManager.RegisterBundle(this.AssetGUID, this.AssetLoadingInfo as BundleAssetLoadingInfo);
            }
            return assetRef;
        }

        public AssetReferenceGameObject Build()
        {
            this.AssetGUID = GUIDGenerator.GenerateDeterministicGUID(this.AssetID);
            var assetRef = new AssetReferenceGameObject();
            AccessTools.Field(typeof(AssetReferenceGameObject), "m_debugName").SetValue(assetRef, this.AssetLoadingInfo.FilePath);
            AccessTools.Field(typeof(AssetReferenceGameObject), "m_AssetGUID").SetValue(assetRef, this.AssetGUID);
            return assetRef;
        }

        public enum AssetTypeEnum
        {
            CardArt,
            Character
        }
    }
}
