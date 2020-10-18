using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace TrainworksModdingTools.Constants
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
        
        // Note: list incomplete. There are many more cardpools than just these two.
    }
}
