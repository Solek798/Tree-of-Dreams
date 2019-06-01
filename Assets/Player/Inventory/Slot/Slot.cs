using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private Stack<InventoryItem> _items;
    private Info _info;


    private void Start()
    {
        _items = new Stack<InventoryItem>();
        _info = GetComponentInChildren<Info>();
    }
    
    public void Swap(Slot other)
    {
        var otherStack = other._items;
        other._items = this._items;
        this._items = otherStack;
    }

    public bool Push(InventoryItem item)
    {
        if (_items.Count > 0 && _items.Peek().Identifier != item.Identifier) 
            return false;
        
        _items.Push(item);
        
        _info.Refresh(_items.Count, item.Icon);
        
        return true;
    }
    
    public InventoryItem Pop()
    {
        var retVal = _items.Pop();
        _info.Refresh(_items.Count);
        return retVal;
    }

    public InventoryItem[] PopAll()
    {
        var retVal = _items.ToArray();
        _items.Clear();
        
        _info.Refresh(_items.Count);

        return retVal;
    }
    
}
