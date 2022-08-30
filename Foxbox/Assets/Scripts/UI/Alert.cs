using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert : MonoBehaviour
{
    public float timeRemaining = 3;
    public float currentTimeRemaining;
    public bool timerIsRunning;


    void Start()
    {
        timerIsRunning = true;
        currentTimeRemaining = timeRemaining;
    }

    
    void Update()
    {
        if(timerIsRunning )
        {
            if(currentTimeRemaining > 0)
            {
                currentTimeRemaining -= Time.deltaTime; return;
            }
            else
            {
                timerIsRunning = false;
                
            }
        }
    }
}
