using System;
using System.Collections.Generic;
using BepInEx;
using BepInEx.Harmony;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using UnityEngine.AddressableAssets;
using ShinyShoe;
using MonsterTrainModdingAPI.Managers;
using MonsterTrainModdingAPI.Enums.MTStatusEffects;

namespace MonsterTrainModdingAPI.Builders
{
    public class RewardNodeDataBuilder
    {
        /// <summary>
        /// ID of the reward node.
        /// </summary>
        public string RewardNodeID { get; set; }

        /// <summary>
        /// The IDs of all map node pools the reward node should be inserted into.
        /// </summary>
        public List<string> MapNodePoolIDs { get; set; }

        public bool GrantImmediately { get; set; }
        public bool OverrideTooltipTitleBody { get; set; }
        public ClassData RequiredClass { get; set; }
        public List<RewardData> Rewards { get; set; }

        public List<MapNodeData> IgnoreIfNodesPresent { get; set; }
        public Sprite MapIcon { get; set; }
        public MapNodeIcon MapIconPrefab { get; set; }
        public Sprite MinimapIcon { get; set; }
        public string NodeSelectedSfxCue { get; set; }
        public bool SkipCheckIfFullHealth { get; set; }
        public bool SkipCheckInBattleMode { get; set; }
        public string TooltipBodyKey { get; set; }
        public string TooltipTitleKey { get; set; }

        public RewardNodeDataBuilder()
        {
            this.MapNodePoolIDs = new List<string>();
        }

        /// <summary>
        /// Builds the RewardNodeData represented by this builder's parameters recursively
        /// and registers it and its components with the appropriate managers.
        /// </summary>
        /// <returns>The newly registered RewardNodeData</returns>
        public RewardNodeData BuildAndRegister()
        {
            var rewardNodeData = this.Build();
            CustomMapNodeManager.RegisterCustomRewardNode(rewardNodeData, this.MapNodePoolIDs);
            return rewardNodeData;
        }

        /// <summary>
        /// Builds the RewardNodeData represented by this builder's parameters recursively;
        /// all Builders represented in this class's various fields will also be built.
        /// </summary>
        /// <returns>The newly created RewardNodeData</returns>
        public RewardNodeData Build()
        {
            RewardNodeData rewardNodeData = ScriptableObject.CreateInstance<RewardNodeData>();
            AccessTools.Field(typeof(GameData), "id").SetValue(rewardNodeData, this.RewardNodeID);
            AccessTools.Field(typeof(MapNodeData), "ignoreIfNodesPresent").SetValue(rewardNodeData, this.IgnoreIfNodesPresent);
            AccessTools.Field(typeof(MapNodeData), "mapIcon").SetValue(rewardNodeData, this.MapIcon);
            AccessTools.Field(typeof(MapNodeData), "mapIconPrefab").SetValue(rewardNodeData, this.MapIconPrefab);
            AccessTools.Field(typeof(MapNodeData), "minimapIcon").SetValue(rewardNodeData, this.MapNodePoolIDs);
            AccessTools.Field(typeof(MapNodeData), "nodeSelectedSfxCue").SetValue(rewardNodeData, this.NodeSelectedSfxCue);
            AccessTools.Field(typeof(MapNodeData), "skipCheckIfFullHealth").SetValue(rewardNodeData, this.SkipCheckIfFullHealth);
            AccessTools.Field(typeof(MapNodeData), "skipCheckInBattleMode").SetValue(rewardNodeData, this.SkipCheckInBattleMode);
            AccessTools.Field(typeof(MapNodeData), "tooltipBodyKey").SetValue(rewardNodeData, this.TooltipBodyKey);
            AccessTools.Field(typeof(MapNodeData), "tooltipTitleKey").SetValue(rewardNodeData, this.TooltipTitleKey);
            AccessTools.Field(typeof(RewardNodeData), "grantImmediately").SetValue(rewardNodeData, this.GrantImmediately);
            AccessTools.Field(typeof(RewardNodeData), "OverrideTooltipTitleBody").SetValue(rewardNodeData, this.OverrideTooltipTitleBody);
            AccessTools.Field(typeof(RewardNodeData), "requiredClass").SetValue(rewardNodeData, this.RequiredClass);
            AccessTools.Field(typeof(RewardNodeData), "rewards").SetValue(rewardNodeData, this.Rewards);
            return rewardNodeData;
        }
    }
}
