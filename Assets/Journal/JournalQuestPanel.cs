using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalQuestPanel : MonoBehaviour
{
    
    [SerializeField] private UnityEngine.UI.Image npcIconUI = null;
    [SerializeField] private GameObject requirementsPanel = null;
    
    private static void Parent( GameObject parentOb, GameObject childOb )
    {
        childOb.transform.SetParent(parentOb.transform, false);
        childOb.transform.localScale = new Vector3(1, 1, 1);
    }



    public void InitializeJournalQuestPanel(Quest quest)
    {
        npcIconUI.sprite = quest.questNPCImage;

        requirementsPanel.GetComponent<RequirementsPanel>().InitializePanel(quest.requirements);
        foreach (var value in quest.requirements)
        {
            var panelVariant = Instantiate(requirementsPanel);
            var reqLayoutGroup = gameObject.GetComponentInChildren<HorizontalLayoutGroup>().gameObject;
            Parent(reqLayoutGroup,panelVariant);
            panelVariant.GetComponent<RequirementsPanel>().InitializePanel(quest.requirements);
        }
    }

}