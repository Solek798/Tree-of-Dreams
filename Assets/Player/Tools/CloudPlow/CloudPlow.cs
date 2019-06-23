using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudPlow : MonoBehaviour, ITool
{
    [SerializeField] private float maxPlowDistance = 60.0f;
    
    public bool Use(FarmlandSpace space)
    {
        Debug.Log("Plowing");
        return space.IsSoil = true;
    }

    public bool IsUsable(FarmlandSpace space, Vector3 usagePoint)
    {
        Debug.Log(space.transform.position);
        Debug.Log(usagePoint);
        return (space.transform.position - usagePoint).sqrMagnitude <= maxPlowDistance;
    }
}
