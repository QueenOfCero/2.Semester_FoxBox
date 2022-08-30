using UnityEngine;
using System.Collections;

public class NichoLinkedIn : MonoBehaviour
{
    public void OpenURL()
    {
        Application.OpenURL("https://www.linkedin.com/in/nicholas-taprogge-69655b1b9/");
        Debug.Log("is this working?");
    }

}
