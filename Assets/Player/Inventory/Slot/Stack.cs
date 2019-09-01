using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Stack : MonoBehaviour
{
    [SerializeField] private int maxStackCount = 9999;
    [SerializeField] private bool isClosed = false;
    
    private Stack<InventoryItem> _items;
    private Text _count;
    private Image _icon;
    

    public Slot Slot { get; set; }
    public bool IsClosed => isClosed;
    public int Count => _count.text == string.Empty ? 1 : Convert.ToInt32(_count.text);
    
    // Make sure Selector is visible
    private void OnDrop()
    {
        transform.SetAsFirstSibling();
    }

    public void Initialize()
    {
        _items = new Stack<InventoryItem>();
        Slot = GetComponentInParent<Slot>();
        _count = GetComponentInChildren<Text>();
        _icon = GetComponentInChildren<Image>();
    }

    public InventoryItem Peek()
    {
        return _items.Peek();
    }
    
    public bool Push(InventoryItem item)
    {
        
        if ((_items.Count > 0 && _items.Peek().Identifier != item.Identifier) || isClosed)
            return false;
        
        _items.Push(item);
        item.transform.SetParent(transform);

        if (_items.Count >= maxStackCount || item.IsTool)
            isClosed = true;
        
        UpdateInfo();
        
        return true;
    }
    
    public InventoryItem Pop()
    {
        
        var retVal = _items.Pop();
        UpdateInfo();
        return retVal;
    }

    public InventoryItem[] PopAll()
    {
        var retVal = _items.ToArray();
        _items.Clear();
        
        UpdateInfo();

        return retVal;
    }

    private void UpdateInfo()
    {
        if (_items.Count == 0)
        {
            Destroy(this.gameObject);
            return;
        }
        
        _count.text = _items.Count > 1 ? _items.Count.ToString() : "";
        if (_icon.sprite == null)
        {
            _icon.sprite = _items.Peek().Icon;
            _icon.color = Color.white;
        }
    }
}
