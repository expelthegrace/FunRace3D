using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinttodie : MonoBehaviour
{
    //Globals
    public float speedRotation = 3;
    public float rotationDir = 1;


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * (speedRotation * rotationDir * Time.deltaTime));
    }
}
