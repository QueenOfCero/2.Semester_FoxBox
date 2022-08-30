using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Easy2DPlayerMovement;

public class GateVerticalRespawn : MonoBehaviour
{
    public GameObject respawnArea;
    public GameObject respawnPoint;

    private GameObject squirrel;

    void Start()
    {

        squirrel = GameObject.Find("Squirrel");

    }

    public void OnTriggerEnter2D(Collider2D gameObject)
    {
        if (gameObject.name == "Fox" || gameObject.name == "Squirrel") {
            if (gameObject.transform.position.y > respawnArea.transform.position.y)
            {
                gameObject.transform.position = new Vector3(respawnPoint.transform.position.x, respawnPoint.transform.position.y, gameObject.transform.position.z);

                gameObject.GetComponent<MovementManager>().hasRespawned = true;
                if (gameObject.name == "Fox" && gameObject.GetComponent<MovementManager>().isPiggybacking == true)
                {
                    squirrel.GetComponent<MovementManager>().hasRespawned = true;
                }
            }
        }
    }
}
