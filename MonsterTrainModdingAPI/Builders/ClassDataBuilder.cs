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
using MonsterTrainModdingAPI.Enums.MTCardPools;
using MonsterTrainModdingAPI.Enums.MTClans;

namespace MonsterTrainModdingAPI.Builders
{
    /// <summary>
    /// Builder class to aid in creating custom clans.
    /// </summary>
    public class ClassDataBuilder
    {
        /// <summary>
        /// Unique string used to store and retrieve the clan data.
        /// </summary>
        public string ClassID { get; set; }
        /// <summary>
        /// Name of the clan.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Description of the clan.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Description of the clan when selected as the allied clan.
        /// </summary>
        public string SubclassDescription { get; set; }

        public ChampionData StartingChampion { get; set; }
        public CardUpgradeTreeDataBuilder UpgradeTree { get; set; }

        /// <summary>
        /// Must contain 4 sprites; in order:
        /// small icon (32x32)
        /// medium icon (??x??)
        /// large icon (89x89)
        /// silhouette icon (43x43)
        /// </summary>
        public List<Sprite> Icons { get; set; }
        public Sprite ChampionIcon { get; set; }
        public ClassCardStyle CardStyle { get; set; }
        public string ClanSelectSfxCue { get; set; }

        public List<ClassData.StartingCardOptions> MainClassStartingCards { get; set; }
        public List<ClassData.StartingCardOptions> SubclassStartingCards { get; set; }

        public Color UiColor { get; set; }
        public Color UiColorDark { get; set; }

        public Dictionary<MetagameSaveData.TrackedValue, string> UnlockKeys { get; set; }
        public MetagameSaveData.TrackedValue ClassUnlockCondition { get; set; }
        public int ClassUnlockParam { get; set; }
        public List<string> ClassUnlockPreviewTexts { get; set; }

        public string TitleLoc { get; set; }
        public string DescriptionLoc { get; set; }
        public string SubclassDescriptionLoc { get; set; }

        public ClassDataBuilder()
        {
            this.Name = "";
            this.Description = "";
            this.Icons = new List<Sprite>();
            this.MainClassStartingCards = new List<ClassData.StartingCardOptions>();
            this.SubclassStartingCards = new List<ClassData.StartingCardOptions>();
            this.UnlockKeys = new Dictionary<MetagameSaveData.TrackedValue, string>();
            this.ClassUnlockPreviewTexts = new List<string>();
        }

        /// <summary>
        /// Builds the ClassData represented by this builder's parameters recursively
        /// and registers it and its components with the appropriate managers.
        /// </summary>
        /// <returns>The newly registered ClassData</returns>
        public ClassData BuildAndRegister()
        {
            var classData = this.Build();
            CustomClassManager.RegisterCustomClass(classData);
            return classData;
        }

        /// <summary>
        /// Builds the ClassData represented by this builder's parameters recursively;
        /// all Builders represented in this class's various fields will also be built.
        /// </summary>
        /// <returns>The newly created ClassData</returns>
        public ClassData Build()
        {
            ClassData classData = ScriptableObject.CreateInstance<ClassData>();
            AccessTools.Field(typeof(ClassData), "id").SetValue(classData, this.ClassID);
            AccessTools.Field(typeof(ClassData), "cardStyle").SetValue(classData, this.CardStyle);
            AccessTools.Field(typeof(ClassData), "championIcon").SetValue(classData, this.ChampionIcon);
            AccessTools.Field(typeof(ClassData), "clanSelectSfxCue").SetValue(classData, this.ClanSelectSfxCue);
            AccessTools.Field(typeof(ClassData), "classUnlockCondition").SetValue(classData, this.ClassUnlockCondition);
            AccessTools.Field(typeof(ClassData), "classUnlockParam").SetValue(classData, this.ClassUnlockParam);
            AccessTools.Field(typeof(ClassData), "classUnlockPreviewTexts").SetValue(classData, this.ClassUnlockPreviewTexts);
            if (this.DescriptionLoc == null)
            {
                this.DescriptionLoc = this.Description;
            }
            AccessTools.Field(typeof(ClassData), "descriptionLoc").SetValue(classData, this.DescriptionLoc);
            Type iconSetType = AccessTools.Inner(typeof(ClassData), "IconSet");
            var iconSet = Activator.CreateInstance(iconSetType);
            AccessTools.Field(iconSetType, "small").SetValue(iconSet, this.Icons[0]);
            AccessTools.Field(iconSetType, "medium").SetValue(iconSet, this.Icons[1]);
            AccessTools.Field(iconSetType, "large").SetValue(iconSet, this.Icons[2]);
            AccessTools.Field(iconSetType, "silhouette").SetValue(iconSet, this.Icons[3]);
            AccessTools.Field(typeof(ClassData), "icons").SetValue(classData, iconSet);
            AccessTools.Field(typeof(ClassData), "mainClassStartingCards").SetValue(classData, this.MainClassStartingCards);
            AccessTools.Field(typeof(ClassData), "startingChampion").SetValue(classData, this.StartingChampion);
            if (this.SubclassDescriptionLoc == null)
            {
                this.SubclassDescriptionLoc = this.SubclassDescription;
            }
            AccessTools.Field(typeof(ClassData), "subclassDescriptionLoc").SetValue(classData, this.SubclassDescriptionLoc);
            AccessTools.Field(typeof(ClassData), "subclassStartingCards").SetValue(classData, this.SubclassStartingCards);
            if (this.TitleLoc == null)
            {
                this.TitleLoc = this.Name;
            }
            AccessTools.Field(typeof(ClassData), "titleLoc").SetValue(classData, this.TitleLoc);
            AccessTools.Field(typeof(ClassData), "uiColor").SetValue(classData, this.UiColor);
            AccessTools.Field(typeof(ClassData), "uiColorDark").SetValue(classData, this.UiColorDark);
            //AccessTools.Field(typeof(ClassData), "UNLOCK_KEYS").SetValue(classData, this.);
            AccessTools.Field(typeof(ClassData), "upgradeTree").SetValue(classData, this.UpgradeTree.Build());

            return classData;
        }
    }
}
