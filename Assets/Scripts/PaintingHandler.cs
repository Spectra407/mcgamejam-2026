using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

struct Biome {};

public class PaintingHandler : MonoBehaviour
{
    Tilemap tilemap;
    InputAction paintAction;

    public TileBase selectedBiome;

    void Start()
    {
        tilemap = gameObject.GetComponent<Tilemap>();
        paintAction = InputSystem.actions.FindAction("RightClick");
    }

    void Update()
    {
        if (paintAction.IsPressed())
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector3 worldCoords = Camera.main.ScreenToWorldPoint(mousePos);
            Vector3Int cellCoords = tilemap.WorldToCell(worldCoords);

            Debug.Log("Mouse: " + mousePos);
            Debug.Log("World: " + worldCoords);
            Debug.Log("Cell: " + cellCoords);
            
            if (tilemap.GetTile(cellCoords).name != selectedBiome.name)
            {
                tilemap.SetTile(cellCoords, selectedBiome);
            }
        }
    }
}
