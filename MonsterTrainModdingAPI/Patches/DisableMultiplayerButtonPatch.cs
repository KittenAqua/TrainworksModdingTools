using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using ShinyShoe;

namespace MonsterTrainModdingAPI.Patches
{

    // Credit to Rawsome, Stable Infery for this one, too: a quick and dirty patch to disable the multiplayer button.
    [HarmonyPatch(typeof(MainMenuScreen), "CollectMenuButtons")]
    class DisableMultiplayerButtonPatch
    {
        static void Postfix(ref GameUISelectableButton ___multiplayerButton, ref List<GameUISelectableButton> ___menuButtons)
        {
            ___menuButtons.Remove(___multiplayerButton);
            ___multiplayerButton.enabled = false;
        }
    }
}
