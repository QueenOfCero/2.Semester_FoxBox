using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadArea : MonoBehaviour
{
    new public string name;


    public AudioManager audioManager;




    public void OnTriggerEnter2D(Collider2D collision)


    {

        //Debug.Log("shousdfgsld2");


        if (collision.gameObject.layer == 9)
        {
            Debug.Log("shousdfgsld2");
            StartLoading(name);
        }
    }



    public void StartLoading(string name)
    {
        // Use a coroutine to load the Scene in the background
        StartCoroutine(LoadYourAsyncScene(name));

    }

    IEnumerator LoadYourAsyncScene(string name)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name);
        Debug.Log("Scene " + name + " loaded.");
        audioManager.OnSceneLoad(name);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            Debug.Log("Loading Scene " + name);
            yield return null;
        }
    }
}
