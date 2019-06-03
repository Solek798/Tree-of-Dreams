using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Lampion : MonoBehaviour
{
    public GameObject player;
    public float maxDistanceToPlayer = 10f;
    public Quest quest;
    public GameObject ui;

    private string _text;

    private void Start()
    {
        _text = quest.questDescription;
        //ui.GetComponentInChildren<Image>() = quest.questNPCImage;
        
    }


    private void LampionActivation()
    {
        ui.GetComponent<Canvas>().enabled = true;
    }

    private void Update()
    {
        var playerInRange = Vector3.Distance(transform.position, player.transform.position);

        if (Input.GetKeyDown(KeyCode.E) && playerInRange <= maxDistanceToPlayer)
        {
            LampionActivation();
        }
    }
}
