using Easy2DPlayerMovement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTriggerMovement : MonoBehaviour
{
    public GhostMovementManagerClimb movementManager;
    public string target = "GhostSquirrel";
    public string action = "JumpRight";
    public void OnTriggerEnter2D(Collider2D other)
    {
       movementManager.ReceiveTrigger(target, action);
      if (action == "Reset") { Debug.Log("Reset Trigger Sent Out"); }
    }
}
