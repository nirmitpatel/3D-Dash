using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using System;

public class CCAI : MonoBehaviour
{
    public float accel = 0.8f;
    public float inertia = 0.9f;
    public float speedLimit = 10.0f;
    public float minSpeed = 1.0f;
    public float stopTime = 1.0f;
    private float currentSpeed = 0.0f;
    private float functionState = 0;

    // The next two variables are used to make sure that while the function "Accell()" is running,
    // the function "Slow()" can not run (as well as the reverse).
    private bool accelState;
    private bool slowState;

    // This variable will store the "active" target object (the waypoint to move to).
    private Transform waypoint;

    // This is the speed the object will rotate to face the active Waypoint.
    public float rotationDamping = 6.0f;

    // If this is false, the object will rotate instantly toward the Waypoint.
    // If true, you get smoooooth rotation baby!
    public bool smoothRotation = true;

    // holds all the Waypoint Objects that you assign in the inspector.
    public Transform[] waypoints;

    // This variable keeps track of which Waypoint Object
    private int WPindexPointer;

    void Start()
    {
        // When the script starts set "0" or function Accell() to be active.
        functionState = 0;
    }

    void Update()
    {
        // If functionState variable is currently "0" then run "Accell()".
        if (functionState == 0)
        {
            Accell();
        }

        // If functionState variable is currently "1" then run "Slow()".
        if (functionState == 1)
        {
            StartCoroutine(Slow());
        }

        waypoint = waypoints[WPindexPointer]; //Keep the object pointed toward the current Waypoint object.
    }

    void Accell()
    {
        if (accelState == false)
        {
            // Make sure that if Accell() is running, Slow() can not run.
            accelState = true;
            slowState = false;
        }

        if (waypoint) //If there is a waypoint do the next "if".
        {
            if (smoothRotation)
            {
                // Look at the active waypoint.
                var rotation = Quaternion.LookRotation(waypoint.position - transform.position);

                // Make the rotation nice and smooth.
                // If smoothRotation is set to "On", do the rotation over time
                // with nice ease in and ease out motion.
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
            }
        }

        // Now do the accelleration toward the active waypoint untill the "speedLimit" is reached
        currentSpeed = currentSpeed + accel * accel;
        transform.Translate(0, 0, Time.deltaTime * currentSpeed);

        // When the "speedlimit" is reached or exceeded ...
        if (currentSpeed >= speedLimit)
        {
            // ... turn off accelleration and set "currentSpeed" to be
            currentSpeed = speedLimit;
        }
    }

    //The function "OnTriggerEnter" is called when a collision happens.
    void OnTriggerEnter()
    {
        // When the GameObject collides with the waypoint's collider,
        // activate "Slow()" by setting "functionState" to "1".
        functionState = 1;

        // When the GameObject collides with the waypoint's collider,
        // change the active waypoint to the next one in the array variable "waypoints".
        WPindexPointer++;

        // When the array variable reaches the end of the list ...
        if (WPindexPointer >= waypoints.Length)
        {
            // ... reset the active waypoint to the first object in the array variable
            // "waypoints" and start from the beginning.
            WPindexPointer = 0;
        }
    }

    IEnumerator Slow()
    {
        if (slowState == false) //
        {
            // Make sure that if Slow() is running, Accell() can not run.
            accelState = false;
            slowState = true;
        }

        // Begin to do the slow down (or speed up if inertia is set above "1.0" in the inspector).
        currentSpeed = currentSpeed * inertia;
        transform.Translate(0, 0, Time.deltaTime * currentSpeed);

        // When the "minSpeed" is reached or exceeded ...
        if (currentSpeed <= minSpeed)
        {
            // ... Stop the movement by setting "currentSpeed to Zero.
            currentSpeed = 0.0f;
            // Wait for the amount of time set in "stopTime" before moving to next waypoint.
            yield return new WaitForSeconds(stopTime);
            // Activate the function "Accell()" to move to next waypoint.
            functionState = 0;
        }
    }

}