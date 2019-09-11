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
    [SerializeField] private BoundsInt randomCellBounds;
    private Dictionary<Vector3Int, GameObject> _register = null;
    private Grid _grid = null;
    
    
    private void Awake()
    {
        
        _register = new Dictionary<Vector3Int, GameObject>();
        _grid = GetComponent<Grid>();
        Debug.Log(_register);
    }

    public FarmlandSpace Interact()
    {
        var cell = _grid.WorldToCell(cellSelector.transform.position);
        
        if (_register.ContainsKey(cell)) 
            return _register[cell].GetComponent<FarmlandSpace>();

        
        return CreateSpace(cell);
    }

    private FarmlandSpace CreateSpace(Vector3Int position)
    {
        var space = Instantiate(farmlandSpacePrefab, transform, false);
        space.transform.position = _grid.GetCellCenterWorld(position);
        _register.Add(position, space);

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

    public FarmlandSpace GetRandomSpace(int maxGeneratingAttempts)
    {
        for (int i = 0 ; i <= maxGeneratingAttempts ; i++)
        {
            var position = new Vector3Int(
                Random.Range(randomCellBounds.xMin, randomCellBounds.xMax),
                Random.Range(randomCellBounds.yMin, randomCellBounds.yMax),
                Random.Range(randomCellBounds.zMin, randomCellBounds.zMax)
            );

            Debug.Log(tilemap);
            Debug.Log(_register);

            if (tilemap.HasTile(position) && !_register.ContainsKey(position))
            {
                return CreateSpace(position);
            }
        }
        
        return null;
    }
}
