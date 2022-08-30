using UnityEngine;
using System.Collections;

public class GinaLinkedIn : MonoBehaviour
{
    public void OpenURL()
    {
        Application.OpenURL("https://www.linkedin.com/in/gina-jaeschke-01a1b21bb/");
        Debug.Log("is this working?");
    }

}
