// Curved World <http://u3d.as/1W8h>
// Copyright (c) Amazing Assets <https://amazingassets.world>
 
using System;

using UnityEngine;
using UnityEditor;


namespace AmazingAssets.CurvedWorld.Editor
{
    [CanEditMultipleObjects]
    public class CurvedWorldSeparatorDrawer : MaterialPropertyDrawer
    {
        public override void OnGUI(Rect position, MaterialProperty prop, String label, MaterialEditor editor)
        {

        }

        public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
        {
            float fValue;
            if (float.TryParse(label, out fValue))
                return fValue;
            else
                return 0;
        }
    }
}
