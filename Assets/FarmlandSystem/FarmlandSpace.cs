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
        set => value?.transform.SetParent(transform, true);
    }

    public void UpdateState()
    {
        if (IsNurtured)
        {
            IsSoil = true;
            Plant?.UpdateCurrentState();
            return;
        }
        
        if (IsSoil && Plant == null)
        {
            IsSoil = false;
            SendMessageUpwards("OnFarmlandSpaceDeleted", this.gameObject);
            Destroy(this.gameObject);
            return;
        }
    }
}
