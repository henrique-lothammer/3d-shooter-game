using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(GunController))]
public class PlayerInput : MonoBehaviour
{
    Vector3 input;
    Plane groundPlane;

    PlayerMovement playerMovement;
    GunController gunController;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        gunController = GetComponent<GunController>();

        // Generate a plane specific for get mouse position
        groundPlane = new Plane(Vector3.up, transform.position);
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
        /*  
            Make a infinite ray from camera to the mouse position on screen
            and test the intersect of ray with the groundPlane, if intersect set it's length
            Finally get the point of the ray where it intersects the plane, passing the lenght
        */
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (groundPlane.Raycast(ray, out float rayLength))
        {
            Vector3 point = ray.GetPoint(rayLength);
            Debug.DrawLine(ray.origin, point, Color.red);
            playerMovement.LookAt(point);
        }
    }

    void GetShoot()
    {
        if (Input.GetMouseButton(0))
        {
            gunController.Shoot();
        }
    }
}
