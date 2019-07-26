using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Serialization;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Text currencyText = null;
    [SerializeField] private GameObject slotContainer = null;
    [SerializeField] private Transform handTransform = null;
    [SerializeField] private int maxCurrency = 0;
    [SerializeField] private int startCurrency = 0;

    [SerializeField] private GameObject cloudPlowPrefab = null;
    [SerializeField] private GameObject stardustBagPrefab = null;
    [SerializeField] private GameObject seedFlutePrefab = null;
    [SerializeField] private GameObject dreamSicklePrefab = null;
    

    private Slot[] _slots = null;

    public Transform HandTransform => handTransform;
    public int MaxCurrency => maxCurrency;

    public InventoryItem SelectedItem { get; private set; }


    public int Currency
    {
        set => currencyText.text = value <= maxCurrency ? value.ToString() : maxCurrency.ToString();
        get => Convert.ToInt32(currencyText.text);
    }
    
    void Start()
    {
        Currency = startCurrency;
        
        _slots =
            slotContainer
                .GetAllChildren()
                .Select(t => t.GetComponent<Slot>())
                .OfType<Slot>()
                .ToArray();

        PickUp(Instantiate(cloudPlowPrefab));
        PickUp(Instantiate(stardustBagPrefab));
        PickUp(Instantiate(seedFlutePrefab));
        PickUp(Instantiate(dreamSicklePrefab));
    }

    void Update()
    {
        for (int i=0; i<=9; i++)
        {
            if (!Input.GetKeyDown(((i + 1) % 10).ToString())) continue;
            
            _slots[i].GetComponent<Toggle>().isOn = true;
            SelectedItem = _slots[i].GetComponentInChildren<Stack>()?.Peek();
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
