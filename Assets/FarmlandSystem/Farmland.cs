using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Farmland : MonoBehaviour, IEnumerable
{
    
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var map in GetComponentsInChildren<Tilemap>())
        {
            map.color = Color.clear;
        }
    }
    
    public FarmlandLevel[] GetAllLevels()
    {
        return GetComponentsInChildren<FarmlandLevel>();
    }

    public IEnumerator GetEnumerator()
    {
        return GetAllLevels().GetEnumerator();
    }
}
