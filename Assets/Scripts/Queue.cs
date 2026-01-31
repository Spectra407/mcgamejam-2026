using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
public class AnimalQueueManager : MonoBehaviour
//the code works fine but if u change the preview count it does not increase the preview count for some reason
// the for loop in updateQueueUI might be the problem??
{
    // animal queue UI
    public TextMeshProUGUI animalQueueUI;

    // array of all the posible animals change this to array of objects?
    public string[] animalPool = { "croc", "horse", "Bear", "camel", "Deer" };

    // the preview count which is 3
    
    public int previewCount = 3;

    // the actual queue its a queue of strings for now
    private Queue<string> animalQueue = new Queue<string>();

    void Start()
    {
        // Fill the initial queue
        for (int i = 0; i < previewCount; i++)
        {
            animalQueue.Enqueue(GetRandomAnimal());
        }

        UpdateQueueUI();
    }

    void Update()
    {
        // Detect right-click to place the first animal

        // IGNORE if (Input.GetMouseButtonDown(1)) // right click
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            PlaceFirstAnimal();
        }

        // Update UI every frame (optional, can remove if you only want on placement)
        UpdateQueueUI();
    }

    // Place the first animal and refill the queue
    void PlaceFirstAnimal()
    {

        
        if (animalQueue.Count == 0) return;

        string placedAnimal = animalQueue.Dequeue();
        Debug.Log("Placed animal: " + placedAnimal);

        // Add a new random animal to the end
        animalQueue.Enqueue(GetRandomAnimal());
    }

    // Get a random animal from the pool
    string GetRandomAnimal()
    {
        int index = Random.Range(0, animalPool.Length);
        return animalPool[index];
    }

    // Update the UI to show the current queue
    void UpdateQueueUI()
    {
        if (animalQueueUI == null) return;
        
        string queueText = "Current Queue:\n";

        
        int i = 0;
        foreach (string animal in animalQueue)
        {
            // Highlight the first animal to indicate it can be placed
            if (i == 0)
                queueText += "> " + animal + " <\n";
            else
                queueText += animal + "\n";
            i++;
        }

        animalQueueUI.text = queueText;
    }
}