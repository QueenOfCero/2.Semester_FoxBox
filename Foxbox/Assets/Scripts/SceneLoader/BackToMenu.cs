using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackToMenu : MonoBehaviour
{
    [SerializeField] 
    int sceneIndex = 0;

    //bool SquirrelIn = false;
    bool FoxIn = false;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Fox")
        {
            FoxIn = true;
        }

        if (other.tag == "Squirrel")
        {
            //SquirrelIn = true;
        }
        /*if (collision.gameObject.layer == 9) 
        {
            SceneManager.LoadScene("MainMenu");
        }*/
        if (FoxIn)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Fox")
        {
            FoxIn = false;
        }

        if (other.tag == "Squirrel")
        {
            //SquirrelIn = false;
        }
    }
}
