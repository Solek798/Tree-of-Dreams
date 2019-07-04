using UnityEngine;
using System.Linq;

public static class GameObjectExtension
{
    public static GameObject[] GetAllChildren(this GameObject mb)
    {
        
        return mb
            .GetComponentsInChildren<Transform>()
            .Select(t => t.gameObject)
            .ToArray();
    }
}