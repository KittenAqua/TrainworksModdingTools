using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace MonsterTrainModdingAPI.Patches.SaveManagerPatches
{
    [HarmonyPatch(typeof(SaveManager), "ActiveSaveData", MethodType.Getter)]
    public class SaveManagerActiveSaveDataPatch
    {
        static void Postfix(ref SaveData __result)
        {
            __result = SaveManagerExtension.moddedPlayerSave.saveData;
        }
    }
}
