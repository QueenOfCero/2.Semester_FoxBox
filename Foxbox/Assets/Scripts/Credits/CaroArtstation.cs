using UnityEngine;
using System.Collections;

public class CaroArtstation : MonoBehaviour
{
    public void OpenURL()
    {
        Application.OpenURL("https://www.artstation.com/feliecho_official");
        Debug.Log("is this working?");
    }

}
