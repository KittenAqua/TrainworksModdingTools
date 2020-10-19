using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Trainworks.Constants
{
    /// <summary>
    /// Provides easy access to all of the base game's relic pool IDs
    /// </summary>
    public static class VanillaRelicPoolIDs
    {
        /// <summary>
        /// Relic pool used for all random relic rewards
        /// </summary>
        public static readonly string MegaRelicPool = "MegaRelicPool";
        /// <summary>
        /// Daedalus boss relic reward
        /// </summary>
        public static readonly string BossPool = "BossPool";
        /// <summary>
        /// Fel boss relic reward
        /// </summary>
        public static readonly string BossPoolLevel6 = "BossPoolLevel6";
    }
}
