using UnityEngine;
using System.Collections;

public class EliasArtstation : MonoBehaviour
{
    public void OpenURL()
    {
        Application.OpenURL("https://www.artstation.com/flausch");
        Debug.Log("is this working?");
    }

}
