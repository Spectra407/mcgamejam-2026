using UnityEngine;

public class CreateAnimal : MonoBehaviour
{
    
    public Animal animalPrefab;
    
    // WETLANDS ANIMALS
    public Animal createBeaver(GameObject tile) {
        Animal a = Instantiate(animalPrefab);
        a.Init(tile, "fodder", "beaver", "forest", "wetlands");
        return a;
    }
    
    public Animal createCrocodile(GameObject tile) {
        Animal a = Instantiate(animalPrefab);
        a.Init(tile, "predator", "crocodile", "desert", "wetlands");
        return a;
    }
    
    // PLAINS ANIMALS
        
    // FOREST ANIMALS
    
    // SNOWY ANIMALS
    
    // DESERT ANIMALS
    
    
    void Start() {
        
    }
    
    void Update() {
        
    }
}
