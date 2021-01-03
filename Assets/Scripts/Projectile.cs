using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] LayerMask collisionMask; 
    [SerializeField] Color trailColor; 

    float speed;
    float damage = 1;
    float lifeTime = 2;
    float skinTargetWidthOffset = .1f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);

        GetComponent<TrailRenderer>().startColor = trailColor;

        Collider[] initialCollisions = Physics.OverlapSphere(transform.position, 0.1f, collisionMask);
        if(initialCollisions.Length > 0)
        {
            OnHitObject(initialCollisions[0], transform.position);
        }
    }

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

        if (Physics.Raycast(ray,out RaycastHit hit, moveDistance + skinTargetWidthOffset, collisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitObject(hit.collider, hit.point);
        }
    }

    void OnHitObject(Collider hit, Vector3 hitPoint)
    {
        LivingEntity livingEntity = hit.gameObject.GetComponent<LivingEntity>();
        if (livingEntity)
        {
            livingEntity.TakeHit(damage, hitPoint, transform.forward);
        }
        GameObject.Destroy(gameObject);
    }
}
