using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace MonsterTrainModdingAPI.Enums.MTCardPools
{
    public interface IMTCardPool
    {
        string ID { get; }
    }

    public class MTCardPool_MegaPool : IMTCardPool { public string ID => "MegaPool"; }
    public class MTCardPool_UnitsAllBanner : IMTCardPool { public string ID => "UnitsAllBanner"; }

    public static class MTCardPoolIDs
    {
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
