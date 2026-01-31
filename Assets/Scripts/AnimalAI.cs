using UnityEditor;
using UnityEngine;

public class AnimalAI : MonoBehaviour
{
    
    
    private BiomeManager biomeManager;
    
    public AnimalData data;

    void Start()
    {
        biomeManager = FindAnyObjectByType<BiomeManager>();
        
    }

    public float CurrentStrength
    {
        get
        {
            BiomeType currentTileType = biomeManager.GetBiomeAtPosition(transform.position);
            if (currentTileType == data.strongBiome)
            {
                return data.baseStrength * 2f;
            }

            else if (currentTileType == data.weakBiome)
            {
                return data.baseStrength * 0.5f;
            }
            else
            {
                return data.baseStrength;
            }
        }
    }
    
    public float CurrentStrengthTest
    {
        get
        {
            return data.baseStrength;
        }
        
    }
    
    public void Interact(AnimalAI attacker)
    {
        float attackerStrength = attacker.CurrentStrengthTest;
        float myStrength = this.CurrentStrengthTest;
        if (attackerStrength > myStrength) {
            // numOfTotalAnimals--;
            this.Die();

        }
    }

    public void Die()
    {
        // ADD LATER: increment the score tracker and animal tracker
        Debug.Log("DIED");
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D attacker)
    {
        Debug.Log("COLLISION");
        AnimalAI attackerAnimal = attacker.GetComponent<AnimalAI>();
        if (attackerAnimal != null)
        {
            Interact(attackerAnimal);
        }
    }
    
}
