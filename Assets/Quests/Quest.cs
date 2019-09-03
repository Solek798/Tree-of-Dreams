using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    private List<QuestDisplay> _displays = new List<QuestDisplay>();
    
    public QuestData Data { get; private set; }

    public void Initialize(QuestData questData)
    {
        Data = questData;
    }

    public void AddDisplay(QuestDisplay newDisplay)
    {
        _displays.Add(newDisplay);
    }

    public void MarkAsFulfilled()
    {
        // TODO(FK): fill with correct content
        foreach (var display in _displays)
        {
            Destroy(display.gameObject);
        }
    }

    public void OnSlotChanged(RequirementSlot slot)
    {
        foreach (var display in _displays)
        {
            display.UpdateSlot(slot);
        }
    }
}
