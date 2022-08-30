using UnityEngine;
using System.Collections;

public class GinaArtstation : MonoBehaviour
{
    public void OpenURL()
    {
        Application.OpenURL("https://www.artstation.com/ginimew");
        Debug.Log("is this working?");
    }

}
