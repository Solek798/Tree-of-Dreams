using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Farmland : MonoBehaviour, IEnumerable
{
    
    private void Start()
    {
        foreach (var map in GetComponentsInChildren<Tilemap>())
        {
            map.color = Color.clear;
        }
    }

    public IEnumerator GetEnumerator()
    {
        return GetComponentsInChildren<FarmlandLevel>().GetEnumerator();
    }

    public FarmlandLevel[] GetAllLevels()
    {
        return GetComponentsInChildren<FarmlandLevel>().ToArray();
    }
}
