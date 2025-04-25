// Curved World <http://u3d.as/1W8h>
// Copyright (c) Amazing Assets <https://amazingassets.world>
 
using System;
using UnityEngine;

namespace AmazingAssets.CurvedWorld.Examples
{
    public class Perspective2D_Restarter : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                //SceneManager.LoadScene(Application.loadedLevelName);
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
        }
    }
}
