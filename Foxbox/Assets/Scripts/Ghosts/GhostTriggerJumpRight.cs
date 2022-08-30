using Easy2DPlayerMovement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTriggerJumpRight : MonoBehaviour
{
    public GhostMovementManager ghostMovementManager;
        public void OnTriggerEnter2D(Collider2D other)
    {
        ghostMovementManager.shouldJump = true;
        ghostMovementManager.isRightPressed = true;
    }
}
