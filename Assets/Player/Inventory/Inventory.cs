using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Text currencyText = null;
    [SerializeField] private GameObject slotContainer = null;
    [SerializeField] private Transform handTransform = null;
    [SerializeField] private int maxCurrency = 0;
    [SerializeField] private int startCurrency = 0;

    [SerializeField] private GameObject[] startingItems;

    private List<Slot> _slots = null;

    public Transform HandTransform => handTransform;
    public int MaxCurrency => maxCurrency;

    public InventoryItem SelectedItem { get; private set; }


    public int Currency
    {
        set => currencyText.text = value <= maxCurrency ? value.ToString() : maxCurrency.ToString();
        get => Convert.ToInt32(currencyText.text);
    }
    
    private void Start()
    {
        Currency = startCurrency;

        _slots =
            slotContainer
                .GetAllChildren()
                .Select(t => t.GetComponent<Slot>())
                .OfType<Slot>()
                .ToList();

        foreach (var item in startingItems)
        {
            PickUp(Instantiate(item));
        }

        SelectAndToggleSlot(0);
    }

    private void Update()
    {
        if (!PlayerScriptor.Instance.AllowInteracting)
            return;

        int index;

        if (Input.mouseScrollDelta.y < 0)
        {
            index = _slots.IndexOf(_slots.First(t => (bool) t.GetComponent<Toggle>()?.isOn)) - 1;
            
            SelectAndToggleSlot(index);
        } 
        else if (Input.mouseScrollDelta.y > 0)
        {
            index = _slots.IndexOf(_slots.First(t => (bool) t.GetComponent<Toggle>()?.isOn)) + 1;
            
            SelectAndToggleSlot(index);
        }
        
        for (int i=0; i<=9; i++)
        {
            if (!Input.GetKeyDown(((i + 1) % 10).ToString())) continue;
            
            SelectAndToggleSlot(i);
        }
    }

    public void SelectSlot(Slot slot)
    {
        var index = _slots.IndexOf(slot);
        
        SelectedItem = _slots[index].GetComponentInChildren<Stack>()?.Peek();
    }

    public void SelectAndToggleSlot(int index)
    {
        if (index < 0 || index >= _slots.Count) return;
        
        _slots[index].GetComponent<Toggle>().isOn = true;
    }

    public bool PickUp(GameObject newObject)
    {
        var item = newObject.GetComponent<InventoryItem>();
        
        if (item == null) 
            return false;

        var fittingSlot =
            _slots
            .FirstOrDefault(t => t.Stack != null &&
                            t.Stack.Peek().Identifier == item.Identifier &&
                            !t.Stack.IsClosed);

        if (fittingSlot != null)
        {
            fittingSlot.Stack.Push(item);
            item.Store(this);
            return true;
        }
        else
        {
            if (_slots.Any(slot => slot.Put(item)))
            {
                item.Store(this);
                return true;
            }
        }
        
        return false;
    }
}
