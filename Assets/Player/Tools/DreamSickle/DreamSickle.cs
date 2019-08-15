using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamSickle : MonoBehaviour, ITool
{
    [SerializeField] private float maxHarvestDistance = 60.0f;
    
    
    public bool Use(FarmlandSpace space)
    {
        var plant = space.Plant;
        space.Plant = null;
        GetComponent<InventoryItem>().Inventory.PickUp(plant.gameObject);
        space.dreamSickleParticle.Play();

        return true;
    }

    public bool IsUsable(FarmlandSpace space)
    {
        var plant = space.Plant;
        
        return plant != null && plant.IsReadyToHarvest();
    }

    public float MaxUsingDistance => maxHarvestDistance;
}
