﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UIElements.Image;


public class Lampion : MonoBehaviour
{
    public GameObject player;
    public float maxDistanceToPlayer = 10f;
    public Quest quest;
    public GameObject ui;
    

    [SerializeField] private GameObject npcImage = null;
    [SerializeField] private GameObject uiRequirements = null;
    [SerializeField] private GameObject uiPanel = null;

    private bool _uiOpened;
    
    private void Start()
    {
        _uiOpened = false;
    }


    private static void Parent( GameObject parentOb, GameObject childOb )
    {
        childOb.transform.parent = parentOb.transform;
        childOb.transform.localScale = new Vector3(1, 1, 1);
    }
    

    private void LampionActivation()
    {
        //Get Data of the Scriptable Object
        ui.GetComponentInChildren<Text>().text = quest.questDescription;
        npcImage.GetComponent<UnityEngine.UI.Image>().sprite = quest.questNPCImage;
        
        foreach (var value in quest.requirements)
        {
            var panelVariant = Instantiate(uiPanel);
            Parent(uiRequirements,panelVariant);
        }

        ui.GetComponent<Canvas>().enabled = true;
        _uiOpened = true;
    }

    private void Update()
    {
        var playerInRange = Vector3.Distance(transform.position, player.transform.position);

        if (Input.GetKeyDown(KeyCode.E) && playerInRange <= maxDistanceToPlayer && _uiOpened == false) 
        {
            LampionActivation();
        }


        if (ui.GetComponent<Canvas>().enabled == false)
        {
            _uiOpened = false;
            

            foreach (Transform child in uiRequirements.transform) 
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}