using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


using System.Linq;

public class FarmlandLevel : MonoBehaviour
{
    [SerializeField] private GameObject cellSelector = null;
    [SerializeField] private GameObject cloudContainer;
    private HashSet<Vector3Int> _lockedCells;
    private List<GameObject> _clouds;
    
    
    void Start()
    {
        _clouds =
            cloudContainer
                .GetComponentsInChildren<Transform>()
                .Select(t => t.gameObject)
                .ToList();

        /*_clouds = new List<GameObject>();
        foreach(Transform cloud in cloudContainer.GetComponentInChildren<Transform>())
        {
            if (cloud.gameObject != cloudContainer)
            {
                _clouds.Add(cloud.gameObject);
            }
        }*/
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
        
        if (_clouds.Contains(objectToVerify))
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
