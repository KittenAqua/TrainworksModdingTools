using System;
using System.Collections.Generic;
using System.Text;

namespace Trainworks.Constants
{
    /// <summary>
    /// Provides easy access to all of the base game's card effect types
    /// </summary>
    public static class VanillaCardEffectTypes
    {
        public static readonly Type CardEffectAddBattleCard = typeof(CardEffectAddBattleCard);
        public static readonly Type CardEffectAddRunCard = typeof(CardEffectAddRunCard);
        public static readonly Type CardEffectAddStatsAndStatusEffectsFromSelf = typeof(CardEffectAddStatsAndStatusEffectsFromSelf);
        public static readonly Type CardEffectAddStatsFromSelf = typeof(CardEffectAddStatsFromSelf);
        public static readonly Type CardEffectAddStatusEffect = typeof(CardEffectAddStatusEffect);
        public static readonly Type CardEffectAddStatusEffectNextMonster = typeof(CardEffectAddStatusEffectNextMonster);
        public static readonly Type CardEffectAddTempCardUpgradeToCardsInHand = typeof(CardEffectAddTempCardUpgradeToCardsInHand);
        public static readonly Type CardEffectAddTempCardUpgradeToDeckScreenChosenCard = typeof(CardEffectAddTempCardUpgradeToDeckScreenChosenCard);
        public static readonly Type CardEffectAddTempCardUpgradeToNextDrawnCard = typeof(CardEffectAddTempCardUpgradeToNextDrawnCard);
        public static readonly Type CardEffectAddTempCardUpgradeToUnits = typeof(CardEffectAddTempCardUpgradeToUnits);
        public static readonly Type CardEffectAddTempSwapStats = typeof(CardEffectAddTempSwapStats);
        public static readonly Type CardEffectAddTrigger = typeof(CardEffectAddTrigger);
        public static readonly Type CardEffectAddUpgradedCopy = typeof(CardEffectAddUpgradedCopy);
        public static readonly Type CardEffectAdjustEnergy = typeof(CardEffectAdjustEnergy);
        public static readonly Type CardEffectAdjustMaxHealth = typeof(CardEffectAdjustMaxHealth);
        public static readonly Type CardEffectAdjustRoomCapacity = typeof(CardEffectAdjustRoomCapacity);
        public static readonly Type CardEffectBuffDamage = typeof(CardEffectBuffDamage);
        public static readonly Type CardEffectBuffMaxHealth = typeof(CardEffectBuffMaxHealth);
        public static readonly Type CardEffectBump = typeof(CardEffectBump);
        public static readonly Type CardEffectChooseDiscard = typeof(CardEffectChooseDiscard);
        public static readonly Type CardEffectCopyCardUpgradesToUnits = typeof(CardEffectCopyCardUpgradesToUnits);
        public static readonly Type CardEffectCopyUnits = typeof(CardEffectCopyUnits);
        public static readonly Type CardEffectDamage = typeof(CardEffectDamage);
        public static readonly Type CardEffectDamageByUnitsKilled = typeof(CardEffectDamageByUnitsKilled);
        public static readonly Type CardEffectDamagePerTargetAttack = typeof(CardEffectDamagePerTargetAttack);
        public static readonly Type CardEffectDebuffMaxHealth = typeof(CardEffectDebuffMaxHealth);
        public static readonly Type CardEffectDespawnCharacter = typeof(CardEffectDespawnCharacter);
        public static readonly Type CardEffectDiscardHand = typeof(CardEffectDiscardHand);
        public static readonly Type CardEffectDraw = typeof(CardEffectDraw);
        public static readonly Type CardEffectDrawAdditionalNextTurn = typeof(CardEffectDrawAdditionalNextTurn);
        public static readonly Type CardEffectDrawType = typeof(CardEffectDrawType);
        public static readonly Type CardEffectEnchant = typeof(CardEffectEnchant);
        public static readonly Type CardEffectFeederRules = typeof(CardEffectFeederRules);
        public static readonly Type CardEffectFloorRearrange = typeof(CardEffectFloorRearrange);
        public static readonly Type CardEffectFreezeCard = typeof(CardEffectFreezeCard);
        public static readonly Type CardEffectFreezeRandomCard = typeof(CardEffectFreezeRandomCard);
        public static readonly Type CardEffectGainEnergy = typeof(CardEffectGainEnergy);
        public static readonly Type CardEffectGainEnergyEveryTurn = typeof(CardEffectGainEnergyEveryTurn);
        public static readonly Type CardEffectGainEnergyNextTurn = typeof(CardEffectGainEnergyNextTurn);
        public static readonly Type CardEffectHeal = typeof(CardEffectHeal);
        public static readonly Type CardEffectHealAndDamageRelative = typeof(CardEffectHealAndDamageRelative);
        public static readonly Type CardEffectHealTrain = typeof(CardEffectHealTrain);
        public static readonly Type CardEffectKill = typeof(CardEffectKill);
        public static readonly Type CardEffectModifyCardCost = typeof(CardEffectModifyCardCost);
        public static readonly Type CardEffectMultiplyStatusEffect = typeof(CardEffectMultiplyStatusEffect);
        public static readonly Type CardEffectPlayUnitTrigger = typeof(CardEffectPlayUnitTrigger);
        public static readonly Type CardEffectRandomDiscard = typeof(CardEffectRandomDiscard);
        public static readonly Type CardEffectRecursion = typeof(CardEffectRecursion);
        public static readonly Type CardEffectRemoveAllStatusEffects = typeof(CardEffectRemoveAllStatusEffects);
        public static readonly Type CardEffectRemoveStatusEffect = typeof(CardEffectRemoveStatusEffect);
        public static readonly Type CardEffectRemoveStatusEffectOnStatusThreshold = typeof(CardEffectRemoveStatusEffectOnStatusThreshold);
        public static readonly Type CardEffectReplayTrigger = typeof(CardEffectReplayTrigger);
        public static readonly Type CardEffectRewardCards = typeof(CardEffectRewardCards);
        public static readonly Type CardEffectRewardGold = typeof(CardEffectRewardGold);
        public static readonly Type CardEffectSacrifice = typeof(CardEffectSacrifice);
        public static readonly Type CardEffectSpawnHero = typeof(CardEffectSpawnHero);
        public static readonly Type CardEffectSpawnMonster = typeof(CardEffectSpawnMonster);
        public static readonly Type CardEffectTransferAllStatusEffects = typeof(CardEffectTransferAllStatusEffects);
        // The following are new as of TLD
        public static readonly Type CardEffectAddPermanentCorruption = typeof(CardEffectAddPermanentCorruption);
        public static readonly Type CardEffectAddStatusEffectPerOtherEffect = typeof(CardEffectAddStatusEffectPerOtherEffect);
        public static readonly Type CardEffectAdjustCorruption = typeof(CardEffectAdjustCorruption);
        public static readonly Type CardEffectAdjustRoomMaxCorruption = typeof(CardEffectAdjustRoomMaxCorruption);
        public static readonly Type CardEffectModifyInkVariable = typeof(CardEffectModifyInkVariable);
        public static readonly Type CardEffectMultiplyAllStatusEffects = typeof(CardEffectMultiplyAllStatusEffects);
        public static readonly Type CardEffectMultiplyAllPositiveStatusEffects = typeof(CardEffectMultiplyAllPositiveStatusEffects);
        public static readonly Type CardEffectMultiplyCorruption = typeof(CardEffectMultiplyCorruption);
        public static readonly Type CardEffectNULL = typeof(CardEffectNULL);
        public static readonly Type CardEffectRecruit = typeof(CardEffectRecruit);
        public static readonly Type CardEffectRemoveStatusEffectPerCorrupt = typeof(CardEffectRemoveStatusEffectPerCorrupt);
        public static readonly Type CardEffectSynergizeStatusEffects = typeof(CardEffectSynergizeStatusEffects);
    }
}
