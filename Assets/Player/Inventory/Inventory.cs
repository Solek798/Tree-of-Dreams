using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Serialization;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Text currencyText;
    [SerializeField] private GameObject slotContainer;
    [SerializeField] private Transform handTransform;

    [SerializeField] private GameObject cloudPlowPrefab;
    [SerializeField] private GameObject stardustBagPrefab;
    [SerializeField] private GameObject seedFlutePrefab;
    [SerializeField] private GameObject dreamSicklePrefab;

    private Slot[] _slots = null;
    private InventoryItem _selectedItem;

    public Transform HandTransform => handTransform;

    public InventoryItem SelectedItem => _selectedItem;
    


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

        PickUp(Instantiate(cloudPlowPrefab));
        PickUp(Instantiate(stardustBagPrefab));
        PickUp(Instantiate(seedFlutePrefab));
        PickUp(Instantiate(dreamSicklePrefab));
    }

    void Update()
    {
        // TODO(FK): remove copy 'n' paste Code
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _slots[0].GetComponent<Toggle>().isOn = true;
            _selectedItem = _slots[0].GetComponentInChildren<Stack>()?.Peek();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _slots[1].GetComponent<Toggle>().isOn = true;
            _selectedItem = _slots[1].GetComponentInChildren<Stack>()?.Peek();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _slots[2].GetComponent<Toggle>().isOn = true;
            _selectedItem = _slots[2].GetComponentInChildren<Stack>()?.Peek();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _slots[3].GetComponent<Toggle>().isOn = true;
            _selectedItem = _slots[3].GetComponentInChildren<Stack>()?.Peek();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            _slots[4].GetComponent<Toggle>().isOn = true;
            _selectedItem = _slots[4].GetComponentInChildren<Stack>()?.Peek();
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            _slots[5].GetComponent<Toggle>().isOn = true;
            _selectedItem = _slots[5].GetComponentInChildren<Stack>()?.Peek();
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            _slots[6].GetComponent<Toggle>().isOn = true;
            _selectedItem = _slots[6].GetComponentInChildren<Stack>()?.Peek();
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            _slots[7].GetComponent<Toggle>().isOn = true;
            _selectedItem = _slots[7].GetComponentInChildren<Stack>()?.Peek();
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            _slots[8].GetComponent<Toggle>().isOn = true;
            _selectedItem = _slots[8].GetComponentInChildren<Stack>()?.Peek();
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            _slots[9].GetComponent<Toggle>().isOn = true;
            _selectedItem = _slots[9].GetComponentInChildren<Stack>()?.Peek();
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
