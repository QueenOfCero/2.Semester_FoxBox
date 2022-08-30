using Easy2DPlayerMovement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTrigger : MonoBehaviour
{
    public string target = "GhostSquirrel";
    public string action = "JumpRight";




    public void OnTriggerEnter2D(Collider2D other)
    {
        if (action == "Reset") { Debug.Log("Reset Trigger Sent"); }
        if (GetComponent<GhostMovementManager>())
            other.GetComponent<GhostMovementManager>().ReceiveTrigger(target, action);
        if (GetComponent<GhostMovementManagerClimb>())
            other.GetComponent<GhostMovementManagerClimb>().ReceiveTrigger(target, action);
        if (action == "Reset") { Debug.Log("Reset Trigger Sent Out"); }
    }
}
