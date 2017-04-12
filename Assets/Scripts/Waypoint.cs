using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public static Transform[] waypoints;
    public static bool[] checkpoint;

    // init waypoints
    void Awake()
    {

        waypoints = new Transform[transform.childCount];
        checkpoint = new bool[transform.childCount];

        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = transform.GetChild(i);
            checkpoint[i] = false;
        }

    }


}
