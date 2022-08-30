using UnityEngine;
using System.Collections;

public class JohanWebsite : MonoBehaviour
{
    public void OpenURL()
    {
        Application.OpenURL("https://jrkaos.com/");
        Debug.Log("is this working?");
    }

}
