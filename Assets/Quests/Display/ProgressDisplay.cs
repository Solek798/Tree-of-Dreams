using UnityEngine;
using UnityEngine.UI;

public class ProgressDisplay : QuestDisplay
{
    [SerializeField] private Text fullfillText = null;
    
    public override void Initialize(Quest quest)
    {
        _quest = quest;
        npcIconUI.sprite = quest.Data.questNPCImage;
        
        
    }
}