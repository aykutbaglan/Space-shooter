using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletCollision : MonoBehaviour
{
    [SerializeField] private GameObject playerbullet;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Asteroids"))
        {
            Destroy(playerbullet);
        }
    }
}
