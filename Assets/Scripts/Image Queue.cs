using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class AnimalQueueManager : MonoBehaviour
{
    // prefab 
    public GameObject[] animalPrefabs;

    
    public Image previewImagePrefab;   // UI Image prefab
    public Transform previewParent;    // QueueContainer

    public int previewCount = 3;

    private Queue<GameObject> animalQueue = new Queue<GameObject>();
    private List<Image> previewImages = new List<Image>();

    void Start()
    {
        CreatePreviewSlots();

        for (int i = 0; i < previewCount; i++)
        {
            animalQueue.Enqueue(GetRandomAnimal());
        }

        UpdateQueueUI();
    }

    void Update()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            PlaceFirstAnimal();
        }
    }

    void CreatePreviewSlots()
    {
        // Clear old slots (important if previewCount changes)
        foreach (Transform child in previewParent)
            Destroy(child.gameObject);

        previewImages.Clear();

        for (int i = 0; i < previewCount; i++)
        {
            Image img = Instantiate(previewImagePrefab, previewParent);
            img.gameObject.SetActive(true);
            previewImages.Add(img);
        }
    }

    void PlaceFirstAnimal()
    {
        if (animalQueue.Count == 0) return;

        GameObject placed = animalQueue.Dequeue();
        Debug.Log("Placed: " + placed.name);

        animalQueue.Enqueue(GetRandomAnimal());
        UpdateQueueUI();
    }

    GameObject GetRandomAnimal()
    {
        int index = Random.Range(0, animalPrefabs.Length);
        return animalPrefabs[index];
    }

    void UpdateQueueUI()
    {
        GameObject[] queueArray = animalQueue.ToArray();

        for (int i = 0; i < previewImages.Count; i++)
        {
            SpriteRenderer sr = queueArray[i].GetComponent<SpriteRenderer>();

            if (sr == null)
            {
                Debug.LogError(queueArray[i].name + " has no SpriteRenderer");
                continue;
            }

            previewImages[i].sprite = sr.sprite;
        }
    }
}
