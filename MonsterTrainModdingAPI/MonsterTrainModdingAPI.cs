using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace MonsterTrainModdingAPI
{
    // Credit to Rawsome, Stable Infery for the base of this method.
    /// <summary>
    /// The entry point for the API.
    /// </summary>
    [BepInPlugin("api.modding.train.monster", "Monster Train Modding API", "0.0.7")]
    [BepInProcess("MonsterTrain.exe")]
    [BepInProcess("MtLinkHandler.exe")]
    public class API : BaseUnityPlugin
    {
        /// <summary>
        /// The API's logging source.
        /// </summary>
        private static readonly ManualLogSource logger = BepInEx.Logging.Logger.CreateLogSource("API");
        
        /// <summary>
        /// Logs a message into the BepInEx console.
        /// </summary>
        /// <param name="lvl">The severity of the message</param>
        /// <param name="msg">The message to log</param>
        public static void Log(LogLevel lvl, string msg)
        {
            logger.Log(lvl, msg);
        }
        
        /// <summary>
        /// Called on startup. Executes all Harmony patches anywhere in the API.
        /// </summary>
        private void Awake()
        {
            DepInjector.AddClient(new MonsterTrainModdingAPI.Managers.ProviderManager());

            MonsterTrainModdingAPI.Managers.CustomTriggerManager.RegisterAllTriggers();

            var harmony = new Harmony("api.modding.train.monster");
            harmony.PatchAll();
        }
    }
}
