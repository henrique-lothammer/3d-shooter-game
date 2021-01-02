using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AddRandomForce : MonoBehaviour
{
    Rigidbody rigidbodyComp;
    [SerializeField] float forceMin = 90;
    [SerializeField] float forceMax = 120;


    void Start()
    {
        rigidbodyComp = GetComponent<Rigidbody>();

        float force = Random.Range(forceMin, forceMax);
        rigidbodyComp.AddForce(Vector3.right * force);
        rigidbodyComp.AddTorque(Random.insideUnitSphere * force);
    }
}
