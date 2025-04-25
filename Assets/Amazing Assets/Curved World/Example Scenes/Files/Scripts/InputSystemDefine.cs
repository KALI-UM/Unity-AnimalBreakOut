// Curved World <http://u3d.as/1W8h>
// Copyright (c) Amazing Assets <https://amazingassets.world>
 
#if UNITY_EDITOR
using System;

using UnityEditor;
using UnityEditor.Build;


namespace AmazingAssets.CurvedWorld.Examples
{
    [InitializeOnLoad]
    public class InputSystemDefine
    {
        static InputSystemDefine()
        {
            var defines = PlayerSettings.GetScriptingDefineSymbols(NamedBuildTarget.Standalone);
            if (Type.GetType("UnityEngine.InputSystem.InputAction, Unity.InputSystem") != null)
            {
                if (!defines.Contains("USE_INPUT_SYSTEM"))
                {
                    defines += ";USE_INPUT_SYSTEM";
                    PlayerSettings.SetScriptingDefineSymbols(NamedBuildTarget.Standalone, defines);
                }
            }
            else
            {
                if (defines.Contains("USE_INPUT_SYSTEM"))
                {
                    defines = defines.Replace("USE_INPUT_SYSTEM", "");
                    PlayerSettings.SetScriptingDefineSymbols(NamedBuildTarget.Standalone, defines);
                }
            }
        }
    }
}
#endif
