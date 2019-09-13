using UnityEngine;

public class QuestCollector : MonoBehaviour
{
    [SerializeField] private DreamPostOffice dreamPostOffice = null;
    [SerializeField] private Journal journal = null;
    [SerializeField] private SleepMenu sleepMenu = null;
    
    [SerializeField] private GameObject questDisplayPrefab = null;
    [SerializeField] private GameObject fullfillableQuestDisplayPrefab = null;
    [SerializeField] private GameObject progressDisplayPrefab = null;
    
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
        
        var newProgressDisplay = Instantiate(progressDisplayPrefab).GetComponent<ProgressDisplay>();
        quest.AddDisplay(newProgressDisplay);
        newProgressDisplay.Initialize(quest);
        sleepMenu.AddDisplay(newProgressDisplay);
    }

    public Quest[] GetAllQuests()
    {
        return GetComponentsInChildren<Quest>();
    }
}
