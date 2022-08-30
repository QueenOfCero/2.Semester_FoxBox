using Easy2DPlayerMovement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gate : MonoBehaviour
{
    public GameObject ScreenGateRight;
    public GameObject ScreenGateLeft;
    public GameObject ScreenGateArea;
    private GameObject fox;
    private GameObject squirrel;

    public bool foxEntered = false;
    public bool squirrelEntered = false;

    private float GateAreaX;

    UnityEvent MyEvent = new UnityEvent();

    private void Awake()
    {
        MyEvent.AddListener(CharcterCheck);
    }

    private void Start()
    {
        fox = GameObject.Find("Fox");
        squirrel = GameObject.Find("Squirrel");
        //ScreenGateLeft.SetActive(false);

        GateAreaX = ScreenGateArea.transform.position.x;

        CharcterCheck();
    }

    private void Update()
    {
        CharcterCheck();
    }

    private void CharcterCheck()
    {
        if ((fox.transform.position.x > ScreenGateArea.transform.position.x) && (squirrel.transform.position.x > ScreenGateArea.transform.position.x))
        {
            ScreenGateRight.SetActive(false);
            ScreenGateLeft.SetActive(true);
        }
        else
        {
            ScreenGateRight.SetActive(true);
            ScreenGateLeft.SetActive(false);
        }
        //Debug.Log("Event");
    }

   
    public void OnTriggerEnter2D(Collider2D other)
    {
        float FoxX = fox.transform.position.x;
        float SquirrelX = squirrel.transform.position.x;

        if (other.tag == "Fox")
        {
            foxEntered = true;

 if (other.gameObject.GetComponent<MovementManager>().isPiggybacking == true)
{
                squirrelEntered = true;
  }

        }

        if (other.tag == "Squirrel")
        {
            squirrelEntered = true;
        }

        if (foxEntered && squirrelEntered)
        {
            ScreenGateRight.SetActive(!ScreenGateRight.activeSelf);
                ScreenGateLeft.SetActive(!ScreenGateLeft.activeSelf);

        } 
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Squirrel")
        {
            squirrelEntered = false;
        }
        else if (other.tag == "Fox")
        {
            foxEntered = false;

            if (other.gameObject.GetComponent<MovementManager>().isPiggybacking == true)
            {
                squirrelEntered = false;
            }

        }
    }
}
