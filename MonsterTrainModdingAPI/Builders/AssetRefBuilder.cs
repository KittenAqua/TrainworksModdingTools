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
using MonsterTrainModdingAPI.Managers;
using MonsterTrainModdingAPI.Utilities;

namespace MonsterTrainModdingAPI.Builders
{
    public class AssetRefBuilder
    {
        /// <summary>
        /// Don't set directly; use Filename instead.
        /// This is the absolute path to the file.
        /// </summary>
        public string filename;

        /// <summary>
        /// This is the absolute path to the file. Implictly sets AssetID if null.
        /// </summary>
        public string Filename
        {
            get { return filename; }
            set
            {
                this.filename = value;
                if (this.AssetID == null)
                {
                    this.AssetID = value;
                }
            }
        }

        public string DebugName { get; set; }
        public string AssetID { get; set; }

        public AssetTypeEnum AssetType { get; set; }

        /// <summary>
        /// Don't bother setting manually; the builder will set this automatically.
        /// </summary>
        public string AssetGUID { get; set; }

        public AssetReferenceGameObject BuildAndRegister()
        {
            var assetRef = this.Build();
            CustomAssetManager.RegisterCustomAsset(this.AssetGUID, this.Filename, this.AssetType);
            return assetRef;
        }

        public AssetReferenceGameObject Build()
        {
            var assetRef = new AssetReferenceGameObject();
            AccessTools.Field(typeof(AssetReferenceGameObject), "m_debugName").SetValue(assetRef, this.DebugName);
            this.AssetGUID = GUIDGenerator.GenerateDeterministicGUID(this.AssetID);
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
