using UnityEngine;

public class Animal : MonoBehaviour
{
    // SOUND
    // public sound spawnSound (idk what object type this is)
    // public sound dieSound
    
    // LOGIC
    public string animalCategory; // fodder or predator only
    public string animalType;
    public string strongIn; // also home biome of fodder animals
    public string weakIn; // also target biome of fodder animals
    public int currentStrength;
    public GameObject currentTile;
    
    // RECORDS
    public static int numOfTotalAnimals;
    
    
    public void Init(GameObject tile, string category, string type, string weakness, string strength) {
        currentTile = tile;
        animalCategory = category;
        animalType = type;
        weakIn = weakness;
        strongIn = strength;
        numOfTotalAnimals++;
    }
    
    // kills this animal if its lower strength than the attacker
    public void interact(Animal attacker) {
        if (attacker.currentStrength > currentStrength) {
            numOfTotalAnimals--;
            Destroy(this);
        }
    }

    private void wander() {
        // makes the animal move around randomly
        // need to see how the tile system works to implement
    }
    /*
    private void updateTile() {
        // checks the tile the object is on based on its transform location and other transform location
        // need to see how the tile system works to implement
        
        // update the tile if it's a fodder animal
        if (animalCategory == "fodder") {
            if (weakIn == currentTile.biome) {
                currentTile.biome = strongIn;
            }
        }
    }
    */
    
    /*
    // 0 = fodder, 1 = weak, 2 = normal, 3 = strong
    private void updateStrength()
    {
        if (animalCategory == "fodder") currentStrength = 0; return;
        if (currentTile.biome == strongIn) currentStrength = 3;
        else if (currentTile.biome == weakIn) currentStrength = 1;
        else currentStrength = 2;
    }
    */
    
    void Start()
    {
        // set location to current tile
        // need to see how the tile system works to implement
    }
    
    /*
    void Update()
    {
        wander();
        updateTile();
        updateStrength();
    }
    */
}
