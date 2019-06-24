using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


using System.Linq;

public class FarmlandLevel : MonoBehaviour
{
    [SerializeField] private GameObject farmlandSpacePrefab;
    private Grid _grid;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private GameObject cellSelector = null;
    [SerializeField] private GameObject ground;
    [SerializeField] private int range = 500;
    private Dictionary<Vector3Int, GameObject> _register;
    
    
    private void Start()
    {
        _register = new Dictionary<Vector3Int, GameObject>();
        _grid = GetComponent<Grid>();
    }

    public FarmlandSpace Interact()
    {
        var cell = _grid.WorldToCell(cellSelector.transform.position);

        if (_register.ContainsKey(cell)) 
            return _register[cell].GetComponent<FarmlandSpace>();
        
        
        var space = Instantiate(farmlandSpacePrefab, transform, false);
        space.transform.position = cellSelector.transform.position;
        _register.Add(cell, space);

        return space.GetComponent<FarmlandSpace>();
    }

    public Vector3 GetWorldCord(Vector3Int cell)
    {
        return _grid.GetCellCenterWorld(cell);
    }

    public void ChangeSelector(bool active, Vector3 position = default(Vector3))
    {
        var cell = _grid.WorldToCell(position);
        
        
        if (active && tilemap.HasTile(cell))
        {
            cellSelector.transform.position = GetWorldCord(cell);
            // TODO(FK): hardcoded values
            cellSelector.transform.Translate(0, 0.2f, 0);
            cellSelector.SetActive(true);
        }
        else
        {
            cellSelector.SetActive(false);
        }
    }

    public bool HitLevel(GameObject objectToVerify)
    {
        /*// Test if object is a plant
        
        var plant = objectToVerify.GetComponent<PlantState>();
        if (plant != null)
        {
            result = GetComponent<Grid>().WorldToCell(objectToVerify.transform.position);
            return true;
        }
        
        // Test if object is a cloud
        
        if ()
        {
            
        }*/
        
        return objectToVerify == ground;
    }

    public FarmlandSpace[] GetAllSpaces()
    {
        return _register
            .Values
            .Select(t => t.GetComponent<FarmlandSpace>())
            .ToArray();
    }
    
    
    
}
