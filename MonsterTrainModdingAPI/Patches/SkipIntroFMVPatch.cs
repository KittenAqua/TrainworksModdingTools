using HarmonyLib;

namespace MonsterTrainModdingAPI.Patches
{
    [HarmonyPatch(typeof(IntroFmvScreen), "Initialize")]
    class IntroFmvScreen_Initialize_Patch
    {
        static bool Prefix(ref IntroFmvScreen __instance)
        {
            __instance.Invoke("GoToNextScreen", 0);
            return false;
        }
    }
}
