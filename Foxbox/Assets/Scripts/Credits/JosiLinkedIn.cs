using UnityEngine;
using System.Collections;

public class JosiLinkedIn : MonoBehaviour
{
    public void OpenURL()
    {
        Application.OpenURL("https://www.linkedin.com/in/jost-hoff-410810198/");
        Debug.Log("is this working?");
    }

}
