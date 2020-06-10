using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace MonsterTrainModdingAPI.Enums.MTCardPools
{
    /// <summary>
    /// Interface representing a card pool.
    /// Effectively an extensible enum.
    /// </summary>
    public interface IMTCardPool
    {
        string ID { get; }
    }

    /// <summary>
    /// Contains the following cards: list the cards in the pool here
    /// </summary>
    public class MTCardPool_MegaPool : IMTCardPool { public string ID => "MegaPool"; }
    /// <summary>
    /// Contains the following cards: list the cards in the pool here
    /// </summary>
    public class MTCardPool_UnitsAllBanner : IMTCardPool { public string ID => "UnitsAllBanner"; }

    /// <summary>
    /// Helper class which gets the ID for a cardpool when given its type.
    /// </summary>
    public static class MTCardPoolIDs
    {
        /// <summary>
        /// Gets the ID for the cardpool with given type.
        /// </summary>
        /// <param name="cardPoolType">Must implement IMTCardPool</param>
        /// <returns></returns>
        public static string GetIDForType(Type cardPoolType)
        {
            if (typeof(IMTCardPool).IsAssignableFrom(cardPoolType))
            {
                var cardPool = (IMTCardPool)Activator.CreateInstance(cardPoolType);
                return cardPool.ID;
            }
            return "";
        }
    }
}
