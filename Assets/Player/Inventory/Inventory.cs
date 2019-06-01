using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Text currencyText;
    [SerializeField] private GameObject slotContainer;
    [SerializeField] private Transform playerTransform;

    private Slot[] _slots = null;

    public Transform PlayerTransform => playerTransform;


    public int Currency
    {
        set => currencyText.text = "Currency: " + value;
        get => Convert.ToInt32(currencyText.text.Replace("Currency: ", ""));
    }
    
    void Start()
    {
        _slots =
            slotContainer
                .GetAllChildren()
                .Select(t => t.GetComponent<Slot>())
                .OfType<Slot>()
                .ToArray();
    }

    void Update()
    {
        
    }

    public bool PickUp(GameObject newObject)
    {
        var item = newObject.GetComponent<InventoryItem>();
        
        if (item == null) 
            return false;

        // TODO(FK): Convert to Linq?
        foreach (var slot in _slots)
        {
            if (slot.Push(item))
            {
                item.Store(this);
                return true;
            }
        }
        
        return false;
    }
}
