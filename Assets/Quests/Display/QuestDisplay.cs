using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class QuestDisplay : MonoBehaviour
{
    
    [SerializeField] protected Image npcIconUI = null;
    [SerializeField] protected GameObject requirementSlotPrefab = null;
    [SerializeField] protected HorizontalLayoutGroup requirementsLayoutGroup = null;
    [SerializeField] protected Animator animator = null;

    protected Quest _quest = null;

    public Quest Quest => _quest;
    
    
    private static void Parent( GameObject parentOb, GameObject childOb )
    {
        childOb.transform.SetParent(parentOb.transform, true);
        childOb.transform.localScale = new Vector3(1, 1, 1);
        
    }
    
    
    public virtual void Initialize(Quest quest)
    {
        _quest = quest;
        npcIconUI.sprite = quest.Data.roundQuestNPCImage;
        
        
        var requirementGroups = quest.Data.requirements
            .Select(t => t.GetComponent<PlantState>())
            .GroupBy(t => t.plantObject, t => t.GetComponent<InventoryItem>());

        
        foreach (var requirementGroup in requirementGroups)
        {
            var newSlot = Instantiate(requirementSlotPrefab, requirementsLayoutGroup.transform)
                .GetComponent<RequirementSlot>();

            newSlot.PlantScriptableObject = requirementGroup.Key;
            newSlot.Display = this;
            newSlot.Amount = requirementGroup.Count();
            newSlot.Icon = requirementGroup.First().Icon;
        }
    }

    public void OnSlotChanged(RequirementSlot slot)
    {
        _quest.OnSlotChanged(slot);
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
        SendMessageUpwards("OnQuestFulfilled");
        animator.Play("Fulfilled");
    }


    public void UpdateSlot(RequirementSlot slot)
    {
        foreach (var requirementSlot in requirementsLayoutGroup.GetComponentsInChildren<RequirementSlot>())
        {
            if (requirementSlot.Icon == slot.Icon)
            {
                if (slot.Amount > 0)
                {
                    requirementSlot.Amount = slot.Amount;
                }
                else
                    requirementSlot.MarkAsSatisfactioned();
            }
        }
    }

    public void OnSelectionCHanged(bool isChecked)
    {
        if (isChecked)
        {
            SendMessageUpwards("OnSelectedDisplayChanged", 
                this, 
                SendMessageOptions.DontRequireReceiver);
        }
    }

    public void OnAnimatonEnd()
    {
        _quest.MarkAsFulfilled();
    }
}
