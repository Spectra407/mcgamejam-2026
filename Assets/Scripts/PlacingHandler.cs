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

    public float spawnInterval = 2f;
    public int animalsPerSpawn = 1;

    private List<Vector3Int> innerTiles = new List<Vector3Int>(); // valid spawn tiles

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        biomeManager = GetComponent<BiomeManager>();
        placeAction = InputSystem.actions.FindAction("Click");
        
        
        Timer.instance.AddIntervalAction(spawnInterval, RunAutoSpawn, 0);

        PrecomputeInnerTiles(1); // border = 1
        RunAutoSpawn();  // Spawns a few fodder animals at the start
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
            if (tile != null)
            {
                GameObject animal = queue.Dequeue();
                SpawnAnimalAt(animal, worldCoords, cellCoords);
            }
        }
    }

    private void RunAutoSpawn()
    {
        if (innerTiles.Count == 0) return;
        
        // choose a random tile, and set a vector at the center
        Vector3Int cell = innerTiles[Random.Range(0, innerTiles.Count)];
        Vector3 spawnPos = tilemap.GetCellCenterWorld(cell);
        
        GameObject animal = AnimalPool.instance.GetRandomFodderAnimal();
        SpawnAnimalAt(animal, spawnPos, cell);
    }

    private void SpawnAnimalAt(GameObject animal, Vector3 position, Vector3Int cell)
    {
        GameObject clone = Instantiate(animal, position, Quaternion.identity);
        
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
