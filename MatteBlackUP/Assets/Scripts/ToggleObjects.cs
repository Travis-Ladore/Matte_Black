using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObjects : MonoBehaviour
{
    // Array to hold the objects you want to toggle
    public GameObject[] objectsToToggle;

    // Variable to track the current toggle state
    private bool isToggledOn = true;

   

    void Update()
    {
        
        // Check if the P key is pressed
        if (Input.GetKeyDown(KeyCode.P))
        {
            // Toggle the state
            isToggledOn = !isToggledOn;

            // Loop through each object in the array and set its active state
            foreach (GameObject obj in objectsToToggle)
            {
                obj.SetActive(isToggledOn);
            }
        }
    }
}
