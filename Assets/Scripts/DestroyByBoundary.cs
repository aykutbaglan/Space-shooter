using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    void OnTriggerExit(Collider other)
    {
        scoreManager.MissNumber();
        Destroy(other.gameObject);
    }
}