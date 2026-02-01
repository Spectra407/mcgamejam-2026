using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PaintingHandler : MonoBehaviour
{
    private Tilemap tilemap;
    private BiomeManager biomeManager;
    private InputAction paintAction;

    public BiomeType selectedBiomeType;

    void Start()
    {
        tilemap = gameObject.GetComponent<Tilemap>();
        biomeManager = gameObject.GetComponent<BiomeManager>();
        paintAction = InputSystem.actions.FindAction("RightClick");
    }

    void Update()
    {
        if (paintAction.IsPressed())
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector3 worldCoords = Camera.main.ScreenToWorldPoint(mousePos);
            Vector3Int cellCoords = tilemap.WorldToCell(worldCoords);
            
            TileBase tile = tilemap.GetTile(cellCoords);

            if (tile != null && biomeManager.TileToBiomeType(tile) != selectedBiomeType)
            {
                tilemap.SetTile(cellCoords, biomeManager.RandomTile(selectedBiomeType));
            }
        }
    }

    public void SetTile(Vector3 worldCoords, BiomeType biome)
    {
        Vector3Int cellCoords = tilemap.WorldToCell(worldCoords);
        
        TileBase tile = tilemap.GetTile(cellCoords);

        if (tile != null && biomeManager.TileToBiomeType(tile) != biome)
        {
            tilemap.SetTile(cellCoords, biomeManager.RandomTile(biome));
        }
    }

    public void SetSelectedBiome(BiomeType biome)
    {
        selectedBiomeType = biome;
    }
}
