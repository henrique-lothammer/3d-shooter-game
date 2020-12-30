using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] float startHealth;
    float health;
    bool dead;

    public event Action OnDeath;

    private void Start()
    {
        health = startHealth;
    }

    public void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        if (hitEffect)
        {
            var effect = Instantiate(hitEffect.gameObject, hitPoint, Quaternion.FromToRotation(Vector3.forward, hitDirection));
            Destroy(effect, hitEffect.main.startLifetime.constant);
        }
        TakeDamage(damage);
    }

    public void TakeDamage(float damage)
    {
        if (health > 0 && !dead)
        {
            health -= damage;
        }
        else
        {
            health = 0;
            Die();
        }
    }

    public void Die()
    {
        dead = true;
        if (OnDeath != null)
        {
            OnDeath();
        }
        GameObject.Destroy(gameObject);
    }
}
