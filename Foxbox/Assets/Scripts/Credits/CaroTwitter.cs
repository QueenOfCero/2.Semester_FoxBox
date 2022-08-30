using UnityEngine;
using System.Collections;

public class CaroTwitter : MonoBehaviour
{
    public void OpenURL()
    {
        Application.OpenURL("https://twitter.com/lavendelfrost");
        Debug.Log("is this working?");
    }

}
