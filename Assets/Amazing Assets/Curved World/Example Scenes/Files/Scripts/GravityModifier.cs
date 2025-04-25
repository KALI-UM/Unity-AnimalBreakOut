// Curved World <http://u3d.as/1W8h>
// Copyright (c) Amazing Assets <https://amazingassets.world>
 
using UnityEngine;


namespace AmazingAssets.CurvedWorld.Examples
{
    public class GravityModifier : MonoBehaviour
    {
        public Vector3 gravity = new Vector3(0, -9.8f, 0);
      

        void Start()
        {
            Physics.gravity = gravity;
        }
        private void OnDisable()
        {
            //Restore
            Physics.gravity = new Vector3(0, -9.8f, 0);
        }
    }
}
