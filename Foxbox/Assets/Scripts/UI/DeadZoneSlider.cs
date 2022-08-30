using Easy2DPlayerMovement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadZoneSlider : MonoBehaviour
{
    

    public Slider slider;
    public int sliderNumber;


    private void Start()
    {
         if (sliderNumber == 1)
        {
            slider.value = PlayerPrefs.GetFloat("FoxDeadZone");
          }
        else
        {
            slider.value = PlayerPrefs.GetFloat("SquirrelDeadZone");
         }
    }

    public void DeadZone(float sliderValue)
    {

        if (sliderNumber == 1)
        {
            PlayerPrefs.SetFloat("FoxDeadZone", sliderValue);
        }
        else
        {
            PlayerPrefs.SetFloat("SquirrelDeadZone",sliderValue); 
        }

    }
}
