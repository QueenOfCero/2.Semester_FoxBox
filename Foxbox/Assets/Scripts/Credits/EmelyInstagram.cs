using UnityEngine;
using System.Collections;

public class EmelyInstagram : MonoBehaviour
{
    public void OpenURL()
    {
        Application.OpenURL("https://www.instagram.com/_neilsart_/");
        Debug.Log("is this working?");
    }

}
