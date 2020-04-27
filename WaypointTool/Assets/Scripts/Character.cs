using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public List<Vector3> wayPoints;
    private int currentWaypoint = 0;
    private Vector3 targetPos;
    public bool repeatPath = true;

    public GameObject wayPointObj;
    
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        targetPos = wayPoints[currentWaypoint];

        if (Vector3.Distance(transform.position, targetPos) >= .5f)
        {
            //agent.SetDestination(targetPos);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 15f * Time.deltaTime);
        }

        else
        {
            if (wayPoints[currentWaypoint] != wayPoints[wayPoints.Count - 1])
            {
                if (currentWaypoint <= wayPoints.Count)
                {
                    currentWaypoint++;
                }
            }
            
            else if (currentWaypoint == wayPoints.Count - 1 && repeatPath)
            {
                currentWaypoint = 0;
            }
        }
    }

    public void CreateWaypoint(Vector3 spawnPosition)
    {
        Vector3 newWaypoint = new Vector3(spawnPosition.x, 1f, spawnPosition.z);
        wayPoints.Add(newWaypoint);
    }
}