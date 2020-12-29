using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float stopDistance = 2.5f;

    NavMeshAgent pathfinder;

    Coroutine updatePath;

    void Start()
    {
        pathfinder = GetComponent<NavMeshAgent>();
        if (!target) target = GameObject.FindGameObjectWithTag("Player").transform;
        pathfinder.stoppingDistance = stopDistance;

        updatePath = StartCoroutine(UpdatePath());
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, target.position) > stopDistance)
        {
            updatePath = StartCoroutine(UpdatePath());
        }
        else
        {
            StopCoroutine(updatePath);
        }
    }

    IEnumerator UpdatePath()
    {
        float refreshRate = 0.25f;

        while (target != null){

            pathfinder.SetDestination(target.position);

            yield return new WaitForSeconds(refreshRate);
        }
    }

    void OnDestroy()
    {
        StopCoroutine(updatePath);
    }
}
