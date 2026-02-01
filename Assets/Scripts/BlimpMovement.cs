using UnityEngine;
using System.Collections;

public class BlimpMovement : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float delaySeconds = 60f; // 1 minute delay
    public float killSeconds = 70f;
    private bool shouldMove = false;
    
        
    private AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        // Start the timer as soon as the blimp exists
        StartCoroutine(StartMovingAfterDelay());
        Destroy(this.gameObject, killSeconds);

    }

    IEnumerator StartMovingAfterDelay()
    {
        // Wait for exactly 60 seconds
        yield return new WaitForSeconds(delaySeconds);
        shouldMove = true;
        audio.Play();
    }
    
    void Update()
    {
        if (shouldMove)
        {
            // Move the blimp to the right constantly
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        
    }
}
