// Curved World <http://u3d.as/1W8h>
// Copyright (c) Amazing Assets <https://amazingassets.world>
 
using UnityEngine;


namespace AmazingAssets.CurvedWorld.Examples
{
    public class ChunkSpawner : MonoBehaviour
    {
        public enum Axis { XPositive, XNegative, ZPositive, ZNegative }


        public GameObject[] chunks;
        public int initialSpawnCount = 5;
        public float destoryZone = 300;

        [Space(10)]
        public Axis axis;

        [HideInInspector]
        public Vector3 moveDirection = new Vector3(-1, 0, 0);
        public float movingSpeed = 1;


        public float chunkSize = 60;        
        GameObject lastChunk;


        void Awake()
        {
            initialSpawnCount = initialSpawnCount > chunks.Length ? initialSpawnCount : chunks.Length;

            int chunkIndex = 0;
            for (int i = 0; i < initialSpawnCount; i++)
            {
                GameObject chunk = (GameObject)Instantiate(chunks[chunkIndex]);
                chunk.SetActive(true);

                chunk.GetComponent<RunnerChunk>().spawner = this;

                switch (axis)
                {
                    case Axis.XPositive:
                        chunk.transform.localPosition = new Vector3(-i * chunkSize, 0, transform.position.z);
                        moveDirection = new Vector3(1, 0, 0);
                        break;

                    case Axis.XNegative:
                        chunk.transform.localPosition = new Vector3(i * chunkSize, 0, transform.position.z);
                        moveDirection = new Vector3(-1, 0, 0);
                        break;

                    case Axis.ZPositive:
                        chunk.transform.localPosition = new Vector3(i * chunkSize, 0, transform.position.z);
                        break;

                    case Axis.ZNegative:
                        chunk.transform.localPosition = new Vector3(i * chunkSize, 0, transform.position.z);
                        break;
                }
                

                lastChunk = chunk;

                if (++chunkIndex >= chunks.Length)
                    chunkIndex = 0;
            }           
        }        
        public void DestroyChunk(RunnerChunk thisChunk)
        {
            Vector3 newPos = lastChunk.transform.position;
            switch (axis)
            {
                case Axis.XPositive:
                    newPos.x -= chunkSize;
                    break;

                case Axis.XNegative:
                    newPos.x += chunkSize;
                    break;

                case Axis.ZPositive:
                    break;

                case Axis.ZNegative:
                    break;
            }
           


            lastChunk = thisChunk.gameObject;
            lastChunk.transform.position = newPos;
        }
    }
}
