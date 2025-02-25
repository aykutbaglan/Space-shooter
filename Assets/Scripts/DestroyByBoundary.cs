using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Asteroids"))
        {
            scoreManager.MissNumber();
        }
        Destroy(other.gameObject);
    }
}