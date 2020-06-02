using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using System.IO;

namespace MonsterTrainModdingAPI.Patches.SaveManagerPatches
{
    /// <summary>
    /// A Post Fix to Redirect Active Save Path Patch
    /// </summary>
    [HarmonyPatch(typeof(SaveManager), "ActiveSavePath", MethodType.Getter)]
    class SaveManagerActiveSavePathPatch
    {
        static void Postfix(ref string __result)
        {
            __result = Path.Combine(SaveManager.GetBaseSaveDirectory(), "saves", "saves-Modded.json");
        }
    }
}
