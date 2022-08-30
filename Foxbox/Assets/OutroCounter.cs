using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutroCounter : MainCollectibles
{
    // Start is called before the first frame update
    void Start()
    {
        if (charakterCounter != 1)
        {
            plural = "s";
        }

        Text.SetText("You found " + charakterCounter + "/4 hidden animal" + plural + "!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
