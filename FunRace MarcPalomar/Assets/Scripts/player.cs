using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    //GameObjects
    public Transform wayPointsParent;
    public Transform spawnPoint;

    //Modifiers
    public float playerSpeed = 1f;
    public float rotateSpeed = 1f;

    public float wayPointNear = 2;

    //Globals
    private List<Transform> waypoints;

    public int actualPoint;

    // Start is called before the first frame update
    void Start()
    {
        actualPoint = 0;

        waypoints = new List<Transform>();
        foreach (Transform child in wayPointsParent)
        {
           waypoints.Add(child);
        }
    }

    private void Reset()
    {
        transform.position = spawnPoint.position;
        actualPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0))
        {

           //Player movement
            transform.position = Vector3.MoveTowards(transform.position, waypoints[actualPoint].position, Time.deltaTime * playerSpeed);
            if (Vector3.Distance(this.transform.position, waypoints[actualPoint].position) < wayPointNear)
            {
                if (actualPoint + 1 < waypoints.Count)
                    actualPoint++;

            }
        }

        //Player rotation
        Vector3 newForward = Vector3.Slerp(transform.forward, (waypoints[actualPoint].position - transform.position).normalized, Time.deltaTime * rotateSpeed);
        transform.forward = newForward;
        float dist = Vector3.Distance(waypoints[actualPoint].position, transform.position);

        if (Input.GetKey(KeyCode.R))
        {
            Reset();
        }
    }
}
