using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UIElements.Image;

public class QuestCard : MonoBehaviour
{

    [SerializeField] private Text description = null;
    [SerializeField] private UnityEngine.UI.Image npcIcon = null;
    [SerializeField] private GameObject uiPanelPrefab = null;
    [SerializeField] private GameObject uiRequirementBar = null;
    
    
    private static void Parent( GameObject parentOb, GameObject childOb )
    {
        childOb.transform.SetParent(parentOb.transform, true);
        childOb.transform.localScale = new Vector3(1, 1, 1);
        
    }

    public void InitializeQuestCard(Quest _quest)
    {
        description.text = _quest.questDescription;
        npcIcon.sprite = _quest.questNPCImage;

        
        foreach (var value in _quest.requirements)
        {
            var panelVariant = Instantiate(uiPanelPrefab);
            Parent(uiRequirementBar,panelVariant);
            uiPanelPrefab.GetComponent<RequirementsPanel>().InitializePanel(_quest.requirements);
        }
        gameObject.GetComponent<Canvas>().enabled = true;
    }
    
    
    public void OnExitButtonPressed()
    {
        GetComponent<Canvas>().enabled = false;
    }

    public void Update()
    {
        if (gameObject.GetComponent<Canvas>().enabled == false)
        {

            foreach (Transform child in uiRequirementBar.transform) 
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}

