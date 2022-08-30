using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Easy2DPlayerMovement
{
    public class GhostMovementManager : MonoBehaviour
    {
        /*
           =====================================================
           Created by Lost Relic Games.
           This project can be used for commercial game making purposes.
           This project may not be re-distributed.
           =====================================================
         */

        /*
         * =====================================================
           This is the movement manager that controls the players movement.
           It listens to the InputManager for input.
           =====================================================
         */

        //audio source for jump sound, used for sound demonstration purposes.
        AudioSource jumpAudioSource;

        const string LEFT = "left";
        const string RIGHT = "right";

        [Header("Walk Settings")]
        [Range(2f, 20f)]
        [SerializeField] private int walkSpeed = 8;
        [Range(2f, 20f)]
        //[SerializeField] private int runSpeed = 8;
        //[Range(2f, 20f)]
        [SerializeField] private int swimSpeed = 4;

        [Header("Collision Sensitivity")]

        [Tooltip("how close must the player be to the ground to detect a hit")]
        [Range(0.1f, 0.3f)]
        [SerializeField] float groundCheckDistance = 0.1f;

        [Tooltip("how close must the player be to a wall allow for a wall jump")]
        [Range(0.1f, 0.3f)]
        [SerializeField] float wallCheckDistance = 0.2f;
        internal string facingDirection;

        [Header("Jump Settings")]
        //these jump specic values can be tweaked through the inspector

        [Tooltip("when enabled player can jump off walls")]
        [SerializeField] private bool wallJumpEnabled;

        //[Tooltip("when enabled player can double jump")]
        //[SerializeField] private bool doubleJumpEnabled;

        [Tooltip("Initial force when jump is pressed.")]
        [SerializeField] private float initialJumpVelocity = 14;

        [Tooltip("Initial sideways force when jump is pressed.")]
        [SerializeField] private float initialWallJumpVelocity = 14;

        [Tooltip("The amount of frames a player isn't allowed to move after a walljump")]
        [SerializeField] private int walljumpWait = 15;

        [Tooltip("Offset for jump down phase.")]
        [SerializeField] private float fallMultiplier = 1;

        [Tooltip("Offset for jump up phase.")]
        [SerializeField] private float jumpMultiplier = 2f;

        [Tooltip("Maximum time the jump button can be held down.")]
        [SerializeField] private float maxJumpTime = 0.22f;

        [Tooltip("minimum time the jump button can be held down.")]
        [SerializeField] private float minJumpTime = 0.09f;

        [Header("Terrain checks ")]
        [Tooltip("The name of the ground layer. Used for collision.")]
        [SerializeField] string groundLayerName = null;

        [Tooltip("The name of the softplatform layer. Used for collision.")]
        [SerializeField] string softgroundLayerName = null;

        [Tooltip("The name of the water layer. Used for collision.")]
        [SerializeField] string waterLayerName = null;

        [Tooltip("Ground check to the left of the player's feet.")]
        [SerializeField] Transform groundCheckL = null;

        [Tooltip("Ground check in the mmiddle of the player's feet.")]
        [SerializeField] Transform groundCheckM = null;

        [Tooltip("Ground check to the right of the player's feet.")]
        [SerializeField] Transform groundCheckR = null;

        [Tooltip("Ceiling check above the player's head.")]
        [SerializeField] Transform ceilingCheckL = null;

        [Tooltip("Ceiling check above the player's head.")]
        [SerializeField] Transform ceilingCheckR = null;

        [Tooltip("If enabled raycasts will be visualised in the editor")]
        [SerializeField] private bool showRayCastLines = true;

        [SerializeField] private GameObject character;

        //private references used by the controller logic, leave these unassigned
        private Rigidbody2D rb2d;
        private Transform t;
        [SerializeField] private GameObject sprite;
        [SerializeField] private Animator animator;


        public Action onJumpPressed; // action set by the MovementManager
        public Action onJumpReleased; //action set by the MovementManager
        public Action onAttackPressed; //action set by the MovementManager
        public Action onAttackReleassed; //action set by the MovementManager

        internal bool isJumpPressed;

        [Header("Ghost Instructions ")]
        public bool shouldJump;
        public bool shouldClimb;

        public bool isLeftPressed;
        public bool isRightPressed;

        internal bool playerIsFox = true;
        internal bool playerIsSquirrel = true;


        [SerializeField]
        private string[] instructions = new string[4];




        internal float xAxis = 0f; //controller and keyboard left and right movement axis
        internal MobileControlsScript mobileControls;

        [Header("State Information ")]
        private bool wasJumpPressed;
        private bool isTryingToJump;
        private float totalJumpTime;
        private int currentSpeed;
        private int groundMask;
        private int softgroundMask;
        private int waterMask;

        public int coyoteFrames;
        [Tooltip("How many frames the player can run back up an edge after slipping off of it")]
        [SerializeField] private int coyoteFramesMax = 30;
        [Tooltip("How many frames the player can still jump after slipping off an edge")]
        [SerializeField] private int coyoteMaxJumpFrames = 7;


        //for JumpMultiplier
        private float JumpVelocity;
        private float JumpDampening = 0.1f;

        private int jumpCount = 0;
        [SerializeField]
        public bool isGrounded;

        private bool isTouchingWall;
        [SerializeField]
        public bool isSwimming;
        //private bool jumpedDuringSprint;
        private float sprintJumpDirection;
        private bool isFox;
        public bool isPiggybacking;
        private bool isTryingToWalljump;
        private int walljumpTimerCount;
        private bool hasWalljumped;
        internal string slippingDirection;
        private GameObject ghostFox;
        private GameObject ghostSquirrel;
        internal bool jumped;
        public bool hasRespawn;
        public bool wants_to_jump;

        public GameObject ghostFoxJump;
        public GameObject owlNonGlow;
        public GameObject ghostSaddle;
        private bool deactivateJumpTrigger;

        public Animator ghostFoxAnimator;




        //private bool isRotation = false;

        //=====================================================================
        // The controller is awake, this is called before start
        //=====================================================================
        private void Awake()
        {
            if (character.name == "GhostFox")
            {
                isFox = true;
            }

            if (character.name == "GhostFox+Squirrel")
            {
                isFox = true;
            }



            if (GetComponent<AudioSource>() != null)
            {
                jumpAudioSource = GetComponent<AudioSource>();
            }

            //assign the rigidbody2D
            //  inputManager = GetComponent<InputManager>();
            rb2d = GetComponent<Rigidbody2D>();
            t = transform;
            if (GameObject.Find("GhostFox") != null) { ghostFox = GameObject.Find("GhostFox"); }
            // replace GhostFox with GhostFox+Squirrel if it exists
            if (GameObject.Find("GhostFox+Squirrel") != null) { ghostFox = GameObject.Find("GhostFox+Squirrel"); }
            if (GameObject.Find("GhostSquirrel") != null) { ghostSquirrel = GameObject.Find("GhostSquirrel"); }




        }

        //=====================================================================
        // The controller is starting
        //=====================================================================
        private void Start()
        {
            //inform the Animator about interesting States
            if(isPiggybacking) { animator.SetBool("Piggybacking", true); }

            //make sure a ground layer has been assigned
            if (groundLayerName == null || groundLayerName == "")
            {
                Debug.LogWarning("Warning: A ground layer has not beed assigned");
            }

            //create a ground mask (for raycasting) based on the assigned ground layer name
            groundMask = 1 << LayerMask.NameToLayer(groundLayerName);

            softgroundMask = 1 << LayerMask.NameToLayer(softgroundLayerName);

            waterMask = 1 << LayerMask.NameToLayer(waterLayerName);

            //assign call backs jumps
            onJumpPressed = OnJumpPressed;
            onJumpReleased = OnJumpReleased;

            //assigning the starting facing direction
            DetermineFacingDirection();

            //check to see if all ray cast points have been assigned
            if (groundCheckL == null ||
               groundCheckR == null ||
               groundCheckM == null ||
               ceilingCheckL == null ||
               ceilingCheckR == null)
            {
                Debug.LogWarning("one of the ceiling or ground check points has not been assigned");
            }
        }

        //=====================================================================
        // The main update loop, this gets called every frame
        //=====================================================================
        private void Update()
        {
            ListenToTrigger();
            if (showRayCastLines) ShowRayCastLines();
            if (shouldJump) { Jump(); }
            //StopJump(-30);
            if (isPiggybacking) { animator.SetBool("Piggybacking", true); }
        }

        private void ListenToTrigger()
        {
            if (deactivateJumpTrigger)
            { deactivateJumpTrigger = false;
                ghostFoxJump.SetActive(false);
                    return;
            }
            if (isFox & ghostFoxJump.activeInHierarchy)
                Jump();
            deactivateJumpTrigger = true;
        }

        //Water Collision Trigger for Jumping into Water
        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == 4 && (!isPiggybacking || isFox))
            {
                Swim(true);
                //Debug.Log("Water Detected On Trigger");

            }
            else
            {
                if (collision.gameObject.layer == 8)
                {
                    Swim(false);
                    //Debug.Log("Ground Detected On Trigger Afer Swimming");
                }
            }
            if (collision.gameObject.layer == 11 && isGrounded == false)
            {
                Piggybacking(true);
                Debug.Log("piggybacking on trigger");


                /*if (!isFox)
                {
                    gameObject.position = Vector2(10,10);
                }*/
            }
        }



        //=====================================================================
        // Main physics update loop, all rigidbody manipulations 
        // and movement code gets executed here
        //=====================================================================
        private void FixedUpdate()
        {
            ProcessRayCastChecks();
            UpdateHorizontalMovement();
            UpdateVerticalMovement();
            //JumpMultiplier();
        }

        private void JumpMultiplier()
        {
            Vector3 pos = transform.position;

            if (JumpVelocity != 0)
            {
                pos.y += JumpVelocity;
                JumpVelocity -= JumpDampening;
                if (JumpVelocity <= 0)
                {
                    rb2d.gravityScale = 1;
                    JumpVelocity = 0;
                }
            }

            transform.position = pos;
        }


        //=====================================================================
        // get facing direction based on player scale
        //=====================================================================
        void DetermineFacingDirection()
        {
            if (sprite.GetComponent<SpriteRenderer>().flipX == true)
            {
                facingDirection = LEFT;
            }
            else
            {
                facingDirection = RIGHT;
            }
        }

        ///=====================================================================
        // Shows a visual representation of all the terrain 
        // collision checks within the editor view
        //=====================================================================
        private void ShowRayCastLines()
        {
            //set a colour for all raycast visualizations
            Color c = Color.green;
            float gDist = groundCheckDistance;
            float wDist = wallCheckDistance;

            //visualize ground check rays
            Debug.DrawLine(groundCheckL.position, groundCheckL.position + Vector3.down * gDist, c);
            Debug.DrawLine(groundCheckM.position, groundCheckM.position + Vector3.down * gDist, c);
            Debug.DrawLine(groundCheckR.position, groundCheckR.position + Vector3.down * gDist, c);

            //visualise ceiling hit ray
            Debug.DrawLine(ceilingCheckL.position, ceilingCheckL.position + Vector3.up * gDist, c);
            Debug.DrawLine(ceilingCheckR.position, ceilingCheckR.position + Vector3.up * gDist, c);

            //visualize wall check rays
            if (facingDirection == Direction.RIGHT)
            {
                Debug.DrawLine(groundCheckR.position, groundCheckR.position + Vector3.right * wDist, c);
                Debug.DrawLine(groundCheckL.position, groundCheckL.position + Vector3.left * wDist, c);
            }
            else
            {
                Debug.DrawLine(groundCheckR.position, groundCheckR.position + Vector3.left * wDist, c);
                Debug.DrawLine(groundCheckL.position, groundCheckL.position + Vector3.right * wDist, c);
            }
        }

        //=====================================================================
        // All terrain ray cast terrain collision checks
        //=====================================================================
        private void ProcessRayCastChecks()
        {
            //riding Squirrels don't do this
            if (!isFox && isPiggybacking) return;

            float gDist = groundCheckDistance;
            float wDist = wallCheckDistance;

            //shoots raycasts down from feet to look for ground
            RaycastHit2D groundHitL = Physics2D.Raycast(groundCheckL.position, Vector3.down, gDist, groundMask);
            RaycastHit2D groundHitM = Physics2D.Raycast(groundCheckM.position, Vector3.down, gDist, groundMask);
            RaycastHit2D groundHitR = Physics2D.Raycast(groundCheckR.position, Vector3.down, gDist, groundMask);

            RaycastHit2D softgroundHitL = Physics2D.Raycast(groundCheckL.position, Vector3.down, gDist, softgroundMask);
            RaycastHit2D softgroundHitM = Physics2D.Raycast(groundCheckM.position, Vector3.down, gDist, softgroundMask);
            RaycastHit2D softgroundHitR = Physics2D.Raycast(groundCheckR.position, Vector3.down, gDist, softgroundMask);

            RaycastHit2D waterHitL = Physics2D.Raycast(groundCheckL.position, Vector3.down, gDist, waterMask);
            RaycastHit2D waterHitM = Physics2D.Raycast(groundCheckM.position, Vector3.down, gDist, waterMask);
            RaycastHit2D waterHitR = Physics2D.Raycast(groundCheckR.position, Vector3.down, gDist, waterMask);

            //shoots a ray upwards to look for a ceiling
            RaycastHit2D ceilingHitL = Physics2D.Raycast(ceilingCheckL.position, Vector3.up, gDist, groundMask);
            RaycastHit2D ceilingHitR = Physics2D.Raycast(ceilingCheckR.position, Vector3.up, gDist, groundMask);

            //shoots a ray Right looking for a wall (used for wall jumping)
            RaycastHit2D wallHitR;
            RaycastHit2D wallHitL;

            wallHitR = Physics2D.Raycast(groundCheckR.position, Vector3.right, wDist, groundMask);
            wallHitL = Physics2D.Raycast(groundCheckL.position, Vector3.left, wDist, groundMask);


            //check if we are hitting a roof during a jump
            if (ceilingHitL.collider != null || ceilingHitR.collider != null)
            {
                //player has hit roof while jumping, reset the jump
                wasJumpPressed = false;
                isTryingToJump = false;
                isTryingToWalljump = false;
            }



            //check if the a ray has found ground below the feet
            if (isGrounded || rb2d.velocity.y < -0.1)
            {
                if (groundHitL.collider != null || groundHitM.collider != null || groundHitR.collider != null)
                {
                    //has found ground
                    Ground(true);

                }
            }

            //has not found ground, check for softground
            if (softgroundHitL.collider != null || softgroundHitM.collider != null || softgroundHitR.collider != null)
            {
                Ground(true);
            }

            if (waterHitL.collider != null || waterHitM.collider != null || waterHitR.collider != null)
            {
                // Debug.Log("SwimDetect");
                Swim(true);
            }


            else
            {
                if (groundHitL.collider != null || groundHitM.collider != null || groundHitR.collider != null)
                {
                    //has found ground
                    Ground(true);
                    Swim(false);
                }

                else
                {

                    //if (softgroundHitL.collider != null || softgroundHitM.collider != null || softgroundHitR.collider != null)                           
                    if (softgroundHitL.collider != null || softgroundHitM.collider != null || softgroundHitR.collider != null)
                    {
                        //has found ground
                        Ground(true);
                    }

                    else
                    {
                        //has not found ground
                        CoyoteTime();
                        //Ground(false); delayed by CoyoteTime()
                    }

                    //wallChecking: check if the player is touching a wall (used for wall jump)
                    if (wallHitR.collider != null || wallHitL.collider != null)
                    {
                        isTouchingWall = true;
                        animator.SetBool("touchesWall", true);
                    }

                    else
                    {
                        isTouchingWall = false;
                        animator.SetBool("touchesWall", false);
                    }
                }
            }
        }


        private void CharacterSlope(float angle, String Way)
        {
            if (Way == RIGHT)
            {
                //gameObject.GetComponent<SpriteRenderer>().transform.rotation = Rotate(new Vector3(0, 0, -angle));
                //sprite.transform.Rotate(new Vector3(0, 0, angle));
            }
            else
            {
                // sprite.transform.Rotate(new Vector3(0, 0, angle));
            }
        }


        //=====================================================================
        //Jump has been pressed (is called by the inputController class)
        //=====================================================================
        internal void OnJumpPressed()
        {
            //only allow the player to jump if grounded or touching wall
            if (isGrounded || (isTouchingWall && jumpCount < 2) || (!isFox && isPiggybacking)) // || isPiggybacking
            {
                //PlayJumpSound();

                wasJumpPressed = true;
                isTryingToJump = true;
                isTryingToWalljump = isTouchingWall && !isGrounded;


                Ground(false);
            }

        }

        //=====================================================================
        //Example jump sound, this is used for demonstartion purposes
        // you will want to replace it with your own custom sound manager
        private void PlayJumpSound()
        {
            if (jumpAudioSource != null)
            {
                jumpAudioSource.Play();
            }
        }

        //=====================================================================
        //Jump has been released ( this function is called by the inputController class)
        //=====================================================================
        internal void OnJumpReleased()
        {
            if (isSwimming) return;

            wasJumpPressed = false;

            //JumpVelocity = 1f;
            //rb2d.gravityScale = 10f;

        }

        //=====================================================================
        //All the jump based vertical calculations 
        //=====================================================================
        private void UpdateVerticalMovement()
        {
            if (!isGrounded)
            {
                animator.SetFloat("VerticalV", rb2d.velocity.y);

                //Vertical Velocity Check
                if (rb2d.velocity.y < -0.1) { animator.SetBool("isFalling", true); }
                if (rb2d.velocity.y > 0.1) { animator.SetBool("isJumping", true); }


            }

            if (isGrounded)
            { animator.SetFloat("VerticalV", 0f); }

            //If our player pressed the jump key... this is to maintain minimum jump height
            if (wasJumpPressed)
            {
                wasJumpPressed = false;
                totalJumpTime = 0; //reset it to zero before the jump
            }

            //The following code is the heart of the variable jump, 
            if (isTryingToJump)
            {
                totalJumpTime += Time.deltaTime;

                if (isJumpPressed)
                {
                    if (totalJumpTime <= maxJumpTime)
                    {
                        if (isTryingToWalljump && !hasWalljumped)
                        {
                            if (facingDirection == RIGHT)
                            {
                                rb2d.velocity = new Vector2(-initialWallJumpVelocity, initialJumpVelocity);
                                SetFacingDir(LEFT);
                                WallJumpTimerReset();

                            }
                            else
                            {
                                rb2d.velocity = new Vector2(initialWallJumpVelocity, initialJumpVelocity);
                                SetFacingDir(RIGHT);
                                WallJumpTimerReset();
                            }
                        }
                        else if (!isSwimming)
                        {   // jump
                            if (!isFox && isPiggybacking)
                            {   //piggybacking Squirrel jumps off of Fox
                                rb2d.isKinematic = false;
                                Piggybacking(false);
                                transform.parent = ghostFox.transform.parent;
                                ghostFox.GetComponent<GhostMovementManager>().Piggybacking(false);
                                sprite.GetComponent<SpriteRenderer>().enabled = true;
                                //Debug.Log("piggybacking exited by Squirrel Jump");
                            }
                            Jump();

                        }
                    }
                    //else
                    //{
                    //    isTryingToJump = false;
                    //    isTryingToWalljump = false;
                    //}
                }
                else
                {
                    if (totalJumpTime < minJumpTime)
                    {
                        if (isTryingToWalljump)
                        {
                            if (facingDirection == RIGHT)
                            {
                                rb2d.velocity = new Vector2(-initialWallJumpVelocity, initialJumpVelocity);
                                SetFacingDir(LEFT);
                            }
                            else
                            {
                                rb2d.velocity = new Vector2(initialWallJumpVelocity, initialJumpVelocity);
                                SetFacingDir(RIGHT);
                            }
                        }
                        else
                        {
                            rb2d.velocity = new Vector2(rb2d.velocity.x * 4, initialJumpVelocity);
                        }
                    }
                    else
                    {
                        isTryingToJump = false;
                        isTryingToWalljump = false;
                    }
                }
            }

            //create a temp gravity value for convenience
            Vector2 vGravityY = Vector2.up * Physics2D.gravity.y;

            //check if the players jump is in the rising or falling phase and calulate physics
            if (rb2d.velocity.y < 0)
            {
                rb2d.velocity += vGravityY * fallMultiplier * Time.deltaTime;
            }
            else if (rb2d.velocity.y > 0 && isTryingToJump)
            {
                //determine how far though the jump we are as a decimal percentage 
                float t = totalJumpTime / maxJumpTime * 1;
                float tempJumpM = jumpMultiplier;

                //smooth out the peak of the jump, just like in super mario
                if (t > 0.5f)
                {
                    tempJumpM = jumpMultiplier * (1 - t);
                }

                //assign the final calculation to the rigidbody2D
                rb2d.velocity += vGravityY * tempJumpM * Time.deltaTime;
            }
        }

        public void Jump()
        {
            if (!isFox && isPiggybacking) { 
            rb2d.isKinematic = false;
            Piggybacking(false);
            transform.parent = ghostFox.transform.parent;
            ghostFox.GetComponent<GhostMovementManager>().Piggybacking(false);
            sprite.GetComponent<SpriteRenderer>().enabled = true;
              //  rb2d.isKinematic = true;
                rb2d.velocity = new Vector2(rb2d.velocity.x, initialJumpVelocity * 1.8f);
                    wants_to_jump = true;
        }
        else {
               // rb2d.isKinematic = false;
                rb2d.velocity = new Vector2(rb2d.velocity.x, initialJumpVelocity*1.8f);
        animator.SetFloat("VerticalV", 1f);
            animator.SetBool("isJumping", true);
            animator.SetBool("isGrounded", false);
            jumped = true;
            
            StartCoroutine(Wait(4));
            shouldJump = false;

        }
}

        private void StopJump(int frame)
        {
            if (coyoteFrames < frame)
            {
                shouldJump = false;
            }
        }

        IEnumerator Wait(int v)
        {
            yield return new WaitForSeconds(v);
        }

        private void WallJumpTimerReset()
        {
            walljumpTimerCount = walljumpWait;
            hasWalljumped = true;
        }

        private void WalljumpTimerCount()
        {
            if (walljumpTimerCount < 1)
            {
                hasWalljumped = false;
            }
            else walljumpTimerCount -= 1;
        }

        //=====================================================================
        //All the horizontal ground and air calculations 
        //=====================================================================
        private void UpdateHorizontalMovement()
        {
            Vector2 vel;
            animator.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));


            if (hasRespawn)
            {
                vel = new Vector2(0f, rb2d.velocity.y);
                rb2d.velocity = vel;
                return;
            }

            vel = rb2d.velocity;

            //Walljump timer check, to see if the player is allowed to move
            if (hasWalljumped) { WalljumpTimerCount(); return; }

            //check if currently walking or in the air
            if (isGrounded)
            {

                currentSpeed = walkSpeed;
            }



            else
            {
                if (isSwimming) { currentSpeed = swimSpeed; }
            }


            if (hasWalljumped) return;

            //check if left, right or nothing is pressed and set the velocity and facing position
            if (isFox || !isPiggybacking)
            {

                if (isLeftPressed)
                {
                    vel.x = -currentSpeed;
                    SetFacingDir(LEFT);
                }
                else if (isRightPressed)
                {
                    vel.x = currentSpeed;
                    SetFacingDir(RIGHT);
                }
                else
                {
                    vel.x = 0;
                }

                if (isFox || !isSwimming)
                {
                    rb2d.velocity = vel;
                }
            }
        }
        private void SetFacingDir(string dir)
        {
            facingDirection = dir;
            // t.localScale = new Vector2(dir == LEFT ? -1 : 1, 1);
            sprite.GetComponent<SpriteRenderer>().flipX = dir == LEFT ? true : false;
        }


        private void Swim(bool input)
        {
            isSwimming = input;
            animator.SetBool("isSwimming", input);
            // set Animator to swimming(input)
        }

        public void Piggybacking(bool input)
        {
            // set Animator to piggybacking(input)

            isPiggybacking = input;
            animator.SetBool("Piggybacking", input);
            if (input == true && !isFox)
            {
                sprite.GetComponent<SpriteRenderer>().enabled = false;
                Ground(true);
            }
        }

        public void Ground(bool input)
        {
            isGrounded = input;
            animator.SetBool("isGrounded", input);
            if (input == true) 
            {
                hasRespawn = false;
                if (isFox || !isPiggybacking)
                { 
                    coyoteFrames = coyoteFramesMax; 
                }
                if (!isFox || isPiggybacking)
                {
                    coyoteFrames = 0;
                }
                Swim(false);
                animator.SetBool("isJumping", false);
                animator.SetBool("isFalling", false);
                animator.SetBool("isSwimming", false);
                hasWalljumped = false;
                jumped = false;
            }
        }

        private void CoyoteTime()
        {
            // Coyote Time suspends calling Ground(false) to allow Players to jump after slipping off of platforms.
            // It also allows the Player to step back up a platform they just slipped off of.

            // How many frames are left when the Player can no longer coyote jump
            int coyoteJumpFrames = coyoteFramesMax - coyoteMaxJumpFrames;

            if (coyoteFrames == coyoteFramesMax)
            {
                slippingDirection = facingDirection;
                //Debug.Log("Coyote Time started");
            }
            if (coyoteFrames < coyoteJumpFrames)
            {
                Ground(false);

            }
            
           coyoteFrames -= 1;
                //Debug.Log("Air-Borne with Coyote Time");

                // Teleport the player back up an edge they just slipped off of, if they input the opposite direction of their slipping movement
           if ((isLeftPressed && slippingDirection == RIGHT && !jumped) || (isRightPressed && slippingDirection == LEFT && !jumped))
           {
                    transform.Translate(Vector3.up * Time.deltaTime * 10, Space.World); //* Time.deltaTime
                    transform.Translate((isLeftPressed ? Vector3.left : Vector3.right) * Time.deltaTime, Space.World); //* Time.deltaTime
                    Debug.Log("Moving against the grain!");
           }
         
        }

        internal void ReceiveTrigger(string target, string action)
        {
            GameObject targetGameObject;
            if (target == "GhostSquirrel")
            {
                targetGameObject = ghostSquirrel;
                

                }

            else { targetGameObject = ghostFox; }

            if (action == "JumpRight"){
                targetGameObject.GetComponent<GhostMovementManager>().shouldJump = true;
                targetGameObject.GetComponent<GhostMovementManager>().isRightPressed = true;
                
            }

            if (!isFox && action == "Reset")
            {
                JumpTutorialReset();

            }
        }

        public void JumpTutorialReset()
        {
            shouldJump = false;
            ghostFox.GetComponent<GhostMovementManager>().Piggybacking(true);
            ghostFox.SetActive(false);
            ghostFox.GetComponent<GhostMovementManager>().shouldJump = true;
            owlNonGlow.SetActive(true);
            Piggybacking(true);
            transform.parent = ghostSaddle.transform;
           transform.localPosition = Vector2.zero;
            rb2d.isKinematic = true;
            rb2d.velocity = Vector2.zero;
            isGrounded = true;
        }
    }
}
    

    
