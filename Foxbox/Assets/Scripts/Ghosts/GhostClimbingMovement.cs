using Easy2DPlayerMovement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostClimbingMovement : MonoBehaviour
{
    private float vertical;
    private float speed = 8f;
    private bool isLadder;
    public bool isClimbing;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private GameObject squirrel;

    public int ghostClimbInput;

    void Update()
    {
        vertical = ghostClimbInput;

        if(isLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
            animator.SetBool("isClimbing", true);
            squirrel.GetComponent<GhostMovementManagerClimb>().Ground(true);
            squirrel.GetComponent<GhostMovementManagerClimb>().coyoteFrames = 0;
            squirrel.GetComponent<GhostMovementManagerClimb>().isClimbing = true;
        }
    }

    private void FixedUpdate()
    {
        if(isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed);

        }
        else
        {
            rb.gravityScale = 7f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
            animator.SetBool("isClimbing", false);
            squirrel.GetComponent<GhostMovementManagerClimb>().coyoteFrames = 0;
            squirrel.GetComponent<GhostMovementManagerClimb>().isClimbing = false;
        }
    }
}
