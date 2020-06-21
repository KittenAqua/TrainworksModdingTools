using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using ShinyShoe;

namespace MonsterTrainModdingAPI.Patches
{
    // Credit to Rawsome, Stable Infery for this one, too: a quick and dirty patch to disable the multiplayer button.
    /// <summary>
    /// Removes the ability to interact with the multiplayer button.
    /// It will still display on the main menu, but it cannot be clicked.
    /// </summary>
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
