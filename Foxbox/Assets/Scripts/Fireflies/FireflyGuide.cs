using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyGuide : MonoBehaviour
{
    public float speed;
    public float guideSpeed;
    public float followSpeed;
    public float acceleration;
    public int startingPoint;
    public Transform[] waypoints;
    private GameObject fox;
    private GameObject squirrel;
    private int i = 0;
    private int possibleWaypoint = 1;
    private GameObject newWaypoint;

    void Start()
    {
        CheckForWaypoints();
        GenerateWaypointList();

        fox = GameObject.Find("Fox");
        squirrel = GameObject.Find("Squirrel");

        transform.position = waypoints[startingPoint].position;
    }

    private void GenerateWaypointList()
    {
        waypoints = new Transform[possibleWaypoint];
        waypoints[0] = GameObject.Find("Waypoint").transform;
        int j = 1;
        while (j < possibleWaypoint)
        { 
            AddWayPoint(j); 
            j += 1;
        }
    }

    private void AddWayPoint(int index)
    {
        waypoints[index] = GameObject.Find("Waypoint (" + index + ")").transform;
    }

    private void CheckForWaypoints()
    {
        if (GameObject.Find("Waypoint (" + possibleWaypoint + ")") != null)
        {
            newWaypoint = GameObject.Find("Waypoint (" + possibleWaypoint + ")");
            possibleWaypoint += 1;
            CheckForWaypoints();
        }
    }

    void FixedUpdate()
    {
       
        // Speed up in case both Players are already ahead of Swarm
        if (fox.transform.position.x > transform.position.x && squirrel.transform.position.x > transform.position.x)
        {
            if (speed < followSpeed)
            {
                speed += acceleration;
            }
        }
        // slow down when moving ahead of players
        else if (speed > guideSpeed) 
        { 
            speed -= acceleration; 
        }

        //progress to the next waypoint when both characters are further right than Swarm
        if (fox.transform.position.x > waypoints[i].position.x && squirrel.transform.position.x > waypoints[i].position.x)
        {
            i++;
            //Debug.Log("Waypoint Index " + i);
            if (i == waypoints.Length)
            {
                return;
            }
        }
        if (transform.position == waypoints[i].position) { return; }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[i].position, speed * Time.deltaTime);
    }
}