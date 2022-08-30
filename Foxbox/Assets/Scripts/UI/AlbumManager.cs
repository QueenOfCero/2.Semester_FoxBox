using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class AlbumManager : MonoBehaviour
{
    public bool isCollectedBear;
    public bool isCollectedSnake;
    public bool isCollectedDragon;
    public bool isCollectedRobin;

    public GameObject UIFrame;
    public GameObject UIFrame2;

    public GameObject HoneyBear;
    public GameObject Snake;

    public GameObject Badger;
    public GameObject Bird;

    public GameObject Unknown1;
    public GameObject Unknown2;

    private void Awake()
    {
        loadCollectibles();
    }

    void Start()
    {
        BookButton(true);
    }

    private void BookButton(bool wert)
    {
        if (wert)
        {
            HoneyBear.SetActive(isCollectedBear);
            Unknown1.SetActive(!isCollectedBear);

            Snake.SetActive(isCollectedSnake);
            Unknown2.SetActive(!isCollectedSnake);

            Badger.SetActive(!wert);
            Bird.SetActive(!wert);
        }
        else
        {
            Badger.SetActive(isCollectedDragon);
            Unknown1.SetActive(!isCollectedDragon);

            Bird.SetActive(isCollectedRobin);
            Unknown2.SetActive(!isCollectedRobin);

            HoneyBear.SetActive(wert);
            Snake.SetActive(wert);
        }

        UIFrame2.SetActive(!wert);
        UIFrame.SetActive(wert);
    }

    public void loadCollectibles()
    {
        try
        {
            for (int i = 0; i <= 4; i++)
            {
                string animalName = name(i);

                if (animalName != "" && File.Exists(Application.persistentDataPath + animalName))
                {
                    BinaryFormatter b = new BinaryFormatter();
                    FileStream f = File.Open(Application.persistentDataPath + animalName, FileMode.Open);
                    save s = (save)b.Deserialize(f);

                    isCollectedBear = s.saveBear;
                    isCollectedSnake = s.saveSnake;
                    isCollectedDragon = s.saveDragon;
                    isCollectedRobin = s.saveRobin;
                }
            }
        }
        catch (IOException)
        {

        }
    }

    new private string  name(int i)
    {
        string animal = "";

        if (i == 1)
        {
            animal = "bear.sv";
        }
        if (i == 2)
        {
            animal = "snake.sv";
        }
        if (i == 3)
        {
            animal = "dragon.sv";
        }
        if (i == 4)
        {
            animal = "robin.sv";
        }
        return animal;
    }
}
