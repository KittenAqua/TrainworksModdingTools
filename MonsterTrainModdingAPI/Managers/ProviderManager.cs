using System;
using System.Collections.Generic;
using System.Text;
using ShinyShoe;
using ShinyShoe.Audio;
using System.Reflection;
using System.Linq;

namespace MonsterTrainModdingAPI.Managers
{

    public class ProviderManager : IClient
    {
        //Use for Searching through all Types Castable to IProvider
        private static IEnumerable<Type> IProviderTypes;

        /// <summary>
        /// Provider Dictionary holds a Single Provider for each type
        /// </summary>
        private static Dictionary<Type, IProvider> ProviderDictionary = new Dictionary<Type, IProvider>();
        /// <summary>
        /// Signal Dictionary holds a Signal<IProvider> (though stored as BaseSignal) for each type
        /// </summary>
        private static Dictionary<Type, BaseSignal> SignalDictionary = new Dictionary<Type, BaseSignal>();
        /// <summary>
        /// Attempts to get a Provider of a type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static bool TryGetProvider<T>(out T provider) where T : IProvider
        {
            if (!ProviderDictionary.ContainsKey(typeof(T)))
            {
                provider = default;
                return false;
            }
            provider = (T)ProviderDictionary[typeof(T)];
            return true;
        }
        /// <summary>
        /// Adds a Listener for when new Providers are initialized
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="callback"></param>
        public static void AddNewProviderListener<T>(Action<T> callback) where T : IProvider
        {
            if (!SignalDictionary.ContainsKey(typeof(T)))
            {
                Signal<T> t = new Signal<T>();
                SignalDictionary.Add(typeof(T), t);
                t.AddListener(callback);
                return;
            }
            Signal<T> s = (Signal<T>)SignalDictionary[typeof(T)];
            s.AddListener(callback);
        }

        #region Singleton
        /// <summary>
        /// Singleton Structure to Allow implementation of IClient 
        /// </summary>
        private static ProviderManager _instance;
        public static ProviderManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ProviderManager();
                    Assembly assembly = Assembly.GetExecutingAssembly();
                    Type[] types = assembly.GetTypes();
                    IProviderTypes = types.Where(t => t.IsAssignableFrom(typeof(IProvider)));
                }
                return _instance;
            }
        }
        #endregion

        #region IClient Methods
        public void NewProviderAvailable(IProvider newProvider)
        {
            foreach (Type type in IProviderTypes)
            {
                if (newProvider.GetType() == type)
                {
                    if (ProviderDictionary.ContainsKey(type))
                    {
                        ProviderDictionary[type] = newProvider;
                        _DispatchSignal(type, newProvider);
                        return;
                    }
                    else
                    {
                        ProviderDictionary.Add(type, newProvider);
                        _DispatchSignal(type, newProvider);
                        return;
                    }
                }
            }
        }

        private void _DispatchSignal(Type SignalGeneric, IProvider provider)
        {
            MethodInfo method = this.GetType().GetMethod("DispatchSignal");
            MethodInfo generic = method.MakeGenericMethod(SignalGeneric);
            generic.Invoke(Convert.ChangeType(provider,SignalGeneric),null);
        }
        private void DispatchSignal<SignalGeneric>(SignalGeneric provider) where SignalGeneric : IProvider
        {
            Signal<SignalGeneric> signal = (Signal<SignalGeneric>)SignalDictionary[typeof(SignalGeneric)];
            signal.Dispatch(provider);
        }

        public void NewProviderFullyInstalled(IProvider newProvider)
        {

        }

        public void ProviderRemoved(IProvider removeProvider)
        {
            foreach (Type type in IProviderTypes)
            {
                if (removeProvider.GetType() == type)
                {
                    ProviderDictionary.Remove(type);
                    return;
                }
            }
        }
        #endregion
    }
}
