using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using BepInEx;
using System.Linq;

namespace Trainworks.Managers
{
    public static class PluginManager
    {
        /// <summary>
        /// Maps BepInEx Plugin GUIDs to their BaseUnityPlugin instances.
        /// </summary>
        public static Dictionary<string, BaseUnityPlugin> Plugins { get; } = new Dictionary<string, BaseUnityPlugin>();
        /// <summary>
        /// Maps AssemblyNames to their respective BepInEx Plugins
        /// </summary>
        public static IDictionary<string, string> AssemblyNameToPluginGUID { get; } = new Dictionary<string, string>();
        /// <summary>
        /// Maps BepInEx Plugin GUIDs to their Directory Paths
        /// </summary>
        public static IDictionary<string, string> PluginGUIDToPath { get; } = new Dictionary<string, string>();
        /// <summary>
        /// Get the GUIDs of all plugins recognized by BepInEx.
        /// </summary>
        /// <returns>GUIDs of all plugin recognized by BepInEx</returns>
        public static List<string> GetAllPluginGUIDs()
        {
            return Plugins.Values.ToList().Select((x) => x.Info.Metadata.GUID).ToList();
        }
        /// <summary>
        /// Get the names of all plugins recognized by BepInEx.
        /// </summary>
        /// <returns>Names of all plugins recognized by BepInEx</returns>
        public static List<string> GetAllPluginNames()
        {
            return Plugins.Keys.ToList();
        }
        /// <summary>
        /// Get the plugin with the specified name.
        /// </summary>
        /// <param name="name">Name of the plugin to get</param>
        /// <returns>Plugin with the specified name</returns>
        public static BaseUnityPlugin GetPluginFromName(string name)
        {
            return Plugins[name];
        }
        /// <summary>
        /// Register a plugin with the plugin manager.
        /// </summary>
        /// <param name="plugin">Plugin to Register</param>
        public static void RegisterPlugin(BaseUnityPlugin plugin)
        {
            Plugins.Add(plugin.Info.Metadata.GUID, plugin);
            
            var assembly = plugin.GetType().Assembly;
            var uri = new UriBuilder(assembly.CodeBase);
            var path = Path.GetDirectoryName(Uri.UnescapeDataString(uri.Path));
            PluginGUIDToPath[plugin.Info.Metadata.GUID] = path;
            AssemblyNameToPluginGUID[assembly.FullName] = plugin.Info.Metadata.GUID;
        }
    }
}
