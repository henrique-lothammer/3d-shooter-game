using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBiteAttack : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float minDistanceToTarget = 2.5f;

    float timeBetweenAttacks = 1;
    float nextAttackTime;

    Coroutine attack;

    private void Start()
    {
        if (!target) target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) <= minDistanceToTarget)
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

        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float interpolation = 4 * (-percent * percent + percent); //parabole equation
            transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);

            yield return null;
        }
    }

    void OnDestroy()
    {
        StopCoroutine(attack);
    }
}
