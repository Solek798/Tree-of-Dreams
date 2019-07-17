using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class QuestPanel : MonoBehaviour
{
    public GameObject npcIcon;
    
    [SerializeField] private Image npcIconUI = null;
    [SerializeField] private GameObject requirementSlotPrefab = null;
    [SerializeField] private HorizontalLayoutGroup requirementsLayoutGroup = null;
    
    
    private static void Parent( GameObject parentOb, GameObject childOb )
    {
        childOb.transform.SetParent(parentOb.transform, true);
        childOb.transform.localScale = new Vector3(1, 1, 1);
        
    }
    
    
    public void InitializeQuestPanel(Quest quest)
    {
        npcIconUI.sprite = quest.questNPCImage;


        var requirementGroups = quest.requirements
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

}
