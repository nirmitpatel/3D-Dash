using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{

    private Transform targetPoint;
    private int waypointIndex = 0;


	// Use this for initialization
	void Start ()
    {
        targetPoint = Waypoint.waypoints[waypointIndex];
 
	}
	
	// Update is called once per frame
	void Update ()
    {
        float distance = Mathf.Infinity;
        float distanceToWaypoint = Vector3.Distance(this.transform.position, targetPoint.transform.position);


        // If we have made it past all waypoints then reset for next lap
        if(waypointIndex == Waypoint.waypoints.Length)
        {
            waypointIndex = 0;

            for (int i = 0; i < Waypoint.waypoints.Length; i++)
            {
                Waypoint.checkpoint[i] = false;
            }
            return;
        }

        //Debug.Log("Distance to waypoint is: " + distanceToWaypoint);

        // Check distance between our car and our desired waypoint
		if(distanceToWaypoint < distance)
        {
            distance = distanceToWaypoint;
        }

        // we have reached out destination
        if(!Waypoint.checkpoint[waypointIndex] && distance <= 5.1f)
        {
            Debug.Log("we have reached waypoint: " + waypointIndex);
            Waypoint.checkpoint[waypointIndex] = true;
            waypointIndex++;
            targetPoint = Waypoint.waypoints[waypointIndex];
        }
	}
}
