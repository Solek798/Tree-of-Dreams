using UnityEngine;
using UnityEngine.UI;

public class ProgressDisplay : QuestDisplay
{
    [SerializeField] private Text fulfillText = null;
    [SerializeField] private bool isFulfilled = false;

    public bool IsFulfilled
    {
        get => isFulfilled;
        set
        {
            if (value) fulfillText.text = _quest.Data.fullfillText;
            isFulfilled = value;
        }
    }
    
    public override void Initialize(Quest quest)
    {
        fulfillText.text = quest.Data.missingText;
        
        base.Initialize(quest);
    }
}