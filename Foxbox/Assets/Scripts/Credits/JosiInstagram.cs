using UnityEngine;
using System.Collections;

public class JosiInstagram : MonoBehaviour
{
    public void OpenURL()
    {
        Application.OpenURL("https://www.instagram.com/josiweltenlicht/");
        Debug.Log("is this working?");
    }

}
