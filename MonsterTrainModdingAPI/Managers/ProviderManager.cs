using System;
using System.Collections.Generic;
using System.Text;

namespace MonsterTrainModdingAPI.Managers
{
    public class ProviderManager : IClient
    {
        private static IDictionary<Type, (bool, IProvider)> ProviderDictionary { get; set; } = new Dictionary<Type, (bool,IProvider)>();

        public static bool TryGetProvider<T>(out T provider) where T : IProvider
        {
            return TryGetProvider<T>(out provider, out _);
        }

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
        public void NewProviderAvailable(IProvider newProvider)
        {
            if (ProviderDictionary.ContainsKey(newProvider.GetType()))
            {
                ProviderDictionary[newProvider.GetType()] = (false,newProvider);
            }
            else
            {
                ProviderDictionary.Add(newProvider.GetType(), (false,newProvider));
            }
            MonsterTrainModdingAPI.API.Log(BepInEx.Logging.LogLevel.Debug, newProvider.GetType().AssemblyQualifiedName + " Was Registered to ProviderManager");

        }

        public void NewProviderFullyInstalled(IProvider newProvider)
        {
            if (ProviderDictionary.ContainsKey(newProvider.GetType()))
            {
                ProviderDictionary[newProvider.GetType()] = (true, newProvider);
            }
        }

        public void ProviderRemoved(IProvider removeProvider)
        {
            if (ProviderDictionary.ContainsKey(removeProvider.GetType()))
            {
                ProviderDictionary.Remove(removeProvider.GetType());
            }
        }
    }
}
