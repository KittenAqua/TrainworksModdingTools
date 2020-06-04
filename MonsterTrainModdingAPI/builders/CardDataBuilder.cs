using System;
using System.Collections.Generic;
using BepInEx;
using BepInEx.Harmony;
using System.Reflection;
using BepInEx.Logging;
using HarmonyLib;
using MonsterTrainModdingAPI.Enum;
using UnityEngine;
using UnityEngine.AddressableAssets;
using ShinyShoe;
using MonsterTrainModdingAPI.Managers;

namespace MonsterTrainModdingAPI.Builder
{
    public class CardDataBuilder
    {
        public string CardID { get; set; }
        public List<int> CardPoolIDs { get; set; }

        public int Cost { get; set; }
        public string Name { get; set; }
        public string OverrideDescriptionKey { get; set; }
        public AssetReferenceGameObject CardArtPrefabVariantRef { get; set; }

        public List<CardEffectData> Effects { get; set; }
        public List<CardTraitData> Traits { get; set; }
        public List<CharacterTriggerData> EffectTriggers { get; set; }
        public List<CardTriggerEffectData> Triggers { get; set; }
        public List<CardData> SharedMasteryCards { get; set; }
        public List<CardUpgradeData> StartingUpgrades { get; set; }
        public List<string> CardLoreTooltipKeys { get; set; }

        public bool Targetless { get; set; }
        public bool TargetsRoom { get; set; }

        public ClassData LinkedClass { get; set; }
        public CardType CardType { get; set; }
        public CollectableRarity Rarity { get; set; }

        public int UnlockLevel { get; set; }
        public CardData LinkedMasteryCard { get; set; }
        public bool IgnoreWhenCountingMastery { get; set; }

        public Sprite SpriteCatch { get; set; }
        public CardData.CostType CostType { get; set; }
        public FallbackData FallbackData { get; set; }

        public MTClan Clan { get; set; }

        public CardDataBuilder()
        {
            this.CardPoolIDs = new List<int>();
            this.Effects = new List<CardEffectData>();
            this.Traits = new List<CardTraitData>();
            this.EffectTriggers = new List<CharacterTriggerData>();
            this.Triggers = new List<CardTriggerEffectData>();
            this.SharedMasteryCards = new List<CardData>();
            this.StartingUpgrades = new List<CardUpgradeData>();
            this.CardLoreTooltipKeys = new List<string>();
        }

        public CardData BuildAndRegister()
        {
            var cardData = this.Build();
            API.Log(LogLevel.Debug, "Adding custom card: " + cardData.GetName());
            CustomCardManager.RegisterCustomCardData(cardData, this.CardPoolIDs);
            return cardData;
        }

        public CardData Build()
        {
            string factionID = Enum.ClanIDs.GetClanID(Clan);
            this.LinkedClass = CustomCardManager.SaveManager.GetAllGameData().FindClassData(factionID);
            CardData cardData = ScriptableObject.CreateInstance<CardData>();
            AccessTools.Field(typeof(CardData), "id").SetValue(cardData, this.CardID);
            AccessTools.Field(typeof(CardData), "cost").SetValue(cardData, this.Cost);
            AccessTools.Field(typeof(CardData), "nameKey").SetValue(cardData, this.Name);
            AccessTools.Field(typeof(CardData), "overrideDescriptionKey").SetValue(cardData, this.OverrideDescriptionKey);
            AccessTools.Field(typeof(CardData), "cardArtPrefabVariantRef").SetValue(cardData, this.CardArtPrefabVariantRef);
            AccessTools.Field(typeof(CardData), "effects").SetValue(cardData, this.Effects);
            foreach (CardTraitData cardTraitData in this.Traits)
            {
                AccessTools.Field(typeof(CardTraitData), "paramCardData").SetValue(cardTraitData, cardData);
            }
            AccessTools.Field(typeof(CardData), "traits").SetValue(cardData, this.Traits);
            AccessTools.Field(typeof(CardData), "effectTriggers").SetValue(cardData, this.EffectTriggers);
            AccessTools.Field(typeof(CardData), "triggers").SetValue(cardData, this.Triggers);
            AccessTools.Field(typeof(CardData), "sharedMasteryCards").SetValue(cardData, this.SharedMasteryCards);
            AccessTools.Field(typeof(CardData), "startingUpgrades").SetValue(cardData, this.StartingUpgrades);
            AccessTools.Field(typeof(CardData), "cardLoreTooltipKeys").SetValue(cardData, this.CardLoreTooltipKeys);
            AccessTools.Field(typeof(CardData), "targetless").SetValue(cardData, this.Targetless);
            AccessTools.Field(typeof(CardData), "targetsRoom").SetValue(cardData, this.TargetsRoom);
            AccessTools.Field(typeof(CardData), "linkedClass").SetValue(cardData, this.LinkedClass);
            AccessTools.Field(typeof(CardData), "cardType").SetValue(cardData, this.CardType);
            AccessTools.Field(typeof(CardData), "rarity").SetValue(cardData, this.Rarity);
            AccessTools.Field(typeof(CardData), "unlockLevel").SetValue(cardData, this.UnlockLevel);
            AccessTools.Field(typeof(CardData), "linkedMasteryCard").SetValue(cardData, this.LinkedMasteryCard);
            AccessTools.Field(typeof(CardData), "ignoreWhenCountingMastery").SetValue(cardData, this.IgnoreWhenCountingMastery);
            if (this.SpriteCatch != null)
            {
                AccessTools.Field(typeof(CardData), "spriteCatch").SetValue(cardData, this.SpriteCatch);
            }
            AccessTools.Field(typeof(CardData), "costType").SetValue(cardData, this.CostType);
            AccessTools.Field(typeof(CardData), "fallbackData").SetValue(cardData, this.FallbackData);

            return cardData;
        }

        public void CreateAndSetCardArtPrefabVariantRef(string m_debugName, string m_AssetGUID)
        {
            var assetReferenceGameObject = new AssetReferenceGameObject();
            AccessTools.Field(typeof(AssetReferenceGameObject), "m_debugName")
                    .SetValue(assetReferenceGameObject, m_debugName);
            AccessTools.Field(typeof(AssetReferenceGameObject), "m_AssetGUID")
                .SetValue(assetReferenceGameObject, m_AssetGUID);
            this.CardArtPrefabVariantRef = assetReferenceGameObject;
        }
        
        public void AddToCardPool(Enum.MTCardPool cardPool)
        {
            int cardPoolID = Enum.CardPoolIDs.GetCardPoolID(cardPool);
            this.CardPoolIDs.Add(cardPoolID);
        }
    }
}
