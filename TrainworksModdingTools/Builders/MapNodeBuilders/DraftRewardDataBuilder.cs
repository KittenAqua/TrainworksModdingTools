using System;
using System.Collections.Generic;
using BepInEx;
using BepInEx.Harmony;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using UnityEngine.AddressableAssets;
using ShinyShoe;
using Trainworks.Managers;
using Trainworks.Builders;

namespace Trainworks.Builders
{
    public class DraftRewardDataBuilder : IRewardDataBuilder
    {
        public string DraftRewardID { get; set; }

        /// <summary>
        /// Name of the reward data
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Description of the reward data
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Name title key, overridden by Name
        /// </summary>
        public string _RewardTitleKey { get; set; }
        /// <summary>
        /// Description title key, overridden by Description
        /// </summary>
        public string _RewardDescriptionKey { get; set; }

        public ClassData ClassDataOverride { get; set; }
        public RunState.ClassType ClassType { get; set; }
        public bool ClassTypeOverride { get; set; }
        /// <summary>
        /// Number of cards the banner offers
        /// </summary>
        public uint DraftOptionsCount { get; set; }
        /// <summary>
        /// Card pool the banner pulls from
        /// </summary>
        public CardPool DraftPool { get; set; }
        public bool GrantSingleCard { get; set; }
        public CollectableRarity RarityFloorOverride { get; set; }
        public bool UseRunRarityFloors { get; set; }

        public bool CanBeSkippedOverride { get; set; }
        public bool ForceContentUnlocked { get; set; }
        public SaveManager SaveManager { get; set; }

        public int[] Costs { get; set; }
        public bool ShowRewardAnimationInEvent { get; set; }
        public string _CollectSFXCueName { get; set; }
        public bool _IsServiceMerchantReward { get; set; }
        public string _RewardSpritePath { get; set; }
        public bool _ShowCancelOverride { get; set; }
        public bool _ShowRewardFlowInEvent { get; set; }
        public int _MerchantServiceIndex { get; set; }
        /// <summary>
        /// Set automatically in the constructor. Base asset path, usually the plugin directory.
        /// </summary>
        public string BaseAssetPath { get; set; }

        public DraftRewardDataBuilder()
        {
            this.Costs = new int[0];
            var assembly = Assembly.GetCallingAssembly();
            this.BaseAssetPath = PluginManager.PluginGUIDToPath[PluginManager.AssemblyNameToPluginGUID[assembly.FullName]];
        }

        /// <summary>
        /// Builds the RewardData represented by this builder's parameters recursively;
        /// all Builders represented in this class's various fields will also be built.
        /// </summary>
        /// <returns>The newly created RewardData</returns>
        public RewardData Build()
        {
            RewardData rewardData = ScriptableObject.CreateInstance<DraftRewardData>();
            AccessTools.Field(typeof(GameData), "id").SetValue(rewardData, this.DraftRewardID);
            AccessTools.Field(typeof(DraftRewardData), "classDataOverride").SetValue(rewardData, this.ClassDataOverride);
            AccessTools.Field(typeof(DraftRewardData), "classType").SetValue(rewardData, this.ClassType);
            AccessTools.Field(typeof(DraftRewardData), "classTypeOverride").SetValue(rewardData, this.ClassTypeOverride);
            AccessTools.Field(typeof(DraftRewardData), "draftOptionsCount").SetValue(rewardData, this.DraftOptionsCount);
            AccessTools.Field(typeof(DraftRewardData), "draftPool").SetValue(rewardData, this.DraftPool);
            AccessTools.Field(typeof(DraftRewardData), "grantSingleCard").SetValue(rewardData, this.GrantSingleCard);
            AccessTools.Field(typeof(DraftRewardData), "rarityFloorOverride").SetValue(rewardData, this.RarityFloorOverride);
            AccessTools.Field(typeof(DraftRewardData), "useRunRarityFloors").SetValue(rewardData, this.UseRunRarityFloors);
            AccessTools.Field(typeof(GrantableRewardData), "CanBeSkippedOverride").SetValue(rewardData, this.CanBeSkippedOverride);
            AccessTools.Field(typeof(GrantableRewardData), "ForceContentUnlocked").SetValue(rewardData, this.ForceContentUnlocked);
            AccessTools.Field(typeof(GrantableRewardData), "saveManager").SetValue(rewardData, this.SaveManager);
            AccessTools.Field(typeof(GrantableRewardData), "_isServiceMerchantReward").SetValue(rewardData, this._IsServiceMerchantReward);
            AccessTools.Field(typeof(GrantableRewardData), "_merchantServiceIndex").SetValue(rewardData, this._MerchantServiceIndex);
            AccessTools.Field(typeof(RewardData), "costs").SetValue(rewardData, this.Costs);
            AccessTools.Field(typeof(RewardData), "ShowRewardAnimationInEvent").SetValue(rewardData, this.ShowRewardAnimationInEvent);
            AccessTools.Field(typeof(RewardData), "_collectSFXCueName").SetValue(rewardData, this._CollectSFXCueName);
            if (this.Description != null)
            {
                this._RewardDescriptionKey = "DraftRewardData_" + this.DraftRewardID + "_RewardDescriptionKey";
                // Use Description field for all languages
                // This should be changed in the future to add proper localization support to custom content
                CustomLocalizationManager.ImportSingleLocalization(this._RewardDescriptionKey, "Text", "", "", "", "", this.Description, this.Description, this.Description, this.Description, this.Description, this.Description);
            }
            AccessTools.Field(typeof(RewardData), "_rewardDescriptionKey").SetValue(rewardData, this._RewardDescriptionKey);
            AccessTools.Field(typeof(RewardData), "_rewardSprite").SetValue(rewardData, CustomAssetManager.LoadSpriteFromPath(this.BaseAssetPath + "/" + this._RewardSpritePath));
            if (this.Name != null)
            {
                this._RewardTitleKey = "DraftRewardData_" + this.DraftRewardID + "_RewardTitleKey";
                // Use Name field for all languages
                // This should be changed in the future to add proper localization support to custom content
                CustomLocalizationManager.ImportSingleLocalization(this._RewardTitleKey, "Text", "", "", "", "", this.Name, this.Name, this.Name, this.Name, this.Name, this.Name);
            }
            AccessTools.Field(typeof(RewardData), "_rewardTitleKey").SetValue(rewardData, this._RewardTitleKey);
            AccessTools.Field(typeof(RewardData), "_showCancelOverride").SetValue(rewardData, this._ShowCancelOverride);
            AccessTools.Field(typeof(RewardData), "_showRewardFlowInEvent").SetValue(rewardData, this._ShowRewardFlowInEvent);
            return rewardData;
        }
    }
}
