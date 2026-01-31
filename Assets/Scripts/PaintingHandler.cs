using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PaintingHandler : MonoBehaviour
{
    private Tilemap tilemap;
    private InputAction paintAction;

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
            
            TileBase tile = tilemap.GetTile(cellCoords);
            if (tile != null && tile.name != selectedBiome.name)
            {
                tilemap.SetTile(cellCoords, selectedBiome);
            }
        }
    }
}
