// Curved World <http://u3d.as/1W8h>
// Copyright (c) Amazing Assets <https://amazingassets.world>
 
#ifndef CURVEDWORLD_SPIRALVERTICALROLLOFF_X_ID11_CGINC
#define CURVEDWORLD_SPIRALVERTICALROLLOFF_X_ID11_CGINC

uniform float3 CurvedWorld_SpiralVerticalRolloff_X_ID11_PivotPoint;
uniform float3 CurvedWorld_SpiralVerticalRolloff_X_ID11_RotationCenter;
uniform float CurvedWorld_SpiralVerticalRolloff_X_ID11_BendAngle;
uniform float CurvedWorld_SpiralVerticalRolloff_X_ID11_BendMinimumRadius;
uniform float CurvedWorld_SpiralVerticalRolloff_X_ID11_BendRolloff;

                 
#include "../../Core/Core.cginc"                           
             
      
////////////////////////////////////////////////////////////////////////////////
//                                                                            //
//                                Main Method                                 //
//                                                                            //
////////////////////////////////////////////////////////////////////////////////
void CurvedWorld_SpiralVerticalRolloff_X_ID11(inout float4 vertexOS)
{
    CurvedWorld_SpiralVerticalRolloff_X(vertexOS, 
							CurvedWorld_SpiralVerticalRolloff_X_ID11_PivotPoint,
	                        CurvedWorld_SpiralVerticalRolloff_X_ID11_RotationCenter,                            
							CurvedWorld_SpiralVerticalRolloff_X_ID11_BendAngle,
							CurvedWorld_SpiralVerticalRolloff_X_ID11_BendMinimumRadius,
							CurvedWorld_SpiralVerticalRolloff_X_ID11_BendRolloff);
}

void CurvedWorld_SpiralVerticalRolloff_X_ID11(inout float4 vertexOS, inout float3 normalOS, float4 tangent)
{
    CurvedWorld_SpiralVerticalRolloff_X(vertexOS, 
                            normalOS, 
                            tangent,
							CurvedWorld_SpiralVerticalRolloff_X_ID11_PivotPoint,
                            CurvedWorld_SpiralVerticalRolloff_X_ID11_RotationCenter,                            
							CurvedWorld_SpiralVerticalRolloff_X_ID11_BendAngle,
							CurvedWorld_SpiralVerticalRolloff_X_ID11_BendMinimumRadius,
							CurvedWorld_SpiralVerticalRolloff_X_ID11_BendRolloff);
}

void CurvedWorld_SpiralVerticalRolloff_X_ID11(inout float3 vertexOS)
{
    float4 vertex = float4(vertexOS, 1);
    CurvedWorld_SpiralVerticalRolloff_X_ID11(vertex);

    vertexOS.xyz = vertex.xyz;
}

void CurvedWorld_SpiralVerticalRolloff_X_ID11(inout float3 vertexOS, inout float3 normalOS, float4 tangent)
{
    float4 vertex = float4(vertexOS, 1);
    CurvedWorld_SpiralVerticalRolloff_X_ID11(vertex, normalOS, tangent);

    vertexOS.xyz = vertex.xyz;
} 

////////////////////////////////////////////////////////////////////////////////
//                                                                            //
//                               SubGraph Methods                             //
//                                                                            // 
////////////////////////////////////////////////////////////////////////////////
void CurvedWorld_SpiralVerticalRolloff_X_ID11_float(float3 vertexOS, out float3 retVertex)
{
    CurvedWorld_SpiralVerticalRolloff_X_ID11(vertexOS); 	

    retVertex = vertexOS.xyz;
}

void CurvedWorld_SpiralVerticalRolloff_X_ID11_half(half3 vertexOS, out half3 retVertex)
{
    CurvedWorld_SpiralVerticalRolloff_X_ID11(vertexOS); 	

    retVertex = vertexOS.xyz;
}

void CurvedWorld_SpiralVerticalRolloff_X_ID11_float(float3 vertexOS, float3 normalOS, float4 tangent, out float3 retVertex, out float3 retNormal)
{
	CurvedWorld_SpiralVerticalRolloff_X_ID11(vertexOS, normalOS, tangent); 	

    retVertex = vertexOS.xyz;
    retNormal = normalOS.xyz;
}

void CurvedWorld_SpiralVerticalRolloff_X_ID11_half(half3 vertexOS, half3 normalOS, half4 tangent, out half3 retVertex, out float3 retNormal)
{
	CurvedWorld_SpiralVerticalRolloff_X_ID11(vertexOS, normalOS, tangent); 	

    retVertex = vertexOS.xyz;
    retNormal = normalOS.xyz;	
}     

#endif
