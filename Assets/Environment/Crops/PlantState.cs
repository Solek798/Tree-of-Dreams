using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using System.Linq;

public class PlantState : MonoBehaviour
{
    public PlantScriptableObject plantObject;
    public int currentState = 0;

    private void Start()
    {
        Instantiate(plantObject.stateModel[currentState], transform.position, Quaternion.identity, transform);
    }


    //Exchanges the current Model with the next Model in the list of the Scriptable Object
    public void UpdateCurrentState()
    {
        if (IsReadyToHarvest())
            return;

        currentState++;
        GameObject newPlantModel = plantObject.stateModel[currentState];

        this.DestroyAllChildren();
        
        foreach (var childTransform in GetComponentsInChildren<Transform>())
        {
            if (childTransform != transform)
                Destroy(childTransform.gameObject);
        }

        Instantiate(newPlantModel, transform.position, Quaternion.identity, transform);
    }

    public bool IsReadyToHarvest()
    {
        return currentState >= plantObject.stateModel.Count - 1;
    }
}