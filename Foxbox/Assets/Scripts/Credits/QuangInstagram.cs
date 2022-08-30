using UnityEngine;
using System.Collections;

public class QuangInstagram : MonoBehaviour
{
    public void OpenURL()
    {
        Application.OpenURL("https://www.instagram.com/quang_anh.ngo/");
        Debug.Log("is this working?");
    }

}
