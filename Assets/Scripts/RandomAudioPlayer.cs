using UnityEngine;
using System.Collections;

public class AnimalSounds : MonoBehaviour
{
    private AudioSource _audioSource;
    public AudioClip[] clips; 
    
    public float minWait = 10f; 
    public float maxWait = 30f;

    void Start()
    {
        
        _audioSource = GetComponent<AudioSource>();

        // Safety check
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }

        StartCoroutine(PlayRoutine());
    }

    IEnumerator PlayRoutine()
    {
        while (true) 
        {
            yield return new WaitForSeconds(Random.Range(minWait, maxWait));

            if (clips.Length > 0 && _audioSource != null)
            {
                int index = Random.Range(0, clips.Length);
                
                _audioSource.clip = clips[index];
                _audioSource.Play();
            }
        }
    }
}