// Curved World <http://u3d.as/1W8h>
// Copyright (c) Amazing Assets <https://amazingassets.world>
 
using UnityEngine;

#if USE_INPUT_SYSTEM && ENABLE_INPUT_SYSTEM
    using UnityEngine.InputSystem;
#endif


namespace AmazingAssets.CurvedWorld.Examples
{
    public class SwitchInput : MonoBehaviour
    {
        public GameObject InputStandard;
        public GameObject InputNew;

        private void Awake()
        {
#if USE_INPUT_SYSTEM && ENABLE_INPUT_SYSTEM
            Instantiate(InputNew);
#else
            Instantiate(InputStandard);
#endif
        }
    }
}
