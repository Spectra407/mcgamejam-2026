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
        gameObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            paintingHandler.SetSelectedBiome(type);
            // disable all arrows
            GameObject.Find("Arrow Plains").SetActive(false);
            GameObject.Find("Arrow Forest").SetActive(false);
            GameObject.Find("Arrow Desert").SetActive(false);
            GameObject.Find("Arrow Snow").SetActive(false);
            GameObject.Find("Arrow Wetland").SetActive(false);
            // enable arrow of this type
            Debug.Log(type.ToString());
            GameObject.Find("Arrow " + type.ToString()).SetActive(true);
        });
        
        // disable all arrows aside from desert
        GameObject.Find("Arrow Plains").SetActive(false);
        GameObject.Find("Arrow Forest").SetActive(false);
        GameObject.Find("Arrow Desert").SetActive(true);
        GameObject.Find("Arrow Snow").SetActive(false);
        GameObject.Find("Arrow Wetland").SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
