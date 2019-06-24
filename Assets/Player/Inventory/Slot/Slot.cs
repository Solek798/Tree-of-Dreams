using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDropTarget
{
    
    [SerializeField] private GameObject stackPrefab;
    [SerializeField] private Transform stackParent;

    public Stack Stack => GetComponentInChildren<Stack>();
    
    private void Start()
    {
        
    }

    public bool Put(InventoryItem item)
    {
        var stack = GetComponentInChildren<Stack>();
        
        if (!stack)
        {
            stack = CreateAndAttacheStack();
            // TODO(FK): Wait a Frame?
            stack.Initialize();
        }

        return stack.Push(item);
    }
    
    public void Swap(Stack otherStack)
    {
        var stack = GetComponentInChildren<Stack>();
        
        if (stack)
        {
            stack.transform.parent = otherStack.Slot.stackParent;
            stack.Slot = otherStack.Slot;
            stack.transform.localPosition = Vector2.zero;
        }

        if (otherStack)
        {
            otherStack.transform.parent = this.stackParent;
            otherStack.Slot = this;
            otherStack.transform.localPosition = Vector2.zero;
        }
    }

    private Stack CreateAndAttacheStack()
    {
        var newStack = Instantiate(stackPrefab, stackParent);
        newStack.transform.SetAsFirstSibling();
        
        return newStack.GetComponent<Stack>();
    }

    public bool Handle(GameObject draggable)
    {
        // TODO(FK): Finish Drag 'n' drop mechanic
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
