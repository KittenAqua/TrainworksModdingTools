using System;
using System.Collections.Generic;
using System.IO;
using BepInEx;
using BepInEx.Harmony;
using System.Reflection;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using UnityEngine.AddressableAssets;
using ShinyShoe;
using MonsterTrainModdingAPI.Managers;

namespace MonsterTrainModdingAPI.Builders
{
    public class CollectableRelicDataBuilder
    {
        /// <summary>
        /// Unique string used to store and retrieve the relic data.
        /// </summary>
        public string CollectableRelicID { get; set; }

        /// <summary>
        /// Name displayed for the relic.
        /// Overridden by the NameKey field.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Localization key for the relic's name.
        /// Overrides the Name field.
        /// </summary>
        public string NameKey { get; set; }
        /// <summary>
        /// Description displayed for the relic.
        /// Overridden by the DescriptionKey field.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Localization key for the relic's description.
        /// Overrides the Description field.
        /// </summary>
        public string DescriptionKey { get; set; }
        /// <summary>
        /// Custom asset path to load relic art from.
        /// </summary>
        public string AssetPath { get; set; }

        /// <summary>
        /// ID of the clan the card is a part of. Leave null for clanless.
        /// Base game clan IDs should be retrieved via helper class "MTClanIDs".
        /// </summary>
        public string ClanID { get; set; }

        public List<RelicEffectDataBuilder> EffectBuilders { get; set; }
        public List<RelicEffectData> Effects { get; set; }
        public CollectableRarity Rarity { get; set; }

        public ClassData LinkedClass { get; set; }
        public int UnlockLevel { get; set; }

        public string RelicActivatedKey { get; set; }
        public List<string> RelicLoreTooltipKeys { get; set; }

        public bool FromStoryEvent { get; set; }
        public bool IsBossGivenRelic { get; set; }

        public Sprite Icon { get; set; }

        public CollectableRelicDataBuilder()
        {
            this.Name = "";
            this.Description = "EmptyString-0000000000000000-00000000000000000000000000000000-v2";

            this.Effects = new List<RelicEffectData>();
            this.EffectBuilders = new List<RelicEffectDataBuilder>();
        }

        /// <summary>
        /// Builds the RelicData represented by this builder's parameters recursively
        /// and registers it and its components with the appropriate managers.
        /// </summary>
        /// <returns>The newly registered RelicData</returns>
        public CollectableRelicData BuildAndRegister()
        {
            var relicData = this.Build();
            CustomCollectableRelicManager.RegisterCustomRelic(relicData);
            return relicData;
        }

        /// <summary>
        /// Builds the RelicData represented by this builder's parameters recursively;
        /// all Builders represented in this class's various fields will also be built.
        /// </summary>
        /// <returns>The newly created RelicData</returns>
        public CollectableRelicData Build()
        {
            foreach (var builder in this.EffectBuilders)
            {
                this.Effects.Add(builder.Build());
            }
            this.LinkedClass = CustomCardManager.SaveManager.GetAllGameData().FindClassData(this.ClanID);

            var relicData = new CollectableRelicData();

            AccessTools.Field(typeof(GameData), "id").SetValue(relicData, this.CollectableRelicID);
            relicData.name = this.CollectableRelicID;
            // RelicData fields
            if (this.DescriptionKey == null)
            {
                this.DescriptionKey = this.CollectableRelicID + "Relic_DescriptionKey";
                // Use Description field for all languages
                // This should be changed in the future to add proper localization support to custom content
                CustomLocalizationManager.ImportSingleLocalization(this.DescriptionKey, "Text", "", "", "", "", this.Description, this.Description, this.Description, this.Description, this.Description, this.Description);
            }
            AccessTools.Field(typeof(RelicData), "descriptionKey").SetValue(relicData, this.DescriptionKey);
            AccessTools.Field(typeof(RelicData), "effects").SetValue(relicData, this.Effects);
            if (this.Icon == null && this.AssetPath != null)
            {
                string path = "BepInEx/plugins/" + this.AssetPath;
                if (File.Exists(path))
                {
                    byte[] fileData = File.ReadAllBytes(path);
                    Texture2D tex = new Texture2D(1, 1);
                    UnityEngine.ImageConversion.LoadImage(tex, fileData);
                    this.Icon = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 128f);
                }
            }
            AccessTools.Field(typeof(RelicData), "icon").SetValue(relicData, this.Icon);
            if (this.NameKey == null)
            {
                this.NameKey = this.CollectableRelicID + "Relic_NameKey";
                // Use Name field for all languages
                // This should be changed in the future to add proper localization support to custom content
                CustomLocalizationManager.ImportSingleLocalization(this.NameKey, "Text", "", "", "", "", this.Name, this.Name, this.Name, this.Name, this.Name, this.Name);
            }
            AccessTools.Field(typeof(RelicData), "nameKey").SetValue(relicData, this.NameKey);
            AccessTools.Field(typeof(RelicData), "relicActivatedKey").SetValue(relicData, this.RelicActivatedKey);
            AccessTools.Field(typeof(RelicData), "relicLoreTooltipKeys").SetValue(relicData, this.RelicLoreTooltipKeys);

            // CollectableRelicData fields
            AccessTools.Field(typeof(CollectableRelicData), "fromStoryEvent").SetValue(relicData, this.FromStoryEvent);
            AccessTools.Field(typeof(CollectableRelicData), "isBossGivenRelic").SetValue(relicData, this.IsBossGivenRelic);
            AccessTools.Field(typeof(CollectableRelicData), "linkedClass").SetValue(relicData, this.LinkedClass);
            AccessTools.Field(typeof(CollectableRelicData), "rarity").SetValue(relicData, this.Rarity);
            AccessTools.Field(typeof(CollectableRelicData), "unlockLevel").SetValue(relicData, this.UnlockLevel);
            return relicData;
        }

        /// <summary>
        /// Sets this card's clan to the clan whose ID is passed in
        /// </summary>
        /// <param name="clanID">ID of the clan, most easily retrieved using the helper class "MTClanIDs"</param>
        public void SetClan(string clanID)
        {
            this.ClanID = clanID;
        }
    }
}
