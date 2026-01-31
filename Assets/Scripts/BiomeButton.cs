using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class BiomeButton : MonoBehaviour
{
    public PaintingHandler paintingHandler;
    public BiomeType type;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        paintingHandler = GameObject.Find("Tilemap").GetComponent<PaintingHandler>();
        gameObject.GetComponent<Button>().onClick.AddListener(
            () => paintingHandler.SetSelectedBiome(type)
        );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
