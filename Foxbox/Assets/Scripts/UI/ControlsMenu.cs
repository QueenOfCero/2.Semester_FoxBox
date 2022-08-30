using Easy2DPlayerMovement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsMenu : InputManager
{
    //internal float xAxis = 0f;
    //private float foxDeadZone;
    //private float squirrelDeadZone;
    //private string  foxAxis;
    //private string  squirrelAxis;

    public Animator foxAnimator;
    public Animator squirrelAnimator;


    void Awake()
    {
        SetDeadZones();


        
    }

    //private void SetDeadZones()
    //{
    //    if (PlayerPrefs.HasKey("FoxDeadZone"))
    //    {
    //        foxDeadZone = PlayerPrefs.GetFloat("FoxDeadZone");
    //    }
    //    else PlayerPrefs.SetFloat("FoxDeadZone", 0.01f);

    //    if (PlayerPrefs.HasKey("SquirrelDeadZone"))
    //    {
    //        squirrelDeadZone = PlayerPrefs.GetFloat("SquirrelDeadZone");
    //    }
    //    else PlayerPrefs.SetFloat("SquirrelDeadZone", 0.01f);
    //}

    private void Update()
    {
        UpdateAnimal(foxAnimator, "Horizontal", foxDeadZone);
        UpdateAnimal(squirrelAnimator, "Horizontal2", squirrelDeadZone);


    }

    private void UpdateAnimal(Animator animator, string axis, float deadZone)
    {
        float value;
        int integer = 0;
        value = DeadZone(axis, deadZone);

        if (value == 0) { integer = 0; }
        if (value > 0) { integer = 1; }
        if (value < 0) { integer = -1; }

        animator.SetInteger("Axis", integer);
    }


    


    void Save()
    {
        PlayerPrefs.SetFloat("FoxDeadZone", foxDeadZone);
        PlayerPrefs.SetFloat("SquirrelDeadZone", squirrelDeadZone);
    }




}