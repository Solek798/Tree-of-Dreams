using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedFlute : MonoBehaviour, ITool
{
    public bool Use()
    {
        Plant();
    }

    public bool IsUsable(FarmlandSpace space)
    {
        throw new System.NotImplementedException();
    }
    
    
    private void Plant(FarmlandLevel level, GameObject plant, Vector3Int cell)
    {

        Vector3 position = level.GetWorldCord(cell);
        
        level.LockCell(cell);

        var newPlant = Instantiate(plant, position, level.transform.rotation);
        
    }
}
