using System;
using System.Collections.Generic;
using BepInEx;
using BepInEx.Harmony;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using UnityEngine.AddressableAssets;
using ShinyShoe;
using Trainworks.Managers;
using Trainworks.Enums;

namespace Trainworks.Builders
{
    public class SubtypeDataBuilder
    {
        public string _Subtype { get; set; }
        public bool _IsChampion { get; set; }
        public bool _IsNone { get; set; }
        public bool _IsTreasureCollector { get; set; }
        public bool _IsImp { get; set; }

        /// <summary>
        /// Builds the SubtypeData represented by this builders's parameters;
        /// </summary>
        /// <returns>The newly created SubtypeData</returns>
        public SubtypeData Build()
        {
            SubtypeData subtypeData = new SubtypeData();
            AccessTools.Field(typeof(SubtypeData), "_subtype").SetValue(subtypeData, this._Subtype);
            AccessTools.Field(typeof(SubtypeData), "_isChampion").SetValue(subtypeData, this._IsChampion);
            AccessTools.Field(typeof(SubtypeData), "_isNone").SetValue(subtypeData, this._IsNone);
            AccessTools.Field(typeof(SubtypeData), "_isTreasureCollector").SetValue(subtypeData, this._IsTreasureCollector);
            AccessTools.Field(typeof(SubtypeData), "_isImp").SetValue(subtypeData, this._IsImp);
            return subtypeData;
        }
    }
}
