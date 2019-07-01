using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;


public class FarmlandLevel : MonoBehaviour
{
    [SerializeField] private GameObject farmlandSpacePrefab;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private GameObject cellSelector = null;
    [SerializeField] private GameObject ground;
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

    public void ChangeSelector(bool active, Vector3 position = default(Vector3), float heightPadding = 0.2f)
    {
        var cell = _grid.WorldToCell(position);
        
        
        if (active && tilemap.HasTile(cell))
        {
            cellSelector.transform.position = _grid.GetCellCenterWorld(cell);
            cellSelector.transform.Translate(0, heightPadding, 0);
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
            .Select(t => t.GetComponent<FarmlandSpace>());
    }

    private void OnFarmlandSpaceDeleted(GameObject space)
    {
        var cell = _grid.WorldToCell(space.transform.position);
        _register.Remove(cell);
    }
    
}
