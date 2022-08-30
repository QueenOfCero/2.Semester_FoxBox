using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SquirrelAgent : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;

    //[SerializeField]
    //private Transform randomTargetTransform;

    //[SerializeField]
    //private float attackRange = 3.0f;

    [SerializeField]
    private LayerMask layer;

    [SerializeField]
    private Transform playerTransform;

    //private float currentWaitIdleTime = 0.0f;

    private Vector3 originalPosition;


    private void Awake()
    {
               agent.SetDestination(playerTransform.position);
    }



    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("Enter: " + other.tag);
    //    if (other.tag == "Player")
    //    {
    //        playerTransform = other.transform;
    //        agent.SetDestination(playerTransform.position);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    Debug.Log("Exit: " + other.tag);
    //    playerTransform = null;
    //    SetRandomTarget();
    //    //agent.SetDestination(originalPosition);
    //}


    private void Update()
    {
        if (playerTransform)
        {
            agent.SetDestination(playerTransform.position);
           // currentWaitIdleTime = 0.0f;

        }
        //else
        //{
        //    if (agent.remainingDistance <= 0.1f)
        //    {
        //        if (currentWaitIdleTime >= waitIdleTime)
        //        {
        //            SetRandomTarget();
        //            currentWaitIdleTime = 0.0f;
        //        }
        //        else
        //        {
        //            currentWaitIdleTime += Time.deltaTime;
        //        }

        //    }
        //}
    }

    //private void SetRandomTarget()
    //{
    //    randomTargetTransform.position = RandomTargetPosition();

    //    agent.SetDestination(randomTargetTransform.position);
    //}

    //private Vector3 RandomTargetPosition()
    //{


    //    Vector3 direction = Random.insideUnitSphere * 20;
    //    direction += transform.position;
    //    NavMesh.SamplePosition(direction, out NavMeshHit hit, 20, 1);

    //    return hit.position;
    //}

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(randomTargetTransform.position, 1);


    //}
}
