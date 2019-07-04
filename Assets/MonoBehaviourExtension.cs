using UnityEngine;

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
}
