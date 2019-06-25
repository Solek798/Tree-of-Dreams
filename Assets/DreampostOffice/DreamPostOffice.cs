using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DreamPostOffice : MonoBehaviour
{
    
    [SerializeField] private GameObject layoutGroup = null;

    [SerializeField] private GameObject requirementsPanel = null;

    [SerializeField] private GameObject questPanel = null;

    [SerializeField] private GameObject dreamTree = null;

    public Scrollbar slider = null;
   
    public GameObject player;
    public float maxDistanceToPostOffice = 10f;
    private bool _uiOpened;
    
    
    private void Start()
    {
        _uiOpened = false;
        SetSliderDefaults();
    }

    
    private static void Parent( GameObject parentOb, GameObject childOb )
    {
        childOb.transform.SetParent(parentOb.transform, true);
        childOb.transform.localScale = new Vector3(1, 1, 1);
        
    }

    private void Update()
    {
        var playerInRange = Vector3.Distance(dreamTree.transform.position, player.transform.position);

        if (Input.GetKeyDown(KeyCode.E) && playerInRange <= maxDistanceToPostOffice && _uiOpened == false) 
        {
            OpenPostOfficeMenu();
            _uiOpened = true;
        }
        
    }
    
    public void QuestAddedToJournal(Quest quest)
    {
        var newQuest = Instantiate(questPanel);
        Parent(layoutGroup, newQuest);
        newQuest.GetComponent<QuestPanel>().npcIcon.GetComponent<Image>().sprite = quest.questNPCImage;
        
        requirementsPanel.GetComponent<RequirementsPanel>().InitializePanel(quest.requirements);
            
        foreach (var value in quest.requirements)
        {
            var panelVariant = Instantiate(requirementsPanel);
            var reqLayoutGroup = newQuest.GetComponentInChildren<HorizontalLayoutGroup>().gameObject;
            Parent(reqLayoutGroup,panelVariant);
            panelVariant.GetComponent<RequirementsPanel>().InitializePanel(quest.requirements);
        }
    }

    private void OpenPostOfficeMenu()
    {
        SetSliderDefaults();
        gameObject.GetComponent<Canvas>().enabled = true;
        _uiOpened = true;
    }

    private void SetSliderDefaults()
    {
        slider.value = 1;
        slider.size = 0.5f;
        slider.numberOfSteps = 0;
    }

    public void OnExitButtonClick()
    {
        _uiOpened = false;
        gameObject.GetComponent<Canvas>().enabled = false;
    }

    public void OnFulfillButtonClick()
    {
        //TODO
        
    }
}
