using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlantState : MonoBehaviour
{
    public PlantScriptableObject plantObject;
    public int currentState = 0;

    private void Start()
    {
        Instantiate(plantObject.StateModel[currentState], transform.position, Quaternion.identity, transform);
    }

    private void Update()
    {
        //TODO: Implement this one to the Day n Night Cycle and remove the Button which is only for Debugging
        //Fire2 = left Alt key
        if (!Input.GetButtonDown("Fire2")) return;
        if (currentState < plantObject.StateModel.Count)
        {
            UpdateCurrentState();
        }
    }

    //Exchanges the current Model with the next Model in the list of the Scriptable Object
    private void UpdateCurrentState()
    {
        if (currentState >= plantObject.StateModel.Count - 1)
            return;
        
        currentState++;
        GameObject newPlantModel = plantObject.StateModel[currentState];
        
        
        foreach (var childTransform in GetComponentsInChildren<Transform>())
        {
            if (childTransform != transform) 
                Destroy(childTransform.gameObject);
        }
        
        GameObject PlantModel = Instantiate(newPlantModel, transform.position,
            Quaternion.identity, transform);
    }
}