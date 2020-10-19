using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Trainworks.Builders
{
    public class CardUpgradeTreeDataBuilder
    {
        /// <summary>
        /// Please make a custom ChampionData class to add here.
        /// </summary>
        public CharacterData Champion { get; set; }

        /// <summary>
        /// Use this field if you're copying an existing already built tree.
        /// </summary>
        public List<CardUpgradeTreeData.UpgradeTree> UpgradeTreesInternal { get; set; }

        /// <summary>
        /// This is a list of lists of CardUpgradeDataBuilders. Base game clans have a 3x3 list.
        /// </summary>
        public List<List<CardUpgradeDataBuilder>> UpgradeTrees { get; set; } = new List<List<CardUpgradeDataBuilder>>();

        public CardUpgradeTreeData Build()
        {
            // The Trunk
            CardUpgradeTreeData cardUpgradeTreeData = ScriptableObject.CreateInstance<CardUpgradeTreeData>();

            // If we have used builders instead of an existing upgrade tree
            if (UpgradeTreesInternal == null)
            {
                // List of dest branches (Upgrade Paths)
                UpgradeTreesInternal = new List<CardUpgradeTreeData.UpgradeTree>();

                // Iterate over each source branch
                foreach (List<CardUpgradeDataBuilder> branch in UpgradeTrees) 
                {
                    // New Branch
                    CardUpgradeTreeData.UpgradeTree newBranch = new CardUpgradeTreeData.UpgradeTree();
                    
                    List<CardUpgradeData> newbranchlist = new List<CardUpgradeData>();

                    // Leaves (Make like a tree)
                    foreach (CardUpgradeDataBuilder leaf in branch)
                    {
                        newbranchlist.Add(leaf.Build());
                    }
                    AccessTools.Field(typeof(CardUpgradeTreeData.UpgradeTree), "cardUpgrades").SetValue(newBranch, newbranchlist);

                    UpgradeTreesInternal.Add(newBranch);
                }
            }

            // There needs to be at least two trees in here to work
            AccessTools.Field(typeof(CardUpgradeTreeData), "upgradeTrees").SetValue(cardUpgradeTreeData, this.UpgradeTreesInternal);

            return cardUpgradeTreeData;
        }
    }
}