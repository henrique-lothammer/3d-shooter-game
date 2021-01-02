using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(GunController))]
public class PlayerInputJoystick : MonoBehaviour
{
    Vector3 input;

    PlayerMovement playerMovement;
    GunController gunController;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        gunController = GetComponent<GunController>();
    }

    void Update()
    {
        GetMovementDirection();
        GetLookDirection();
        GetShoot();
    }

    void GetMovementDirection()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 direction = input.normalized;
        playerMovement.SetMovementDirection(direction);
    }

    void GetLookDirection()
    {
        Vector3 point = new Vector3(transform.position.x + Input.GetAxis("LookX"), transform.position.y, transform.position.z + Input.GetAxis("LookY"));
       
        playerMovement.LookAt(point);
    }

    void GetShoot()
    {      
        if (Input.GetAxis("FireTrigger") < 0)
        {
            gunController.Shoot();
        }
    }
}
