// Curved World <http://u3d.as/1W8h>
// Copyright (c) Amazing Assets <https://amazingassets.world>
 
using UnityEngine;


namespace AmazingAssets.CurvedWorld.Examples
{
    public class TransformStaticPosition : MonoBehaviour
    {
        public CurvedWorld.CurvedWorldController curvedWorldController;

        Vector3 originalPosition;
        Quaternion originalRotation;

        Vector3 forward;
        Vector3 right;


        private void Start()
        {
            originalPosition = transform.position;
            originalRotation = transform.rotation;

            forward = transform.forward;
            right = transform.right;
        }
        void Update()
        {
            if (curvedWorldController != null)
            {
                //Transform position
                transform.position = curvedWorldController.TransformPosition(originalPosition);

                //Transform normal (calcualte rotation)
                transform.rotation = curvedWorldController.TransformRotation(originalPosition, forward, right);
            }
        }
    }
}
