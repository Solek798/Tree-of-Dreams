using UnityEngine;
using UnityEngine.UI;


public class QuestPanel : MonoBehaviour
{
    public GameObject npcIcon;
    
    [SerializeField] private Image npcIconUI = null;
    [SerializeField] private GameObject requirementsPanel = null;
    [SerializeField] private HorizontalLayoutGroup requirementsLayoutGroup = null;
    
    
    private static void Parent( GameObject parentOb, GameObject childOb )
    {
        childOb.transform.SetParent(parentOb.transform, true);
        childOb.transform.localScale = new Vector3(1, 1, 1);
        
    }
    
    
    public void InitializeQuestPanel(Quest quest)
    {
        npcIconUI.sprite = quest.questNPCImage;
        
        requirementsPanel.GetComponent<RequirementsPanel>().InitializePanel(quest.requirements);
        
        foreach (var value in quest.requirements)
        {
            var panelVariant = Instantiate(requirementsPanel);
            var reqLayoutGroup = requirementsLayoutGroup.gameObject;
            Parent(reqLayoutGroup,panelVariant);
            panelVariant.GetComponent<RequirementsPanel>().InitializePanel(quest.requirements);
        }
    }

}
