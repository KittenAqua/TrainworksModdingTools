using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using BepInEx.Logging;
using HarmonyLib;
using I2.Loc;
using MonsterTrainModdingAPI.Builders;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace MonsterTrainModdingAPI.Managers
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
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader("BepInEx/plugins/" + path))
                {
                    // Read the stream to a string, and write the string to the console.
                    CSVstring = sr.ReadToEnd();
                }
            }
            catch (IOException e)
            {
                API.Log(LogLevel.Error, "We couldn't read the file at " + "BepInEx/plugins/" + path);
                API.Log(LogLevel.Error, e.Message);
            }

            List<string> categories = LocalizationManager.Sources[0].GetCategories(true, (List<string>)null);
            foreach (string Category in categories)
                LocalizationManager.Sources[0].Import_CSV(Category, CSVstring, eSpreadsheetUpdateMode.AddNewTerms, Separator);
        }

        public static void ImportSingleLocalization(string key, string type, string desc, string plural, string group, string descriptions, string english, string french, string german, string russian, string portuguese, string chinese)
        {
            if (!key.HasTranslation())
            {
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
