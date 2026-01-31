using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlacingHandler : MonoBehaviour
{
    private Tilemap tilemap;
    [SerializeField] private Queue queue;
    private InputAction placeAction;

    public GameObject testAnimal;

    void Start()
    {
        tilemap = gameObject.GetComponent<Tilemap>();
        placeAction = InputSystem.actions.FindAction("Click");
    }

    void Update()
    {
        if (placeAction.WasPressedThisFrame())
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector3 worldCoords = Camera.main.ScreenToWorldPoint(mousePos);
            worldCoords.z = 0;
            Vector3Int cellCoords = tilemap.WorldToCell(worldCoords);
            
            TileBase tile = tilemap.GetTile(cellCoords);
            if (tile != null)
            {
                GameObject animal = testAnimal;
                GameObject clone = Instantiate(animal, worldCoords, Quaternion.identity);
                Debug.Log("spawned at " + clone.transform.position);
            }
        }
    }
}
