// Curved World <http://u3d.as/1W8h>
// Copyright (c) Amazing Assets <https://amazingassets.world>
 
using UnityEngine;


namespace AmazingAssets.CurvedWorld
{
    static public class CurvedWorldUtilities
    {
        static public Vector3 TransformPosition(Vector3 vertex, CurvedWorld.CurvedWorldController controller)
        {
            if (controller == null ||
               (controller.disableInEditor && Application.isEditor && Application.isPlaying == false))
            {
                return vertex;
            }


            switch (controller.bendType)
            {
                case BendType.ClassicRunner_X_Positive:
                case BendType.ClassicRunner_X_Negative:
                case BendType.ClassicRunner_Z_Positive:
                case BendType.ClassicRunner_Z_Negative:
                    {
                        return TransformPosition(vertex, controller.bendType, controller.bendPivotPointPosition, new Vector2(controller.bendVerticalSize, controller.bendHorizontalSize), new Vector2(controller.bendVerticalOffset, controller.bendHorizontalOffset));
                    }

                case BendType.TwistedSpiral_X_Positive:
                case BendType.TwistedSpiral_X_Negative:
                case BendType.TwistedSpiral_Z_Positive:
                case BendType.TwistedSpiral_Z_Negative:
                    {
                        return TransformPosition(vertex, controller.bendType, controller.bendPivotPointPosition, controller.bendRotationAxis, new Vector3(controller.bendCurvatureSize, controller.bendVerticalSize, controller.bendHorizontalSize), new Vector3(controller.bendCurvatureOffset, controller.bendVerticalOffset, controller.bendHorizontalOffset));
                    }

                case BendType.LittlePlanet_X:
                case BendType.LittlePlanet_Y:
                case BendType.LittlePlanet_Z:
                case BendType.CylindricalTower_X:
                case BendType.CylindricalTower_Z:
                case BendType.CylindricalRolloff_X:
                case BendType.CylindricalRolloff_Z:
                    {
                        return TransformPosition(vertex, controller.bendType, controller.bendPivotPointPosition, controller.bendCurvatureSize, controller.bendCurvatureOffset);
                    }

                case BendType.SpiralHorizontal_X_Positive:
                case BendType.SpiralHorizontal_X_Negative:
                case BendType.SpiralHorizontal_Z_Positive:
                case BendType.SpiralHorizontal_Z_Negative:
                case BendType.SpiralVertical_X_Positive:
                case BendType.SpiralVertical_X_Negative:
                case BendType.SpiralVertical_Z_Positive:
                case BendType.SpiralVertical_Z_Negative:
                    {
                        return TransformPosition(vertex, controller.bendType, controller.bendPivotPointPosition, controller.bendRotationCenterPosition, controller.bendAngle, controller.bendMinimumRadius);
                    }

                case BendType.SpiralHorizontalDouble_X:
                case BendType.SpiralHorizontalDouble_Z:
                case BendType.SpiralVerticalDouble_X:
                case BendType.SpiralVerticalDouble_Z:
                    {
                        return TransformPosition(vertex, controller.bendType, controller.bendPivotPointPosition, controller.bendRotationCenterPosition, controller.bendRotationCenter2Position, controller.bendAngle, controller.bendMinimumRadius, controller.bendAngle2, controller.bendMinimumRadius2);
                    }

                case BendType.SpiralHorizontalRolloff_X:
                case BendType.SpiralHorizontalRolloff_Z:
                case BendType.SpiralVerticalRolloff_X:
                case BendType.SpiralVerticalRolloff_Z:
                    {
                        return TransformPosition(vertex, controller.bendType, controller.bendPivotPointPosition, controller.bendRotationCenterPosition, controller.bendAngle, controller.bendMinimumRadius, controller.bendRolloff);
                    }

                default:
                    return vertex;
            }
        }
        static Vector3 TransformPosition(Vector3 vertex, BendType bendType, Vector3 pivotPoint, Vector2 bendSize, Vector2 bendOffset)
        {
            switch (bendType)
            {
                case BendType.ClassicRunner_X_Positive:
                    {
                        Vector3 newPoint = vertex - pivotPoint;


                        float xOff = Mathf.Max(0.0f, newPoint.x - bendOffset.x);
                        float yOff = Mathf.Max(0.0f, newPoint.x - bendOffset.y);

                        newPoint = new Vector3(0.0f, bendSize.x * xOff * xOff, -bendSize.y * yOff * yOff) * 0.001f;


                        return vertex + newPoint;
                    }
                case BendType.ClassicRunner_X_Negative:
                    {
                        Vector3 newPoint = vertex - pivotPoint;


                        float xOff = Mathf.Min(0.0f, newPoint.x + bendOffset.x);
                        float yOff = Mathf.Min(0.0f, newPoint.x + bendOffset.y);

                        newPoint = new Vector3(0.0f, bendSize.x * xOff * xOff, bendSize.y * yOff * yOff) * 0.001f;


                        return vertex + newPoint;
                    }
                case BendType.ClassicRunner_Z_Positive:
                    {
                        Vector3 newPoint = vertex - pivotPoint;


                        float xOff = Mathf.Max(0.0f, newPoint.z - bendOffset.x);
                        float yOff = Mathf.Max(0.0f, newPoint.z - bendOffset.y);

                        newPoint = new Vector3(bendSize.y * yOff * yOff, bendSize.x * xOff * xOff, 0.0f) * 0.001f;


                        return vertex + newPoint;
                    }
                case BendType.ClassicRunner_Z_Negative:
                    {
                        Vector3 newPoint = vertex - pivotPoint;


                        float xOff = Mathf.Min(0.0f, newPoint.z + bendOffset.x);
                        float yOff = Mathf.Min(0.0f, newPoint.z + bendOffset.y);

                        newPoint = new Vector3(-bendSize.y * yOff * yOff, bendSize.x * xOff * xOff, 0.0f) * 0.001f;


                        return vertex + newPoint;
                    }

                default: return vertex;
            }
        }
        static Vector3 TransformPosition(Vector3 vertex, BendType bendType, Vector3 pivotPoint, float bendSize, float bendOffset)
        {
            switch (bendType)
            {
                case BendType.LittlePlanet_X:
                    {
                        Vector3 newPoint = vertex - pivotPoint;


                        float yOff = Mathf.Max(0.0f, Mathf.Abs(newPoint.y) - (bendOffset < 0 ? 0 : bendOffset)) * (newPoint.y < 0.0f ? -1.0f : 1.0f);
                        float zOff = Mathf.Max(0.0f, Mathf.Abs(newPoint.z) - (bendOffset < 0 ? 0 : bendOffset)) * (newPoint.z < 0.0f ? -1.0f : 1.0f);

                        newPoint = new Vector3(-(bendSize * yOff * yOff + bendSize * zOff * zOff) * 0.001f, 0.0f, 0.0f);


                        return vertex + newPoint;
                    }
                case BendType.LittlePlanet_Y:
                    {
                        Vector3 newPoint = vertex - pivotPoint;


                        float xOff = Mathf.Max(0.0f, Mathf.Abs(newPoint.x) - (bendOffset < 0 ? 0 : bendOffset)) * (newPoint.x < 0.0f ? -1.0f : 1.0f);
                        float zOff = Mathf.Max(0.0f, Mathf.Abs(newPoint.z) - (bendOffset < 0 ? 0 : bendOffset)) * (newPoint.z < 0.0f ? -1.0f : 1.0f);

                        newPoint = new Vector3(0.0f, -(bendSize * zOff * zOff + bendSize * xOff * xOff) * 0.001f, 0.0f);


                        return vertex + newPoint;
                    }
                case BendType.LittlePlanet_Z:
                    {
                        Vector3 newPoint = vertex - pivotPoint;


                        float xOff = Mathf.Max(0.0f, Mathf.Abs(newPoint.x) - (bendOffset < 0 ? 0 : bendOffset)) * (newPoint.x < 0.0f ? -1.0f : 1.0f);
                        float yOff = Mathf.Max(0.0f, Mathf.Abs(newPoint.y) - (bendOffset < 0 ? 0 : bendOffset)) * (newPoint.y < 0.0f ? -1.0f : 1.0f);

                        newPoint = new Vector3(0.0f, 0.0f, -(bendSize * xOff * xOff + bendSize * yOff * yOff) * 0.001f);


                        return vertex + newPoint;
                    }

                case BendType.CylindricalTower_X:
                    {
                        Vector3 newPoint = vertex - pivotPoint;

                        float zOff = Mathf.Max(0.0f, Mathf.Abs(newPoint.x) - bendOffset) * (newPoint.x < 0.0f ? -1.0f : 1.0f);
                        newPoint = new Vector3(0.0f, 0.0f, bendSize * zOff * zOff * 0.001f);


                        return vertex + newPoint;
                    }
                case BendType.CylindricalTower_Z:
                    {
                        Vector3 newPoint = vertex - pivotPoint;

                        float xOff = Mathf.Max(0.0f, Mathf.Abs(newPoint.z) - bendOffset) * (newPoint.z < 0.0f ? -1.0f : 1.0f);
                        newPoint = new Vector3(bendSize * xOff * xOff * 0.001f, 0.0f, 0.0f);


                        return vertex + newPoint;
                    }


                case BendType.CylindricalRolloff_X:
                    {
                        Vector3 newPoint = vertex - pivotPoint;

                        float yOff = Mathf.Max(0.0f, Mathf.Abs(newPoint.x) - bendOffset) * (newPoint.x < 0.0f ? -1.0f : 1.0f);
                        newPoint = new Vector3(0.0f, -bendSize * yOff * yOff * 0.001f, 0.0f);


                        return vertex + newPoint;
                    }
                case BendType.CylindricalRolloff_Z:
                    {
                        Vector3 newPoint = vertex - pivotPoint;

                        float xOff = Mathf.Max(0.0f, Mathf.Abs(newPoint.z) - bendOffset) * (newPoint.z < 0.0f ? -1.0f : 1.0f);
                        newPoint = new Vector3(0.0f, -bendSize * xOff * xOff * 0.001f, 0.0f);


                        return vertex + newPoint;
                    }

                default: return vertex;
            }
        }
        static Vector3 TransformPosition(Vector3 vertex, BendType bendType, Vector3 pivotPoint, Vector3 rotationCenter, float bendAngle, float bendMinimumRadius)
        {
            switch (bendType)
            {
                case BendType.SpiralHorizontal_X_Positive:
                    {
                        Vector3 positionWS = vertex;


                        positionWS -= pivotPoint;
                        rotationCenter -= pivotPoint;

                        if (positionWS.x > rotationCenter.x)
                        {
                            rotationCenter.z = Mathf.Abs(rotationCenter.z) < bendMinimumRadius ? bendMinimumRadius * Sign(rotationCenter.z) : rotationCenter.z;
                            float radius = rotationCenter.z;

                            float angle = bendAngle * Sign(radius);
                            float l = 6.28318530717f * radius * (angle / 360);

                            float absX = Mathf.Abs(rotationCenter.x - positionWS.x) / l;
                            float smoothAbsX = Smooth(absX);


                            Spiral_H_Rotate_X_Negative(ref positionWS, rotationCenter, absX, smoothAbsX, l, angle);
                        }

                        positionWS += pivotPoint;

                        return positionWS;
                    }
                case BendType.SpiralHorizontal_X_Negative:
                    {
                        Vector3 positionWS = vertex;


                        positionWS -= pivotPoint;
                        rotationCenter -= pivotPoint;

                        if (positionWS.x < rotationCenter.x)
                        {
                            rotationCenter.z = Mathf.Abs(rotationCenter.z) < bendMinimumRadius ? bendMinimumRadius * Sign(rotationCenter.z) : rotationCenter.z;
                            float radius = rotationCenter.z;

                            float angle = bendAngle * Sign(radius);
                            float l = 6.28318530717f * radius * (angle / 360);

                            float absX = Mathf.Abs(rotationCenter.x - positionWS.x) / l;
                            float smoothAbsX = Smooth(absX);


                            Spiral_H_Rotate_X_Positive(ref positionWS, rotationCenter, absX, smoothAbsX, l, angle);
                        }

                        positionWS += pivotPoint;


                        return positionWS;
                    }
                case BendType.SpiralHorizontal_Z_Positive:
                    {
                        Vector3 positionWS = vertex;


                        positionWS -= pivotPoint;
                        rotationCenter -= pivotPoint;

                        if (positionWS.z > rotationCenter.z)
                        {
                            rotationCenter.x = Mathf.Abs(rotationCenter.x) < bendMinimumRadius ? bendMinimumRadius * Sign(rotationCenter.x) : rotationCenter.x;
                            float radius = rotationCenter.x;

                            float angle = bendAngle * Sign(radius);
                            float l = 6.28318530717f * radius * (angle / 360);

                            float absZ = Mathf.Abs(rotationCenter.z - positionWS.z) / l;
                            float smoothAbsZ = Smooth(absZ);


                            Spiral_H_Rotate_Z_Positive(ref positionWS, rotationCenter, absZ, smoothAbsZ, l, angle);
                        }


                        positionWS += pivotPoint;


                        return positionWS;
                    }
                case BendType.SpiralHorizontal_Z_Negative:
                    {
                        Vector3 positionWS = vertex;

                        positionWS -= pivotPoint;
                        rotationCenter -= pivotPoint;

                        if (positionWS.z < rotationCenter.z)
                        {
                            rotationCenter.x = Mathf.Abs(rotationCenter.x) < bendMinimumRadius ? bendMinimumRadius * Sign(rotationCenter.x) : rotationCenter.x;
                            float radius = rotationCenter.x;

                            float angle = bendAngle * Sign(radius);
                            float l = 6.28318530717f * radius * (angle / 360);

                            float absZ = Mathf.Abs(rotationCenter.z - positionWS.z) / l;
                            float smoothAbsZ = Smooth(absZ);


                            Spiral_H_Rotate_Z_Negative(ref positionWS, rotationCenter, absZ, smoothAbsZ, l, angle);
                        }


                        positionWS += pivotPoint;


                        return positionWS;
                    }

                case BendType.SpiralVertical_X_Positive:
                    {
                        Vector3 positionWS = vertex;


                        positionWS -= pivotPoint;
                        rotationCenter -= pivotPoint;

                        if (positionWS.x > rotationCenter.x)
                        {
                            rotationCenter.y = Mathf.Abs(rotationCenter.y) < bendMinimumRadius ? bendMinimumRadius * Sign(rotationCenter.y) : rotationCenter.y;
                            float radius = rotationCenter.y;

                            float angle = bendAngle * Sign(radius);
                            float l = 6.28318530717f * radius * (angle / 360);

                            float absX = Mathf.Abs(rotationCenter.x - positionWS.x) / l;
                            float smoothAbsX = Smooth(absX);


                            Spiral_V_Rotate_X_Negative(ref positionWS, rotationCenter, absX, smoothAbsX, l, angle);
                        }


                        positionWS += pivotPoint;


                        return positionWS;
                    }
                case BendType.SpiralVertical_X_Negative:
                    {
                        Vector3 positionWS = vertex;


                        positionWS -= pivotPoint;
                        rotationCenter -= pivotPoint;

                        if (positionWS.x < rotationCenter.x)
                        {
                            rotationCenter.y = Mathf.Abs(rotationCenter.y) < bendMinimumRadius ? bendMinimumRadius * Sign(rotationCenter.y) : rotationCenter.y;
                            float radius = rotationCenter.y;

                            float angle = bendAngle * Sign(radius);
                            float l = 6.28318530717f * radius * (angle / 360);

                            float absX = Mathf.Abs(rotationCenter.x - positionWS.x) / l;
                            float smoothAbsX = Smooth(absX);


                            Spiral_V_Rotate_X_Positive(ref positionWS, rotationCenter, absX, smoothAbsX, l, angle);
                        }


                        positionWS += pivotPoint;


                        return positionWS;
                    }
                case BendType.SpiralVertical_Z_Positive:
                    {
                        Vector3 positionWS = vertex;


                        positionWS -= pivotPoint;
                        rotationCenter -= pivotPoint;

                        if (positionWS.z > rotationCenter.z)
                        {
                            rotationCenter.y = Mathf.Abs(rotationCenter.y) < bendMinimumRadius ? bendMinimumRadius * Sign(rotationCenter.y) : rotationCenter.y;
                            float radius = rotationCenter.y;

                            float angle = bendAngle * Sign(radius);
                            float l = 6.28318530717f * radius * (angle / 360);

                            float absZ = Mathf.Abs(rotationCenter.z - positionWS.z) / l;
                            float smoothAbsZ = Smooth(absZ);


                            Spiral_V_Rotate_Z_Positive(ref positionWS, rotationCenter, absZ, smoothAbsZ, l, angle);
                        }


                        positionWS += pivotPoint;


                        return positionWS;
                    }
                case BendType.SpiralVertical_Z_Negative:
                    {
                        Vector3 positionWS = vertex;


                        positionWS -= pivotPoint;
                        rotationCenter -= pivotPoint;

                        if (positionWS.z < rotationCenter.z)
                        {
                            rotationCenter.y = Mathf.Abs(rotationCenter.y) < bendMinimumRadius ? bendMinimumRadius * Sign(rotationCenter.y) : rotationCenter.y;
                            float radius = rotationCenter.y;

                            float angle = bendAngle * Sign(radius);
                            float l = 6.28318530717f * radius * (angle / 360);

                            float absZ = Mathf.Abs(rotationCenter.z - positionWS.z) / l;
                            float smoothAbsZ = Smooth(absZ);


                            Spiral_V_Rotate_Z_Negative(ref positionWS, rotationCenter, absZ, smoothAbsZ, l, angle);
                        }


                        positionWS += pivotPoint;


                        return positionWS;
                    }

                default: return vertex;
            }
        }
        static Vector3 TransformPosition(Vector3 vertex, BendType bendType, Vector3 pivotPoint, Vector3 rotationCenter, Vector3 rotationCenter2, float bendAngle, float bendMinimumRadius, float bendAngle2, float bendMinimumRadius2)
        {
            switch (bendType)
            {
                case BendType.SpiralHorizontalDouble_X:
                    {
                        Vector3 positionWS = vertex;

                        if (positionWS.x < pivotPoint.x)
                        {
                            return TransformPosition(vertex, BendType.SpiralHorizontal_X_Negative, pivotPoint, rotationCenter, bendAngle, bendMinimumRadius);
                        }
                        else if (positionWS.x > pivotPoint.x)
                        {
                            return TransformPosition(vertex, BendType.SpiralHorizontal_X_Positive, pivotPoint, rotationCenter2, bendAngle2, bendMinimumRadius2);
                        }
                        else
                            return vertex;
                    }
                case BendType.SpiralHorizontalDouble_Z:
                    {
                        Vector3 positionWS = vertex;


                        if (positionWS.z > pivotPoint.z)
                        {
                            return TransformPosition(vertex, BendType.SpiralHorizontal_Z_Positive, pivotPoint, rotationCenter2, bendAngle2, bendMinimumRadius2);
                        }
                        else if (positionWS.z < pivotPoint.z)
                        {
                            return TransformPosition(vertex, BendType.SpiralHorizontal_Z_Negative, pivotPoint, rotationCenter, bendAngle, bendMinimumRadius);
                        }
                        else
                            return vertex;
                    }

                case BendType.SpiralVerticalDouble_X:
                    {
                        Vector3 positionWS = vertex;

                        if (positionWS.x < pivotPoint.x)
                        {
                            return TransformPosition(vertex, BendType.SpiralVertical_X_Negative, pivotPoint, rotationCenter, bendAngle, bendMinimumRadius);
                        }
                        else if (positionWS.x > pivotPoint.x)
                        {
                            return TransformPosition(vertex, BendType.SpiralVertical_X_Positive, pivotPoint, rotationCenter2, bendAngle2, bendMinimumRadius2);
                        }
                        else
                            return vertex;
                    }
                case BendType.SpiralVerticalDouble_Z:
                    {
                        Vector3 positionWS = vertex;

                        if (positionWS.z > pivotPoint.z)
                        {
                            return TransformPosition(vertex, BendType.SpiralVertical_Z_Positive, pivotPoint, rotationCenter2, bendAngle2, bendMinimumRadius2);
                        }
                        else if (positionWS.z < pivotPoint.z)
                        {
                            return TransformPosition(vertex, BendType.SpiralVertical_Z_Negative, pivotPoint, rotationCenter, bendAngle, bendMinimumRadius);
                        }
                        else
                            return vertex;
                    }

                default: return vertex;
            }
        }
        static Vector3 TransformPosition(Vector3 vertex, BendType bendType, Vector3 pivotPoint, Vector3 rotationCenter, float bendAngle, float bendMinimumRadius, float bendRolloff)
        {
            switch (bendType)
            {
                case BendType.SpiralHorizontalRolloff_X:
                    {
                        if (vertex.x < rotationCenter.x - bendRolloff)
                        {
                            rotationCenter.x -= bendRolloff;

                            return TransformPosition(vertex, BendType.SpiralHorizontal_X_Negative, pivotPoint, rotationCenter, bendAngle, bendMinimumRadius);
                        }
                        else if (vertex.x > rotationCenter.x + bendRolloff)
                        {
                            rotationCenter.x += bendRolloff;

                            return TransformPosition(vertex, BendType.SpiralHorizontal_X_Positive, pivotPoint, rotationCenter, bendAngle, bendMinimumRadius);
                        }
                        else
                            return vertex;
                    }
                case BendType.SpiralHorizontalRolloff_Z:
                    {
                        if (vertex.z > rotationCenter.z + bendRolloff)
                        {
                            rotationCenter.z += bendRolloff;

                            return TransformPosition(vertex, BendType.SpiralHorizontal_Z_Positive, pivotPoint, rotationCenter, bendAngle, bendMinimumRadius);
                        }
                        else if (vertex.z < rotationCenter.z - bendRolloff)
                        {
                            rotationCenter.z -= bendRolloff;

                            return TransformPosition(vertex, BendType.SpiralHorizontal_Z_Negative, pivotPoint, rotationCenter, bendAngle, bendMinimumRadius);
                        }
                        else
                            return vertex;
                    }

                case BendType.SpiralVerticalRolloff_X:
                    {
                        if (vertex.x < rotationCenter.x - bendRolloff)
                        {
                            rotationCenter.x -= bendRolloff;

                            return TransformPosition(vertex, BendType.SpiralVertical_X_Negative, pivotPoint, rotationCenter, bendAngle, bendMinimumRadius);
                        }
                        else if (vertex.x > rotationCenter.x + bendRolloff)
                        {
                            rotationCenter.x += bendRolloff;

                            return TransformPosition(vertex, BendType.SpiralVertical_X_Positive, pivotPoint, rotationCenter, bendAngle, bendMinimumRadius);
                        }
                        else
                            return vertex;
                    }
                case BendType.SpiralVerticalRolloff_Z:
                    {
                        if (vertex.z > rotationCenter.z + bendRolloff)
                        {
                            rotationCenter.z += bendRolloff;

                            return TransformPosition(vertex, BendType.SpiralVertical_Z_Positive, pivotPoint, rotationCenter, bendAngle, bendMinimumRadius);
                        }
                        else if (vertex.z < rotationCenter.z - bendRolloff)
                        {
                            rotationCenter.z -= bendRolloff;

                            return TransformPosition(vertex, BendType.SpiralVertical_Z_Negative, pivotPoint, rotationCenter, bendAngle, bendMinimumRadius);
                        }
                        else
                            return vertex;
                    }

                default: return vertex;
            }
        }
        static Vector3 TransformPosition(Vector3 vertex, BendType bendType, Vector3 pivotPoint, Vector3 rotationAxis, Vector3 bendSize, Vector3 bendOffset)
        {
            switch (bendType)
            {
                case BendType.TwistedSpiral_X_Positive:
                    {
                        Vector3 positionWS = vertex;


                        positionWS -= pivotPoint;

                        float d = Mathf.Max(0, positionWS.x - bendOffset.x);
                        d = SmoothTwistedPositive(d, 100);
                        float angle = bendSize.x * d;

                        RotateVertex(ref positionWS, pivotPoint + new Vector3(bendOffset.x, 0, 0), rotationAxis, angle);

                        float yOff = Mathf.Max(0, positionWS.x - bendOffset.y);
                        float zOff = Mathf.Max(0, positionWS.x - bendOffset.z);

                        positionWS += new Vector3(0.0f, bendSize.y * yOff * yOff, -bendSize.z * zOff * zOff) * 0.001f;


                        positionWS += pivotPoint;


                        return positionWS;
                    }
                case BendType.TwistedSpiral_X_Negative:
                    {
                        Vector3 positionWS = vertex;


                        positionWS -= pivotPoint;

                        float d = Mathf.Min(0, positionWS.x + bendOffset.x);
                        d = SmoothTwistedNegative(d, -100);
                        float angle = bendSize.x * d;

                        RotateVertex(ref positionWS, pivotPoint - new Vector3(bendOffset.x, 0, 0), rotationAxis, angle);

                        float yOff = Mathf.Min(0, positionWS.x + bendOffset.y);
                        float zOff = Mathf.Min(0, positionWS.x + bendOffset.z);

                        positionWS += new Vector3(0.0f, bendSize.y * yOff * yOff, bendSize.z * zOff * zOff) * 0.001f;


                        positionWS += pivotPoint;


                        return positionWS;
                    }
                case BendType.TwistedSpiral_Z_Positive:
                    {
                        Vector3 positionWS = vertex;


                        positionWS -= pivotPoint;

                        float d = Mathf.Max(0, positionWS.z - bendOffset.x);
                        d = SmoothTwistedPositive(d, 100);
                        float angle = bendSize.x * d;

                        RotateVertex(ref positionWS, pivotPoint + new Vector3(0, 0, bendOffset.x), rotationAxis, angle);

                        float xOff = Mathf.Max(0, positionWS.z - bendOffset.z);
                        float yOff = Mathf.Max(0, positionWS.z - bendOffset.y);

                        positionWS += new Vector3(bendSize.z * xOff * xOff, bendSize.y * yOff * yOff, 0.0f) * 0.001f;


                        positionWS += pivotPoint;


                        return positionWS;
                    }
                case BendType.TwistedSpiral_Z_Negative:
                    {
                        Vector3 positionWS = vertex;


                        positionWS -= pivotPoint;

                        float d = Mathf.Min(0, positionWS.z + bendOffset.x);
                        d = SmoothTwistedNegative(d, -100);
                        float angle = bendSize.x * d;

                        RotateVertex(ref positionWS, pivotPoint - new Vector3(0, 0, bendOffset.x), rotationAxis, angle);

                        float xOff = Mathf.Min(0, positionWS.z + bendOffset.z);
                        float yOff = Mathf.Min(0, positionWS.z + bendOffset.y);

                        positionWS += new Vector3(-bendSize.z * xOff * xOff, bendSize.y * yOff * yOff, 0.0f) * 0.001f;


                        positionWS += pivotPoint;


                        return positionWS;
                    }

                default: return vertex;
            }
        }


        static float Smooth(float x)
        {
            float t = Mathf.Cos(x * 1.57079632679f);

            return 1 - t * t;
        }
        static float SmoothTwistedPositive(float x, float scale)
        {
            float d = x / scale;
            float s = d * d;
            float smooth = Mathf.Lerp(s, d, s) * scale;

            return x < scale ? smooth : x;
        }
        static float SmoothTwistedNegative(float x, float scale)
        {
            float d = x / scale;
            float s = d * d;
            float smooth = Mathf.Lerp(s, d, s) * scale;

            return x < scale ? x : smooth;
        }
        static float Sign(float a)
        {
            return a < 0 ? -1.0f : 1.0f;
        }


        static void RotateVertex(ref Vector3 vertex, Vector3 pivot, Vector3 axis, float angle)
        {
            //degree to rad / 2
            angle *= 0.00872664625f;


            float sinA = Mathf.Sin(angle);
            float cosA = Mathf.Cos(angle);

            Vector3 q = axis * sinA;

            //vertex
            vertex -= pivot;
            vertex += Vector3.Cross(q, Vector3.Cross(q, vertex) + vertex * cosA) * 2;
            vertex += pivot;
        }
        static void Spiral_H_Rotate_X_Positive(ref Vector3 vertex, Vector3 pivot, float absoluteValue, float smoothValue, float l, float angle)
        {
            if (absoluteValue < 1)
            {
                vertex.x = pivot.x;
                vertex.y += pivot.y * smoothValue;
            }
            else
            {
                vertex.x += l;
                vertex.y += pivot.y;
            }

            RotateVertex(ref vertex, pivot, new Vector3(0, 1, 0), angle * Mathf.Clamp01(absoluteValue));
        }
        static void Spiral_H_Rotate_X_Negative(ref Vector3 vertex, Vector3 pivot, float absoluteValue, float smoothValue, float l, float angle)
        {
            if (absoluteValue < 1)
            {
                vertex.x = pivot.x;
                vertex.y += pivot.y * smoothValue;
            }
            else
            {
                vertex.x += -l;
                vertex.y += pivot.y;
            }

            RotateVertex(ref vertex, pivot, new Vector3(0, -1, 0), angle * Mathf.Clamp01(absoluteValue));
        }
        static void Spiral_H_Rotate_Z_Positive(ref Vector3 vertex, Vector3 pivot, float absoluteValue, float smoothValue, float l, float angle)
        {
            if (absoluteValue < 1)
            {
                vertex.z = pivot.z;
                vertex.y += pivot.y * smoothValue;
            }
            else
            {
                vertex.z += -l;
                vertex.y += pivot.y;
            }

            RotateVertex(ref vertex, pivot, new Vector3(0, 1, 0), angle * Mathf.Clamp01(absoluteValue));
        }
        static void Spiral_H_Rotate_Z_Negative(ref Vector3 vertex, Vector3 pivot, float absoluteValue, float smoothValue, float l, float angle)
        {
            if (absoluteValue < 1)
            {
                vertex.z = pivot.z;
                vertex.y += pivot.y * smoothValue;
            }
            else
            {
                vertex.z += l;
                vertex.y += pivot.y;
            }

            RotateVertex(ref vertex, pivot, new Vector3(0, -1, 0), angle * Mathf.Clamp01(absoluteValue));
        }
        static void Spiral_V_Rotate_X_Positive(ref Vector3 vertex, Vector3 pivot, float absoluteValue, float smoothValue, float l, float angle)
        {
            if (absoluteValue < 1)
            {
                vertex.x = pivot.x;
                vertex.z += pivot.z * smoothValue;
            }
            else
            {
                vertex.x += l;
                vertex.z += pivot.z;
            }

            RotateVertex(ref vertex, pivot, new Vector3(0, 0, -1), angle * Mathf.Clamp01(absoluteValue));
        }
        static void Spiral_V_Rotate_X_Negative(ref Vector3 vertex, Vector3 pivot, float absoluteValue, float smoothValue, float l, float angle)
        {
            if (absoluteValue < 1)
            {
                vertex.x = pivot.x;
                vertex.z += pivot.z * smoothValue;
            }
            else
            {
                vertex.x += -l;
                vertex.z += pivot.z;
            }

            RotateVertex(ref vertex, pivot, new Vector3(0, 0, 1), angle * Mathf.Clamp01(absoluteValue));
        }
        static void Spiral_V_Rotate_Z_Positive(ref Vector3 vertex, Vector3 pivot, float absoluteValue, float smoothValue, float l, float angle)
        {
            if (absoluteValue < 1)
            {
                vertex.z = pivot.z;
                vertex.x += pivot.x * smoothValue;
            }
            else
            {
                vertex.z += -l;
                vertex.x += pivot.x;
            }

            RotateVertex(ref vertex, pivot, new Vector3(-1, 0, 0), angle * Mathf.Clamp01(absoluteValue));
        }
        static void Spiral_V_Rotate_Z_Negative(ref Vector3 vertex, Vector3 pivot, float absoluteValue, float smoothValue, float l, float angle)
        {
            if (absoluteValue < 1)
            {
                vertex.z = pivot.z;
                vertex.x += pivot.x * smoothValue;
            }
            else
            {
                vertex.z += l;
                vertex.x += pivot.x;
            }

            RotateVertex(ref vertex, pivot, new Vector3(1, 0, 0), angle * Mathf.Clamp01(absoluteValue));
        }
    }
}
