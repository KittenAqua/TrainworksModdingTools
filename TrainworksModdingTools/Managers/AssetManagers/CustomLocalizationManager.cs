using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using BepInEx.Logging;
using HarmonyLib;
using I2.Loc;
using Trainworks.Builders;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace Trainworks.Managers
{
    /// <summary>
    /// Handles loading of custom localized strings.
    /// </summary>
    public class CustomLocalizationManager
    {

        // Required because the library just chokes. When we need plural support, we can reimplement this.
        // The existing issue was that LocalizationUtil.GetPluralsUsedByLanguages() returns one less than mTerm.Languages.Length
        // It needs to return the same amount. Changing the number is also a problem, too lazy to debug now.
        // This function is only in use by csv imports, so we affect nothing else by skipping it.
        [HarmonyPatch(typeof(LanguageSourceData), "SimplifyPlurals")]
        class SkipBrokenLibraryFunction
        {
            // Creates and registers card data for each card class
            static bool Prefix(ref LanguageSourceData __instance)
            {
                return true;
            }
        }

        /// <summary>
        /// Imports new data to the English Localization from a CSV string with separators ';'.
        /// Required columns are 'Keys', 'Type', 'Plural', 'Group', 'Desc', 'Descriptions'
        /// </summary>
        public static void ImportCSV(string path, char Separator = ',')
        {
            string CSVstring = "";

            var localPath = Path.GetDirectoryName(new Uri(Assembly.GetCallingAssembly().CodeBase).LocalPath);
            Trainworks.Log(BepInEx.Logging.LogLevel.All, "File: " + Path.Combine(localPath, path));

            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(Path.Combine(localPath, path)))
                {
                    // Read the stream to a string, and write the string to the console.
                    CSVstring = sr.ReadToEnd();
                }
            }
            catch (IOException e)
            {
                Trainworks.Log(LogLevel.Error, "We couldn't read the file at " + Path.Combine(localPath, path));
                Trainworks.Log(LogLevel.Error, e.Message);
            }

            List<string> categories = LocalizationManager.Sources[0].GetCategories(true, (List<string>)null);
            foreach (string Category in categories)
                LocalizationManager.Sources[0].Import_CSV(Category, CSVstring, eSpreadsheetUpdateMode.AddNewTerms, Separator);
        }
        /// <summary>
        /// Appends a Single Localization to the Localization Manager
        /// </summary>
        /// <param name="key">a string that is not null or empty that will act as a key for other strings</param>
        /// <param name="type">type for the string to be converted to, typically is Text</param>
        /// <param name="desc">description of what key represents, typically unused</param>
        /// <param name="plural">The plural of the string, currently broken</param>
        /// <param name="group">the name of the group that the key is apart of</param>
        /// <param name="descriptions">an extra description</param>
        /// <param name="english">The English Translation</param>
        /// <param name="french">The French Translation</param>
        /// <param name="german">The German Translation</param>
        /// <param name="russian">The Russian Translation</param>
        /// <param name="portuguese">The Portuguese Translation</param>
        /// <param name="chinese">The Chinese Translation</param>
        public static void ImportSingleLocalization(string key, string type, string desc, string plural, string group, string descriptions, string english, string french = null, string german = null, string russian = null, string portuguese = null, string chinese = null)
        {
            if (string.IsNullOrEmpty(key)) return;
            if (!key.HasTranslation())
            {
                if (french == null) french = english;
                if (german == null) german = english;
                if (russian == null) russian = english;
                if (portuguese == null) portuguese = english;
                if (chinese == null) chinese = english;

                var miniCSVBuilder = new System.Text.StringBuilder();
                miniCSVBuilder.Append("Key;Type;Desc;Plural;Group;Descriptions;English [en-US];French [fr-FR];German [de-DE];Russian;Portuguese (Brazil);Chinese [zh-CN]\n");
                miniCSVBuilder.Append(key + ";");
                miniCSVBuilder.Append(type + ";");
                miniCSVBuilder.Append(desc + ";");
                miniCSVBuilder.Append(plural + ";");
                miniCSVBuilder.Append(group + ";");
                miniCSVBuilder.Append(descriptions + ";");
                miniCSVBuilder.Append(english + ";");
                miniCSVBuilder.Append(french + ";");
                miniCSVBuilder.Append(german + ";");
                miniCSVBuilder.Append(russian + ";");
                miniCSVBuilder.Append(portuguese + ";");
                miniCSVBuilder.Append(chinese);

                List<string> categories = LocalizationManager.Sources[0].GetCategories(true, (List<string>)null);
                foreach (string Category in categories)
                {
                    LocalizationManager.Sources[0].Import_CSV(Category, miniCSVBuilder.ToString(), eSpreadsheetUpdateMode.AddNewTerms, ';');
                }
            }
        }

        public static string ExportCSV(int language=0)
        {
            string ret = "";
            List<string> categories = LocalizationManager.Sources[0].GetCategories(true, (List<string>)null);
            foreach (string Category in categories)
                ret += LocalizationManager.Sources[0].Export_CSV(Category);
            
            return ret;
        }
    }
}
