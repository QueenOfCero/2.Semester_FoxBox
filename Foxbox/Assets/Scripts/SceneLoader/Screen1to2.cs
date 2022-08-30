using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class Screen1to2 : MonoBehaviour
{
    [SerializeField]
    public GameObject Screen1Collider;
    public CameraController Camera;
    public Action CharacterPosition;

    public bool foxIsHere = false;
    public bool squirrelIsHere = false;

    UnityEvent MyEvent;

    private void Start()
    {
        if(MyEvent == null)
        {
            MyEvent = new UnityEvent();
        }
        MyEvent.AddListener(CharacterCheck);
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Fox")
        { 
            foxIsHere = true;
        }  
        else if (other.tag == "Squirrel")
        {
            squirrelIsHere = true;
        }
        else if (other.tag == "MovingPlatform")
        {
            return;
        }
        if (foxIsHere && squirrelIsHere)
        {
            Camera.ChangeCamera(Screen1Collider.transform.position);
            MyEvent.Invoke();
            //set other Screen fox/squirrel bools to false - oder OnTriggerExit2D
            //foxIsHere = false;
            //squirrelIsHere = false;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Fox")
        {
            foxIsHere = false;
        }
        else if (other.tag == "Squirrel")
        {
            squirrelIsHere = false;
        }
    }

    void CharacterCheck()
    {

    }
}

