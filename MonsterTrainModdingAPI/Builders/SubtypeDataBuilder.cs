using System;
using System.Collections.Generic;
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
    public class SubtypeDataBuilder
    {
        public string _subtype { get; set; }
        public bool _isChampion { get; set; }
        public bool _isNone { get; set; }
        public bool _isTreasureCollector { get; set; }
        public bool _isImp { get; set; }

        /// <summary>
        /// Builds the SubtypeData represented by this builders's parameters;
        /// </summary>
        /// <returns>The newly created SubtypeData</returns>
        public SubtypeData Build()
        {
            SubtypeData subtypeData = new SubtypeData();
            AccessTools.Field(typeof(SubtypeData), "_subtype").SetValue(subtypeData, this._subtype);
            AccessTools.Field(typeof(SubtypeData), "_isChampion").SetValue(subtypeData, this._isChampion);
            AccessTools.Field(typeof(SubtypeData), "_isNone").SetValue(subtypeData, this._isNone);
            AccessTools.Field(typeof(SubtypeData), "_isTreasureCollector").SetValue(subtypeData, this._isTreasureCollector);
            AccessTools.Field(typeof(SubtypeData), "_isImp").SetValue(subtypeData, this._isImp);
            return subtypeData;
        }
    }
}
