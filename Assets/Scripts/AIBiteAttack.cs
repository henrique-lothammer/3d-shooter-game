using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBiteAttack : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float minDistanceToTarget = 2.5f;
    [SerializeField] float damage = 1;
    [SerializeField] Color attackColor;

    Renderer rendererComp;
    Color initialColor;
    float timeBetweenAttacks = 1;
    float nextAttackTime;

    Coroutine attack;

    private void Start()
    {
        rendererComp = GetComponent<Renderer>();
        initialColor = rendererComp.material.color;
        if (!target) target = GameObject.FindGameObjectWithTag("Player").transform;

        target.GetComponent<LivingEntity>().OnDeath += OnTargetDeath;
    }

    void Update()
    {
        if (target && Vector3.Distance(transform.position, target.position) <= minDistanceToTarget)
        {
            if (Time.time > nextAttackTime)
            {
                nextAttackTime = Time.time + timeBetweenAttacks;
                attack = StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        /* 
         percent is a value from 0 to 1;
         interpolation creates a parabole based in the percent, so it goes 0 to 1 to 0, based in 0 to 1
         and finally the lerp pick the percent, based in the interpolation 0-1-0, of the distance between the values
         to do a nice attack and go back
        */

        Vector3 originalPosition = transform.position;
        Vector3 attackPosition = target.position;

        float attackSpeed = 3;
        float percent = 0;
        bool hasBitted = false;
        rendererComp.material.color = attackColor;
        while (percent <= 1)
        {
            if(percent >= 0.5f && !hasBitted)
            {
                hasBitted = true;
                target.GetComponent<LivingEntity>().TakeHit(damage,attackPosition, transform.up);
            }

            percent += Time.deltaTime * attackSpeed;
            float interpolation = 4 * (-percent * percent + percent); //parabole equation
            transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);

            yield return null;
        }
        rendererComp.material.color = initialColor;
    }

    void OnTargetDeath()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void OnDestroy()
    {
        if (attack != null) StopCoroutine(attack);
    }
}
