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
        //var CurrentModel = PlantObject.StateModel;
        if (!Input.GetButtonDown("Fire2")) return;
        if (currentState < PlantObject.StateModel.Count)
        {
            UpdateCurrentState();
        }
    }

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