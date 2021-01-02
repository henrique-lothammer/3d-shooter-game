using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MuzzleFlash))]
public class Gun : MonoBehaviour
{
    [SerializeField] Transform muzzle;
    [SerializeField] Projectile projectile;
    [SerializeField] Transform shell;
    [SerializeField] Transform shellEjectionPoint;
    MuzzleFlash muzzleFlash;

    [SerializeField] float rateOfFire = 100;
    [SerializeField] float shootVelocity = 35;

    float nextShootTime;

    void Start()
    {
        muzzleFlash = GetComponent<MuzzleFlash>();
    }

    public void Shoot()
    {
        if (Time.time > nextShootTime)
        {
            nextShootTime = Time.time + rateOfFire / 1000;
            Projectile newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation);
            newProjectile.SetSpeed(shootVelocity);

            Instantiate(shell, shellEjectionPoint.position, shellEjectionPoint.rotation);

            muzzleFlash.Activate();
        }
    }
}
