using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private Sprite icon = null;
    [SerializeField] private string identifier = null;
    [SerializeField] private bool isTool = false;

    public Sprite Icon => icon;
    public string Identifier => identifier;
    public Inventory Inventory { get; private set; }
    public bool IsTool => isTool;


    public void Store(Inventory inventory)
    {
        Inventory = inventory;
        gameObject.SetActive(false);
        if (IsTool) transform.SetParent(inventory.transform, true);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void Release()
    {
        transform.SetParent(transform.root);
        gameObject.SetActive(true);
    }
}
