using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Trainworks.Constants
{
    /// <summary>
    /// Provides easy access to all of the base game's card pool IDs
    /// </summary>
    public static class VanillaCardPoolIDs
    {
        /// <summary>
        /// Card pool used for the Cov 1 random pool and end-of-battle card rewards, among other things
        /// </summary>
        public static readonly string MegaPool = "MegaPool";
        /// <summary>
        /// Use unknown
        /// </summary>
        public static readonly string UnitsAllBanner = "UnitsAllBanner";
        /// <summary>
        /// Hellhorned banner pool
        /// </summary>
        public static readonly string HellhornedBanner = "UnitsHellhornedBanner";
        /// <summary>
        /// Awoken banner pool
        /// </summary>
        public static readonly string AwokenBanner = "UnitsAwokenBanner";
        /// <summary>
        /// Stygian banner pool
        /// </summary>
        public static readonly string StygianBanner = "UnitsStygianBanner";
        /// <summary>
        /// Umbra banner pool
        /// </summary>
        public static readonly string UmbraBanner = "UnitsUmbraBanner";
        /// <summary>
        /// Melting Remnant banner pool
        /// </summary>
        public static readonly string RemnantBanner = "UnitsRemnantBanner";
        /// <summary>
        /// Pool containing all champions
        /// </summary>
        public static readonly string ChampionPool = "ChampionPool";
        /// <summary>
        /// Pool used when an effect randomly generates an imp
        /// </summary>
        public static readonly string ImpPool = "ImpPool";
        /// <summary>
        /// Pool used when an effect randomly generates a morsel
        /// </summary>
        public static readonly string MorselPool = "Class5Food";
        /// <summary>
        /// Pool used when one of the Umbra starter cards randomly generates a morsel
        /// </summary>
        public static readonly string MorselPoolStarter = "Class5StarterFoodCard";

        // Note: list incomplete. There are many more cardpools than just these two.
    }
}
