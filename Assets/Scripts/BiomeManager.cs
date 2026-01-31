using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class BiomeManager : MonoBehaviour
{
    // Fields
    public List<BiomeData> allBiomes;   // List of all BiomeData assets, make new Biomes through BiomeData prefabs.
    public Tilemap biomeTilemap;
    
    // Methods
    public BiomeType GetBiomeAtPosition(Vector3 worldPos)
    {
        Vector3Int cellPos = biomeTilemap.WorldToCell(worldPos);
        TileBase tileAtCell = biomeTilemap.GetTile(cellPos);

        if (tileAtCell == null)
        {
            Debug.Log("Error: There is no tile here!");
        }
        // Figure out how to get tile data
        foreach (var biome in allBiomes)
        {
            if (biome.tileVisual == tileAtCell)
            {
                return biome.type;
            }
        }
        
        Debug.Log("Error: no biomes match, desert has been given as the dfault value!");
        
        return BiomeType.Desert;
    }
}
