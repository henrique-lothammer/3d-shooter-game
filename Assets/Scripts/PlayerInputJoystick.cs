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
        Vector2 joy = new Vector2(Input.GetAxis("LookX"), Input.GetAxis("LookY"));
        if (joy.sqrMagnitude > 0.1f)
        {
            float angle = Mathf.Atan2(joy.x, joy.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
        }
    }

    void GetShoot()
    {      
        
        if (Input.GetAxis("FireTrigger") < 0)
        {
            gunController.Shoot();
        }
    }
}
