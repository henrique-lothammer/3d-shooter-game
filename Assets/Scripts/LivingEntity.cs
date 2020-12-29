using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{
    [SerializeField] float startHealth;
    float health;
    bool dead;

    private void Start()
    {
        health = startHealth;
    }

    public void TakeHit(float damage, RaycastHit hit)
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
        GameObject.Destroy(gameObject);
    }
}
