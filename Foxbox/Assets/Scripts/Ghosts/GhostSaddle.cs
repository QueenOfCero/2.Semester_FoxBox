using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Easy2DPlayerMovement;



public class GhostSaddle : MonoBehaviour
{
    private GameObject ghostFoxSprite;

    public GhostPiggybacking ghostFox;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        
        if (collision.gameObject.GetComponent<GhostMovementManager>() == null) return;
        if (collision.gameObject.GetComponent<GhostMovementManager>().isGrounded) return;
        if (collision.gameObject.GetComponent<GhostMovementManager>().wants_to_jump) return;

                collision.gameObject.GetComponent<GhostMovementManager>().isPiggybacking = true;
        ghostFox.StartGhostPiggybacking(collision.gameObject);

        ghostFox.StartGhostPiggybacking(collision.gameObject);
        collision.gameObject.transform.parent = transform;
        collision.gameObject.transform.localPosition = Vector2.zero;
        collision.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        collision.gameObject.GetComponent<GhostMovementManager>().isGrounded = true;
        Debug.Log("piggybacking started on ghostsaddle collision");
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
