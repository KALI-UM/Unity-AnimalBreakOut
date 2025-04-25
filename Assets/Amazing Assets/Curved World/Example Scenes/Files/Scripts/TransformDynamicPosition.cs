// Curved World <http://u3d.as/1W8h>
// Copyright (c) Amazing Assets <https://amazingassets.world>
 
using UnityEngine;


namespace AmazingAssets.CurvedWorld.Examples
{
    [ExecuteAlways]
    public class TransformDynamicPosition : MonoBehaviour
    {
        public CurvedWorld.CurvedWorldController curvedWorldController;

        public Transform parent;

        public Vector3 offset;
        public bool recalculateRotation;
               

        void Start()
        {
            if (parent == null)
                parent = transform.parent;
        }
        void Update()
        {
            if (parent == null || curvedWorldController == null)
            {
                //Do nothing
            }
            else 
            {
                //Transform position
                transform.position = curvedWorldController.TransformPosition(parent.position + offset);


                //Transform normal (calcualte rotation)
                if (recalculateRotation)
                    transform.rotation = curvedWorldController.TransformRotation(parent.position + offset, parent.forward, parent.right);
            }
        }
    }
}
