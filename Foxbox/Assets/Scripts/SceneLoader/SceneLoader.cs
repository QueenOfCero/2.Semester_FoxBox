using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject audioManagerObject;
   // public AudioManager audioManager;

	public void LoadSceneByIndex(int sceneBuildIndex)
	{
		SceneManager.LoadScene(sceneBuildIndex);
	}

    private void Awake()
    {
        if (GameObject.Find("AudioManager"))
        {
            audioManagerObject = GameObject.Find("AudioManager");
        }
        else
        {
            Debug.Log("AudioManager not found!");
        }
    }

    public void StartLoading(string name)
    {
        PlayerPrefs.Save();
        Debug.Log("prefs saved");
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
        audioManagerObject.GetComponent<AudioManager>().OnSceneLoad(name);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            Debug.Log("Loading Scene " + name);
            yield return null; 
        }
    }

    public void OnExitButtonClicked()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit ();
#endif
	}

}
