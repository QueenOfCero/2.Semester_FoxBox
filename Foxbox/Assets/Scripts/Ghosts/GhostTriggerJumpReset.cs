using Easy2DPlayerMovement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTriggerJumpReset : MonoBehaviour
{
    public GhostMovementManager ghostMovementManager;
        public void OnTriggerEnter2D(Collider2D other)
    {
        ghostMovementManager.JumpTutorialReset();
    }
}
