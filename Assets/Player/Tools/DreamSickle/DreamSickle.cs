using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamSickle : MonoBehaviour, ITool
{
    [SerializeField] private float maxHarvestDistance = 60.0f;
    [SerializeField] private AudioClip DreamSickleSfx;
    [SerializeField] private AudioSource DreamSicklePlayer;


    public bool Use(FarmlandSpace space)
    {
        var plant = space.Plant;
        space.Plant = null;
        GetComponent<InventoryItem>().Inventory.PickUp(plant.gameObject);
        DreamSicklePlayer.clip = DreamSickleSfx;
        DreamSicklePlayer.Play();
        return true;
    }

    public bool IsUsable(FarmlandSpace space, Vector3 usagePoint)
    {
        var plant = space.Plant;       
        return (space.transform.position - usagePoint).sqrMagnitude <= maxHarvestDistance &&
               plant != null &&
               plant.IsReadyToHarvest();
    }

    public float MaxUsingDistance => maxHarvestDistance;
}
