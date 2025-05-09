// Curved World <http://u3d.as/1W8h>
// Copyright (c) Amazing Assets <https://amazingassets.world>
 


Shader "Amazing Assets/Curved World/Particles/Standard Unlit"
{
    Properties
    {
[HideInInspector][CurvedWorldBendSettings]	  _CurvedWorldBendSettings("0,2,27|1|1", Vector) = (0, 0, 0, 0)



        _MainTex("Albedo", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)

        _Cutoff("Alpha Cutoff", Range(0.0, 1.0)) = 0.5

        _BumpScale("Scale", Float) = 1.0
        _BumpMap("Normal Map", 2D) = "bump" {}

        _EmissionColor("Color", Color) = (0,0,0)
        _EmissionMap("Emission", 2D) = "white" {}

        _DistortionStrength("Strength", Float) = 1.0
        _DistortionBlend("Blend", Range(0.0, 1.0)) = 0.5

        _SoftParticlesNearFadeDistance("Soft Particles Near Fade", Float) = 0.0
        _SoftParticlesFarFadeDistance("Soft Particles Far Fade", Float) = 1.0
        _CameraNearFadeDistance("Camera Near Fade", Float) = 1.0
        _CameraFarFadeDistance("Camera Far Fade", Float) = 2.0

        // Hidden properties
        [HideInInspector] _Mode ("__mode", Float) = 0.0
        [HideInInspector] _ColorMode ("__colormode", Float) = 0.0
        [HideInInspector] _FlipbookMode ("__flipbookmode", Float) = 0.0
        [HideInInspector] _LightingEnabled ("__lightingenabled", Float) = 0.0
        [HideInInspector] _DistortionEnabled ("__distortionenabled", Float) = 0.0
        [HideInInspector] _EmissionEnabled ("__emissionenabled", Float) = 0.0
        [HideInInspector] _BlendOp ("__blendop", Float) = 0.0
        [HideInInspector] _SrcBlend ("__src", Float) = 1.0
        [HideInInspector] _DstBlend ("__dst", Float) = 0.0
        [HideInInspector] _ZWrite ("__zw", Float) = 1.0
        [HideInInspector] _Cull ("__cull", Float) = 2.0
        [HideInInspector] _SoftParticlesEnabled ("__softparticlesenabled", Float) = 0.0
        [HideInInspector] _CameraFadingEnabled ("__camerafadingenabled", Float) = 0.0
        [HideInInspector] _SoftParticleFadeParams ("__softparticlefadeparams", Vector) = (0,0,0,0)
        [HideInInspector] _CameraFadeParams ("__camerafadeparams", Vector) = (0,0,0,0)
        [HideInInspector] _ColorAddSubDiff ("__coloraddsubdiff", Vector) = (0,0,0,0)
        [HideInInspector] _DistortionStrengthScaled ("__distortionstrengthscaled", Float) = 0.0
    }

    Category
    {
        SubShader
        {
            Tags { "RenderType"="Opaque" "IgnoreProjector"="True" "PreviewType"="Plane" "PerformanceChecks"="False" }

            BlendOp [_BlendOp]
            Blend [_SrcBlend] [_DstBlend]
            ZWrite [_ZWrite]
            Cull [_Cull]
            ColorMask RGB

            GrabPass
            {
                Tags { "LightMode" = "GrabPass" }
                "_GrabTexture"
            }

            Pass
            {
                Name "ShadowCaster"
                Tags { "LightMode" = "ShadowCaster" }

                BlendOp Add
                Blend One Zero
                ZWrite On
                Cull Off

                CGPROGRAM
                //vertInstancingSetup writes to global, not allowed with DXC
                #pragma never_use_dxc
                #pragma target 2.5

                #pragma shader_feature_local _ _ALPHATEST_ON _ALPHABLEND_ON _ALPHAPREMULTIPLY_ON _ALPHAMODULATE_ON
                #pragma shader_feature_local _REQUIRE_UV2
                #pragma multi_compile_shadowcaster
                #pragma multi_compile_instancing
                #pragma instancing_options procedural:vertInstancingSetup

                #pragma vertex vertParticleShadowCaster
                #pragma fragment fragParticleShadowCaster



#pragma shader_feature_local CURVEDWORLD_BEND_TYPE_CLASSICRUNNER_X_POSITIVE CURVEDWORLD_BEND_TYPE_CLASSICRUNNER_Z_POSITIVE CURVEDWORLD_BEND_TYPE_TWISTEDSPIRAL_X_POSITIVE
#define CURVEDWORLD_BEND_ID_1
#pragma shader_feature_local CURVEDWORLD_DISABLED_ON



                #include "UnityStandardParticleShadow.cginc"
                ENDCG
            }

            Pass
            {
                Name "SceneSelectionPass"
                Tags { "LightMode" = "SceneSelectionPass" }

                BlendOp Add
                Blend One Zero
                ZWrite On
                Cull Off

                CGPROGRAM
                //vertInstancingSetup writes to global, not allowed with DXC
                #pragma never_use_dxc
                #pragma target 2.5

                #pragma shader_feature_local_fragment _ALPHATEST_ON
                #pragma shader_feature_local _REQUIRE_UV2
                #pragma multi_compile_instancing
                #pragma instancing_options procedural:vertInstancingSetup

                #pragma vertex vertEditorPass
                #pragma fragment fragSceneHighlightPass


#pragma shader_feature_local CURVEDWORLD_BEND_TYPE_CLASSICRUNNER_X_POSITIVE CURVEDWORLD_BEND_TYPE_CLASSICRUNNER_Z_POSITIVE CURVEDWORLD_BEND_TYPE_TWISTEDSPIRAL_X_POSITIVE
#define CURVEDWORLD_BEND_ID_1
#pragma shader_feature_local CURVEDWORLD_DISABLED_ON


                #include "UnityStandardParticleEditor.cginc"
                ENDCG
            }
            
            Pass
            {
                CGPROGRAM
                //vertInstancingSetup writes to global, not allowed with DXC
                #pragma never_use_dxc
                #pragma multi_compile __ SOFTPARTICLES_ON
                #pragma multi_compile_fog
                #pragma target 2.5
                #pragma shader_feature_local_fragment _ _ALPHATEST_ON _ALPHABLEND_ON _ALPHAPREMULTIPLY_ON _ALPHAMODULATE_ON
                #pragma shader_feature_local_fragment _ _COLOROVERLAY_ON _COLORCOLOR_ON _COLORADDSUBDIFF_ON
                #pragma shader_feature_local _NORMALMAP
                #pragma shader_feature_fragment _EMISSION
                #pragma shader_feature_local _FADING_ON
                #pragma shader_feature_local _REQUIRE_UV2
                #pragma shader_feature_local EFFECT_BUMP

                #pragma vertex vertParticleUnlit
                #pragma fragment fragParticleUnlit
                #pragma multi_compile_instancing
                #pragma instancing_options procedural:vertInstancingSetup



#pragma shader_feature_local CURVEDWORLD_BEND_TYPE_CLASSICRUNNER_X_POSITIVE CURVEDWORLD_BEND_TYPE_CLASSICRUNNER_Z_POSITIVE CURVEDWORLD_BEND_TYPE_TWISTEDSPIRAL_X_POSITIVE
#define CURVEDWORLD_BEND_ID_1
#pragma shader_feature_local CURVEDWORLD_DISABLED_ON
#pragma shader_feature_local CURVEDWORLD_NORMAL_TRANSFORMATION_ON



                #include "UnityStandardParticles.cginc"
                ENDCG
            }
        }
    }

    Fallback "VertexLit"
    CustomEditor "AmazingAssets.CurvedWorld.Editor.StandardParticlesShaderGUI"
}
