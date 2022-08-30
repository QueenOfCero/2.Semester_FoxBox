using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneWayPlatformSquirrel : MonoBehaviour
{
    private GameObject currentOneWayPlatform;

    [SerializeField]
    public BoxCollider2D playerBox;
   


    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentOneWayPlatform != null)
            {
                StartCoroutine(DisableCollision());
            }
        }
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            currentOneWayPlatform = collision.gameObject;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            currentOneWayPlatform = null;
        }

    }

    private IEnumerator DisableCollision()
    {
        BoxCollider2D platformCollider = currentOneWayPlatform.GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(playerBox, platformCollider);
        yield return new WaitForSeconds(0.25f);
        Physics2D.IgnoreCollision(playerBox, platformCollider, false);
      
    }

}
