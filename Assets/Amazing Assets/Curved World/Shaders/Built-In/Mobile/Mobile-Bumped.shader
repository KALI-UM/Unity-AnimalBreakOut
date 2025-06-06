// Curved World <http://u3d.as/1W8h>
// Copyright (c) Amazing Assets <https://amazingassets.world>
 


// Simplified Bumped shader. Differences from regular Bumped one:
// - no Main Color
// - Normalmap uses Tiling/Offset of the Base texture
// - fully supports only 1 directional light. Other lights can affect it, but it will be per-vertex/SH.

Shader "Amazing Assets/Curved World/Mobile/Bumped Diffuse" 
{
    Properties 
    {
        [HideInInspector][CurvedWorldBendSettings]	  _CurvedWorldBendSettings("0|1|1", Vector) = (0, 0, 0, 0)

        _MainTex ("Base (RGB)", 2D) = "white" {}
        [NoScaleOffset] _BumpMap ("Normalmap", 2D) = "bump" {}
    }

    SubShader 
    {
        Tags { "RenderType"="CurvedWorld_Opaque" }
        LOD 250

        CGPROGRAM
        #pragma surface surf Lambert noforwardadd vertex:vert addshadow


#define CURVEDWORLD_BEND_TYPE_CLASSICRUNNER_X_POSITIVE
#define CURVEDWORLD_BEND_ID_1
#pragma shader_feature_local CURVEDWORLD_DISABLED_ON
#pragma shader_feature_local CURVEDWORLD_NORMAL_TRANSFORMATION_ON
#include "../../Core/CurvedWorldTransform.cginc" 


        sampler2D _MainTex;
        sampler2D _BumpMap;

        void vert (inout appdata_full v) 
        {
            #if defined(CURVEDWORLD_IS_INSTALLED) && !defined(CURVEDWORLD_DISABLED_ON)
                #ifdef CURVEDWORLD_NORMAL_TRANSFORMATION_ON
                    CURVEDWORLD_TRANSFORM_VERTEX_AND_NORMAL(v.vertex, v.normal, v.tangent)
                #else
                    CURVEDWORLD_TRANSFORM_VERTEX(v.vertex)
                #endif
            #endif
        }

        struct Input 
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o) 
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
            o.Alpha = c.a;
            o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
        }
        ENDCG
    }

    FallBack "Hidden/Amazing Assets/Curved World/Fallback/VertexLit"

    CustomEditor "AmazingAssets.CurvedWorld.Editor.DefaultShaderGUI"
}
