using HarmonyLib;
using Trainworks.Managers;
using System;

namespace Trainworks.Patches
{
    public class AccessUnitSynthesisMapping
    {
        public static void FindUnitSynthesisMappingInstanceToStub()
        {
            // Gets a reference to AllGameData with Trainworks
            AllGameData testData = ProviderManager.SaveManager.GetAllGameData();

            // Use AllGameData to get access to BalanceData
            BalanceData balanceData = testData.GetBalanceData();

            // Use BalanceData to get access to the current instance of the UnitSynthesisMapping
            UnitSynthesisMapping mappingInstance = balanceData.SynthesisMapping;
            if (mappingInstance == null)
            {
                Trainworks.Log(BepInEx.Logging.LogLevel.Warning, "Failed to find a mapping instance.");
            }
            else
            {
                Trainworks.Log("Able to find mapping instance: " + mappingInstance.GetID()); // Test to see if this is a different instance
            }

            // Calls CollectMappingData method
            RecallingCollectMappingData.CollectMappingDataStub(mappingInstance);
        }
    }

    /// <summary>
    /// Solely exists to allow calling of private method CollectMappingData from within a UnitSynthesisMapping instance
    /// This resets the editorMappings list within the UnitSynthesisMapping class in Monster Train
    /// Effective for creating unit synthesis connections in TLD DLC after appropriate custom CharacterData and custom CardUpgradeData has been added to AllGameData
    /// *Required: Called after adding custom content to work for this purpose
    /// Current Use: Each mod will have access to this Trainworks method so they can call it after adding all their content (At the end of their Initialize method generally works)
    /// Ideally: This is called a single time after all mods/plugins have added their custom content
    /// </summary>
    [HarmonyPatch(typeof(UnitSynthesisMapping), "CollectMappingData", new Type[] { })]
    class RecallingCollectMappingData
    {
        [HarmonyReversePatch]
        public static void CollectMappingDataStub(object instance)
        {
            // It's a stub so it has no initial content
        }
    }
}
