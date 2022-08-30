using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Easy2DPlayerMovement;

public class Saddle : MonoBehaviour
{
    public Piggybacking fox;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<MovementManager>() == null) return;
        if (collision.gameObject.GetComponent<MovementManager>().isGrounded && !collision.gameObject.GetComponent<MovementManager>().isSwimming) return;

        collision.gameObject.GetComponent<MovementManager>().isPiggybacking = true;
        fox.StartPiggybacking(collision.gameObject);
        collision.gameObject.transform.parent = transform;
        collision.gameObject.transform.localPosition = Vector2.zero;
        collision.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        collision.gameObject.GetComponent<MovementManager>().isGrounded = true;
        //Debug.Log("piggybacking started on saddle collision");
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        //if (collision.gameObject.GetComponent<MovementManager>() == null) return;

        
        //collision.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        //collision.gameObject.GetComponent<MovementManager>().isPiggybacking = false;
        //collision.gameObject.transform.parent = fox.transform.parent;
        //fox.EndPiggybacking(collision.gameObject);
        //// Debug.Log("piggybacking exited");
    }
}
