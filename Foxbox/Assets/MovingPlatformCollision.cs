using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject platform;
    public GameObject fox;
    public GameObject squirrel;


    private void Start()
    {
        fox = GameObject.Find("Fox");
        squirrel = GameObject.Find("Squirrel");
    }

    void Update() {
       // Debug.Log("collision with platform");
        if (fox.transform.position.y < transform.position.y)
        {
            Debug.Log("Fox below Platform");
            Physics.IgnoreLayerCollision(8, 9);
        }
        if (squirrel.transform.position.y < transform.position.y)
        {
            Physics.IgnoreLayerCollision(8, 10);
        }

    }
}
