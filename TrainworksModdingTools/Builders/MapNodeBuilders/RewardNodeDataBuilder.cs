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
using UnityEngine.UI;

namespace Trainworks.Builders
{
    public class RewardNodeDataBuilder
    {
        /// <summary>
        /// ID of the reward node.
        /// </summary>
        public string RewardNodeID { get; set; }

        /// <summary>
        /// Name for the node.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Description for the node.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Localization key for the node's name.
        /// If both this and Name are set, Name will override this.
        /// </summary>
        public string TooltipTitleKey { get; set; }
        /// <summary>
        /// Localization key for the node's description.
        /// If both this and Description are set, Description will override this.
        /// </summary>
        public string TooltipBodyKey { get; set; }

        /// <summary>
        /// Set automatically in the constructor. Base asset path, usually the plugin directory.
        /// </summary>
        public string BaseAssetPath { get; set; }
        /// <summary>
        /// Sprite used when the node is selected by the controller.
        /// </summary>
        public string ControllerSelectedOutline { get; set; }
        /// <summary>
        /// Sprite used when the node is on the same path but has not been visited.
        /// </summary>
        /// ControllerSelectedOutline
        public string EnabledSpritePath { get; set; }
        /// <summary>
        /// Sprite used when the node is on the same path and has been visited.
        /// </summary>
        public string EnabledVisitedSpritePath { get; set; }
        /// <summary>
        /// Sprite used when the node is on a different path.
        /// </summary>
        public string DisabledSpritePath { get; set; }
        /// <summary>
        /// Sprite used when the node cannot be visited because it already has been.
        /// </summary>
        public string DisabledVisitedSpritePath { get; set; }
        /// <summary>
        /// Sprite used when the node is on a path in a future zone, which is still frozen.
        /// </summary>
        public string FrozenSpritePath { get; set; }
        /// <summary>
        /// Sprite used for the mouseover glow effect. Currently unused.
        /// </summary>
        public string GlowSpritePath { get; set; }

        /// <summary>
        /// The IDs of all map node pools the reward node should be inserted into.
        /// </summary>
        public List<string> MapNodePoolIDs { get; set; }

        public bool GrantImmediately { get; set; }
        public bool OverrideTooltipTitleBody { get; set; }

        /// <summary>
        /// This node will not be selected for your run's map unless your clan matches the one specified here
        /// </summary>
        public ClassData RequiredClass { get; set; }

        public List<IRewardDataBuilder> RewardBuilders { get; set; }
        public List<RewardData> Rewards { get; set; }

        public List<MapNodeData> IgnoreIfNodesPresent { get; set; }
        public string MapIconPath { get; set; }
        public string MinimapIconPath { get; set; }
        /// <summary>
        /// Clickable game object representing the node
        /// </summary>
        public MapNodeIcon MapIconPrefab { get; set; }
        public string NodeSelectedSfxCue { get; set; }
        public bool SkipCheckIfFullHealth { get; set; }
        public bool SkipCheckInBattleMode { get; set; }

        public RewardNodeDataBuilder()
        {
            this.MapNodePoolIDs = new List<string>();
            this.Rewards = new List<RewardData>();
            this.RewardBuilders = new List<IRewardDataBuilder>();

            var assembly = Assembly.GetCallingAssembly();
            this.BaseAssetPath = PluginManager.PluginGUIDToPath[PluginManager.AssemblyNameToPluginGUID[assembly.FullName]];
        }

        /// <summary>
        /// Builds the RewardNodeData represented by this builder's parameters recursively
        /// and registers it and its components with the appropriate managers.
        /// </summary>
        /// <returns>The newly registered RewardNodeData</returns>
        public RewardNodeData BuildAndRegister()
        {
            var rewardNodeData = this.Build();
            CustomMapNodePoolManager.RegisterCustomRewardNode(rewardNodeData, this.MapNodePoolIDs);
            return rewardNodeData;
        }

        /// <summary>
        /// Builds the RewardNodeData represented by this builder's parameters recursively;
        /// all Builders represented in this class's various fields will also be built.
        /// </summary>
        /// <returns>The newly created RewardNodeData</returns>
        public RewardNodeData Build()
        {
            foreach (var builder in this.RewardBuilders)
            {
                this.Rewards.Add(builder.Build());
            }
            RewardNodeData rewardNodeData = ScriptableObject.CreateInstance<RewardNodeData>();
            AccessTools.Field(typeof(GameData), "id").SetValue(rewardNodeData, this.RewardNodeID);
            AccessTools.Field(typeof(MapNodeData), "ignoreIfNodesPresent").SetValue(rewardNodeData, this.IgnoreIfNodesPresent);
            AccessTools.Field(typeof(MapNodeData), "mapIcon").SetValue(rewardNodeData, CustomAssetManager.LoadSpriteFromPath(this.BaseAssetPath + "/" + this.MapIconPath));
            if (this.MapIconPrefab == null)
            { // These are too complicated to create from scratch, so by default we copy from an existing game banner and apply our sprites to it
                RewardNodeData copyBanner = (ProviderManager.SaveManager.GetAllGameData().FindMapNodeData("5f35b7b7-75d1-4957-9f78-7d2072237038") as RewardNodeData);
                this.MapIconPrefab = GameObject.Instantiate(copyBanner.GetMapIconPrefab());
                this.MapIconPrefab.transform.parent = null;
                this.MapIconPrefab.name = this.RewardNodeID;
                GameObject.DontDestroyOnLoad(this.MapIconPrefab);
                var images = this.MapIconPrefab.GetComponentsInChildren<Image>(true);
                List<string> spritePaths = new List<string>
                { // This is the order they're listed on the prefab
                    this.ControllerSelectedOutline,
                    this.EnabledSpritePath,
                    this.EnabledVisitedSpritePath,
                    this.DisabledVisitedSpritePath,
                    this.DisabledSpritePath,
                    this.FrozenSpritePath
                };
                for (int i = 0; i < images.Length; i++)
                { // This method of modifying the image's sprite has the unfortunate side-effect of removing the white mouse-over outline
                    var sprite = CustomAssetManager.LoadSpriteFromPath(this.BaseAssetPath + "/" + spritePaths[i]);
                    if (sprite != null)
                    {
                        images[i].sprite = sprite;
                        images[i].material = null;
                    }
                }
            }
            AccessTools.Field(typeof(MapNodeData), "mapIconPrefab").SetValue(rewardNodeData, this.MapIconPrefab);
            AccessTools.Field(typeof(MapNodeData), "minimapIcon").SetValue(rewardNodeData, CustomAssetManager.LoadSpriteFromPath(this.BaseAssetPath + "/" + this.MinimapIconPath));
            AccessTools.Field(typeof(MapNodeData), "nodeSelectedSfxCue").SetValue(rewardNodeData, this.NodeSelectedSfxCue);
            if (this.SkipCheckIfFullHealth && this.SkipCheckInBattleMode)
            {
                AccessTools.Field(typeof(MapNodeData), "skipCheckSettings").SetValue(rewardNodeData, MapNodeData.SkipCheckSettings.Always | MapNodeData.SkipCheckSettings.IfFullHealth | MapNodeData.SkipCheckSettings.InBattleMode );
            }
            else if (this.SkipCheckIfFullHealth)
            {
                AccessTools.Field(typeof(MapNodeData), "skipCheckSettings").SetValue(rewardNodeData, MapNodeData.SkipCheckSettings.Always | MapNodeData.SkipCheckSettings.IfFullHealth);
            }
            else if (this.SkipCheckInBattleMode)
            {
                AccessTools.Field(typeof(MapNodeData), "skipCheckSettings").SetValue(rewardNodeData, MapNodeData.SkipCheckSettings.Always | MapNodeData.SkipCheckSettings.InBattleMode);
            }
            else
            {
                AccessTools.Field(typeof(MapNodeData), "skipCheckSettings").SetValue(rewardNodeData, MapNodeData.SkipCheckSettings.Always);
            }
            if (this.Description != null)
            {
                this.TooltipBodyKey = "RewardNodeData_" + this.RewardNodeID + "_TooltipBodyKey";
                // Use Description field for all languages
                // This should be changed in the future to add proper localization support to custom content
                CustomLocalizationManager.ImportSingleLocalization(this.TooltipBodyKey, "Text", "", "", "", "", this.Description, this.Description, this.Description, this.Description, this.Description, this.Description);
            }
            AccessTools.Field(typeof(MapNodeData), "tooltipBodyKey").SetValue(rewardNodeData, this.TooltipBodyKey);
            if (this.Name != null)
            {
                this.TooltipTitleKey = "RewardNodeData_" + this.RewardNodeID + "_TooltipTitleKey";
                // Use Name field for all languages
                // This should be changed in the future to add proper localization support to custom content
                CustomLocalizationManager.ImportSingleLocalization(this.TooltipTitleKey, "Text", "", "", "", "", this.Name, this.Name, this.Name, this.Name, this.Name, this.Name);
            }
            AccessTools.Field(typeof(MapNodeData), "tooltipTitleKey").SetValue(rewardNodeData, this.TooltipTitleKey);
            AccessTools.Field(typeof(RewardNodeData), "grantImmediately").SetValue(rewardNodeData, this.GrantImmediately);
            AccessTools.Field(typeof(RewardNodeData), "OverrideTooltipTitleBody").SetValue(rewardNodeData, this.OverrideTooltipTitleBody);
            AccessTools.Field(typeof(RewardNodeData), "requiredClass").SetValue(rewardNodeData, this.RequiredClass);
            AccessTools.Field(typeof(RewardNodeData), "rewards").SetValue(rewardNodeData, this.Rewards);
            return rewardNodeData;
        }
    }
}
