using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class Quest : MonoBehaviour
{
    public GameObject npcIcon;
    
    [SerializeField] private Image npcIconUI = null;
    [SerializeField] private GameObject requirementSlotPrefab = null;
    [SerializeField] private HorizontalLayoutGroup requirementsLayoutGroup = null;

    private QuestData _questData = null;
    
    
    private static void Parent( GameObject parentOb, GameObject childOb )
    {
        childOb.transform.SetParent(parentOb.transform, true);
        childOb.transform.localScale = new Vector3(1, 1, 1);
        
    }
    
    
    public void Initialize(QuestData questData, GameObject[] requirements)
    {
        _questData = questData;
        npcIconUI.sprite = questData.questNPCImage;
        
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
            _questData.rewardDreamEssence, 
            SendMessageOptions.RequireReceiver);
        
        Destroy(this.gameObject);
    }

}
