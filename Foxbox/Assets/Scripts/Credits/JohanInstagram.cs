using UnityEngine;
using System.Collections;

public class JohanInstagram : MonoBehaviour
{
    public void OpenURL()
    {
        Application.OpenURL("https://www.instagram.com/jrkaos.official/");
        Debug.Log("is this working?");
    }

}
