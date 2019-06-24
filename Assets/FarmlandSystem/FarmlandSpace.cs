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
        set
        {
            if (value) IsNurtured = false;
            soil.SetActive(value);
        }
    }

    public bool IsNurtured
    {
        get => nurturedSoil.activeInHierarchy;
        set
        {
            if (value) IsSoil = false;
            nurturedSoil.SetActive(value);
        }
    }

    public PlantState Plant
    {
        get => GetComponentInChildren<PlantState>();
        set => value.transform.SetParent(transform, true);
    }

    private void UpdateState()
    {
        // TODO(FK)
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
