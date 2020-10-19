using System;
using System.Collections.Generic;
using System.Text;

namespace Trainworks.Interfaces
{
    /// <summary>
    /// Indicates that the class has something to initialize when the game starts up
    /// </summary>
    public interface IInitializable
    {
        /// <summary>
        /// Called as soon as the Trainworks is set up.
        /// As of right now, this is immediately following AssetLoadingManager.Start().
        /// </summary>
        void Initialize();
    }
}
