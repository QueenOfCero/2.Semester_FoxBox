using UnityEngine;
using System.Collections;

public class BalLinkedIn : MonoBehaviour
{
    public void OpenURL()
    {
        Application.OpenURL("https://www.linkedin.com/in/balthazar-lindsay-188b021ba/");
        Debug.Log("is this working?");
    }

}
