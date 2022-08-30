using Easy2DPlayerMovement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTriggerClimb : MonoBehaviour
{
    public GhostClimbingMovement climbing;
    public int climbingInput;
    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision detected with " + other);
        if (other.name == "GhostSquirrel")

            climbing.ghostClimbInput = climbingInput;
    }
}
