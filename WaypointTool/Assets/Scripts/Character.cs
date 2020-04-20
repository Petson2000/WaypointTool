using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    public Vector3[] wayPoints;
    public int currentWaypoint = 0;
    public Vector3 targetPos;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        targetPos = wayPoints[currentWaypoint];

        if (transform.position != targetPos)
        {
            agent.SetDestination(targetPos);
            //transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }

        else
        {
            if (wayPoints[currentWaypoint] != wayPoints[wayPoints.Length - 1])
            {
                currentWaypoint++;
            }
        }
    }

    public Vector3[] GetWaypoints()
    {
        return wayPoints;
    }
}
