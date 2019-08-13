﻿using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class QuestDisplay : MonoBehaviour
{
    
    [SerializeField] private Image npcIconUI = null;
    [SerializeField] private GameObject requirementSlotPrefab = null;
    [SerializeField] private HorizontalLayoutGroup requirementsLayoutGroup = null;

    private Quest _quest = null;
    
    
    private static void Parent( GameObject parentOb, GameObject childOb )
    {
        childOb.transform.SetParent(parentOb.transform, true);
        childOb.transform.localScale = new Vector3(1, 1, 1);
        
    }
    
    
    public void Initialize(Quest quest)
    {
        _quest = quest;
        npcIconUI.sprite = quest.Data.questNPCImage;
        
        /*foreach (var requirement in requirements)
        {
            requirement.transform.SetParent(requirementsLayoutGroup.transform);
        }*/
        
        var requirementGroups = quest.Data.requirements
            .Select(t => t.GetComponent<PlantState>())
            .GroupBy(t => t.plantObject, t => t.GetComponent<InventoryItem>());

        
        foreach (var requirementGroup in requirementGroups)
        {
            var newSlot = Instantiate(requirementSlotPrefab, requirementsLayoutGroup.transform)
                .GetComponent<RequirementSlot>();

            newSlot.PlantScriptableObject = requirementGroup.Key;
            newSlot.Amount = requirementGroup.Count();
            newSlot.Icon = requirementGroup.First().Icon;
        }
    }

    public void OnRequirementSatisfactioned(RequirementSlot slot)
    {
        _quest.OnSlotSatisfactioned(slot);
        CheckForFullfillment();
    }

    public void CheckForFullfillment()
    {
        if (requirementsLayoutGroup.GetComponentsInChildren<Toggle>()
            .Any(requirement => !requirement.isOn))
            return;

        GetComponentInChildren<Button>().interactable = true;
    }

    public void OnButtonPressed()
    {
        SendMessageUpwards("OnQuestFillfilled", 
            _quest.Data.rewardDreamEssence, 
            SendMessageOptions.RequireReceiver);
        
        _quest.DestroyAllDisplays();
    }


    public void SetSlotSatisfactioned(RequirementSlot slot)
    {
        foreach (var requirementSlotlot in requirementsLayoutGroup.GetComponentsInChildren<RequirementSlot>())
        {
            if (requirementSlotlot.Icon == slot.Icon)
            {
                requirementSlotlot.MarkAsSatisfactioned();
            }
        }
    }
}