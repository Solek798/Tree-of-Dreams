using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagOfStardust : MonoBehaviour, ITool
{
    [SerializeField] private float maxThrowDistance = 60.0f;
    
    public bool Use(FarmlandSpace space)
    {
        return space.IsNurtured = true;
    }

    public bool IsUsable(FarmlandSpace space, Vector3 usagePoint)
    {
        return (space.transform.position - usagePoint).sqrMagnitude <= maxThrowDistance &&
               space.IsSoil;
    }

    public float MaxUsingDistance => maxThrowDistance;
}
