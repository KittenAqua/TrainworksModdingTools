using System;
using System.Collections.Generic;
using System.Text;

namespace MonsterTrainModdingAPI.Patches
{
    /// <summary>
    /// A Class that has additional functionality desired for each saveManager Instance
    /// </summary>
    public class SaveManagerExtension
    {
        public static PublicSaveRunTypeData moddedPlayerSave = new PublicSaveRunTypeData("save-Modded");
    }
}
