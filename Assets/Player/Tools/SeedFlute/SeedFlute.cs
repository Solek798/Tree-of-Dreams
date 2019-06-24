using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedFlute : MonoBehaviour, ITool
{
    [SerializeField] private float maxPlantDistance = 60.0f;
    private FarmlandSpace _space;

    public PopupCropUi cropUi;

    public bool Use(FarmlandSpace space)
    {
        _space = space;

        //var ui = GetComponentInChildren<PopupCropUi>();

        //if (ui == null) return false;
        
        cropUi.OpenUiMenu();

        return true;
    }

    public bool IsUsable(FarmlandSpace space, Vector3 usagePoint)
    {
        return (space.transform.position - usagePoint).sqrMagnitude <= maxPlantDistance &&
               space.IsSoil;
    }

    public void Plant(GameObject plant)
    {
        _space.Plant = Instantiate(plant, _space.transform, false).GetComponent<PlantState>();  
    }
}
