using System;
using System.Collections.Generic;
using BepInEx;
using BepInEx.Harmony;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using UnityEngine.AddressableAssets;
using ShinyShoe;

namespace MonsterTrainModdingAPI
{
    // Credit to Rawsome, Stable Infery for the base of this method.
    [BepInPlugin("api.modding.train.monster", "Monster Train Modding API", "0.0.2")]
    [BepInProcess("MonsterTrain.exe")]
    [BepInProcess("MtLinkHandler.exe")]
    public class TestPlugin : BaseUnityPlugin
    {
        void Awake()
        {
            var harmony = new Harmony("api.modding.train.monster");
            harmony.PatchAll();
        }
    }
}
