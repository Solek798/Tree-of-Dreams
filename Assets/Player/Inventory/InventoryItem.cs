using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private Sprite icon;
    [SerializeField] private string identifier;
    private Inventory _inventory;

    public Sprite Icon => icon;
    public string Identifier => identifier;
    public Inventory Inventory => _inventory;

    
    public void Store(Inventory inventory)
    {
        _inventory = inventory;
        gameObject.SetActive(false);
        transform.SetParent(inventory.HandTransform, true);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void Release()
    {
        transform.parent = transform.root;
        gameObject.SetActive(true);
    }
}
