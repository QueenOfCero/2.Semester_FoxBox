using UnityEngine;
using System.Collections;

public class RajaLinkedIn : MonoBehaviour
{
    public void OpenURL()
    {
        Application.OpenURL("https://www.linkedin.com/in/raja-kabierski-3a1747208/");
        Debug.Log("is this working?");
    }

}
