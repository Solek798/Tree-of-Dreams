using System.Collections;
using UnityEngine;

public class SeedFlute : MonoBehaviour, ITool
{
    [SerializeField] private float maxPlantDistance = 60.0f;
    [SerializeField] private AudioSource audioPlayer = null;
    [SerializeField] private AudioClip audioCou = null;
    [SerializeField] private AudioClip audioCur = null;
    [SerializeField] private AudioClip audioHap = null;
    [SerializeField] private AudioClip audioLov = null;
    [SerializeField] private AudioClip audioTru = null;

    private FarmlandSpace _space;

    public PopupCropUi cropUi;

    public IEnumerator Use(FarmlandSpace space)
    {
        _space = space;
        
        var inventory = GetComponent<InventoryItem>().Inventory;

        if (inventory != null)
        {
            Debug.Log(Input.mousePosition);
            cropUi.transform.position = Input.mousePosition;
            
            cropUi.OpenUiMenu(inventory);
        }

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
