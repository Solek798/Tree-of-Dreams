using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    [SerializeField] private bool isFulFilled = false;
    private List<QuestDisplay> _displays = new List<QuestDisplay>();

    public bool IsFulFilled => isFulFilled;

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
        foreach (var display in _displays)
        {
            if (display is ProgressDisplay progressDisplay)
            {
                progressDisplay.IsFulfilled = true;
                continue;
            }
            Destroy(display.gameObject);
        }

        isFulFilled = true;
    }

    public void OnSlotChanged(RequirementSlot slot)
    {
        foreach (var display in _displays)
        {
            display.UpdateSlot(slot);
        }
    }
}
