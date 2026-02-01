using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public Image tintImage;
    public RectTransform dialogueBox;
    public TextMeshProUGUI text;
    public MainMenuController mainMenuController;
    public List<Sprite> tints;
    public List<List<string>> dialogues;
    private InputAction forwardAction;

    private bool tutorialRunning;
    private int step;
    private int substep;

    void Start()
    {
        forwardAction = InputSystem.actions.FindAction("Click");
        tutorialRunning = false;

        dialogues = new()
        {
            new()
            {
                "Welcome to Ecoside, Inc.! You'll be helping us replenish the ecosystem with *Unnatural Selection!*",
            },
            new()
            {
                "Look at this terrain -- not a single animal in sight! We need to fix that.",
            },
            new()
            {
                "Your queue shows you what animals are coming up. Place the next animal on the terrain with Left Click!",
                "If you want some completely new animals, you can *Roll The Dice!* Gambling is so exciting!~",
            },
            new()
            {
                "Sometimes animals have trouble adapting quickly enough to their environment...",
                "In this case, it's best to adapt the environment to the animal!",
                "This is your biome palette. You can paint the terrain with Right Click!",
                "Animals like certain biomes more than others. Use biomes to control the balance!",
            },
            new()
            {
                "This is your score! Get a high enough score and you might just earn yourself a raise!"
            },
            new()
            {
                "Well, I'll leave you to it! We'll check in in 5 minutes, okay?"
            },
        };
    }

    void Update()
    {
        if (tutorialRunning && forwardAction.WasPressedThisFrame())
        {
            substep++;

            if (substep >= dialogues[step].Count)
            {
                substep = 0;
                step++;

                if (step >= tints.Count)
                {
                    tutorialRunning = false;
                    mainMenuController.returnButtonOnClick();
                }
            }

            UpdateView();
        }
    }

    private void UpdateView()
    {
        tintImage.sprite = tints[step];
        text.text = dialogues[step][substep];
    }

    public void StartTutorial()
    {
        tutorialRunning = true;
        step = 0;
        substep = 0;
        UpdateView();
    }
}
