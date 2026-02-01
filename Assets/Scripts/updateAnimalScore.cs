using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class updateAnimalScore : MonoBehaviour
{
    // this goes on the text object for the score mode that shows number of each animal. It uses CountTracker
    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = CountTracker.Instance?.getSpeciesPopulationCount(this.GameObject().name).ToString();
    }
}
