using HarmonyLib;
using Trainworks.Builders;
using System.Collections.Generic;
using Trainworks.Managers;
using UnityEngine;
using System.IO;
using System.Reflection;

namespace Trainworks.Builders
{
    public class EnhancerDataBuilder
    {
        /// <summary>
        /// Internal ID for use by Unity
        /// </summary>
        public string ID { get; set; }
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
        /// <summary>
        /// Holds the upgrade to be applied to the chosen card after the enhancer is purchased.
        /// </summary>
        public CardUpgradeDataBuilder Upgrade { get; set; }
        public CollectableRarity Rarity { get; set; }
        public ClassData LinkedClass { get; set; }
        /// <summary>
        /// Determines which kinds of cards it upgrades.
        /// </summary>
        public CardType CardType { get; set; }
        /// <summary>
        /// The IDs of all enhancer pools the relic should be inserted into.
        /// </summary>
        public List<string> EnhancerPoolIDs { get; set; }

        public string BaseAssetPath { get; set; }


        public EnhancerDataBuilder()
        {
            var assembly = Assembly.GetCallingAssembly();
            this.BaseAssetPath = PluginManager.PluginGUIDToPath[PluginManager.AssemblyNameToPluginGUID[assembly.FullName]];

            this.Name = "";
            this.Description = "EmptyString-0000000000000000-00000000000000000000000000000000-v2";
            this.EnhancerPoolIDs = new List<string>();
        }

        /// <summary>
        /// Builds the EnhancerData represented by this builder's parameters
        /// and registers it and its components with the appropriate managers.
        /// </summary>
        /// <returns>The newly registered EnhancerData</returns>
        public EnhancerData BuildAndRegister()
        {
            var enhancerData = this.Build();
            foreach (var pool in EnhancerPoolIDs)
            {
                CustomEnhancerPoolManager.AddEnhancerToPool(enhancerData, pool);
            }
            return enhancerData;
        }

        /// <summary>
        /// Builds the EnhancerData represented by this builder's parameters.
        /// </summary>
        /// <returns>The newly created EnhancerData</returns>
        public EnhancerData Build()
        {
            EnhancerData enhancerData = new EnhancerData();
            var t = Traverse.Create(enhancerData);

            // Set the name for the unity object
            enhancerData.name = this.ID;

            // Upgrades are contained within a relic effect - this is mandatory or the game will crash
            List<RelicEffectData> Effects = new List<RelicEffectData>
            {
                new RelicEffectDataBuilder
                {
                    relicEffectClassName = "RelicEffectCardUpgrade",
                    ParamCardUpgradeData = Upgrade.Build(),
                    ParamCardType = CardType,
                    ParamCharacterSubtype = "SubtypesData_None",
                }.Build()
            };
            t.Field("effects").SetValue(Effects);

            // Grab the LinkedClass from the ClanID
            this.LinkedClass = ProviderManager.SaveManager.GetAllGameData().FindClassData(this.ClanID);
            t.Field("linkedClass").SetValue(LinkedClass);

            // Take care of the localized strings
            BuilderUtils.ImportStandardLocalization(this.DescriptionKey, this.Description);
            t.Field("descriptionKey").SetValue(DescriptionKey);

            BuilderUtils.ImportStandardLocalization(this.NameKey, this.Name);
            t.Field("nameKey").SetValue(NameKey);

            // Create the icon from the asset path
            if (this.AssetPath != null)
            {
                Sprite iconSprite = CustomAssetManager.LoadSpriteFromPath(this.BaseAssetPath + "/" + this.AssetPath);
                t.Field("icon").SetValue(iconSprite);
            }

            // A steak pun is a rare medium well done.
            t.Field("rarity").SetValue(Rarity);

            return enhancerData;
        }
    }
}
