// Curved World <http://u3d.as/1W8h>
// Copyright (c) Amazing Assets <https://amazingassets.world>
 
using UnityEngine;


namespace AmazingAssets.CurvedWorld.Examples
{
    public class RunnerChunk : MonoBehaviour
    {
        public ChunkSpawner spawner;
        

        void Update()
        {
            transform.Translate(spawner.moveDirection * spawner.movingSpeed * Time.deltaTime);
        }
        void FixedUpdate()
        {
            switch (spawner.axis)
            {
                case ChunkSpawner.Axis.XPositive:
                    if (transform.position.x > spawner.destoryZone)
                        spawner.DestroyChunk(this);
                    break;

                case ChunkSpawner.Axis.XNegative:
                    if (transform.position.x < -spawner.destoryZone)
                        spawner.DestroyChunk(this);
                    break;

                case ChunkSpawner.Axis.ZPositive:
                    if (transform.position.z > spawner.destoryZone)
                        spawner.DestroyChunk(this);
                    break;

                case ChunkSpawner.Axis.ZNegative:
                    if (transform.position.z < -spawner.destoryZone)
                        spawner.DestroyChunk(this);
                    break;
            }
            
        }
    }
}
