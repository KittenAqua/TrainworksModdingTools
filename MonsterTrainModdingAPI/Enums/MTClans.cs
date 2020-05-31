using System;
using System.Collections.Generic;
using System.Text;

namespace MonsterTrainModdingAPI.Enum
{
    public enum MTClan
    {
        Hellhorned,
        Awoken,
        Stygian,
        Umbra,
        MeltingRemnant
    }

    public static class ClanIDs
    {
        private static readonly Dictionary<MTClan, string> clanIDDictionary = new Dictionary<MTClan, string>
        {
            { MTClan.Hellhorned, "c595c344-d323-4cf1-9ad6-41edc2aebbd0" },
            { MTClan.Awoken, "fd119fcf-c2cf-469e-8a5a-e9b0f265560d" },
            { MTClan.Stygian, "9317cf9a-04ec-49da-be29-0e4ed61eb8ba" },
            { MTClan.Umbra, "4fe56363-b1d9-46b7-9a09-bd2df1a5329f" },
            { MTClan.MeltingRemnant, "fda62ada-520e-42f3-aa88-e4a78549c4a2" }
        };

        public static string GetClanID(MTClan clan)
        {
            return clanIDDictionary[clan];
        }
    }
}
