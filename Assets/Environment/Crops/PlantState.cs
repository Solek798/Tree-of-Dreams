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
        //yRotRandomizer = Random.Range(-15, 15);
        //float x = Camera.main.transform.position.x;
        //float z = Camera.main.transform.position.z;
        //yRot = Mathf.Atan2(z, x) * Mathf.Rad2Deg;

        //if (yRot < 0)
        //{           
        //    if (z / x >= -1) // -2
        //    {
        //        yRot += 202.5f;// + yRotRandomizer;
        //    }
        //    else // -0.5
        //    {
        //        yRot += 157.5f;// + yRotRandomizer;
        //    }
        //}
        //else if (yRot <= 0) 
        //{
        //    yRot += yRotRandomizer;
        //    if (z / x <= 1) // 0,5
        //    {
        //        yRot += -22.5f + yRotRandomizer;
        //    }
        //    else // 2
        //    {
        //        yRot += 22.5f + yRotRandomizer;
        //    }
        //}
        //if (x <= 0 && z <= 0)
        //{
        //    yRot += ;
        //}
        //else if (x >= 0 && z >= 0)
        //{
        //   yRot += ;
        //}
        //else if (x > 0 && z < 0)
        //{
        //    yRot += 180;
        //}
        //else if (x < 0 && z > 0)
        //{
        //    yRot += 180;  //yRotRandomizer;
        //}

        //Debug.Log("X position " + x);
        //Debug.Log("Z position " + z);
        //Debug.Log(yRot);
        //Debug.Log(yRotRandomizer);
        var plantInstance = Instantiate(plantObject.stateModel[currentState], transform.position, Quaternion.Euler(0, yRot, 0), transform);
        //yRot += 180 - yRotRandomizer; 
        plantInstance.transform.LookAt(Camera.main.transform, Vector3.up);

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

                var plantInstance = Instantiate(newPlantModel, transform.position, Quaternion.Euler(0, yRot, 0), transform);
                plantInstance.transform.LookAt(Camera.main.transform, Vector3.up);
            }
        }
    }

    public bool IsReadyToHarvest()
    {
        return currentState >= plantObject.stateModel.Count - 1;
    }
}