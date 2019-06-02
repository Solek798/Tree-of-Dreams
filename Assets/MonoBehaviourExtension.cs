using UnityEngine;
using System.Linq;

public static class MonoBehaviourExtension
{
    public static void DestroyAllChildren(this MonoBehaviour mb)
    {
        foreach (var childTransform in mb.GetComponentsInChildren<Transform>())
        {
            if (childTransform != mb.transform)
                MonoBehaviour.Destroy(childTransform.gameObject);
        }
    }

    public static GameObject[] GetAllChildren(this GameObject mb)
    {
        
        return mb
            .GetComponentsInChildren<Transform>()
            .Select(t => t.gameObject)
            .ToArray();
    }
}
