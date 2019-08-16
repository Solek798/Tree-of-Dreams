using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;


public class FarmlandLevel : MonoBehaviour
{
    [SerializeField] private GameObject farmlandSpacePrefab = null;
    [SerializeField] private Tilemap tilemap = null;
    [SerializeField] private GameObject cellSelector = null;
    [SerializeField] private GameObject ground = null;
    private Dictionary<Vector3Int, GameObject> _register;
    private Grid _grid;
    
    
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

    public void ChangeSelector(bool active, Vector3 position = default(Vector3))
    {
        var cell = _grid.WorldToCell(position);
        
        if (active && tilemap.HasTile(cell))
        {
            var newPosition = _grid.GetCellCenterWorld(cell);
            newPosition.y = 0;
            
            cellSelector.transform.position = newPosition;
            cellSelector.SetActive(true);
        }
        else
        {
            cellSelector.SetActive(false);
        }
    }

    public bool HitLevel(GameObject objectToVerify)
    {
        return objectToVerify == ground;
    }

    public IEnumerable<FarmlandSpace> GetAllSpaces()
    {
        return _register
            .Values
            .Select(t => t.GetComponent<FarmlandSpace>())
            .ToArray();
    }

    private void OnFarmlandSpaceDeleted(GameObject space)
    {
        var cell = _grid.WorldToCell(space.transform.position);
        _register.Remove(cell);
    }

    public Vector3 GetCellPosition(Vector3 worldPosition)
    {
        return _grid.GetCellCenterWorld(_grid.WorldToCell(worldPosition));
    }
    
}
