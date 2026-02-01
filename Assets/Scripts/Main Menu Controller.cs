using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private GameObject mainCanvas;
    private GameObject tutorialCanvas;
    
    void Start() {
        mainCanvas = GameObject.Find("Main Canvas");
        tutorialCanvas = GameObject.Find("Tutorial Canvas");
        tutorialCanvas.SetActive(false);
    }
    
    // MAIN MENU
    public void playButtonOnClick() {
        SceneManager.LoadScene(1);
    }
    
    public void tutorialButtonOnClick() {
        mainCanvas.SetActive(false);
        tutorialCanvas.SetActive(true);
    }
    
    public void quitButtonOnClick() {
        Application.Quit();
    }
    
    // OPTIONS MENU
    public void returnButtonOnClick() {
        tutorialCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }
}