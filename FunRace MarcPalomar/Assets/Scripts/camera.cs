using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    //GameObjects
    public Transform cameraWayPointsParent;
    public Transform player;
    public Transform cameraSpawn;
    public Transform cameraDeadtarget;
    public Transform cameraDeadPosition;
    public player Player;

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

        Player = GameObject.Find("Player").GetComponent<player>();
    }

    private void Reset()
    {
        transform.position = cameraSpawn.position;
        actualPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0) && !Player.dead)
        {
            //Camera movement
            transform.position = Vector3.MoveTowards(transform.position, waypoints[actualPoint].position, Time.deltaTime * cameraSpeed);
            if (Vector3.Distance(this.transform.position, waypoints[actualPoint].position) < wayPointNear)
            {
                if (actualPoint + 1 < waypoints.Count)
                    actualPoint++;

            }
        }
        if (Player.dead)
        {

            transform.position = Vector3.MoveTowards(transform.position, cameraDeadPosition.position, Time.deltaTime * cameraSpeed * 20);
            transform.rotation = Quaternion.LookRotation((cameraDeadtarget.position - transform.position).normalized);
            Debug.DrawRay(transform.position, (cameraDeadtarget.position - transform.position).normalized);
        }
        else transform.rotation = Quaternion.LookRotation((player.position - transform.position).normalized);
 



        if (Input.GetKey(KeyCode.R))
        {
            Reset();
        }
    }
}
