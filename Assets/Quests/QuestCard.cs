using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class QuestCard : MonoBehaviour
{

    [SerializeField] private Text description = null;
    [SerializeField] private Image npcIcon = null;
    [SerializeField] private GameObject questPrefab = null;
    [SerializeField] private GameObject requirementSlotPrefab = null;
    [SerializeField] private Transform questParent = null;
    [SerializeField] private AudioSource audioPlayer = null;

    private Quest _quest = null;
    
    private static void Parent( GameObject parentOb, GameObject childOb )
    {
        childOb.transform.SetParent(parentOb.transform, true);
        childOb.transform.localScale = new Vector3(1, 1, 1);
        
    }

    public void InitializeQuestCard(Quest quest)
    {
        _quest = quest;
        description.text = quest.Data.questDescription;
        npcIcon.sprite = quest.Data.questNPCImage;

        var requirementGroups = quest.Data.requirements
            .Select(t => t.GetComponent<PlantState>())
            .GroupBy(t => t.plantObject, t => t.GetComponent<InventoryItem>());

        
        foreach (var requirementGroup in requirementGroups)
        {
            var newSlot = Instantiate(requirementSlotPrefab, questParent)
                .GetComponent<RequirementSlot>();

            newSlot.PlantScriptableObject = requirementGroup.Key;
            newSlot.Amount = requirementGroup.Count();
            newSlot.Icon = requirementGroup.First().Icon;
        }
        
        gameObject.SetActive(true);
        UIStatus.Instance.DialogOpened = true;
        audioPlayer.Play();
    }
    
    
    public void OnExitButtonPressed()
    {
        UIStatus.Instance.DialogOpened = false;
        
        SendMessageUpwards("OnQuestCardClosed", SendMessageOptions.RequireReceiver);
        
        gameObject.SetActive(false);
        
        //_quest.Initialize(_quest.Data);
        
        // Send Quest to DreamPostOffice
    }

    /*public void Update()
    {
        if (gameObject.GetComponent<Canvas>().enabled == false)
        {

            foreach (Transform child in uiRequirementBar.transform) 
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }*/
}

