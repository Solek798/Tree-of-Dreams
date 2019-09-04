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
    private int yRotRandomizer;
    private float yRot;

    [SerializeField] public Animator animator;

    private void Start()
    {
        yRotRandomizer = Random.Range(-30, 30);
        var plantInstance = Instantiate(plantObject.stateModel[currentState], transform.position, Quaternion.Euler(0, yRot, 0), transform);
        plantInstance.transform.LookAt(Camera.main.transform, Vector3.up);
        var rotation = plantInstance.transform.rotation.eulerAngles;
        yRot = rotation.y + yRotRandomizer;
        Debug.Log(yRot);
        Debug.Log(yRotRandomizer);

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

                Instantiate(newPlantModel, transform.position, Quaternion.Euler(0, yRot, 0), transform);
            }
        }
    }

    public bool IsReadyToHarvest()
    {
        return currentState >= plantObject.stateModel.Count - 1;
    }
}