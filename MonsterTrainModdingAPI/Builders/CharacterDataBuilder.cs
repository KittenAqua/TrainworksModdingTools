using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.IO;
using BepInEx;
using BepInEx.Harmony;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using UnityEngine.AddressableAssets;
using ShinyShoe;
using MonsterTrainModdingAPI.Managers;
using MonsterTrainModdingAPI.Enums;

namespace MonsterTrainModdingAPI.Builders
{
    public class CharacterDataBuilder
    {
        public string CharacterID { get; set; }

        public string Name { get; set; }

        public int AttackDamage { get; set; }
        public int Health { get; set; }
        public int Size { get; set; }

        public List<CharacterTriggerData> Triggers { get; set; }

        public StatusEffectStackData[] StartingStatusEffects { get; set; }
        public string[] StatusEffectImmunities { get; set; }

        public string AssetPath { get; set; }
        public AssetReferenceGameObject CharacterPrefabVariantRef { get; set; }

        public bool CanAttack { get; set; }
        public bool CanBeHealed { get; set; }

        public bool IsMiniboss { get; set; }
        public bool IsOuterTrainBoss { get; set; }

        public bool DeathSlidesBackwards { get; set; }

        public List<ActionGroupData> BossActionGroups { get; set; }

        public List<string> CharacterLoreTooltipKeys { get; set; }
        public List<RoomModifierData> RoomModifiers { get; set; }

        public bool AscendsTrainAutomatically { get; set; }
        public bool AttackTeleportsToDefender { get; set; } // When attacking, this character moves next to its target before hitting it.

        public List<string> SubtypeKeys { get; set; }

        public CharacterSoundData CharacterSoundData { get; set; }

        public CharacterChatterData CharacterChatterData { get; set; }

        public RuntimeAnimatorController AnimationController { get; set; }
        public CharacterDeathVFX.Type DeathType { get; set; }
        public VfxAtLoc DeathVFX { get; set; }
        public VfxAtLoc ImpactVFX { get; set; } // The VFX to put on the target when attacking to override CombatManager CombatReactVFXPrefab
        public VfxAtLoc ProjectilePrefab { get; set; } // The projectile to fire when attacking
        public VfxAtLoc BossRoomSpellCastVFX { get; set; }
        public VfxAtLoc BossSpellCastVFX { get; set; }
        public Sprite CharacterSpriteCache { get; set; }

        public FallbackData FallBackData { get; set; } // The default character prefab to use if one isn't found.  (Which should never happen in the shpped game)

        public CharacterDataBuilder()
        {
            this.AttackDamage = 10;
            this.Size = 2;
            this.AttackTeleportsToDefender = true;
            this.CanAttack = true;
            this.CanBeHealed = true;
            this.DeathSlidesBackwards = true;
            this.Triggers = new List<CharacterTriggerData>();
            this.SubtypeKeys = new List<string>();
            this.BossActionGroups = new List<ActionGroupData>();
            this.RoomModifiers = new List<RoomModifierData>();
            this.CharacterLoreTooltipKeys = new List<string>();
            this.StartingStatusEffects = new StatusEffectStackData[0];
            this.StatusEffectImmunities = new string[0];
            this.ImpactVFX = (VfxAtLoc)FormatterServices.GetUninitializedObject(typeof(VfxAtLoc));
        }

        public CharacterData BuildAndRegister()
        {
            var characterData = this.Build();
            CustomCharacterManager.RegisterCustomCharacter(characterData);
            return characterData;
        }

        public CharacterData Build()
        {
            CharacterData characterData = ScriptableObject.CreateInstance<CharacterData>();
            AccessTools.Field(typeof(CharacterData), "id").SetValue(characterData, this.CharacterID);
            AccessTools.Field(typeof(CharacterData), "animationController").SetValue(characterData, this.AnimationController);
            AccessTools.Field(typeof(CharacterData), "ascendsTrainAutomatically").SetValue(characterData, this.AscendsTrainAutomatically);
            AccessTools.Field(typeof(CharacterData), "attackDamage").SetValue(characterData, this.AttackDamage);
            AccessTools.Field(typeof(CharacterData), "attackTeleportsToDefender").SetValue(characterData, this.AttackTeleportsToDefender);
            AccessTools.Field(typeof(CharacterData), "bossActionGroups").SetValue(characterData, this.BossActionGroups);
            AccessTools.Field(typeof(CharacterData), "bossRoomSpellCastVFX").SetValue(characterData, this.BossRoomSpellCastVFX);
            AccessTools.Field(typeof(CharacterData), "bossSpellCastVFX").SetValue(characterData, this.BossSpellCastVFX);
            AccessTools.Field(typeof(CharacterData), "canAttack").SetValue(characterData, this.CanAttack);
            AccessTools.Field(typeof(CharacterData), "canBeHealed").SetValue(characterData, this.CanBeHealed);
            AccessTools.Field(typeof(CharacterData), "characterChatterData").SetValue(characterData, this.CharacterChatterData);
            AccessTools.Field(typeof(CharacterData), "characterLoreTooltipKeys").SetValue(characterData, this.CharacterLoreTooltipKeys);
            if (this.CharacterPrefabVariantRef == null)
            {
                this.CreateAndSetCharacterArtPrefabVariantRef(this.AssetPath, this.AssetPath);
            }
            AccessTools.Field(typeof(CharacterData), "characterPrefabVariantRef").SetValue(characterData, this.CharacterPrefabVariantRef);
            AccessTools.Field(typeof(CharacterData), "characterSoundData").SetValue(characterData, this.CharacterSoundData);
            AccessTools.Field(typeof(CharacterData), "characterSpriteCache").SetValue(characterData, this.CharacterSpriteCache);
            AccessTools.Field(typeof(CharacterData), "deathSlidesBackwards").SetValue(characterData, this.DeathSlidesBackwards);
            AccessTools.Field(typeof(CharacterData), "deathType").SetValue(characterData, this.DeathType);
            AccessTools.Field(typeof(CharacterData), "deathVFX").SetValue(characterData, this.DeathVFX);
            AccessTools.Field(typeof(CharacterData), "fallbackData").SetValue(characterData, this.FallBackData);
            AccessTools.Field(typeof(CharacterData), "health").SetValue(characterData, this.Health);
            AccessTools.Field(typeof(CharacterData), "impactVFX").SetValue(characterData, this.ImpactVFX);
            AccessTools.Field(typeof(CharacterData), "isMiniboss").SetValue(characterData, this.IsMiniboss);
            AccessTools.Field(typeof(CharacterData), "isOuterTrainBoss").SetValue(characterData, this.IsOuterTrainBoss);
            AccessTools.Field(typeof(CharacterData), "nameKey").SetValue(characterData, this.Name);
            AccessTools.Field(typeof(CharacterData), "projectilePrefab").SetValue(characterData, this.ProjectilePrefab);
            AccessTools.Field(typeof(CharacterData), "roomModifiers").SetValue(characterData, this.RoomModifiers);
            AccessTools.Field(typeof(CharacterData), "size").SetValue(characterData, this.Size);
            AccessTools.Field(typeof(CharacterData), "startingStatusEffects").SetValue(characterData, this.StartingStatusEffects);
            AccessTools.Field(typeof(CharacterData), "statusEffectImmunities").SetValue(characterData, this.StatusEffectImmunities);
            //AccessTools.Field(typeof(CardData), "stringBuilder").SetValue(cardData, this.);
            AccessTools.Field(typeof(CharacterData), "subtypeKeys").SetValue(characterData, this.SubtypeKeys);
            AccessTools.Field(typeof(CharacterData), "triggers").SetValue(characterData, this.Triggers);
            return characterData;
        }

        public void CreateAndSetCharacterArtPrefabVariantRef(string m_debugName, string m_AssetGUID)
        {
            var assetReferenceGameObject = new AssetReferenceGameObject();
            AccessTools.Field(typeof(AssetReferenceGameObject), "m_debugName")
                    .SetValue(assetReferenceGameObject, m_debugName);
            AccessTools.Field(typeof(AssetReferenceGameObject), "m_AssetGUID")
                .SetValue(assetReferenceGameObject, m_AssetGUID);
            this.CharacterPrefabVariantRef = assetReferenceGameObject;

            this.AssetPath = m_AssetGUID;
        }

        public void AddStartingStatusEffect(MTStatusEffect statusEffect, int stackCount)
        {
            this.StartingStatusEffects = BuilderUtils.AddStatusEffect(statusEffect, stackCount, this.StartingStatusEffects);
        }
    }
}
