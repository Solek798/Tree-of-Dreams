using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedFlute : MonoBehaviour, ITool
{
    [SerializeField] private float maxPlantDistance = 60.0f;
    [SerializeField] private AudioSource audioPlayer;
    [SerializeField] private AudioClip audioCou;
    [SerializeField] private AudioClip audioCur;
    [SerializeField] private AudioClip audioHap;
    [SerializeField] private AudioClip audioLov;
    [SerializeField] private AudioClip audioTru;

    private FarmlandSpace _space;

    public PopupCropUi cropUi;

    public IEnumerator Use(FarmlandSpace space)
    {
        _space = space;
        
        var inventory = GetComponent<InventoryItem>().Inventory;
        
        if (inventory != null)
            cropUi.OpenUiMenu(inventory);

        yield return true;
    }

    public bool IsUsable(FarmlandSpace space)
    {
        return space.IsSoil && space.Plant == null;
    }

    public void Plant(GameObject plant)
    {
        _space.Plant = Instantiate(plant, _space.transform, false).GetComponent<PlantState>();  
        _space.seedFluteParticle.Play();

        switch (_space.Plant.plantObject.Name)
        {
            case PlantScriptableObject.CropType.Courage:
                audioPlayer.clip = audioCou;
                break;
            case PlantScriptableObject.CropType.Curiosity:
                audioPlayer.clip = audioCur;
                break;
            case PlantScriptableObject.CropType.Happiness:
                audioPlayer.clip = audioHap;
                break;
            case PlantScriptableObject.CropType.Love:
                audioPlayer.clip = audioLov;
                break;
            case PlantScriptableObject.CropType.Trust:
                audioPlayer.clip = audioTru;
                break;
        }


        audioPlayer.Play();

        _space.Plant.animator.Play("PlantingAnimation");
        _space.animator.Play("PlantingAnimation");
    }

    public float MaxUsingDistance => maxPlantDistance;
}
