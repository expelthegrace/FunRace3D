﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    //GameObjects
    public Transform cameraWayPointsParent;
    public Transform player;
    public Transform cameraSpawn;

    //Modifiers
    public float wayPointNear = 2;
    public float cameraSpeed = 10;


    //Globals
    private List<Transform> waypoints;
    public int actualPoint;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = cameraSpawn.position;
        actualPoint = 0;
        waypoints = new List<Transform>();
        foreach (Transform child in cameraWayPointsParent)
        {
            waypoints.Add(child);
        }
    }

    private void Reset()
    {
        transform.position = cameraSpawn.position;
        actualPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0))
        {
            //Camera movement
            transform.position = Vector3.MoveTowards(transform.position, waypoints[actualPoint].position, Time.deltaTime * cameraSpeed);
            if (Vector3.Distance(this.transform.position, waypoints[actualPoint].position) < wayPointNear)
            {
                if (actualPoint + 1 < waypoints.Count)
                    actualPoint++;

            }
        }

        //Camera rotation
        transform.rotation = Quaternion.LookRotation(player.position - transform.position).normalized;


        if (Input.GetKey(KeyCode.R))
        {
            Reset();
        }
    }
}