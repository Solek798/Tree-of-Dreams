using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudPlow : MonoBehaviour, ITool
{
    public bool Use(FarmlandSpace space)
    {
        throw new System.NotImplementedException();
    }

    public bool IsUsable(FarmlandSpace space, Vector3 usagePoint)
    {
        throw new System.NotImplementedException();
    }

    public float MaxUsingDistance { get; }
}
