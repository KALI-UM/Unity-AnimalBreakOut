// Curved World <http://u3d.as/1W8h>
// Copyright (c) Amazing Assets <https://amazingassets.world>
 


// Simplified Additive Particle shader. Differences from regular Additive Particle one:
// - no Tint color
// - no Smooth particle support
// - no AlphaTest
// - no ColorMask


Shader "Amazing Assets/Curved World/Mobile/Particles/Additive" 
{
    Properties 
    {
        [HideInInspector][CurvedWorldBendSettings]	  _CurvedWorldBendSettings("0|1", Vector) = (0, 0, 0, 0)

        _MainTex ("Particle Texture", 2D) = "white" { }
    }

    SubShader 
    { 
        Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" "PreviewType"="Plane" }
        Pass 
        {
        	Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" "PreviewType"="Plane" }
            ZWrite Off
            Cull Off
            Blend SrcAlpha One
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0
            #include "UnityCG.cginc"
            #pragma multi_compile_fog
            #define USING_FOG (defined(FOG_LINEAR) || defined(FOG_EXP) || defined(FOG_EXP2))


#define CURVEDWORLD_BEND_TYPE_CLASSICRUNNER_X_POSITIVE
#define CURVEDWORLD_BEND_ID_1
#pragma shader_feature_local CURVEDWORLD_DISABLED_ON
#include "../../Core/CurvedWorldTransform.cginc" 


            // uniforms
            float4 _MainTex_ST;

            // vertex shader input data
struct appdata {
            float3 pos : POSITION;
            half4 color : COLOR;
            float3 uv0 : TEXCOORD0;
            UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            // vertex-to-fragment interpolators
struct v2f {
            fixed4 color : COLOR0;
            float2 uv0 : TEXCOORD0;
            #if USING_FOG
                fixed fog : TEXCOORD1;
            #endif
            float4 pos : SV_POSITION;
            UNITY_VERTEX_OUTPUT_STEREO
            };

            // vertex shader
            v2f vert (appdata IN) 
            {
            v2f o;
            UNITY_SETUP_INSTANCE_ID(IN);
            UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);


            #if defined(CURVEDWORLD_IS_INSTALLED) && !defined(CURVEDWORLD_DISABLED_ON)
                CURVEDWORLD_TRANSFORM_VERTEX(IN.pos)
            #endif


            half4 color = IN.color;
            float3 eyePos = UnityObjectToViewPos(IN.pos.xyz);// mul (UNITY_MATRIX_MV, IN.pos).xyz;
            half3 viewDir = 0.0;
            o.color = saturate(color);
            // compute texture coordinates
            o.uv0 = IN.uv0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
            // fog
            #if USING_FOG
                float fogCoord = length(eyePos.xyz); // radial fog distance
                UNITY_CALC_FOG_FACTOR_RAW(fogCoord);
                o.fog = saturate(unityFogFactor);
            #endif
            // transform position
            o.pos = UnityObjectToClipPos(IN.pos.xyz);
            return o;
            }

            // textures
            sampler2D _MainTex;

            // fragment shader
            fixed4 frag (v2f IN) : SV_Target 
            {
            fixed4 col;
            fixed4 tex, tmp0, tmp1, tmp2;
            // SetTexture #0
            tex = tex2D (_MainTex, IN.uv0.xy);
            col = tex * IN.color;
            // fog
            #if USING_FOG
                col.rgb = lerp (unity_FogColor.rgb, col.rgb, IN.fog);
            #endif
            return col;
            }

            // texenvs
            //! TexEnv0: 01010103 01010103 [_MainTex]
            ENDCG
        }        
    }


    CustomEditor "AmazingAssets.CurvedWorld.Editor.DefaultShaderGUI"
}
