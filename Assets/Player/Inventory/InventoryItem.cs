using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private Sprite icon;
    [SerializeField] private string identifier;

    public Sprite Icon => icon;
    public string Identifier => identifier;

    
    public void Store(Inventory inventory)
    {
        gameObject.SetActive(false);
        transform.parent = inventory.PlayerTransform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void Release()
    {
        transform.parent = transform.root;
        gameObject.SetActive(true);
    }
}
