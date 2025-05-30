// Curved World <http://u3d.as/1W8h>
// Copyright (c) Amazing Assets <https://amazingassets.world>
 
Shader "Amazing Assets/Curved World/Terrain/Initial"
{
    Properties
    {
       [HideInInspector][CurvedWorldBendSettingsTerrain] _CurvedWorldBendSettings("", vector) = (0, 0, 0, 0)
    }
    
	SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            float4 vert (float4 vertex : POSITION) : SV_POSITION
            {
                return UnityObjectToClipPos(vertex);
            }
			
            fixed4 frag () : SV_Target
            {
                return 0; 
            }
            ENDCG
        }
    }

	CustomEditor "AmazingAssets.CurvedWorld.Editor.TerrainShaderGUI"
}
