using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] private Image stackIcon;
    private Stack<InventoryItem> _items;
    private Text _stackCount;


    private void Start()
    {
        _items = new Stack<InventoryItem>();
        _stackCount = GetComponentInChildren<Text>();
    }

    public bool Push(InventoryItem item)
    {
        if (_items.Count > 0 && _items.Peek().Identifier != item.Identifier) 
            return false;
        
        _items.Push(item);
        stackIcon.sprite = item.Icon;
        
        UpdateCount();
        
        return true;
    }

    public InventoryItem Pop()
    {
        var retVal = _items.Pop();
        UpdateCount();
        return retVal;
    }

    private void UpdateCount()
    {
        _stackCount.text = _items.Count <= 1 ? "" : _items.Count.ToString();
    }
}
