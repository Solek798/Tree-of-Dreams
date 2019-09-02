using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Journal : MonoBehaviour
{
    public Scrollbar slider = null;
    
    [SerializeField] private GameObject layoutGroup = null;
    [SerializeField] private Canvas journalCanvas = null;
    [SerializeField] private Toggle pauseTab = null;
    [SerializeField] private Text dayCounter = null;
    [SerializeField] private Text earningsCounter = null;
    [SerializeField] private Animator animator;
    
    private bool _uiOpened;

    public int Days
    {
        get => Convert.ToInt32(dayCounter.text);
        set => dayCounter.text = value.ToString();
    }

    public int EarningsCounter
    {
        get => Convert.ToInt32(earningsCounter.text);
        set => earningsCounter.text = value.ToString();
    }

    private void Start()
    {
        Days = 0;
        EarningsCounter = 0;
    }

    
    private static void Parent( GameObject parentOb, GameObject childOb )
    {
        childOb.transform.SetParent(parentOb.transform, false);
        childOb.transform.localScale = new Vector3(1, 1, 1);
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape) && _uiOpened == false)
        {
            OpenJournal();
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && _uiOpened == true)
        {
            CloseJournal();
        }
        
    }

    public void AddDisplay(QuestDisplay display)
    {
        
        //var newQuest = Instantiate(questPanel);
        //Parent(layoutGroup, newQuest);
        display.transform.SetParent(layoutGroup.transform);
        display.transform.localScale = new Vector3(1, 1, 1);
//        newQuest.GetComponent<JournalQuestPanel>().npcIcon.GetComponent<Image>().sprite = quest.questNPCImage;
//
//        requirementsPanel.GetComponent<RequirementsPanel>().InitializePanel(quest.requirements);
//        foreach (var value in quest.requirements)
//        {
//            var panelVariant = Instantiate(requirementsPanel);
//            var reqLayoutGroup = newQuest.GetComponentInChildren<HorizontalLayoutGroup>().gameObject;
//            Parent(reqLayoutGroup,panelVariant);
//            panelVariant.GetComponent<RequirementsPanel>().InitializePanel(quest.requirements);
//        }
    //newQuest.GetComponent<JournalQuestPanel>().InitializeJournalQuestPanel(questData);
        
    }

    public void OpenJournal()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("IdleState"))
        {
            animator.SetTrigger("Open");

            pauseTab.isOn = true;

            slider.value = 1;

            _uiOpened = true;

            UIStatus.Instance.DialogOpened = true;
        }      
    }
    
    
    private void CloseJournal()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("PopUPPanelIn"))
        animator.SetTrigger("Leave");

        _uiOpened = false;

        UIStatus.Instance.DialogOpened = false;
    }
    
    public void OnExitButtonClick()
    {
        CloseJournal();
    }

}
