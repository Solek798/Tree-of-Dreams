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

    public InventoryItem SelectedItem
    {
        get
        {
            return _slots
                .Select(t => t.gameObject.GetComponent<Toggle>())
                .FirstOrDefault(t => t.isOn)
                ?.gameObject.GetComponent<Stack>()
                .Peek();
        }
    }
    


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
        // TODO(FK): remove copy 'n' paste Code
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _slots[0].GetComponent<Toggle>().isOn = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _slots[1].GetComponent<Toggle>().isOn = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _slots[2].GetComponent<Toggle>().isOn = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _slots[3].GetComponent<Toggle>().isOn = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            _slots[4].GetComponent<Toggle>().isOn = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            _slots[5].GetComponent<Toggle>().isOn = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            _slots[6].GetComponent<Toggle>().isOn = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            _slots[7].GetComponent<Toggle>().isOn = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            _slots[8].GetComponent<Toggle>().isOn = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            _slots[9].GetComponent<Toggle>().isOn = true;
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
