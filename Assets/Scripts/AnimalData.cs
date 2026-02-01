using UnityEngine;

[CreateAssetMenu(fileName = "AnimalData", menuName = "Scriptable Objects/AnimalData")]
public class AnimalData : ScriptableObject
{
    public string speciesName;
    public float baseStrength;
    public BiomeType strongBiome;
    public BiomeType weakBiome;
    public Sprite normalIcon;
    public Sprite queueIcon;
    public Sprite strongIcon;
    public Sprite weakIcon;
}
