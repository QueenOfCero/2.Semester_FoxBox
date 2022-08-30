using UnityEngine;
using System.Collections;

public class JohanLinkedIn : MonoBehaviour
{
    public void OpenURL()
    {
        Application.OpenURL("https://www.linkedin.com/in/johan-rosier/");
        Debug.Log("is this working?");
    }

}
