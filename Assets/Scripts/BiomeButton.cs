using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

// Allows for switching current biome for painting. most of this stuff (the getComponent stuff) is for toggling the arrow
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
            GameObject.Find("Arrow Plains").GetComponent<Image>().enabled = false;
            GameObject.Find("Arrow Forest").GetComponent<Image>().enabled = false;
            GameObject.Find("Arrow Desert").GetComponent<Image>().enabled = false;
            GameObject.Find("Arrow Snow").GetComponent<Image>().enabled = false;
            GameObject.Find("Arrow Wetland").GetComponent<Image>().enabled = false;
            // enable arrow of this type
            Debug.Log(type.ToString());
            GameObject.Find("Arrow " + type.ToString()).GetComponent<Image>().enabled = true;
        });
        // disable all arrows aside from desert
        GameObject.Find("Arrow Plains").GetComponent<Image>().enabled = false;
        GameObject.Find("Arrow Forest").GetComponent<Image>().enabled = false;
        GameObject.Find("Arrow Desert").GetComponent<Image>().enabled = true;
        GameObject.Find("Arrow Snow").GetComponent<Image>().enabled = false;
        GameObject.Find("Arrow Wetland").GetComponent<Image>().enabled = false;
    }
}
