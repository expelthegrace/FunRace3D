using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui : MonoBehaviour
{
    //GameObjects
    public GameObject startPanel;
    public player Player;

    //Globals
    public int state; //0 to start, 1 playing, 2 dead


    public void Start()
    {
        startPanel.SetActive(true);

        Player = GameObject.Find("Player").GetComponent<player>();
        state = 0;
    }

    public void Update()
    {
        if (state == 0)
        {
            startPanel.SetActive(true);           
            
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0))
            {
                state = 1;
                startPanel.SetActive(false);

            }
        }
        else if (Player.dead)
        {
            state = 2;
            startPanel.SetActive(true);
        }
        if (state == 2 && !Player.dead)
        {
            state = 0;
        }
    }
}
