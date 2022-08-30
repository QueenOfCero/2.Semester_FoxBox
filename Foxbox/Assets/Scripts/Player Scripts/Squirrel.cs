using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squirrel : Animal
{
    public float maxSpeed = 3;
    public float speed = 50f;
    public float jumpPower = 150f;

    public bool grounded;

    private Rigidbody2D rb2d;


    void Start()
    {

        rb2d = gameObject.GetComponent<Rigidbody2D>();

    }


    void Update()
    {



        if (Input.GetAxis("Horizontal Arrows") < -0.1)
        {
            transform.localScale = new Vector3(-1, 1,1);

        }
        if (Input.GetAxis("Horizontal Arrows") > 0.1)
        {
            transform.localScale = new Vector3(1, 1, 1);

        }

        if (Input.GetButtonUp("Vertical Arrows") && Mathf.Abs(rb2d.velocity.y) < 0.001f)
        {
            rb2d.AddForce(new Vector2(Input.GetAxis("Horizontal Arrows"), jumpPower), ForceMode2D.Impulse);

        }

    }

    void FixedUpdate()
    {

        Vector3 easeVelocity = rb2d.velocity;
        easeVelocity.y = rb2d.velocity.y;
        easeVelocity.z = 0.0f;
        easeVelocity.x *= 0.75f;

        float h = Input.GetAxis("Horizontal Arrows");

        //Fake friction / Easing the x speed of our player
        if (grounded)
        {

            rb2d.velocity = easeVelocity;


        }



        //Moving the Player
        rb2d.AddForce((Vector2.right * speed) * h);

        //Limiting the Speed of the Player
        if (rb2d.velocity.x > maxSpeed)
        {
            rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);

        }

        if (rb2d.velocity.x < -maxSpeed)
        {
            rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);

        }
    }
}
