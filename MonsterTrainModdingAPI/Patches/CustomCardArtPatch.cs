using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using HarmonyLib;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using ShinyShoe;
using Spine.Unity;
using MonsterTrainModdingAPI.Managers;
using TMPro;
using MonsterTrainModdingAPI.Utilities;

namespace MonsterTrainModdingAPI.Patches
{
    [HarmonyPatch(typeof(CharacterData), "GetCharacterPrefabVariant")]
    class LoadCustomCharacterArtPatch
    {
        static GameObject realCharacterReference;

        static void Postfix(ref CharacterData __instance, ref GameObject __result)
        {
            if (__result != null)
            {
                realCharacterReference = __result;
            }
            if (__result == null && CustomCardManager.CustomCharacterData.ContainsKey(__instance.GetID()))
            {
                Debug.Log("CUSTOM CHARACTER: " + __instance.GetName());
                CharacterData characterData = CustomCardManager.CustomCharacterData[__instance.GetID()];

                var realCharacter = GameObject.Instantiate(realCharacterReference);
                var uiMeshSpine = realCharacter.GetComponentInChildren<CharacterUIMeshSpine>();
                var uiMeshParent = uiMeshSpine.gameObject.transform.parent;
                var characterState = realCharacter.GetComponentInChildren<CharacterState>();
                //GameObject characterUIMeshSpineGO = new GameObject();
                //GameObject characterUIMeshSpineChild = new GameObject();
                //characterUIMeshSpineChild.transform.parent = characterUIMeshSpineGO.transform.parent;
                //var comp4 = characterUIMeshSpineGO.AddComponent<CharacterUIMesh>();
                //var comp5 = characterUIMeshSpineGO.AddComponent<MeshRenderer>();
                //var comp6 = characterUIMeshSpineGO.AddComponent<MeshFilter>();
                Debug.Log("UI MESH SPINE: " + uiMeshSpine);
                uiMeshSpine.gameObject.transform.parent = null;
                GameObject.Destroy(uiMeshSpine.gameObject);
                Debug.Log("UI MESH SPINE: " + uiMeshSpine);
                Debug.Log("UI MESH SPINE: " + realCharacter.GetComponentInChildren<CharacterUIMeshSpine>());
                Debug.Log("UI MESH: " + realCharacter.GetComponentInChildren<CharacterUIMesh>());
                //characterUIMeshSpineGO.transform.parent = uiMeshParent.transform;
                Debug.Log("UI MESH: " + realCharacter.GetComponentInChildren<CharacterUIMesh>());
                var realCharacterUI = realCharacter.GetComponentInChildren<CharacterUI>();
                Debug.Log("CHARACTER: " + realCharacter);
                Debug.Log("CHARACTER UI: " + realCharacterUI);
                Debug.Log("CHARACTER STATE: " + characterState);
                Debug.Log("HALFWAY THERE");

                Debug.Log("CHARACTER MESH: " + realCharacter.GetComponentInChildren<CharacterUIMeshBase>(false));
                Debug.Log("CHARACTER MESH: " + realCharacter.GetComponentInChildren<CharacterUIMeshBase>(true));
                Debug.Log("CHARACTER MESH: " + realCharacterUI.GetComponentInChildren<CharacterUIMeshBase>(false));
                Debug.Log("CHARACTER MESH: " + realCharacterUI.GetComponentInChildren<CharacterUIMeshBase>(true));

                Debug.Log(realCharacter.GetComponentInChildren<MeshRenderer>(true));
                Debug.Log(realCharacter.GetComponentInChildren<MeshRenderer>(true).gameObject);
                realCharacter.GetComponentInChildren<MeshRenderer>(true).gameObject.SetActive(true);
                //Debug.Log(comp4.isActiveAndEnabled);
                //Debug.Log(comp5.);

                var assetRef = (AssetReferenceGameObject)AccessTools.Field(typeof(CharacterData), "characterPrefabVariantRef").GetValue(characterData);
                string assetPath = (string)AccessTools.Field(typeof(AssetReferenceGameObject), "m_AssetGUID").GetValue(assetRef);
                string cardPath = "BepInEx/plugins/" + assetPath;
                byte[] fileData = File.ReadAllBytes(cardPath);
                Texture2D tex = new Texture2D(1, 1);
                UnityEngine.ImageConversion.LoadImage(tex, fileData);
                Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 128f);

                Debug.Log("SPRITE: " + sprite);
                Debug.Log("SPRITE RENDERER: " + realCharacterUI.GetComponentInChildren<SpriteRenderer>());

                realCharacterUI.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
                AccessTools.Field(typeof(CharacterState), "sprite").SetValue(characterState, sprite);

                Debug.Log("SPRITE IN: " + characterState.GetSprite());

                //Debug.Log("1");
                //GameObject characterStateGO = new GameObject(); //Create the GameObject
                //GameObject characterScaleGO = new GameObject();
                //characterScaleGO.transform.parent = characterStateGO.transform;
                //GameObject characterUIGO = new GameObject();
                //characterUIGO.transform.parent = characterScaleGO.transform;
                //GameObject characterUIMeshSpineGO = new GameObject();
                //characterUIMeshSpineGO.transform.parent = characterUIGO.transform;
                //var comp1 = characterStateGO.AddComponent<CharacterState>();
                //var comp2 = characterUIGO.AddComponent<CharacterUI>();
                //var comp3 = characterUIGO.AddComponent<SpriteRenderer>();
                //comp3.sprite = sprite;
                //var comp4 = characterUIMeshSpineGO.AddComponent<CharacterUIMesh>();
                //var comp5 = characterUIMeshSpineGO.AddComponent<MeshFilter>();

                __result = realCharacter;
            }
        }
    }
}
