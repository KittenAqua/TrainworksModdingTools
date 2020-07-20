using HarmonyLib;
using MonsterTrainModdingAPI.Builders;
using System.Collections.Generic;
using MonsterTrainModdingAPI.Managers;
using UnityEngine;
using System.IO;

namespace MonsterTrainModdingAPI.Builders
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
        public Sprite Icon { get; set; }
        /// <summary>
        /// Holds the upgrade to be applied to the chosen card after the enhancer is purchased.
        /// </summary>
        public CardUpgradeDataBuilder Upgrade { get; set; }
        public CollectableRarity Rarity { get; set; }
        public ClassData LinkedClass { get; set; }


        public EnhancerDataBuilder()
        {
            this.Name = "";
            this.Description = "EmptyString-0000000000000000-00000000000000000000000000000000-v2";
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
                }.Build()
            };
            t.Field("effects").SetValue(Effects);

            // Grab the LinkedClass from the ClanID
            this.LinkedClass = CustomCardManager.SaveManager.GetAllGameData().FindClassData(this.ClanID);
            t.Field("linkedClass").SetValue(LinkedClass);

            // Take care of the localized strings
            BuilderUtils.ImportStandardLocalization(this.DescriptionKey, this.Description);
            t.Field("descriptionKey").SetValue(DescriptionKey);

            BuilderUtils.ImportStandardLocalization(this.NameKey, this.Name);
            t.Field("nameKey").SetValue(NameKey);

            // Create the icon from the asset path
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
            t.Field("icon").SetValue(Icon);

            // Rarity
            t.Field("rarity").SetValue(Rarity);

            return enhancerData;
        }
    }
}
