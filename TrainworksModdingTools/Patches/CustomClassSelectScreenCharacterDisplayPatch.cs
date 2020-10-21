using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using UnityEngine;
using Trainworks.Managers;
using BepInEx.Logging;

namespace Trainworks.Patches
{

    // This patch displays custom characters on the clan select screen
    [HarmonyPatch(typeof(ClassSelectionScreen), "RefreshCharacters")]
    public class CustomClanSelectScreenPatch
    {
        static void Prefix(ref bool __state, ref ClassSelectCharacterDisplay[] ___characterDisplays)
        {
            __state = false;
            if (___characterDisplays == null)
            {
                __state = true;
            }
        }

        static void Postfix(ref bool __state, ref ClassSelectCharacterDisplay[] ___characterDisplays, ref Transform ___charactersRoot)
        {
            if (__state)
            {
                int customClassCount = CustomClassManager.CustomClassData.Values.Count;
                int totalClassCount = ProviderManager.SaveManager.GetAllGameData().GetAllClassDatas().Count;
                int vanillaClassCount = totalClassCount - customClassCount;
                
                // "totalClassCount + 1" to account for the random slot
                var characterDisplaysNew = new ClassSelectCharacterDisplay[(totalClassCount + 1) * 2];

                var characterDisplay = ___characterDisplays[0];

                //Debug.Log("DISPLAYS LENGTH: " + ___characterDisplays.Length);
                int i;
                for (i = 0; i < ___characterDisplays.Length; i++)
                {
                    int clanIndex = (int)AccessTools.Field(typeof(ClassSelectCharacterDisplay), "clanIndex").GetValue(___characterDisplays[i]);
                    if (clanIndex == vanillaClassCount + 1)
                    { // Change index of random clan select display to account for custom classes
                        AccessTools.Field(typeof(ClassSelectCharacterDisplay), "clanIndex").SetValue(___characterDisplays[i], totalClassCount + 1);
                        i++;
                    }
                    characterDisplaysNew[i] = ___characterDisplays[i];
                }

                var customClasses = CustomClassManager.CustomClassData.Values;

                int j = 0;
                foreach (ClassData customClassData in customClasses)
                {
                    // After vanilla clans, but before random slot
                    // Note that each clan has two entries in __state, hence "(j / 2)"
                    int clanIndex = vanillaClassCount + j + 1;

                    var customMainCharacterDisplay = GameObject.Instantiate(characterDisplay);
                    customMainCharacterDisplay.name = customClassData.name;
                    characterDisplaysNew[clanIndex] = customMainCharacterDisplay;
                    customMainCharacterDisplay.gameObject.SetActive(false);

                    var customSubCharacterDisplay = GameObject.Instantiate(characterDisplay);
                    customSubCharacterDisplay.name = customClassData.name + "sub";
                    characterDisplaysNew[clanIndex + vanillaClassCount + 1] = customMainCharacterDisplay;
                    customSubCharacterDisplay.gameObject.SetActive(false);

                    //var mainCharacters = (List<CharacterState>)(AccessTools.Field(typeof(ClassSelectCharacterDisplay), "characters").GetValue(customMainCharacterDisplay));

                    //CharacterState[] oldCharacterStates = customMainCharacterDisplay.GetComponentsInChildren<CharacterState>();

                    //var oldCharacterState = oldCharacterStates[0];
                    //var characterStateObject = oldCharacterState.gameObject;

                    //var mainCharacterIDs = CustomClassManager.CustomClassSelectScreenCharacterIDsMain[customClassData.GetID()];
                    //foreach (string mainCharacterID in mainCharacterIDs)
                    //{
                    //    var mainCharacterData = CustomCharacterManager.GetCharacterDataByID(mainCharacterID);
                    //    var assetRef = mainCharacterData.characterPrefabVariantRef;

                    //    var customStateObject = CustomAssetManager.LoadGameObjectFromAssetRef(assetRef);

                    //    customStateObject.transform.parent = customMainCharacterDisplay.gameObject.transform;
                    //}

                    //AccessTools.Field(typeof(ClassSelectCharacterDisplay), "clanIndex").SetValue(customMainCharacterDisplay, clanIndex);
                    //customMainCharacterDisplay.name = customClassData.GetID() + "_Main";

                    //foreach (CharacterState cs in oldCharacterStates)
                    //{
                    //    Debug.Log("DESTROY: " + cs.name);
                    //    cs.transform.parent = null;
                    //    GameObject.Destroy(cs);
                    //}

                    //oldCharacterStates = customMainCharacterDisplay.GetComponentsInChildren<CharacterState>();
                    //foreach (CharacterState cs in oldCharacterStates)
                    //{
                    //    Debug.Log("CHARACTER STATE: " + cs.name);
                    //}

                    //    i++;

                    //    var customSubCharacterDisplay = GameObject.Instantiate(characterDisplay);
                    //    customSubCharacterDisplay.transform.parent = ___charactersRoot;
                    //    customSubCharacterDisplay.gameObject.SetActive(false);
                    //    characterDisplaysNew[i] = customSubCharacterDisplay;
                    //    AccessTools.Field(typeof(ClassSelectCharacterDisplay), "clanIndex").SetValue(customSubCharacterDisplay, clanIndex);
                    //    customSubCharacterDisplay.name = customClassData.GetID() + "_Sub";

                    j++;
                }

                ___characterDisplays = characterDisplaysNew;

                //Debug.Log("LENGTH: " + characterDisplaysNew.Length);
                //for (int q = 0; q < characterDisplaysNew.Length; q++)
                //{
                //    Debug.Log("wow  " + characterDisplaysNew[q].name);
                //}
            }
        }
    }
}