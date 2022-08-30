using UnityEngine;
using System.Collections;

public class EmelyLinkedIn : MonoBehaviour
{
    public void OpenURL()
    {
        Application.OpenURL("https://www.linkedin.com/in/emely-drawert-487643216/");
        Debug.Log("is this working?");
    }

}
