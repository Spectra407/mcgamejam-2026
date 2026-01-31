using UnityEngine;

[CreateAssetMenu(fileName = "AnimalData", menuName = "Scriptable Objects/AnimalData")]
public class AnimalData : ScriptableObject
{
    public string speciesName;
    public float baseStrength;
    public BiomeType strongBiome;
    public BiomeType weakBiome;
    public Sprite queueIcon;
    public Sprite gameVisual;
}
