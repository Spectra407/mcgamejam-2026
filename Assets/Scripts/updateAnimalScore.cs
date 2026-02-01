using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class updateAnimalScore : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        this.GameObject().GetComponent<TextMeshPro>().text = CountTracker.Instance?.getSpeciesPopulationCount(this.GameObject().name).ToString();
    }
}
