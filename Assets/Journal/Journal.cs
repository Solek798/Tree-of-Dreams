using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Journal : MonoBehaviour
{
    
    [SerializeField] private GameObject layoutGroup = null;

    [SerializeField] private GameObject requirementsPanel = null;

    [SerializeField] private GameObject questPanel = null;

    [SerializeField] private Scrollbar slider = null;

    [SerializeField] private Canvas journalCanvas = null;
    
    private bool _uiOpened;

    private void Start()
    {
        CloseJournal();
        slider.value = 1;
    }

    
    private static void Parent( GameObject parentOb, GameObject childOb )
    {
        childOb.transform.SetParent(parentOb.transform, false);
        childOb.transform.localScale = new Vector3(1, 1, 1);
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape)  && _uiOpened == false)
        {
            OpenJournal();
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && _uiOpened == true)
        {
            CloseJournal();
        }
        
    }

    public void QuestAddedToJournal(Quest quest)
    {
        var newQuest = Instantiate(questPanel);
        Parent(layoutGroup, newQuest);
        newQuest.GetComponent<JournalQuestPanel>().npcIcon.GetComponent<Image>().sprite = quest.questNPCImage;

        requirementsPanel.GetComponent<RequirementsPanel>().InitializePanel(quest.requirements);
        foreach (var value in quest.requirements)
        {
            var panelVariant = Instantiate(requirementsPanel);
            var reqLayoutGroup = newQuest.GetComponentInChildren<HorizontalLayoutGroup>().gameObject;
            Parent(reqLayoutGroup,panelVariant);
            panelVariant.GetComponent<RequirementsPanel>().InitializePanel(quest.requirements);
        }
        
    }

    private void OpenJournal()
    {
        journalCanvas.enabled = true;
        slider.value = 1;

        _uiOpened = true;
    }
    
    
    private void CloseJournal()
    {
        journalCanvas.enabled = false;
        _uiOpened = false;
    }
    
    public void OnExitButtonClick()
    {
        _uiOpened = false;
        journalCanvas.enabled = false;
    }

}
