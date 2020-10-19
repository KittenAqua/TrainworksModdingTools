using System;
using System.Collections.Generic;
using System.Text;

namespace Trainworks.Managers
{
    public class ProviderManager : IClient
    {
        private static IDictionary<Type, (bool, IProvider)> ProviderDictionary { get; set; } = new Dictionary<Type, (bool, IProvider)>();
        
        public static SaveManager SaveManager
        {
            get
            {
                TryGetProvider<SaveManager>(out SaveManager provider);
                return provider;
            }
        }

        public static CombatManager CombatManager
        {
            get
            {
                TryGetProvider<CombatManager>(out CombatManager provider);
                return provider;
            }
        }

        public static CardStatistics CardStatistics
        {
            get
            {
                TryGetProvider<CardStatistics>(out CardStatistics provider);
                return provider;
            }
        }

        /// <summary>
        /// Attempts to Get an IProvider
        /// </summary>
        /// <typeparam name="T">Type of IProvider</typeparam>
        /// <param name="provider">Provider if Succesful</param>
        /// <returns></returns>
        public static bool TryGetProvider<T>(out T provider) where T : IProvider
        {
            return TryGetProvider<T>(out provider, out _);
        }

        /// <summary>
        /// Attempts to Get an IProvider
        /// </summary>
        /// <typeparam name="T">Type of IProvider</typeparam>
        /// <param name="provider">Provider if Succesful</param>
        /// <param name="fullyInitialized">Whether the Provider has been fullyInitialized</param>
        /// <returns></returns>
        public static bool TryGetProvider<T>(out T provider, out bool fullyInitialized) where T : IProvider
        {
            if (ProviderDictionary.TryGetValue(typeof(T), out (bool, IProvider) provider1))
            {
                fullyInitialized = provider1.Item1;
                provider = (T)provider1.Item2;
                return true;
            }
            fullyInitialized = provider1.Item1;
            provider = (T)provider1.Item2;
            return false;
        }

        /// <summary>
        /// Informs ProviderManager of a new Provider
        /// </summary>
        /// <param name="newProvider">Provider</param>
        public void NewProviderAvailable(IProvider newProvider)
        {
            ProviderDictionary[newProvider.GetType()] = (false, newProvider);
            Trainworks.Log(BepInEx.Logging.LogLevel.Debug, newProvider.GetType().AssemblyQualifiedName + " Was Registered to ProviderManager");
        }

        /// <summary>
        /// Informs ProviderManager of a New fully Initialized Provider
        /// </summary>
        /// <param name="newProvider">Fully Initialized Provider</param>
        public void NewProviderFullyInstalled(IProvider newProvider)
        {
            if (ProviderDictionary.ContainsKey(newProvider.GetType()))
            {
                ProviderDictionary[newProvider.GetType()] = (true, newProvider);
            }
        }

        /// <summary>
        /// Informs ProviderManager of the Removal of a Provider
        /// </summary>
        /// <param name="removeProvider">Provider to Remove</param>
        public void ProviderRemoved(IProvider removeProvider)
        {
            ProviderDictionary.Remove(removeProvider.GetType());
        }
    }
}
