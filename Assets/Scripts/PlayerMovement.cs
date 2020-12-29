using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    Rigidbody rigidbd;
    
    float movementSpeed = 5;
    Vector3 movementDirection;

    void Start()
    {
        rigidbd = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector3 velocity = movementDirection * movementSpeed;
        rigidbd.MovePosition(rigidbd.position + velocity * Time.deltaTime);
    }

    public void SetMovementDirection(Vector3 direction)
    {
        movementDirection = direction;
    }

    public void LookAt(Vector3 LookAtPoint)
    {
        Vector3 heightCorrectPoint = new Vector3(LookAtPoint.x,transform.position.y,LookAtPoint.z);
        transform.LookAt(LookAtPoint);
    }
}