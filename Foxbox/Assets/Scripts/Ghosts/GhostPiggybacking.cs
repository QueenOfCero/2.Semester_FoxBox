using Easy2DPlayerMovement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GhostPiggybacking : MonoBehaviour
{
    [SerializeField] private GameObject foxSprite;
    [SerializeField] private float jumpForce = 20f;
    public void StartGhostPiggybacking(GameObject ghostSquirrel)
    {
        //foxSprite.GetComponent<Animator>().SetBool("Piggybacking", true);
        //squirrel.GetComponent<SpriteRenderer>().enabled = false;
        ghostSquirrel.GetComponent<GhostMovementManager>().Piggybacking(true);

    }
    public void EndPiggybacking(GameObject squirrel)
    {
        Debug.Log("Endpiggybackong on Fox called");
        foxSprite.GetComponent<Animator>().SetBool("Piggybacking", false);
        squirrel.GetComponent<Rigidbody2D>().isKinematic = false;
        //squirrel.GetComponent<SpriteRenderer>().enabled = true;
        squirrel.GetComponent<Rigidbody2D>().AddForce(transform.up * jumpForce);
    }
}
