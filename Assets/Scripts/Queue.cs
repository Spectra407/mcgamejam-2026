using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Queue : MonoBehaviour

{
    public List<GameObject> animalPool;
    public List<Image> previewImages;
    private RingBuffer<GameObject> animalQueue;

    void Start()
    {
        // Fill the initial queue
        animalQueue = new(previewImages.Count);
        for (int i = 0; i < previewImages.Count; i++)
        {
            animalQueue.Enqueue(AnimalPool.instance.GetRandomAnimal());
        }

        UpdateQueueUI();
    }

    void Update()
    {
        
    }

    // Place the first animal and refill the queue
    public GameObject Dequeue()
    {
        if (animalQueue.Count() == 0) return null;

        GameObject nextAnimal = animalQueue.Dequeue();
        animalQueue.Enqueue(AnimalPool.instance.GetRandomAnimal());

        UpdateQueueUI();

        return nextAnimal;
    }

    // Update the UI to show the current queue
    void UpdateQueueUI()
    {
        for (int i = 0; i < previewImages.Count; i++)
        {
            previewImages[i].sprite = animalQueue.Get(i).GetComponent<AnimalAI>().data.queueIcon;
        }
    }
}