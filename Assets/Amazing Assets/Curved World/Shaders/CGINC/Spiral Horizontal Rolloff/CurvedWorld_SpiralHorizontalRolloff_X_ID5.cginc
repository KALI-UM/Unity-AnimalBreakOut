// Curved World <http://u3d.as/1W8h>
// Copyright (c) Amazing Assets <https://amazingassets.world>
 
#ifndef CURVEDWORLD_SPIRALHORIZONTALROLLOFF_X_ID5_CGINC
#define CURVEDWORLD_SPIRALHORIZONTALROLLOFF_X_ID5_CGINC

uniform float3 CurvedWorld_SpiralHorizontalRolloff_X_ID5_PivotPoint;
uniform float3 CurvedWorld_SpiralHorizontalRolloff_X_ID5_RotationCenter;
uniform float CurvedWorld_SpiralHorizontalRolloff_X_ID5_BendAngle;
uniform float CurvedWorld_SpiralHorizontalRolloff_X_ID5_BendMinimumRadius;
uniform float CurvedWorld_SpiralHorizontalRolloff_X_ID5_BendRolloff;

                 
#include "../../Core/Core.cginc"                           
             
      
////////////////////////////////////////////////////////////////////////////////
//                                                                            //
//                                Main Method                                 //
//                                                                            //
////////////////////////////////////////////////////////////////////////////////
void CurvedWorld_SpiralHorizontalRolloff_X_ID5(inout float4 vertexOS)
{
    CurvedWorld_SpiralHorizontalRolloff_X(vertexOS, 
							CurvedWorld_SpiralHorizontalRolloff_X_ID5_PivotPoint,
	                        CurvedWorld_SpiralHorizontalRolloff_X_ID5_RotationCenter,                            
							CurvedWorld_SpiralHorizontalRolloff_X_ID5_BendAngle,
							CurvedWorld_SpiralHorizontalRolloff_X_ID5_BendMinimumRadius,
							CurvedWorld_SpiralHorizontalRolloff_X_ID5_BendRolloff);
}

void CurvedWorld_SpiralHorizontalRolloff_X_ID5(inout float4 vertexOS, inout float3 normalOS, float4 tangent)
{
    CurvedWorld_SpiralHorizontalRolloff_X(vertexOS, 
                            normalOS, 
                            tangent,
							CurvedWorld_SpiralHorizontalRolloff_X_ID5_PivotPoint,
                            CurvedWorld_SpiralHorizontalRolloff_X_ID5_RotationCenter,                            
							CurvedWorld_SpiralHorizontalRolloff_X_ID5_BendAngle,
							CurvedWorld_SpiralHorizontalRolloff_X_ID5_BendMinimumRadius,
							CurvedWorld_SpiralHorizontalRolloff_X_ID5_BendRolloff);
}

void CurvedWorld_SpiralHorizontalRolloff_X_ID5(inout float3 vertexOS)
{
    float4 vertex = float4(vertexOS, 1);
    CurvedWorld_SpiralHorizontalRolloff_X_ID5(vertex);

    vertexOS.xyz = vertex.xyz;
}

void CurvedWorld_SpiralHorizontalRolloff_X_ID5(inout float3 vertexOS, inout float3 normalOS, float4 tangent)
{
    float4 vertex = float4(vertexOS, 1);
    CurvedWorld_SpiralHorizontalRolloff_X_ID5(vertex, normalOS, tangent);

    vertexOS.xyz = vertex.xyz;
} 

////////////////////////////////////////////////////////////////////////////////
//                                                                            //
//                               SubGraph Methods                             //
//                                                                            // 
////////////////////////////////////////////////////////////////////////////////
void CurvedWorld_SpiralHorizontalRolloff_X_ID5_float(float3 vertexOS, out float3 retVertex)
{
    CurvedWorld_SpiralHorizontalRolloff_X_ID5(vertexOS); 	

    retVertex = vertexOS.xyz;
}

void CurvedWorld_SpiralHorizontalRolloff_X_ID5_half(half3 vertexOS, out half3 retVertex)
{
    CurvedWorld_SpiralHorizontalRolloff_X_ID5(vertexOS); 	

    retVertex = vertexOS.xyz;
}

void CurvedWorld_SpiralHorizontalRolloff_X_ID5_float(float3 vertexOS, float3 normalOS, float4 tangent, out float3 retVertex, out float3 retNormal)
{
	CurvedWorld_SpiralHorizontalRolloff_X_ID5(vertexOS, normalOS, tangent); 	

    retVertex = vertexOS.xyz;
    retNormal = normalOS.xyz;
}

void CurvedWorld_SpiralHorizontalRolloff_X_ID5_half(half3 vertexOS, half3 normalOS, half4 tangent, out half3 retVertex, out float3 retNormal)
{
	CurvedWorld_SpiralHorizontalRolloff_X_ID5(vertexOS, normalOS, tangent); 	

    retVertex = vertexOS.xyz;
    retNormal = normalOS.xyz;	
}     

#endif
