using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    //GameObjects
    public Transform wayPointsParent;
    public Transform spawnPoint;
    public Transform cameraDeadtarget;

    //Modifiers
    public float playerSpeed = 1f;
    public float rotateSpeed = 1f;

    public float wayPointNear = 2;
    public float dieForce = 5f;
    public float dieForceModifier = 0.9f;

    //Globals
    private List<Transform> waypoints;

    public int actualPoint;

    public bool dead;
    public float dieForceNow;

    // Start is called before the first frame update
    void Start()
    {
        actualPoint = 0;

        waypoints = new List<Transform>();
        foreach (Transform child in wayPointsParent)
        {
           waypoints.Add(child);
        }

        transform.position = spawnPoint.position;
        dead = false;
        dieForceNow = dieForce;
        
    }

    private void Reset()
    {
        transform.position = spawnPoint.position;
        actualPoint = 0;
        dead = false;
        dieForceNow = dieForce;

    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
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

        }
        else
        {
            transform.position += new Vector3(1, 1, 1).normalized * dieForceNow;
            dieForceNow *= dieForceModifier;
            dieForceNow = Mathf.Max(0.5f, dieForceNow);

            //Player rotation
            Vector3 newForward = Vector3.Slerp(transform.forward, (cameraDeadtarget.position - transform.position).normalized, Time.deltaTime * rotateSpeed * 30);
            transform.forward = newForward;
        }


        if (Input.GetKey(KeyCode.R))
        {
            Reset();
        }
    }

  
}
