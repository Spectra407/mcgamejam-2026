using UnityEngine;
using System.Collections.Generic;

public class AnimalPool : MonoBehaviour
{
    public static AnimalPool instance;
    public List<GameObject> allAnimals;
    private List<GameObject> animalPool;
    private List<GameObject> unavailablePool;

    public int numStartingAnimals;
    public int expansionInterval;
    public int increasePerExpansion;

    private float lastTime;

    void Awake()
    {
        if (instance != null) Destroy(this);
        instance = this;

        animalPool = new();
        unavailablePool = new(allAnimals);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Timer.instance.AddIntervalAction(() => ExpandPool(increasePerExpansion), expansionInterval);
        ExpandPool(numStartingAnimals);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetRandomAnimal()
    {
        return animalPool[Random.Range(0, animalPool.Count)];
    }

    public void ExpandPool(int numToAdd)
    {
        for (int i = 0; i < numToAdd; i++)
        {
            int index = Random.Range(0, unavailablePool.Count);
            animalPool.Add(unavailablePool[index]);
            unavailablePool.RemoveAt(index);
        }
    }
}
