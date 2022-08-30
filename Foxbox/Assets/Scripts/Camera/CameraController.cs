using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public GameObject Camera;
   // public int currentCameraIndex;

    // Use this for initialization
    void Start()
    {
        //currentCameraIndex = 0;

        ////Turn all cameras off, except the first default one
        //for (int i = 1; i < cameras.Length; i++)
        //{
        //    cameras[i].gameObject.SetActive(false);
        //}

        ////If any cameras were added to the controller, enable the first one
        //if (cameras.Length > 0)
        //{
        //    cameras[0].gameObject.SetActive(true);
        //    Debug.Log("Camera with name: " + GetComponent<Camera>() + ", is now enabled");
        //}
    }

    // Update is called once per frame
    public void ChangeCamera(Vector3 position)
    {
        Camera.transform.position = position;

    }
}
