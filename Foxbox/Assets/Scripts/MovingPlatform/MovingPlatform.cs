using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;
    public int startingPoints;
    public Transform[] points;

    public GameObject mover;


    private int i;

    void Start()
    {
       
        transform.position = points[startingPoints].position;
        
    }
    void FixedUpdate()
    {
        if (!mover.activeInHierarchy)
            return;

        if(Vector2.Distance(transform.position, points[i].position) < 0.02f)
        { 
        i++;
            if (i == points.Length)
            {
                i = 0;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        //Debug.Log($"{transform.position},{i},{points[i].position})");
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }





}
