using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{
    [SerializeField] float startHealth;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] ParticleSystem deathEffect;
    [Header("Instantiate on dead (optional)")]
    [SerializeField] GameObject deadBodyPrefab;
    [SerializeField] Color deadBodyColor;

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
        TakeDamage(damage, hitDirection);
    }

    public void TakeDamage(float damage, Vector3 hitDirection)
    {
        if (health > 0 && !dead)
        {
            health -= damage;
        }
        else
        {
            health = 0;
            Die(hitDirection);
        }
    }

    public void Die(Vector3 hitDirection)
    {
        if (deathEffect)
        {
            var effect = Instantiate(deathEffect.gameObject, transform.position, Quaternion.FromToRotation(Vector3.forward, hitDirection));
            Destroy(effect, deathEffect.main.startLifetime.constant);
        }
        if (deadBodyPrefab)
        {
            GameObject deadBody = Instantiate(deadBodyPrefab, transform.position, Quaternion.FromToRotation(Vector3.forward, hitDirection));
            deadBody.GetComponent<Renderer>().material.color = deadBodyColor;
            deadBody.GetComponent<Rigidbody>().AddForce(hitDirection * 20);
        }

        dead = true;
        GameObject.Destroy(gameObject);
        if (OnDeath != null)
        {
            OnDeath();
        }
    }
}
