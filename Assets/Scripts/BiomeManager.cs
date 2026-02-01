using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class BiomeManager : MonoBehaviour
{
    // Fields
    public List<BiomeData> allBiomes;
    [System.Serializable] public struct BiomeMapping
    {
        public BiomeType type;
        public TileBase tileAsset;
    }
    public List<BiomeMapping> biomeMappings;
    
    private Dictionary<BiomeType, List<TileBase>> biomeTypeToTiles;
    private Dictionary<TileBase, BiomeType> tileToBiomeType;

    private Tilemap biomeTilemap;

    void Start()
    {
        biomeTypeToTiles = new();
        tileToBiomeType = new();
        foreach (BiomeData biome in allBiomes)
        {
            biomeTypeToTiles.Add(biome.type, biome.tiles);
            foreach (TileBase tile in biome.tiles)
            {
                tileToBiomeType.Add(tile, biome.type);
            }
        }

        biomeTilemap = gameObject.GetComponent<Tilemap>();
    }
    
    // Methods
    public BiomeType GetBiomeAtPosition(Vector3 worldPos)
    {
        Vector3Int cellPos = biomeTilemap.WorldToCell(worldPos);
        TileBase tileAtCell = biomeTilemap.GetTile(cellPos);

        if (tileAtCell == null)
        {
            Debug.Log("Error: There is no tile here!");
        }
        
        return TileToBiomeType(tileAtCell);
    }

    public BiomeType TileToBiomeType(TileBase tile)
    {
        return tileToBiomeType[tile];
    }

    public List<TileBase> BiomeTypeToTiles(BiomeType type)
    {
        return biomeTypeToTiles[type];
    }

    public TileBase RandomTile(BiomeType type)
    {
        List<TileBase> tiles = biomeTypeToTiles[type];
        return tiles[Random.Range(0, tiles.Count)];
    }
    
    public TileBase GetTileAssetByType(BiomeType type)
    {
        foreach(var mapping in biomeMappings)
        {
            if (mapping.type == type)
            {
                return mapping.tileAsset;
            }
        }
        Debug.LogWarning($"No tile asset found for biometype");
        return null;
    }
    
    public BiomeType GetBiomeTypeAt(Vector3 worldPos)
    {
        Vector3Int cellPos = biomeTilemap.WorldToCell(worldPos);
        TileBase tileAtCell = biomeTilemap.GetTile(cellPos);
        foreach (var mapping in biomeMappings)
        {
            if (mapping.tileAsset == tileAtCell)
            {
                return mapping.type;
            }
        }

        return BiomeType.Plains;
    }
}
