// Curved World <http://u3d.as/1W8h>
// Copyright (c) Amazing Assets <https://amazingassets.world>
 
using UnityEngine;

#if USE_INPUT_SYSTEM && ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif


namespace AmazingAssets.CurvedWorld.Examples
{
    public class RunnerPlayer : MonoBehaviour
    {
        public enum Side { Left, Right }


        Vector3 initialPosition;
        Vector3 newPos;
        Side side;


#if USE_INPUT_SYSTEM && ENABLE_INPUT_SYSTEM
        Key moveLeftKey = Key.A;
        Key moveRightKey = Key.D;
#else
        KeyCode moveLeftKey = KeyCode.A;
        KeyCode moveRightKey = KeyCode.D;
#endif

        Animation animationComp;
        public AnimationClip moveLeftAnimation;
        public AnimationClip moveRightAnimation;

        float translateOffset = 3.5f;


        void Start()
        {
            initialPosition = transform.position;

            side = Side.Left;
            newPos = transform.localPosition + new Vector3(0, 0, translateOffset);

            animationComp = GetComponent<Animation>();
        }        
        void Update()
        {
            if (ExampleInput.GetKeyDown(moveLeftKey))
            {
                if (side == Side.Right)
                {
                    newPos = initialPosition + new Vector3(0, 0, translateOffset);
                    side = Side.Left;

                    animationComp.Play(moveLeftAnimation.name);
                }
            }
            else if (ExampleInput.GetKeyDown(moveRightKey))
            {
                if (side == Side.Left)
                {
                    newPos = initialPosition + new Vector3(0, 0, -translateOffset);
                    side = Side.Right;

                    animationComp.Play(moveRightAnimation.name);
                }
            }

            transform.localPosition = Vector3.Lerp(transform.localPosition, newPos, Time.deltaTime * 10);
        }
    }
}
