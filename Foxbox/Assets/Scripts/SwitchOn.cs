using Easy2DPlayerMovement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchOn : MonoBehaviour
{
    public GameObject nonGlow;
    public List <GameObject> objectsToSwitch = new List<GameObject>();

    private GameObject fox;
    private GameObject squirrel;

    private Material owlMaterial;
    private float nonGlowAlpha;
    private float playerDistance;
    private float desiredAlpha;

    public float timeRemaining = 3;
    public float currentTimeRemaining;
    public bool timerIsRunning = false;
    private bool timeToFade;

    // Value used to know when the enemy has been spawned


    void Start()
    {

        if (GameObject.Find("Fox") != null) { fox = GameObject.Find("Fox"); }

        if (GameObject.Find("Squirrel") != null) { squirrel = GameObject.Find("Squirrel"); }

        owlMaterial = nonGlow.GetComponent<SpriteRenderer>().material;

        currentTimeRemaining = timeRemaining;


    }

    void Update()
    {
        desiredAlpha = 1f - (Mathf.Pow((7f - playerDistance), 2)) / 100;

        if (CheckPlayerDistance() < 7)
        {
            SetAlpha(desiredAlpha);
        }
        // Set the alpha according to the current distance to the closest fox/squirrel

        if (timerIsRunning)
        {
            if (currentTimeRemaining > 0)
            {
                currentTimeRemaining -= Time.deltaTime; return;
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
        else playerDistance = distanceToFox; return distanceToFox;
    }

    void SetAlpha(float alpha)
    {
        Color color = owlMaterial.color;
        //Debug.Log(color);
        color.a = Mathf.Clamp(alpha, 0, 1);
        // Debug.Log(color + "(2)");
        owlMaterial.color = color;
        nonGlow.GetComponent<SpriteRenderer>().color = owlMaterial.color;
    }




    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.attachedRigidbody.velocity.y < 0.1)
        {
            foreach (GameObject objectToSwitch in objectsToSwitch)
            {
                if (!objectToSwitch) continue;
                objectToSwitch.SetActive(true);
            }
            nonGlow.SetActive(false);
            timerIsRunning = true;
        }
    }
}
