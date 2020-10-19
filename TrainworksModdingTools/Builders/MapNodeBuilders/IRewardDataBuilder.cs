using System;
using System.Collections.Generic;
using System.Text;

namespace Trainworks.Builders
{
    public interface IRewardDataBuilder
    {
        /// <summary>
        /// Builds the RewardData represented by this builder's parameters recursively;
        /// all Builders represented in this class's various fields will also be built.
        /// </summary>
        /// <returns>The newly created RewardData</returns>
        RewardData Build();
    }
}
