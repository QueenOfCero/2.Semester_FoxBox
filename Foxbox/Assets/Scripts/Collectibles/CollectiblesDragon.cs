using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesDragon : MonoBehaviour
{

    public GameObject MainCollectibles;
    public GameObject animal;
    public ParticleSystem ps;
    public GameObject alert;


    public float timeRemaining = 3;
    public float currentTimeRemaining;
    public bool timerIsRunning;

    // Start is called before the first frame update
    void Start()
    {
        animal.SetActive(true);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Fox" || collision.tag == "Squirrel")
        {

            if (!MainCollectibles.GetComponent<MainCollectibles>().isCollectedDragon)
            {
                MainCollectibles.GetComponent<MainCollectibles>().Collectibles("Dragon");

                ps.Play();

                alert.SetActive(true);

                timerIsRunning = true;
                currentTimeRemaining = timeRemaining;
            }

        }

    }

    void Update()
    {

        if (timerIsRunning)
        {

            if (currentTimeRemaining > 0)
            {
                currentTimeRemaining -= Time.deltaTime; return;
            }
            else
            {
                ps.Stop();
                timerIsRunning = false;
                alert.SetActive(false);
            }
        }
    }
}
