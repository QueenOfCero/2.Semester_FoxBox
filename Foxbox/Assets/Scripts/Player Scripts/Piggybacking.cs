using Easy2DPlayerMovement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Piggybacking : MonoBehaviour
{
    [SerializeField] private GameObject foxSprite;
    [SerializeField] private GameObject fox;

    [SerializeField] private float jumpForce = 20f;
    public void StartPiggybacking(GameObject squirrel)
    {
        foxSprite.GetComponent<Animator>().SetBool("Piggybacking",true);
        //squirrel.GetComponent<SpriteRenderer>().enabled = false;
        squirrel.GetComponent<MovementManager>().Piggybacking(true);
        fox.GetComponent<MovementManager>().isPiggybacking = true;



    }
    public void EndPiggybacking(GameObject squirrel)
    {
        Debug.Log("Endpiggybackong on Fox called");
        foxSprite.GetComponent<Animator>().SetBool("Piggybacking",false);
        fox.GetComponent<MovementManager>().isPiggybacking = false;
        //squirrel.GetComponent<SpriteRenderer>().enabled = true;
        squirrel.GetComponent<Rigidbody2D>().AddForce(transform.up * jumpForce);
    }
}
