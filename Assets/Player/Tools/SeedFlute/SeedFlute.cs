﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedFlute : MonoBehaviour, ITool
{
    [SerializeField] private float maxPlantDistance = 60.0f;
    [SerializeField] private AudioClip SeedfluteSfx;
    [SerializeField] private AudioSource SeedflutePlayer;

    private FarmlandSpace _space;

    public PopupCropUi cropUi;

    public bool Use(FarmlandSpace space)
    {
        _space = space;
        
        var inventory = GetComponent<InventoryItem>().Inventory;
        Debug.Log(inventory);
        if (inventory != null)
            cropUi.OpenUiMenu(inventory);

        return true;
    }

    public bool IsUsable(FarmlandSpace space, Vector3 usagePoint)
    {
        return (space.transform.position - usagePoint).sqrMagnitude <= maxPlantDistance &&
               space.IsSoil &&
               space.Plant == null;
    }

    public void Plant(GameObject plant)
    {
        _space.Plant = Instantiate(plant, _space.transform, false).GetComponent<PlantState>();  
        _space.seedFluteParticle.Play();
        SeedflutePlayer.clip = SeedfluteSfx;
        SeedflutePlayer.Play();

    }
    
    public float MaxUsingDistance => maxPlantDistance;
}
