using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlacingHandler : MonoBehaviour
{
    private Tilemap tilemap;
    [SerializeField] private Queue queue;
    private InputAction placeAction;

    private float spawnTimer = 0f;
    private float spawnInterval = 30f;
    private int animalsPerSpawn = 5;

    private List<Vector3Int> innerTiles = new List<Vector3Int>(); // valid spawn tiles
    private HashSet<Vector3Int> occupiedTiles = new HashSet<Vector3Int>(); // tiles with animals

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        placeAction = InputSystem.actions.FindAction("Click");

        PrecomputeInnerTiles(1); // border = 1
    }

    void Update()
    {
        HandleManualPlacement();
        HandleAutoSpawn();
    }

    private void HandleManualPlacement()
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
                SpawnAnimalAt(worldCoords, cellCoords);
            }
        }
    }

    private void HandleAutoSpawn()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;

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
                    SpawnAnimalAt(spawnPos, cell);
                    spawned++;
                }
            }
        }
    }

    private void SpawnAnimalAt(Vector3 position, Vector3Int cell)
    {
        GameObject animal = queue.Dequeue();
        GameObject clone = Instantiate(animal, position, Quaternion.identity);
        Debug.Log("spawned at " + clone.transform.position);
        occupiedTiles.Add(cell); // mark tile as occupied
        
        // Get the AnimalAI component of the prefab.
        AnimalAI ai = testAnimal.GetComponent<AnimalAI>();
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
