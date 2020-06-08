using System;
using System.Collections.Generic;
using System.Text;
using BepInEx;
using System.Linq;

namespace MonsterTrainModdingAPI.Managers
{
    public static class PluginManager
    {
        public static Dictionary<string, BaseUnityPlugin> Plugins { get; } = new Dictionary<string, BaseUnityPlugin>();

        public static List<string> GetAllPluginGUIDs()
        {
            return Plugins.Values.ToList().Select((x) => x.Info.Metadata.GUID).ToList();
        }

        public static List<string> GetAllPluginNames()
        {
            return Plugins.Keys.ToList();
        }

        public static BaseUnityPlugin GetPluginFromName(string name)
        {
            return Plugins[name];
        }

        public static void RegisterPlugin(BaseUnityPlugin plugin)
        {
            Plugins.Add(plugin.Info.Metadata.Name, plugin);
        }
    }
}
