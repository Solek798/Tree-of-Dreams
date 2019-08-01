using System.Linq;
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
    
    
    public void Initialize(Quest quest, GameObject[] requirements)
    {
        _quest = quest;
        npcIconUI.sprite = quest.Data.questNPCImage;
        
        foreach (var requirement in requirements)
        {
            requirement.transform.SetParent(requirementsLayoutGroup.transform);
        }
    }

    public void OnButtonPressed()
    {
        if (requirementsLayoutGroup.GetComponentsInChildren<Toggle>()
            .Any(requirement => !requirement.isOn))
            return;

        SendMessageUpwards("OnQuestFillfilled", 
            _quest.Data.rewardDreamEssence, 
            SendMessageOptions.RequireReceiver);
        
        Destroy(this.gameObject);
    }

}
