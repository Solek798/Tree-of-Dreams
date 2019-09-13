using UnityEngine;

public class FarmlandSpace : MonoBehaviour
{
    public ParticleSystem seedFluteParticle = null;
    public ParticleSystem bagOfStardustParticle = null;
    public ParticleSystem farmlandSpaceParticle = null;
    public ParticleSystem dreamSickleParticle = null;
    

    [SerializeField] private GameObject soil = null;
    [SerializeField] private GameObject nurturedSoil = null;
    [SerializeField] public Animator animator = null;

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
        set => value.transform.SetParent(transform, true);
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

        if ((IsSoil && Plant == null) || (!IsSoil && Lampion == null))
        {
            
            IsSoil = false;
            SendMessageUpwards("OnFarmlandSpaceDeleted", this.gameObject);
            Destroy(this.gameObject);
        }
    }
}
