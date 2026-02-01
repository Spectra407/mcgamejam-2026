using UnityEngine;
using UnityEngine.Video;

public class DiceStartFix : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        VideoPlayer vp = GetComponent<VideoPlayer>();
        vp.Play();
        vp.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
