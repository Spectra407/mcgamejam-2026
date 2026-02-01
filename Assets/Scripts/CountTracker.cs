using UnityEngine;
using System.Collections.Generic;

public class CountTracker : MonoBehaviour
{
    public static CountTracker Instance;
    
    public Dictionary<string, int> animalCount = new Dictionary<string, int>();

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        foreach (GameObject animal in AnimalPool.instance.allAnimals)
        {
            AnimalAI ai = animal.GetComponent<AnimalAI>();
            if (ai != null && ai.data != null)
            {
                if (!animalCount.ContainsKey(ai.data.speciesName))
                {
                    animalCount.Add(ai.data.speciesName, 0);
                }
            }
        }
    }

    public void IncrementCount(string speciesName)
    {
        animalCount[speciesName] += 1;
    }

    public void DecrementCount(string speciesName)
    {
        animalCount[speciesName] -= 1;
    }

    public List<int> GetPopulationReport()
    {
        List<int> countList = new List<int>();
        foreach (GameObject prefab in AnimalPool.instance.availablePool)
        {
            AnimalAI ai = prefab.GetComponent<AnimalAI>();
            string speciesName = ai.data.speciesName;
            countList.Add(animalCount[speciesName]);
        }
        return countList;
    }

    public int GetTotalPredatorCount()
    {
        int total = 0;
        foreach (int count in GetPopulationReport())
        {
            total += count;
        }
        return total;
    }

    public int GetLowestPredatorCount()
    {
        List<int> counts = GetPopulationReport();
        int min = counts[0];
        foreach (int count in counts)
        {
            if (count < min) min = count;
        }
        return min;
    }

    public int getSpeciesPopulationCount(string speciesName) {
        return  animalCount[speciesName];
    }
}
