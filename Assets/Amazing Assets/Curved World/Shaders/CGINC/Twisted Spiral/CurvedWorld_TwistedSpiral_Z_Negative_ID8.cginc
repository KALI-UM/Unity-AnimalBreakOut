// Curved World <http://u3d.as/1W8h>
// Copyright (c) Amazing Assets <https://amazingassets.world>
 
#ifndef CURVEDWORLD_TWISTEDSPIRAL_Z_NEGATIVE_ID8_CGINC
#define CURVEDWORLD_TWISTEDSPIRAL_Z_NEGATIVE_ID8_CGINC

uniform float3 CurvedWorld_TwistedSpiral_Z_Negative_ID8_PivotPoint;
uniform float3 CurvedWorld_TwistedSpiral_Z_Negative_ID8_RotationAxis;
uniform float3 CurvedWorld_TwistedSpiral_Z_Negative_ID8_BendSize;    
uniform float3 CurvedWorld_TwistedSpiral_Z_Negative_ID8_BendOffset;
  
                 
#include "../../Core/Core.cginc"                           
             
      
////////////////////////////////////////////////////////////////////////////////
//                                                                            //
//                                Main Method                                 //
//                                                                            //
////////////////////////////////////////////////////////////////////////////////
void CurvedWorld_TwistedSpiral_Z_Negative_ID8(inout float4 vertexOS)
{
    CurvedWorld_TwistedSpiral_Z_Negative(vertexOS, 
	                        CurvedWorld_TwistedSpiral_Z_Negative_ID8_PivotPoint,
                            CurvedWorld_TwistedSpiral_Z_Negative_ID8_RotationAxis,
							CurvedWorld_TwistedSpiral_Z_Negative_ID8_BendSize,
							CurvedWorld_TwistedSpiral_Z_Negative_ID8_BendOffset);
}

void CurvedWorld_TwistedSpiral_Z_Negative_ID8(inout float4 vertexOS, inout float3 normalOS, float4 tangent)
{
    CurvedWorld_TwistedSpiral_Z_Negative(vertexOS, 
                            normalOS, 
                            tangent,
                            CurvedWorld_TwistedSpiral_Z_Negative_ID8_PivotPoint,
                            CurvedWorld_TwistedSpiral_Z_Negative_ID8_RotationAxis,
							CurvedWorld_TwistedSpiral_Z_Negative_ID8_BendSize,
							CurvedWorld_TwistedSpiral_Z_Negative_ID8_BendOffset);
}    

void CurvedWorld_TwistedSpiral_Z_Negative_ID8(inout float3 vertexOS)
{
    float4 vertex = float4(vertexOS, 1);
    CurvedWorld_TwistedSpiral_Z_Negative_ID8(vertex);

    vertexOS.xyz = vertex.xyz;
}

void CurvedWorld_TwistedSpiral_Z_Negative_ID8(inout float3 vertexOS, inout float3 normalOS, float4 tangent)
{
    float4 vertex = float4(vertexOS, 1);
    CurvedWorld_TwistedSpiral_Z_Negative_ID8(vertex, normalOS, tangent);

    vertexOS.xyz = vertex.xyz;
}  
                  
////////////////////////////////////////////////////////////////////////////////
//                                                                            //
//                               SubGraph Methods                             //
//                                                                            // 
////////////////////////////////////////////////////////////////////////////////
void CurvedWorld_TwistedSpiral_Z_Negative_ID8_float(float3 vertexOS, out float3 retVertex)
{
    CurvedWorld_TwistedSpiral_Z_Negative_ID8(vertexOS); 	

    retVertex = vertexOS.xyz;
}

void CurvedWorld_TwistedSpiral_Z_Negative_ID8_half(half3 vertexOS, out half3 retVertex)
{
    CurvedWorld_TwistedSpiral_Z_Negative_ID8(vertexOS); 	

    retVertex = vertexOS.xyz;
}

void CurvedWorld_TwistedSpiral_Z_Negative_ID8_float(float3 vertexOS, float3 normalOS, float4 tangent, out float3 retVertex, out float3 retNormal)
{
	CurvedWorld_TwistedSpiral_Z_Negative_ID8(vertexOS, normalOS, tangent); 	

    retVertex = vertexOS.xyz;
    retNormal = normalOS.xyz;
}

void CurvedWorld_TwistedSpiral_Z_Negative_ID8_half(half3 vertexOS, half3 normalOS, half4 tangent, out half3 retVertex, out float3 retNormal)
{
	CurvedWorld_TwistedSpiral_Z_Negative_ID8(vertexOS, normalOS, tangent); 	

    retVertex = vertexOS.xyz;
    retNormal = normalOS.xyz;	
}     

#endif
