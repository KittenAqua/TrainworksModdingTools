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
        public delegate void ProviderDelegate(IProvider newProvider);
        public static event ProviderDelegate NewProviderAvailableEvent;
        public static event ProviderDelegate NewProviderFullyInstalledEvent;
        public static event ProviderDelegate ProviderRemovedEvent;

        private static ProviderManager _instance;
        public static ProviderManager Instance
        {
            get
            {
                if (_instance == null) _instance = new ProviderManager();
                return _instance;
            }
        }

        public void NewProviderAvailable(IProvider newProvider)
        {
            NewProviderAvailableEvent(newProvider);
        }


        public void NewProviderFullyInstalled(IProvider newProvider)
        {
            NewProviderFullyInstalledEvent(newProvider);
        }

        public void ProviderRemoved(IProvider removeProvider)
        {
            ProviderRemovedEvent(removeProvider);
        }
    }
}
