using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed;
    private Rigidbody physic;

    void Start()
    {
        physic = GetComponent<Rigidbody>();
        physic.velocity = transform.forward * speed;
    }   
}