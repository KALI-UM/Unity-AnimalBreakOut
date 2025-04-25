// Curved World <http://u3d.as/1W8h>
// Copyright (c) Amazing Assets <https://amazingassets.world>
 
using UnityEngine;

#if USE_INPUT_SYSTEM && ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif


namespace AmazingAssets.CurvedWorld.Examples
{
    static public class ExampleInput
    {
#if USE_INPUT_SYSTEM && ENABLE_INPUT_SYSTEM
        static public bool GetKeyDown(Key key)
        {
            return Keyboard.current[key].wasPressedThisFrame;
        }

        static public bool GetKey(Key key)
        {
            return Keyboard.current[key].isPressed;
        }

        static public bool GetKeyUp(Key key)
        {
            return Keyboard.current[key].wasReleasedThisFrame;
        }
#else
        static public bool GetKeyDown(KeyCode key)
        {
            return Input.GetKeyDown(key);
        }

        static public bool GetKey(KeyCode key)
        {
            return Input.GetKey(key);
        }

        static public bool GetKeyUp(KeyCode key)
        {
            return Input.GetKeyUp(key);
        }
#endif
    }
}
