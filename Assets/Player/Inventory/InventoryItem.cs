using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private Sprite icon = null;
    [SerializeField] private string identifier = null;

    public Sprite Icon => icon;
    public string Identifier => identifier;
    public Inventory Inventory { get; private set; }


    public void Store(Inventory inventory)
    {
        Inventory = inventory;
        gameObject.SetActive(false);
        transform.SetParent(inventory.HandTransform, true);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void Release()
    {
        transform.SetParent(transform.root);
        gameObject.SetActive(true);
    }
}
