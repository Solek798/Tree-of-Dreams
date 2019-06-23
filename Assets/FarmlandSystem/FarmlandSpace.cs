using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmlandSpace : MonoBehaviour
{
    [SerializeField] private GameObject soil;
    [SerializeField] private GameObject nurturedSoil;
    private Vector3Int _cell;

    public bool IsSoil
    {
        get => soil.activeInHierarchy || IsNurtured;
        set => soil.SetActive(value && !IsNurtured);
    }

    public bool IsNurtured
    {
        get => nurturedSoil.activeInHierarchy;
        set => nurturedSoil.SetActive(value && IsSoil);
    }

    public PlantState Plant
    {
        get => GetComponentInChildren<PlantState>();
        set => value.transform.SetParent(transform, true);
    }

    private void UpdateState()
    {
        
        if (IsNurtured)
        {
            if (Plant == null)
            {
                IsNurtured = false;
                IsSoil = true;
            }
            else
            {
                Plant.UpdateCurrentState();
            }
        }
        
        if (IsSoil && Plant == null)
        {
            IsSoil = false;
        }
    }
}
