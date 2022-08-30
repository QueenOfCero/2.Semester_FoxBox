using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using TMPro;

[Serializable]
public class save
{
    public bool saveBear;
    public bool saveSnake;
    public bool saveDragon;
    public bool saveRobin;

    public int saveCounter;
}

public class MainCollectibles : MonoBehaviour
{
    public bool isCollectedBear;
    public bool isCollectedSnake;
    public bool isCollectedDragon;
    public bool isCollectedRobin;
    public TextMeshPro Text;

    public int charakterCounter;

    public string plural;

    private void Awake()
    {
        loadCollectibles();
    }

    private void Start()
    {
        //clearCollectibles();
        //isCollectedDragon = true;
        //isCollectedRobin = true;
        saveCollectibles();
    }

    public void Collectibles(string animal)
    {

        if (animal == "Bear")
        {
            isCollectedBear = true;
        }
        if (animal == "Snake")
        {
            isCollectedSnake = true;
        }
        if (animal == "Dragon")
        {
            isCollectedDragon = true;
        }
        if (animal == "Robin")
        {
            isCollectedRobin = true;
        }

        charakterCounter += 1;

        saveCollectibles();
    }

    private void saveCollectibles()
    {
        save s = new save();

        s.saveBear = isCollectedBear;
        s.saveSnake = isCollectedSnake;
        s.saveDragon = isCollectedDragon;
        s.saveRobin = isCollectedRobin;

        s.saveCounter = charakterCounter;

        for (int i = 0; i <= 5; i++)
        {
            string animalName = Name(i);

            if (animalName != "")
            {
                if (File.Exists(Application.persistentDataPath + animalName))
                {
                    File.Delete(Application.persistentDataPath + animalName);
                }

                BinaryFormatter b = new BinaryFormatter();
                FileStream f = File.Create(Application.persistentDataPath + animalName);
                b.Serialize(f, s);
                f.Close();
            }
        }
    }

    public void loadCollectibles()
    {
        try
        {
            for (int i = 0; i <= 5; i++)
            {
                string animalName = Name(i);

                if (animalName != "" && File.Exists(Application.persistentDataPath + animalName))
                {
                     BinaryFormatter b = new BinaryFormatter();
                     FileStream f = File.Open(Application.persistentDataPath + animalName, FileMode.Open);
                     save s = (save)b.Deserialize(f);

                     isCollectedBear = s.saveBear;
                     isCollectedSnake = s.saveSnake;
                     isCollectedDragon = s.saveDragon;
                     isCollectedRobin = s.saveRobin;

                     charakterCounter = s.saveCounter;
                }
            }
        }
        catch (IOException)
        {

        }
    }

    public void clearCollectibles()
    {
        isCollectedBear = false;
        isCollectedSnake = false;
        isCollectedDragon = false;
        isCollectedRobin = false;

        charakterCounter = 0;

        saveCollectibles();
    }

    private string Name(int i)
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
        if (i == 5)
        {
            animal = "conter.sv";
        }

        return animal;
    }
}
