using UnityEngine;
using System.Collections.Generic;

public class AnimalPool : MonoBehaviour
{
    public static AnimalPool instance;
    public List<GameObject> allAnimals;
    public List<GameObject> availablePool;
    public List<GameObject> unavailablePool;
    public List<GameObject> fodderPool;

    public int numStartingAnimals;
    public int expansionInterval;
    public int increasePerExpansion;

    private float lastTime;

    void Awake()
    {
        if (instance != null) Destroy(this);
        instance = this;

        availablePool = new();
        unavailablePool = new(allAnimals);
        
        ExpandPool(numStartingAnimals);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Timer.instance.AddIntervalAction(expansionInterval, () => ExpandPool(increasePerExpansion), 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetRandomAnimal()
    {
        return availablePool[Random.Range(0, availablePool.Count)];
    }

    public GameObject GetRandomBiomeAnimal(BiomeType biomeType)
    {
        List<GameObject> strongAnimals = new();
        foreach (GameObject animal in fodderPool)
        {
            if (animal.GetComponent<AnimalAI>().data.strongBiome == biomeType)
            {
                strongAnimals.Add(animal);
            }
        }

        if (strongAnimals.Count > 0)
        {
            return strongAnimals[Random.Range(0, strongAnimals.Count)];
        }
        else
        {
            return GetRandomAnimal();
        }
    }

    public void ExpandPool(int n)
    {
        int numToAdd = System.Math.Min(n, unavailablePool.Count);
        for (int i = 0; i < numToAdd; i++)
        {
            int index = Random.Range(0, unavailablePool.Count);
            availablePool.Add(unavailablePool[index]);
            unavailablePool.RemoveAt(index);
        }
    }
}
