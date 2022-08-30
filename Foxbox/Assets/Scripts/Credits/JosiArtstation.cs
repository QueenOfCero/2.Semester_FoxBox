using UnityEngine;
using System.Collections;

public class JosiArtstation : MonoBehaviour
{
    public void OpenURL()
    {
        Application.OpenURL("https://www.artstation.com/josiweltenlicht");
        Debug.Log("is this working?");
    }

}
