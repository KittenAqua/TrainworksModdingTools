using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using MonsterTrainModdingAPI.Managers;

namespace MonsterTrainModdingAPI
{
    // Credit to Rawsome, Stable Infery for the base of this method.
    [BepInPlugin("api.modding.train.monster", "Monster Train Modding API", "0.0.5")]
    [BepInProcess("MonsterTrain.exe")]
    [BepInProcess("MtLinkHandler.exe")]
    public class API : BaseUnityPlugin
    {
        private static ManualLogSource logger = BepInEx.Logging.Logger.CreateLogSource("API");
        
        public static void Log(LogLevel lvl, string msg)
        {
            logger.Log(lvl, msg);
        }
        
        private void Awake()
        {
            var harmony = new Harmony("api.modding.train.monster");
            harmony.PatchAll();

            DepInjector.AddClient(ProviderManager.Instance);
        }
    }
}
