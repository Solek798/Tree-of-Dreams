using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FarmlandLevel : MonoBehaviour
{
    private HashSet<Vector3Int> _lockedCells;
    public List<GameObject> clouds;
    
    // Start is called before the first frame update
    void Start()
    {
        _lockedCells = new HashSet<Vector3Int>();
    }

    public void LockCell(Vector3Int cellToLock)
    {
        _lockedCells.Add(cellToLock);
    }

    public void UnlockCell(Vector3Int cellToUnlock)
    {
        _lockedCells.Remove(cellToUnlock);
    }

    public bool IsLocked(Vector3Int cell)
    {
        return _lockedCells.Contains(cell);
    }

    public bool Validate(GameObject cloud, Vector3 position, out Vector3Int result)
    {
        if (clouds.Contains(cloud))
        {
            var grid = GetComponent<Grid>();
            Vector3Int cell = grid.WorldToCell(position);

            if (GetComponentInChildren<Tilemap>().HasTile(cell) && !IsLocked(cell))
            {
                result = cell;
                return true;
            }
        }
        
        result = Vector3Int.zero;
        return false;
    }

    public void Plant(GameObject plant, Vector3Int cell)
    {
        var grid = GetComponent<Grid>();
        Vector3 position = grid.GetCellCenterWorld(cell);
        
        LockCell(cell);

        Instantiate(plant, position, grid.transform.rotation);
    }
    
    
    
}
