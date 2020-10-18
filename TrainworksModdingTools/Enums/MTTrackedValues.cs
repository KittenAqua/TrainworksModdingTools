using System;
using System.Collections.Generic;
using System.Text;

namespace TrainworksModdingTools.Enums
{
    /// <summary>
    /// An Extended Enum
    /// </summary>
    public class TrackedValueType : ExtendedEnum<TrackedValueType, CardStatistics.TrackedValueType>
    {
        private static int NumTrackedValues = 576;

        public TrackedValueType(string Name, int? ID = null) : base(Name, ID ?? GetNewCharacterGUID())
        {

        }

        public static int GetNewCharacterGUID()
        {
            NumTrackedValues++;
            return NumTrackedValues;
        }
    }
}
