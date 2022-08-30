using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchOff : MonoBehaviour
{
    public GameObject objectToSwitch;






    void Start()
    {

    }


    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        objectToSwitch.SetActive(false);
    }

   
}
