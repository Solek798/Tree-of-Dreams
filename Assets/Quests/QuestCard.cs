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
    
    
    private static void Parent( GameObject parentOb, GameObject childOb )
    {
        childOb.transform.SetParent(parentOb.transform, true);
        childOb.transform.localScale = new Vector3(1, 1, 1);
        
    }

    public void InitializeQuestCard(QuestData questData)
    {
        description.text = questData.questDescription;
        npcIcon.sprite = questData.questNPCImage;

        var requirementGroups = questData.requirements
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
    }
    
    
    public void OnExitButtonPressed()
    {
        gameObject.SetActive(false);
        
        var newQuest = Instantiate(questPrefab, questParent);
        
        newQuest.GetComponent<Quest>().Initialize(questData);
        
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

