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
    // Check if you actually assigned the UI images
    if (previewImages == null || previewImages.Count == 0)
    {
        Debug.LogError("STOP! You haven't assigned any Preview Images in the Inspector on the Queue script.");
        return;
    }

    animalQueue = new RingBuffer<GameObject>(previewImages.Count);
    
    // Fill the initial queue
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

    // 1. Grab the current animal (the one the player sees in slot 1)
    GameObject nextAnimal = animalQueue.Dequeue();

    // 2. Add a new one to the end of the line
    animalQueue.Enqueue(AnimalPool.instance.GetRandomAnimal());

    // 3. REFRESH THE UI
    // This shifts all images so the player sees the "new" slot 1
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

public void RandomizeQueue()
{
    // debugg
    if (AnimalPool.instance == null || AnimalPool.instance.availablePool.Count == 0) {
        Debug.LogWarning("Pool not ready yet!");
        return;
    }

    // for debugginggg Safety check for the UI list
    if (previewImages.Count == 0) {
        Debug.LogError("You forgot to drag the 4 images into the Inspector!");
        return;
    }

    // 3. Re-initialize or Clear the buffer to the correct size
    if (animalQueue == null) animalQueue = new(previewImages.Count);
    animalQueue.Clear();

    // 4. Fill the 4 slots
    for (int i = 0; i < previewImages.Count; i++)
    {
        animalQueue.Enqueue(AnimalPool.instance.GetRandomAnimal());
    }

    UpdateQueueUI();

    

    //for debugging
    Debug.Log($"Queue Refreshed! Slots filled: {animalQueue.Count()}");
}

 

}