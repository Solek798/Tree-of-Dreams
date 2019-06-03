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

    /*public InventoryItem SelectedItem => _slots
        .Select(t => t.gameObject.GetComponent<InventoryItem>())
        .FirstOrDefault(t => t.gameObject.GetComponent<Toggle>()?.isOn);*/


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
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            print(KeyCode.Alpha3.ToString());
        }
    }

    public bool PickUp(GameObject newObject)
    {
        var item = newObject.GetComponent<InventoryItem>();
        
        if (item == null) 
            return false;

        // TODO(FK): Convert to Linq?
        foreach (var slot in _slots)
        {
            if (slot.Put(item))
            {
                item.Store(this);
                return true;
            }
        }
        
        return false;
    }
}
