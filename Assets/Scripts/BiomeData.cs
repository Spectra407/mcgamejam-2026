using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

[System.Serializable] public enum BiomeType
{
    Desert, Plains, Forest, Wetland, Snow
}

[CreateAssetMenu(fileName = "BiomeData", menuName = "Scriptable Objects/BiomeData")]
public class BiomeData : ScriptableObject
{
    public BiomeType type;
    public List<TileBase> tiles;

    public BiomeData(BiomeType type, List<TileBase> tiles)
    {
        this.type = type;
        this.tiles = tiles;
    }
}
