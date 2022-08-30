using UnityEngine;
using System.Collections;

public class MarcoLinkedIn : MonoBehaviour
{
    public void OpenURL()
    {
        Application.OpenURL("https://www.linkedin.com/in/marco-eberhardt-8612b2216/");
        Debug.Log("is this working?");
    }

}
