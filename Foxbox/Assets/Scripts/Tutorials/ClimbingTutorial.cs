using Easy2DPlayerMovement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingTutorial : MonoBehaviour
{
    public GameObject nonGlow;
    public GameObject objectToSwitch;

    private GameObject fox;
    private GameObject squirrel;
   // public GameObject ghostFox;
    public GameObject ghostSquirrel;

    public Animator ghostFoxAnimator;

    private Material owlMaterial;
    private float nonGlowAlpha;
    private float playerDistance;
    private float desiredAlpha;

    public float secondsUntilStart = 3;
    //public float secondsUntilReset = 3;
    public float currentTimeRemaining;
    public bool timerIsRunning = false;
  
    private bool stopTimer;
  

    // Value used to know when the enemy has been spawned


    void Start()
        {

        if (GameObject.Find("Fox") != null) { fox = GameObject.Find("Fox"); }

        if (GameObject.Find("Squirrel") != null) { squirrel = GameObject.Find("Squirrel"); }

      //  if (GameObject.Find("GhostFox") != null) { ghostFox = GameObject.Find("GhostFox"); }

        //if (GameObject.Find("GhostFox+Squirrel") != null) { ghostFox = GameObject.Find("GhostFox+Squirrel"); }


        // Because `Material` is a class,
        // The following line does not create a copy of the material
        // But creates a reference (points to) the material of the renderer
        owlMaterial = nonGlow.GetComponent<SpriteRenderer>().material;

        currentTimeRemaining = secondsUntilStart;


        }

        // Update is called once per frame
        void Update()
        {
        desiredAlpha = 1f - (Mathf.Pow((7f - playerDistance), 2)) / 100;

        if (CheckPlayerDistance() < 7) 
        {
            SetAlpha(desiredAlpha);
            //SetAlpha();
        }
        // Set the alpha according to the current distance to the closest fox/squirrel

        if (timerIsRunning && !stopTimer)
        {
            if (currentTimeRemaining > 0)
            {
                currentTimeRemaining -= Time.deltaTime; return;
            }
            else
            {
                     currentTimeRemaining = secondsUntilStart;
                    timerIsRunning = false;
            //  ghostSquirrel.GetComponent<GhostMovementManagerClimb>().Jump();
                ghostSquirrel.GetComponent<GhostMovementManagerClimb>().isRightPressed = true;
                 
                    //start second timer

                  
                }


            
        }

    }

    private float CheckPlayerDistance()
    {
        float distanceToFox = Vector2.Distance(transform.position, fox.transform.position);
        float distanceToSquirrel = Vector2.Distance(transform.position, squirrel.transform.position);
        if (distanceToFox > distanceToSquirrel)
            {
            playerDistance = distanceToSquirrel; return distanceToSquirrel;
        }
        else playerDistance = distanceToFox;  return distanceToFox;
    }

    void SetAlpha(float alpha)
        {
            // Here you assign a color to the referenced material,
            // changing the color of your renderer
            Color color = owlMaterial.color;
        //Debug.Log(color);
            color.a = Mathf.Clamp(alpha, 0, 1);
       // Debug.Log(color + "(2)");
        owlMaterial.color = color;
        nonGlow.GetComponent<SpriteRenderer>().color = owlMaterial.color;
        }
  



public void OnTriggerEnter2D(Collider2D other)

{
      
    if (other.attachedRigidbody.velocity.y < 0)
    {
            Debug.Log("switch nht)");
            //ghostFox.GetComponent<GhostMovementManager>().shouldJump = false;
            objectToSwitch.SetActive(true);
        nonGlow.SetActive(false);
        timerIsRunning = true;
            stopTimer = false;
       //  ghostFoxAnimator.SetTrigger("FadeInTrigger");
            
        }
}
}
