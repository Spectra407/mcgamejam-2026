using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private GameObject mainCanvas;
    private GameObject optionsCanvas;
    
    void Start() {
        mainCanvas = GameObject.Find("Main Canvas");
        optionsCanvas = GameObject.Find("Options Canvas");
        optionsCanvas.SetActive(false);
    }
    
    // MAIN MENU
    public void playButtonOnClick() {
        SceneManager.LoadScene(1);
    }
    
    public void optionsButtonOnClick() {
        mainCanvas.SetActive(false);
        optionsCanvas.SetActive(true);
    }
    
    public void quitButtonOnClick() {
        Application.Quit();
    }
    
    // OPTIONS MENU
    public void returnButtonOnClick() {
        optionsCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }
}