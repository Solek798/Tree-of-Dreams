using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Stack : MonoBehaviour
{
    private Stack<InventoryItem> _items;
    private Text _count;
    private Image _icon;
    private Slot _slot;

    public Slot Slot => _slot;


    // Make sure Selector is visible
    private void OnDrop()
    {
        transform.SetAsFirstSibling();
    }

    public void Initialize()
    {
        _items = new Stack<InventoryItem>();
        _slot = GetComponentInParent<Slot>();
        _count = GetComponentInChildren<Text>();
        _icon = GetComponentInChildren<Image>();
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
            Destroy(this);
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
