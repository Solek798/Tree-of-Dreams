﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamSickle : MonoBehaviour, ITool
{
    [SerializeField] private float maxHarvestDistance = 60.0f;
    [SerializeField] private AudioSource audioPlayer = null;
    [SerializeField] private List<AudioClip> audioClips = null;

    private bool _isUsed = false;


    public IEnumerator Use(FarmlandSpace space)
    {
        if (!_isUsed)
        {
            _isUsed = true;

            var plant = space.Plant;

            int randInt = Random.Range(0, audioClips.Count - 1);
            audioPlayer.clip = audioClips[randInt];
            audioPlayer.Play();


            plant.animator.Play("HarvestingAnimation");
            space.dreamSickleParticle.Play();


            yield return new WaitForSeconds(plant.animator.GetCurrentAnimatorStateInfo(0).length - 0.5f);

            space.Plant = null;
            GetComponent<InventoryItem>().Inventory.PickUp(plant.gameObject);

            _isUsed = false;
        }
    }

    public bool IsUsable(FarmlandSpace space)
    {
        var plant = space.Plant;
        
        return plant != null && plant.IsReadyToHarvest();
    }

    public float MaxUsingDistance => maxHarvestDistance;
}
