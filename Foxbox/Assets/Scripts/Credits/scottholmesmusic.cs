using UnityEngine;
using System.Collections;

public class scottholmesmusic : MonoBehaviour
{
    public void OpenURL()
    {
        Application.OpenURL("https://scottholmesmusic.com");
        Debug.Log("is this working?");
    }

}
