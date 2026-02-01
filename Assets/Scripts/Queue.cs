using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Queue : MonoBehaviour
//the code works fine but if u change the preview count it does not increase the preview count for some reason
// the for loop in updateQueueUI might be the problem??
{
    // animal queue UI
    public TextMeshProUGUI animalQueueUI;

    // array of all the posible animals change this to array of objects?
    public List<GameObject> animalPool;

    // List of Images to display previews with
    public List<Image> previewImages;

    // the actual queue its a queue of strings for now
    private RingBuffer<GameObject> animalQueue;

    void Start()
    {
        // Fill the initial queue
        animalQueue = new(previewImages.Count);
        for (int i = 0; i < previewImages.Count; i++)
        {
            animalQueue.Enqueue(GetRandomAnimal());
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
        animalQueue.Enqueue(GetRandomAnimal());

        UpdateQueueUI();

        return nextAnimal;
    }

    // Get a random animal from the pool
    GameObject GetRandomAnimal()
    {
        return animalPool[Random.Range(0, animalPool.Count)];
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