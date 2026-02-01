using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

// AI for the fodder animals. These animals are weaker, spawn randomly instead of being placed, and expand their territory as they move
public class FodderAI : MonoBehaviour
{
    
    
    private BiomeManager biomeManager;
    private SpriteRenderer spriteRenderer;
    private Tilemap tilemap;

    public float paintInterval = 0.5f;
    private float paintTimer;
    
    public AnimalData data;
    

    void Start()
    {
        biomeManager = FindAnyObjectByType<BiomeManager>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    
    void Update()
    {
        HandlePainting();
    }
    
    public void HandlePainting()
    {
        paintTimer += Time.deltaTime;
        if (paintTimer >= paintInterval)
        {
            Vector3Int cellPos = tilemap.WorldToCell(transform.position);
            
            TileBase preferredTile = biomeManager.GetTileAssetByType(data.strongBiome);
            
            if (preferredTile != null)
            {
                tilemap.SetTile(cellPos, preferredTile);
            }
            paintTimer = 0f;
        }
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
        float attackerStrength = attacker.CurrentStrength;
        float myStrength = this.CurrentStrength;
        if (attackerStrength > myStrength) {
            // numOfTotalAnimals--;
            this.Die();
        }
    }

    public void Die()
    {
        // ADD LATER: increment the score tracker and animal tracker
        CountTracker.Instance?.DecrementCount(data.speciesName);
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
