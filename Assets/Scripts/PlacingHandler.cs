using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlacingHandler : MonoBehaviour
{
    private Tilemap tilemap;
    private BiomeManager biomeManager;
    [SerializeField] private Queue queue;
    private InputAction placeAction;

    public float spawnInterval = 30f;
    public int animalsPerSpawn = 5;

    private List<Vector3Int> innerTiles = new List<Vector3Int>(); // valid spawn tiles
    private HashSet<Vector3Int> occupiedTiles = new HashSet<Vector3Int>(); // tiles with animals

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        biomeManager = GetComponent<BiomeManager>();
        placeAction = InputSystem.actions.FindAction("Click");

        Timer.instance.AddIntervalAction(spawnInterval, RunAutoSpawn, 0);

        PrecomputeInnerTiles(1); // border = 1
    }

    void Update()
    {
        if (placeAction.WasPressedThisFrame())
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector3 worldCoords = Camera.main.ScreenToWorldPoint(mousePos);
            worldCoords.z = 0;
            Vector3Int cellCoords = tilemap.WorldToCell(worldCoords);

            TileBase tile = tilemap.GetTile(cellCoords);
            if (tile != null && !occupiedTiles.Contains(cellCoords))
            {
                GameObject animal = queue.Dequeue();
                SpawnAnimalAt(animal, worldCoords, cellCoords);
            }
        }
    }

    private void RunAutoSpawn()
    {
        int spawned = 0;
        int attempts = 0;

        while (spawned < animalsPerSpawn && attempts < 50) // prevent infinite loop
        {
            attempts++;

            if (innerTiles.Count == 0) return;

            Vector3Int cell = innerTiles[Random.Range(0, innerTiles.Count)];

            if (!occupiedTiles.Contains(cell))
            {
                Vector3 spawnPos = tilemap.GetCellCenterWorld(cell);
                
                BiomeType biomeType = biomeManager.GetBiomeAtPosition(spawnPos);
                GameObject animal = AnimalPool.instance.GetRandomBiomeAnimal(biomeType);
                SpawnAnimalAt(animal, spawnPos, cell);
                spawned++;
            }
        }
    }

    private void SpawnAnimalAt(GameObject animal, Vector3 position, Vector3Int cell)
    {
        GameObject clone = Instantiate(animal, position, Quaternion.identity);
        Debug.Log("spawned at " + clone.transform.position);
        occupiedTiles.Add(cell); // mark tile as occupied
        
        // Get the AnimalAI component of the prefab.
        AnimalAI ai = animal.GetComponent<AnimalAI>();
        CountTracker.Instance?.IncrementCount(ai.data.speciesName);
    }

    // to make sure the animals dont spawn on the outer tile 
    private void PrecomputeInnerTiles(int border)
    {
        BoundsInt bounds = tilemap.cellBounds;

        int minX = int.MaxValue, maxX = int.MinValue;
        int minY = int.MaxValue, maxY = int.MinValue;

        // Find actual occupied edges
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int cell = new Vector3Int(x, y, 0);
                if (tilemap.GetTile(cell) != null)
                {
                    if (x < minX) minX = x;
                    if (x > maxX) maxX = x;
                    if (y < minY) minY = y;
                    if (y > maxY) maxY = y;
                }
            }
        }

        innerTiles.Clear();

        // Only tiles strictly inside the outer edges minus border
        for (int x = minX + border; x <= maxX - border; x++)
        {
            for (int y = minY + border; y <= maxY - border; y++)
            {
                Vector3Int cell = new Vector3Int(x, y, 0);
                if (tilemap.GetTile(cell) != null)
                {
                    innerTiles.Add(cell);
                }
            }
        }

        
    }
}
