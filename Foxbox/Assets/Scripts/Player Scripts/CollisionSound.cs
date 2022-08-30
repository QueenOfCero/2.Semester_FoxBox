using Easy2DPlayerMovement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSound : MonoBehaviour
{
  //  public float force = 3f;
    public Rigidbody2D rb2d;
    public MovementManager movement;
  //  public float axisModifier = 0.003f;



    void OnTriggerEnter2D(Collider2D c)
    {
      //  Debug.Log("Char Collider detected Collision");
        if (c.gameObject.tag == "Box")
        {
            Rigidbody2D body = c.GetComponent<Rigidbody2D>();
            Debug.Log("Char Collider detected Collision with " + c);
            //check if the box is falling fast enough
            if (body.velocity.y < -0.25)
            {
               // float currentForce;

             //   curretForce = force * Mathf.Abs(body.velocity.y);

          //      Vector2 dir = Vector2.Reflect((new Vector2(axisModifier * (c.transform.position.x), axisModifier * (c.transform.position.y))), transform.position);
                // dir = -dir.normalized;
           //     rb2d.AddForce(dir * curretForce);

                movement.PlayEeekSound();
            //    Debug.Log("Force of " + curretForce + " into Direction " + dir + " added and Eeek-Sound played");

            }
        }
    }
}
