using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class testScript : MonoBehaviour
{
    [SerializeField] private TileBase tile;
    private Tilemap _tilemap;
    // Start is called before the first frame update
    void Start()
    {
        _tilemap = GetComponentInChildren<Tilemap>();
        var p = _tilemap.cellBounds.position;
        var s = _tilemap.cellBounds.size;
        _tilemap.BoxFill(Vector3Int.zero, tile, p.x, p.y, s.x, s.y);
        Debug.Log(_tilemap.cellBounds);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
