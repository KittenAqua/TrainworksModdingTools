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

namespace MonsterTrainModdingAPI.Patches
{
    [HarmonyPatch(typeof(CardState), "GetCardArtPrefabVariant")]
    class LoadCustomCardArtPatch
    {
        static void Postfix(ref CardState __instance, ref GameObject __result)
        {
            if (__result == null && CustomCardManager.CustomCardData.ContainsKey(__instance.GetCardDataID()))
            {
                CardData cardData = CustomCardManager.GetCardDataByID(__instance.GetCardDataID());
                var assetRef = (AssetReferenceGameObject)AccessTools.Field(typeof(CardData), "cardArtPrefabVariantRef").GetValue(cardData);
                string assetPath = (string)AccessTools.Field(typeof(AssetReferenceGameObject), "m_AssetGUID").GetValue(assetRef);
                string cardPath = "BepInEx/plugins/" + assetPath;
                byte[] fileData = File.ReadAllBytes(cardPath);

                Texture2D tex = new Texture2D(1, 1);
                UnityEngine.ImageConversion.LoadImage(tex, fileData);

                Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 128f);

                GameObject NewObj = new GameObject(); //Create the GameObject
                Image newImage = NewObj.AddComponent<Image>(); //Add the Image Component script
                newImage.sprite = sprite; //Set the Sprite of the Image Component on the new GameObject

                __result = NewObj;
            }
        }
    }

    [HarmonyPatch(typeof(CharacterData), "HasCharacterPrefabVariant")]
    class YesIHaveArtPatch
    {
        static void Postfix(ref CharacterData __instance, ref bool __result)
        {
            if (CustomCardManager.CustomCharacterData.ContainsKey(__instance.GetID()))
            {
                __result = true;
                //Debug.Log("I HAVE IT DUDE");
            }
        }
    }

    [HarmonyPatch(typeof(CharacterUIMesh), "Setup")]
    class LoadCustomCharacterArtPatch1
    {
        static void Prefix(ref Sprite charSprite, ref CharacterUIMesh __instance)
        {
            Debug.Log("CHARACTER UI MESH SETUP");
            Debug.Log(__instance.gameObject.name);
            Debug.Log(charSprite);
            Debug.Log(__instance.GetComponent<MeshFilter>());
            Debug.Log(__instance.GetComponent<MeshRenderer>());
            Debug.Log(__instance.meshRenderer);
            Debug.Log(__instance.meshRenderer.material);
            Debug.Log(__instance.meshRenderer.material.mainTexture);
            Debug.Log(charSprite.bounds.size.x);
            Debug.Log(charSprite.bounds.size.y);
            //Debug.Log(__instance.GetComponent<MeshFilter>().mesh);
            //Debug.Log(__instance.GetComponent<MeshFilter>().mesh.bounds);
            Debug.Log(charSprite.texture);
            //Debug.Log(System.Enum.GetNames(typeof(CharacterUI.Anim)).Length);
            //Debug.Log(__instance.GetComponentsInChildren<SkeletonAnimation>().Length);
            //Debug.Log(__instance.GetComponentsInChildren<BoneFollower>().Length);
            //Traverse filename = Traverse.Create(__instance).Field("_animInfos");
            //AccessTools.
            //Debug.Log(charSprite.texture);
            //Debug.Log(charSprite.bounds);
            //Debug.Log(charSprite.bounds.size);
            //Debug.Log(AccessTools.Field(typeof(CharacterUIMeshSpine), "_animInfos").GetValue(__instance));
        }
        static void Postfix(ref Sprite charSprite, ref CharacterUIMesh __instance)
        {
            Debug.Log(".......POST");
            Debug.Log(__instance.gameObject.name);
            Debug.Log(charSprite);
            Debug.Log(__instance.meshRenderer.material.mainTexture);
            //Debug.Log(charSprite.texture);
            //Debug.Log(charSprite.bounds);
            //Debug.Log(charSprite.bounds.size);
            //Debug.Log(AccessTools.Field(typeof(CharacterUIMesh), "_animInfos").GetValue(__instance));
        }
    }

    ////[HarmonyPatch(typeof(CharacterUIMeshSpine), "Setup")]
    ////class LoadCustomCharacterArtPatch1
    ////{
    ////    static void Prefix(ref Sprite charSprite, ref CharacterUIMeshSpine __instance)
    ////    {
    ////        Debug.Log("CHARACTER UI MESH SETUP");
    ////        Debug.Log(__instance.gameObject.name);
    ////        Debug.Log(charSprite);
    ////        Debug.Log(System.Enum.GetNames(typeof(CharacterUI.Anim)).Length);
    ////        Debug.Log(__instance.GetComponentsInChildren<SkeletonAnimation>().Length);
    ////        Debug.Log(__instance.GetComponentsInChildren<BoneFollower>().Length);
    ////        //Traverse filename = Traverse.Create(__instance).Field("_animInfos");
    ////        //AccessTools.
    ////        //Debug.Log(charSprite.texture);
    ////        //Debug.Log(charSprite.bounds);
    ////        //Debug.Log(charSprite.bounds.size);
    ////        //Debug.Log(AccessTools.Field(typeof(CharacterUIMeshSpine), "_animInfos").GetValue(__instance));
    ////    }
    ////    static void Postfix(ref Sprite charSprite, ref CharacterUIMeshSpine __instance)
    ////    {
    ////        Debug.Log(".......POST");
    ////        Debug.Log(__instance.gameObject.name);
    ////        Debug.Log(charSprite);
    ////        //Debug.Log(charSprite.texture);
    ////        //Debug.Log(charSprite.bounds);
    ////        //Debug.Log(charSprite.bounds.size);
    ////        Debug.Log(AccessTools.Field(typeof(CharacterUIMeshSpine), "_animInfos").GetValue(__instance));
    ////    }
    ////}

    ////[HarmonyPatch(typeof(CharacterUIMeshSpine), "PlayAnimLoop")]
    ////class LoadCustomCharacterArtPatch3
    ////{
    ////    static void Postfix(ref CharacterUIMeshSpine __instance)
    ////    {
    ////        Debug.Log("PlayAnimLoop");
    ////        Debug.Log(__instance.MeshRenderer);
    ////        Debug.Log(__instance.MeshRenderer.GetComponent<MeshFilter>());
    ////        Debug.Log(__instance.MeshRenderer.GetComponent<MeshFilter>().mesh);
    ////        Debug.Log(__instance.MeshRenderer.GetComponent<MeshFilter>().mesh.bounds);
    ////    }
    ////}

    ////[HarmonyPatch(typeof(CharacterUIMeshSpine), "GetAnimDuration")]
    ////class LoadCustomCharacterArtPatch6
    ////{
    ////    static void Prefix(ref CharacterUIMeshSpine __instance)
    ////    {
    ////        Debug.Log("GetAnimDuration");
    ////    }
    ////}

    ////[HarmonyPatch(typeof(CharacterUIMeshSpine), "ChooseRandomAnimStart")]
    ////class LoadCustomCharacterArtPatch7
    ////{
    ////    static void Prefix()
    ////    {
    ////        Debug.Log("ChooseRandomAnimStart");
    ////    }
    ////}

    ////[HarmonyPatch(typeof(CharacterUIMeshSpine), "CreateAnimInfo")]
    ////class LoadCustomCharacterArtPatch4
    ////{
    ////    static void Postfix(ref CharacterUIMeshSpine __instance)
    ////    {
    ////        Debug.Log("CreateAnimInfo");
    ////        Debug.Log(AccessTools.Field(typeof(CharacterUIMeshSpine), "_animInfos").GetValue(__instance));
    ////    }
    ////}

    ////[HarmonyPatch(typeof(MaterialPropertyBlock), "SetFloat", new Type[] { typeof(int), typeof(float) })]
    ////class LoadCustomCharacterArtPatch8
    ////{
    ////    static void Postfix()
    ////    {
    ////        Debug.Log("SetFloat");
    ////    }
    ////}

    //[HarmonyPatch(typeof(CharacterUIMeshSpine), "GetAnimInfo")]
    //class LoadCustomCharacterArtPatch5
    //{
    //    static void Postfix(ref CharacterUIMeshSpine __instance)
    //    {
    //        if (__instance.gameObject.name == "BLUE EYES SPINE")
    //        {
    //            Debug.Log("GetAnimInfo");
    //            //Debug.Log(__instance.gameObject.name);
    //            //Debug.Log(AccessTools.Field(typeof(CharacterUIMeshSpine), "_animInfos").GetValue(__instance));
    //        }
    //    }
    //}

    //[HarmonyPatch(typeof(CharacterUI), "SetupAnchors")]
    //class LoadCustomCharacterArtPatch9
    //{
    //    static void Prefix(ref Transform ___vfxCenterAnchor, ref Transform ___vfxSideFwdAnchor, ref Transform ___vfxSideBackAnchor, ref Transform ___vfxSideFwdBottomAnchor, ref Transform ___vfxSideBackBottomAnchor)
    //    {
    //        Debug.Log("SetupAnchors");
    //        Debug.Log(___vfxCenterAnchor);
    //        Debug.Log(___vfxSideBackAnchor);
    //        Debug.Log(___vfxSideFwdAnchor);
    //        Debug.Log(___vfxSideBackBottomAnchor);
    //        Debug.Log(___vfxSideFwdBottomAnchor);
    //    }
    //}

    //[HarmonyPatch(typeof(CharacterUI), "SetDetailsAlpha")]
    //class LoadCustomCharacterArtPatch10
    //{
    //    static bool Prefix()
    //    {
    //        Debug.Log("SetDetailsAlpha");
    //        return false;
    //    }
    //}

    //[HarmonyPatch(typeof(CharacterUI), "UpdateBasicAbility")]
    //class LoadCustomCharacterArtPatch11
    //{
    //    static void Prefix(CharacterState characterState, CharacterUI __instance, PrimaryAbilityDisplay ___primaryAbilityDisplay)
    //    {
    //        Debug.Log("UpdateBasicAbility");
    //        Debug.Log(__instance);
    //        Debug.Log(characterState);
    //        Debug.Log(___primaryAbilityDisplay);
    //    }
    //}

    //[HarmonyPatch(typeof(CharacterUI), "RefreshIcons")]
    //class LoadCustomCharacterArtPatch26
    //{
    //    static void Prefix()
    //    {
    //        Debug.Log("RefreshIcons");
    //    }
    //}

    //[HarmonyPatch(typeof(CharacterUI), "SetRimLights")]
    //class LoadCustomCharacterArtPatch27
    //{
    //    static void Prefix()
    //    {
    //        Debug.Log("RefreshIcons");
    //    }
    //}

    //[HarmonyPatch(typeof(CharacterUI), "AddAttachedParticleSystems")]
    //class LoadCustomCharacterArtPatch28
    //{
    //    static void Prefix()
    //    {
    //        Debug.Log("AddAttachedParticleSystems");
    //    }
    //}

    //[HarmonyPatch(typeof(CharacterUI), "Setup")]
    //class LoadCustomCharacterArtPatch29
    //{
    //    static void Prefix(CharacterState characterState, CharacterUI __instance)
    //    {
    //        Debug.Log("START SETUP");
    //        Debug.Log(characterState);
    //        Debug.Log(AccessTools.Field(typeof(CharacterUI), "spriteRenderer").GetValue(__instance));
    //    }
    //}

    [HarmonyPatch(typeof(CharacterData), "GetSpriteFromCharacterPrefab")]
    class LoadCustomCharacterArtPatch2
    {
        static void Prefix(ref GameObject characterPrefab, ref Sprite __result)
        {
            Debug.Log("GetSpriteFromCharacterPrefab");
            //__result = var assetRef = (AssetReferenceGameObject)AccessTools.Field(typeof(CharacterData), "characterPrefabVariantRef").GetValue(characterData);
            //string assetPath = (string)AccessTools.Field(typeof(AssetReferenceGameObject), "m_AssetGUID").GetValue(assetRef);
            string cardPath = "BepInEx/plugins/" + "netstandard2.0/blueeyes_character.png";
            //Debug.Log(cardPath);
            byte[] fileData = File.ReadAllBytes(cardPath);
            //Debug.Log(fileData.Length);

            Texture2D tex = new Texture2D(1, 1);
            UnityEngine.ImageConversion.LoadImage(tex, fileData);

            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 128f);
            __result = sprite;
        }
    }

    //[HarmonyPatch(typeof(CharacterData), "GetCharacterPrefabVariant")]
    //class LoadCustomCharacterArtPatch
    //{
    //    static void Postfix(ref CharacterData __instance, ref GameObject __result)
    //    {
    //        if (__result == null && CustomCardManager.CustomCharacterData.ContainsKey(__instance.GetID()))
    //        {
    //            Debug.Log("CUSTOM CHARACTER: " + __instance.GetName());
    //            CharacterData characterData = CustomCardManager.CustomCharacterData[__instance.GetID()];
    //            var assetRef = (AssetReferenceGameObject)AccessTools.Field(typeof(CharacterData), "characterPrefabVariantRef").GetValue(characterData);
    //            string assetPath = (string)AccessTools.Field(typeof(AssetReferenceGameObject), "m_AssetGUID").GetValue(assetRef);
    //            string cardPath = "BepInEx/plugins/" + assetPath;
    //            byte[] fileData = File.ReadAllBytes(cardPath);

    //            Texture2D tex = new Texture2D(1, 1);
    //            UnityEngine.ImageConversion.LoadImage(tex, fileData);

    //            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 128f);
    //            Debug.Log("1");
    //            GameObject characterStateGO = new GameObject(); //Create the GameObject
    //            GameObject characterScaleGO = new GameObject();
    //            characterScaleGO.transform.parent = characterStateGO.transform;
    //            GameObject characterUIGO = new GameObject();
    //            characterUIGO.transform.parent = characterScaleGO.transform;
    //            GameObject characterUIMeshSpineGO = new GameObject();
    //            characterUIMeshSpineGO.transform.parent = characterUIGO.transform;
    //            var comp1 = characterStateGO.AddComponent<CharacterState>();
    //            var comp2 = characterUIGO.AddComponent<CharacterUI>();
    //            var comp3 = characterUIGO.AddComponent<SpriteRenderer>();
    //            var comp4 = characterUIMeshSpineGO.AddComponent<CharacterUIMesh>();
    //            var comp5 = characterUIMeshSpineGO.AddComponent<MeshFilter>();
    //            Debug.Log("2");
    //            var vfxSideBackAnchorGO = new GameObject();
    //            var vfxSideBackBottomAnchorGO = new GameObject();
    //            var vfxSideFwdAnchorGO = new GameObject();
    //            var vfxSideFwdBottomAnchorGO = new GameObject();
    //            var vfxSideTopAnchorGO = new GameObject();
    //            var vfxBottomAnchorGO = new GameObject();
    //            var vfxCenterAnchorGO = new GameObject();
    //            var scaleTransformGO = new GameObject();
    //            var primaryAbilityDisplayGO = new GameObject();
    //            var comp6 = primaryAbilityDisplayGO.AddComponent<PrimaryAbilityDisplay>();
    //            var healthBarUIGO = new GameObject();
    //            var comp8 = healthBarUIGO.AddComponent<HealthBarUI>();
    //            //var statusEffectIconContainerGO = new GameObject();
    //            //var comp12 = statusEffectIconContainerGO.AddComponent<LayoutGroup>();
    //            //var triggerIconContainerGO = new GameObject();
    //            //var comp13 = triggerIconContainerGO.AddComponent<LayoutGroup>();
    //            var cheatIdTextGO = new GameObject();
    //            var comp14 = cheatIdTextGO.AddComponent<TextMeshProUGUI>();
    //            var animatorGO = new GameObject();
    //            var comp15 = animatorGO.AddComponent<Animator>();
    //            AccessTools.Field(typeof(CharacterUI), "vfxSideBackAnchor").SetValue(comp2, vfxSideBackAnchorGO.transform);
    //            AccessTools.Field(typeof(CharacterUI), "vfxSideBackBottomAnchor").SetValue(comp2, vfxSideBackBottomAnchorGO.transform);
    //            AccessTools.Field(typeof(CharacterUI), "vfxSideFwdAnchor").SetValue(comp2, vfxSideFwdAnchorGO.transform);
    //            AccessTools.Field(typeof(CharacterUI), "vfxSideFwdBottomAnchor").SetValue(comp2, vfxSideFwdBottomAnchorGO.transform);
    //            AccessTools.Field(typeof(CharacterUI), "vfxTopAnchor").SetValue(comp2, vfxSideTopAnchorGO.transform);
    //            AccessTools.Field(typeof(CharacterUI), "vfxBottomAnchor").SetValue(comp2, vfxBottomAnchorGO.transform);
    //            AccessTools.Field(typeof(CharacterUI), "vfxCenterAnchor").SetValue(comp2, vfxCenterAnchorGO.transform);
    //            AccessTools.Field(typeof(CharacterUI), "primaryAbilityDisplay").SetValue(comp2, comp6);
    //            AccessTools.Field(typeof(CharacterUI), "healthBarUI").SetValue(comp2, comp8);
    //            AccessTools.Field(typeof(CharacterUI), "scaleTransform").SetValue(comp2, scaleTransformGO.transform);
    //            //AccessTools.Field(typeof(CharacterUI), "statusEffectIconContainer").SetValue(comp2, comp12);
    //            //AccessTools.Field(typeof(CharacterUI), "triggerIconContainer").SetValue(comp2, comp13);
    //            AccessTools.Field(typeof(CharacterUI), "cheatIdText").SetValue(comp2, comp14);
    //            AccessTools.Field(typeof(CharacterUI), "animator").SetValue(comp2, comp15);

    //            var attackIconGO = new GameObject();
    //            var healAbilityIconGO = new GameObject();
    //            var noAttackIconGO = new GameObject();
    //            var abilityLabelGO = new GameObject();
    //            var comp7 = abilityLabelGO.AddComponent<TextMeshProUGUI>();
    //            AccessTools.Field(typeof(PrimaryAbilityDisplay), "attackIcon").SetValue(comp6, attackIconGO);
    //            AccessTools.Field(typeof(PrimaryAbilityDisplay), "healAbilityIcon").SetValue(comp6, healAbilityIconGO);
    //            AccessTools.Field(typeof(PrimaryAbilityDisplay), "noAttackIcon").SetValue(comp6, noAttackIconGO);
    //            AccessTools.Field(typeof(PrimaryAbilityDisplay), "abilityLabel").SetValue(comp6, comp7);
    //            var armorContainerGO = new GameObject();
    //            var currentHealthTextGO = new GameObject();
    //            var comp9 = currentHealthTextGO.AddComponent<TextMeshProUGUI>();
    //            var armorTextGO = new GameObject();
    //            var comp10 = armorTextGO.AddComponent<TextMeshProUGUI>();
    //            AccessTools.Field(typeof(HealthBarUI), "currentHealthText").SetValue(comp8, comp9);
    //            AccessTools.Field(typeof(HealthBarUI), "armorContainer").SetValue(comp8, armorContainerGO);
    //            AccessTools.Field(typeof(HealthBarUI), "armorText").SetValue(comp8, comp10);

    //            //AccessTools.Field(typeof(CharacterUI), "detailsRootCanvasGroup").SetValue(comp2, new );
    //            Debug.Log("3");
    //            //GameObject idleMeshGO = new GameObject();
    //            //idleMeshGO.transform.parent = characterUIMeshSpineGO.transform;
    //            //idleMeshGO.AddComponent<SkeletonAnimation>();
    //            //idleMeshGO.AddComponent<MeshRenderer>();
    //            //idleMeshGO.AddComponent<MeshFilter>();

    //            comp3.sprite = sprite;
    //            AccessTools.Field(typeof(CharacterState), "charUI").SetValue(comp1, comp2);
    //            AccessTools.Field(typeof(CharacterState), "sprite").SetValue(comp1, sprite);
    //            AccessTools.Field(typeof(CharacterUI), "spriteRenderer").SetValue(comp2, comp3);
    //            AccessTools.Field(typeof(CharacterUI), "statsResizer").SetValue(comp2, characterUIGO.AddComponent<TransformResizer>());
    //            //AccessTools.Field(typeof(CharacterUIMeshSpine), "_matProps").SetValue(comp4, new MaterialPropertyBlock());
    //            Debug.Log("4");
    //            comp1.gameObject.name = "BLUE EYES STATE";
    //            comp2.gameObject.name = "BLUE EYES UI";
    //            comp4.gameObject.name = "BLUE EYES SPINE";
    //            //var comp5 = comp3.gameObject.AddComponent<SkeletonAnimation>();
    //            //var comp6 = comp3.gameObject.AddComponent<BoneFollower>();

    //            //Debug.Log(NewObj.GetComponentInChildren<CharacterUI>());
    //            //Debug.Log(comp2.GetSpriteRenderer());
    //            //Debug.Log(CharacterData.GetSpriteFromCharacterPrefab(NewObj));
    //            //Debug.Log(comp2.GetComponentInChildren<CharacterUIMeshBase>(false));
    //            Debug.Log("REACHED THE END");


    //            __result = characterStateGO;
    //        }
    //    }
    //}
}
