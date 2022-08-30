using UnityEngine;
using System.Collections;

public class CaroInstagram : MonoBehaviour
{
    public void OpenURL()
    {
        Application.OpenURL("https://www.instagram.com/feliecho_official/");
        Debug.Log("is this working?");
    }

}
