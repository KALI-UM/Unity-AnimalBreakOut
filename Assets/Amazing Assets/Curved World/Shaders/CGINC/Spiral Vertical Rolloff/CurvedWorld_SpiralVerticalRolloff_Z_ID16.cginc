// Curved World <http://u3d.as/1W8h>
// Copyright (c) Amazing Assets <https://amazingassets.world>
 
#ifndef CURVEDWORLD_SPIRALVERTICALROLLOFF_Z_ID16_CGINC
#define CURVEDWORLD_SPIRALVERTICALROLLOFF_Z_ID16_CGINC

uniform float3 CurvedWorld_SpiralVerticalRolloff_Z_ID16_PivotPoint;
uniform float3 CurvedWorld_SpiralVerticalRolloff_Z_ID16_RotationCenter;
uniform float CurvedWorld_SpiralVerticalRolloff_Z_ID16_BendAngle;
uniform float CurvedWorld_SpiralVerticalRolloff_Z_ID16_BendMinimumRadius;
uniform float CurvedWorld_SpiralVerticalRolloff_Z_ID16_BendRolloff;

                 
#include "../../Core/Core.cginc"                           
             
      
////////////////////////////////////////////////////////////////////////////////
//                                                                            //
//                                Main Method                                 //
//                                                                            //
////////////////////////////////////////////////////////////////////////////////
void CurvedWorld_SpiralVerticalRolloff_Z_ID16(inout float4 vertexOS)
{
    CurvedWorld_SpiralVerticalRolloff_Z(vertexOS, 
							CurvedWorld_SpiralVerticalRolloff_Z_ID16_PivotPoint,
	                        CurvedWorld_SpiralVerticalRolloff_Z_ID16_RotationCenter,                            
							CurvedWorld_SpiralVerticalRolloff_Z_ID16_BendAngle,
							CurvedWorld_SpiralVerticalRolloff_Z_ID16_BendMinimumRadius,
							CurvedWorld_SpiralVerticalRolloff_Z_ID16_BendRolloff);
}

void CurvedWorld_SpiralVerticalRolloff_Z_ID16(inout float4 vertexOS, inout float3 normalOS, float4 tangent)
{
    CurvedWorld_SpiralVerticalRolloff_Z(vertexOS, 
                            normalOS, 
                            tangent,
							CurvedWorld_SpiralVerticalRolloff_Z_ID16_PivotPoint,
                            CurvedWorld_SpiralVerticalRolloff_Z_ID16_RotationCenter,                            
							CurvedWorld_SpiralVerticalRolloff_Z_ID16_BendAngle,
							CurvedWorld_SpiralVerticalRolloff_Z_ID16_BendMinimumRadius,
							CurvedWorld_SpiralVerticalRolloff_Z_ID16_BendRolloff);
}

void CurvedWorld_SpiralVerticalRolloff_Z_ID16(inout float3 vertexOS)
{
    float4 vertex = float4(vertexOS, 1);
    CurvedWorld_SpiralVerticalRolloff_Z_ID16(vertex);

    vertexOS.xyz = vertex.xyz;
}

void CurvedWorld_SpiralVerticalRolloff_Z_ID16(inout float3 vertexOS, inout float3 normalOS, float4 tangent)
{
    float4 vertex = float4(vertexOS, 1);
    CurvedWorld_SpiralVerticalRolloff_Z_ID16(vertex, normalOS, tangent);

    vertexOS.xyz = vertex.xyz;
} 

////////////////////////////////////////////////////////////////////////////////
//                                                                            //
//                               SubGraph Methods                             //
//                                                                            // 
////////////////////////////////////////////////////////////////////////////////
void CurvedWorld_SpiralVerticalRolloff_Z_ID16_float(float3 vertexOS, out float3 retVertex)
{
    CurvedWorld_SpiralVerticalRolloff_Z_ID16(vertexOS); 	

    retVertex = vertexOS.xyz;
}

void CurvedWorld_SpiralVerticalRolloff_Z_ID16_half(half3 vertexOS, out half3 retVertex)
{
    CurvedWorld_SpiralVerticalRolloff_Z_ID16(vertexOS); 	

    retVertex = vertexOS.xyz;
}

void CurvedWorld_SpiralVerticalRolloff_Z_ID16_float(float3 vertexOS, float3 normalOS, float4 tangent, out float3 retVertex, out float3 retNormal)
{
	CurvedWorld_SpiralVerticalRolloff_Z_ID16(vertexOS, normalOS, tangent); 	

    retVertex = vertexOS.xyz;
    retNormal = normalOS.xyz;
}

void CurvedWorld_SpiralVerticalRolloff_Z_ID16_half(half3 vertexOS, half3 normalOS, half4 tangent, out half3 retVertex, out float3 retNormal)
{
	CurvedWorld_SpiralVerticalRolloff_Z_ID16(vertexOS, normalOS, tangent); 	

    retVertex = vertexOS.xyz;
    retNormal = normalOS.xyz;	
}     

#endif
