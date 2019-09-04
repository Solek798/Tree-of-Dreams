using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDropTarget
{
    
    [SerializeField] private GameObject stackPrefab = null;
    [SerializeField] private Transform stackParent = null;

    public Stack Stack => GetComponentInChildren<Stack>();

    public bool Put(InventoryItem item)
    {
        var stack = GetComponentInChildren<Stack>();
        
        if (!stack)
        {
            stack = CreateAndAttacheStack();
            stack.Initialize();
        }

        return stack.Push(item);
    }
    
    public void Swap(Stack otherStack)
    {
        var stack = GetComponentInChildren<Stack>();
        
        if (stack)
        {
            stack.transform.SetParent(otherStack.Slot.stackParent);
            stack.transform.SetAsLastSibling();
            stack.Slot = otherStack.Slot;
            stack.transform.localPosition = Vector2.zero;
        }

        if (otherStack)
        {
            otherStack.transform.SetParent(this.stackParent);
            otherStack.transform.SetAsLastSibling();
            otherStack.Slot = this;
            otherStack.transform.localPosition = Vector2.zero;
        }

        GetComponent<Toggle>().isOn = true;
    }

    private Stack CreateAndAttacheStack()
    {
        var newStack = Instantiate(stackPrefab, stackParent);
        newStack.transform.SetAsLastSibling();
        
        return newStack.GetComponent<Stack>();
    }

    public bool Handle(GameObject draggable)
    {
        var otherStack = draggable.GetComponent<Stack>();

        if (otherStack)
        {
            Swap(otherStack);
            return true;
        }

        return false;
    }

    public void ToggleFirstItem()
    {
        var stack = GetComponentInChildren<Stack>();
        var toggle = GetComponent<Toggle>();
        
        stack?.Peek().gameObject.SetActive(toggle.isOn);
    }
    
    
}
