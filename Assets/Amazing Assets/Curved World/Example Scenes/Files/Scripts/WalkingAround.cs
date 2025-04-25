// Curved World <http://u3d.as/1W8h>
// Copyright (c) Amazing Assets <https://amazingassets.world>
 
using UnityEngine;
using UnityEngine.AI;


namespace AmazingAssets.CurvedWorld.Examples
{
    public class WalkingAround : MonoBehaviour
    {
        public Vector2 xMinMaxRange;
        public Vector2 zMinMaxRange;

        NavMeshAgent agent;

        // Use this for initialization
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }
        // Update is called once per frame
        void FixedUpdate()
        {
            if (agent.velocity.magnitude < 0.5f)
                agent.SetDestination(new Vector3(Random.Range(xMinMaxRange.x, xMinMaxRange.y), 0, Random.Range(zMinMaxRange.x, zMinMaxRange.y)));
        }
    }
}
