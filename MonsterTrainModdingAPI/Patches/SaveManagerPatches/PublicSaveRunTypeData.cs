using System;
using System.Collections.Generic;
using System.Text;

namespace MonsterTrainModdingAPI.Patches
{
    /// <summary>
    /// A public copy of SaveRunTypeData in the SaveManager class
    /// </summary>
    public class PublicSaveRunTypeData
    {
        public PublicSaveRunTypeData(string filename)
        {
            this.filename = filename;
        }

        // Token: 0x04004472 RID: 17522
        public SaveData saveData;

        // Token: 0x04004473 RID: 17523
        public string filename;
    }
}
