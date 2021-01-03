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

    [SerializeField] int joystickNum = 1;

    string jVertical;
    string jHorizontal;
    string jLookX;
    string jLookY;
    string jFireTrigger;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        gunController = GetComponent<GunController>();

        jVertical = $"J{joystickNum}Vertical";
        jHorizontal = $"J{joystickNum}Horizontal";
        jLookX = $"J{joystickNum}LookX";
        jLookY = $"J{joystickNum}LookY";
        jFireTrigger = $"J{joystickNum}FireTrigger";
    }

    void Update()
    {
        GetMovementDirection();
        GetLookDirection();
        GetShoot();
    }

    void GetMovementDirection()
    {
        input = new Vector3(Input.GetAxis(jHorizontal), 0, Input.GetAxis(jVertical));

        if (input.sqrMagnitude > 0.4f)
        {
            Vector3 direction = input.normalized;
            playerMovement.SetMovementDirection(direction);
        }
        else
        {
            Vector3 direction = Vector3.zero.normalized;
            playerMovement.SetMovementDirection(direction);
        }
    }

    void GetLookDirection()
    {
        Vector2 joy = new Vector2(Input.GetAxis(jLookX), Input.GetAxis(jLookY));
        if (joy.sqrMagnitude > 0.1f)
        {
            float angle = Mathf.Atan2(joy.x, joy.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
        }
    }

    void GetShoot()
    {      
        
        if (Input.GetAxis(jFireTrigger) == 1)
        {
            gunController.Shoot();
        }
    }
}
