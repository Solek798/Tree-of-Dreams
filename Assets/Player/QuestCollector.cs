using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCollector : MonoBehaviour
{
    [SerializeField] private DreamPostOffice dreamPostOffice = null;
    [SerializeField] private Journal journal = null;
    [SerializeField] private GameObject questDisplayPrefab = null;
    [SerializeField] private GameObject fullfillableQuestDisplayPrefab = null;
    
    public void AddNewQuest(Quest quest)
    {
        quest.transform.SetParent(transform);

        var newDisplay = Instantiate(questDisplayPrefab).GetComponent<QuestDisplay>();
        quest.AddDisplay(newDisplay);
        newDisplay.Initialize(quest);
        journal.AddDisplay(newDisplay);
        
        newDisplay = Instantiate(fullfillableQuestDisplayPrefab).GetComponent<QuestDisplay>();
        quest.AddDisplay(newDisplay);
        newDisplay.Initialize(quest);
        dreamPostOffice.AddDisplay(newDisplay);
    }
}
