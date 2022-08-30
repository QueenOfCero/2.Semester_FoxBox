using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Easy2DPlayerMovement
{
    public class InputManager : MonoBehaviour
    {
        /*
         * =====================================================
           This is the input manager that listens to all the 
           button presses from keyboard and joysticks.
           The movement controller listens to this class to 
           determine player movement.
         * =====================================================
         */

        public MovementManager controller;
        public Animator animator;



        public Action onJumpPressed; // action set by the MovementManager
        public Action onJumpReleased; //action set by the MovementManager
        public Action onAttackPressed; //action set by the MovementManager
        public Action onAttackReleassed; //action set by the MovementManager

        internal bool isJumpPressed;
        internal bool isSprintPressed;
        internal bool isLeftPressed;
        internal bool isRightPressed;
        
        //public float deadZoneV;
        internal float foxDeadZone;
        internal float squirrelDeadZone;
        internal string foxAxis;
        internal string squirrelAxis;


        internal bool playerIsFox = true;
        internal bool playerIsSquirrel = true;

        [SerializeField]
        private GameObject player;

        public float timeRemaining = 0.6f;
        public float currentTimeRemaining;
        public bool timerIsRunning = false;
        public bool jumpAllowed = true;



        internal float xAxis = 0f; //controller and keyboard left and right movement axis
        internal MobileControlsScript mobileControls;

        private void Awake()
        {
            if (player.name == "Fox")
            {
                playerIsFox = true;
                playerIsSquirrel = false;
            }
            else
            {
                playerIsFox = false;
                playerIsSquirrel = true;
            }

            SetDeadZones();
            currentTimeRemaining = timeRemaining;
        }


        void Update()
        {


            if (playerIsFox)
            {

                UpdateInput("Horizontal", InputType.JUMP, foxDeadZone);

            }
            if (playerIsSquirrel)
            {
                UpdateInput("Horizontal2", InputType.JUMP2, squirrelDeadZone);
            }

            JumpTimer();


        }

        private void JumpTimer()
        {
            if (timerIsRunning)
            {
                if (currentTimeRemaining > 0)
                {
                    currentTimeRemaining -= Time.deltaTime; return;
                }

                timerIsRunning = false;
                jumpAllowed = true;

            }
        }

        private void StartJumpTimer()
        {
            currentTimeRemaining = timeRemaining;
            jumpAllowed = false;
            timerIsRunning = true;
        }

        private void UpdateInput(string axisX, string jumpInput, float zone)
        {
            //Checks both joystick axis, as well as keyboard A and D, as well as Arrows Left and Right
            //xAxis = Mathf.Clamp(Input.GetAxis(axisX), deadZoneH, 1f);

            // Applies Dead Zone to Input

            xAxis = DeadZone(axisX, zone);

            //xAxis = Input.GetAxisRaw(axisX);

            //speedcheck moved to movementmanager vertical update to prevent running into walls
            //axis check introduced to prevent sliding down slopes in run animation
            if (xAxis == 0) {animator.SetBool("noYinput", true); } else animator.SetBool("noYinput", false);


            //check if pressing Left, Right or neither
            if (xAxis < 0 || (mobileControls != null && mobileControls.isLeftBtnDown))
            {
                isLeftPressed = true;
                isRightPressed = false;
            }
            else if (xAxis > 0 || (mobileControls != null && mobileControls.isRightBtnDown))
            {
                isRightPressed = true;
                isLeftPressed = false;
            }
            else
            {
                isLeftPressed = false;
                isRightPressed = false;
            }



            //==========================================
            //check if jump was pressed
            if (Input.GetButtonDown(jumpInput))
            {
               
                if (onJumpPressed != null)
                {
                    onJumpPressed();
                }
            }

            //check if jump was released
            if (Input.GetButtonUp(jumpInput) && jumpAllowed)
            {
                StartJumpTimer();
                if (onJumpReleased != null)
                {
                    onJumpReleased();
                }
            }

            

            //check if jump is being held down
            if (Input.GetButton(jumpInput) || (mobileControls != null && mobileControls.isPressed_A))
            {
                isJumpPressed = true;
            }
            else
            {
                isJumpPressed = false;
            }  
        }

   

    internal void SetDeadZones()
    {
        if (PlayerPrefs.HasKey("FoxDeadZone"))
        {
            foxDeadZone = PlayerPrefs.GetFloat("FoxDeadZone");
        }
        else PlayerPrefs.SetFloat("FoxDeadZone", 0.01f);

        if (PlayerPrefs.HasKey("SquirrelDeadZone"))
        {
            squirrelDeadZone = PlayerPrefs.GetFloat("SquirrelDeadZone");
        }
        else PlayerPrefs.SetFloat("SquirrelDeadZone", 0.01f);
    }

    internal float DeadZone(string axis, float zone)
    {
        float unclampedAxis = Input.GetAxisRaw(axis);
        float clampedAxis;

        if (unclampedAxis != 0)
        {
            if (unclampedAxis > 0)
            {
                clampedAxis = Mathf.Clamp(unclampedAxis, zone, 1f);

            }
            else
            {
                clampedAxis = Mathf.Clamp(unclampedAxis, -1f, -zone);

            }
            return clampedAxis;
        }
        return unclampedAxis;
    }
}
}