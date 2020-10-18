using System;
using System.Collections.Generic;
using System.Text;

namespace TrainworksModdingTools.Interfaces
{
    /// <summary>
    /// Indicates that the class has something to initialize when the game starts up
    /// </summary>
    public interface IInitializable
    {
        /// <summary>
        /// Called as soon as the framework is set up.
        /// As of right now, this is immediately following AssetLoadingManager.Start().
        /// </summary>
        void Initialize();
    }
}
