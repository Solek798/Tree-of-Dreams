using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalQuestPanel : MonoBehaviour
{
    
    [SerializeField] private Image npcIconUI = null;
    [SerializeField] private GameObject requirementsPanel = null;
    
    private static void Parent( GameObject parentOb, GameObject childOb )
    {
        childOb.transform.SetParent(parentOb.transform, false);
        childOb.transform.localScale = new Vector3(1, 1, 1);
    }



    public void InitializeJournalQuestPanel(QuestData questData)
    {
        npcIconUI.sprite = questData.questNPCImage;

        requirementsPanel.GetComponent<RequirementsPanel>().InitializePanel(questData.requirements);
        foreach (var value in questData.requirements)
        {
            var panelVariant = Instantiate(requirementsPanel);
            var reqLayoutGroup = gameObject.GetComponentInChildren<HorizontalLayoutGroup>().gameObject;
            Parent(reqLayoutGroup,panelVariant);
            panelVariant.GetComponent<RequirementsPanel>().InitializePanel(questData.requirements);
        }
    }

}