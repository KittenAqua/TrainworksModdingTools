using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonsterTrainModdingAPI.Builders
{
    public class CardUpgradeTreeDataBuilder
    {
        /// <summary>
        /// Please make a custom ChampionData class to add here.
        /// </summary>
        public CharacterData champion;

        /// <summary>
        /// Use this field if you're copying an existing already built tree.
        /// </summary>
        public List<CardUpgradeTreeData.UpgradeTree> upgradeTreesInternal = null;

        /// <summary>
        /// This is a list of lists of CardUpgradeDataBuilders. Base game clans have a 3x3 list.
        /// </summary>
        public List<List<CardUpgradeDataBuilder>> upgradeTrees = new List<List<CardUpgradeDataBuilder>>();

        public CardUpgradeTreeData Build()
        {
            CardUpgradeTreeData cardUpgradeTreeData = new CardUpgradeTreeData();

            if (upgradeTreesInternal == null)
            {
                CardUpgradeTreeData.UpgradeTree tree = new CardUpgradeTreeData.UpgradeTree();
                
                foreach (List<CardUpgradeDataBuilder> branch in upgradeTrees) 
                {
                    List<CardUpgradeData> newbranch = new List<CardUpgradeData>();

                    foreach (CardUpgradeDataBuilder leaf in branch)
                    {
                        newbranch.Add(leaf.Build());
                    }
                    AccessTools.Field(typeof(CardUpgradeTreeData.UpgradeTree), "cardUpgrades").SetValue(tree, branch);

                }
                upgradeTreesInternal.Add(tree);
            }

            AccessTools.Field(typeof(CardUpgradeTreeData), "champion").SetValue(cardUpgradeTreeData, this.champion);
            AccessTools.Field(typeof(CardUpgradeTreeData), "upgradeTrees").SetValue(cardUpgradeTreeData, this.upgradeTreesInternal);

            return cardUpgradeTreeData;
        }
    }
}