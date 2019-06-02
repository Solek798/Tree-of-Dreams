using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDropTarget
{
    
    [SerializeField] private GameObject stackPrefab;
    [SerializeField] private Transform stackParent;

    private void Start()
    {
        
    }

    /*public void OnDroppedOnTarget(GameObject dropedObject)
    {
        var origSlot = dropedObject.GetComponent<Stack>()?.Slot;

        if (origSlot)
            Swap(origSlot);
        
    }*/

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
    
    public void Swap(Slot other)
    {
        var stack = this.GetComponentInChildren<Stack>();
        var otherStack = other.GetComponentInChildren<Stack>();

        if (stack)
        {
            stack.transform.parent = other.stackParent;
            stack.transform.localPosition = Vector2.zero;
        }

        if (otherStack)
        {
            otherStack.transform.parent = this.stackParent;
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
        // TODO(FK): FFinish Drag 'n' drop mechanic
        return true;
    }
}
