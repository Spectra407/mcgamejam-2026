using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class updateAnimalScore : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = CountTracker.Instance?.getSpeciesPopulationCount(this.GameObject().name).ToString();
    }
}
