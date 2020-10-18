using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace TrainworksModdingTools.Constants
{
    /// <summary>
    /// Provides easy access to all of the base game's enhancer pool IDs
    /// </summary>
    public static class VanillaEnhancerPoolIDs
    {
        /// <summary>
        /// The first slot in the Spell Upgrade Shop, always contains a -1 Ember upgrade.
        /// </summary>
        public static readonly string SpellUpgradePoolCostReduction = "SpellUpgradePoolCostReduction";
        /// <summary>
        /// The second slot in the Spell Upgrade Shop. Contains Magic Power +10 and Consume, Magic Power +20. 
        /// </summary>
        public static readonly string SpellUpgradePoolCommon = "SpellUpgradePoolCommon";
        /// <summary>
        /// The third slot in the Spell Upgrade Shop. Contains Holdover, remove Consume, Permafrost and Doublestack. Permafrost is Stygian themed but not limited.
        /// </summary>
        public static readonly string SpellUpgradePool = "SpellUpgradePool";
        /// <summary>
        /// The first two slots in the Unit Upgrade Shop. Contains +10 attack, +25 Health, and +10/+10. Also contains a class specific upgrade for your main and subclass.
        /// </summary>
        public static readonly string UnitUpgradePoolCommon = "UnitUpgradePoolCommon";
        /// <summary>
        /// The last slots in the Unit Upgrade Shop. Contains themed rare upgrades available for all classes. Multistrike (Hellhorned), Quick (Awoken), Largestone (Umbra) and Endless (Remnant). Stygian is in rare Spell upgrades.
        /// </summary>
        public static readonly string UnitUpgradePool = "UnitUpgradePool";
    }
}
