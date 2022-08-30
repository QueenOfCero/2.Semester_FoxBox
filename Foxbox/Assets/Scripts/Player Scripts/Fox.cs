using System.Collections;
using System.Collections.Generic;
using UnityEngine;




namespace RayWenderlich.Unity.StatePatternInUnity
{
    //[RequireComponent(typeof(CapsuleCollider))]
    public class Fox : Animal
    {
        [SerializeField]
        private FoxData data;

        private Rigidbody2D rb2d;

        //Duplikate von FoxData um Compiler Errors zu umgehen
        public float maxSpeed = 3;
        public float speed = 50f;
        public float jumpPower = 150f;
        public bool grounded;

        void Start()
        {

            rb2d = gameObject.GetComponent<Rigidbody2D>();

        }


        void Update()
        {



            if (Input.GetAxis("Horizontal") < -0.5)
            {
                transform.localScale = new Vector3(-5, 5, 5);

            }
            if (Input.GetAxis("Horizontal") > 0.5)
            {
                transform.localScale = new Vector3(5, 5, 5);

            }

            if (Input.GetButtonUp("Jump") && Mathf.Abs(rb2d.velocity.y) < 0.001f)
            {
                rb2d.AddForce(new Vector2(Input.GetAxis("Horizontal"), jumpPower), ForceMode2D.Impulse);

            }

        }

        void FixedUpdate()
        {

            Vector3 easeVelocity = rb2d.velocity;
            easeVelocity.y = rb2d.velocity.y;
            easeVelocity.z = 0.0f;
            easeVelocity.x *= 0.75f;

            float h = Input.GetAxis("Horizontal");

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
}

