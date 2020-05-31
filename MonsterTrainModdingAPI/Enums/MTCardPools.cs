using System;
using System.Collections.Generic;
using System.Text;

namespace MonsterTrainModdingAPI.Enum
{
    public enum MTCardPool
    {
        StandardPool, // Used for clan packs, allied clan packs, rare packs, and cov 1 cards
        TrainStewardPool,
        StarterCardPool, // Contains the starting card for each clan, e.g. "Torch"
        HellhornedBannerPool,
        AwokenBannerPool,
        StygianBannerPool,
        UmbraBannerPool,
        MeltingRemnantBannerPool,
        UnitDraftPool // From major bosses/trials
    }

    public static class CardPoolIDs
    {
        private static readonly Dictionary<MTCardPool, int> cardPoolIDDictionary = new Dictionary<MTCardPool, int>
        {
            { MTCardPool.StandardPool, 9366 },
            { MTCardPool.TrainStewardPool, 10856 },
            { MTCardPool.StarterCardPool, 12548 },
            { MTCardPool.HellhornedBannerPool, 9354 },
            { MTCardPool.AwokenBannerPool, 9352 },
            { MTCardPool.StygianBannerPool, 9362 },
            { MTCardPool.UmbraBannerPool, 9364 },
            { MTCardPool.MeltingRemnantBannerPool, 9360 },
            { MTCardPool.UnitDraftPool, 9358 }
        };

        public static int GetCardPoolID(MTCardPool cardPool)
        {
            return cardPoolIDDictionary[cardPool];
        }
    }
}
