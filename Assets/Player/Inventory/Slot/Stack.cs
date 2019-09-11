using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stack : MonoBehaviour
{
    [SerializeField] private int maxStackCount = 9999;
    [SerializeField] private bool isClosed = false;
    [SerializeField] private Text count = null;
    [SerializeField] private Image countBG = null;
    [SerializeField] private Image icon = null;
    
    private Stack<InventoryItem> _items = null;

    public Slot Slot { get; set; }
    public bool IsClosed => isClosed;
    public int Count => count.text == string.Empty ? 1 : Convert.ToInt32(count.text);
    
    // Make sure Selector is visible
    private void OnDrop()
    {
        transform.SetAsFirstSibling();
    }

    public void Initialize()
    {
        _items = new Stack<InventoryItem>();
        Slot = GetComponentInParent<Slot>();
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
        
        count.text = _items.Count.ToString();
        countBG.gameObject.SetActive(_items.Count > 1);
        
        if (icon.sprite == null)
        {
            icon.sprite = _items.Peek().Icon;
            icon.color = Color.white;
        }
    }
}
