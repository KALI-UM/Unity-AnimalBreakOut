// Curved World <http://u3d.as/1W8h>
// Copyright (c) Amazing Assets <https://amazingassets.world>
 
using UnityEditor;


namespace AmazingAssets.CurvedWorld.Editor
{
    internal class SpritesShaderGUI : ShaderGUI
    {
        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            MaterialProperties.InitCurvedWorldMaterialProperties(properties);
            MaterialProperties.DrawCurvedWorldMaterialProperties(materialEditor, MaterialProperties.Style.HelpBox, false, true);

            base.OnGUI(materialEditor, properties);
        }
    }
}
