using System;
using System.Collections.Generic;
using System.Text;
using BepInEx;
using System.Linq;

namespace MonsterTrainModdingAPI.Managers
{
    public static class PluginManager
    {
        private static readonly Dictionary<string, BaseUnityPlugin> plugins = new Dictionary<string, BaseUnityPlugin>();

        public static List<string> GetAllPluginGUIDs()
        {
            return plugins.Values.ToList().Select((x) => x.Info.Metadata.GUID).ToList();
        }

        public static List<string> GetAllPluginNames()
        {
            return plugins.Keys.ToList();
        }
        public static BaseUnityPlugin GetPluginFromName(string name)
        {
            return plugins[name];
        }

        public static void RegisterPlugin(BaseUnityPlugin plugin)
        {
            plugins.Add(plugin.name, plugin);
        }
    }
}
