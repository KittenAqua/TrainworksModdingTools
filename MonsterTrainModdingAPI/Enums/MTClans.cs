using System;
using System.Collections.Generic;
using System.Text;

namespace MonsterTrainModdingAPI.Enums.MTClans
{
    /// <summary>
    /// Interface representing a clan.
    /// Effectively an extensible enum.
    /// </summary>
    public interface IMTClan
    {
        string ID { get; }
    }

    public class MTClan_Hellhorned : IMTClan { public string ID => "c595c344-d323-4cf1-9ad6-41edc2aebbd0"; }
    public class MTClan_Awoken : IMTClan { public string ID => "fd119fcf-c2cf-469e-8a5a-e9b0f265560d"; }
    public class MTClan_Stygian : IMTClan { public string ID => "9317cf9a-04ec-49da-be29-0e4ed61eb8ba"; }
    public class MTClan_Umbra : IMTClan { public string ID => "4fe56363-b1d9-46b7-9a09-bd2df1a5329f"; }
    public class MTClan_MeltingRemnant : IMTClan { public string ID => "fda62ada-520e-42f3-aa88-e4a78549c4a2"; }
    public class MTClan_Clanless : IMTClan { public string ID => null; }

    /// <summary>
    /// Helper class which gets the ID for a clan when given its type.
    /// </summary>
    public static class MTClanIDs
    {
        /// <summary>
        /// Gets the ID for the clan with given type.
        /// </summary>
        /// <param name="clanType">Must implement IMTClan</param>
        /// <returns></returns>
        public static string GetIDForType(Type clanType)
        {
            if (typeof(IMTClan).IsAssignableFrom(clanType))
            {
                var clan = (IMTClan)Activator.CreateInstance(clanType);
                return clan.ID;
            }
            return "";
        }
    }
}
