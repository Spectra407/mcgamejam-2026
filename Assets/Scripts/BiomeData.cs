using UnityEngine;
using UnityEngine.Tilemaps;

public enum BiomeType
{
    Desert, Plains, Forest, Wetland, Snow
}

[CreateAssetMenu(fileName = "BiomeData", menuName = "Scriptable Objects/BiomeData")]
public class BiomeData : ScriptableObject
{
    public string biomeName;
    public BiomeTile tileVisual;
    public BiomeType type;
}
