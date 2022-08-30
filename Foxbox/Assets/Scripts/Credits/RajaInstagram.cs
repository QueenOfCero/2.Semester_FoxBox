using UnityEngine;
using System.Collections;

public class RajaInstagram : MonoBehaviour
{
    public void OpenURL()
    {
        Application.OpenURL("https://www.instagram.com/_rarainka_/");
        Debug.Log("is this working?");
    }

}
