using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantStateSystem : MonoBehaviour
{
    public PlantScriptableObject PlantObject;
    public int currentState = 0;

    private void Update()
    {
        //TODO: Implement this one to the Day n Night Cycle and remove the Button which is only for Debugging
        //Fire2 = left Alt key
        if (!Input.GetButtonDown("Fire2")) return;
        if (currentState < PlantObject.StateModel.Count)
        {
            UpdateCurrentState();
        }
    }

    //Exchanges the current Model with the next Model in the list of the Scriptable Object
    private void UpdateCurrentState()
    {
        GameObject newPlantModel = PlantObject.StateModel[currentState];
        currentState += 1;

        if (transform.GetChild(0).childCount > 0)
        {
            foreach (Transform GO in transform.GetChild(0))
            {
                Destroy(GO.gameObject);
            }
        }

        GameObject PlantModel = Instantiate(newPlantModel, transform.GetChild(0).transform.position,
            Quaternion.identity, transform.GetChild(0));
    }
}