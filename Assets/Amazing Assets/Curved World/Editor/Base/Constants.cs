// Curved World <http://u3d.as/1W8h>
// Copyright (c) Amazing Assets <https://amazingassets.world>


using System.Linq;

namespace AmazingAssets.CurvedWorld.Editor
{
    static public class Constants
    {
        static public readonly int MAX_SUPPORTED_BEND_IDS = 32;
        static public readonly int MAX_SUPPORTED_BEND_TYPES = System.Enum.GetValues(typeof(CurvedWorld.BendType)).Length;
        static public string[] bendTypesNamesForLabel = System.Enum.GetValues(typeof(CurvedWorld.BendType)).Cast<int>().Select(x => EditorUtilities.GetBendTypeNameInfo((CurvedWorld.BendType)x).forLable).ToArray();
        static public string[] bendTypesNamesForMenu = System.Enum.GetValues(typeof(CurvedWorld.BendType)).Cast<int>().Select(x => EditorUtilities.GetBendTypeNameInfo((CurvedWorld.BendType)x).forMenu).ToArray();

        static public string shaderProprty_BendSettings = "CurvedWorldBendSettings";
        static public string shaderProprtyName_BendSettings = "_" + shaderProprty_BendSettings;
        static public string shaderKeywordName_CurvedWorldDisabled = "CURVEDWORLD_DISABLED_ON";
        static public string shaderKeywordName_BendTransformNormal = "CURVEDWORLD_NORMAL_TRANSFORMATION_ON";
        static public string shaderKeywordPrefix_BendType = "CURVEDWORLD_BEND_TYPE_";
        static public string shaderKeywordPrefix_BendID = "CURVEDWORLD_BEND_ID_";
    }
}