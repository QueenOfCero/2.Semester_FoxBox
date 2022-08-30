using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflySwarmSection : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f,0f,speed,Space.Self);
    }
}
