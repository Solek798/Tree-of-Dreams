using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using System.Linq;

public class PlantState : MonoBehaviour
{
    public PlantScriptableObject plantObject;
    public int currentState = 1;
    private int ageOfState = 0;
    private int yRotation;

    [SerializeField] public Animator animator;

    private void Start()
    {
        yRotation = Random.Range(0, 360);
        Debug.Log(yRotation);
        Instantiate(plantObject.stateModel[currentState], transform.position, Quaternion.Euler(0, yRotation, 0), transform);
        transform.Rotate(0, yRotation, 0);
    }


    //Exchanges the current Model with the next Model in the list of the Scriptable Object
    public void UpdateCurrentState()
    {
        if (IsReadyToHarvest())
        {
            return;
        }
        else
        {
            ageOfState++;
            if (ageOfState == plantObject.stateReachTime[currentState])
            {
                currentState++;
                GameObject newPlantModel = plantObject.stateModel[currentState];

                this.DestroyAllChildren();

                foreach (var childTransform in GetComponentsInChildren<Transform>())
                {
                    if (childTransform != transform)
                        Destroy(childTransform.gameObject);
                }

                Instantiate(newPlantModel, transform.position, Quaternion.Euler(0, yRotation ,0), transform);
            }
        }
    }

    public bool IsReadyToHarvest()
    {
        return currentState >= plantObject.stateModel.Count - 1;
    }
}