using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementView : MonoBehaviour
{
    public Sprite jumpUp;
    public Sprite jumpMid;
    public Sprite jumpDown;

    private Vector2 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        var velocity = GetComponent<Rigidbody2D>().velocity;

        if (velocity.y < -5)
        {
            GetComponent<SpriteRenderer>().sprite = jumpDown;
        }
        else if(velocity.y < 5)
        {
            GetComponent<SpriteRenderer>().sprite = jumpMid;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = jumpUp;
        }

        

        lastPosition = transform.position;

    }



}
