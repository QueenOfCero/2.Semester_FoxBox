using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateVertical : MonoBehaviour
{
    public GameObject ScreenGateTop;
    //public GameObject ScreenGateArea;
    private GameObject fox;
    private GameObject squirrel;

    public bool FoxIn = false;
    public bool SquirrelIn = false;
    void Start()
    {
        fox = GameObject.Find("Fox");
        squirrel = GameObject.Find("Squirrel");
    }

    private void Update()
    {
        if (!FoxIn || !SquirrelIn)
        {
            if ((fox.transform.position.y > ScreenGateTop.transform.position.y) && (squirrel.transform.position.y > ScreenGateTop.transform.position.y))
            {
                ScreenGateTop.SetActive(false);

            }
            if (fox.transform.position.y < ScreenGateTop.transform.position.y)
            {
                ScreenGateTop.SetActive(true);

            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Fox")
        {
            CharacterCheck("Fox", true);
        }

        if (other.tag == "Squirrel")
        {
            CharacterCheck("Squirrel", true);
        }

        if (FoxIn && SquirrelIn)
        {
            ScreenGateTop.SetActive(false);
            Debug.Log("Test");
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Fox")
        {
            CharacterCheck("Fox", false);
        }

        if (other.tag == "Squirrel")
        {
            CharacterCheck("Squirrel", false);
        }
    }

    private void CharacterCheck(string character, bool wert)
    {
        if (character == "Fox")
        {
            FoxIn = wert;
            Debug.Log(FoxIn);

        } else if (character == "Squirrel")
        {
            SquirrelIn = wert;
           // Debug.Log(SquirrelIn);
        }
    }
}
