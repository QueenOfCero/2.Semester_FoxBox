using UnityEngine;
using System.Collections;

public class JohanSpotify : MonoBehaviour
{
    public void OpenURL()
    {
        Application.OpenURL("https://open.spotify.com/artist/65Dm2Mnmf8o9aNrmzJjIJR?si=pUfX42DlSNi2C2-wT1eRKg&dl_branch=1");
        Debug.Log("is this working?");
    }

}
