using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smash : MonoBehaviour
{

    //GameObjects
    public Transform begin;
    public Transform end;
    public Transform cilinder;

    
    //Modifiers
    public float period = 2;
    public float downSpeed = 2;
    public float upSpeed = 2;

    //Globals
    private float lastSmash;
    private int state; // 0 idle, 1 way down, 2 recovering


    // Start is called before the first frame update
    void Start()
    {
        lastSmash = Time.fixedTime;
        cilinder.position = begin.position;
        state = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Time.fixedTime - lastSmash > period)
        {
            state = 1;
            lastSmash = Time.fixedTime;
        }

        else if (state == 1)
        {
          
            Vector3 newPos = Vector3.Lerp(cilinder.position, end.position, Time.deltaTime * downSpeed);
            cilinder.position = newPos;

            if (newPos.y - 0.5 < end.position.y)
            {
                state = 2;
            }
        }

        else if (state == 2)
        {
            Vector3 newPos = Vector3.Lerp(cilinder.position, begin.position, Time.deltaTime * upSpeed);
            cilinder.position = newPos;

            if (newPos.y + 0.5 > begin.position.y)
            {
                state = 0;
            }
        }
    }
}
