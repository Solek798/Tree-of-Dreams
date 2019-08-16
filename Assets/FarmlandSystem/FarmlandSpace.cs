using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmlandSpace : MonoBehaviour
{
    public ParticleSystem seedFluteParticle;
    public ParticleSystem bagOfStardustParticle;
    public ParticleSystem farmlandSpaceParticle;
    public ParticleSystem dreamSickleParticle;
    

    [SerializeField] private GameObject soil = null;
    [SerializeField] private GameObject nurturedSoil = null;
    
    
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
            farmlandSpaceParticle.Stop();
        }
    }

    public Lampion Lampion
    {
        get => GetComponentInChildren<Lampion>();
        set => value.transform.SetParent(transform);
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
        
        if ((IsSoil && Plant == null) || !IsSoil)
        {
            IsSoil = false;
            SendMessageUpwards("OnFarmlandSpaceDeleted", this.gameObject);
            Destroy(this.gameObject);
            return;
        }
    }
}
