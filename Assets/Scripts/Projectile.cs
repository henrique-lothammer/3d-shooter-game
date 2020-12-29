using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] LayerMask collisionMask; 

    float speed;
    float damage = 1;

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    void FixedUpdate()
    {
        float moveDistance = Time.deltaTime * speed;
        CheckCollisions(moveDistance);
        transform.Translate(Vector3.forward * moveDistance);
    }

    void CheckCollisions(float moveDistance)
    {
        /*
        This make a test creating a new ray who have the length of the displacement of the bullet
        (moveDistance) in the loop of FixedUpdate, the reason is to not miss any collisions even
        the bullet move too fast.
        */

        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray,out RaycastHit hit, moveDistance, collisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitObject(hit);
        }
    }

    void OnHitObject(RaycastHit hit)
    {
        LivingEntity livingEntity = hit.collider.gameObject.GetComponent<LivingEntity>();
        if (livingEntity)
        {
            livingEntity.TakeHit(damage, hit);
        }
        GameObject.Destroy(gameObject);
    }
}
