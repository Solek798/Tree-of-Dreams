using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FarmlandLevel : MonoBehaviour
{
    [SerializeField] private GameObject cellSelector;
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

    public Vector3 GetWorldCord(Vector3Int cell)
    {
        return GetComponent<Grid>().GetCellCenterWorld(cell);
    }

    public void SetSelector(bool active, Vector3Int cell)
    {
        cellSelector.transform.position = GetWorldCord(cell);
        // TODO(FK): hardcoded values
        cellSelector.transform.Translate(0, 0.2f, 0);
        cellSelector.SetActive(active);
    }

    public bool Validate(GameObject objectToVerify, Vector3 position, out Vector3Int result)
    {
        // Test if object is a plant
        
        var plant = objectToVerify.GetComponent<PlantState>();
        if (plant != null)
        {
            result = GetComponent<Grid>().WorldToCell(objectToVerify.transform.position);
            return true;
        }
        
        // Test if object is a cloud
        
        if (clouds.Contains(objectToVerify))
        {
            Vector3Int cell = GetComponent<Grid>().WorldToCell(position);

            if (GetComponentInChildren<Tilemap>().HasTile(cell))
            {
                result = cell;
                return true;
            }
        }
        
        result = Vector3Int.zero;
        return false;
    }

    
    
    
    
}
