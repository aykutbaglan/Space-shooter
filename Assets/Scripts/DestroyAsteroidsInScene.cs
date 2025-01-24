using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAsteroidsInScene : MonoBehaviour
{
    public void DestroyAsteroids()
    {
         GameObject[] asteroidsInScene = GameObject.FindGameObjectsWithTag("Asteroids");
         foreach (GameObject asteroid in asteroidsInScene)
         {
            Destroy(asteroid);
         }
    }   
}
