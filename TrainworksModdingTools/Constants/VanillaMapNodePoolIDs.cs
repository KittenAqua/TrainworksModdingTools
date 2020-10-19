using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Trainworks.Constants
{
    /// <summary>
    /// Provides easy access to all of the base game's map node pool IDs
    /// </summary>
    public static class VanillaMapNodePoolIDs
    {
        /// <summary>
        /// Holds all the clans' banners, but selects only the one corresponding to the current primary clan
        /// </summary>
        public static readonly string RandomChosenMainClassUnit = "RandomChosenMainClassUnit";
        /// <summary>
        /// Holds all the clans' banners, but selects only the one corresponding to the current allied clan
        /// </summary>
        public static readonly string RandomChosenSubClassUnit = "RandomChosenSubClassUnit";
        
        // Note: list incomplete. There are many more map node pools than just these two.
    }
}
