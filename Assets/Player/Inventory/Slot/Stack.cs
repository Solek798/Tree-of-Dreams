using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Stack : MonoBehaviour
{
    private Stack<InventoryItem> _items;
    [SerializeField] private Text count;
    [SerializeField] private Image icon;
    [SerializeField] private Image countBackground;

    public Slot Slot { get; set; }

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
        
        if (_items.Count > 0 && _items.Peek().Identifier != item.Identifier) 
            return false;
        
        _items.Push(item);
        
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

        if (_items.Count > 1)
        {
            count.text = _items.Count.ToString();
            countBackground.gameObject.SetActive(true);
        }
        else
        {
            count.text = "";
            countBackground.gameObject.SetActive(false);
        }

        if (icon.sprite == null)
        {
            icon.sprite = _items.Peek().Icon;
            icon.color = Color.white;
        }
    }
}
