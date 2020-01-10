using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    //GameObjects
    public Transform wayPointsParent;
    public Transform spawnPoint;
    public Transform cameraDeadtarget;
    public camera cam;

    public AudioSource mamamia;
    public AudioSource starget;
    public AudioSource backgroundMusic;

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
        cam = GameObject.Find("Main Camera").GetComponent<camera>();

    }

    public void Reset()
    {
        dead = false;
        transform.position = spawnPoint.position;
        actualPoint = 0;
        
        dieForceNow = dieForce;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().Sleep();
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
            cam.dead = true;
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
            {

                Reset();
                Debug.Log("reset");
                cam.Reset();
            }
            else
            {
                //Player movement
                transform.position += new Vector3(1, 1, 1).normalized * dieForceNow;
                dieForceNow *= dieForceModifier;
                dieForceNow = Mathf.Max(0.5f, dieForceNow);

                //Player rotation
                Vector3 newForward = Vector3.Slerp(transform.forward, (cameraDeadtarget.position - transform.position).normalized, Time.deltaTime * rotateSpeed * 30);
                transform.forward = newForward;


            }
        }


        if (Input.GetKey(KeyCode.R))
        {
            Reset();
            cam.Reset();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "death")
        {
            dead = true;
            mamamia.Play();
        }
        
    }

    public void OnTriggerEnter(Collider other)
    {
         if (other.gameObject.tag == "end")
        {
            starget.Play();
            backgroundMusic.Stop();
            cam.end = true;
        }
    }
}
