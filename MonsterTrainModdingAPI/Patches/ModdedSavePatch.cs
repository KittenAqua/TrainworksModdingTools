using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace MonsterTrainModdingAPI.Patches
{
    /// <summary>
    /// Credit to Rawsome for this code
    /// </summary>
    [HarmonyPatch(typeof(SaveManager), "ActiveSaveRunTypeData", MethodType.Getter)]
    class ModdedSavePatch
    {
        static readonly string moddedPrefix = "_modded-";

        static void Postfix(ref object __result)
        {
            if (__result != null)
            {
                Traverse filename = Traverse.Create(__result).Field("filename");

                if (!((string)filename.GetValue()).StartsWith(moddedPrefix))
                {
                    filename.SetValue($"{moddedPrefix}{filename.GetValue()}");
                }
            }
        }
    }
}
