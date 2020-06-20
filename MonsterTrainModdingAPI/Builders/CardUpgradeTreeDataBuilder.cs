using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonsterTrainModdingAPI.Builders
{
    class CardUpgradeTreeDataBuilder
    {
        /// <summary>
        /// Please make a custom ChampionData class to add here.
        /// </summary>
        public CharacterData champion;

        /// <summary>
        /// Add in upgrade trees here by using the UpgradeTree() method call.
        /// </summary>
        public List<CardUpgradeTreeData.UpgradeTree> upgradeTrees = new List<CardUpgradeTreeData.UpgradeTree>();

        /// <summary>
        /// Pass it three CardUpgradeDataBuilders, which will create one Champion Upgrade path.
        /// </summary>
        /// <param name="First"></param>
        /// <param name="Second"></param>
        /// <param name="Third"></param>
        /// <returns></returns>
        public CardUpgradeTreeData.UpgradeTree UpgradeTree(CardUpgradeDataBuilder First, CardUpgradeDataBuilder Second, CardUpgradeDataBuilder Third)
        {
            CardUpgradeTreeData.UpgradeTree tree = new CardUpgradeTreeData.UpgradeTree();

            List < CardUpgradeData > branch = new List<CardUpgradeData>
            {
                First.Build(),
                Second.Build(),
                Third.Build()
            };

            AccessTools.Field(typeof(CardUpgradeTreeData.UpgradeTree), "cardUpgrades").SetValue(tree, branch);

            return tree;
        }

        /// <summary>
        /// Pass it three CardUpgradeData, which will create one Champion Upgrade path.
        /// </summary>
        /// <param name="First"></param>
        /// <param name="Second"></param>
        /// <param name="Third"></param>
        /// <returns></returns>
        public CardUpgradeTreeData.UpgradeTree UpgradeTree(CardUpgradeData First, CardUpgradeData Second, CardUpgradeData Third)
        {
            CardUpgradeTreeData.UpgradeTree tree = new CardUpgradeTreeData.UpgradeTree();

            List<CardUpgradeData> branch = new List<CardUpgradeData>
            {
                First,
                Second,
                Third
            };

            AccessTools.Field(typeof(CardUpgradeTreeData.UpgradeTree), "cardUpgrades").SetValue(tree, branch);

            return tree;
        }

        public CardUpgradeTreeData Build()
        {
            CardUpgradeTreeData cardUpgradeTreeData = new CardUpgradeTreeData();

            AccessTools.Field(typeof(CardUpgradeTreeData), "champion").SetValue(cardUpgradeTreeData, this.champion);
            AccessTools.Field(typeof(CardUpgradeTreeData), "upgradeTrees").SetValue(cardUpgradeTreeData, this.upgradeTrees);

            return cardUpgradeTreeData;
        }
    }
}