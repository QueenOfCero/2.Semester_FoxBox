using UnityEngine;
using System.Collections;

public class GinaInstagram : MonoBehaviour
{
    public void OpenURL()
    {
        Application.OpenURL("https://www.instagram.com/ginimew/");
        Debug.Log("is this working?");
    }

}
