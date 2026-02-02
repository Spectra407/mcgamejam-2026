using UnityEngine;

public class DiceStartFix : MonoBehaviour
{
    public Texture2D staticImage;
    public RenderTexture renderTexture;

    void Start()
    {
        Graphics.Blit(staticImage, renderTexture);
    }
}
