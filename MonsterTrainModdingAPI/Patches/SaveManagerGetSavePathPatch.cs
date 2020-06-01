using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using MonsterTrainModdingAPI.Managers;

namespace MonsterTrainModdingAPI.Patches
{
    [HarmonyPatch(typeof(SaveManager), "GetGameSavePath")]
    class SaveManagerGetSavePathPatch
    {
        static void Postfix(ref string __result, SaveManager __instance)
        {
            string newFileName = "";
            foreach(string s in PluginManager.GetAllPluginNames())
            {
                
            }
            __result += "..\\" + newFileName + ".json";
        }
    }
}
