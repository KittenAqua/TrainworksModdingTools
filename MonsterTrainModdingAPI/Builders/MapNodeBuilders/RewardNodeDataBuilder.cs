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
using UnityEngine.UI;

namespace MonsterTrainModdingAPI.Builders
{
    public class RewardNodeDataBuilder
    {
        /// <summary>
        /// ID of the reward node.
        /// </summary>
        public string RewardNodeID { get; set; }

        public string EnabledSpritePath { get; set; }
        public string EnabledVisitedSpritePath { get; set; }
        public string DisabledSpritePath { get; set; }
        public string DisabledVisitedSpritePath { get; set; }
        public string FrozenSpritePath { get; set; }
        public string GlowSpritePath { get; set; }

        /// <summary>
        /// The IDs of all map node pools the reward node should be inserted into.
        /// </summary>
        public List<string> MapNodePoolIDs { get; set; }

        public bool GrantImmediately { get; set; }
        public bool OverrideTooltipTitleBody { get; set; }
        public ClassData RequiredClass { get; set; }

        public List<IRewardDataBuilder> RewardBuilders { get; set; }
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
            this.Rewards = new List<RewardData>();
            this.RewardBuilders = new List<IRewardDataBuilder>();
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
            foreach (var builder in this.RewardBuilders)
            {
                this.Rewards.Add(builder.Build());
            }
            RewardNodeData rewardNodeData = ScriptableObject.CreateInstance<RewardNodeData>();
            AccessTools.Field(typeof(GameData), "id").SetValue(rewardNodeData, this.RewardNodeID);
            AccessTools.Field(typeof(MapNodeData), "ignoreIfNodesPresent").SetValue(rewardNodeData, this.IgnoreIfNodesPresent);
            AccessTools.Field(typeof(MapNodeData), "mapIcon").SetValue(rewardNodeData, this.MapIcon);
            if (this.MapIconPrefab == null)
            { // These are too complicated to create from scratch, so by default we copy from an existing game banner and apply our sprites to it
                RewardNodeData copyBanner = (CustomMapNodeManager.SaveManager.GetAllGameData().FindMapNodeData("5f35b7b7-75d1-4957-9f78-7d2072237038") as RewardNodeData);
                this.MapIconPrefab = GameObject.Instantiate(copyBanner.GetMapIconPrefab());
                this.MapIconPrefab.transform.parent = null;
                this.MapIconPrefab.name = this.RewardNodeID;
                GameObject.DontDestroyOnLoad(this.MapIconPrefab);
                var images = this.MapIconPrefab.GetComponentsInChildren<Image>(true);
                List<string> spritePaths = new List<string>
                { // This is the order they're listed on the prefab
                    this.EnabledSpritePath,
                    this.EnabledVisitedSpritePath,
                    this.DisabledVisitedSpritePath,
                    this.DisabledSpritePath,
                    this.FrozenSpritePath
                };
                for (int i = 0; i < images.Length; i++)
                { // This method of modifying the image's sprite has the unfortunate side-effect of removing the white mouse-over outline
                    var sprite = CustomAssetManager.LoadSpriteFromPath(spritePaths[i]);
                    if (sprite != null)
                    {
                        images[i].sprite = sprite;
                        images[i].material = null;
                    }
                }
            }
            AccessTools.Field(typeof(MapNodeData), "mapIconPrefab").SetValue(rewardNodeData, this.MapIconPrefab);
            AccessTools.Field(typeof(MapNodeData), "minimapIcon").SetValue(rewardNodeData, this.MinimapIcon);
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
