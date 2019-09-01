using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamSickle : MonoBehaviour, ITool
{
    [SerializeField] private float maxHarvestDistance = 60.0f;
    
    
    public IEnumerator Use(FarmlandSpace space)
    {
        var plant = space.Plant;
        plant.animator.Play("HarvestingAnimation");
        space.dreamSickleParticle.Play();
    

        yield return new WaitForSeconds(plant.animator.GetCurrentAnimatorStateInfo(0).length - 0.5f);

        space.Plant = null;
        GetComponent<InventoryItem>().Inventory.PickUp(plant.gameObject);

    }

    public bool IsUsable(FarmlandSpace space)
    {
        var plant = space.Plant;
        
        return plant != null && plant.IsReadyToHarvest();
    }

    public float MaxUsingDistance => maxHarvestDistance;
}
